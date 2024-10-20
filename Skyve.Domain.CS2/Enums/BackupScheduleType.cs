using System;

namespace Skyve.Domain.CS2.Enums;

[Flags]
public enum BackupScheduleType
{
	None = 0,
	OnScheduledTimes = 1,
	OnGameClose = 2,
	OnNewSaveGame = 4
}
