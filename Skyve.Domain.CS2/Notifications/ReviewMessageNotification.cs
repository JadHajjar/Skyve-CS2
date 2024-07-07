using Skyve.Domain.Systems;
using Skyve.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Notifications;
public class ReviewMessageNotification(ReviewReply reply, IInterfaceService interfaceService) : INotificationInfo
{
	public DateTime Time { get; } = reply.Timestamp.ToLocalTime();
	public string Title => Locale.ReviewRequestNotification;
	public string? Description => Locale.ReviewRequestNotificationInfo.Format(new GenericPackageIdentity(reply.PackageId).CleanName());
	public string Icon { get; } = "RequestReview";
	public Color? Color { get; }
	public bool HasAction { get; } = true;
	public bool CanBeRead { get; }

	public void OnClick()
	{
		interfaceService.OpenPackagePage(new GenericPackageIdentity(reply.PackageId), openCompatibilityPage: true);
	}

	public void OnRead()
	{
	}

	public void OnRightClick()
	{
	}
}
