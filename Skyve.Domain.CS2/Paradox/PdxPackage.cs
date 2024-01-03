using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;

using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.Systems;
using Skyve.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using PdxMod = PDX.SDK.Contracts.Service.Mods.Models.Mod;

namespace Skyve.Domain.CS2.Content;

public class PdxPackage : IPackage, IWorkshopInfo
{
	public PdxPackage(PdxMod mod)
	{
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
		Rating = mod.Rating;
		RatingsTotal = mod.RatingsTotal;
		State = mod.State;
		LatestUpdate = mod.LatestUpdate;
		InstalledDate = mod.InstalledDate;
		if(mod.LocalData is not null)
		ThumbnailPath = CrossIO.Combine(mod.LocalData.FolderAbsolutePath, mod.LocalData.ThumbnailFilename);
		Name = mod.DisplayName;
		Description = mod.ShortDescription;
		ServerTime = mod.LatestUpdate ?? default;
		ServerSize = (long)mod.Size;
		IsLocal = false;
		IsCollection = false;
		IsCodeMod = mod.Tags.Any(x => x.Id == "Mod");
		VoteCount = mod.RatingsTotal;
		IsRemoved = mod.State is ModState.Removed;
		IsInvalid = mod.State is ModState.Unknown;
		IsBanned = mod.State is ModState.Rejected or ModState.AutoBlocked;
		Tags = mod.Tags;
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
	}

	public IWorkshopInfo? WorkshopInfo => this;

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
	public ILocalPackageData? LocalData { get; }
	public ulong Id { get; }
	public string Name { get; }
	public string? Url { get; }

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = ThumbnailUrl;

		if (string.IsNullOrEmpty(ThumbnailPath))
			thumbnail = imageService.GetImage(ThumbnailUrl, true, $"{Id}_{Guid}_{Path.GetFileName(ThumbnailUrl)}").Result;
		else
			thumbnail = imageService.GetImage(ThumbnailPath, true, $"{Id}_{Guid}_{Path.GetFileName(ThumbnailPath)}", isFilePath: true).Result;

		return thumbnail is not null;
	}
}