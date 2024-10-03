namespace Skyve.Domain.CS2.Utilities;

public class BackupInstructions : IBackupInstructions
{
	public bool DoSavesBackup { get; set; }
	public bool DoLocalModsBackup { get; set; }
}
