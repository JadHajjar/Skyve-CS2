using Extensions;

using Skyve.Domain.CS2.Enums;

using System;

namespace Skyve.Domain.CS2.Utilities;
[SaveName(nameof(BackupSettings) + ".json")]
public class BackupSettings : ConfigFile, IBackupSettings
{
	public BackupSchedule Schedule { get; set; }
	public TimeSpan[] ScheduleTimes { get; set; } = [];
	public Cleanup CleanupSettings { get; set; } = new();

	public class Cleanup
	{
		public CleanupType Type { get; set; }
		public int MaxBackups { get; set; }
		public TimeSpan MaxTimespan { get; set; }
		public ulong MaxStorage { get; set; }
	}
}
