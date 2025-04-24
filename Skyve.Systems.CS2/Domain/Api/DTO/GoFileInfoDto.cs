using Skyve.Domain;

using SkyveApi.Domain.Generic;

namespace Skyve.Systems.CS2.Domain.Api.DTO;

internal class GoFileInfoDto : IDTO<GoFileUploadData, GoFileUploadInfo>
{
	public GoFileUploadInfo Convert(GoFileUploadData? data)
	{
		if (data == null)
		{
			return new();
		}

		return new GoFileUploadInfo
		{
			Token = data.Token,
			FolderId = data.FolderId,
			ServerId = data.ServerId,
		};
	}
}