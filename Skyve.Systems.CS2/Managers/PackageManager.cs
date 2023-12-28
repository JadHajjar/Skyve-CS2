using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Skyve.Systems.CS2.Managers;
internal class PackageManager : IPackageManager
{
	private readonly Dictionary<ulong, IPackage> indexedPackages = new();
	private readonly Dictionary<string, List<IPackage>> indexedMods = new();
	private readonly List<Package> packages = new();

	private readonly IModLogicManager _modLogicManager;
	private readonly ISettings _settings;
	private readonly ILogger _logger;
	private readonly INotifier _notifier;
	private readonly ILocationService _locationManager;

	public PackageManager(IModLogicManager modLogicManager, ISettings settings, ILogger logger, INotifier notifier, ILocationService locationManager)
	{
		_modLogicManager = modLogicManager;
		_settings = settings;
		_logger = logger;
		_notifier = notifier;
		_locationManager = locationManager;
	}

	public IEnumerable<IPackage> Packages
	{
		get
		{
			var currentPackages = packages is null ? new() : new List<Package>(packages);
			
			if (_settings.UserSettings.HidePseudoMods)
			{
				foreach (var package in currentPackages)
				{
					if (_modLogicManager.IsPseudoMod(package))
						continue;

					yield return package;
				}

				yield break;
			}

			foreach (var package in currentPackages)
			{
				yield return package;
			}
		}
	}

	public IEnumerable<IAsset> Assets
	{
		get
		{
			var currentPackages = packages is null ? new() : new List<Package>(packages);

			if (_settings.UserSettings.HidePseudoMods)
			{
				foreach (var package in currentPackages)
				{
					if (_modLogicManager.IsPseudoMod(package))
						continue;

					foreach (var item in package.LocalData.Assets)
					{
						yield return item;
					}
				}

				yield break;
			}

			foreach (var package in currentPackages)
			{
				foreach (var item in package.LocalData!.Assets)
				{
					yield return item;
				}
			}
		}
	}

	public void AddPackage(IPackage package)
	{
		packages.Add((Package)package);

		if (indexedPackages is not null && package.Id != 0)
		{
			indexedPackages[package.Id] = package;
		}

		if (package.IsCodeMod)
		{
			indexedMods.GetOrAdd(Path.GetFileName(package.LocalData!.FilePath)).Add(package);
		}

		_notifier.OnInformationUpdated();
		_notifier.OnContentLoaded();
	}

	public void RemovePackage(IPackage package)
	{
		packages.Remove((Package)package);
		indexedPackages.Remove(package.Id);

		if (package.IsCodeMod)
		{
			_modLogicManager.ModRemoved(package);
		}

		if (package.IsCodeMod)
		{
			indexedMods.GetOrAdd(Path.GetFileName(package.LocalData!.FilePath)).Remove(package);
		}

		_notifier.OnContentLoaded();
		_notifier.OnWorkshopInfoUpdated();

		DeleteAll(package.LocalData!.Folder);
	}

	public IPackage? GetPackageById(IPackageIdentity identity)
	{
		if (identity is ILocalPackageIdentity localPackageIdentity)
		{
			var folder = _locationManager.ToLocalPath(localPackageIdentity.Folder);
			var matchedPackage = Packages.FirstOrDefault(x => x.LocalData!.Folder == folder);

			if (matchedPackage is not null)
				return matchedPackage;
		}

		if (indexedPackages?.TryGetValue(identity.Id, out var package) ?? false)
		{
			return package;
		}

		return null;
	}

	public IPackage? GetPackageByFolder(string folder)
	{
		return Packages.FirstOrDefault(x => x.LocalData!.Folder.PathEquals(folder));
	}

	public void SetPackages(List<IPackage> content)
	{
		packages.Clear();
		packages.AddRange(content.Cast<Package>());

		indexedPackages.Clear();
		indexedPackages.AddRange(content
			.OrderBy(x => !x.IsLocal)
			.GroupBy(x => x.Id)
			.ToDictionary(x => x.Key, x => x.First()));

		indexedPackages.Remove(0);

		indexedMods.Clear();
		indexedMods.AddRange(content.Where(x => x.IsCodeMod)
			.GroupBy(x => Path.GetFileName(x.LocalData!.FilePath))
			.ToDictionary(x => x.Key, x => x.ToList())!);
	}

	//public void DeleteAll(IEnumerable<ulong> ids)
	//{
	//	foreach (var id in ids.ToList())
	//	{
	//		DeleteAll(CrossIO.Combine(_locationManager.WorkshopContentPath, id.ToString()));
	//	}
	//}

	public void DeleteAll(string folder)
	{
		var package = Packages.FirstOrDefault(x => x.LocalData!.Folder.PathEquals(folder));

		if (package != null)
		{
			RemovePackage(package);
		}

		PackageWatcher.Pause();
		try
		{ CrossIO.DeleteFolder(folder); }
		catch (Exception ex) { _logger.Exception(ex, $"Failed to delete the folder '{folder}'"); }
		PackageWatcher.Resume();
	}

	public void MoveToLocalFolder(IPackage item)
	{
		throw new NotImplementedException();
		//if (item is Asset asset)
		//{
		//	CrossIO.CopyFile(asset.FilePath, CrossIO.Combine(_locationManager.AssetsPath, Path.GetFileName(asset.FilePath)), true);
		//	return;
		//}

		//if (item.GetLocalPackage()?.Assets?.Any() ?? false)
		//{
		//	var target = new DirectoryInfo(CrossIO.Combine(_locationManager.AssetsPath, Path.GetFileName(item.Folder)));

		//	new DirectoryInfo(item.Folder).CopyAll(target, x => Path.GetExtension(x).Equals(".crp", StringComparison.CurrentCultureIgnoreCase));

		//	target.RemoveEmptyFolders();
		//}

		//if (item.GetLocalPackage()?.Mod is not null)
		//{
		//	var target = new DirectoryInfo(CrossIO.Combine(_locationManager.ModsPath, Path.GetFileName(item.Folder)));

		//	new DirectoryInfo(item.Folder).CopyAll(target, x => !Path.GetExtension(x).Equals(".crp", StringComparison.CurrentCultureIgnoreCase));

		//	target.RemoveEmptyFolders();
		//}
	}

	public List<IPackage> GetModsByName(string modName)
	{
		if (indexedMods?.TryGetValue(modName, out var mods) == true)
		{
			return mods;
		}

		return new();
	}
}
