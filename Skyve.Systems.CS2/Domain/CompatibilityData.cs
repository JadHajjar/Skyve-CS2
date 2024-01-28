using Skyve.Compatibility.Domain;

using SkyveApi.Domain.CS2;

using System.Collections.Generic;

namespace Skyve.Systems.CS2.Domain;
public class CompatibilityData
{
	public List<CompatibilityPackageData>? Packages { get; set; }
	public List<Author>? Authors { get; set; }
	public List<ulong>? BlackListedIds { get; set; }
	public List<string>? BlackListedNames { get; set; }
}
