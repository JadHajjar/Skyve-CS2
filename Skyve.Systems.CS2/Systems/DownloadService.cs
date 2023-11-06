using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using System.Collections.Generic;

namespace Skyve.Systems.CS2.Systems;
internal class DownloadService : IDownloadService
{
	public void Download(IEnumerable<IPackageIdentity> packageIds)
	{
		SteamUtil.Download(packageIds);
	}
}
