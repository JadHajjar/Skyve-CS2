using Extensions;

using Skyve.Domain.CS2.Enums;

using System;

namespace Skyve.Domain.CS2.Utilities;
[SaveName(nameof(BackupSettings) + ".json")]
public class BackupSettings : ConfigFile, IBackupSettings
{
	public string? DestinationFolder { get; set; } = "D:\\back";
	public BackupSchedule Schedule { get; set; }
	public TimeSpan[] ScheduleTimes { get; set; } = [];
	public Cleanup CleanupSettings { get; set; } = new();
	public bool IgnoreAutoSaves { get; set; }
	public bool BackupSavesOnSchedule { get; set; }
	public bool BackupLocalModsOnSchedule { get; set; }

	public class Cleanup
	{
		public CleanupType Type { get; set; }
		public int MaxBackups { get; set; }
		public TimeSpan MaxTimespan { get; set; }
		public ulong MaxStorage { get; set; }
	}
}
