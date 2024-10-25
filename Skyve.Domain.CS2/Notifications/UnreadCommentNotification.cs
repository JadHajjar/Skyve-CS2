using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;
using Skyve.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Notifications;
public class UnreadCommentNotification : INotificationInfo
{
	private readonly IPackageIdentity _package;
	private readonly IModComment _comment;
	private readonly IUpdateManager _updateManager;
	private readonly IInterfaceService _interfaceService;
	private readonly INotificationsService _notificationsService;

	public UnreadCommentNotification(IPackageIdentity package, IModComment comment, IUpdateManager updateManager, IInterfaceService interfaceService, INotificationsService notificationsService)
	{
		_package = package;
		_comment = comment;
		_updateManager = updateManager;
		_interfaceService = interfaceService;
		_notificationsService = notificationsService;

		PackageId = package.Id;
		Time = comment.Created.ToLocalTime();
		Title = string.Empty;
		Description= LocaleCS2.UnreadComment.Format(package.CleanName());
		Icon = "Chat";
		CanBeRead = HasAction = true;
	}

	public DateTime Time { get; }
	public string Title { get; }
	public string? Description { get; }
	public string Icon { get; }
	public Color? Color { get; }
	public bool HasAction { get; }
	public bool CanBeRead { get; }
	public ulong PackageId { get; }

	public void OnClick()
	{
		_notificationsService.RemoveNotification(this);
		_interfaceService.OpenPackagePage(_package, openCommentsPage: true);
	}

	public void OnRead()
	{
		_updateManager.MarkCommentAsRead(_package);
	}

	public void OnRightClick()
	{
	}
}
