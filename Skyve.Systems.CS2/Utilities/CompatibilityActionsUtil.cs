using Extensions;

using Skyve.Compatibility.Domain.Enums;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Domain.Systems;

using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
public class CompatibilityActionsUtil(ICompatibilityManager compatibilityManager, IPackageUtil packageUtil, IPlaysetManager playsetManager) : ICompatibilityActionsHelper
{
	private readonly IPackageUtil _packageUtil = packageUtil;
	private readonly ICompatibilityManager _compatibilityManager = compatibilityManager;
	private readonly IPlaysetManager _playsetManager = playsetManager;

	public bool CanSnooze(ICompatibilityItem message)
	{
		return message.Status.Notification > NotificationType.Info && message.PackageId != 0;
	}

	#region RecommendedAction
	public bool HasRecommendedAction(ICompatibilityItem message)
	{
		if (_playsetManager.CurrentPlayset is null)
		{
			return false;
		}

		return message.Status.Action switch
		{
			StatusAction.DisableOther or StatusAction.IncludeOther or StatusAction.ExcludeOther or StatusAction.UnsubscribeOther => message.Packages.Any(),
			StatusAction.IncludeThis or StatusAction.UnsubscribeThis or StatusAction.ExcludeThis or StatusAction.DisableThis => true,
			StatusAction.Switch => message.Packages.Count() == 1,
			_ => false
		};
	}

	public ICompatibilityActionInfo? GetRecommendedAction(ICompatibilityItem message)
	{
		if (_playsetManager.CurrentPlayset is null)
		{
			return null;
		}

		switch (message.Status.Action)
		{
			case StatusAction.IncludeOther:
				if (message.Packages.Any())
				{
					return new ActionInfo(IncludeAndEnablePackages, Locale.IncludeAll, "Add");
				}

				break;
			case StatusAction.ExcludeOther:
				if (message.Packages.Any())
				{
					return new ActionInfo(ExcludePackages, Locale.ExcludeAll, "X");
				}

				break;
			case StatusAction.DisableOther:
				if (message.Packages.Any())
				{
					return new ActionInfo(DisablePackages, Locale.DisableAll, "Enabled");
				}

				break;
			case StatusAction.UnsubscribeOther:
				if (message.Packages.Any())
				{
					return new ActionInfo(ExcludePackagesInAllPlaysets, Locale.ExcludeThisItemInAllPlaysets.Plural, "Trash");
				}

				break;

			case StatusAction.UnsubscribeThis:
				return new ActionInfo(ExcludeMainInAllPlaysets, Locale.ExcludeThisItemInAllPlaysets, "Trash");
			case StatusAction.ExcludeThis:
				return new ActionInfo(ExcludeMain, Locale.Exclude, "X");
			case StatusAction.IncludeThis:
				return new ActionInfo(IncludeAndEnableMain, Locale.Include, "Add");
			case StatusAction.DisableThis:
				return new ActionInfo(DisableMain, Locale.DisableItem, "Enabled");

			case StatusAction.Switch:
				if (message.Packages.Count() == 1)
				{
					return new ActionInfo(SwitchToPackages, LocaleCR.Switch, "Switch");
				}

				break;
		}

		return null;
	}

	private async Task SwitchToPackages(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetEnabled(message.Packages, true);

		await _packageUtil.SetIncluded(message.Packages, true, withVersion: false, promptForDependencies: false);

		await _packageUtil.SetIncluded(message, false, withVersion: false, promptForDependencies: false);
	}

	private async Task IncludeAndEnablePackages(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetIncluded(message.Packages, true, withVersion: false, promptForDependencies: false);

		await _packageUtil.SetEnabled(message.Packages, true);
	}

	private async Task ExcludePackages(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetIncluded(message.Packages, false, withVersion: false, promptForDependencies: false);
	}

	private async Task DisablePackages(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetEnabled(message.Packages, false);
	}

