using Skyve.Compatibility.Domain.Enums;

using System.Collections.Generic;

namespace Skyve.Systems.CS2.Domain.Api;

public class BulkCompatibilityPackageUpdate
{
	public List<ulong> Packages { get; set; } = [];
	public PackageStability Stability { get; set; }
	public string? ReviewedGameVersion { get; set; }
}
