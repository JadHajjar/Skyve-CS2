using Extensions;

using Microsoft.Extensions.DependencyInjection;

using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
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
	private Dictionary<int, Dictionary<ulong, bool>> modConfig = [];
	private readonly List<Dictionary<int, Dictionary<ulong, bool>>> _modConfigHistory = [];
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

	public ModsUtil(IWorkshopService workshopService, ISubscriptionsManager subscriptionsManager, IModLogicManager modLogicManager, INotifier notifier, AssemblyUtil assemblyUtil, MacAssemblyUtil macAssemblyUtil, ISettings settings, IServiceProvider serviceProvider, ILogger logger)
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

		var config = new Dictionary<int, Dictionary<ulong, bool>>();

		foreach (var mod in mods)
		{
			foreach (var item in mod.Playsets)
			{
				if (!config.ContainsKey(item.PlaysetId))
				{
					config[item.PlaysetId] = [];
				}

				config[item.PlaysetId][(ulong)mod.Id] = item.ModIsEnabled;
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
		var mods = _serviceProvider.GetService<ILoadOrderHelper>()?.GetOrderedMods().Reverse().OfType<IMod>();
		var orderedMods = new List<ModLoadOrder>();
		var playset = await _workshopService.GetActivePlaysetId();

		if (mods is null)
		{
			return;
		}

		foreach (var mod in mods)
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
		return mod.Id <= 0 || (modConfig.ContainsKey(playsetId ?? currentPlayset) && modConfig[playsetId ?? currentPlayset].ContainsKey(mod.Id));
	}

	public bool IsEnabled(IPackageIdentity mod, int? playsetId = null)
	{
		if (mod.Id <= 0)
		{
			var folder = mod.GetLocalPackageIdentity()?.Folder;

			return folder is null or "" || Path.GetFileName(folder)[0] != '.';
		}

		return modConfig.ContainsKey(playsetId ?? currentPlayset) && modConfig[playsetId ?? currentPlayset].TryGet(mod.Id);
	}

	public async Task SetIncluded(IPackageIdentity mod, bool value, int? playsetId = null)
	{
		await SetIncluded([mod], value, playsetId);
	}

	public async Task SetIncluded(IEnumerable<IPackageIdentity> mods, bool value, int? playsetId = null)
	{
		//value = (value || _modLogicManager.IsRequired(mod, this)) && !_modLogicManager.IsForbidden(mod);

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

		//await _workshopService.WaitUntilReady();

		if (!modConfig.ContainsKey(playset))
		{
			modConfig[playset] = [];
		}

		mods = mods.AllWhere(x => x.Id > 0 && IsIncluded(x, playset) != value);

		if (!mods.Any())
		{
			return;
		}

		var tempConfig = new Dictionary<ulong, bool>(modConfig[playset]);
		var result = value
			? await _subscriptionsManager.Subscribe(mods, playset)
			: await _subscriptionsManager.UnSubscribe(mods, playset);

		if (result)
		{
			foreach (var item in mods)
			{
				if (item.Id <= 0)
				{
					continue;
				}

				if (!modConfig.ContainsKey(playset))
				{
					modConfig[playset] = [];
				}

				if (value)
				{
					modConfig[playset][item.Id] = !_settings.UserSettings.DisableNewModsByDefault;
				}
				else
				{
					modConfig[playset].Remove(item.Id);
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
		//value = (value || _modLogicManager.IsRequired(mod, this)) && !_modLogicManager.IsForbidden(mod);

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

		SetLocalModEnabled(mods.AllWhere(x => x.Id <= 0 && IsEnabled(x, playset) != value), value);

		mods = mods.AllWhere(x => x.Id > 0 && IsEnabled(x, playset) != value);

		if (!mods.Any())
		{
			return;
		}

		_enabling.AddRange(mods.Select(x => x.Id).Where(x => x > 0));

		_notifier.OnRefreshUI(true);

		await _workshopService.WaitUntilReady();

		if (!modConfig.ContainsKey(playset))
		{
			modConfig[playset] = [];
		}

		var modKeys = mods.ToList(x => (int)x.Id).DistinctList();

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

				modConfig[playset][item.Id] = value;
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

	private void SetLocalModEnabled(List<IPackageIdentity> mods, bool value)
	{
		if (mods.Count == 0)
		{
			return;
		}

		foreach (var item in mods)
		{
			var localIdentity = item.GetLocalPackageIdentity();

			if (localIdentity is null)
			{
				continue;
			}

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
	}

	public int GetLoadOrder(IPackage package)
	{
		if (package.LocalData?.Folder is null)
		{
			return 0;
		}

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
		if (modConfig.Count == 0)
		{
			return;
		}

		var differentConfig = _modConfigHistory.Count == 0;
		var config = modConfig.ToDictionary(x => x.Key, x => new Dictionary<ulong, bool>(x.Value));

		if (_modConfigHistory.Count == 0 || AreConfigsDifferent(config, _modConfigHistory[0]))
		{
			_modConfigHistory.Insert(0, config);

			currentHistoryIndex = 0;
		}
	}

	private bool AreConfigsDifferent(Dictionary<int, Dictionary<ulong, bool>> config1, Dictionary<int, Dictionary<ulong, bool>> config2)
	{
		if (!config1.Keys.SequenceEqual(config2.Keys))
		{
			return true;
		}

		foreach (var key in config1.Keys)
		{
			if (!config1[key].SequenceEqual(config2[key]))
			{
				return true;
			}
		}

		return false;
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

	public async Task ApplyModConfig(Dictionary<int, Dictionary<ulong, bool>> modConfigNew)
	{
		var modConfigOld = modConfig;

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

					if (item.Value)
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

					if (item.Value)
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
					if (item.Value)
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

		modConfig = modConfigNew.ToDictionary(x => x.Key, x => new Dictionary<ulong, bool>(x.Value));

		_enabling.RemoveRange(itemsToExclude.Select(x => x.Item2));
		_enabling.RemoveRange(itemsToInclude.Select(x => x.Item2));
		_enabling.RemoveRange(itemsToDisable.Select(x => x.Item2));
		_enabling.RemoveRange(itemsToEnable.Select(x => x.Item2));

		_notifier.OnInclusionUpdated();
		_notifier.OnRefreshUI(true);
	}
}
