using Extensions;

using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Service.Mods;
using PDX.SDK.Contracts.Service.Mods.Events;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain;
using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Skyve.Systems.CS2.Managers;

internal class SubscriptionsManager : ISubscriptionsManager
{
	const int DOWNLOAD_SPEED_CHECK_TIMER = 1000;

	private readonly List<IPackageIdentity> _subscribingTo = [];
	private readonly WorkshopService _workshopService;
	private bool modsDownloading = false;
	private List<Guid> lastEntries = [];
	private readonly Dictionary<Guid, ulong> byteTracking = [];
	private List<double> speedTracking = [];
	private double completedDownloads;

	private IModsService? modsService;

	public ulong DownloadSpeed { get; private set; }
	public bool IsPaused => modsService?.IsPaused ?? false;
	public bool IsRunning => modsService?.IsSyncOngoing == true || modsService?.IsQueueRunning == true;
	public double TotalProgress => CalculateTotalProgress();

	public event Action? UpdateDisplayNotification;
	public event Action? DownloadAddedOrRemoved;
	public event Action? DownloadProgressChanged;

	public SubscriptionsManager(IWorkshopService workshopService)
	{
		_workshopService = (WorkshopService)workshopService;

		new Timer(DOWNLOAD_SPEED_CHECK_TIMER) { Enabled = true }.Elapsed += ProcessByteTracking;
	}

	public bool IsSubscribing(IPackageIdentity package)
	{
		lock (this)
		{
			return _subscribingTo.Any(x => x.Id == package.Id && x.Version == package.Version);
		}
	}

	public void TogglePause()
	{
		if (modsService == null)
			return;

		if (modsService.IsPaused || !modsService.IsQueueRunning)
			modsService.ResumeDownloads();
		else
			modsService.PauseDownloads();
	}

	public void CancelDownloads()
	{
		modsService?.CancelDownloads();
	}

	public void RegisterModsCallbacks(IContext context)
	{
		modsService = context.Mods;

		context.Mods.Downloads.DownloadManagerStateChanged += Downloads_DownloadManagerStateChanged;
		context.Mods.Downloads.DownloadProgressChanged += Downloads_DownloadProgressChanged;
	}

	private void Downloads_DownloadProgressChanged(Guid guid, IModDownloadStatus payload)
	{
		Task.Run(() =>
		{
			if (payload.Stage == PDX.SDK.Contracts.Service.Mods.Enums.ModDownloadStage.Completed)
			{
				completedDownloads += payload.TotalBytesToDownload;
			}

			DownloadProgressChanged?.Invoke();

			var entries = _workshopService.GetOngoingDownloads().Select(x => x.Id).ToList();

			if (entries.Count != lastEntries.Count || entries.SequenceEqual(lastEntries))
				DownloadAddedOrRemoved?.Invoke();
		});
	}

	private void ProcessByteTracking(object sender, ElapsedEventArgs e)
	{
		var speed = 0D;
		foreach (var payload in _workshopService.GetOngoingDownloads())
		{
			if (payload.Stage != PDX.SDK.Contracts.Service.Mods.Enums.ModDownloadStage.Downloading)
				continue;

			var guid = payload.Id;

			if (byteTracking.TryGetValue(guid, out var lastBytes))
			{
				var deltaBytes = payload.DownloadedBytes - lastBytes;

				speed += deltaBytes * 1000 / DOWNLOAD_SPEED_CHECK_TIMER; // bytes/sec
			}

			byteTracking[guid] = payload.DownloadedBytes;
		}

		if (speed == 0)
		{
			speedTracking.Clear();
			DownloadSpeed = 0;
		}
		else
		{
			speedTracking.Add(speed);

			if (speedTracking.Count > 60_000 / DOWNLOAD_SPEED_CHECK_TIMER)
				speedTracking.RemoveAt(0);

			DownloadSpeed = (ulong)speedTracking.Average();
		}
	}

	private void Downloads_DownloadManagerStateChanged(PDX.SDK.Contracts.Service.Mods.Enums.DownloadManagerState payload)
	{
		if (modsDownloading != (payload == PDX.SDK.Contracts.Service.Mods.Enums.DownloadManagerState.DownloadStarted))
		{
			completedDownloads = 0;
			speedTracking.Clear();
			modsDownloading = !modsDownloading;
			UpdateDisplayNotification?.Invoke();
		}
	}

	private double CalculateTotalProgress()
	{
		var completed = completedDownloads;
		var total = completedDownloads;
		foreach (var x in _workshopService.GetOngoingDownloads())
		{
			completed += x.TotalProgress * x.TotalBytesToDownload;
			total += x.TotalBytesToDownload;
		}

		return completed / total;
	}

	public IEnumerable<ISubscriptionStatus> GetDownloads()
	{
		foreach (var x in _workshopService.GetOngoingDownloads())
		{
			yield return new SubscriptionStatus(x);
		}
	}

	public async Task RetryDownload(ISubscriptionStatus item)
	{
		if (modsService is not null)
			await modsService.RetryDownload(new PdxModBase(item.Mod.Id.ToString(), item.Mod.Version));
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

	public bool TryGetDownloadStatus(ulong id, out ISubscriptionStatus subscriptionStatus)
	{
		if (_workshopService.TryGetDownloadStatus(id, out var status))
		{
			subscriptionStatus = new SubscriptionStatus(status);
			return true;
		}

		subscriptionStatus = null!;
		return false;
	}

	private class SubscriptionStatus(IModDownloadStatus status) : ISubscriptionStatus
	{
		private IWorkshopInfo? mod;

		public Guid Id => status.Id;
		public ModDownloadStage Stage => (ModDownloadStage)(int)status.Stage;
		public ulong DownloadedBytes => status.DownloadedBytes;
		public ulong TotalBytesToDownload => status.TotalBytesToDownload;
		public float TotalProgress => status.TotalProgress;
		public float StageProgress => status.StageProgress;
		public IWorkshopInfo Mod => mod ??= new PdxModDetails(status.Mod);
	}
}