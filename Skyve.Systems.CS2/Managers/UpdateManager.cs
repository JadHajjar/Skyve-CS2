using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Skyve.Systems.CS2.Managers;
internal class UpdateManager : IUpdateManager
{
	private readonly Dictionary<string, DateTime> _previousPackages = new(new PathEqualityComparer());
	private readonly INotificationsService _notificationsService;
	private readonly IPackageManager _packageManager;
	private readonly SaveHandler _saveHandler;

	public UpdateManager(INotificationsService notificationsService, IPackageManager packageManager, SaveHandler saveHandler)
	{
		_notificationsService = notificationsService;
		_packageManager = packageManager;
		_saveHandler = saveHandler;

		try
		{
			_saveHandler.Load(out List<KnownPackage> packages, "LastPackages.json");

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
			_notificationsService.SendNotification(new NewPackagesNotificationInfo(newPackages));
		}

		if (updatedPackages.Count > 0)
		{
			_notificationsService.SendNotification(new UpdatedPackagesNotificationInfo(updatedPackages));
		}

		_saveHandler.Save(ServiceCenter.Get<IPackageManager>().Packages.Where(x => x.LocalData is not null).Select(x => new KnownPackage(x.LocalData!)), "LastPackages.json");
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
}
