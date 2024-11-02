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
		var actionRecommended = message.Status.Action switch
		{
			StatusAction.SubscribeToPackages or StatusAction.IncludeOther or StatusAction.ExcludeOther or StatusAction.UnsubscribeOther => message.Packages.Any(),
			StatusAction.IncludeThis or StatusAction.UnsubscribeThis or StatusAction.ExcludeThis => true,
			StatusAction.Switch => message.Packages.Count() == 1,
			_ => false
		};

		if (actionRecommended)
		{
			return true;
		}

		if (message.Status.Notification is NotificationType.Unsubscribe && _packageUtil.IsIncluded(message))
		{
			return true;
		}

		if (message.Status.Notification is NotificationType.Exclude && _packageUtil.IsEnabled(message))
		{
			return true;
		}

		return false;
	}

	public ICompatibilityActionInfo? GetRecommendedAction(ICompatibilityItem message)
	{
		switch (message.Status.Action)
		{
			case StatusAction.SubscribeToPackages:
			case StatusAction.IncludeOther:
				if (message.Packages.Any())
				{
					return new ActionInfo(IncludeAndEnablePackages, Locale.IncludeAll, "Add");
				}

				break;
			case StatusAction.ExcludeOther:
			case StatusAction.UnsubscribeOther:
				if (message.Packages.Any())
				{
					return new ActionInfo(ExcludeAndDisablePackages, Locale.ExcludeAll, "X");
				}

				break;
			case StatusAction.UnsubscribeThis:
				return new ActionInfo(ExcludeAndDisableMain, Locale.Exclude, "X");
			case StatusAction.ExcludeThis:
				return new ActionInfo(DisableMain, Locale.Exclude, "X");
			case StatusAction.IncludeThis:
				return new ActionInfo(IncludeAndEnableMain, Locale.Include, "Add");
			case StatusAction.Switch:
				if (message.Packages.Count() == 1)
				{
					return new ActionInfo(SwitchToPackages, LocaleCR.Switch, "Switch");
				}

				break;
		}

		if (message.Status.Notification is NotificationType.Unsubscribe && _packageUtil.IsIncluded(message))
		{
			return new ActionInfo(ExcludeAndDisableMain, Locale.Exclude, "X");
		}

		if (message.Status.Notification is NotificationType.Exclude && _packageUtil.IsEnabled(message))
		{
			return new ActionInfo(DisableMain, Locale.DisableItem, "X");
		}

		return null;
	}

	private async Task SwitchToPackages(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetEnabled(message.Packages, true);
		
		await _packageUtil.SetIncluded(message.Packages, true);
		
		await _packageUtil.SetEnabled(message, false);
		
		await _packageUtil.SetIncluded(message, false);
	}

	private async Task IncludeAndEnablePackages(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetEnabled(message.Packages, true);
		
		await _packageUtil.SetIncluded(message.Packages, true);
	}

	private async Task ExcludeAndDisablePackages(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetEnabled(message.Packages, false);
		
		await _packageUtil.SetIncluded(message.Packages, false);
	}

	private async Task IncludeAndEnableMain(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetEnabled(message, true);
		
		await _packageUtil.SetIncluded(message, true);
	}

	private async Task ExcludeAndDisableMain(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		if (message.IsIncluded())
		{
			await _packageUtil.SetIncluded(message, false);
		}
		else
		{
			await _playsetManager.SetIncludedForAll(message, false);
		}
	}

	private async Task DisableMain(ICompatibilityItem message, IPackageIdentity? package = null)
	{
		await _packageUtil.SetEnabled(message, false);
	}

	#endregion

	#region BulkAction
	public bool HasBulkAction(ICompatibilityItem message)
	{
		return message.Status.Action switch
		{
			StatusAction.SubscribeToPackages or StatusAction.IncludeOther or StatusAction.ExcludeOther or StatusAction.UnsubscribeOther => message.Packages.Count() > 1,
			StatusAction.RequiresConfiguration => true,
			_ => false,
		};
	}

	public ICompatibilityActionInfo? GetBulkAction(ICompatibilityItem message)
	{
		switch (message.Status.Action)
		{
			case StatusAction.SubscribeToPackages:
			case StatusAction.IncludeOther:
				if (message.Packages.Count() > 1)
				{
					var anyExcluded = message.Packages.Any(x => !_packageUtil.IsIncluded(x));

					return new ActionInfo(IncludeAndEnablePackages
						, anyExcluded ? Locale.IncludeAll : Locale.EnableAll
						, anyExcluded ? "Add" : "Ok"
						, FormDesign.Design.GreenColor);
				}

				break;

			case StatusAction.ExcludeOther:
			case StatusAction.UnsubscribeOther:
				if (message.Packages.Count() > 1)
				{
					return new ActionInfo(ExcludeAndDisablePackages, Locale.ExcludeAll, "X");
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
		switch (message.Status.Action)
		{
			case StatusAction.SubscribeToPackages:
				if (!_packageUtil.IsIncluded(package))
				{
					return true;
				}
				else if (!_packageUtil.IsEnabled(package))
				{
					return true;
				}

				break;

			case StatusAction.SelectOne:
				return true;

			case StatusAction.Switch:
				return true;
		}

		return false;
	}

	public ICompatibilityActionInfo? GetAction(ICompatibilityItem message, IPackageIdentity package)
	{
		switch (message.Status.Action)
		{
			case StatusAction.SubscribeToPackages:
				if (!_packageUtil.IsIncluded(package))
				{
					return new ActionInfo(IncludePackage, Locale.IncludeItem, "Add");
				}
				else if (!_packageUtil.IsEnabled(package))
				{
					return new ActionInfo(EnablePackage, Locale.EnableItem, "Enabled", FormDesign.Design.GreenColor);
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
		
		await _packageUtil.SetIncluded(message.Packages.Where(x => !x.Equals(package)), false);
		
		await _packageUtil.SetIncluded(package, true);

		await _packageUtil.SetEnabled(package, true);
	}

	private async Task IncludePackage(ICompatibilityItem message, IPackageIdentity? package)
	{
		if (package is null)
		{
			return;
		}
		
		await _packageUtil.SetIncluded(package, true);

		await _packageUtil.SetEnabled(package, true);
	}

	private async Task EnablePackage(ICompatibilityItem message, IPackageIdentity? package)
	{
		if (package is null)
		{
			return;
		}

		await _packageUtil.SetEnabled(package, true);
	}

	private async Task SwitchPackage(ICompatibilityItem message, IPackageIdentity? package)
	{
		if (package is null)
		{
			return;
		}
		
		await _packageUtil.SetIncluded([message, .. message.Packages.Where(x => !x.Equals(package))], false);
		
		await _packageUtil.SetIncluded(package, true);

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
