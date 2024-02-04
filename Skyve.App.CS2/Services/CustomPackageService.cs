using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Forms;
using Skyve.App.UserInterface.Panels;

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
		var anyWorkshop = list.Any(x => !x.IsLocal());
		var anyWorkshopAndInstalled = list.Any(x => !x.IsLocal() && x.GetLocalPackageIdentity() is not null);

		return
		[
			new(Locale.ViewOnWorkshop, "I_Link", () => PlatformUtil.OpenUrl(list[0].Url), visible: list.Count == 1 && !string.IsNullOrWhiteSpace(list[0].Url)),
			new(Locale.OpenPackageFolder.FormatPlural(list.Count), "I_Folder", () => list.Select(x => x.GetLocalPackageIdentity()?.FilePath).WhereNotEmpty().Foreach(PlatformUtil.OpenFolder), visible: anyInstalled),
			new(Locale.MovePackageToLocalFolder.FormatPlural(list.Count), "I_PC", () => list.SelectWhereNotNull(x => x.GetLocalPackage()).Foreach(x => ServiceCenter.Get<IPackageManager>().MoveToLocalFolder(x!.Package)), visible: settings.UserSettings.ComplexListUI && anyWorkshopAndInstalled),
			new((anyLocal && list[0] is IAsset ? Locale.DeleteAsset : Locale.DeletePackage).FormatPlural(list.Count), "I_Disposable", () => AskThenDelete(list), visible: settings.UserSettings.ComplexListUI && anyLocal),

			SlickStripItem.Empty,

			new(Locale.Manage, "I_Wrench", disabled: true)
			{
				SubItems = [
					new(Locale.EnableItem, "I_Ok", async () => await packageUtil.SetEnabled(items, true), visible: list.Count == 1 && anyDisabled),
					new(Locale.DisableItem, "I_Enabled", async () => await packageUtil.SetEnabled(items, false), visible: list.Count == 1 && anyEnabled),
					new(Locale.IncludeItem, "I_Add", async () => await packageUtil.SetIncluded(items, true), visible: list.Count == 1 && anyExcluded),
					new(Locale.ExcludeItem, "I_X", async () => await packageUtil.SetIncluded(items, false), visible: list.Count == 1 && anyIncluded),
					SlickStripItem.Empty,
					new(Locale.EditTags.FormatPlural(list.Count), "I_Tag", () => EditTags(list)),
					new(Locale.EditCompatibility.FormatPlural(list.Count), "I_CompatibilityReport", () => { App.Program.MainForm.PushPanel(null, new PC_CompatibilityManagement(items.Select(x => x.Id))); }, visible: userService.User.Manager || list.Any(item => userService.User.Equals(item.GetWorkshopInfo()?.Author))),
				]
			},

			new(Locale.OtherPlaysets, "I_Playsets", disabled: true)
			{
				SubItems = [
					new(Locale.EnableThisItemInAllPlaysets.FormatPlural(list.Count), "I_Ok", async () => await ServiceCenter.Get<IPlaysetManager>().SetEnabledForAll(list, true)),
					new(Locale.DisableThisItemInAllPlaysets.FormatPlural(list.Count), "I_Enabled", async () => await ServiceCenter.Get<IPlaysetManager>().SetEnabledForAll(list, false)),
					new(Locale.IncludeThisItemInAllPlaysets.FormatPlural(list.Count), "I_Add", async () => await ServiceCenter.Get<IPlaysetManager>().SetIncludedForAll(list, true)),
					new(Locale.ExcludeThisItemInAllPlaysets.FormatPlural(list.Count), "I_X", async () => await ServiceCenter.Get<IPlaysetManager>().SetIncludedForAll(list, false)),
				]
			},

			new(Locale.CopyPackageName.FormatPlural(list.Count), "I_Copy", () => Clipboard.SetText(list.ListStrings(CrossIO.NewLine)), visible: !anyWorkshop),
			new(LocaleSlickUI.Copy, "I_Copy", disabled: true, visible: anyWorkshop)
			{
				SubItems = [
					new(Locale.CopyPackageName.FormatPlural(list.Count), () => Clipboard.SetText(list.ListStrings(CrossIO.NewLine))),
					new(Locale.CopyWorkshopLink.FormatPlural(list.Count), () => Clipboard.SetText(list.ListStrings(x => x.Url, CrossIO.NewLine))),
					new(Locale.CopyWorkshopId.FormatPlural(list.Count), () => Clipboard.SetText(list.ListStrings(x => x.Id.ToString(), CrossIO.NewLine)))
				]
			}
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
