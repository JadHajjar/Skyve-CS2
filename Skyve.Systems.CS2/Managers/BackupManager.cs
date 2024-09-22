using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class BackupManager
{
	private readonly ISettings _settings;

	public BackupManager(ISettings settings)
    {
		_settings = settings;


	}

	//public async Task<bool> DoBackup()
	//{
	//	r
	//}
}
