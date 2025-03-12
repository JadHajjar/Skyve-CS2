using Extensions;

using Skyve.Domain.Systems;
using Skyve.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Skyve.Domain.CS2.Notifications;
public class NewPackagesNotificationInfo : INotificationInfo
{
	private readonly IInterfaceService _interfaceService;
	public NewPackagesNotificationInfo(List<ILocalPackageData> newPackages, IInterfaceService interfaceService)
	{
		_interfaceService = interfaceService;
		_packages = newPackages;
		Time = newPackages.Max(x => x.LocalTime).ToLocalTime();
		Title = Locale.NewPackages;
		Description = Locale.NewPackagesSinceSession.FormatPlural(newPackages.Count, newPackages[0].CleanName());
		Icon = "New";
		HasAction = true;
	}

	private readonly List<ILocalPackageData> _packages;

	public DateTime Time { get; }
	public string Title { get; }
	public string? Description { get; }
	public string Icon { get; }
	public Color? Color { get; }
	public bool HasAction { get; }
	public bool CanBeRead { get; }

	public void OnClick()
	{
		_interfaceService.ViewSpecificPackages(_packages.ToList(x => (IPackageIdentity)x), Title);
	}

	public void OnRightClick()
	{
	}

	public void OnRead()
	{
		throw new NotImplementedException();
	}
}
