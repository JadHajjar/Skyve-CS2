using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;

using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.CS2.Utilities;
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
	private readonly IModLogicManager _modLogicManager;
	private readonly IUpdateManager _updateManager;
	private readonly ISkyveDataManager _skyveDataManager;
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly WorkshopService _workshopService;

	public ContentManager(IPackageManager packageManager, ILocationService locationManager, ICompatibilityManager compatibilityManager, ILogger logger, INotifier notifier, IModUtil modUtil, IAssetUtil assetUtil, IPackageUtil packageUtil, ISettings settings, IWorkshopService workshopService, IModLogicManager modLogicManager, IUpdateManager updateManager, ISkyveDataManager skyveDataManager, ISubscriptionsManager subscriptionsManager)
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
		_modLogicManager = modLogicManager;
		_updateManager = updateManager;
		_skyveDataManager = skyveDataManager;
		_workshopService = (WorkshopService)workshopService;

		_notifier.WorkshopSyncEnded += _notifier_WorkshopSyncEnded;
		_subscriptionsManager = subscriptionsManager;
	}

	private async void _notifier_WorkshopSyncEnded()
	{
		_packageManager.SetPackages(await LoadContents());

		_notifier.OnContentLoaded();
	}

	//public IEnumerable<IPackage> GetReferencingPackage(ulong steamId, bool includedOnly)
	//{
	//	foreach (var item in _packageManager.Packages)
	//	{
	//		if (includedOnly && !(_packageUtil.IsIncluded(item, out var partiallyIncluded) || partiallyIncluded))
	//		{
	//			continue;
	//		}

	//		var crData = _compatibilityManager.GetPackageInfo(item);

	//		if (crData == null)
	//		{
	//			if (item.GetWorkshopInfo()?.Requirements.Any(x => x.Id == steamId) == true)
	//			{
	//				yield return item;
	//			}
	//		}
	//		else if (crData.Interactions?.Any(x => x.Type == InteractionType.RequiredPackages && (x.Packages?.Contains(steamId) ?? false)) ?? false)
	//		{
	//			yield return item;
	//		}
	//	}
	//}

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
		var gameSavesPath = CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Saves");
		var gameMapsPath = CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Maps");

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

		var savedPackage = GetPackage(gameSavesPath, true, null);
		var mapsPackage = GetPackage(gameMapsPath, true, null);

		if (savedPackage is not null)
		{
			packages.Add(savedPackage);
		}

		if (mapsPackage is not null)
		{
			packages.Add(mapsPackage);
		}

		var subscribedItems = await _workshopService.GetLocalPackages();

		foreach (var mod in subscribedItems.OrderBy(x => x.LocalData is null).Distinct(x => x.Id))
		{
			if (mod.LocalData is null)
			{
				//packages.Add(new PdxPackage(mod));
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

		try
		{

			_logger.Info($"Analyzing packages..");

			await AnalyzePackages(packages);

			_logger.Info($"Finished analyzing packages..");
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to analyze packages");
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

			if (pdxMod is not null)
			{
				return new LocalPdxPackage(pdxMod,
					assets,
					isCodeMod,
					version?.GetString(),
					modDll);
			}

			return new Package(folder,
				assets,
				[],
				isCodeMod,
				version?.GetString(),
				modDll,
				null);
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
					_modLogicManager.Analyze(newPackage, _modUtil);
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
			modDll,
			localPackage.LocalData?.SuggestedGameVersion);

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

	private async Task AnalyzePackages(List<IPackage> packages)
	{
		var blackList = new List<IPackage>();
		var firstTime = _updateManager.IsFirstTime();

		_modLogicManager.Clear();

		_notifier.IsBulkUpdating = true;

		foreach (var package in packages)
		{
			if (_skyveDataManager.IsBlacklisted(package))
			{
				blackList.Add(package);
				continue;
			}

			if (package.IsCodeMod)
			{
				if (_settings.UserSettings.LinkModAssets && package.LocalData is not null)
				{
					await _packageUtil.SetIncluded(package.LocalData.Assets, _modUtil.IsIncluded(package));
				}

				_modLogicManager.Analyze(package, _modUtil);

				if (!firstTime && !_updateManager.IsPackageKnown(package.LocalData!))
				{
					await _modUtil.SetEnabled(package, _modUtil.IsIncluded(package));
				}
			}
		}

		_notifier.IsBulkUpdating = false;
		_modUtil.SaveChanges();
		_assetUtil.SaveChanges();

		packages.RemoveAll(blackList.Contains);

		foreach (var item in blackList)
		{
			_packageManager.DeleteAll(item.LocalData!.Folder);
		}

		if (blackList.Count > 0)
		{
			await _workshopService.UnsubscribeBulkCompletely(blackList.Select(x => (int)x.Id));
		}

		_logger.Info($"Applying analysis results..");

		_modLogicManager.ApplyRequiredStates(_modUtil);
	}
}
