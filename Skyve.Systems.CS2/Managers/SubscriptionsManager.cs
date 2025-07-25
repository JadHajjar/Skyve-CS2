using Extensions;

using Skyve.Domain;
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

	public event Action? UpdateDisplayNotification;

	public SubscriptionStatus Status { get; private set; }

	public bool IsSubscribing(IPackageIdentity package)
	{
		lock (this)
		{
			return _subscribingTo.Any(x => x.Id == package.Id && x.Version == package.Version);
		}
	}

	public void OnDownloadProgress(PackageDownloadProgress info)
	{
		Status = new SubscriptionStatus(
			isActive: true,
			modId: info.Id.If(0UL, Status.ModId),
			progress: info.Progress * 3 / 4f,
			processedBytes: info.ProcessedBytes,
			totalSize: info.Size);

		UpdateDisplayNotification?.Invoke();
	}

	public void OnInstallFinished(PackageInstallProgress info)
	{
		Status = new SubscriptionStatus(
			isActive: false,
			modId: info.Id.If(0UL, Status.ModId),
			progress: 1f,
			processedBytes: 0,
			totalSize: 0);

		UpdateDisplayNotification?.Invoke();

		_notifier.OnRefreshUI(true);
	}

	public void OnDownloadCancelled(PackageInstallProgress info)
	{
		Status = new SubscriptionStatus(
			isActive: false,
			modId: info.Id.If(0UL, Status.ModId),
			progress: -2f,
			processedBytes: 0,
			totalSize: 0);

		UpdateDisplayNotification?.Invoke();

		_notifier.OnRefreshUI(true);
	}

	public void OnInstallProgress(PackageInstallProgress info)
	{
		Status = new SubscriptionStatus(
			isActive: true,
			modId: info.Id.If(0UL, Status.ModId),
			progress: (info.Progress + 3) / 4f,
			processedBytes: 0,
			totalSize: 0);

		UpdateDisplayNotification?.Invoke();
	}

	public void OnInstallStarted(PackageInstallProgress info)
	{
		Status = new SubscriptionStatus(
			isActive: true,
			modId: info.Id.If(0UL, Status.ModId),
			progress: info.Progress,
			processedBytes: 0,
			totalSize: 0);

		UpdateDisplayNotification?.Invoke();
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
}