using System;

namespace Skyve.Domain.CS2.Enums;

[Flags]
public enum CleanupType
{
	None,
	CountBased,
	TimeBased,
	StorageBased
}
