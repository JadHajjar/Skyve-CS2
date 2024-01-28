using Extensions;

using Skyve.Compatibility.Domain.Enums;
using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Skyve.Systems.CS2.Managers;
internal class ModLogicManager : IModLogicManager
{
	private const string Skyve_ASSEMBLY = "SkyveMod.dll";

	private readonly ModCollection _modCollection = new(GetGroupInfo());

	private static Dictionary<string, CollectionInfo> GetGroupInfo()
	{
		return new(StringComparer.OrdinalIgnoreCase)
		{
			[Skyve_ASSEMBLY] = new() { Required = true },
		};
	}

	private readonly ISettings _settings;

	public ModLogicManager(ISettings settings)
	{
		_settings = settings;
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

	public bool IsRequired(ILocalPackageIdentity? mod, IModUtil modUtil)
	{
		if (mod is null)
		{
			return false;
		}

		var list = _modCollection.GetCollection(Path.GetFileName(mod.FilePath), out var collection);

		if (mod.IsLocal())
		{
			return true;
		}

		if (!(collection?.Required ?? false) || list is null)
		{
			return false;
		}

		foreach (var modItem in list)
		{
			if (modItem != mod && modUtil.IsIncluded(modItem) && modUtil.IsEnabled(modItem))
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
		foreach (var item in _modCollection.Collections)
		{
			if (item.Any(mod => modUtil.IsIncluded(mod) && modUtil.IsEnabled(mod)))
			{
				continue;
			}

			modUtil.SetIncluded(item[0], true);
			modUtil.SetEnabled(item[0], true);
		}
	}

	public bool IsPseudoMod(IPackage package)
	{
		return package.GetPackageInfo()?.Type is not null and not PackageType.GenericPackage and not PackageType.MusicPack and not PackageType.CSM and not PackageType.ContentPackage;
	}

	public bool AreMultipleSkyvesPresent(out List<IPackageIdentity> skyveInstances)
	{
		skyveInstances = [];

		//skyveInstances.AddRange(_modCollection.GetCollection(Skyve_ASSEMBLY, out _)?.ToList(x => x.GetLocalPackage()) ?? new());

		return skyveInstances.Count > 1;
	}
}
