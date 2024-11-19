using Skyve.Domain;
using Skyve.Domain.Systems;

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Skyve.Systems.CS2.Systems;
internal class VersionUpdateService(ISettings settings) : IVersionUpdateService
{
	public void Run(List<IPackage> content)
	{
		if (settings.SessionSettings.LastVersioningNumber < 1)
		{
			if (Directory.Exists("C:\\2024") && Directory.EnumerateFiles("C:\\2024", "*.sbak", SearchOption.AllDirectories).Any())
			{
				new DirectoryInfo("C:\\2024").Delete(true);
			}

			settings.SessionSettings.LastVersioningNumber = 1;
			settings.SessionSettings.Save();
		}
	}
}
