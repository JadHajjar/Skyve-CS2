using Extensions;

using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class SubscriptionsManager(IWorkshopService workshopService, ISettings settings, INotifier notifier) : ISubscriptionsManager
{
	private readonly List<ulong> _subscribingTo = [];
	private readonly List<ulong> _unsubscribingFrom = [];
	private readonly WorkshopService _workshopService = (WorkshopService)workshopService;
	private readonly ISettings _settings = settings;
	private readonly INotifier _notifier = notifier;

	private float downloadProgress;
	private float installProgress;

	public event Action? UpdateDisplayNotification;

	public SubscriptionStatus Status { get; private set; }

	public bool IsSubscribing(IPackageIdentity package)
	{
		return _subscribingTo.Contains(package.Id) || _unsubscribingFrom.Contains(package.Id);
	}

	public void OnDownloadProgress(PackageDownloadProgress info)
	{
		downloadProgress = info.Progress;

		Status = new SubscriptionStatus(
			isActive: true,
			modId: info.Id,
			progress: (installProgress + downloadProgress * 3) / 4f,
			processedBytes: info.ProcessedBytes,
			totalSize: info.Size);

		UpdateDisplayNotification?.Invoke();
	}

	public void OnInstallFinished(PackageInstallProgress info)
	{
		Status = new SubscriptionStatus(
			isActive: false,
			modId: info.Id,
			progress: info.Progress,
			processedBytes: 0,
			totalSize: 0);

		UpdateDisplayNotification?.Invoke();

		_notifier.OnRefreshUI(true);
	}

	public void OnInstallProgress(PackageInstallProgress info)
	{
		installProgress = info.Progress;

		Status = new SubscriptionStatus(
			isActive: true,
			modId: info.Id,
			progress: (installProgress + downloadProgress * 3) / 4f,
			processedBytes: 0,
			totalSize: 0);

		UpdateDisplayNotification?.Invoke();
	}

	public void OnInstallStarted(PackageInstallProgress info)
	{
		installProgress = downloadProgress = 0f;
		Status = new SubscriptionStatus(
			isActive: true,
			modId: info.Id,
			progress: info.Progress,
			processedBytes: 0,
			totalSize: 0);

		UpdateDisplayNotification?.Invoke();
	}

	public async Task<bool> Subscribe(IEnumerable<IPackageIdentity> ids, int? playsetId = null)
	{
		if (!_workshopService.IsAvailable)
		{
			return false;
		}

		var currentPlayset = playsetId ?? await _workshopService.GetActivePlaysetId();

		if (currentPlayset == 0)
		{
			return false;
		}

		_subscribingTo.AddRange(ids.Select(x => x.Id).Where(x => x > 0));

		_notifier.OnRefreshUI(true);

		await _workshopService.WaitUntilReady();

		bool result;
		using (_workshopService.Lock)
		{
			result = await _workshopService.SubscribeBulk(
				ids.Distinct(x => x.Id).Select(x => new KeyValuePair<int, string?>((int)x.Id, null)),
				currentPlayset,
				!_settings.UserSettings.DisableNewModsByDefault);
		}

		foreach (var item in ids)
		{
			_subscribingTo.Remove(item.Id);
		}

		_notifier.OnRefreshUI(true);
		_notifier.OnPlaysetChanged();

		return result;
	}

	public async Task<bool> UnSubscribe(IEnumerable<IPackageIdentity> ids, int? playsetId = null)
	{
		if (!_workshopService.IsAvailable)
		{
			return false;
		}

		var currentPlayset = playsetId ?? await _workshopService.GetActivePlaysetId();

		if (currentPlayset == 0)
		{
			return false;
		}

		_unsubscribingFrom.AddRange(ids.Select(x => x.Id));

		_notifier.OnRefreshUI(true);

		await _workshopService.WaitUntilReady();

		bool result;
		using (_workshopService.Lock)
		{
			result = await _workshopService.UnsubscribeBulk(ids.Distinct(x => x.Id).Select(x => (int)x.Id), currentPlayset);
		}

		foreach (var item in ids)
		{
			_unsubscribingFrom.Remove(item.Id);
		}

		_notifier.OnRefreshUI(true);
		_notifier.OnPlaysetChanged();

		return result;
	}
}