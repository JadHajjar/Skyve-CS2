using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
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
			modUtil.SetIncluded(mod.LocalData, false);
			modUtil.SetEnabled(mod.LocalData, false);
		}
		else if (IsPseudoMod(mod) && _settings.UserSettings.HidePseudoMods)
		{
			modUtil.SetIncluded(mod.LocalData, true);
			modUtil.SetEnabled(mod.LocalData, true);
		}
	}

	public bool IsRequired(ILocalPackageIdentity mod, IModUtil modUtil)
	{
		var list = _modCollection.GetCollection(Path.GetFileName(mod.FilePath), out var collection);

		if (!(collection?.Required ?? false) || list is null)
		{
			return false;
		}

		foreach (var modItem in list)
		{
			if (modItem != mod && modUtil.IsIncluded(modItem.LocalData) && modUtil.IsEnabled(modItem.LocalData))
			{
				return false;
			}
		}

		return true;
	}

	public bool IsForbidden(ILocalPackageIdentity mod)
	{
		var list = _modCollection.GetCollection(Path.GetFileName(mod.FilePath), out var collection);

		if (!(collection?.Forbidden ?? false) || list is null)
		{
			return false;
		}

		return true;
	}

	public void ModRemoved(IPackage mod)
	{
		_modCollection.RemoveMod(mod);
	}

	public void ApplyRequiredStates(IModUtil modUtil)
	{
		foreach (var item in _modCollection.Collections)
		{
			if (item.Any(mod => modUtil.IsIncluded(mod.LocalData) && modUtil.IsEnabled(mod.LocalData)))
			{
				continue;
			}

			modUtil.SetIncluded(item[0].LocalData, true);
			modUtil.SetEnabled(item[0].LocalData, true);
		}
	}

	public bool IsPseudoMod(IPackage package)
	{
		if (package.GetPackageInfo()?.Type is not null and not PackageType.GenericPackage and not PackageType.MusicPack and not PackageType.CSM and not PackageType.ContentPackage)
		{
			return true;
		}

		return false;
	}

	public bool AreMultipleSkyvesPresent(out List<IPackage> skyveInstances)
	{
		skyveInstances = new();

		//skyveInstances.AddRange(_modCollection.GetCollection(Skyve_ASSEMBLY, out _)?.ToList(x => x.LocalParentPackage) ?? new());

		return skyveInstances.Count > 1;
	}
}
