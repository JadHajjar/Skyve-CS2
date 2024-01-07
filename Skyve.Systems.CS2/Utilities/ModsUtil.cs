using Extensions;

using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain;
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
	private Dictionary<int, Dictionary<ulong, bool>> modConfig = [];

	private readonly AssemblyUtil _assemblyUtil;
	private readonly MacAssemblyUtil _macAssemblyUtil;
	private readonly WorkshopService _workshopService;
	private readonly IModLogicManager _modLogicManager;
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly INotifier _notifier;
	private readonly ISettings _settings;

	public ModsUtil(IWorkshopService workshopService, ISubscriptionsManager subscriptionsManager, IModLogicManager modLogicManager, INotifier notifier, AssemblyUtil assemblyUtil, MacAssemblyUtil macAssemblyUtil, ISettings settings)
	{
		_assemblyUtil = assemblyUtil;
		_workshopService = (WorkshopService)workshopService;
		_modLogicManager = modLogicManager;
		_macAssemblyUtil = macAssemblyUtil;
		_subscriptionsManager = subscriptionsManager;
		_notifier = notifier;
		_settings = settings;

		_notifier.CompatibilityDataLoaded += BuildLoadOrder;
		_notifier.PlaysetChanged += _notifier_PlaysetChanged;
	}

	private void _notifier_PlaysetChanged()
	{
		currentPlayset = ServiceCenter.Get<IPlaysetManager>().CurrentPlayset?.Id ?? 0;
	}

	private async Task RefreshModConfig()
	{
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
		var mods = ServiceCenter.Get<ILoadOrderHelper>().GetOrderedMods().Reverse().OfType<IMod>();
		var orderedMods = new List<ModLoadOrder>();
		var playset = await _workshopService.GetActivePlaysetId();

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
		if (_notifier.ApplyingPlayset || _notifier.BulkUpdating)
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
		return mod.Id <= 0 || (modConfig.ContainsKey(playsetId ?? currentPlayset) && modConfig[playsetId ?? currentPlayset].TryGet(mod.Id));
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

		await _workshopService.WaitUntilReady();

		//foreach (var item in mods)
		//{
		//	if (item.Id <= 0)
		//	{
		//		continue;
		//	}

		//	if (!modConfig.ContainsKey(playset))
		//	{
		//		modConfig[playset] = [];
		//	}

		//	if (value)
		//	{
		//		modConfig[playset][item.Id] = !_settings.UserSettings.DisableNewModsByDefault;
		//	}
		//	else
		//	{
		//		modConfig[playset].Remove(item.Id);
		//	}
		//}

		//_notifier.OnRefreshUI(true);

		mods = mods.Where(x => x.Id > 0 && IsEnabled(x, playset) != value);

		var tempConfig = new Dictionary<ulong, bool>(modConfig[playset]);
		var result = value
			? await _subscriptionsManager.Subscribe(mods)
			: await _subscriptionsManager.UnSubscribe(mods);

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
			//foreach (var item in mods)
			//{
			//	if (item.Id <= 0)
			//	{
			//		continue;
			//	}

			//	if (!modConfig.ContainsKey(playset))
			//	{
			//		modConfig[playset] = [];
			//	}

			//	if (value)
			//	{
			//		modConfig[playset].Remove(item.Id);
			//	}
			//	else
			//	{
			//		modConfig[playset][item.Id] = tempConfig.TryGet(item.Id);
			//	}
			//}
		}

		if (_notifier.ApplyingPlayset || _notifier.BulkUpdating)
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

		await _workshopService.WaitUntilReady();

		if (!modConfig.ContainsKey(playset))
		{
			modConfig[playset] = [];
		}

		//var tempConfig = new Dictionary<ulong, bool>(modConfig[playset]);

		//foreach (var item in mods)
		//{
		//	if (item.Id <= 0)
		//	{
		//		continue;
		//	}

		//	modConfig[playset][item.Id] = value;
		//}

		//_notifier.OnRefreshUI(true);

		mods = mods.Where(x => x.Id > 0 && IsEnabled(x, playset) != value);

		var modKeys = mods.ToList(x => (int)x.Id);

		var result = await _workshopService.SetEnableBulk(modKeys, playset, value);

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
			//foreach (var item in mods)
			//{
			//	if (item.Id <= 0)
			//	{
			//		continue;
			//	}

			//	modConfig[playset][item.Id] = tempConfig.TryGet(item.Id);
			//}
		}

		if (_notifier.ApplyingPlayset || _notifier.BulkUpdating)
		{
			return;
		}

		_notifier.OnInclusionUpdated();
		_notifier.OnRefreshUI(true);
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
}
