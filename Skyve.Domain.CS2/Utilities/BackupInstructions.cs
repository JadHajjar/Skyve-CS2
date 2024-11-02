namespace Skyve.Domain.CS2.Utilities;

public class BackupInstructions : IBackupInstructions
{
	public bool DoSavesBackup { get; set; } = true;
	public bool DoLocalModsBackup { get; set; } = true;
	public bool DoMapsBackup { get; set; } = true;
}
