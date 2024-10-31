using Extensions;

using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Skyve.Domain.CS2.Paradox;

public class PdxBannedMod(int id) : IModDetails
{
	public ulong Id { get; } = (ulong)id;
	public string Name => Id.ToString();
	public string? Url { get; }
	public string? Version { get; set; }
	public string? VersionName { get; }
	public bool IsCodeMod { get; }
	public bool IsLocal { get; }
	public bool IsBuiltIn { get; }
	public ILocalPackageData? LocalData { get; }
	public IUser? Author { get; }
	public string? ThumbnailUrl { get; }
	public string? ShortDescription { get; }
	public string? Description { get; }
	public string? SuggestedGameVersion { get; }
	public DateTime ServerTime { get; }
	public long ServerSize { get; }
	public int Subscribers { get; }
	public bool HasVoted { get; set; }
	public int VoteCount { get; set; }
	public bool IsRemoved { get; }
	public bool IsBanned { get; } = true;
	public bool IsCollection { get; }
	public bool IsInvalid { get; }
	public Dictionary<string, string> Tags { get; } = [];
	public IEnumerable<IPackageRequirement> Requirements { get; } = [];
	public IEnumerable<IModChangelog> Changelog { get; } = [];
	public IEnumerable<IThumbnailObject> Images { get; } = [];
	public IEnumerable<ILink> Links { get; } = [];
	public bool IsPartialInfo { get; }
	public DateTime Timestamp { get; } = DateTime.Now.AddDays(15);
	public string? LatestVersion { get; }

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = null;
		thumbnailUrl = null;
		return true;
	}

	public bool HasComments()
	{
		return false;
	}
}
