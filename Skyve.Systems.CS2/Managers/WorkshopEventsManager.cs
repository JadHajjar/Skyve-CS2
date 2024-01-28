using Extensions;

using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Events.Download;
using PDX.SDK.Contracts.Events.Mods;

using Skyve.Domain;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class WorkshopEventsManager
{
	private readonly WorkshopService _workshopService;
	private readonly ContentManager _contentManager;
	private readonly INotificationsService _notificationsService;
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly IPackageManager _packageManager;

	internal WorkshopEventsManager(WorkshopService workshopService)
	{
		_workshopService = workshopService;
		_contentManager = ServiceCenter.Get<IContentManager, ContentManager>();

		ServiceCenter.Get(out _notificationsService, out _subscriptionsManager, out _packageManager);
	}

	internal void RegisterModsCallbacks(IContext context)
	{
		context.Events.Subscribe<IModDownloadStarted>(OnDownloadStarted);
		context.Events.Subscribe<IModDownloadCompleted>(OnDownloadComplete);
		context.Events.Subscribe<IModUnsubscribed>(OnModUnsubscribe);
		context.Events.Subscribe<ITransferStatusUpdated>(OnTransferUpdated);
		context.Events.Subscribe<IInstallProgressEvent>(OnInstallProgress);
		context.Events.Subscribe<IModDownloadFailed>(OnModDownloadFailed);
	}

	private void OnModDownloadFailed(IModDownloadFailed failed)
	{
		_notificationsService.SendNotification(new PdxModDownloadFailed(failed.ModId));
	}

	private void OnInstallProgress(IInstallProgressEvent @event)
	{
		_subscriptionsManager.OnInstallProgress(new PackageInstallProgress
		{
			Id = (ulong)@event.Reference.SmartParse(),
			Progress = @event.Progress,
		});
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

	private void OnModUnsubscribe(IModUnsubscribed unsubscribed)
	{
		//_subscriptionsManager.OnModUnsubscribed(unsubscribed.ManagedModId);

		var oldPackage = _packageManager.GetPackageById(new GenericPackageIdentity((ulong)unsubscribed.ManagedModId));
		if (oldPackage is not null)
		{
			_packageManager.RemovePackage(oldPackage);
		}
	}

	private async void OnDownloadComplete(IModDownloadCompleted completed)
	{
		_subscriptionsManager.OnInstallFinished(new PackageInstallProgress
		{
			Id = (ulong)completed.ModId,
			Progress = 1
		});

		var allMods = await _workshopService.GetLocalPackages();
		var newMod = allMods.FirstOrDefault(x => x.Id == completed.ModId);

		if (newMod is not null)
		{
			var pdxPackage = _contentManager.GetPackage(newMod.LocalData.FolderAbsolutePath, true, newMod);

			if (pdxPackage is not null)
			{
				_packageManager.AddPackage(pdxPackage);
			}
		}
	}

	private void OnDownloadStarted(IModDownloadStarted started)
	{
		_subscriptionsManager.OnInstallStarted(new PackageInstallProgress
		{
			Id = (ulong)started.ModId,
			Progress = 0
		});

		Task.Run(() => _workshopService.GetInfoAsync(new GenericPackageIdentity((ulong)started.ModId)));
	}
}
