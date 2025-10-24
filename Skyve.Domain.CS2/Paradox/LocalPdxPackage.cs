using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Interfaces;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using PdxIMod = PDX.SDK.Contracts.Service.Mods.Interfaces.IMod;
using PdxMod = PDX.SDK.Contracts.Service.Mods.Models.Mod;

namespace Skyve.Domain.CS2.Paradox;

public class LocalPdxPackage : Package, PdxIMod, IWorkshopInfo, IFullThumbnailObject
{
	private LocalData PdxLocalData;
	private string _modId;

	public LocalPdxPackage(PdxMod mod, IAsset[] assets, bool isCodeMod, string? version, string? versionName, string? filePath) : base(mod.LocalData.FolderAbsolutePath, assets, mod.LocalData.ScreenshotsFilenames?.ToArray(x => new ParadoxScreenshot(CrossIO.Combine(mod.LocalData.FolderAbsolutePath, x), ulong.TryParse(mod.Id, out var id) ? id : 0, mod.Version, true)) ?? [], isCodeMod, version, versionName, filePath, mod.RequiredGameVersion)
	{
		_modId = mod.Id;
		Source = mod.Source;
		IsLocal = mod.Source != SourceType.PdxMods;
		Id = ulong.TryParse(mod.Id, out var id) ? id : 0;
		Guid = mod.Name;
		Name = DisplayName = mod.DisplayName;
		ShortDescription = mod.ShortDescription;
		LongDescription = mod.LongDescription;
		RequiredGameVersion = mod.RequiredGameVersion;
		VersionName = mod.UserModVersion.IfEmpty(versionName);
		LatestVersion = mod.LatestVersion;
		ThumbnailUrl = mod.ThumbnailPath;
		OperatingSystem = mod.OperatingSystem;
		ExternalLinks = mod.ExternalLinks;
		Author = mod.Author;
		Version = mod.Version;
		Rating = mod.Rating;
		RatingsTotal = mod.RatingsTotal;
		State = mod.State;
		LatestUpdate = mod.LatestUpdate;
		InstalledDate = mod.InstalledDate;
		SuggestedGameVersion = mod.RequiredGameVersion;
		PdxLocalData = mod.LocalData;
		ThumbnailPath = mod.LocalData.ThumbnailFilename is null ? string.Empty : CrossIO.Combine(mod.LocalData.FolderAbsolutePath, mod.LocalData.ThumbnailFilename);
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
	public List<ModTag> Tags { get; set; }
	public int Rating { get; set; }
	public int RatingsTotal { get; set; }
	public ModState State { get; set; }
	public DateTime? LatestUpdate { get; set; }
	public DateTime? InstalledDate { get; set; }
	LocalData PDX.SDK.Contracts.Service.Mods.Interfaces.ILocalModBase.LocalData { get => PdxLocalData; set => PdxLocalData = value; }
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
	public ModAccessControlLevelState? AccessControlLevelState { get; set; }
	IUser? IWorkshopInfo.Author => new PdxUser(Author);
	Dictionary<string, string> IWorkshopInfo.Tags => Tags.ToDictionary(x => x.Id, x => x.DisplayName);
	string IModBase.Id { get => _modId; set => _modId = value; }
	string PdxIMod.Name { get => Guid; set => Guid = value; }
	string IModBase.Version { get => Version ?? ""; set => Version = value; }
	string PdxIMod.UserModVersion { get => VersionName ?? ""; set => VersionName = value; }
	bool IWorkshopInfo.IsPartialInfo => true;
	public IEnumerable<IThumbnailObject> Images => LocalData.Images;
	IEnumerable<IPackageRequirement> IWorkshopInfo.Requirements => [];
	IEnumerable<IModChangelog> IWorkshopInfo.Changelog => [];
	IEnumerable<ILink> IWorkshopInfo.Links => [];

	public SourceType Source { get; }
	public ModPlatform OperatingSystem { get; set; }
	public List<ExternalLink> ExternalLinks { get; set; }

	bool IWorkshopInfo.HasComments()
	{
		return false;
	}

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = ThumbnailUrl;
		thumbnail = DomainUtils.GetThumbnail(imageService, ThumbnailPath, ThumbnailUrl, Id.ToString(), Version ?? "");

		return true;
	}

	public bool GetFullThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = ThumbnailUrl;
		thumbnail = DomainUtils.GetThumbnail(imageService, ThumbnailPath, ThumbnailUrl, Id.ToString(), Version ?? "", false);

		return true;
	}
}