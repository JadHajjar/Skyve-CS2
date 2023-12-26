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

public class Package : ILocalPackageData, IEqualityComparer<Package>
{
	public IAsset[] Assets { get; set; }
	public IMod? Mod { get; set; }
	public long LocalSize { get; set; }
	public DateTime LocalTime { get; set; }
	public string Folder { get; protected set; }
	public bool IsCodeMod { get; protected set; }
	public bool IsLocal { get; protected set; }
	public bool IsBuiltIn { get; protected set; }
	public string FilePath { get; protected set; }
	public ulong Id { get; protected set; }
	public string Name { get; protected set; }
	public string? Url { get; protected set; }
	public ILocalPackageData? LocalPackage => this;
	public ILocalPackageData LocalParentPackage => this;
	public IEnumerable<IPackageRequirement> Requirements => this.GetWorkshopInfo()?.Requirements ?? Enumerable.Empty<IPackageRequirement>();

	public Package(string folder, long localSize, DateTime localTime)
	{
		Folder = FilePath = folder.FormatPath();
		Name = Path.GetFileName(Folder);
		LocalSize = localSize;
		LocalTime = localTime;
		IsLocal = true;
		Assets = new Asset[0];
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
		return obj is ILocalPackageData package && Folder == package.Folder;
	}

	public override int GetHashCode()
	{
		return -1486376059 + EqualityComparer<string>.Default.GetHashCode(Folder);
	}

	public bool Equals(Package x, Package y)
	{
		return x.Folder == y.Folder;
	}

	public int GetHashCode(Package obj)
	{
		return obj.GetHashCode();
	}

	public static bool operator ==(Package? left, Package? right)
	{
		return left?.Folder == right?.Folder;
	}

	public static bool operator !=(Package? left, Package? right)
	{
		return !(left == right);
	}
	#endregion
}

public class PdxPackage : Package, PdxIMod, IWorkshopInfo
{
	public PdxPackage(PdxMod mod) : base(mod.LocalData.FolderAbsolutePath, (long)mod.Size, mod.LatestUpdate ?? DateTime.Now)
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
		LocalSize = (long)mod.Size;
		//Tags
		Rating = mod.Rating;
		RatingsTotal = mod.RatingsTotal;
		State = mod.State;
		LatestUpdate = mod.LatestUpdate;
		InstalledDate = mod.InstalledDate;
		LocalData = mod.LocalData;

		if (mod.LocalData is not null)
		{
			Folder = mod.LocalData.FolderAbsolutePath;
			ThumbnailPath = CrossIO.Combine(Folder, mod.LocalData.ThumbnailFilename);
		}

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
	public PDX.SDK.Contracts.Service.Mods.Models.LocalData LocalData { get; set; }
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