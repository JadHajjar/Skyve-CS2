using Extensions;

using Skyve.Compatibility.Domain.Enums;
using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Skyve.Systems.CS2.Managers;
internal class ModLogicManager : IModLogicManager
{
	private const string SKYVE_ASSEMBLY = "Skyve Mod.dll";

	private ModCollection _modCollection = new(GetGroupInfo());

	private static Dictionary<string, CollectionInfo> GetGroupInfo()
	{
		return new(StringComparer.OrdinalIgnoreCase)
		{
			[SKYVE_ASSEMBLY] = new() { Required = true },
		};
	}

	private readonly ISettings _settings;
	private readonly INotifier _notifier;

	public ModLogicManager(ISettings settings, INotifier notifier)
	{
		_settings = settings;
		_notifier = notifier;
	}

	public void Analyze(IPackage mod, IModUtil modUtil)
	{
		_modCollection.CheckAndAdd(mod);

		if (IsForbidden(mod.LocalData))
		{
			modUtil.SetIncluded(mod, false);
			modUtil.SetEnabled(mod, false);
		}
		else if (IsPseudoMod(mod) && _settings.UserSettings.HidePseudoMods)
		{
			modUtil.SetIncluded(mod, true);
			modUtil.SetEnabled(mod, true);
		}
	}

	public IEnumerable<IPackage> GetCollection(string key)
	{
		return _modCollection.GetCollection(key, out _) ?? [];
	}

	public bool IsRequired(ILocalPackageIdentity? mod, IModUtil modUtil)
	{
		if (mod is null)
		{
			return false;
		}

		if (mod.GetPackage()?.IsBuiltIn ?? false)
		{
			return true;
		}

		if (mod.IsLocal() && Path.GetFileName(mod.Folder) == _settings.FolderSettings.UserIdentifier)
		{
			return true;
		}

		var list = _modCollection.GetCollection(Path.GetFileName(mod.FilePath), out var collection);

		if (!(collection?.Required ?? false) || list is null)
		{
			return false;
		}

		foreach (var modItem in list)
		{
			if (modItem.LocalData != mod && modUtil.IsIncluded(modItem) && modUtil.IsEnabled(modItem))
			{
				return false;
			}
		}

		return true;
	}

	public bool IsForbidden(ILocalPackageIdentity? mod)
	{
		if (mod is null)
		{
			return false;
		}

		var list = _modCollection.GetCollection(Path.GetFileName(mod.FilePath), out var collection);

		return (collection?.Forbidden ?? false) && list is not null;
	}

	public void ModRemoved(IPackage mod)
	{
		_modCollection.RemoveMod(mod);
	}

	public void ApplyRequiredStates(IModUtil modUtil)
	{
		var skyveMods = _modCollection.GetCollection(SKYVE_ASSEMBLY, out var collectionInfo);

		foreach (var item in skyveMods?.Where(x => x.LocalData != null) ?? [])
		{
			var skyvePath = CrossIO.Combine(item.LocalData!.Folder, ".App", "Skyve.exe");
			var currentPath = Application.ExecutablePath;

			var skyveVersion = FileVersionInfo.GetVersionInfo(skyvePath).FileVersion;
			var currentVersion = FileVersionInfo.GetVersionInfo(currentPath).FileVersion;

			if (Version.TryParse(skyveVersion, out var skyveVer) && Version.TryParse(currentVersion, out var currentVer))
			{
				if (skyveVer > currentVer)
				{
					_notifier.OnSkyveUpdateAvailable();
				}
			}
		}

		//foreach (var item in _modCollection.Collections)
		//{
		//	if (item.Any(mod => modUtil.IsIncluded(mod) && modUtil.IsEnabled(mod)))
		//	{
		//		continue;
		//	}

		//	modUtil.SetIncluded(item[0], true);
		//	modUtil.SetEnabled(item[0], true);
		//}
	}

	public bool IsPseudoMod(IPackage package)
	{
		var info = package.GetPackageInfo();

		if (info is null)
		{
			return false;
		}

		return info.Type is PackageType.MusicPack or PackageType.NameList;
	}

	public bool AreMultipleSkyvesPresent(out List<IPackageIdentity> skyveInstances)
	{
		skyveInstances = [];

		//skyveInstances.AddRange(_modCollection.GetCollection(Skyve_ASSEMBLY, out _)?.ToList(x => x.GetLocalPackage()) ?? new());

		return skyveInstances.Count > 1;
	}

	public void Clear()
	{
		_modCollection = new(GetGroupInfo());
	}
}
