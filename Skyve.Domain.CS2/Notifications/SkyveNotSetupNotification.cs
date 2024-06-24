using Extensions;

using Skyve.Domain.CS2.Utilities;

using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Notifications;

public class SkyveNotSetupNotification : INotificationInfo
{
	public SkyveNotSetupNotification()
	{
		Time = DateTime.Now;
		Title = LocaleCS2.InvalidFolderSettings;
		Description = LocaleCS2.SkyvenotSetUpInfo;
		Icon = "Hazard";
	}

	public DateTime Time { get; }
	public string Title { get; }
	public string? Description { get; }
	public string Icon { get; }
	public Color? Color => FormDesign.Design.RedColor;
	public bool HasAction { get; }

	public void OnClick()
	{
	}

	public void OnRightClick()
	{
	}
}
