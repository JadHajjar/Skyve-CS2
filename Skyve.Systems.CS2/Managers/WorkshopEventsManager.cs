using Extensions;

using Microsoft.Extensions.DependencyInjection;

using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Events;

using Skyve.Domain;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Diagnostics;
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

	//internal void RegisterModsCallbacks(IContext context)
	//{
	//	context.Mods.Downloads.DownloadStageChanged += OnDownloadStatusChanged;
	//}

	//private void DownloadProgressChanged(Guid guid, IModDownloadStatus payload)
	//{
	//	Debug.WriteLine($"{payload.Stage} - {payload.TotalProgress*100:0.0}%");
	//	_subscriptionsManager.OnDownloadProgress(new PackageDownloadProgress
	//	{
	//		Status = payload.Stage.ToString(),
	//		Id = ulong.Parse(payload.Mod.Id),
	//		Progress = payload.TotalProgress,
	//		ProcessedBytes = payload.DownloadedBytes,
	//		Size = payload.TotalBytesToDownload
	//	});
	//}

	//private void OnDownloadStatusChanged(Guid guid, IModDownloadStatus payload)
	//{
	//	if (payload.Stage == ModDownloadStage.Failed)
	//	{
	//		SendDownloadFailedNotification(payload);
	//	}

	//	if (payload.Stage == ModDownloadStage.Started)
	//	{
	//		Task.Run(() => _workshopService.GetInfoAsync(new GenericPackageIdentity(ulong.Parse(payload.Mod.Id))));
	//	}

	//	DownloadProgressChanged(guid, payload);
	//}

	//private void SendDownloadFailedNotification(IModDownloadStatus payload)
	//{
	//	var notification = _notificationsService.GetNotifications<PdxModDownloadFailed>().FirstOrDefault() ?? new PdxModDownloadFailed();

	//	notification.Time = DateTime.Now;
	//	notification.Mods.AddIfNotExist(ulong.Parse(payload.Mod.Id));

	//	_notificationsService.RemoveNotificationsOfType<PdxModDownloadFailed>();
	//	_notificationsService.SendNotification(notification);
	//}
}
