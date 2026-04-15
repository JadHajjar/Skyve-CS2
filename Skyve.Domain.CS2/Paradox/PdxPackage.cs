using Extensions;

using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Interfaces;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using IPdxTag = PDX.SDK.Contracts.Service.Mods.Interfaces.IModTag;
using PdxIMod = PDX.SDK.Contracts.Service.Mods.Interfaces.ILocalModBase;

namespace Skyve.Domain.CS2.Content;

public class PdxPackage : IPackage, PdxIMod, IWorkshopInfo, IThumbnailObject, IFullThumbnailObject
{
	private LocalData PdxLocalData;

	public PdxPackage(IModSearch mod)
	{
		Source = mod.Source;
		Id = mod.Id;
		Name = mod.DisplayName;
		ShortDescription = mod.ShortDescription;
		VersionName = mod.UserModVersion;
		LatestVersion = mod.LatestVersion;
		ThumbnailUrl = mod.ThumbnailUrl;
		Author = mod.Author;
		Version = mod.Version;
		Description = mod.ShortDescription;
		ServerTime = mod.LatestModUpdate ?? default;
		ServerSize = (long)mod.Size;
		IsLocal = false;
		IsCollection = false;
		IsCodeMod = mod.Tags.Any(x => x.Id == "Code Mod");
		VoteCount = mod.RatingsTotal;
		IsRemoved = mod.State is ModState.Removed;
		IsInvalid = mod.State is ModState.Unknown;
		IsBanned = mod.State is ModState.Rejected or ModState.AutoBlocked;
		SuggestedGameVersion = mod.RequiredGameVersion;
		Tags = mod.Tags;
		Subscribers = mod.SubscriptionsTotal;
		Playsets = mod.Playsets;
		IsSubscribedInActivePlayset = mod.IsSubscribedInActivePlayset;
		IsEnabledInActivePlayset = mod.IsEnabledInActivePlayset;
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";

		PdxLocalData = mod.LocalData;

		if (mod.LocalData?.FolderAbsolutePath is not null)
		{
			LocalData = new LocalPackageData(this, [], 0, [], mod.LocalData.FolderAbsolutePath, mod.UserModVersion, string.Empty, mod.RequiredGameVersion);

			if (mod.LocalData.ThumbnailFilename is not null)
			{
				ThumbnailPath = CrossIO.Combine(mod.LocalData.FolderAbsolutePath, mod.LocalData.ThumbnailFilename);
			}
			else
			{
				ThumbnailPath = string.Empty;
			}
		}
		else
		{
			ThumbnailPath = string.Empty;
		}
	}

	public string? Version { get; set; }
	public string VersionName { get; set; }
	public string ShortDescription { get; set; }
	public string LatestVersion { get; set; }
	public string? ThumbnailUrl { get; set; }
	public string? Description { get; set; }
	public DateTime ServerTime { get; set; }
	public long ServerSize { get; set; }
	public int Subscribers { get; set; }
	public int VoteCount { get; set; }
	public bool IsRemoved { get; set; }
	public bool IsBanned { get; set; }
	public IEnumerable<IPdxTag> Tags { get; }
	public bool IsCollection { get; set; }
	public bool IsInvalid { get; set; }
	public string Author { get; set; }
	public IEnumerable<IPackageRequirement> Requirements => [];
	IUser? IWorkshopInfo.Author => new PdxUser(Author);
	Dictionary<string, string> IWorkshopInfo.Tags => Tags.ToDictionary(x => x.Id, x => x.DisplayName);
	public bool HasVoted { get; set; }
	public bool IsCodeMod { get; }
	public bool IsLocal { get; }
	public bool IsBuiltIn { get; }
	public ILocalPackageData? LocalData { get; }
	public string ThumbnailPath { get; }
	public SourceType Source { get; }
	public string Id { get; private set; }
	public string Name { get; }
	public string? Url { get; }
	string IModBase.Id { get => Id.ToString(); set => Id = value; }
	public string? SuggestedGameVersion { get; set; }
	bool IWorkshopInfo.IsPartialInfo => true;
	LocalData ILocalModBase.LocalData { get => PdxLocalData; set => PdxLocalData = value; }
	IEnumerable<IModChangelog> IWorkshopInfo.Changelog => [];
	IEnumerable<IThumbnailObject> IWorkshopInfo.Images => [];
	IEnumerable<ILink> IWorkshopInfo.Links => [];
	public List<PlaysetInMod> Playsets { get; set; }
	public bool IsSubscribedInActivePlayset { get; set; }
	public bool IsEnabledInActivePlayset { get; set; }
	string IPackageIdentity.Source => Source;

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = ThumbnailUrl;
		thumbnail = DomainUtils.GetThumbnail(imageService, ThumbnailPath, ThumbnailUrl, Id.ToString(), Version ?? string.Empty);

		return true;
	}

	bool IWorkshopInfo.HasComments()
	{
		return false;
	}

	public bool GetFullThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = ThumbnailUrl;
		thumbnail = DomainUtils.GetThumbnail(imageService, ThumbnailPath, ThumbnailUrl, Id.ToString(), Version ?? string.Empty, false);

		return true;
	}

	public override bool Equals(object? obj)
	{
		return obj is IPackageIdentity identity &&
			Source == identity.Source &&
			Id == identity.Id &&
			Version == identity.Version;
	}

	public override int GetHashCode()
	{
		var hashCode = -781363793;
		hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Source);
		hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
		hashCode = hashCode * -1521134295 + EqualityComparer<string?>.Default.GetHashCode(Version);
		return hashCode;
	}

}