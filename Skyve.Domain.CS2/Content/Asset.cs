using Extensions;

using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Skyve.Domain.CS2.Content;
public class Asset : IAsset
{
	public string Folder { get; }
	public string FilePath { get; }
	public ulong Id { get; }
	public string Name { get; }
	public string? Url { get; }
	public IPackage Package { get; }
	public long FileSize { get; }
	public DateTime LocalTime { get; }
	public string[] Tags { get; }

	public Asset(IPackage package, string filePath)
	{
		FilePath = filePath;
		Package = package;
		Folder = package.LocalData!.Folder;
		FileSize = new FileInfo(FilePath).Length;
		LocalTime = File.GetLastWriteTimeUtc(FilePath);
		Name = Path.GetFileNameWithoutExtension(FilePath).FormatWords();
		Tags = new string[0];
	}

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		return Package.GetThumbnail(imageService, out thumbnail, out thumbnailUrl);
	}

	public override bool Equals(object? obj)
	{
		return obj is IAsset asset &&
			   FilePath == asset.FilePath;
	}

	public override int GetHashCode()
	{
		return 901043656 + EqualityComparer<string>.Default.GetHashCode(FilePath);
	}

	public override string ToString()
	{
		return Name;
	}

	public static bool operator ==(Asset? left, Asset? right)
	{
		return
			left is null ? right is null :
			right is null ? left is null :
			EqualityComparer<Asset>.Default.Equals(left, right);
	}

	public static bool operator !=(Asset? left, Asset? right)
	{
		return !(left == right);
	}
}