	private async Task ExcludePackagesInAllPlaysets(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _playsetManager.SetIncludedForAll(message.Packages, false, withVersion: false, promptForDependencies: false);
	}

	private async Task IncludeAndEnableMain(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetIncluded(message, true, withVersion: false, promptForDependencies: false);

		await _packageUtil.SetEnabled(message, true);
	}

	private async Task ExcludeMain(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetIncluded(message, false, withVersion: false, promptForDependencies: false);
	}

	private async Task DisableMain(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetEnabled(message, false);
	}

	private async Task ExcludeMainInAllPlaysets(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _playsetManager.SetIncludedForAll(message, false, withVersion: false, promptForDependencies: false);
	}

	#endregion

	#region BulkAction
	public bool HasBulkAction(ICompatibilityItem message)
	{
		return message.Status.Action switch
		{
			StatusAction.DisableOther or StatusAction.IncludeOther or StatusAction.ExcludeOther or StatusAction.UnsubscribeOther => _playsetManager.CurrentPlayset is not null && message.Packages.Count() > 1,
			StatusAction.RequiresConfiguration => true,
			_ => false,
		};
	}

	public ICompatibilityActionInfo? GetBulkAction(ICompatibilityItem message)
	{
		if (_playsetManager.CurrentPlayset is null && message.Status.Action != StatusAction.RequiresConfiguration)
		{
			return null;
		}

		switch (message.Status.Action)
		{
			case StatusAction.IncludeOther:
				if (message.Packages.Count() > 1)
				{
					var anyExcluded = message.Packages.Any(x => !_packageUtil.IsIncluded(x, withVersion: false));

					return new ActionInfo(IncludeAndEnablePackages
						, anyExcluded ? Locale.IncludeAll : Locale.EnableAll
						, anyExcluded ? "Add" : "Ok"
						, FormDesign.Design.GreenColor);
				}

				break;
			case StatusAction.ExcludeOther:
				if (message.Packages.Count() > 1)
				{
					return new ActionInfo(ExcludePackages, Locale.ExcludeAll, "X", FormDesign.Design.RedColor);
				}

				break;
			case StatusAction.DisableOther:
				if (message.Packages.Count() > 1)
				{
					return new ActionInfo(DisablePackages, Locale.DisableAll, "Enabled", FormDesign.Design.OrangeColor);
				}

				break;
			case StatusAction.UnsubscribeOther:
				if (message.Packages.Count() > 1)
				{
					return new ActionInfo(ExcludePackagesInAllPlaysets, Locale.ExcludeThisItemInAllPlaysets.Plural, "Trash", FormDesign.Design.RedColor);
				}

				break;

			case StatusAction.RequiresConfiguration:
				return new ActionInfo(ToggleSnoozed
					, _compatibilityManager.IsSnoozed(message) ? Locale.UnSnooze : Locale.Snooze
					, "Snooze"
					, Color.FromArgb(100, 60, 220));
		}

		return null;
	}

	private Task ToggleSnoozed(ICompatibilityItem message, IPackageIdentity? package)
	{
		_compatibilityManager.ToggleSnoozed(message);

		return Task.CompletedTask;
	}
	#endregion

	#region Action
	public bool HasAction(ICompatibilityItem message, IPackageIdentity package)
	{
		if (_playsetManager.CurrentPlayset is null)
		{
			return false;
		}

		return message.Status.Action switch
		{
			StatusAction.IncludeOther => !_packageUtil.IsIncludedAndEnabled(package, withVersion: false),
			StatusAction.DisableOther => _packageUtil.IsEnabled(package, withVersion: false),
			StatusAction.ExcludeOther => _packageUtil.IsIncluded(package, withVersion: false),
			StatusAction.UnsubscribeOther => package.GetLocalPackage() is not null,
			StatusAction.SelectOne => true,
			StatusAction.Switch => true,
			_ => false,
		};
	}

