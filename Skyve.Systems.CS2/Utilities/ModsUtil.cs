using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2;
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
	private readonly ModConfig _config;
	private readonly Dictionary<string, ModConfig.ModInfo> _modConfigInfo;

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

		_config = ModConfig.Load();
		_modConfigInfo = _config.GetModsInfo();
	}

	private void BuildLoadOrder()
	{
		if (!_notifier.IsContentLoaded)
		{
			return;
		}

		var index = 1;
		var mods = ServiceCenter.Get<ILoadOrderHelper>().GetOrderedMods().Reverse();

		lock (this)
		{
			foreach (var mod in mods)
			{
				var modInfo = _modConfigInfo.TryGetValue(mod.LocalData.Folder, out var info) ? info : new();

				modInfo.LoadOrder = index++;

				_modConfigInfo[mod.LocalData.Folder] = modInfo;
			}
		}

		SaveChanges();
	}

	public void SaveChanges()
	{
		if (_notifier.ApplyingPlayset || _notifier.BulkUpdating)
		{
			return;
		}

		lock (this)
		{
			_config.SetModsInfo(_modConfigInfo);
		}

		_config.Save();
	}

	public bool IsIncluded(ILocalPackageIdentity mod)
	{
		return !(_modConfigInfo.TryGetValue(mod.Folder, out var info) && info.Excluded);
	}

	public bool IsEnabled(ILocalPackageIdentity mod)
	{
		return IsIncluded(mod);// (await _workshopService.Context!.Mods.GetDetails((Mod)mod)).Mod.Playsets?.;
	}

	public void SetIncluded(ILocalPackageIdentity mod, bool value)
	{
		value = (value || _modLogicManager.IsRequired(mod, this)) && !_modLogicManager.IsForbidden(mod);

		var modInfo = _modConfigInfo.TryGetValue(mod.Folder, out var info) ? info : new();

		modInfo.Excluded = !value;

		lock (this)
		{
			_modConfigInfo[mod.Folder] = modInfo;
		}

		//if (!_settings.UserSettings.AdvancedIncludeEnable)
		//{
		//	_colossalOrderUtil.SetEnabled(mod, value);
		//}

		if (_notifier.ApplyingPlayset || _notifier.BulkUpdating)
		{
			return;
		}

		_notifier.OnInclusionUpdated();
		_notifier.TriggerAutoSave();

		SaveChanges();
	}

	public void SetEnabled(ILocalPackageIdentity mod, bool value)
	{
		value = (value || _modLogicManager.IsRequired(mod, this)) && !_modLogicManager.IsForbidden(mod);

		//_colossalOrderUtil.SetEnabled(mod, value);

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

		if (_modConfigInfo.TryGetValue(package.LocalData.Folder, out var info))
		{
			return info.LoadOrder;
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

	public bool GetModInfo(ILocalPackageIdentity package, out string? modDll, out Version? version)
	{
		return IsValidModFolder(package.Folder, out modDll, out version);
	}
}
