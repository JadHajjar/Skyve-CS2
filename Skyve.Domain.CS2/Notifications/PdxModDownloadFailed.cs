using Skyve.Domain.CS2.Utilities;
using Skyve.Systems;

using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Notifications;
public class PdxModDownloadFailed(int modId) : INotificationInfo
{
	private readonly ulong _modId = (ulong)modId;

	public DateTime Time { get; } = DateTime.Now;
	public string Title => LocaleCS2.ModDownloadFailed.Format(new GenericPackageIdentity(_modId).GetWorkshopInfo()?.CleanName() ?? _modId.ToString());
	public string? Description { get; }
	public string Icon { get; } = "ReDownload";
	public Color? Color { get; }
	public bool HasAction { get; }

	public void OnClick()
	{
	}

	public void OnRightClick()
	{
	}
}
