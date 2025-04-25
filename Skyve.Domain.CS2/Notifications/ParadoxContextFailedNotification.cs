using Extensions;

using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Notifications;

public class ParadoxContextFailedNotification : INotificationInfo
{
	private readonly IWorkshopService _workshopService;

	public ParadoxContextFailedNotification(IWorkshopService workshopService)
	{
		_workshopService = workshopService;
		Time = DateTime.Now;
		Title = LocaleCS2.ParadoxContextFailed;
		Description = LocaleCS2.ParadoxContextFailedInfo;
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
		Task.Run(_workshopService.RepairContext);
	}

	public void OnRead()
	{
	}

	public void OnRightClick()
	{

	}
}
