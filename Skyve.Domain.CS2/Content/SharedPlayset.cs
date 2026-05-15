using Extensions;

using Skyve.Domain.CS2.Paradox;
using Skyve.Systems;

using System;
using System.Linq;

namespace Skyve.Domain.CS2.Content;

public class SharedPlayset : IOnlinePlayset
{
	public string Author { get; set; }
	public string? Version { get; set; }
	public string? PreferredVersion { get; set; }
	public string LatestPublicVersion { get; set; }
	public int SubscriptionsTotal { get; set; }
	public int Rating { get; set; }
	public int RatingsTotal { get; set; }
	public IPlaysetVersion[]? PublicVersions { get; set; }
	public IPlaysetVersion? LatestPublicVersionData { get; set; }
	IUser IOnlinePlayset.Author => new PdxUser(Author);

	public SharedPlayset(PDX.SDK.Contracts.Service.Mods.Models.Playset playset)
	{
		Author = playset.Author;
		Version = playset.Version;
		PreferredVersion = playset.PreferredVersion;
		LatestPublicVersion = playset.LatestPublicVersion;
		SubscriptionsTotal = playset.SubscriptionsTotal;
		Rating = playset.Rating;
		RatingsTotal = playset.RatingsTotal;
		PublicVersions = playset.PublicVersions?.ToArray(x => new SharedPlaysetVersion(x, null));
		LatestPublicVersionData = playset.LatestPublicVersionData is null ? null : new SharedPlaysetVersion(playset.LatestPublicVersionData, playset.LatestPublicVersionData.Mods?.ToArray(x => new GenericPackageIdentity(x.Source, x.Id, version: x.Version)));
	}
}

public class SharedPlaysetVersion : IPlaysetVersion
{
	public string Version { get; set; }
	public string DisplayName { get; set; }
	public DateTime? Created { get; set; }
	public int ModsCount { get; set; }
	public IPackageIdentity[]? Mods { get; set; }

	public SharedPlaysetVersion(PDX.SDK.Contracts.Service.Mods.Models.PlaysetVersion playsetVersion, IPackageIdentity[]? mods)
	{
		Version = playsetVersion.Version;
		DisplayName = playsetVersion.DisplayName;
		ModsCount = playsetVersion.ModsCount;
		Created = playsetVersion.Created;
		Mods = mods;
	}
}
