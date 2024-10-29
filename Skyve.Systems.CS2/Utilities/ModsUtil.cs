using Extensions;

using Microsoft.Extensions.DependencyInjection;

using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;
using Skyve.Systems.CS2.Utilities.IO;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
internal class ModsUtil : IModUtil
{
	private int currentPlayset;
	private int currentHistoryIndex;
	private ModStateCollection modConfig = new();
	private readonly List<ModStateCollection> _modConfigHistory = [];
	private readonly List<ulong> _enabling = [];

	private readonly AssemblyUtil _assemblyUtil;
	private readonly MacAssemblyUtil _macAssemblyUtil;
	private readonly WorkshopService _workshopService;
	private readonly IModLogicManager _modLogicManager;
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly INotifier _notifier;
	private readonly ISettings _settings;
	private readonly ILogger _logger;
	private readonly IServiceProvider _serviceProvider;
	private readonly IInterfaceService _interfaceService;

	public ModsUtil(IWorkshopService workshopService, ISubscriptionsManager subscriptionsManager, IModLogicManager modLogicManager, INotifier notifier, AssemblyUtil assemblyUtil, MacAssemblyUtil macAssemblyUtil, ISettings settings, IServiceProvider serviceProvider, ILogger logger, IInterfaceService interfaceService)
	{
		_assemblyUtil = assemblyUtil;
		_workshopService = (WorkshopService)workshopService;
		_modLogicManager = modLogicManager;
		_macAssemblyUtil = macAssemblyUtil;
		_subscriptionsManager = subscriptionsManager;
		_notifier = notifier;
		_settings = settings;
		_logger = logger;
		_serviceProvider = serviceProvider;
		_interfaceService = interfaceService;
		_notifier.CompatibilityDataLoaded += BuildLoadOrder;
		_notifier.PlaysetChanged += _notifier_PlaysetChanged;
		_notifier.WorkshopSyncEnded += async () => await RefreshModConfig();
	}

	public bool IsEnabling(IPackageIdentity package)
	{
		return _enabling.Contains(package.Id);
	}

	private void _notifier_PlaysetChanged()
	{
		currentPlayset = _serviceProvider.GetService<IPlaysetManager>()?.CurrentPlayset?.Id ?? 0;
	}

	private async Task RefreshModConfig()
	{
		SaveHistory();

		var mods = await _workshopService.GetLocalPackages();

		var config = new ModStateCollection();

		foreach (var mod in mods)
		{
			foreach (var item in mod.Playsets)
			{
				config.SetState(item.PlaysetId
					, new GenericPackageIdentity { Id = (ulong)mod.Id, Name = mod.Name }
					, item.ModIsEnabled
					, item.Version);
			}
		}

		modConfig = config;
	}

	private async void BuildLoadOrder()
	{
		if (!_notifier.IsContentLoaded || !_workshopService.IsAvailable)
		{
			return;
		}

		var index = 1;
		var mods = _serviceProvider.GetService<ILoadOrderHelper>()?.GetOrderedMods().OfType<IMod>();
		var orderedMods = new List<ModLoadOrder>();
		var playset = await _workshopService.GetActivePlaysetId();

		if (mods is null)
		{
			return;
		}

		foreach (var mod in mods.Distinct(x => x.Id))
		{
			orderedMods.Add(new ModLoadOrder
			{
				Mod = mod,
				LoadOrder = index++,
			});
		}

		await _workshopService.SetLoadOrder(orderedMods, playset);
	}

	public void SaveChanges()
	{
		if (_notifier.IsApplyingPlayset || _notifier.IsBulkUpdating)
		{
			return;
		}

		//lock (this)
		//{
		//	_config.SetModsInfo(_modConfigInfo);
		//}

		//_config.Save();
	}

