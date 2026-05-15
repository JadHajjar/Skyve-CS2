using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Interfaces;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using PdxIMod = PDX.SDK.Contracts.Service.Mods.Interfaces.IMod;

namespace Skyve.Domain.CS2.Paradox;

public class LocalPdxPackage : Package, IModBase, IWorkshopInfo, IFullThumbnailObject
{
	private LocalData PdxLocalData;
	private string _modId;

	public LocalPdxPackage(PdxIMod mod, IAsset[] assets, int assetCount, bool isCodeMod, string? version, string? versionName, string? filePath) : base(mod.LocalData.FolderAbsolutePath, assets, assetCount, [], isCodeMod, version, versionName, filePath, mod.RequiredGameVersion)
	{
		_modId = mod.Id;
		Source = mod.Source;
		IsLocal = mod.Source != SourceType.PdxMods;
		Id = mod.Id;
		Name = DisplayName = mod.DisplayName;
		ShortDescription = mod.ShortDescription;
		LongDescription = string.Empty;
		RequiredGameVersion = mod.RequiredGameVersion;
		VersionName = mod.UserModVersion.IfEmpty(versionName);
		LatestVersion = mod.LatestVersion;
		ThumbnailUrl = mod.ThumbnailUrl;
		OperatingSystem = mod.OperatingSystem ?? ModPlatform.Any;
		ExternalLinks = [];
		Author = mod.Author;
		Version = mod.Version;
		Rating = 0;
		RatingsTotal = mod.RatingsTotal;
		State = mod.State;
		LatestUpdate = mod.LatestModUpdate;
		InstalledDate = mod.SubscriptionDate;
		SuggestedGameVersion = mod.RequiredGameVersion;
		PdxLocalData = mod.LocalData;
		AccessLevel = mod.AccessControlLevelState switch { ModAccessControlLevelState.Unlisted => AccessLevel.Unlisted, ModAccessControlLevelState.Private => AccessLevel.Private, _ => AccessLevel.Public };
		ThumbnailPath = mod.LocalData.ThumbnailFilename is null ? string.Empty : CrossIO.Combine(mod.LocalData.FolderAbsolutePath, mod.LocalData.ThumbnailFilename);
		ServerTime = mod.UpdatedDate ?? default;
		ServerSize = (long)mod.Size;
		IsCollection = false;
		VoteCount = -1;// mod.RatingsTotal;
		Subscribers = -1;
		IsRemoved = mod.State is ModState.Removed;
		IsInvalid = mod.State is ModState.Unknown;
		IsBanned = mod.State is ModState.Rejected or ModState.AutoBlocked;
		Tags = mod.Tags?.ToList() ?? [];
		Url = string.IsNullOrEmpty(Id) ? null : $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
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
	public List<PDX.SDK.Contracts.Service.Mods.Interfaces.IModTag> Tags { get; set; }
	public int Rating { get; set; }
	public int RatingsTotal { get; set; }
	public ModState State { get; set; }
	public DateTime? LatestUpdate { get; set; }
	public DateTime? InstalledDate { get; set; }
	public string? ThumbnailUrl { get; set; }
	public string? Description => LongDescription;
	public DateTime ServerTime { get; set; }
	public long ServerSize { get; set; }
	public int Subscribers { get; set; }
	public int VoteCount { get; set; }
	public bool IsRemoved { get; set; }
	public bool IsBanned { get; set; }
	public bool IsCollection { get; set; }
	public bool IsInvalid { get; set; }
	public bool HasVoted { get; set; }
	IUser? IWorkshopInfo.Author => new PdxUser(Author);
	Dictionary<string, string> IWorkshopInfo.Tags => Tags.ToDictionary(x => x.Id, x => x.DisplayName);
	string IModBase.Id { get => _modId; set => _modId = value; }
	string IModBase.Version { get => Version ?? ""; set => Version = value; }
	bool IWorkshopInfo.IsPartialInfo => true;
	public IEnumerable<IThumbnailObject> Images => LocalData.Images;
	IEnumerable<IPackageRequirement> IWorkshopInfo.Requirements => [];
	IEnumerable<IModChangelog> IWorkshopInfo.Changelog => [];
	IEnumerable<ILink> IWorkshopInfo.Links => [];
	public ModPlatform OperatingSystem { get; set; }
	public List<ExternalLink> ExternalLinks { get; set; }
	SourceType IModBase.Source => Source;
	public AccessLevel AccessLevel { get; }

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