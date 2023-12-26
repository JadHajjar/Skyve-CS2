﻿using Extensions;

using Skyve.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Skyve.Domain.CS2;
public class Asset : IAsset
{
	public Asset(ILocalPackageData package, string filePath)
	{
		FilePath = filePath;
		LocalParentPackage = package;
		LocalSize = new FileInfo(FilePath).Length;
		LocalTime = File.GetLastWriteTimeUtc(FilePath);

		Name = Path.GetFileNameWithoutExtension(FilePath).FormatWords();
		FullName = (IsLocal ? "" : $"{Id}.") + Path.GetFileNameWithoutExtension(FilePath).RemoveDoubleSpaces().Replace(' ', '_');
		AssetTags = new string[0];
	}

	public string FilePath { get; }
	public ILocalPackageData LocalParentPackage { get; }
	public long LocalSize { get; }
	public DateTime LocalTime { get; }
	public string Name { get; }
	public string[] AssetTags { get; }
	public string FullName { get; }
	public bool IsCodeMod => LocalParentPackage.IsCodeMod;
	public string Folder => LocalParentPackage.Folder;
	public bool IsLocal => LocalParentPackage.IsLocal;
	public bool IsBuiltIn => LocalParentPackage.IsBuiltIn;
	public IEnumerable<IPackageRequirement> Requirements => LocalParentPackage.Requirements;
	public ulong Id => LocalParentPackage.Id;
	public string? Url => LocalParentPackage.Url;
	ILocalPackageData? IPackage.LocalPackage => this;

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

	public IWorkshopInfo? GetWorkshopInfo()
	{
		return LocalParentPackage.GetWorkshopInfo();
	}

	public bool GetThumbnail(out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		return LocalParentPackage.GetThumbnail(out thumbnail, out thumbnailUrl);
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
