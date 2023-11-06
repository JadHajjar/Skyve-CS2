using Extensions;

using Skyve.Domain;
using Skyve.Domain.Enums;
using Skyve.Systems.Compatibility;
using Skyve.Systems.Compatibility.Domain;
using Skyve.Systems.Compatibility.Domain.Api;

using System;
using System.Linq;

namespace Skyve.Systems.CS2.Systems;
internal class CompatibilityUtil : ICompatibilityUtil
{
	public CompatibilityUtil()
	{
	}

	public DateTime MinimumModDate { get; } = new DateTime(2023, 11, 01);

	public void PopulateAutomaticPackageInfo(CompatibilityPackageData info, IPackage package, IWorkshopInfo? workshopInfo)
	{
		//if (workshopInfo?.Requirements?.Any(x => x.Id == MUSIC_MOD_ID) ?? false)
		//{
		//	info.Statuses!.Add(new(StatusType.MusicCanBeCopyrighted));
		//}
	}

	public void PopulatePackageReport(IndexedPackage packageData, CompatibilityInfo info, CompatibilityHelper compatibilityHelper)
	{

	}
}
