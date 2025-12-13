using System;
using System.Collections.Generic;
using System.IO;

namespace Skyve.Domain.CS2.Content;

public class Package : IPackage, IEquatable<Package?>
{
	public ulong Id { get; protected set; }
	public string Name { get; protected set; }
	public string? Url { get; protected set; }
	public bool IsCodeMod { get; protected set; }
	public bool IsLocal { get; protected set; }
	public bool IsBuiltIn { get; set; }
	public LocalPackageData LocalData { get; private set; }
	ILocalPackageData? IPackage.LocalData => LocalData;
	public string? Version { get; set; }
	public string? VersionName { get; set; }

	public Package(string folder, IAsset[] assets, int assetCount, IThumbnailObject[] images, bool isCodeMod, string? version, string? versionName, string? filePath, string? suggestedGameVersion)
	{
		Name = Path.GetFileName(folder).TrimStart('.');
		IsCodeMod = isCodeMod;
		IsLocal = true;
		Version = version;
		VersionName = versionName;
		LocalData = new LocalPackageData(this, assets, assetCount,  images, folder, versionName, filePath ?? folder, suggestedGameVersion);
	}

	public void RefreshData(IAsset[] assets, int assetCount, bool isCodeMod, string version, string? versionName, string? filePath, string? suggestedGameVersion)
	{
		IsCodeMod = isCodeMod;
		IsLocal = true;
		Version = version;
		VersionName = versionName;
		LocalData = new LocalPackageData(this, assets, assetCount, LocalData.Images, LocalData.Folder, versionName, filePath ?? LocalData.Folder, suggestedGameVersion);
	}

	#region EqualityOverrides
	public override string ToString()
	{
		return Name;
	}

	public override bool Equals(object? obj)
	{
		return Equals(obj as Package);
	}

	public bool Equals(Package? other)
	{
		return other is not null &&
			   LocalData.Folder == other.LocalData.Folder;
	}

	public override int GetHashCode()
	{
		return 539060726 + EqualityComparer<string>.Default.GetHashCode(LocalData.Folder);
	}

	public static bool operator ==(Package? left, Package? right)
	{
		return left?.LocalData.Folder == right?.LocalData.Folder;
	}

	public static bool operator !=(Package? left, Package? right)
	{
		return !(left == right);
	}
	#endregion
}
