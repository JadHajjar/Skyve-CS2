﻿using Extensions;

using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Notifications;
public class ParadoxLoginRequiredNotification : INotificationInfo
{
	private readonly IInterfaceService _interfaceService;

	public ParadoxLoginRequiredNotification(bool badLogin, IInterfaceService interfaceService)
	{
		_interfaceService = interfaceService;
		Time = DateTime.Now;
		Title = LocaleCS2.ParadoxLoginFailedTitle;
		Description = badLogin ? LocaleCS2.ParadoxLoginFailedBadCredentials : LocaleCS2.ParadoxLoginFailedEmpty;
		Icon = "Paradox";
		HasAction = true;
	}

	public DateTime Time { get; }
	public string Title { get; }
	public string? Description { get; }
	public string Icon { get; }
	public Color? Color => FormDesign.Design.RedColor;
	public bool HasAction { get; }
	public bool CanBeRead { get; }

	public void OnClick()
	{
		_interfaceService.OpenParadoxLogin();
	}

	public void OnRead()
	{
		throw new NotImplementedException();
	}

	public void OnRightClick()
	{
	}
}
