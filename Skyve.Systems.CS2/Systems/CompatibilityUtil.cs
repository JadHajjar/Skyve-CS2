using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Systems.Compatibility;
using Skyve.Systems.Compatibility.Domain;
using Skyve.Systems.CS2.Domain.Api;

using System;
using System.Linq;

namespace Skyve.Systems.CS2.Systems;
internal class CompatibilityUtil : ICompatibilityUtil
{
	private const ulong MUSIC_MOD_ID = 75862;
	private const ulong EAI_MOD_ID = 80529;
	private const ulong APM_MOD_ID = 78903;

	public CompatibilityUtil()
	{
	}

	public DateTime MinimumModDate { get; } = new DateTime(2023, 11, 01);

	public void PopulateAutomaticPackageInfo(PackageData info, IPackageIdentity package, IWorkshopInfo? workshopInfo)
	{
		if (workshopInfo is null)
		{
			return;
		}

		if (workshopInfo.Tags.ContainsKey("Savegame") || workshopInfo.Tags.ContainsKey("Map"))
		{
			info.Type = PackageType.MapSavegame;
			info.SavegameEffect = SavegameEffect.None;
		}
		else if (workshopInfo.Requirements?.Any(x => x.Id is MUSIC_MOD_ID) ?? false)
		{
			info.Type = PackageType.MusicPack;
			info.SavegameEffect = SavegameEffect.None;

			info.Statuses!.Add(new PackageStatus(StatusType.MusicCanBeCopyrighted));
		}
		else if (workshopInfo.Requirements?.Any(x => x.Id is EAI_MOD_ID or APM_MOD_ID) ?? false)
		{
			info.Type = PackageType.ContentPackage;
			info.SavegameEffect = SavegameEffect.AssetsRemain;
		}
	}

	public void PopulatePackageReport(IPackageCompatibilityInfo packageData, CompatibilityInfo info, CompatibilityHelper compatibilityHelper)
	{
	}
}
