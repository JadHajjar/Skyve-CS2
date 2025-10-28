using Extensions;

using Microsoft.Extensions.DependencyInjection;

using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Events.Mods;

using Skyve.Domain;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class WorkshopEventsManager
{
	private readonly WorkshopService _workshopService;
	private readonly INotificationsService _notificationsService;
	private readonly ISubscriptionsManager _subscriptionsManager;

	internal WorkshopEventsManager(WorkshopService workshopService, IServiceProvider serviceProvider)
	{
		_workshopService = workshopService;

		_notificationsService = serviceProvider.GetService<INotificationsService>()!;
		_subscriptionsManager = serviceProvider.GetService<ISubscriptionsManager>()!;
	}

	internal void RegisterModsCallbacks(IContext context)
	{
		context.Mods.Downloads.DownloadStatusChanged += OnDownloadStatusChanged;
		context.Mods.Downloads.DownloadProgressChanged += DownloadProgressChanged;
	}

	private void DownloadProgressChanged(Guid guid, IDownloadStatus payload)
	{
		_subscriptionsManager.OnDownloadProgress(new PackageDownloadProgress
		{
			Status = payload.Status.ToString(),
			Id = ulong.Parse(payload.Id),
			Progress = payload.TotalProgress,
			ProcessedBytes = payload.DownloadedBytes,
			Size = payload.TotalBytesToDownload
		});
	}

	private void OnDownloadStatusChanged(Guid guid, IDownloadStatus payload)
	{
		if (payload.Status == PDX.SDK.Contracts.Enums.ModDownloadStatus.Failed)
		{
			SendDownloadFailedNotification(payload);
		}

		if (payload.Status == PDX.SDK.Contracts.Enums.ModDownloadStatus.Started)
		{
			Task.Run(() => _workshopService.GetInfoAsync(new GenericPackageIdentity(ulong.Parse(payload.Id))));
		}

		DownloadProgressChanged(guid, payload);
	}

	private void SendDownloadFailedNotification(IDownloadStatus payload)
	{
		var notification = _notificationsService.GetNotifications<PdxModDownloadFailed>().FirstOrDefault() ?? new PdxModDownloadFailed();

		notification.Time = DateTime.Now;
		notification.Mods.AddIfNotExist(ulong.Parse(payload.Id));

		_notificationsService.RemoveNotificationsOfType<PdxModDownloadFailed>();
		_notificationsService.SendNotification(notification);
	}
}
