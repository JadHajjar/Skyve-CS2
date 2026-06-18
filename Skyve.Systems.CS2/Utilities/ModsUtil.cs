using Extensions;

using Microsoft.Extensions.DependencyInjection;

using PDX.SDK.Contracts.Service.Mods.Interfaces;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
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
	//private string? currentPlayset;
	//private int currentHistoryIndex;
	private ModStateCollection modConfig = new();
	private Dictionary<string, int> _orderedMods = [];
	private readonly List<ModStateCollection> _modConfigHistory = [];
	private readonly HashSet<int> _enabling = [];

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
		//_notifier.PlaysetChanged += Notifier_PlaysetChanged;
		//_notifier.WorkshopSyncEnded += async () => await RefreshModConfig();
	}

	public bool IsEnabling(IPackageIdentity package)
	{
		return _enabling.Contains(package.GetKey());
	}

	//private void Notifier_PlaysetChanged()
	//{
	//	currentPlayset = _serviceProvider.GetService<IPlaysetManager>()?.CurrentPlayset?.Id;
	//}

	//private async Task RefreshModConfig()
	//{
	//	//SaveHistory();

	//	var mods = await _workshopService.GetLocalPackages();

	//	var config = new ModStateCollection();

	//	foreach (var mod in mods)
	//	{
	//		foreach (var item in mod.Playsets)
	//		{
	//			config.SetState(item.PlaysetId
	//				, new GenericPackageIdentity(mod.Source, mod.Id, version: item.PreferredVersion)
	//				, item.ModIsEnabled);
	//		}
	//	}

	//	modConfig = config;
	//}

	private async void BuildLoadOrder()
	{
		if (!_notifier.IsContentLoaded || !_workshopService.IsAvailable)
		{
			return;
		}

		var playset = _workshopService.Context?.Mods.GetActivePlaysetId();

		if (playset is null)
		{
			return;
		}

		var index = 1;
		var mods = _serviceProvider.GetService<ILoadOrderHelper>()?.GetOrderedMods().OfType<IMod>();
		var orderedMods = new List<ModLoadOrder>();

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

		_orderedMods = orderedMods.ToDictionary(x => x.Mod.Id, x => x.LoadOrder);

		await _workshopService.SetLoadOrder(orderedMods, playset);
	}

	public void SaveChanges()
	{
		//if (_notifier.IsApplyingPlayset || _notifier.IsBulkUpdating)
		//{
		//	return;
		//}

		//lock (this)
		//{
		//	_config.SetModsInfo(_modConfigInfo);
		//}

		//_config.Save();
	}

	public bool IsIncluded(IPackageIdentity mod, string? playsetId = null, bool withVersion = true)
	{
		if (string.IsNullOrEmpty(mod.Source))
		{
			var folder = mod.GetLocalPackageIdentity()?.Folder;

			return folder is null or "" || Path.GetFileName(folder)[0] != '.';
		}

		playsetId ??= _workshopService.Context?.Mods.GetActivePlaysetId();

		return _workshopService.Context?.Mods.IsIncludedInPlayset(new ModBase(mod.Source, mod.Id, withVersion ? mod.Version : null), playsetId, out _) == true;
	}

	public bool IsEnabled(IPackageIdentity mod, string? playsetId = null, bool withVersion = true)
	{
		if (string.IsNullOrEmpty(mod.Source))
		{
			var folder = mod.GetLocalPackageIdentity()?.Folder;

			return folder is null or "" || Path.GetFileName(folder)[0] != '.';
		}

		playsetId ??= _workshopService.Context?.Mods.GetActivePlaysetId();

		return _workshopService.Context?.Mods.IsIncludedInPlayset(new ModBase(mod.Source, mod.Id, withVersion ? mod.Version : null), playsetId, out var isEnabled) == true
			&& isEnabled;
	}

	public async Task SetIncluded(IPackageIdentity mod, bool value, string? playsetId = null, bool withVersion = true, bool promptForDependencies = true)
	{
		await SetIncluded([mod], value, playsetId, withVersion, promptForDependencies);
	}

	public async Task SetIncluded(IEnumerable<IPackageIdentity> mods, bool value, string? playsetId = null, bool withVersion = true, bool promptForDependencies = true)
	{
		if (!_workshopService.IsAvailable)
		{
			return;
		}

		if (!value)
		{
			mods = mods.Where(x => !_modLogicManager.IsRequired(x.GetLocalPackageIdentity(), this, playsetId));
		}

		var localMods = mods.Where(x => string.IsNullOrEmpty(x.Source));

		SetLocalModIncluded(mods.Where(x => string.IsNullOrEmpty(x.Source)), value);

		mods = mods.Where(x => !string.IsNullOrEmpty(x.Source));

		var playset = playsetId ?? _workshopService.Context?.Mods.GetActivePlaysetId();

		if (playset is null || !mods.Any())
		{
			return;
		}

		if (value && promptForDependencies
			&& mods is List<IPackageIdentity> modList
			&& _settings.UserSettings.DependencyResolution is not DependencyResolveBehavior.None)
		{
			var dependencies = await ResolveDependencies(modList, playsetId);

			if (dependencies.Count > 0
				&& (_settings.UserSettings.DependencyResolution is DependencyResolveBehavior.Automatic
					|| _interfaceService.AskForDependencyConfirmation(modList, dependencies)))
			{
				modList.AddRange(dependencies);
			}
		}

		//var tempConfig = modConfig.CreateFragment(playset);
		var result = value
			? await Subscribe(mods, playset, withVersion)
			: await UnSubscribe(mods, playset);

		if (result)
		{
			foreach (var item in mods)
			{
				if (value)
				{
					modConfig.SetEnabled(playset, item, true);
					modConfig.SetVersion(playset, item);
				}
				else
				{
					modConfig.Remove(playset, item);
				}
			}

			//SaveHistory();
		}

		if (_notifier.IsApplyingPlayset || _notifier.IsBulkUpdating)
		{
			return;
		}

		_notifier.OnInclusionUpdated();
		_notifier.OnRefreshUI(true);
	}

	public async Task SetEnabled(IPackageIdentity mod, bool value, string? playsetId = null)
	{
		await SetEnabled([mod], value, playsetId);
	}

	public async Task SetEnabled(IEnumerable<IPackageIdentity> mods, bool value, string? playsetId = null)
	{
		if (!_workshopService.IsAvailable)
		{
			return;
		}

		//SaveHistory();

		if (!value)
		{
			mods = mods.Where(x => !_modLogicManager.IsRequired(x.GetLocalPackageIdentity(), this, playsetId));
		}

		var localMods = mods.Where(x => string.IsNullOrEmpty(x.Source));

		SetLocalModIncluded(mods.Where(x => string.IsNullOrEmpty(x.Source)), value);

		mods = mods.Where(x => !string.IsNullOrEmpty(x.Source));

		var playset = playsetId ?? _workshopService.Context?.Mods.GetActivePlaysetId();

		if (playset is null || !mods.Any())
		{
			return;
		}

		foreach (var mod in mods)
		{
			_enabling.Add(mod.GetKey());
		}

		_notifier.OnRefreshUI(true);

		var result = await _workshopService.SetEnableBulk(mods, playset, value);

		if (result)
		{
			foreach (var item in mods)
			{
				modConfig.SetEnabled(playset, item, value);
			}

			//SaveHistory();
		}

		foreach (var mod in mods)
		{
			_enabling.Remove(mod.GetKey());
		}

		if (_notifier.IsApplyingPlayset || _notifier.IsBulkUpdating)
		{
			return;
		}

		_notifier.OnInclusionUpdated();
		_notifier.OnRefreshUI(true);
	}

	private void SetLocalModIncluded(IEnumerable<IPackageIdentity> mods, bool value)
	{
		foreach (var item in mods)
		{
			var localIdentity = item.GetLocalPackageIdentity();

			if (localIdentity is null || IsIncluded(item, string.Empty) == value)
			{
				continue;
			}

			SetLocalFolderIncluded(value, item, localIdentity);
		}
	}

	private void SetLocalFolderIncluded(bool value, IPackageIdentity item, ILocalPackageIdentity localIdentity)
	{
		try
		{
			var newFolder = CrossIO.Combine(Path.GetDirectoryName(localIdentity.Folder), value ? Path.GetFileName(localIdentity.Folder).TrimStart('.') : ('.' + Path.GetFileName(localIdentity.Folder).TrimStart('.')));

			if (Directory.Exists(newFolder))
			{
				CrossIO.DeleteFolder(newFolder);
			}

			Directory.Move(localIdentity.Folder, newFolder);

			if (localIdentity.GetLocalPackage() is LocalPackageData packageData)
			{
				packageData.Folder = newFolder;
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: $"Failed to {(value ? "enable" : "disable")} the local mod {item.Name}");
		}
	}

	private async Task<List<IPackageIdentity>> ResolveDependencies(List<IPackageIdentity> mods, string? playsetId)
	{
		var dependencies = await Resolve(mods, playsetId);

		dependencies.RemoveAll(x => mods.Any(y => x.Id == y.Id));

		return dependencies.Distinct(x => x.Id).ToList();

		async Task<List<IPackageIdentity>> Resolve(List<IPackageIdentity> mods, string? playsetId)
		{
			if (mods.Count == 0)
			{
				return [];
			}

			var list = new List<IPackageIdentity>(mods);

			foreach (var mod in mods)
			{
				var workshopInfo = await _workshopService.GetInfoAsync(mod);

				if (workshopInfo is not null)
				{
					foreach (var item in workshopInfo.Requirements)
					{
						if (!item.IsDlc
							&& !list.Any(x => x.Id == item.Id)
							&& !IsEnabled(item, playsetId, false))
						{
							item.Version = null;

							list.Add(item);
						}
					}
				}
			}

			if (list.Count != mods.Count)
			{
				list.AddRange(await Resolve(list, playsetId));
			}

			return list;
		}
	}

	public int GetLoadOrder(IPackage package)
	{
		if (_orderedMods.TryGetValue(package.Id.ToString(), out var order))
		{
			return order;
		}

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
		//Notifier_PlaysetChanged();

		//await RefreshModConfig();
	}

	public string? GetSelectedVersion(IPackageIdentity package, string? playsetId = null)
	{
		playsetId ??= _workshopService.Context?.Mods.GetActivePlaysetId();

		return _workshopService.Context?.Mods.GetLocalModData(new ModBase(package.Source, package.Id))?.Playsets?.FirstOrDefault(x => x.PlaysetId == playsetId)?.PreferredVersion;
	}

	private async Task<bool> Subscribe(IEnumerable<IPackageIdentity> ids, string? playsetId = null, bool withVersion = true)
	{
		if (!_workshopService.IsAvailable)
		{
			return false;
		}

		var currentPlayset = playsetId ?? _workshopService.Context?.Mods.GetActivePlaysetId();

		if (currentPlayset is null)
		{
			return false;
		}

		_subscriptionsManager.AddSubscribing(ids);

		_notifier.OnRefreshUI(true);

		var dictionary = new List<IPackageIdentity>();

		foreach (var item in ids)
		{
			dictionary.Add(new PdxModIdentityPackage(item.Id) { Version = withVersion ? item.Version == "" || item.GetWorkshopInfo()?.LatestVersion == item.Version ? null : item.Version : null });
		}

		var result = await _workshopService.SubscribeBulk(dictionary, currentPlayset);

		_subscriptionsManager.RemoveSubscribing(ids);

		_notifier.OnRefreshUI(true);
		_notifier.OnPlaysetChanged();

		return result;
	}

	private async Task<bool> UnSubscribe(IEnumerable<IPackageIdentity> ids, string? playsetId = null)
	{
		if (!_workshopService.IsAvailable)
		{
			return false;
		}

		var currentPlayset = playsetId ?? _workshopService.Context?.Mods.GetActivePlaysetId();

		if (currentPlayset is null)
		{
			return false;
		}

		_subscriptionsManager.AddSubscribing(ids);

		_notifier.OnRefreshUI(true);

		var result = await _workshopService.UnsubscribeBulk(ids, currentPlayset);

		_subscriptionsManager.RemoveSubscribing(ids);

		_notifier.OnRefreshUI(true);
		_notifier.OnPlaysetChanged();

		return result;
	}

	public bool IsIncludedInOtherPlaysets(ILocalPackageIdentity mod, string? playsetId = null, bool withVersion = true, bool andEnabled = false)
	{
		playsetId ??= _workshopService.Context?.Mods.GetActivePlaysetId();

		return _workshopService.Context?.Mods.IsIncludedInPlayset(new ModBase(mod.Source, mod.Id, withVersion ? mod.Version : null), playsetId, out _) == true;
	}
}
