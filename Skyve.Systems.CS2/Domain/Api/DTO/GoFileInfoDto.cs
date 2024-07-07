using Skyve.Domain;

using SkyveApi.Domain.Generic;

namespace Skyve.Systems.CS2.Domain.Api.DTO;

internal class GoFileInfoDto : IDTO<GoFileInfoData, GoFileInfo>
{
	public GoFileInfo Convert(GoFileInfoData? data)
	{
		if (data == null)
		{
			return new();
		}

		return new GoFileInfo
		{
			RootFolder = data.RootFolder,
			Token = data.Token,
		};
	}
}