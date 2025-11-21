using Skyve.Domain;

using SkyveApi.Domain.CS2;

namespace Skyve.Systems.CS2.Domain.Api.DTO;

internal class BulkUpdateDto : IDTO<BulkCompatibilityPackageUpdate, BulkCompatibilityPackageUpdateData>
{
	public BulkCompatibilityPackageUpdateData Convert(BulkCompatibilityPackageUpdate? data)
	{
		if (data is null)
		{
			return new();
		}

		return new BulkCompatibilityPackageUpdateData
		{
			Packages = data.Packages ?? [],
			ReviewedGameVersion = data.ReviewedGameVersion,
			Stability = (int)data.Stability,
		};
	}
}