	public bool IsIncluded(IPackageIdentity mod, int? playsetId = null)
	{
		if (mod.Id <= 0)
		{
			return mod is LocalPdxPackage ? modConfig.IsIncluded(playsetId ?? currentPlayset, mod.Name) : IsEnabled(mod);
		}

		if (!modConfig.IsIncluded(playsetId ?? currentPlayset, mod.Id))
		{
			return false;
		}

		return mod.GetPackage() is not LocalPdxPackage localPdxPackage
|| modConfig.GetVersion(playsetId ?? currentPlayset, mod.Id) == localPdxPackage.Version;
	}

	public bool IsEnabled(IPackageIdentity mod, int? playsetId = null)
	{
		if (mod.Id <= 0)
		{
			if (mod is LocalPdxPackage)
			{
				return modConfig.IsEnabled(playsetId ?? currentPlayset, mod.Name);
			}

			var folder = mod.GetLocalPackageIdentity()?.Folder;

			return folder is null or "" || Path.GetFileName(folder)[0] != '.';
		}

		if (!modConfig.IsEnabled(playsetId ?? currentPlayset, mod.Id))
		{
			return false;
		}

		return mod.GetPackage() is not LocalPdxPackage localPdxPackage
|| modConfig.GetVersion(playsetId ?? currentPlayset, mod.Id) == localPdxPackage.Version;
	}

	public async Task SetIncluded(IPackageIdentity mod, bool value, int? playsetId = null)
	{
		await SetIncluded([mod], value, playsetId);
	}

	public async Task SetIncluded(IEnumerable<IPackageIdentity> mods, bool value, int? playsetId = null)
	{
		if (!_workshopService.IsAvailable)
		{
			return;
		}

		var playset = playsetId ?? currentPlayset;

		if (playset <= 0)
		{
			return;
		}

		SaveHistory();

		mods = mods.Where(x => !_modLogicManager.IsRequired(x.GetLocalPackageIdentity(), this));

		await SetLocalModIncluded(mods.AllWhere(x => x.Id <= 0 && IsIncluded(x, playset) != value), playset, value);

		mods = mods.AllWhere(x => x.Id > 0 && IsIncluded(x, playset) != value);

		if (!mods.Any())
		{
			return;
		}

		if (value && mods is List<IPackageIdentity> modList)
		{
			switch (_settings.UserSettings.DependencyResolution)
			{
				case Skyve.Domain.Enums.DependencyResolveBehavior.Automatic:
					modList.AddRange(await ResolveDependencies(modList, playsetId));
					break;
				case Skyve.Domain.Enums.DependencyResolveBehavior.Ask:
					var dependencies = await ResolveDependencies(modList, playsetId);

					if (dependencies.Count > 0 && _interfaceService.AskForDependencyConfirmation(modList, dependencies))
					{
						modList.AddRange(dependencies);
					}

					break;
			}
		}

		var tempConfig = modConfig.CreateFragment(playset);
		var result = value
			? await _subscriptionsManager.Subscribe(mods, playset)
			: await _subscriptionsManager.UnSubscribe(mods, playset);

		if (result)
		{
			//if (value)
			//{
			//	await SetEnabled(mods, !_settings.UserSettings.DisableNewModsByDefault, playsetId);
			//}

			foreach (var item in mods)
			{
				if (item.Id <= 0)
				{
					continue;
				}

				if (value)
				{
					modConfig.SetEnabled(playset, item.Id, !_settings.UserSettings.DisableNewModsByDefault);
				}
				else
				{
					modConfig.Remove(playset, item.Id);
				}
			}

			SaveHistory();
		}

		if (_notifier.IsApplyingPlayset || _notifier.IsBulkUpdating)
		{
			return;
		}

		_notifier.OnInclusionUpdated();
		_notifier.OnRefreshUI(true);
	}

	public async Task SetEnabled(IPackageIdentity mod, bool value, int? playsetId = null)
	{
		await SetEnabled([mod], value, playsetId);
	}

