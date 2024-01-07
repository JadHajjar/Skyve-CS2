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
	public LocalPackageData LocalData { get; private set; }
	ILocalPackageData? IPackage.LocalData => LocalData;
	string? IPackage.Version => LocalData.Version;

	public Package(string folder, IAsset[] assets, bool isCodeMod, string? version, string? filePath)
	{
		Name = Path.GetFileName(folder);
		IsCodeMod = isCodeMod;
		IsLocal = true;
		LocalData = new LocalPackageData(this, assets, folder, version, filePath ?? folder);
	}

	public void RefreshData(IAsset[] assets, bool isCodeMod, string version, string? filePath)
	{
		IsCodeMod = isCodeMod;
		IsLocal = true;
		LocalData = new LocalPackageData(this, assets, LocalData.Folder, version, filePath ?? LocalData.Folder);
	}

	public virtual bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		var info = this.GetWorkshopInfo();

		if (info is not null)
		{
			return info.GetThumbnail(imageService, out thumbnail, out thumbnailUrl);
		}

		thumbnail = null;
		thumbnailUrl = null;
		return false;
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
