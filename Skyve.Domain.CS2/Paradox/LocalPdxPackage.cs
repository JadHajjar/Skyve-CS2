using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;

using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using PdxIMod = PDX.SDK.Contracts.Service.Mods.Models.IMod;
using PdxMod = PDX.SDK.Contracts.Service.Mods.Models.Mod;

namespace Skyve.Domain.CS2.Paradox;

public class LocalPdxPackage : Package, PdxIMod, IWorkshopInfo
{
	private PDX.SDK.Contracts.Service.Mods.Models.LocalData PdxLocalData;

	public LocalPdxPackage(PdxMod mod, IAsset[] assets, bool isCodeMod, string? version, string? versionName, string? filePath) : base(mod.LocalData.FolderAbsolutePath, assets, mod.LocalData.ScreenshotsFilenames.ToArray(x => new ParadoxScreenshot(CrossIO.Combine(mod.LocalData.FolderAbsolutePath, x), (ulong)mod.Id, mod.Version, true)), isCodeMod, version, versionName, filePath, mod.RequiredGameVersion)
	{
		IsLocal = mod.Id <= 0;
		Id = (ulong)mod.Id;
		Guid = mod.Name;
		Name = DisplayName = mod.DisplayName;
		ShortDescription = mod.ShortDescription;
		LongDescription = mod.LongDescription;
		RequiredGameVersion = mod.RequiredGameVersion;
		VersionName = mod.UserModVersion.IfEmpty(versionName);
		LatestVersion = mod.LatestVersion;
		ThumbnailUrl = mod.ThumbnailPath;
		Author = mod.Author;
		Version = mod.Version;
		Rating = mod.Rating;
		RatingsTotal = mod.RatingsTotal;
		State = mod.State;
		LatestUpdate = mod.LatestUpdate;
		InstalledDate = mod.InstalledDate;
		SuggestedGameVersion = mod.RequiredGameVersion;
		PdxLocalData = mod.LocalData;
		ThumbnailPath = CrossIO.Combine(mod.LocalData.FolderAbsolutePath, mod.LocalData.ThumbnailFilename);
		ServerTime = mod.LatestUpdate ?? default;
		ServerSize = (long)mod.Size;
		IsCollection = false;
		VoteCount = -1;// mod.RatingsTotal;
		Subscribers = -1;
		IsRemoved = mod.State is ModState.Removed;
		IsInvalid = mod.State is ModState.Unknown;
		IsBanned = mod.State is ModState.Rejected or ModState.AutoBlocked;
		Tags = mod.Tags ?? [];
		Url = Id == 0 ? null : $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
	}

	public string DisplayName { get; set; }
	public string Author { get; set; }
	public string ShortDescription { get; set; }
	public string LongDescription { get; set; }
	public string RequiredGameVersion { get; set; }
	public string LatestVersion { get; set; }
	public string? SuggestedGameVersion { get; }
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
	public string? Description => LongDescription;
	public DateTime ServerTime { get; set; }
	public long ServerSize { get; set; }
	public int Subscribers { get; set; }
	public int VoteCount { get; set; }
	public bool IsRemoved { get; set; }
	public bool IsIncompatible { get; set; }
	public bool IsBanned { get; set; }
	public bool IsCollection { get; set; }
	public bool IsInvalid { get; set; }
	public string Guid { get; set; }
	public bool HasVoted { get; set; }
	IUser? IWorkshopInfo.Author => new PdxUser(Author);
	Dictionary<string, string> IWorkshopInfo.Tags => Tags.ToDictionary(x => x.Id, x => x.DisplayName);
	int PdxIMod.Id { get => (int)Id; set => Id = (ulong)value; }
	string PdxIMod.Name { get => Guid; set => Guid = value; }
	string PdxIMod.Version { get => Version ?? ""; set => Version = value; }
	string PdxIMod.UserModVersion { get => VersionName ?? ""; set => VersionName = value; }
	bool IWorkshopInfo.IsPartialInfo => true;
	public IEnumerable<IThumbnailObject> Images => LocalData.Images;
	IEnumerable<IPackageRequirement> IWorkshopInfo.Requirements => [];
	IEnumerable<IModChangelog> IWorkshopInfo.Changelog => [];
	IEnumerable<ILink> IWorkshopInfo.Links => [];

	bool IWorkshopInfo.HasComments()
	{
		return false;
	}

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = ThumbnailUrl;
		thumbnail = DomainUtils.GetThumbnail(imageService, ThumbnailPath, ThumbnailUrl, Id, Version ?? "");

		return true;
	}
}