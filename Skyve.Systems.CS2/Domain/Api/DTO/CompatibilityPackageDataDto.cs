using Extensions;

using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Domain;
using Skyve.Domain.Enums;
using Skyve.Systems.CS2.Domain.Api;

using SkyveApi.Domain.CS2;

using System.Linq;

namespace Skyve.Systems.CS2.Domain.DTO;
internal class PackageDataDto : IDTO<CompatibilityPackageData, PackageData>
{
	public PackageData Convert(CompatibilityPackageData? data)
	{
		if (data is null)
		{
			return new();
		}

		return new PackageData
		{
			Id = data.Id,
			Name = data.Name,
			AuthorId = data.AuthorId,
			FileName = data.FileName,
			Note = data.Note,
			ReviewDate = data.ReviewDate,
			ReviewedGameVersion = data.ReviewedGameVersion,
			Tags = data.Tags ?? [],
			Stability = data.Stability.TryCast<PackageStability>(),
			Type = data.Type.TryCast<PackageType>(),
			SavegameEffect = data.SavegameEffect.TryCast<SavegameEffect>(),
			Usage = data.Usage.TryCast<PackageUsage>(),
			RemovalSteps = data.RemovalSteps,
			RequiredDLCs = data.RequiredDLCs ?? [],
			Interactions = data.Interactions?.ToList(Convert) ?? [],
			Links = data.Links?.ToList(Convert) ?? [],
			Statuses = data.Statuses?.ToList(Convert) ?? [],
			ActiveReports = data.ActiveReports,
			ThumbnailUrl = data.ThumbnailUrl,
		};
	}

	private PackageInteraction Convert(PackageInteractionData data)
	{
		return new PackageInteraction
		{
			Type = data.Type.TryCast<InteractionType>(),
			Action = data.Action.TryCast<StatusAction>(),
			Packages = data.Packages?.ToList(x => new CompatibilityPackageReference(new GenericPackageIdentity(x))),
			Note = data.Note
		};
	}

	private PackageStatus Convert(PackageStatusData data)
	{
		return new PackageStatus
		{
			Type = data.Type.TryCast<StatusType>(),
			Action = data.Action.TryCast<StatusAction>(),
			Packages = data.Packages?.ToList(x => new CompatibilityPackageReference(new GenericPackageIdentity(x))),
			Note = data.Note
		};
	}

	private PackageLink Convert(PackageLinkData data)
	{
		return new PackageLink
		{
			Type = data.Type.TryCast<LinkType>(),
			Title = data.Title,
			Url = data.Url,
		};
	}
}
