using Extensions;

using Skyve.Domain.CS2.Game;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Skyve.Domain.CS2.Content;
public class Asset : IAsset, IThumbnailObject
{
	public AssetType AssetType { get; }
	public string Folder { get; }
	public string FilePath { get; }
	public ulong Id => Package?.Id ?? 0;
	public string Name { get; set; }
	public string? Url => Package?.Url;
	public IPackage? Package { get; set; }
	public long FileSize { get; }
	public DateTime LocalTime { get; }
	public string[] Tags { get; set; }
	public string AssetId { get; }
	public SaveGameMetaData? SaveGameMetaData { get; set; }
	public MapMetaData? MapMetaData { get; set; }
	public string? Thumbnail { get; private set; }
	public string? Type { get; set; }

	public Asset(AssetType assetType, string folder, string filePath)
	{
		FilePath = filePath;
		AssetType = assetType;
		Folder = folder;
		FileSize = new FileInfo(FilePath).Length;
		LocalTime = File.GetLastWriteTimeUtc(FilePath);
		Name = Path.GetFileNameWithoutExtension(FilePath).FormatWords();
		Tags = [];
		AssetId = FilePath.Substring(Path.GetDirectoryName(Folder).Length + 1);
	}

	public Asset(string name, AssetType assetType, string folder, string filePath, long fileSize, DateTime dateModified, string[] tags)
	{
		FilePath = filePath;
		AssetType = assetType;
		Folder = folder;
		FileSize = fileSize;
		LocalTime = dateModified;
		Name = name;
		Tags = tags;
		AssetId = FilePath.Substring(Path.GetDirectoryName(Folder).Length + 1) + "_" + name;
	}

	public string SetThumbnail(IImageService imageService)
	{
		return Thumbnail = imageService.File("", AssetId.Replace('/', '_').Replace('\\', '_') + ".png").FullName;
	}

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		if (Thumbnail is not null)
		{
			thumbnail = imageService.GetImage(Thumbnail, true, AssetId.Replace('/', '_').Replace('\\', '_') + ".png", isFilePath: true).Result;
			thumbnailUrl = null;
			return true;
		}

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
