using PDX.SDK.Contracts;

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
	private readonly List<ulong> _subscribingTo = new();
	private readonly List<ulong> _unsubscribingFrom = new();
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
			progress: (installProgress + downloadProgress) / 2f,
			processedBytes: info.ProcessedBytes,
			totalSize: info.Size);

		UpdateDisplayNotification?.Invoke();
	}

	public void OnInstallFinished(PackageInstallProgress info)
	{
		Status = new SubscriptionStatus(
			isActive: false,
			modId: info.Id,
			progress: 1f,
			processedBytes: 0,
			totalSize: 0);

		UpdateDisplayNotification?.Invoke();

		_notifier.OnRefreshUI();
	}

	public void OnInstallProgress(PackageInstallProgress info)
	{
		installProgress = info.Progress;

		Status = new SubscriptionStatus(
			isActive: true,
			modId: info.Id,
			progress: (installProgress + downloadProgress) / 2f,
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

	public async Task<bool> Subscribe(IEnumerable<IPackageIdentity> ids)
	{
		if (_workshopService.Context is null)
		{
			return false;
		}

		var currentPlayset = await _workshopService.Context.Mods.GetActivePlayset();

		if (!currentPlayset.Success)
		{
			return false;
		}

		_subscribingTo.AddRange(ids.Select(x => x.Id));

		_notifier.OnRefreshUI();

		var result = await _workshopService.Context!.Mods.SubscribeBulk(
			ids.Select(x => new KeyValuePair<int, string?>((int)x.Id, null)),
			currentPlayset.PlaysetId,
			!_settings.UserSettings.DisableNewModsByDefault);

		foreach (var item in ids)
		{
			_subscribingTo.Remove(item.Id);
		}

		_notifier.OnRefreshUI();
		_notifier.OnPlaysetChanged();
		
		return result.Success;
	}

	public async Task<bool> UnSubscribe(IEnumerable<IPackageIdentity> ids)
	{
		if (_workshopService.Context is null)
		{
			return false;
		}

		var currentPlayset = await _workshopService.Context.Mods.GetActivePlayset();

		if (!currentPlayset.Success)
		{
			return false;
		}

		_unsubscribingFrom.AddRange(ids.Select(x => x.Id));

		_notifier.OnRefreshUI();

		var results = new List<Result>();

		foreach (var id in ids)
		{
			var result = await _workshopService.Context!.Mods.Unsubscribe((int)id.Id, currentPlayset.PlaysetId);

			results.Add(result);
		}

		foreach (var item in ids)
		{
			_unsubscribingFrom.Remove(item.Id);
		}

		_notifier.OnRefreshUI();
		_notifier.OnPlaysetChanged();

		return results.All(x => x.Success);
	}
}