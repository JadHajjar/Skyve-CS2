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
		return GetRightClickMenuItems([item]);
	}

	public SlickStripItem[] GetRightClickMenuItems(IEnumerable<IPackageIdentity> items)
	{
		var list = items.ToList();

		var packageUtil = ServiceCenter.Get<IPackageUtil>();
		var userService = ServiceCenter.Get<IUserService>();
		var settings = ServiceCenter.Get<ISettings>();

		var anyIncluded = list.Any(x => packageUtil.IsIncluded(x));
		var anyExcluded = list.Any(x => !packageUtil.IsIncluded(x));
		var anyEnabled = list.Any(x => packageUtil.IsIncluded(x) && packageUtil.IsEnabled(x));
		var anyDisabled = list.Any(x => packageUtil.IsIncluded(x) && !packageUtil.IsEnabled(x));
		var anyInstalled = list.Any(x => x.GetLocalPackageIdentity() is not null);
		var anyLocal = list.Any(x => x.IsLocal());
		var anyWorkshop = list.Any(x => x.IsLocal());
		var anyWorkshopAndInstalled = list.Any(x => !x.IsLocal() && x.GetLocalPackageIdentity() is not null);

		return
		[
			new(LocaleCS2.ViewOnParadoxMods, "I_Link", list.Count == 1 && !string.IsNullOrWhiteSpace(list[0].Url), action: () => PlatformUtil.OpenUrl(list[0].Url)),
			new(LocaleCS2.OpenPackageFolder.FormatPlural(list.Count), "I_Folder", anyInstalled, action: () => list.Select(x => x.GetLocalPackageIdentity()?.FilePath).WhereNotEmpty().Foreach(PlatformUtil.OpenFolder)),
			new(Locale.MovePackageToLocalFolder.FormatPlural(list.Count), "I_PC", settings.UserSettings.ExtendedListInfo && anyWorkshopAndInstalled, action: () => list.SelectWhereNotNull(x => x.GetLocalPackage()).Foreach(x => ServiceCenter.Get<IPackageManager>().MoveToLocalFolder(x!.Package))),
			new((anyLocal && list[0] is IAsset ? Locale.DeleteAsset : Locale.DeletePackage).FormatPlural(list.Count), "I_Disposable", settings.UserSettings.ExtendedListInfo && anyLocal, action: () => AskThenDelete(list)),
			new(string.Empty),

			new(Locale.Manage, "I_Wrench", fade: true),
			new(Locale.EnableAll, "I_Ok", list.Count == 1 && anyDisabled, tab: 1, action: async () => await packageUtil.SetEnabled(items, true)),
			new(Locale.DisableAll, "I_Enabled", list.Count == 1 && anyEnabled, tab: 1, action: async () => await packageUtil.SetEnabled(items, false)),
			new(Locale.IncludeAll, "I_Add", list.Count == 1 && anyExcluded, tab: 1, action: async () => await packageUtil.SetIncluded(items, true)),
			new(Locale.ExcludeAll, "I_X", list.Count == 1 && anyIncluded, tab: 1, action: async () => await packageUtil.SetIncluded(items, false)),
			new(Locale.EditTags.FormatPlural(list.Count), "I_Tag", tab: 1, action: () => EditTags(list)),
			new(Locale.EditCompatibility.FormatPlural(list.Count), "I_CompatibilityReport", userService.User.Manager || list.Any(item => userService.User.Equals(item.GetWorkshopInfo()?.Author)), tab: 1, action: () => { App.Program.MainForm.PushPanel(null, new PC_CompatibilityManagement(items.Select(x => x.Id))); }),

			new(Locale.OtherPlaysets, "I_Playsets", fade: true),
			new(Locale.IncludeThisItemInAllPlaysets.FormatPlural(list.Count), "I_Ok", tab: 1, action: async () => await ServiceCenter.Get<IPlaysetManager>().SetEnabledForAll(list, true)),
			new(Locale.ExcludeThisItemInAllPlaysets.FormatPlural(list.Count), "I_Enabled", tab: 1, action: async () => await ServiceCenter.Get<IPlaysetManager>().SetEnabledForAll(list, false)),
			new(Locale.IncludeThisItemInAllPlaysets.FormatPlural(list.Count), "I_Add", tab: 1, action: async () => await ServiceCenter.Get<IPlaysetManager>().SetIncludedForAll(list, true)),
			new(Locale.ExcludeThisItemInAllPlaysets.FormatPlural(list.Count), "I_X", tab: 1, action: async () => await ServiceCenter.Get<IPlaysetManager>().SetIncludedForAll(list, false)),

			new(Locale.Copy, "I_Copy", anyWorkshop, fade: true),
			new(Locale.CopyWorkshopLink.FormatPlural(list.Count), null, anyWorkshop, tab: 1, action: () => Clipboard.SetText(list.ListStrings(x => x.Url, CrossIO.NewLine))),
			new(Locale.CopyPackageName.FormatPlural(list.Count), anyWorkshop ? null : "I_Copy", tab: anyWorkshop ? 1 : 0, action: () => Clipboard.SetText(list.ListStrings(CrossIO.NewLine))),
			new(Locale.CopyWorkshopId.FormatPlural(list.Count), null, anyWorkshop, tab: 1, action: () => Clipboard.SetText(list.ListStrings(x => x.Id.ToString(), CrossIO.NewLine)))
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
						ServiceCenter.Get<IPackageManager>().DeleteAll(item!.Folder);
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
