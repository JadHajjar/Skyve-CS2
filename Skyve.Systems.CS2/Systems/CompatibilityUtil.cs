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

	public CompatibilityUtil()
	{
	}

	public DateTime MinimumModDate { get; } = new DateTime(2023, 11, 01);

	public void PopulateAutomaticPackageInfo(PackageData info, IPackageIdentity package, IWorkshopInfo? workshopInfo)
	{
		if (workshopInfo?.Requirements?.Any(x => x.Id == MUSIC_MOD_ID) ?? false)
		{
			info.Type = PackageType.MusicPack;

			info.Statuses!.Add(new PackageStatus(StatusType.MusicCanBeCopyrighted));
		}
	}

	public void PopulatePackageReport(IPackageCompatibilityInfo packageData, CompatibilityInfo info, CompatibilityHelper compatibilityHelper)
	{
	}
}
