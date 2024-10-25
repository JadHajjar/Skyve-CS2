using System;

namespace Skyve.Domain.CS2.Enums;

[Flags]
public enum BackupCleanupType
{
	None = 0,
	CountBased = 1,
	TimeBased = 2,
	StorageBased = 4
}
