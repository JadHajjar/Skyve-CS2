using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Paradox;
public class SyncConflictInfo : ISyncConflictInfo
{
	public string? LocalPlaysetName { get; }
	public string? OnlinePlaysetName { get; }

	public SyncConflictInfo(PDX.SDK.Contracts.Service.Mods.Result.PlaysetSyncConflict conflict)
	{
		LocalPlaysetName = conflict.LocalPlayset?.Name;
		OnlinePlaysetName = conflict.OnlinePlayset?.Name;
	}
}
