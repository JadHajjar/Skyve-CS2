using Extensions;

using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Notifications;
public class ParadoxLoginRequiredNotification : INotificationInfo
{
	public ParadoxLoginRequiredNotification(bool badLogin)
	{
		Time = DateTime.MaxValue;
		Title = LocaleCS2.ParadoxLoginFailedTitle;
		Description = badLogin ? LocaleCS2.ParadoxLoginFailedBadCredentials : LocaleCS2.ParadoxLoginFailedEmpty;
		Icon = "I_Paradox";
		HasAction = true;
	}

	public DateTime Time { get; }
	public string Title { get; }
	public string? Description { get; }
	public string Icon { get; }
	public Color? Color => FormDesign.Design.RedColor;
	public bool HasAction { get; }

	public void OnClick()
	{
		ServiceCenter.Get<IInterfaceService>().OpenParadoxLogin();
	}

	public void OnRightClick()
	{
		throw new NotImplementedException();
	}
}
