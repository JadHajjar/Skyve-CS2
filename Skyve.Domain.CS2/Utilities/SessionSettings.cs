using Extensions;

using System.Drawing;

namespace Skyve.Domain.CS2.Utilities;
[SaveName(nameof(SessionSettings) + ".json")]
public class SessionSettings : ConfigFile, ISessionSettings
{
	public bool FirstTimeSetupCompleted { get; set; }
	public Rectangle? LastWindowsBounds { get; set; }
	public bool WindowWasMaximized { get; set; }
	public bool SubscribeFirstTimeShown { get; set; }
	public bool CleanupFirstTimeShown { get; set; }
	public bool FpsBoosterLogWarning { get; set; }
	public string? LastVersionNotification { get; set; }
	public int LastVersioningNumber { get; set; }
	public bool DashboardFirstTimeShown { get; set; }

	public void Save()
	{
		Handler?.Save(this);
	}
}
