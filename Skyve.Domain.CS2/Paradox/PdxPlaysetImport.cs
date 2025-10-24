using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Paradox;
public class PdxPlaysetImport : ITemporaryPlayset
{
	public int ContractFormatVersion { get; set; }
	public GeneralDataInfo? GeneralData { get; set; }
	public Dictionary<string, ModInfo>? SubscribedMods { get; set; }
	public Dictionary<string, ModInfo>? LocalMods { get; set; }

	public class GeneralDataInfo
	{
		public string? Id { get; set; }
		public string? Name { get; set; }
        public byte[]? BannerBytes { get; set; }
        public Color? Color { get; set; }
		public PackageUsage? Usage { get; set; }
	}

	public class ModInfo : IPlaysetPackage
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Version { get; set; }
		public int LoadOrder { get; set; }
		public bool IsEnabled { get; set; }
		public bool IsVersionLocked { get; set; }

		ulong IPackageIdentity.Id => (ulong)Id;
		string IPackageIdentity.Name => Name ?? Id.ToString();
		string? IPackageIdentity.Url { get; }
		bool IPackage.IsCodeMod { get; }
		bool IPackage.IsBuiltIn { get; }
		bool IPackage.IsLocal => Id <= 0;
		string? IPackage.VersionName { get; }
		ILocalPackageData? IPackage.LocalData { get; }
	}

	string? IPlayset.Id => GeneralData?.Id;
	string? IPlayset.Name => GeneralData?.Name;
	DateTime IPlayset.DateUpdated { get; } = DateTime.Now;
	int IPlayset.ModCount { get => SubscribedMods?.Count ?? 0; set { } }
	ulong IPlayset.ModSize { get; set; }

	bool IThumbnailObject.GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = null;
		thumbnailUrl = null;
		return false;
	}

	Task<IEnumerable<IPackageIdentity>> ITemporaryPlayset.GetPackages()
	{
		return Task.FromResult((SubscribedMods?.Values.Cast<IPackageIdentity>() ?? []).Concat(LocalMods?.Values.Cast<IPackageIdentity>() ?? []));
	}
}