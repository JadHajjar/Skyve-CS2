using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Compatibility.Domain.Interfaces;

using System.Collections.Generic;

namespace Skyve.Systems.CS2.Domain;

public class IndexedPackageInteraction : IIndexedPackageStatus<InteractionType>
{
	public IPackageStatus<InteractionType> Status { get; }
	public Dictionary<ulong, IIndexedPackageCompatibilityInfo> Packages { get; }

	public IndexedPackageInteraction(PackageInteraction interaction, Dictionary<ulong, IndexedPackage> packages)
	{
		Status = interaction;
		Packages = [];

		if (interaction.Packages is not null)
		{
			foreach (var item in interaction.Packages)
			{
				if (packages.ContainsKey(item))
				{
					Packages[item] = packages[item];
				}
			}
		}
	}

	public static implicit operator IndexedPackageInteraction(PackageInteraction status)
	{
		return new(status, []);
	}
}