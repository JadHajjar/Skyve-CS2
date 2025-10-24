using Extensions;

using Microsoft.Extensions.DependencyInjection;

using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Events.Download;
using PDX.SDK.Contracts.Events.Mods;

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

	internal void RegisterModsCallbacks(IContext context)
	{
		context.Events.Subscribe<IModDownloadStarted>(OnDownloadStarted);
		context.Events.Subscribe<IModDownloadCompleted>(OnDownloadComplete);
		context.Events.Subscribe<ITransferStatusUpdated>(OnTransferUpdated);
		context.Events.Subscribe<IInstallProgressEvent>(OnInstallProgress);
		context.Events.Subscribe<IModDownloadFailed>(OnModDownloadFailed);
		context.Events.Subscribe<IModDownloadCancelledByUser>(OnModDownloadCancelled);
		//context.Events.Subscribe<IModUnsubscribed>(OnModUnsubscribe);
	}

	private void OnModDownloadFailed(IModDownloadFailed failed)
	{
		var notification = _notificationsService.GetNotifications<PdxModDownloadFailed>().FirstOrDefault() ?? new PdxModDownloadFailed();

		notification.Time = DateTime.Now;
		notification.Mods.Add(ulong.Parse(failed.Id));

		_notificationsService.RemoveNotificationsOfType<PdxModDownloadFailed>();
		_notificationsService.SendNotification(notification);

		_subscriptionsManager.OnInstallFinished(new PackageInstallProgress
		{
			Id = ulong.Parse(failed.Id),
			Progress = -1
		});
	}

	private void OnInstallProgress(IInstallProgressEvent @event)
	{
		switch (@event.CurrentStage)
		{
			case PDX.SDK.Contracts.Enums.InstallStage.PreStepsStarted:
			case PDX.SDK.Contracts.Enums.InstallStage.PatchDownloadStarted:
				_subscriptionsManager.OnInstallStarted(new PackageInstallProgress
				{
					Id = (ulong)@event.Reference.SmartParse(),
					Progress = 0
				});
				break;
			case PDX.SDK.Contracts.Enums.InstallStage.PatchDownloadCancelled:
				_subscriptionsManager.OnInstallProgress(new PackageInstallProgress
				{
					Id = (ulong)@event.Reference.SmartParse(),
					Progress = -2f,
				});
				break;
			default:
				_subscriptionsManager.OnInstallProgress(new PackageInstallProgress
				{
					Id = (ulong)@event.Reference.SmartParse(),
					Progress = @event.Progress,
				});
				break;
		}
	}

	private void OnTransferUpdated(ITransferStatusUpdated updated)
	{
		_subscriptionsManager.OnDownloadProgress(new PackageDownloadProgress
		{
			Id = (ulong)updated.TransferStatus.Id.SmartParse(),
			Progress = updated.TransferStatus.Progress,
			ProcessedBytes = updated.TransferStatus.ProcessedBytes,
			Size = updated.TransferStatus.Size
		});
	}

	private void OnDownloadComplete(IModDownloadCompleted completed)
	{
		_subscriptionsManager.OnInstallFinished(new PackageInstallProgress
		{
			Id = ulong.Parse(completed.Id),
			Progress = 1
		});
	}

	private void OnModDownloadCancelled(IModDownloadCancelledByUser cancelled)
	{
		_subscriptionsManager.OnDownloadCancelled(new PackageInstallProgress
		{
			Id = ulong.Parse(cancelled.Id),
			Progress = -2
		});
	}

	private void OnDownloadStarted(IModDownloadStarted started)
	{
		_subscriptionsManager.OnInstallStarted(new PackageInstallProgress
		{
			Id = ulong.Parse(started.Id),
			Progress = 0
		});

		Task.Run(() => _workshopService.GetInfoAsync(new GenericPackageIdentity(ulong.Parse(started.Id))));
	}
}
