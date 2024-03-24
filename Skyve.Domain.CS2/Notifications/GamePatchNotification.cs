using Extensions;
using Skyve.Domain.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Notifications;
public class GamePatchNotification(DateTime time, string version) : INotificationInfo
{
	public DateTime Time { get; } = time;
	public string Title => LocaleCS2.NewGameUpdate;
	public string? Description { get; } = LocaleCS2.NewGameUpdateInfo.Format(version);
	public string Icon { get; } = "CS";
	public Color? Color => System.Drawing.Color.FromArgb(7, 138, 247);
	public bool HasAction { get; }

	public void OnClick()
	{
	}

	public void OnRightClick()
	{
	}
}
