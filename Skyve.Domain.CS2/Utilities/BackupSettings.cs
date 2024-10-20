using Extensions;

using Skyve.Domain.CS2.Enums;

using System;

namespace Skyve.Domain.CS2.Utilities;
[SaveName(nameof(BackupSettings) + ".json")]
public class BackupSettings : ConfigFile, IBackupSettings
{
	public string? DestinationFolder { get; set; }
	public Schedule ScheduleSettings { get; set; } = new();
	public Cleanup CleanupSettings { get; set; } = new();
	public bool IgnoreAutoSaves { get; set; } = true;

	public class Schedule
	{
		public BackupScheduleType Type { get; set; } = BackupScheduleType.OnScheduledTimes;
		public TimeSpan[] ScheduleTimes { get; set; } = [TimeSpan.FromHours(18)];
		public bool BackupSaves { get; set; } = true;
		public bool BackupLocalMods { get; set; } = true;
	}

	public class Cleanup
	{
		public BackupCleanupType Type { get; set; } = BackupCleanupType.TimeBased | BackupCleanupType.StorageBased;
		public TimeSpan MaxTimespan { get; set; } = TimeSpan.FromDays(30 * 2);
		public ulong MaxStorage { get; set; } = 100 * 1024UL * 1024UL * 1024UL;
		public int MaxBackups { get; set; }
	}
}
