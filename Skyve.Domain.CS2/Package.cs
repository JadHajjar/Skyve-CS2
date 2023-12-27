using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;

using Skyve.Domain.Systems;
using Skyve.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using PdxIMod = PDX.SDK.Contracts.Service.Mods.Models.IMod;
using PdxMod = PDX.SDK.Contracts.Service.Mods.Models.Mod;

namespace Skyve.Domain.CS2;

public class Package : IPackage, IEquatable<Package?>
{
	public ulong Id { get; protected set; }
	public string Name { get; protected set; }
	public string? Url { get; protected set; }
	public bool IsCodeMod { get; protected set; }
	public bool IsLocal { get; protected set; }
	public LocalPackageData LocalData { get; }
	public virtual IWorkshopInfo? WorkshopInfo => this.GetWorkshopInfo();
	ILocalPackageData? IPackage.LocalData => LocalData;

	public Package(string folder, long localSize, DateTime localTime, string version, string filePath)
	{
		Name = Path.GetFileName(folder);
		IsCodeMod = !string.IsNullOrEmpty(version);
		IsLocal = true;
		LocalData = new LocalPackageData(this, new IAsset[0], folder, localSize, localTime, version, filePath);
	}

	public virtual bool GetThumbnail(out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		var info = this.GetWorkshopInfo();

		if (info is not null)
		{
			return info.GetThumbnail(out thumbnail, out thumbnailUrl);
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

public class LocalPackageData : ILocalPackageData
{
	public IPackage Package { get; }
	public long LocalSize { get; }
	public DateTime LocalTime { get; }
	public string Version { get; }
	public IAsset[] Assets { get; }
	public string Folder { get; }
	public string FilePath { get; }

	public LocalPackageData(IPackage package, IAsset[] assets, string folder, long localSize, DateTime localTime, string version, string filePath)
	{
		Package = package;
		LocalSize = localSize;
		LocalTime = localTime;
		Version = version;
		Assets = assets;
		Folder = folder;
		FilePath = filePath;
	}

	public bool GetThumbnail(out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		return Package.GetThumbnail(out thumbnail, out thumbnailUrl); 
	}

	bool ILocalPackageData.IsCodeMod => Package.IsCodeMod;
	ulong IPackageIdentity.Id => Package.Id;
	string IPackageIdentity.Name => Package.Name;
	string? IPackageIdentity.Url => Package.Url;
}

public class LocalPdxPackage : Package, PdxIMod, IWorkshopInfo
{
	private PDX.SDK.Contracts.Service.Mods.Models.LocalData PdxLocalData;

	public LocalPdxPackage(PdxMod mod) : base(mod.LocalData.FolderAbsolutePath, (long)mod.Size, mod.LatestUpdate ?? DateTime.Now, mod.Version, mod.LocalData.FolderAbsolutePath)
	{
		IsLocal = false;
		Id = (ulong)mod.Id;
		Guid = mod.Name;
		Name = DisplayName = mod.DisplayName;
		ShortDescription = mod.ShortDescription;
		LongDescription = mod.LongDescription;
		RequiredGameVersion = mod.RequiredGameVersion;
		UserModVersion = mod.UserModVersion;
		LatestVersion = mod.LatestVersion;
		ThumbnailUrl = mod.ThumbnailPath;
		Author = mod.Author;
		Version = mod.Version;
		//Tags
		Rating = mod.Rating;
		RatingsTotal = mod.RatingsTotal;
		State = mod.State;
		LatestUpdate = mod.LatestUpdate;
		InstalledDate = mod.InstalledDate;
		PdxLocalData = mod.LocalData;
		ThumbnailPath = CrossIO.Combine(mod.LocalData.FolderAbsolutePath, mod.LocalData.ThumbnailFilename);
		Name = mod.DisplayName;
		Description = mod.ShortDescription;
		ServerTime = mod.LatestUpdate ?? default;
		ServerSize = (long)mod.Size;
		IsCollection = false;
		Score = mod.Rating;
		ScoreVoteCount = mod.RatingsTotal;
		IsRemoved = mod.State is ModState.Removed;
		IsInvalid = mod.State is ModState.Unknown;
		IsBanned = mod.State is ModState.Rejected or ModState.AutoBlocked;
		Tags = mod.Tags;
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
	}

	public string Version { get; set; }
	public string DisplayName { get; set; }
	public string Author { get; set; }
	public string ShortDescription { get; set; }
	public string LongDescription { get; set; }
	public string RequiredGameVersion { get; set; }
	public string UserModVersion { get; set; }
	public string LatestVersion { get; set; }
	public string ThumbnailPath { get; set; }
	public ulong Size { get; set; }
	public List<PDX.SDK.Contracts.Service.Mods.Models.ModTag> Tags { get; set; }
	public int Rating { get; set; }
	public int RatingsTotal { get; set; }
	public ModState State { get; set; }
	public DateTime? LatestUpdate { get; set; }
	public DateTime? InstalledDate { get; set; }
	PDX.SDK.Contracts.Service.Mods.Models.LocalData PdxIMod.LocalData { get => PdxLocalData; set => PdxLocalData = value; }
	public string? ThumbnailUrl { get; set; }
	public string? Description { get; set; }
	public DateTime ServerTime { get; set; }
	public long ServerSize { get; set; }
	public int Subscribers { get; set; }
	public int Score { get; set; }
	public int ScoreVoteCount { get; set; }
	public bool IsMod { get; set; }
	public bool IsRemoved { get; set; }
	public bool IsIncompatible { get; set; }
	public bool IsBanned { get; set; }
	public bool IsCollection { get; set; }
	public bool IsInvalid { get; set; }
	public IEnumerable<IPackageRequirement> Requirements { get; set; }
	public string Guid { get; set; }
	IUser? IWorkshopInfo.Author { get; }
	Dictionary<string, string> IWorkshopInfo.Tags => Tags.ToDictionary(x => x.Id, x => x.DisplayName);
	int PdxIMod.Id { get => (int)Id; set => Id = (ulong)value; }
	string PdxIMod.Name { get => Guid; set => Guid = value; }

	public override bool GetThumbnail(out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = ThumbnailUrl;

		thumbnail = ServiceCenter.Get<IImageService>().GetImage(ThumbnailPath, true, isFilePath: true).Result;

		return thumbnail is not null;
	}
}