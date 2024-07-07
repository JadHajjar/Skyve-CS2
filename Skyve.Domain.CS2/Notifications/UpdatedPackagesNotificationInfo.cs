using Extensions;

using Skyve.Domain.Systems;
using Skyve.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Skyve.Domain.CS2.Notifications;
public class UpdatedPackagesNotificationInfo : INotificationInfo
{
	private readonly IInterfaceService _interfaceService;
	public UpdatedPackagesNotificationInfo(List<ILocalPackageData> updatedPackages, IInterfaceService interfaceService)
	{
		_interfaceService = interfaceService;
		_packages = updatedPackages;
		Time = updatedPackages.Max(x => x.LocalTime);
		Title = Locale.PackageUpdates;
		Description = Locale.PackagesUpdatedSinceSession.FormatPlural(updatedPackages.Count, updatedPackages[0].CleanName());
		Icon = "ReDownload";
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
