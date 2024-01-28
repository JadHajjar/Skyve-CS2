using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Compatibility.Domain.Interfaces;

using System.Collections.Generic;

namespace Skyve.Systems.CS2.Domain;

public class IndexedPackageStatus : IIndexedPackageStatus<StatusType>
{
	public IPackageStatus<StatusType> Status { get; }
	public Dictionary<ulong, IIndexedPackageCompatibilityInfo> Packages { get; }

	public IndexedPackageStatus(PackageStatus status, Dictionary<ulong, IndexedPackage> packages)
	{
		Status = status;
		Packages = [];

		if (status.Packages is not null)
		{
			foreach (var item in status.Packages)
			{
				if (packages.ContainsKey(item))
				{
					Packages[item] = packages[item];
				}
			}
		}
	}

	public static implicit operator IndexedPackageStatus(PackageStatus status)
	{
		return new(status, []);
	}
}
