using Skyve.Domain;

using SkyveApi.Domain.CS2;

namespace Skyve.Systems.CS2.Domain.Api.DTO;

internal class ReviewReplyDto : IDTO<ReviewReplyData, ReviewReply>
{
	public ReviewReply Convert(ReviewReplyData? data)
	{
		if (data == null)
		{
			return new ReviewReply();
		}

		return new ReviewReply
		{
			Username = data.Username,
			Link = data.Link,
			Message = data.Message,
			Timestamp = data.Timestamp,
			PackageId = data.PackageId,
			RequestUpdate = data.RequestUpdate,
		};
	}
}

internal class ReviewReplyDataDto : IDTO<ReviewReply, ReviewReplyData>
{
	public ReviewReplyData Convert(ReviewReply? data)
	{
		if (data == null)
		{
			return new ReviewReplyData();
		}

		return new ReviewReplyData
		{
			Username = data.Username,
			Link = data.Link,
			Message = data.Message,
			Timestamp = data.Timestamp,
			PackageId = data.PackageId,
			RequestUpdate = data.RequestUpdate,
		};
	}
}
