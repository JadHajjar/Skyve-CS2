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
	private Dictionary<ulong, bool> modConfig = [];

	private readonly AssemblyUtil _assemblyUtil;
	private readonly MacAssemblyUtil _macAssemblyUtil;
	private readonly WorkshopService _workshopService;
	private readonly IModLogicManager _modLogicManager;
	private readonly INotifier _notifier;
	private readonly ISettings _settings;

	public ModsUtil(IWorkshopService workshopService, IModLogicManager modLogicManager, INotifier notifier, AssemblyUtil assemblyUtil, MacAssemblyUtil macAssemblyUtil, ISettings settings)
	{
		_assemblyUtil = assemblyUtil;
		_workshopService = (WorkshopService)workshopService;
		_modLogicManager = modLogicManager;
		_macAssemblyUtil = macAssemblyUtil;
		_notifier = notifier;
		_settings = settings;

		_notifier.CompatibilityDataLoaded += BuildLoadOrder;
		_notifier.PlaysetChanged += _notifier_PlaysetChanged;
	}

	private async void _notifier_PlaysetChanged()
	{
		if (_workshopService.Context is null)
		{
			modConfig = [];
			return;
		}

		var playset = (await _workshopService.Context.Mods.GetActivePlayset()).PlaysetId;
		var mods = await _workshopService.Context.Mods.ListModsInPlayset(playset);
		
		modConfig = mods.Mods
			.OfType<PlaysetSubscribedMod>()
			.ToDictionary(x => (ulong)x.Id, x => x.IsEnabled);
	}

	private async void BuildLoadOrder()
	{
		if (!_notifier.IsContentLoaded || _workshopService.Context is null)
		{
			return;
		}

		var index = 1;
		var mods = ServiceCenter.Get<ILoadOrderHelper>().GetOrderedMods().Reverse().OfType<IMod>();
		var orderedMods = new List<ModLoadOrder>();
		var playset = (await _workshopService.Context.Mods.GetActivePlayset()).PlaysetId;
		await _workshopService.Context.Mods.ResetLoadOrder(playset);

		foreach (var mod in mods)
		{
			orderedMods.Add(new ModLoadOrder
			{
				Mod = mod,
				LoadOrder = index++,
			});
		}

		await _workshopService.Context.Mods.SetLoadOrder(orderedMods, playset);
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

	public bool IsIncluded(IPackageIdentity mod)
	{
		return mod.Id <= 0 || modConfig.ContainsKey(mod.Id);
	}

	public bool IsEnabled(IPackageIdentity mod)
	{
		return mod.Id <= 0 || modConfig.TryGet(mod.Id);
	}

	public async Task SetIncluded(IPackageIdentity mod, bool value) => await SetIncluded([mod], value);

	public async Task SetIncluded(IEnumerable<IPackageIdentity> mods, bool value)
	{
		//value = (value || _modLogicManager.IsRequired(mod, this)) && !_modLogicManager.IsForbidden(mod);

		if (_workshopService.Context is null)
			return;

		var playset = (await _workshopService.Context.Mods.GetActivePlayset()).PlaysetId;

		if (value)
		{
			foreach (var item in mods)
			{
				if (item.Id <= 0)
					continue;

				modConfig[item.Id] = !_settings.UserSettings.DisableNewModsByDefault;
			}

			var modKeys = mods.Where(x => x.Id > 0).Select(x => new KeyValuePair<int, string?>((int)x.Id, null));

			var result = await _workshopService.Context.Mods.SubscribeBulk(modKeys, playset, !_settings.UserSettings.DisableNewModsByDefault);

			if (!result.Success)
				foreach (var item in mods)
				{
					if (item.Id <= 0)
						continue;

					modConfig.Remove(item.Id);
				}
		}
		else
		{
			var tempConfig = new Dictionary<ulong, bool>(modConfig);

			foreach (var item in mods)
			{
				if (item.Id <= 0)
					continue;

				modConfig.Remove(item.Id);
			}

			foreach (var item in mods)
			{
				if (item.Id <= 0)
					continue;

				var result = await _workshopService.Context.Mods.Unsubscribe((int)item.Id, playset);

				if (!result.Success)
					modConfig[item.Id] = tempConfig.TryGet(item.Id);
			}
		}

		if (_notifier.ApplyingPlayset || _notifier.BulkUpdating)
		{
			return;
		}

		_notifier.OnInclusionUpdated();
		_notifier.TriggerAutoSave();

		SaveChanges();
	}

	public async Task SetEnabled(IPackageIdentity mod, bool value) => await SetEnabled([mod], value);

	public async Task SetEnabled(IEnumerable<IPackageIdentity> mods, bool value)
	{
		//value = (value || _modLogicManager.IsRequired(mod, this)) && !_modLogicManager.IsForbidden(mod);

		if (_workshopService.Context is null)
			return;

		var tempConfig = new Dictionary<ulong, bool>(modConfig);

		foreach (var item in mods)
		{
			if (item.Id <= 0)
				continue;

			modConfig[item.Id] = value;
		}

		var modKeys = mods.Where(x => x.Id > 0).ToList(x => (int)x.Id);
		var playset = (await _workshopService.Context.Mods.GetActivePlayset()).PlaysetId;

		var result = value
			? await _workshopService.Context.Mods.EnableBulk(modKeys, playset)
			: await _workshopService.Context.Mods.DisableBulk(modKeys, playset);

		if (!result.Success)
			foreach (var item in mods)
			{
				if (item.Id <= 0)
					continue;

				modConfig[item.Id] = tempConfig.TryGet(item.Id);
			}
		else
			await _workshopService.Context.Mods.Sync(PDX.SDK.Contracts.Service.Mods.Enums.SyncDirection.Downstream);

		if (_notifier.ApplyingPlayset || _notifier.BulkUpdating)
		{
			return;
		}

		_notifier.OnInclusionUpdated();
		_notifier.TriggerAutoSave();

		SaveChanges();
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
}
