using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System.IO;

namespace Skyve.Systems.CS2.Services;
internal class SettingsService : ISettings
{
	public SessionSettings SessionSettings { get; private set; }
	public FolderSettings FolderSettings { get; private set; }
	public UserSettings UserSettings { get; private set; }

	IUserSettings ISettings.UserSettings => UserSettings;
	ISessionSettings ISettings.SessionSettings => SessionSettings;
	IFolderSettings ISettings.FolderSettings => FolderSettings;

	public SettingsService(SaveHandler saveHandler)
	{
		var settingsSaveHandler = new SaveHandler(CrossIO.Combine(Path.GetDirectoryName(saveHandler.SaveDirectory), "ModsSettings"));

		FolderSettings = settingsSaveHandler.Load<FolderSettings>();
		SessionSettings = settingsSaveHandler.Load<SessionSettings>();
		UserSettings = settingsSaveHandler.Load<UserSettings>();

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
