﻿using Extensions;

using Skyve.Domain.CS2.Utilities;

using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Notifications;

public class ParadoxLoginWaitingConnectionNotification : INotificationInfo
{
	public ParadoxLoginWaitingConnectionNotification()
	{
		Time = DateTime.MaxValue;
		Title = LocaleCS2.ParadoxLoginFailedNoConnectionTitle;
		Description = LocaleCS2.ParadoxLoginFailedNoConnection;
		Icon = "I_Paradox";
	}

	public DateTime Time { get; }
	public string Title { get; }
	public string? Description { get; }
	public string Icon { get; }
	public Color? Color => FormDesign.Design.YellowColor;
	public bool HasAction { get; }

	public void OnClick()
	{
		throw new NotImplementedException();
	}

	public void OnRightClick()
	{
		throw new NotImplementedException();
	}
}
