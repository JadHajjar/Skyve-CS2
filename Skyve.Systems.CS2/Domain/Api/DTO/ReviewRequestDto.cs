using Skyve.Domain;

using SkyveApi.Domain.CS2;

namespace Skyve.Systems.CS2.Domain.Api.DTO;
internal class ReviewRequestDto : IDTO<ReviewRequestData, ReviewRequest>, IDTO<ReviewRequest, ReviewRequestData>
{
	public ReviewRequestData Convert(ReviewRequest? data)
	{
		if (data is null)
		{
			return new();
		}

		return new ReviewRequestData
		{
			IsInteraction = data.IsInteraction,
			IsStatus = data.IsStatus,
			LogFile = data.LogFile,
			PackageId = data.PackageId,
			PackageNote = data.PackageNote,
			PackageStability = data.PackageStability,
			PackageType = data.PackageType,
			PackageUsage = data.PackageUsage,
			RequiredDLCs = data.RequiredDLCs,
			StatusAction = data.StatusAction,
			StatusNote = data.StatusNote,
			StatusType = data.StatusType,
			StatusPackages = data.StatusPackages,
			Timestamp = data.Timestamp,
			UserId = data.UserId,
		};
	}

	public ReviewRequest Convert(ReviewRequestData? data)
	{
		if (data is null)
		{
			return new();
		}

		return new ReviewRequest
		{
			IsInteraction = data.IsInteraction,
			IsStatus = data.IsStatus,
			LogFile = data.LogFile,
			PackageId = data.PackageId,
			PackageNote = data.PackageNote,
			PackageStability = data.PackageStability,
			PackageType = data.PackageType,
			PackageUsage = data.PackageUsage,
			RequiredDLCs = data.RequiredDLCs,
			StatusAction = data.StatusAction,
			StatusNote = data.StatusNote,
			StatusType = data.StatusType,
			StatusPackages = data.StatusPackages,
			Timestamp = data.Timestamp,
			UserId = data.UserId,
		};
	}
}
