using Skyve.Domain;
using Skyve.Domain.Systems;

using System.Collections.Generic;

namespace Skyve.Systems.CS2.Systems;
internal class VersionUpdateService : IVersionUpdateService
{
	private readonly ISettings _settings;

	public VersionUpdateService(ISettings settings)
	{
		_settings = settings;
	}

	public void Run(List<IPackage> content)
	{
		//if (_settings.SessionSettings.LastVersioningNumber < 1)
	}
}
