using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Domain.Enums;

using System;
using System.Collections.Generic;

namespace Skyve.Systems.CS2.Domain.Api;
public class PackageData
{
	public ulong Id { get; set; }
	public string? Name { get; set; }
	public string? FileName { get; set; }
	public string? AuthorId { get; set; }
	public string? Note { get; set; }
	public string? EditNote { get; set; }
	public DateTime ReviewDate { get; set; }
	public string? ReviewedGameVersion { get; set; }
	public PackageStability Stability { get; set; }
	public PackageUsage Usage { get; set; } = (PackageUsage)(-1);
	public PackageType Type { get; set; }
	public List<uint> RequiredDLCs { get; set; }
	public List<string> Tags { get; set; }
	public List<PackageLink> Links { get; set; }
	public List<PackageStatus> Statuses { get; set; }
	public List<PackageInteraction> Interactions { get; set; }

    public PackageData()
    {
        Tags = [];
		Links = [];
		Statuses = [];
		Interactions = [];
		RequiredDLCs = [];
	}
}
