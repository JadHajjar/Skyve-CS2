using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class ContentManager : IContentManager
{
	//public const string EXCLUDED_FILE_NAME = ".excluded";

	private readonly object _contentUpdateLock = new();

	private readonly IPackageManager _packageManager;
	private readonly ILocationService _locationManager;
	private readonly ICompatibilityManager _compatibilityManager;
	private readonly IPackageUtil _packageUtil;
	private readonly IModUtil _modUtil;
	private readonly IAssetUtil _assetUtil;
	private readonly ILogger _logger;
	private readonly INotifier _notifier;
	private readonly ISettings _settings;
	private readonly WorkshopService _workshopService;

	public ContentManager(IPackageManager packageManager, ILocationService locationManager, ICompatibilityManager compatibilityManager, ILogger logger, INotifier notifier, IModUtil modUtil, IAssetUtil assetUtil, IPackageUtil packageUtil, ISettings settings, IWorkshopService workshopService)
	{
		_packageManager = packageManager;
		_locationManager = locationManager;
		_compatibilityManager = compatibilityManager;
		_packageUtil = packageUtil;
		_modUtil = modUtil;
		_assetUtil = assetUtil;
		_logger = logger;
		_notifier = notifier;
		_settings = settings;
		_workshopService = (WorkshopService)workshopService;
	}

	public IEnumerable<IPackage> GetReferencingPackage(ulong steamId, bool includedOnly)
	{
		foreach (var item in _packageManager.Packages)
		{
			if (includedOnly && !(_packageUtil.IsIncluded(item, out var partiallyIncluded) || partiallyIncluded))
			{
				continue;
			}

			var crData = _compatibilityManager.GetPackageInfo(item);

			if (crData == null)
			{
				if (item.GetWorkshopInfo()?.Requirements.Any(x => x.Id == steamId) == true)
				{
					yield return item;
				}
			}
			else if (crData.Interactions?.Any(x => x.Type == InteractionType.RequiredPackages && (x.Packages?.Contains(steamId) ?? false)) ?? false)
			{
				yield return item;
			}
		}
	}

	public static DateTime GetLocalUpdatedTime(string path)
	{
		var dateTime = DateTime.MinValue;

		try
		{
			if (Directory.Exists(path))
			{
				foreach (var filePAth in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
				{
					var lastWriteTimeUtc = File.GetLastWriteTimeUtc(filePAth);

					if (lastWriteTimeUtc > dateTime)
					{
						dateTime = lastWriteTimeUtc;
					}
				}
			}
		}
		catch { }

		return dateTime;
	}

	public static DateTime GetLocalSubscribeTime(string path)
	{
		var dateTime = DateTime.MaxValue;

		if (Directory.Exists(path))
		{
			foreach (var filePAth in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
			{
				var lastWriteTimeUtc = File.GetCreationTimeUtc(filePAth);

				if (lastWriteTimeUtc < dateTime)
				{
					dateTime = lastWriteTimeUtc;
				}
			}
		}

		return dateTime;
	}

	public static long GetTotalSize(string path)
	{
		try
		{
			if (Directory.Exists(path))
			{
				return new DirectoryInfo(path)
					.GetFiles("*", SearchOption.AllDirectories)
					.Sum(f => f.Length);
			}
		}
		catch { }

		return 0;
	}

	public async Task<List<IPackage>> LoadContents()
	{
		var packages = new List<IPackage>();
		var gameModsPath = CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Mods");

		if (Directory.Exists(gameModsPath))
		{
			_logger.Info($"Looking for packages in: '{gameModsPath}'");

			foreach (var folder in Directory.GetDirectories(gameModsPath))
			{
				var package = GetPackage(folder);

				if (package is not null)
				{
					packages.Add(package);
				}
			}
		}
		else
		{
			_logger.Warning($"Folder not found: '{gameModsPath}'");
		}

		var subscribedItems = await _workshopService.GetLocalPackages();

		foreach (var mod in subscribedItems)
		{
			if (mod.LocalData is null)
			{
				packages.Add(new PdxPackage(mod));
			}
			else if (mod.LocalData.LocalType == LocalType.Subscribed)
			{
				var pdxPackage = GetPackage(mod.LocalData.FolderAbsolutePath, true, mod);

				if (pdxPackage is not null)
				{
					packages.Add(pdxPackage);
				}
			}
		}

		return packages;
	}

	internal Package? GetPackage(string folder, bool withSubDirectories = true, PDX.SDK.Contracts.Service.Mods.Models.Mod? pdxMod = null)
	{
		try
		{
			if (Regex.IsMatch(Path.GetFileName(folder), ExtensionClass.CharBlackListPattern))
			{
				_logger.Warning($"Package folder contains blacklisted characters: '{folder}'");

				return null;
			}

			if (!Directory.Exists(folder))
			{
				_logger.Warning($"Package folder not found: '{folder}'");

				return null;
			}

#if DEBUG
			_logger.Debug("Creating package for: " + folder);
#endif

			var isCodeMod = _modUtil.GetModInfo(folder, out var modDll, out var version);
			var assets = _assetUtil.GetAssets(folder, withSubDirectories).ToArray();

			return pdxMod is null
				? new Package(folder,
				assets,
				isCodeMod,
				version.GetString(),
				modDll)
				: new LocalPdxPackage(pdxMod,
					assets,
					isCodeMod,
					version?.GetString(),
					modDll);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Failed to create a package from the folder: '{folder}'");

			return null;
		}
	}

	public void ContentUpdated(string path, bool builtIn, bool workshop, bool self)
	{
		lock (_contentUpdateLock)
		{
			//if ((!workshop &&
			//	!path.PathContains(_locationManager.AssetsPath) &&
			//	!path.PathContains(_locationManager.StylesPath) &&
			//	!path.PathContains(_locationManager.MapThemesPath) &&
			//	!path.PathContains(_locationManager.ModsPath)) ||
			//	path.PathEquals(_locationManager.ModsPath))
			//{
			//	return;
			//}

			var existingPackage = _packageManager.Packages.FirstOrDefault(x => x.LocalData?.Folder.PathEquals(path) ?? false);

			if (existingPackage is Package package)
			{
				RefreshPackage(package, self);
			}
			else
			{
				var newPackage = GetPackage(path, self);

				if (newPackage is null)
				{
					return;
				}

				if (newPackage.IsCodeMod)
				{
					ServiceCenter.Get<IModLogicManager>().Analyze(newPackage, _modUtil);
				}

				_packageManager.AddPackage(newPackage);
			}
		}
	}

	public void RefreshPackage(IPackage localPackage, bool self)
	{
		if (localPackage is not Package package)
		{
			return;
		}

		if (IsDirectoryEmpty(package.LocalData.Folder))
		{
			_packageManager.RemovePackage(package);
			return;
		}

		var isCodeMod = _modUtil.GetModInfo(package.LocalData.Folder, out var modDll, out var version);
		var assets = _assetUtil.GetAssets(package.LocalData.Folder, !self).ToArray();

		package.RefreshData(
			assets,
			isCodeMod,
			version.GetString(),
			modDll);

		if (package.IsLocal && !package.IsCodeMod)
		{
			_notifier.OnContentLoaded();
		}

		_notifier.OnInformationUpdated();
	}

	private bool IsDirectoryEmpty(string path)
	{
		return !Directory.Exists(path) || GetTotalSize(path) == 0;
	}

	public void StartListeners()
	{
		PackageWatcher.Dispose();

		//PackageWatcher.Create(_locationManager.ModsPath, false, false);

		//PackageWatcher.Create(_locationManager.WorkshopContentPath, false, true);
	}
}
