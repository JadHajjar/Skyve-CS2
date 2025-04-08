using Extensions;

using System.Collections.Generic;

namespace Skyve.Domain.CS2.Utilities
{
	[SaveName(nameof(DlcConfig) + ".json")]
	public class DlcConfig : ConfigFile
	{
		public List<ulong> AvailableDLCs { get; set; } = new();
		public List<ulong> RemovedDLCs { get; set; } = new();
	}

	[SaveName(nameof(AssetConfig) + ".json")]
	public class AssetConfig : ConfigFile
	{
	}
}