	public async Task SetEnabled(IEnumerable<IPackageIdentity> mods, bool value, int? playsetId = null)
	{
		if (!_workshopService.IsAvailable)
		{
			return;
		}

		var playset = playsetId ?? currentPlayset;

		if (playset <= 0)
		{
			return;
		}

		SaveHistory();

		mods = mods.Where(x => !_modLogicManager.IsRequired(x.GetLocalPackageIdentity(), this));

		await SetLocalModEnabled(mods.AllWhere(x => x.Id <= 0 && IsEnabled(x, playset) != value), playset, value);

		mods = mods.AllWhere(x => x.Id > 0 && IsIncluded(x, playset) && IsEnabled(x, playset) != value);

		if (!mods.Any())
		{
			return;
		}

		_enabling.AddRange(mods.Select(x => x.Id).Where(x => x > 0));

		_notifier.OnRefreshUI(true);

		var modKeys = mods.ToList(x => (int)x.Id).DistinctList();

		await _workshopService.WaitUntilReady();

		bool result;
		using (_workshopService.Lock)
		{
			result = await _workshopService.SetEnableBulk(modKeys, playset, value);
		}

		if (result)
		{
			foreach (var item in mods)
			{
				if (item.Id <= 0)
				{
					continue;
				}

				modConfig.SetEnabled(playset, item.Id, value);
			}

			SaveHistory();
		}

		_enabling.RemoveAll(x => mods.Any(y => y.Id == x));

		if (_notifier.IsApplyingPlayset || _notifier.IsBulkUpdating)
		{
			return;
		}

		_notifier.OnInclusionUpdated();
		_notifier.OnRefreshUI(true);
	}

	private async Task SetLocalModIncluded(List<IPackageIdentity> mods, int playset, bool value)
	{
		if (mods.Count == 0)
		{
			return;
		}

		foreach (var item in mods)
		{
			if (item is LocalPdxPackage)
			{
				continue;
			}

			var localIdentity = item.GetLocalPackageIdentity();

			if (localIdentity is null)
			{
				continue;
			}

			SetLocalFolderIncluded(value, item, localIdentity);
		}

		var pdxMods = mods.Where(x => x is LocalPdxPackage).ToList(x => x.Name);

		if (value)
		{
			await _workshopService.SubscribeBulk(pdxMods, playset);

			pdxMods.ForEach(x => modConfig.SetEnabled(playset, x, true));
		}
		else
		{
			await _workshopService.UnsubscribeBulk(pdxMods, playset);

			pdxMods.ForEach(x => modConfig.Remove(playset, x));
		}
	}

	private async Task SetLocalModEnabled(List<IPackageIdentity> mods, int playset, bool value)
	{
		if (mods.Count == 0)
		{
			return;
		}

		foreach (var item in mods)
		{
			if (item is LocalPdxPackage)
			{
				continue;
			}

			var localIdentity = item.GetLocalPackageIdentity();

			if (localIdentity is null)
			{
				continue;
			}

			SetLocalFolderIncluded(value, item, localIdentity);
		}

		var pdxMods = mods.Where(x => x is LocalPdxPackage).ToList(x => x.Name);

		await _workshopService.SetEnableBulk(pdxMods, playset, value);

		pdxMods.ForEach(x => modConfig.SetEnabled(playset, x, value));
	}

