using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Utilities;
internal abstract class BackupItem : IBackupItem
{
	internal class Save : BackupItem
	{

	}
	internal class Settings : BackupItem
	{

	}
	internal class Playset : BackupItem
	{

	}
	internal class LocalMod : BackupItem
	{

	}
}