	public ICompatibilityActionInfo? GetAction(ICompatibilityItem message, IPackageIdentity package)
	{
		if (_playsetManager.CurrentPlayset is null)
		{
			return null;
		}

		switch (message.Status.Action)
		{
			case StatusAction.IncludeOther:
				if (!_packageUtil.IsIncluded(package, withVersion: false))
				{
					return new ActionInfo(IncludeAndEnablePackage, Locale.IncludeItem, "Add", FormDesign.Design.GreenColor);
				}
				else if (!_packageUtil.IsEnabled(package, withVersion: false))
				{
					return new ActionInfo(IncludeAndEnablePackage, Locale.EnableItem, "Ok", FormDesign.Design.GreenColor);
				}

				break;

			case StatusAction.DisableOther:
				if (_packageUtil.IsEnabled(package, withVersion: false))
				{
					return new ActionInfo(DisablePackage, Locale.EnableItem, "Enabled", FormDesign.Design.OrangeColor);
				}

				break;

			case StatusAction.ExcludeOther:
				if (_packageUtil.IsIncluded(package, withVersion: false))
				{
					return new ActionInfo(ExcludePackage, Locale.ExcludeItem, "X", FormDesign.Design.RedColor);
				}

				break;

			case StatusAction.UnsubscribeOther:
				if (package.GetLocalPackage() is not null)
				{
					return new ActionInfo(ExcludePackageInAllPlaysets, Locale.ExcludeThisItemInAllPlaysets, "Trash", FormDesign.Design.RedColor);
				}

				break;

			case StatusAction.SelectOne:
				return new ActionInfo(SelectPackage, Locale.SelectThisPackage, string.Empty);

			case StatusAction.Switch:
				return new ActionInfo(SwitchPackage, Locale.SwitchToItem, "Switch");
		}

		return null;
	}

	private async Task SelectPackage(ICompatibilityItem message, IPackageIdentity? package)
	{
		if (package is null)
		{
			return;
		}

		await _packageUtil.SetIncluded(message.Packages.Where(x => !x.Equals(package)), false, withVersion: false, promptForDependencies: false);

		await _packageUtil.SetIncluded(package, true, withVersion: false, promptForDependencies: false);

		await _packageUtil.SetEnabled(package, true);
	}

	private async Task IncludeAndEnablePackage(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		if (package is null)
		{
			return;
		}

		await _packageUtil.SetIncluded(package, true, withVersion: false, promptForDependencies: false);

		await _packageUtil.SetEnabled(package, true);
	}

	private async Task ExcludePackage(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		if (package is null)
		{
			return;
		}

		await _packageUtil.SetIncluded(package, false, withVersion: false, promptForDependencies: false);
	}

	private async Task DisablePackage(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		if (package is null)
		{
			return;
		}

		await _packageUtil.SetEnabled(package, false);
	}

	private async Task ExcludePackageInAllPlaysets(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		if (package is null)
		{
			return;
		}

		await _playsetManager.SetIncludedForAll(package, false, withVersion: false, promptForDependencies: false);
	}

	private async Task SwitchPackage(ICompatibilityItem message, IPackageIdentity? package)
	{
		if (package is null)
		{
			return;
		}

		await _packageUtil.SetIncluded([message, .. message.Packages.Where(x => !x.Equals(package))], false, withVersion: false, promptForDependencies: false);

		await _packageUtil.SetIncluded(package, true, withVersion: false, promptForDependencies: false);

		await _packageUtil.SetEnabled(package, true);
	}
	#endregion

	private class ActionInfo(ActionInfoDelegate action, string text, string icon, Color? color = null) : ICompatibilityActionInfo
	{
		public ActionInfoDelegate Action { get; } = action;
		public string Text { get; } = text;
		public string Icon { get; } = icon;
		public Color? Color { get; } = color;

		public async Task Invoke(ICompatibilityItem message, IPackageIdentity? package = null)
		{
			await Action(message, package);
		}
	}

	private delegate Task ActionInfoDelegate(ICompatibilityItem message, IPackageIdentity? package = null);
}
