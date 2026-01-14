using Extensions;

using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Service.Mods.Events;

using Skyve.Domain;
using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Skyve.Systems.CS2.Managers;
internal class SubscriptionsManager(IWorkshopService workshopService, ISettings settings, INotifier notifier) : ISubscriptionsManager
{
	private readonly List<IPackageIdentity> _subscribingTo = [];
	private readonly WorkshopService _workshopService = (WorkshopService)workshopService;
	private readonly ISettings _settings = settings;
	private readonly INotifier _notifier = notifier;
	private bool modsDownloading = false;

	public event Action? UpdateDisplayNotification;

	public bool IsSubscribing(IPackageIdentity package)
	{
		lock (this)
		{
			return _subscribingTo.Any(x => x.Id == package.Id && x.Version == package.Version);
		}
	}

	public void RegisterModsCallbacks(IContext context)
	{
		context.Mods.Downloads.DownloadStageChanged += DownloadProgressChanged;
	}

	private void DownloadProgressChanged(Guid guid, IModDownloadStatus payload)
	{
		if (modsDownloading != _workshopService.GetOngoingDownloads().Any(x => x.Stage is > PDX.SDK.Contracts.Service.Mods.Enums.ModDownloadStage.Pending and < PDX.SDK.Contracts.Service.Mods.Enums.ModDownloadStage.Completed))
		{
			modsDownloading = !modsDownloading;

			UpdateDisplayNotification?.Invoke();
		}
	}

	public IEnumerable<SubscriptionStatus> GetDownloads()
	{
		foreach (var x in _workshopService.GetOngoingDownloads())
		{
			if (x.Stage is <= PDX.SDK.Contracts.Service.Mods.Enums.ModDownloadStage.Pending or >= PDX.SDK.Contracts.Service.Mods.Enums.ModDownloadStage.Completed)
			{
				continue;
			}

			yield return new SubscriptionStatus((ModDownloadStage)(int)x.Stage, x.DownloadedBytes, x.TotalBytesToDownload, x.TotalProgress, x.StageProgress, new PdxModDetails(x.Mod));
		}
	}

	public void AddSubscribing(IEnumerable<IPackageIdentity> ids)
	{
		lock (this)
		{
			_subscribingTo.AddRange(ids);
		}
	}

	public void RemoveSubscribing(IEnumerable<IPackageIdentity> ids)
	{
		lock (this)
		{
			_subscribingTo.RemoveAll(x => ids.Contains(x));
		}
	}

	public bool TryGetDownloadStatus(ulong id, out SubscriptionStatus subscriptionStatus)
	{
		if ( _workshopService.TryGetDownloadStatus(id, out var status))
		{
			subscriptionStatus= new SubscriptionStatus((ModDownloadStage)(int)status.Stage, status.DownloadedBytes, status.TotalBytesToDownload, status.TotalProgress, status.StageProgress, new PdxModDetails(status.Mod));
			return true;
		}

		subscriptionStatus = null!;
		return false;
	}
}