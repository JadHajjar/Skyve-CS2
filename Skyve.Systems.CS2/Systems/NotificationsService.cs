using Extensions;

using Skyve.Domain;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;

namespace Skyve.Systems.CS2.Systems;
internal class NotificationsService : INotificationsService
{
	private readonly List<INotificationInfo> _notifications = [];
	private readonly Dictionary<string, DateTime> _readNotifications;
	private readonly SaveHandler _saveHandler;

	public event Action? OnNewNotification;

	public NotificationsService(SaveHandler saveHandler)
	{
		_saveHandler = saveHandler;

		_saveHandler.Load(out _readNotifications, "ReadNotifications.json");

		_readNotifications ??= [];
	}

	public IEnumerable<INotificationInfo> GetNotifications()
	{
		List<INotificationInfo> notifications;

		lock (this)
		{
			notifications = new(_notifications);
		}

		foreach (var item in notifications)
		{
			yield return item;
		}
	}

	public IEnumerable<TNotificationInfo> GetNotifications<TNotificationInfo>() where TNotificationInfo : INotificationInfo
	{
		List<INotificationInfo> notifications;

		lock (this)
		{
			notifications = new(_notifications);
		}

		foreach (var item in notifications)
		{
			if (item is TNotificationInfo tnotif)
			{
				yield return tnotif;
			}
		}
	}

	public void RemoveNotification(INotificationInfo notification)
	{
		lock (this)
		{
			_notifications.Remove(notification);
		}

		OnNewNotification?.Invoke();
	}

	public void RemoveNotificationsOfType<T>() where T : INotificationInfo
	{
		lock (this)
		{
			_notifications.RemoveAll(x => x is T);
		}

		OnNewNotification?.Invoke();
	}

	public void SendNotification(INotificationInfo notification)
	{
		if (_readNotifications.TryGetValue(notification.GetType().Name, out var date) && date == notification.Time)
		{
			return;
		}

		lock (this)
		{
			_notifications.Add(notification);
		}

		OnNewNotification?.Invoke();
	}

	public void MarkNotificationAsRead(INotificationInfo notification)
	{
		_readNotifications[notification.GetType().Name] = notification.Time;

		_saveHandler.Save(_readNotifications, "ReadNotifications.json");

		RemoveNotification(notification);
	}
}
