using System;

namespace Skyve.Domain.CS2.Enums;

[Flags]
public enum BackupSchedule
{
	None,
	OnScheduledTimes,
	OnGameClose,
	OnNewSaveGame
}
