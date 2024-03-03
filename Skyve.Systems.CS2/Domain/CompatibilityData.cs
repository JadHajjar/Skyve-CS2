using Extensions;

using Skyve.Compatibility.Domain;
using Skyve.Systems.CS2.Domain.Api;

using SkyveApi.Domain.CS2;

using System.Collections.Generic;

namespace Skyve.Systems.CS2.Domain;
[SaveName("CompatibilityDataCache.json")]
public class CompatibilityData : ISaveObject
{
	public PackageData[]? Packages { get; set; }
	public List<ulong>? BlackListedIds { get; set; }
	public List<string>? BlackListedNames { get; set; }

	public SaveHandler? Handler { get; set; }
}
