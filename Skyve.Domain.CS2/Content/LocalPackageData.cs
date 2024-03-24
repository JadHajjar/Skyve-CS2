using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;

using Skyve.Domain.Systems;

using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Skyve.Domain.CS2.Content;

public class LocalPackageData : ILocalPackageData, IThumbnailObject
{
	public IPackage Package { get; }
	public long FileSize { get; }
	public DateTime LocalTime { get; }
	public string? Version { get; }
	public IAsset[] Assets { get; set; }
	public IThumbnailObject[] Images { get; set; }
	public string Folder { get; set; }
	public string FilePath { get; }
	public string? SuggestedGameVersion { get; }

	public LocalPackageData(IPackage package, IAsset[] assets, IThumbnailObject[] images, string folder, string? version, string filePath, string? suggestedGameVersion)
	{
		Package = package;
		Version = version;
		Assets = assets;
		Folder = folder;
		FilePath = filePath;
		Images = images;
		SuggestedGameVersion = suggestedGameVersion;

		for (var i = 0; i < Assets.Length; i++)
		{
			Assets[i].Package = Package;
		}

		try
		{
			if (!Directory.Exists(Folder))
			{
				return;
			}

			var files = new DirectoryInfo(Folder)
				.GetFiles("*", SearchOption.AllDirectories)
				.AllWhere(x => Path.GetFileName(Path.GetDirectoryName(x.FullName)) is not (".cpatch" or ".metadata"));

			FileSize = files.Sum(f => f.Length);
			LocalTime = files.Max(x => x.LastWriteTimeUtc);
		}
		catch { }
	}

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		if (Package is IThumbnailObject thumbnailObject)
		{
			return thumbnailObject.GetThumbnail(imageService, out thumbnail, out thumbnailUrl);
		}

		thumbnail = null;
		thumbnailUrl = null;
		return false;
	}

	bool ILocalPackageData.IsCodeMod => Package.IsCodeMod;
	ulong IPackageIdentity.Id => Package.Id;
	string IPackageIdentity.Name => Package.Name;
	string? IPackageIdentity.Url => Package.Url;
}
