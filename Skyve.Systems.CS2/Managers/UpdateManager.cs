using Extensions;

using Microsoft.Extensions.DependencyInjection;

using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Skyve.Systems.CS2.Managers;
internal class UpdateManager : IUpdateManager
{
	private readonly Dictionary<string, DateTime> _previousPackages = new(new PathEqualityComparer());
	private readonly Dictionary<ulong, DateTime> _lastViewedComments = [];
	private readonly INotificationsService _notificationsService;
	private readonly IPackageManager _packageManager;
	private readonly IServiceProvider _serviceProvider;
	private readonly IInterfaceService _interfaceService;
	private readonly IWorkshopService _workshopService;
	private readonly ISkyveDataManager _skyveDataManager;
	private readonly ICompatibilityManager _compatibilityManager;
	private readonly IUserService _userService;
	private readonly ILogger _logger;
	private readonly SkyveApiUtil _skyveApiUtil;
	private readonly SaveHandler _saveHandler;

	public UpdateManager(INotificationsService notificationsService, IPackageManager packageManager, IServiceProvider serviceProvider, IInterfaceService interfaceService, SaveHandler saveHandler, ILogger logger, IWorkshopService workshopService, IUserService userService, SkyveApiUtil skyveApiUtil, ISkyveDataManager skyveDataManager, ICompatibilityManager compatibilityManager)
	{
		_notificationsService = notificationsService;
		_packageManager = packageManager;
		_serviceProvider = serviceProvider;
		_interfaceService = interfaceService;
		_saveHandler = saveHandler;
		_logger = logger;
		_workshopService = workshopService;
		_userService = userService;
		_skyveApiUtil = skyveApiUtil;
		_compatibilityManager = compatibilityManager;
		_skyveDataManager = skyveDataManager;

		try
		{
			_saveHandler.Load(out List<KnownPackage> packages, "LastPackages.json");
			_saveHandler.Load(out List<ReadComment> lastViewedComments, "LastReadComments.json");

			_lastViewedComments = lastViewedComments?.ToDictionary(x => x.PackageId, x => x.DateRead) ?? [];

			if (packages != null)
			{
				foreach (var package in packages)
				{
					if (package.Folder is not null or "")
					{
						_previousPackages[package.Folder] = package.UpdateTime;
					}
				}
			}
		}
		catch { }

		var timer = new Timer(TimeSpan.FromHours(2).TotalMilliseconds);
		timer.Elapsed += Timer_Elapsed;
		timer.Start();
	}

	public void SendUpdateNotifications()
	{
		if (IsFirstTime())
		{
			return;
		}

		var newPackages = new List<ILocalPackageData>();
		var updatedPackages = new List<ILocalPackageData>();

		foreach (var package in _packageManager.Packages.Where(x => x.LocalData is not null))
		{
			var date = _previousPackages.TryGet(package.LocalData!.Folder);

			if (date == default)
			{
				newPackages.Add(package.LocalData!);
			}
			else if (package.LocalData!.LocalTime > date)
			{
				updatedPackages.Add(package.LocalData!);
			}
		}

		if (newPackages.Count > 0)
		{
			_notificationsService.SendNotification(new NewPackagesNotificationInfo(newPackages, _interfaceService));
		}

		if (updatedPackages.Count > 0)
		{
			_notificationsService.SendNotification(new UpdatedPackagesNotificationInfo(updatedPackages, _interfaceService));
		}

		try
		{
			_saveHandler.Save(_serviceProvider.GetService<IPackageManager>()!.Packages.Where(x => x.LocalData is not null).Select(x => new KnownPackage(x.LocalData!)), "LastPackages.json");
		}
		catch (Exception ex)
		{
			_logger.Exception(ex);
		}
	}

	public bool IsPackageKnown(ILocalPackageData package)
	{
		return _previousPackages.ContainsKey(package.Folder);
	}

	public DateTime GetLastUpdateTime(ILocalPackageData package)
	{
		return _previousPackages.TryGet(package.Folder);
	}

	public bool IsFirstTime()
	{
		return _previousPackages.Count == 0;
	}

	public IEnumerable<ILocalPackageData> GetNewOrUpdatedPackages()
	{
		if (IsFirstTime())
		{
			yield break;
		}

		foreach (var package in _packageManager.Packages.Where(x => x.LocalData is not null))
		{
			var date = _previousPackages.TryGet(package.LocalData!.Folder);

			if (package.LocalData.LocalTime > date)
			{
				yield return package.LocalData;
			}
		}
	}

	public async Task SendUnreadCommentsNotifications()
	{
		await _workshopService.WaitUntilReady();

		if (_userService.User.Id is null)
		{
			return;
		}

		var dictionary = new Dictionary<IPackageIdentity, IModComment>();

		using (_workshopService.Lock)
		{
			var mods = await _workshopService.GetWorkshopItemsByUserAsync(_userService.User.Id!, sorting: WorkshopQuerySorting.DateUpdated, limit: 99);

			foreach (var mod in mods)
			{
				var comments = await _workshopService.GetComments(mod, 1, 1);

				if (comments is not null && (comments.Posts?.Any() ?? false))
				{
					if ((!_lastViewedComments.TryGetValue(mod.Id, out var date) || date < comments.Posts[0].Created) && comments.Posts[0].Username != _userService.User.Id?.ToString())
					{
						if (!_notificationsService.GetNotifications<UnreadCommentNotification>().Any(x => x.PackageId == mod.Id))
						{
							_notificationsService.SendNotification(new UnreadCommentNotification(mod, comments.Posts[0], this, _interfaceService, _notificationsService));
						}
					}

					dictionary[mod] = comments.Posts[0];
				}
			}
		}
	}

	public void MarkCommentAsRead(IPackageIdentity package)
	{
		_lastViewedComments[package.Id] = DateTime.UtcNow;

		var notification = _notificationsService.GetNotifications<UnreadCommentNotification>().FirstOrDefault(x => x.PackageId == package.Id);

		if (notification != null)
		{
			_notificationsService.RemoveNotification(notification);
		}

		_saveHandler.Save(_lastViewedComments.ToList(x => new ReadComment
		{
			DateRead = x.Value,
			PackageId = x.Key,
		}), "LastReadComments.json");
	}

	public DateTime GetLastReadComment(IPackageIdentity package)
	{
		if (_lastViewedComments.TryGetValue(package.Id, out var date))
		{
			return date;
		}

		return default;
	}

	public async Task SendReviewRequestNotifications()
	{
		var messages = await _skyveApiUtil.GetReviewMessages();

		if (messages is null)
		{
			return;
		}

		_notificationsService.RemoveNotificationsOfType<ReviewMessageNotification>();

		foreach (var item in messages)
		{
			_notificationsService.SendNotification(new ReviewMessageNotification(item, _interfaceService));
		}
	}

	public async Task MarkReviewReplyAsRead(IPackageIdentity package)
	{
		await _skyveApiUtil.DeleteReviewMessage(package.Id);

		await SendReviewRequestNotifications();
	}

	private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
	{
		try
		{
			await _skyveDataManager.DownloadData();

			await SendReviewRequestNotifications();

			await SendUnreadCommentsNotifications();

			_compatibilityManager.CacheReport();
		}
		catch { }
	}
}
