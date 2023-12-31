﻿using Extensions;

using Newtonsoft.Json;

using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Skyve.Domain.CS2.Paradox;
public class PdxModDetails : IPackage, IWorkshopInfo, ITimestamped
{
	[Obsolete("", true)]
    public PdxModDetails()
    {
		Name = string.Empty;
		Guid = string.Empty;
		Version = string.Empty;
		Tags = [];
	}

    public PdxModDetails(ModDetails mod, bool hasVoted)
	{
		Id = (ulong)mod.Id;
		Name = mod.DisplayName;
		Guid = mod.Name;
		ThumbnailUrl = mod.ThumbnailPath;
		AuthorId = mod.Author;
		ShortDescription = mod.ShortDescription;
		Description = mod.LongDescription;
		ServerTime = mod.LatestUpdate ?? default;
		ServerSize = (long)mod.Size;
		Version = mod.UserModVersion;
		Subscribers = mod.SubscriptionsTotal;
		HasVoted = hasVoted;
		VoteCount = mod.RatingsTotal;
		IsRemoved = mod.State is ModState.Removed;
		IsInvalid = mod.State is ModState.Unknown;
		IsBanned = mod.State is ModState.Rejected or ModState.AutoBlocked;
		Tags = mod.Tags.ToDictionary(x => x.Id, x => x.DisplayName);
		Requirements = mod.Dependencies?.ToArray(x => new PdxModRequirement(x)) ?? [];
		Changelog = mod.Changelog?.ToArray(x => new ModChangelog(x)) ?? [];
		Timestamp = DateTime.Now;
	}

	public ulong Id { get; set; }
	public DateTime Timestamp { get; set; }
	public string Name { get; set; }
	public string Guid { get; set; }
	public DateTime ServerTime { get; set; }
	public string? ThumbnailUrl { get; set; }
	public string? AuthorId { get; set; }
	public string? ShortDescription { get; set; }
	public string? Description { get; set; }
	public long ServerSize { get; set; }
	public string Version { get; set; }
	public int Subscribers { get; set; }
	public bool IsCollection { get; set; }
	public int VoteCount { get; set; }
	public bool IsRemoved { get; set; }
	public bool IsIncompatible { get; set; }
	public bool IsBanned { get; set; }
	public bool IsInvalid { get; set; }
	public bool HasVoted { get; set; }
	public PdxModRequirement[]? Requirements { get; set; }
	public ModChangelog[]? Changelog { get; set; }
	public Dictionary<string, string> Tags { get; set; }

	bool IPackage.IsLocal { get; }
	ILocalPackageData? IPackage.LocalData { get; }
	string? IPackageIdentity.Url => $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
	bool IPackage.IsCodeMod => Tags.Any(x => x.Key == "Mod");
	bool IWorkshopInfo.IsCodeMod => Tags.Any(x => x.Key == "Mod");
	IUser? IWorkshopInfo.Author => string.IsNullOrWhiteSpace(AuthorId) ? null : new PdxUser(AuthorId!);
	IEnumerable<IPackageRequirement> IWorkshopInfo.Requirements => Requirements ?? [];
	IEnumerable<IModChangelog> IWorkshopInfo.Changelog => Changelog ?? [];

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = string.IsNullOrEmpty(ThumbnailUrl) ? null : imageService.GetImage(ThumbnailUrl, true, $"{Id}_{Guid}_{Path.GetFileName(ThumbnailUrl)}").Result;
		thumbnailUrl = null;

		return true;
	}
}
