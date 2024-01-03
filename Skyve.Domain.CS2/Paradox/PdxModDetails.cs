using Extensions;

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
    { }

    public PdxModDetails(ModDetails mod, bool hasVoted)
	{
		Id = (ulong)mod.Id;
		Name = mod.DisplayName;
		Guid = mod.Name;
		ThumbnailUrl = mod.ThumbnailPath;
		AuthorId = mod.Author;
		Description = mod.ShortDescription;
		ServerTime = mod.LatestUpdate ?? default;
		ServerSize = (long)mod.Size;
		Version = mod.Version;
		Subscribers = mod.SubscriptionsTotal;
		IsCollection = false;
		HasVoted = hasVoted;
		VoteCount = mod.RatingsTotal;
		Dependencies = mod.Dependencies?.ToArray() ?? [];
		IsRemoved = mod.State is ModState.Removed;
		IsInvalid = mod.State is ModState.Unknown;
		IsBanned = mod.State is ModState.Rejected or ModState.AutoBlocked;
		Playsets = mod.Playsets;
		Tags = mod.Tags.ToDictionary(x => x.Id, x => x.DisplayName);
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
		Timestamp = DateTime.Now;
	}

	public ulong Id { get; set; }
	public string Name { get; set; }
	public string Guid { get; set; }
	public string? Url { get; set; }
	public DateTime Timestamp { get; set; }
	public string? ThumbnailUrl { get; set; }
	public string? AuthorId { get; set; }
	public string? Description { get; set; }
	public DateTime ServerTime { get; set; }
	public long ServerSize { get; set; }
	public string Version { get; set; }
	public int Subscribers { get; set; }
	public bool IsCollection { get; set; }
	public int VoteCount { get; set; }
	public ModDependency[] Dependencies { get; set; }
	public bool IsRemoved { get; set; }
	public bool IsIncompatible { get; set; }
	public bool IsBanned { get; set; }
	public List<PlaysetInMod> Playsets { get; }
	public bool IsInvalid { get; set; }
	public bool HasVoted { get; set; }
	public Dictionary<string, string> Tags { get; set; }
	[JsonIgnore] public bool IsLocal { get; }
	[JsonIgnore] public bool IsCodeMod => Tags.Any(x => x.Key == "Mod");
	[JsonIgnore] public ILocalPackageData? LocalData { get; }
	[JsonIgnore] public IWorkshopInfo? WorkshopInfo => this;
	[JsonIgnore] public IUser? Author => string.IsNullOrWhiteSpace(AuthorId) ? null : new PdxUser(AuthorId!);
	[JsonIgnore] public IEnumerable<IPackageRequirement> Requirements => Dependencies?.Select(x => new PdxModRequirement(x)) ?? [];

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = string.IsNullOrEmpty(ThumbnailUrl) ? null : imageService.GetImage(ThumbnailUrl, true, $"{Id}_{Guid}_{Path.GetFileName(ThumbnailUrl)}").Result;
		thumbnailUrl = null;

		return true;
	}
}
