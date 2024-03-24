using System.Collections.Generic;

namespace Skyve.Domain.CS2.Paradox;
public class PdxPlaysetImport
{
	public int ContractFormatVersion { get; set; }
	public GeneralDataInfo? GeneralData { get; set; }
	public Dictionary<string, ModInfo>? SubscribedMods { get; set; }

	public class GeneralDataInfo
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? DisplayImagePath { get; set; }
		public int ModsCount { get; set; }
		public int ModsSize { get; set; }
		public string? Updated { get; set; }
		public string? PlaysetModsLatestUpdated { get; set; }
		public string? UpdatedSyncedWithBackend { get; set; }
	}

	public class ModInfo : IPackageIdentity
	{
		public int Id { get; set; }
		public string? Version { get; set; }
		public int LoadOrder { get; set; }
		public bool IsEnabled { get; set; }

		ulong IPackageIdentity.Id =>(ulong)Id;
		string IPackageIdentity.Name => Id.ToString();
		string? IPackageIdentity.Url { get; }
	}
}