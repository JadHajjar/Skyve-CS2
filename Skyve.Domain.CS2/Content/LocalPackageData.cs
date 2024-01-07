using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;

using Skyve.Domain.Systems;

using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Skyve.Domain.CS2.Content;

public class LocalPackageData : ILocalPackageData
{
	public IPackage Package { get; }
	public long FileSize { get; }
	public DateTime LocalTime { get; }
	public string Version { get; }
	public IAsset[] Assets { get; set; }
	public string Folder { get; }
	public string FilePath { get; }

	public LocalPackageData(IPackage package, IAsset[] assets, string folder, string version, string filePath)
	{
		Package = package;
		Version = version;
		Assets = assets;
		Folder = folder;
		FilePath = filePath;

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
		return Package.GetThumbnail(imageService, out thumbnail, out thumbnailUrl);
	}

	bool ILocalPackageData.IsCodeMod => Package.IsCodeMod;
	ulong IPackageIdentity.Id => Package.Id;
	string IPackageIdentity.Name => Package.Name;
	string? IPackageIdentity.Url => Package.Url;
}