	private void SetLocalFolderIncluded(bool value, IPackageIdentity item, ILocalPackageIdentity localIdentity)
	{
		try
		{
			var newFolder = CrossIO.Combine(Path.GetDirectoryName(localIdentity.Folder), value ? Path.GetFileName(localIdentity.Folder).TrimStart('.') : ('.' + Path.GetFileName(localIdentity.Folder).TrimStart('.')));

			Directory.Move(localIdentity.Folder, newFolder);

			if (localIdentity.GetLocalPackage() is LocalPackageData packageData)
			{
				packageData.Folder = newFolder;
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Failed to {(value ? "enable" : "disable")} the local mod {item.Name}");
		}
	}

	private async Task<List<IPackageIdentity>> ResolveDependencies(List<IPackageIdentity> mods, int? playsetId)
	{
		if (mods.Count == 0)
		{
			return [];
		}

		var list = new List<IPackageIdentity>();

		foreach (var mod in mods)
		{
			var workshopInfo = await _workshopService.GetInfoAsync(mod);

			if (workshopInfo is not null)
			{
				foreach (var item in workshopInfo.Requirements)
				{
					if (!item.IsDlc
						&& !mods.Any(x => x.Id == item.Id)
						&& !list.Any(x => x.Id == item.Id)
						&& !IsEnabled(item, playsetId))
					{
						list.Add(item);
					}
				}
			}
		}

		list.AddRange(await ResolveDependencies(list, playsetId));

		return list;
	}

	public int GetLoadOrder(IPackage package)
	{
		//if (package.LocalData?.Folder is null)
		//{
		//	return 0;
		//}

		//if (modConfig.TryGetValue(package.LocalData.Folder, out var info))
		//{
		//	return info.LoadOrder;
		//}

		return 0;
	}

	private bool IsValidModFolder(string dir, out string? dllPath, out Version? version)
	{
		try
		{
			var files = Directory.GetFiles(dir, "*.dll", SearchOption.AllDirectories);

			if (files != null && files.Length > 0)
			{
				return CrossIO.CurrentPlatform is Platform.MacOSX
					? _macAssemblyUtil.FindImplementation(files, out dllPath, out version)
					: _assemblyUtil.FindImplementation(files, out dllPath, out version);
			}
		}
		catch { }

		dllPath = null;
		version = null;
		return false;
	}

	public bool GetModInfo(string folder, out string? modDll, out Version? version)
	{
		return IsValidModFolder(folder, out modDll, out version);
	}

	public async Task Initialize()
	{
		await RefreshModConfig();
	}

	private void SaveHistory()
	{
		if (modConfig.IsEmpty)
		{
			return;
		}

		var differentConfig = _modConfigHistory.Count == 0;
		var config = modConfig.Clone();

		if (_modConfigHistory.Count == 0 || config.Equals(_modConfigHistory[0]))
		{
			_modConfigHistory.Insert(0, config);

			currentHistoryIndex = 0;
		}
	}

	public async Task UndoChanges()
	{
		if (currentHistoryIndex + 1 < _modConfigHistory.Count && _modConfigHistory.Count > 0)
		{
			currentHistoryIndex++;

			await ApplyModConfig(_modConfigHistory[currentHistoryIndex]);
		}
	}

	public async Task RedoChanges()
	{
		if (currentHistoryIndex - 1 > 0 && _modConfigHistory.Count > 0)
		{
			currentHistoryIndex--;

			await ApplyModConfig(_modConfigHistory[currentHistoryIndex]);
		}
	}

	public async Task ApplyModConfig(ModStateCollection newState)
	{
		var modConfigOld = modConfig.ToDictionary();
		var modConfigNew = newState.ToDictionary();

		var itemsToExclude = new List<(int, ulong)>();
		var itemsToInclude = new List<(int, ulong)>();
		var itemsToDisable = new List<(int, ulong)>();
		var itemsToEnable = new List<(int, ulong)>();

		await _workshopService.WaitUntilReady();

		foreach (var key in modConfigNew.Keys)
		{
			if (!modConfigOld.TryGetValue(key, out var dic))
			{
				foreach (var item in modConfigNew[key])
				{
					itemsToInclude.Add((key, item.Key));

					if (item.Value.IsEnabled)
					{
						itemsToEnable.Add((key, item.Key));
					}
					else
					{
						itemsToDisable.Add((key, item.Key));
					}
				}

				continue;
			}

			foreach (var item in modConfigNew[key])
			{
				if (!dic.TryGetValue(item.Key, out var enabled))
				{
					itemsToInclude.Add((key, item.Key));

					if (item.Value.IsEnabled)
					{
						itemsToEnable.Add((key, item.Key));
					}
					else
					{
						itemsToDisable.Add((key, item.Key));
					}

					continue;
				}

				if (enabled != item.Value)
				{
					if (item.Value.IsEnabled)
					{
						itemsToEnable.Add((key, item.Key));
					}
					else
					{
						itemsToDisable.Add((key, item.Key));
					}
				}
			}
		}

		foreach (var key in modConfigOld.Keys)
		{
			if (!modConfigNew.TryGetValue(key, out var dic))
			{
				foreach (var item in modConfigOld[key])
				{
					itemsToExclude.Add((key, item.Key));
				}

				continue;
			}

			foreach (var item in modConfigOld[key])
			{
				if (!dic.TryGetValue(item.Key, out var enabled))
				{
					itemsToExclude.Add((key, item.Key));
				}
			}
		}

		_enabling.AddRange(itemsToExclude.Select(x => x.Item2));
		_enabling.AddRange(itemsToInclude.Select(x => x.Item2));
		_enabling.AddRange(itemsToDisable.Select(x => x.Item2));
		_enabling.AddRange(itemsToEnable.Select(x => x.Item2));

		_notifier.OnRefreshUI(true);

		await _workshopService.WaitUntilReady();

		foreach (var grp in itemsToExclude.GroupBy(x => x.Item1))
		{
			await _subscriptionsManager.UnSubscribe(grp.Select(x => (IPackageIdentity)new GenericPackageIdentity(x.Item2)), grp.Key);
		}

		foreach (var grp in itemsToInclude.GroupBy(x => x.Item1))
		{
			await _subscriptionsManager.Subscribe(grp.Select(x => (IPackageIdentity)new GenericPackageIdentity(x.Item2)), grp.Key);
		}

		foreach (var grp in itemsToEnable.GroupBy(x => x.Item1))
		{
			await _workshopService.SetEnableBulk(grp.ToList(x => (int)x.Item2), grp.Key, true);
		}

		foreach (var grp in itemsToDisable.GroupBy(x => x.Item1))
		{
			await _workshopService.SetEnableBulk(grp.ToList(x => (int)x.Item2), grp.Key, false);
		}

		modConfig = newState.Clone();

		_enabling.RemoveRange(itemsToExclude.Select(x => x.Item2));
		_enabling.RemoveRange(itemsToInclude.Select(x => x.Item2));
		_enabling.RemoveRange(itemsToDisable.Select(x => x.Item2));
		_enabling.RemoveRange(itemsToEnable.Select(x => x.Item2));

		_notifier.OnInclusionUpdated();
		_notifier.OnRefreshUI(true);
	}

	public string? GetSelectedVersion(IPackageIdentity package, int? playsetId = null)
	{
		return package.GetPackage() is LocalPdxPackage localPdxPackage
			? localPdxPackage.Version
			: modConfig.GetVersion(playsetId ?? currentPlayset, package.Id);
	}

	public async Task SetVersion(IPackageIdentity package, string version, int? playsetId = null)
	{
		if (!_workshopService.IsAvailable)
		{
			return;
		}

		var playset = playsetId ?? currentPlayset;

		if (playset <= 0)
		{
			return;
		}

		await _workshopService.WaitUntilReady();

		var result = await _workshopService.SubscribeBulk([new KeyValuePair<int, string?>((int)package.Id, version)], playset);

		if (result)
		{
			var latestVersion = package.GetWorkshopInfo()?.VersionId;

			modConfig.SetVersion(playset, package.Id, version.IfEmpty(latestVersion));

			if (package.GetPackage() is LocalPdxPackage localPdxPackage)
			{
				localPdxPackage.Version = version.IfEmpty(latestVersion);
			}
		}

		if (_notifier.IsApplyingPlayset || _notifier.IsBulkUpdating)
		{
			return;
		}

		_notifier.OnWorkshopSyncEnded();
		_notifier.OnInclusionUpdated();
		_notifier.OnRefreshUI(true);
	}
}
