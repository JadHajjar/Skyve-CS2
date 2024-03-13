using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Systems.Compatibility;
using Skyve.Systems.Compatibility.Domain;
using Skyve.Systems.CS2.Domain;
using Skyve.Systems.CS2.Domain.Api;

using SkyveApi.Domain.CS2;

using System;

namespace Skyve.Systems.CS2.Systems;
internal class CompatibilityUtil : ICompatibilityUtil
{
	public CompatibilityUtil()
	{
	}

	public DateTime MinimumModDate { get; } = new DateTime(2023, 11, 01);

	public void PopulateAutomaticPackageInfo(PackageData info, IPackageIdentity package, IWorkshopInfo? workshopInfo)
	{
		//if (workshopInfo?.Requirements?.Any(x => x.Id == MUSIC_MOD_ID) ?? false)
		//{
		//	info.Statuses!.Add(new(StatusType.MusicCanBeCopyrighted));
		//}
	}

	public void PopulatePackageReport(IPackageCompatibilityInfo packageData, CompatibilityInfo info, CompatibilityHelper compatibilityHelper)
	{
	}
}
