using Extensions;

using Skyve.Compatibility.Domain;
using Skyve.Domain;

using SkyveApi.Domain.CS2;

using System.Linq;

namespace Skyve.Systems.CS2.Domain.Api.DTO;
internal class PostPackageDto : IDTO<CompatibilityPostPackage, PostPackage>
{
	public PostPackage Convert(CompatibilityPostPackage? data)
	{
		if (data is null)
		{
			return new();
		}

		return new PostPackage
		{
			AuthorId = data.AuthorId,
			BlackListId = data.IsBlackListedById,
			BlackListName = data.IsBlackListedByName,
			FileName = data.FileName,
			Id = data.Id,
			Name = data.Name,
			Note = data.Note,
			EditNote = data.EditNote,
			RequiredDLCs = data.RequiredDLCs,
			ReviewDate = data.ReviewDate,
			ReviewedGameVersion = data.ReviewedGameVersion,
			Stability = (int)data.Stability,
			Usage = (int)data.Usage,
			Type = (int)data.Type,
			SavegameEffect = (int)data.SavegameEffect,
			RemovalSteps = data.RemovalSteps,
			Tags = data.Tags,
			ActiveReports = data.ActiveReports,
			Links = data.Links.ToList(Convert),
			Statuses = data.Statuses.ToList(Convert),
			Interactions = data.Interactions.ToList(Convert),
		};
	}

	private PackageInteractionData Convert(PackageInteraction data)
	{
		return new PackageInteractionData
		{
			Type = (int)data.Type,
			Action = (int)data.Action,
			Packages = data.Packages?.Select(x => x.Id).ToArray(),
			Note = data.Note
		};
	}

	private PackageStatusData Convert(PackageStatus data)
	{
		return new PackageStatusData
		{
			Type = (int)data.Type,
			Action = (int)data.Action,
			Packages = data.Packages?.Select(x => x.Id).ToArray(),
			Note = data.Note
		};
	}

	private PackageLinkData Convert(PackageLink data)
	{
		return new PackageLinkData
		{
			Type = (int)data.Type,
			Title = data.Title,
			Url = data.Url,
		};
	}
}
