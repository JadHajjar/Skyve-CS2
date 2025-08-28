using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using PdxIMod = PDX.SDK.Contracts.Service.Mods.Models.IMod;
using PdxMod = PDX.SDK.Contracts.Service.Mods.Models.Mod;

namespace Skyve.Domain.CS2.Content;

public class PdxPackage : IPackage, PdxIMod, IWorkshopInfo, IThumbnailObject, IFullThumbnailObject
{
	private LocalData PdxLocalData;

	public PdxPackage(PdxMod mod)
	{
		Id = (ulong)mod.Id;
		Guid = mod.Name;
		Name = DisplayName = mod.DisplayName;
		ShortDescription = mod.ShortDescription;
		LongDescription = mod.LongDescription;
		RequiredGameVersion = mod.RequiredGameVersion;
		VersionName = mod.UserModVersion;
		LatestVersion = mod.LatestVersion;
		ThumbnailUrl = mod.ThumbnailPath;
		Author = mod.Author;
		Version = mod.Version;
		Rating = mod.Rating;
		RatingsTotal = mod.RatingsTotal;
		State = mod.State;
		LatestUpdate = mod.LatestUpdate;
		InstalledDate = mod.InstalledDate;
		Description = mod.ShortDescription;
		ServerTime = mod.LatestUpdate ?? default;
		ServerSize = (long)mod.Size;
		IsLocal = false;
		IsCollection = false;
		IsCodeMod = mod.Tags.Any(x => x.Id == "Code Mod");
		VoteCount = mod.RatingsTotal;
		IsRemoved = mod.State is ModState.Removed;
		IsInvalid = mod.State is ModState.Unknown;
		IsBanned = mod.State is ModState.Rejected or ModState.AutoBlocked;
		Tags = mod.Tags;
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";

		PdxLocalData = mod.LocalData;

		if (mod.LocalData?.FolderAbsolutePath is not null)
		{
			LocalData = new LocalPackageData(this, [], [], mod.LocalData.FolderAbsolutePath, mod.UserModVersion, mod.LocalData.ContentFileOrFolder, mod.RequiredGameVersion);

			ThumbnailPath = CrossIO.Combine(mod.LocalData.FolderAbsolutePath, mod.LocalData.ThumbnailFilename);
		}
		else
		{
			ThumbnailPath = string.Empty;
		}

		if (mod is ModSearch modSearch)
		{
			Subscribers = modSearch.InstalledCount;
		}
	}

	public string? Version { get; set; }
	public string VersionName { get; set; }
	public string DisplayName { get; set; }
	public string Author { get; set; }
	public string ShortDescription { get; set; }
	public string LongDescription { get; set; }
	public string RequiredGameVersion { get; set; }
	public string LatestVersion { get; set; }
	public string ThumbnailPath { get; set; }
	public ulong Size { get; set; }
	public List<ModTag> Tags { get; set; }
	public int Rating { get; set; }
	public int RatingsTotal { get; set; }
	public ModState State { get; set; }
	public DateTime? LatestUpdate { get; set; }
	public DateTime? InstalledDate { get; set; }
	public string? ThumbnailUrl { get; set; }
	public string? Description { get; set; }
	public DateTime ServerTime { get; set; }
	public long ServerSize { get; set; }
	public int Subscribers { get; set; }
	public int VoteCount { get; set; }
	public bool IsRemoved { get; set; }
	public bool IsIncompatible { get; set; }
	public bool IsBanned { get; set; }
	public bool IsCollection { get; set; }
	public bool IsInvalid { get; set; }
	public IEnumerable<IPackageRequirement> Requirements => [];
	public string Guid { get; set; }
	IUser? IWorkshopInfo.Author => new PdxUser(Author);
	Dictionary<string, string> IWorkshopInfo.Tags => Tags.ToDictionary(x => x.Id, x => x.DisplayName);
	public bool HasVoted { get; set; }
	public bool IsCodeMod { get; }
	public bool IsLocal { get; }
	public bool IsBuiltIn { get; }
	public ILocalPackageData? LocalData { get; }
	public ulong Id { get; private set; }
	public string Name { get; }
	public string? Url { get; }
	public ModAccessControlLevelState? AccessControlLevelState { get; set; }
	int PdxIMod.Id { get => (int)Id; set => Id = (ulong)value; }
	string PdxIMod.Name { get => Guid; set => Guid = value; }
	string? IWorkshopInfo.SuggestedGameVersion => RequiredGameVersion;
	bool IWorkshopInfo.IsPartialInfo => true;
	LocalData PdxIMod.LocalData { get => PdxLocalData; set => PdxLocalData = value; }
	IEnumerable<IModChangelog> IWorkshopInfo.Changelog => [];
	IEnumerable<IThumbnailObject> IWorkshopInfo.Images => [];
	IEnumerable<ILink> IWorkshopInfo.Links => [];
	string PdxIMod.UserModVersion { get => VersionName; set => VersionName = value; }

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = ThumbnailUrl;
		thumbnail = DomainUtils.GetThumbnail(imageService, ThumbnailPath, ThumbnailUrl, Id, Version ?? string.Empty);

		return true;
	}

	bool IWorkshopInfo.HasComments()
	{
		return false;
	}

	public bool GetFullThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = ThumbnailUrl;
		thumbnail = DomainUtils.GetThumbnail(imageService, ThumbnailPath, ThumbnailUrl, Id, Version ?? string.Empty, false);

		return true;
	}
}