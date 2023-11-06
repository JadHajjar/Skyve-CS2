using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

namespace Skyve.Systems.CS2.Systems;
internal class SettingsService : ISettings
{
	public SessionSettings SessionSettings { get; private set; }
	public FolderSettings FolderSettings { get; private set; }
	public UserSettings UserSettings { get; private set; }

	IUserSettings ISettings.UserSettings => UserSettings;
	ISessionSettings ISettings.SessionSettings => SessionSettings;
	IFolderSettings ISettings.FolderSettings => FolderSettings;

	public SettingsService()
	{
		FolderSettings = FolderSettings.Load();
		SessionSettings = SessionSettings.Load();
		UserSettings = UserSettings.Load();

		CrossIO.CurrentPlatform = FolderSettings.Platform;
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
