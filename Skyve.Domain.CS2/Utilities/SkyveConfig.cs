using Extensions;

using System.Collections.Generic;

namespace Skyve.Domain.CS2.Utilities;

[SaveName(nameof(DlcConfig) + ".json")]
public class DlcConfig : ConfigFile
{
	public List<uint> RemovedDLCs { get; set; } = [];
}

[SaveName(nameof(AssetConfig) + ".json")]
public class AssetConfig : ConfigFile
{
}