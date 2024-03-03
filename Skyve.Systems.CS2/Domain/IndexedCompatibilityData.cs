﻿using Extensions;

using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Systems.CS2.Domain.Api;
using Skyve.Systems.CS2.Domain.DTO;

using SkyveApi.Domain.CS2;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Skyve.Systems.CS2.Domain;

public class IndexedCompatibilityData
{
	public IndexedCompatibilityData(PackageData[]? packages = null, List<ulong>? blackListIds = null, List<string>? blackListNames = null)
	{
		Packages = packages?.ToDictionary(x => x.Id, x => GenerateIndexedPackage(x, packages)) ?? [];
		PackageNames = new(StringComparer.InvariantCultureIgnoreCase);
		BlackListedIds = new(blackListIds ?? []);
		BlackListedNames = new(blackListNames ?? []);

		foreach (var item in Packages.Values)
		{
			if (item.Package.FileName is not null and not "")
			{
				PackageNames[item.Package.FileName] = item.Package.Id;
			}

			item.Load(Packages);
		}

		foreach (var item in Packages.Values)
		{
			item.SetUpInteractions();
		}
	}

	private static IndexedPackage GenerateIndexedPackage(PackageData package, PackageData[] packages)
	{
		var nonTest = package.Statuses?.FirstOrDefault(x => x.Type == StatusType.TestVersion && (x.Packages?.Any() ?? false));

		if (nonTest is not null)
		{
			var id = nonTest.Packages![0];
			var stable = packages.FirstOrDefault(x => x.Id == id);

			if (stable is not null)
			{
				package.Links = stable.Links;
				package.RequiredDLCs = stable.RequiredDLCs;
				package.Tags = stable.Tags;
				package.Note = stable.Note;
				package.Usage = stable.Usage;
				package.Type = stable.Type;
				package.Statuses ??= [];
				package.Statuses.AddRange(stable.Statuses ?? []);
				package.Interactions ??= [];
				package.Interactions.AddRange(stable.Interactions?.Where(x => x.Type > InteractionType.Alternative) ?? Enumerable.Empty<PackageInteraction>());
			}
		}

		return new IndexedPackage(package);
	}

	public Dictionary<string, ulong> PackageNames { get; }
	public Dictionary<ulong, IndexedPackage> Packages { get; }
	public HashSet<ulong> BlackListedIds { get; }
	public HashSet<string> BlackListedNames { get; }
}
