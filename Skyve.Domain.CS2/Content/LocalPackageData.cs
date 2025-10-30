using Extensions;

using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Skyve.Domain.CS2.Content;

public class LocalPackageData : ILocalPackageData, IThumbnailObject, IEquatable<ILocalPackageData?>
{
	public IPackage Package { get; }
	public long FileSize { get; }
	public DateTime LocalTime { get; }
	public string? VersionName { get; }
	public IAsset[] Assets { get; set; }
	public IThumbnailObject[] Images { get; set; }
	public string Folder { get; set; }
	public string FilePath { get; }
	public string? SuggestedGameVersion { get; }

	public LocalPackageData(IPackage package, IAsset[] assets, IThumbnailObject[] images, string folder, string? version, string filePath, string? suggestedGameVersion)
	{
		Package = package;
		VersionName = version;
		Assets = assets;
		Folder = folder;
		FilePath = filePath.IfEmpty(Folder);
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

	public override bool Equals(object? obj)
	{
		return Equals(obj as LocalPackageData);
	}

	public bool Equals(LocalPackageData? other)
	{
		return other is not null &&
			   FilePath == other.FilePath;
	}

	public override int GetHashCode()
	{
		var hashCode = 1028951657;
		hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(FilePath);
		return hashCode;
	}

	public bool Equals(ILocalPackageData? other)
	{
		return other is not null &&
			   FilePath == other.FilePath;
	}

	bool ILocalPackageData.IsCodeMod => Package.IsCodeMod;
	ulong IPackageIdentity.Id => Package.Id;
	string IPackageIdentity.Name => Package.Name;
	string? IPackageIdentity.Url => Package.Url;
	string? IPackageIdentity.Version { get => Package.Version; set => Package.Version = value; }

	public static bool operator ==(LocalPackageData? left, LocalPackageData? right)
	{
		return EqualityComparer<LocalPackageData?>.Default.Equals(left, right);
	}

	public static bool operator !=(LocalPackageData? left, LocalPackageData? right)
	{
		return !(left == right);
	}
}
