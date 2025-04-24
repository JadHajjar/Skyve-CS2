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
			IsMissingInfo = data.IsMissingInfo,
			LogFile = data.LogFile,
			PackageId = data.PackageId,
			PackageNote = data.PackageNote,
			PackageStability = data.PackageStability,
			PackageType = data.PackageType,
			PackageUsage = data.PackageUsage,
			RequiredDLCs = data.RequiredDLCs,
			SavegameEffect = data.SavegameEffect,
			Timestamp = data.Timestamp,
			UserId = data.UserId,
			SaveUrl = data.SaveUrl
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
			IsMissingInfo = data.IsMissingInfo,
			LogFile = data.LogFile,
			PackageId = data.PackageId,
			PackageNote = data.PackageNote,
			PackageStability = data.PackageStability,
			PackageType = data.PackageType,
			PackageUsage = data.PackageUsage,
			RequiredDLCs = data.RequiredDLCs,
			SavegameEffect = data.SavegameEffect,
			Timestamp = data.Timestamp,
			UserId = data.UserId,
			SaveUrl = data.SaveUrl
		};
	}
}
