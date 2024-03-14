using Extensions;

using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Notifications;

public class ParadoxLoginWaitingConnectionNotification : INotificationInfo
{
	private readonly IWorkshopService _workshopService;

	public ParadoxLoginWaitingConnectionNotification(IWorkshopService workshopService)
	{
		Time = DateTime.MaxValue;
		Title = LocaleCS2.ParadoxLoginFailedTitle;
		Description = LocaleCS2.ParadoxLoginFailedNoConnection;
		Icon = "Paradox";
		_workshopService = workshopService;
	}

	public DateTime Time { get; }
	public string Title { get; }
	public string? Description { get; }
	public string Icon { get; }
	public Color? Color => FormDesign.Design.YellowColor;
	public bool HasAction { get; }

	public void OnClick()
	{
		_workshopService.Login();
	}

	public void OnRightClick()
	{

	}
}
