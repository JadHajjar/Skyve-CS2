using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

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
	private readonly IWorkshopService _workshopService;

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
		_workshopService = workshopService;
	}

	public IEnumerable<ILocalPackageData> GetReferencingPackage(ulong steamId, bool includedOnly)
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
				if (item.Requirements.Any(x => x.Id == steamId))
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
				return new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories).Sum(f => f.Length);
			}
		}
		catch { }

		return 0;
	}

	public async Task<List<ILocalPackageData>> LoadContents()
	{
		var packages = new List<ILocalPackageData>();
		var gameModsPath = CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Mods");

		if (Directory.Exists(gameModsPath))
		{
			_logger.Info($"Looking for packages in: '{gameModsPath}'");

			foreach (var folder in Directory.GetDirectories(gameModsPath))
			{
				getPackage(folder, false, false, false);
			}
		}
		else
		{
			_logger.Warning($"Folder not found: '{gameModsPath}'");
		}

		var subscribedItems = await _workshopService.GetInstalledPackages();

		lock (packages)
		{
			packages.AddRange(subscribedItems);
		}

		return packages;

		void getPackage(string folder, bool builtIn, bool workshop, bool expectAssets, bool withSubDirectories = true)
		{
			try
			{
				if (Regex.IsMatch(Path.GetFileName(folder), ExtensionClass.CharBlackListPattern))
				{
					_logger.Warning($"Package folder contains blacklisted characters: '{folder}'");

					return;
				}

				if (!Directory.Exists(folder))
				{
					_logger.Warning($"Package folder not found: '{folder}'");

					return;
				}

#if DEBUG
				_logger.Debug("Creating package for: " + folder);
#endif

				var package = new Package(folder, GetTotalSize(folder), GetLocalUpdatedTime(folder));

				package.Assets = _assetUtil.GetAssets(package, withSubDirectories).ToArray();
				package.Mod = expectAssets ? null : _modUtil.GetMod(package);

				if (package.Assets.Length != 0 || package.Mod != null)
				{
					lock (packages)
					{
						packages.Add(package);
					}
				}
#if DEBUG
				else
				{
					_logger.Debug("No mods/assets found in: " + folder);
				}
#endif
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, $"Failed to create a package from the folder: '{folder}'");
			}
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

			var existingPackage = _packageManager.Packages.FirstOrDefault(x => x.Folder.PathEquals(path));

			if (existingPackage is Package package)
			{
				RefreshPackage(package, self);
			}
			else
			{
				AddNewPackage(path, builtIn, workshop, self);
			}
		}
	}

	private void AddNewPackage(string path, bool builtIn, bool workshop, bool self)
	{
		if (workshop && !ulong.TryParse(Path.GetFileName(path), out _))
		{
			return;
		}

		var package = new Package(path, GetTotalSize(path), GetLocalUpdatedTime(path));

		package.Assets = _assetUtil.GetAssets(package, !self).ToArray();
		package.Mod = _modUtil.GetMod(package);

		if (package.Mod is not null)
		{
			ServiceCenter.Get<IModLogicManager>().Analyze(package.Mod, _modUtil);
		}

		_packageManager.AddPackage(package);
	}

	public void RefreshPackage(ILocalPackageData localPackage, bool self)
	{
		if (localPackage is not Package package)
		{
			return;
		}

		if (IsDirectoryEmpty(package.Folder))
		{
			_packageManager.RemovePackage(package);
			return;
		}

		package.Assets = _assetUtil.GetAssets(package, !self).ToArray();
		package.Mod = _modUtil.GetMod(package);
		package.LocalSize = GetTotalSize(package.Folder);
		package.LocalTime = GetLocalUpdatedTime(package.Folder);

		if (package.IsLocal && package.Mod is null)
		{
			_notifier.OnContentLoaded();
		}

		_notifier.OnInformationUpdated();
	}

	private bool IsDirectoryEmpty(string path)
	{
		if (!Directory.Exists(path))
		{
			return true;
		}

		return GetTotalSize(path) == 0;
	}

	public void StartListeners()
	{
		PackageWatcher.Dispose();

		//PackageWatcher.Create(_locationManager.ModsPath, false, false);

		//PackageWatcher.Create(_locationManager.WorkshopContentPath, false, true);
	}
}
