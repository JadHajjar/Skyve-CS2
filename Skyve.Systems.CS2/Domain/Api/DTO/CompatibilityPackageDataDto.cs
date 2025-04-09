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
			Stability = (PackageStability)data.Stability,
			Type = (PackageType)data.Type,
			Usage = (PackageUsage)data.Usage,
			RequiredDLCs = data.RequiredDLCs ?? [],
			Interactions = data.Interactions?.ToList(Convert) ?? [],
			Links = data.Links?.ToList(Convert) ?? [],
			Statuses = data.Statuses?.ToList(Convert) ?? [],
		};
	}

	private PackageInteraction Convert(PackageInteractionData data)
	{
		return new PackageInteraction
		{
			Type = (InteractionType)data.Type,
			Action = (StatusAction)data.Action,
			Packages = data.Packages?.ToList(x => new CompatibilityPackageReference(new GenericPackageIdentity(x))),
			Note = data.Note
		};
	}

	private PackageStatus Convert(PackageStatusData data)
	{
		return new PackageStatus
		{
			Type = (StatusType)data.Type,
			Action = (StatusAction)data.Action,
			Packages = data.Packages?.ToList(x => new CompatibilityPackageReference(new GenericPackageIdentity(x))),
			Note = data.Note
		};
	}

	private PackageLink Convert(PackageLinkData data)
	{
		return new PackageLink
		{
			Type = (LinkType)data.Type,
			Title = data.Title,
			Url = data.Url,
		};
	}
}
