using Skyve.Domain.Systems;
using Skyve.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
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
	string? IPackage.Version => LocalData.Version;

	public Package(string folder, IAsset[] assets, IThumbnailObject[] images, bool isCodeMod, string? version, string? filePath, string? suggestedGameVersion)
	{
		Name = Path.GetFileName(folder).TrimStart('.');
		IsCodeMod = isCodeMod;
		IsLocal = true;
		LocalData = new LocalPackageData(this, assets, images, folder, version, filePath ?? folder, suggestedGameVersion);
	}

	public void RefreshData(IAsset[] assets, bool isCodeMod, string version, string? filePath, string? suggestedGameVersion)
	{
		IsCodeMod = isCodeMod;
		IsLocal = true;
		LocalData = new LocalPackageData(this, assets, LocalData.Images, LocalData.Folder, version, filePath ?? LocalData.Folder, suggestedGameVersion);
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
