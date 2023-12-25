using Newtonsoft.Json;

using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;
using PDX.SDK.Contracts.Service.Mods.Result;
using Skyve.Domain.CS2.Steam;
using Skyve.Domain.Systems;

using Skyve.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.ParadoxMods;
public class PdxModDetails : IWorkshopInfo
{
	public PdxModDetails(ModDetails mod)
	{
		Id = (ulong)mod.Id;
		Name = mod.DisplayName;
		ThumbnailUrl = mod.ThumbnailPath;
		AuthorId = mod.Author;
		Description = mod.ShortDescription;
		ServerTime = mod.LatestUpdate ?? default;
		ServerSize = (long)mod.Size;
		Subscribers = mod.SubscriptionsTotal;
		IsCollection = false;
		Score = mod.Rating;
		ScoreVoteCount = mod.RatingsTotal;
		IsMod = true;//mod.Tags?.Contains("Mod")
		Dependencies = mod.Dependencies.ToArray();
		IsRemoved = mod.State is ModState.Removed;
		IsInvalid = mod.State is ModState.Unknown;
		IsBanned = mod.State is ModState.Rejected or ModState.AutoBlocked;
		Tags = mod.Tags.ToDictionary(x => x.Id, x => x.DisplayName);
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
		Timestamp = DateTime.Now;
	}

	public string? ThumbnailUrl { get; set; }
	public string? AuthorId { get; set; }
	public string? Description { get; set; }
	public DateTime ServerTime { get; set; }
	public long ServerSize { get; set; }
	public int Subscribers { get; set; }
	public bool IsCollection { get; set; }
	public int Score { get; set; }
	public int ScoreVoteCount { get; set; }
	public bool IsMod { get; set; }
	public ModDependency[] Dependencies { get; set; }
	public bool IsRemoved { get; set; }
	public bool IsIncompatible { get; set; }
	public bool IsBanned { get; set; }
	public bool IsInvalid { get; set; }
	public string Name { get; set; }
	public Dictionary<string, string> Tags { get; set; }
	public ulong Id { get; set; }
	public string? Url { get; set; }
	public DateTime Timestamp { get; set; }

	[JsonIgnore] public IEnumerable<IPackageRequirement> Requirements => null;// RequiredPackageIds?.Select(x => new SteamPackageRequirement(ServiceCenter.Get<ICompatibilityManager>().GetFinalSuccessor(new GenericPackageIdentity(x)).Id, !IsMod)) ?? Enumerable.Empty<IPackageRequirement>();
	[JsonIgnore] public IUser? Author => null;//ServiceCenter.Get<IWorkshopService>().GetUser(AuthorId) ?? (AuthorId > 0 ? new SteamUser { SteamId = AuthorId, Name = AuthorId.ToString() } : null);

	public bool GetThumbnail(out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = null;
		thumbnailUrl = ThumbnailUrl;

		return false;
	}
}
