using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Forms;
using Skyve.App.UserInterface.Panels;
using Skyve.Domain.CS2.Utilities;

using System.Windows.Forms;

namespace Skyve.App.CS2.Services;
internal class CustomPackageService : ICustomPackageService
{
	public SlickStripItem[] GetRightClickMenuItems(IPackageIdentity item)
	{
		return GetRightClickMenuItems(new[] { item });
	}

	public SlickStripItem[] GetRightClickMenuItems(IEnumerable<IPackageIdentity> items)
	{
		var list = items.ToList();
		var isInstalled = list.Any(item => item.GetLocalPackage() is not null);
		var isLocal = isInstalled && list.Any(item => item.IsLocal());

		var packageUtil = ServiceCenter.Get<IPackageUtil>();
		var packageManager = ServiceCenter.Get<IPackageManager>();
		var subscriptionManager = ServiceCenter.Get<ISubscriptionsManager>();
		var profileManager = ServiceCenter.Get<IPlaysetManager>();
		var compatibilityManager = ServiceCenter.Get<ICompatibilityManager>();
		var userService = ServiceCenter.Get<IUserService>();
		var settings = ServiceCenter.Get<ISettings>();



		return
		[
			 new (LocaleCS2.RemoveFromPlayset.FormatPlural(list.Count), "I_Steam", isInstalled && !isLocal, action: () => subscriptionManager.UnSubscribe(list.Cast<IPackageIdentity>()))
			, new (Locale.MovePackageToLocalFolder.FormatPlural(list.Count), "I_PC", settings.UserSettings.ExtendedListInfo && isInstalled && !isLocal, action: () => list.SelectWhereNotNull(x => x.GetLocalPackage()).Foreach(x => packageManager.MoveToLocalFolder(x!.Package)))
			, new ((isLocal && list[0] is IAsset ? Locale.DeleteAsset : Locale.DeletePackage).FormatPlural(list.Count), "I_Disposable", isInstalled && isLocal, action: () => AskThenDelete(list))
			, new (string.Empty)
			, new (Locale.Manage, "I_ProfileSettings", fade: true)
			, new (Locale.EditTags.FormatPlural(list.Count), "I_Tag", isInstalled, tab: 1, action: () => EditTags(list))
			, new (Locale.EditCompatibility.FormatPlural(list.Count), "I_CompatibilityReport", userService.User.Manager || list.Any(item => userService.User.Equals(item.GetWorkshopInfo()?.Author)), tab: 1, action: () => { App.Program.MainForm.PushPanel(null, new PC_CompatibilityManagement(items.Select(x => x.Id)));})
			, new (Locale.OtherPlaysets, "I_ProfileSettings", fade: true)
			, new (Locale.IncludeThisItemInAllPlaysets.FormatPlural(list.Count), "I_Ok", tab: 1, action: () => { new BackgroundAction(() => list.SelectWhereNotNull(x => x.GetPackage()).Foreach(x => profileManager.SetIncludedForAll(x!, true))).Run(); packageUtil.SetIncluded(list.SelectWhereNotNull(x => x.GetLocalPackageIdentity())!, true); })
			, new (Locale.ExcludeThisItemInAllPlaysets.FormatPlural(list.Count), "I_Cancel", tab: 1, action: () => { new BackgroundAction(() => list.SelectWhereNotNull(x => x.GetPackage()).Foreach(x => profileManager.SetIncludedForAll(x!, false))).Run(); packageUtil.SetIncluded(list.SelectWhereNotNull(x => x.GetLocalPackageIdentity())!, false); })
			, new (Locale.Copy, "I_Copy", !isLocal, fade: true)
			, new (Locale.CopyWorkshopLink.FormatPlural(list.Count), null, !isLocal, tab: 1, action: () => Clipboard.SetText(list.ListStrings(x => x.Url, CrossIO.NewLine)))
			, new (Locale.CopyPackageName.FormatPlural(list.Count), !isLocal ? null : "I_Copy", tab: !isLocal ? 1 : 0, action: () => Clipboard.SetText(list.ListStrings(CrossIO.NewLine)))
			, new (Locale.CopyWorkshopId.FormatPlural(list.Count), null, !isLocal, tab: 1,  action: () => Clipboard.SetText(list.ListStrings(x => x.Id.ToString(), CrossIO.NewLine)))
		];
	}

	private static EditTagsForm EditTags(IEnumerable<IPackageIdentity> item)
	{
		var frm = new EditTagsForm(item);

		App.Program.MainForm.OnNextIdle(() =>
		{
			frm.Show(App.Program.MainForm);

			frm.ShowUp();
		});

		return frm;
	}

	private static void AskThenDelete<T>(IEnumerable<T> items) where T : IPackageIdentity
	{
		if (MessagePrompt.Show(Locale.AreYouSure + "\r\n\r\n" + Locale.ActionUnreversible.FormatPlural(items.Count()), PromptButtons.YesNo, form: App.Program.MainForm) == DialogResult.Yes)
		{
			foreach (var item in items.SelectWhereNotNull(x => x.GetLocalPackageIdentity()))
			{
				try
				{
					if (item!.IsLocal() && item is IAsset asset)
					{
						CrossIO.DeleteFile(asset.FilePath);
					}
					else if (item!.GetLocalPackage() is not null)
					{
						ServiceCenter.Get<IPackageManager>().DeleteAll(item!.GetLocalPackage().Folder);
					}
				}
				catch (Exception ex)
				{
					MessagePrompt.Show(ex, Locale.FailedToDeleteItem);
				}
			}
		}
	}
}
