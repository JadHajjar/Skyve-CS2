using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System.IO;
using System.Runtime.InteropServices;

namespace Skyve.Systems.CS2.Services;
internal class SettingsService : ISettings
{
	public SessionSettings SessionSettings { get; private set; }
	public FolderSettings FolderSettings { get; private set; }
	public UserSettings UserSettings { get; private set; }
	public BackupSettings BackupSettings { get; private set; }

	IUserSettings ISettings.UserSettings => UserSettings;
	ISessionSettings ISettings.SessionSettings => SessionSettings;
	IFolderSettings ISettings.FolderSettings => FolderSettings;
	IBackupSettings ISettings.BackupSettings => BackupSettings;

	public SettingsService(SaveHandler saveHandler)
	{
		var settingsSaveHandler = new SaveHandler(CrossIO.Combine(Path.GetDirectoryName(saveHandler.SaveDirectory), "ModsSettings"));

		//var isRunning = SteamAPI_IsSteamRunning();
		//var init = SteamAPI_Init();
		//var userId = GetSteamUserId();
		//SteamAPI_Shutdown();

		FolderSettings = settingsSaveHandler.Load<FolderSettings>();
		SessionSettings = settingsSaveHandler.Load<SessionSettings>();
		UserSettings = settingsSaveHandler.Load<UserSettings>();
		BackupSettings = settingsSaveHandler.Load<BackupSettings>();

		CrossIO.CurrentPlatform = FolderSettings.Platform;

		FolderSettings.GamePath = FolderSettings.GamePath?.FormatPath() ?? string.Empty;
		FolderSettings.AppDataPath = FolderSettings.AppDataPath?.FormatPath() ?? string.Empty;
		FolderSettings.SteamPath = FolderSettings.SteamPath?.FormatPath() ?? string.Empty;
	}

	public void ResetFolderSettings()
	{
		FolderSettings.Reset();
	}

	public void ResetUserSettings()
	{
		SessionSettings.Reset();
	}
}
