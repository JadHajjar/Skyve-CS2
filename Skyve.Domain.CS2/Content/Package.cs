using System;
using System.Collections.Generic;
using System.IO;

namespace Skyve.Domain.CS2.Content;

public class Package : IPackage, IEquatable<Package?>
{
	public string Source { get; protected set; } = string.Empty;
	public string Id { get; protected set; } = string.Empty;
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
		return obj is IPackageIdentity identity &&
			Source == identity.Source &&
			Id == identity.Id &&
			Version == identity.Version;
	}

	public bool Equals(Package? identity)
	{
		return identity is not null&&
			Source == identity.Source &&
			Id == identity.Id &&
			Version == identity.Version;
	}

	public override int GetHashCode()
	{
		var hashCode = -781363793;
		hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Source);
		hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
		hashCode = hashCode * -1521134295 + EqualityComparer<string?>.Default.GetHashCode(Version);
		return hashCode;
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
