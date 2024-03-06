using Extensions;

using Skyve.Compatibility.Domain.Enums;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Domain.Enums;
using Skyve.Systems.CS2.Domain.Api;

using SkyveApi.Domain.CS2;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Skyve.Systems.CS2.Domain;

public class IndexedPackage : IIndexedPackageCompatibilityInfo
{
	public PackageData Package { get; }
	public Dictionary<ulong, IIndexedPackageCompatibilityInfo> Group { get; }
	public Dictionary<ulong, IIndexedPackageCompatibilityInfo> RequirementAlternatives { get; }
	public Dictionary<StatusType, IList<IIndexedPackageStatus<StatusType>>> IndexedStatuses { get; }
	public Dictionary<InteractionType, IList<IIndexedPackageStatus<InteractionType>>> IndexedInteractions { get; }
	public IIndexedPackageStatus<InteractionType>? SucceededBy { get; private set; }

	public IndexedPackage(PackageData package)
	{
		Package = package;
		IndexedStatuses = [];
		Group = [];
		RequirementAlternatives = [];
		IndexedInteractions = [];
	}

	public void Load(Dictionary<ulong, IndexedPackage> packages)
	{
		if (Package.Statuses is not null)
		{
			foreach (var grp in Package.Statuses.GroupBy(x => x.Type))
			{
				IndexedStatuses[grp.Key] = grp.Select(x => (IIndexedPackageStatus<StatusType>)new IndexedPackageStatus(x, packages)).ToList();
			}
		}

		if (Package.Interactions is not null)
		{
			foreach (var grp in Package.Interactions.GroupBy(x => x.Type))
			{
				IndexedInteractions[grp.Key] = grp.Select(x => (IIndexedPackageStatus<InteractionType>)new IndexedPackageInteraction(x, packages)).ToList();
			}
		}

		if (IndexedStatuses.ContainsKey(StatusType.TestVersion))
		{
			foreach (var item in IndexedStatuses[StatusType.TestVersion].SelectMany(x => x.Packages))
			{
				Group[item.Key] = item.Value;

				item.Value.Group[Package.Id] = this;
			}
		}

		if (IndexedStatuses.ContainsKey(StatusType.Deprecated))
		{
			foreach (var item in IndexedStatuses[StatusType.Deprecated].SelectMany(x => x.Packages))
			{
				RequirementAlternatives[item.Key] = item.Value;
			}
		}

		if (IndexedStatuses.ContainsKey(StatusType.Reupload))
		{
			foreach (var item in IndexedStatuses[StatusType.Reupload].SelectMany(x => x.Packages))
			{
				RequirementAlternatives[item.Key] = item.Value;
			}
		}

		if (IndexedInteractions.ContainsKey(InteractionType.RequirementAlternative))
		{
			foreach (var item in IndexedInteractions[InteractionType.RequirementAlternative].SelectMany(x => x.Packages))
			{
				RequirementAlternatives[item.Key] = item.Value;
			}
		}

		if (IndexedInteractions.ContainsKey(InteractionType.Alternative))
		{
			foreach (var item in IndexedInteractions[InteractionType.Alternative].SelectMany(x => x.Packages))
			{
				RequirementAlternatives[item.Key] = item.Value;
			}
		}

		if (IndexedStatuses.ContainsKey(StatusType.Deprecated) && IndexedStatuses[StatusType.Deprecated].Any(x => x.Packages.Any()))
		{
			SucceededBy = new IndexedPackageInteraction(new() { Type = InteractionType.SucceededBy, Action = StatusAction.Switch, Packages = IndexedStatuses[StatusType.Deprecated].SelectMany(x => x.Packages.Keys).ToArray() }, packages);

			//if (!Interactions.ContainsKey(InteractionType.SucceededBy))
			//{
			//	Interactions[InteractionType.SucceededBy] = new() { interaction };
			//}
			//else
			//{
			//	Interactions[InteractionType.SucceededBy].Add(interaction);
			//}
		}
	}

	public void SetUpInteractions()
	{
		if (IndexedInteractions.ContainsKey(InteractionType.Successor))
		{
			RecursiveSetSuccessor();
		}

		//if (Interactions.ContainsKey(InteractionType.Alternative))
		//{
		//	foreach (var item in Interactions[InteractionType.Alternative])
		//	{
		//		var linkedPackages = item.Interaction.Packages.ToList();

		//		linkedPackages.Add(Package.SteamId);

		//		foreach (var package in item.Packages.Values)
		//		{
		//			var replacedInteraction = item.Interaction.Clone();

		//			replacedInteraction.Packages = linkedPackages.Where(x => x != package.Package.SteamId).ToArray();

		//			if (package.Interactions.ContainsKey(InteractionType.Alternative))
		//			{
		//				package.Interactions[InteractionType.Alternative][0].Interaction.Packages = linkedPackages.Concat(package.Interactions[InteractionType.Alternative][0].Interaction.Packages ?? new ulong[0]).Distinct().ToArray();
		//			}
		//			else
		//			{
		//				package.Interactions[InteractionType.Alternative] = new() { new(replacedInteraction, packages) };
		//			}
		//		}
		//	}
		//}
	}

	private void RecursiveSetSuccessor()
	{
		foreach (var item in IndexedInteractions[InteractionType.Successor])
		{
			foreach (var package in item.Packages.Values)
			{
				if (package.Id == Package.Id)
				{
					continue;
				}

				if (package.SucceededBy?.Packages.ContainsKey(Package.Id) ?? false)
				{
					continue;
				}

				(package as IndexedPackage)!.SucceededBy = SucceededBy ?? new IndexedPackageInteraction(new()
				{
					Type = InteractionType.SucceededBy,
					Action = item.Status.Action,
					Note = item.Status.Note,
					Packages = [Package.Id]
				}, new() { [Package.Id] = this });

				if (package.IndexedInteractions.ContainsKey(InteractionType.Successor))
				{
					(package as IndexedPackage)!.RecursiveSetSuccessor();
				}
			}
		}
	}

	public override bool Equals(object? obj)
	{
		return obj is IPackageCompatibilityInfo package && Package.Id == package.Id;
	}

	public override int GetHashCode()
	{
		return Package.Id.GetHashCode();
	}

	#region IPackageCompatibilityInfo
	public ulong Id => (Package).Id;
	public string? Name => (Package).Name;
	public string? FileName => (Package).FileName;
	public string? AuthorId => (Package).AuthorId;
	public string? Note => (Package).Note;
	public DateTime ReviewDate => (Package).ReviewDate;
	public PackageStability Stability => (Package).Stability;
	public PackageUsage Usage => (Package).Usage;
	public PackageType Type => (Package).Type;
	public List<uint>? RequiredDLCs => (Package).RequiredDLCs;
	public List<string>? Tags => (Package).Tags;
	public List<ILink>? Links => (Package).Links.ToList(x => (ILink)x);
	List<IPackageStatus<InteractionType>> IPackageCompatibilityInfo.Interactions => (Package).Interactions.ToList(x => (IPackageStatus<InteractionType>)x);
	List<IPackageStatus<StatusType>> IPackageCompatibilityInfo.Statuses => (Package).Statuses.ToList(x => (IPackageStatus<StatusType>)x);
	#endregion
}
