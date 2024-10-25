using Skyve.Domain;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Domain.Api;
public class AnnouncementNotification(string title, string description, DateTime date) : INotificationInfo
{
	public DateTime Time { get; } = date;
	public string Title { get; } = title;
	public string? Description { get; } = description;
	public string Icon { get; } = "Megaphone";
	public Color? Color { get; }
	public bool HasAction { get; }
	public bool CanBeRead { get; }

	public void OnClick()
	{
	}

	public void OnRead()
	{
		throw new NotImplementedException();
	}

	public void OnRightClick()
	{
	}
}
