using Skyve.Domain.Enums;

using System;

namespace Skyve.Domain.CS2.Utilities;
public class BackupMetaData : IBackupMetaData
{
	public bool IsArchived { get; set; }
	public string? Name { get; set; }
	public DateTime BackupTime { get; set; }
	public DateTime ContentTime { get; set; }
	public int FileCount { get; set; }
	public string? Root { get; set; }
	public string? Type { get; set; }
	public RestoreAction RestoreType { get; set; }
	public string? ItemMetaDataType { get; set; }
}
