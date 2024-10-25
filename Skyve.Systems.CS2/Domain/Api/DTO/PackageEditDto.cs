using Skyve.Domain;

using SkyveApi.Domain.CS2;

namespace Skyve.Systems.CS2.Domain.Api.DTO;

internal class PackageEditDto : IDTO<PackageEditData, PackageEdit>
{
	public PackageEdit Convert(PackageEditData? data)
	{
		if (data == null)
		{
			return new PackageEdit(string.Empty, default, string.Empty);
		}

		return new PackageEdit(data.Username!, data.EditDate.ToLocalTime(), data.Note!);
	}
}
