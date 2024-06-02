using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Forms;
using Skyve.App.UserInterface.Panels;
using Skyve.App.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.Services;
internal class RightClickService : IRightClickService
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
		var modUtil = ServiceCenter.Get<IModUtil>();
		var modLogicManager = ServiceCenter.Get<IModLogicManager>();

		var anyIncluded = list.Any(x => packageUtil.IsIncluded(x));
		var anyExcluded = list.Any(x => !packageUtil.IsIncluded(x));
		var anyEnabled = list.Any(x => packageUtil.IsIncluded(x) && packageUtil.IsEnabled(x));
		var anyDisabled = list.Any(x => packageUtil.IsIncluded(x) && !packageUtil.IsEnabled(x));
		var anyInstalled = list.Any(x => x.GetLocalPackageIdentity() is not null);
		var anyLocal = list.Any(x => x.IsLocal());
		var anyWorkshop = list.Any(x => !x.IsLocal());
		var anyWorkshopAndInstalled = list.Any(x => !x.IsLocal() && x.GetLocalPackageIdentity() is not null);
		var anyNotRequired = list.Any(x => !modLogicManager.IsRequired(x.GetLocalPackageIdentity(), modUtil));

		return
		[
			new(Locale.ViewOnWorkshop, "Link", () => PlatformUtil.OpenUrl(list[0].Url), visible: list.Count == 1 && !string.IsNullOrWhiteSpace(list[0].Url)),
			new(Locale.OpenPackageFolder.FormatPlural(list.Count), "Folder", () => list.Select(x => x.GetLocalPackageIdentity()?.FilePath).WhereNotEmpty().Foreach(PlatformUtil.OpenFolder), visible: anyInstalled),
			new(Locale.MovePackageToLocalFolder.FormatPlural(list.Count), "PC", () => list.SelectWhereNotNull(x => x.GetLocalPackage()).Foreach(x => ServiceCenter.Get<IPackageManager>().MoveToLocalFolder(x!.Package)), visible: settings.UserSettings.ComplexListUI && anyWorkshopAndInstalled),

			SlickStripItem.Empty,

			new(Locale.Manage, "Wrench", disabled: true)
			{
				SubItems = [
					new(list.Count == 1 ? Locale.EnableItem : Locale.EnableAllSelected, "Ok", async () => await packageUtil.SetEnabled(items, true), visible: anyDisabled),
					new(list.Count == 1 ? Locale.DisableItem : Locale.DisableAllSelected, "Enabled", async () => await packageUtil.SetEnabled(items, false), visible: anyEnabled && anyNotRequired),
					new(list.Count == 1 ? Locale.IncludeItem : Locale.IncludeAllSelected, "Add", async () => await packageUtil.SetIncluded(items, true), visible: anyExcluded),
					new(list.Count == 1 ? Locale.ExcludeItem : Locale.ExcludeAllSelected, "X", async () => await packageUtil.SetIncluded(items, false), visible: anyIncluded && anyNotRequired),
					SlickStripItem.Empty,
					new(Locale.EditTags.FormatPlural(list.Count), "Tag", () => EditTags(list)),
					new(Locale.EditCompatibility.FormatPlural(list.Count), "CompatibilityReport", () => { App.Program.MainForm.PushPanel(new PC_CompatibilityManagement(items)); }, visible: (userService.User.Manager || list.Any(item => userService.User.Equals(item.GetWorkshopInfo()?.Author))) && anyWorkshop),
					SlickStripItem.Empty,
					new((anyLocal && list[0] is IAsset ? Locale.DeleteAsset : Locale.DeletePackage).FormatPlural(list.Count), "Trash", () => AskThenDelete(list), visible: anyLocal),
				]
			},

			new(Locale.OtherPlaysets, "Playsets", disabled: true, visible: anyWorkshop)
			{
				SubItems = [
					new(Locale.EnableThisItemInAllPlaysets.FormatPlural(list.Count), "Ok", async () => await ServiceCenter.Get<IPlaysetManager>().SetEnabledForAll(list, true)),
					new(Locale.DisableThisItemInAllPlaysets.FormatPlural(list.Count), "Enabled", async () => await ServiceCenter.Get<IPlaysetManager>().SetEnabledForAll(list, false)),
					new(Locale.IncludeThisItemInAllPlaysets.FormatPlural(list.Count), "Add", async () => await ServiceCenter.Get<IPlaysetManager>().SetIncludedForAll(list, true)),
					new(Locale.ExcludeThisItemInAllPlaysets.FormatPlural(list.Count), "X", async () => await ServiceCenter.Get<IPlaysetManager>().SetIncludedForAll(list, false)),
				]
			},

			new(Locale.CopyPackageName.FormatPlural(list.Count), "Copy", () => Clipboard.SetText(list.ListStrings(CrossIO.NewLine)), visible: !anyWorkshop),
			new(LocaleSlickUI.Copy, "Copy", disabled: true, visible: anyWorkshop)
			{
				SubItems = [
					new(Locale.CopyPackageName.FormatPlural(list.Count), () => Clipboard.SetText(list.ListStrings(x => x.CleanName(true), CrossIO.NewLine))),
					new(Locale.CopyWorkshopLink.FormatPlural(list.Count), () => Clipboard.SetText(list.ListStrings(x => x.Url, CrossIO.NewLine))),
					new(Locale.CopyWorkshopId.FormatPlural(list.Count), () => Clipboard.SetText(list.ListStrings(x => x.Id.ToString(), CrossIO.NewLine)))
				]
			},
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

	public SlickStripItem[] GetRightClickMenuItems(IPlayset playset, bool isLocal)
	{
		var isCurrent = playset == ServiceCenter.Get<IPlaysetManager>().CurrentPlayset;
		var customPlayset = playset.GetCustomPlayset();

		return [
			new(Locale.ActivatePlayset, "Check", action: () => ActivatePlayset(playset), visible: isLocal && !isCurrent),
			new(Locale.OpenPlaysetPage, "PlaysetSettings", () => OpenPlaysetPage(playset), visible: isLocal),
			SlickStripItem.Empty,
			new(Locale.BulkActions, "Actions", visible: isLocal && !isCurrent, disabled: true)
			{
				SubItems = [
					new(Locale.PlaysetMerge, "Merge", action: () => MergePlayset(playset)),
					new(Locale.PlaysetExclude, "Exclude", action: () => ExcludePlayset(playset)),
				]
			},
			new(Locale.Manage, "Wrench", visible: isLocal, disabled: true)
			{
				SubItems = [
					new(customPlayset.IsFavorite ? Locale.UnFavoriteThisPlayset : Locale.FavoriteThisPlayset, "Star", () => TogglePlaysetFavorite(playset)),
					new(Locale.ChangePlaysetColor, "Paint", () => ChangeColor(playset)),
					new(Locale.EditPlaysetThumbnail, "EditImage", () => ChangeThumbnail(playset)),
					SlickStripItem.Empty,
					new(Locale.ResetPlaysetColor, "Select", () => ResetColor(playset), visible: customPlayset.Color.HasValue),
					new(Locale.ResetPlaysetImage, "RemoveImage", () => ResetThumbnail(playset), visible: customPlayset.IsCustomThumbnailSet),
				]
			},
			SlickStripItem.Empty,
			new(Locale.PlaysetDelete, "Trash", () => DeletePlayset(playset))
		];
	}

	private async void ActivatePlayset(IPlayset playset)
	{
		await ServiceCenter.Get<IPlaysetManager>().ActivatePlayset(playset);
	}

	private async void ExcludePlayset(IPlayset playset)
	{
		await ServiceCenter.Get<IPlaysetManager>().ExcludeFromCurrentPlayset(playset);
	}

	private async void MergePlayset(IPlayset playset)
	{
		await ServiceCenter.Get<IPlaysetManager>().MergeIntoCurrentPlayset(playset);
	}

	private async void DeletePlayset(IPlayset playset)
	{
		await ServiceCenter.Get<IPlaysetManager>().DeletePlayset(playset);
	}

	private void ChangeColor(IPlayset playset)
	{
		App.Program.MainForm.TryBeginInvoke(() =>
		{
			var customPlayset = playset.GetCustomPlayset();
			var colorDialog = new SlickColorPicker(customPlayset.Color ?? FormDesign.Design.ActiveColor);

			if (colorDialog.ShowDialog(App.Program.MainForm) != DialogResult.OK)
			{
				return;
			}

			customPlayset.Color = colorDialog.Color;

			ServiceCenter.Get<IPlaysetManager>().Save(customPlayset);

			ServiceCenter.Get<INotifier>().OnRefreshUI(true);
		});
	}

	private void ChangeThumbnail(IPlayset playset)
	{
		App.Program.MainForm.TryBeginInvoke(() =>
		{
			var imagePrompt = new IOSelectionDialog
			{
				ValidExtensions = IO.ImageExtensions
			};

			if (imagePrompt.PromptFile(App.Program.MainForm) == DialogResult.OK)
			{
				try
				{
					var customPlayset = playset.GetCustomPlayset();

					customPlayset.SetThumbnail(Image.FromFile(imagePrompt.SelectedPath));

					ServiceCenter.Get<IPlaysetManager>().Save(customPlayset);
					ServiceCenter.Get<INotifier>().OnRefreshUI(true);
				}
				catch { }
			}
		});
	}

	private void ResetColor(IPlayset playset)
	{
		var customPlayset = playset.GetCustomPlayset();

		customPlayset.Color = null;

		ServiceCenter.Get<IPlaysetManager>().Save(customPlayset);
		ServiceCenter.Get<INotifier>().OnRefreshUI(true);
	}

	private void ResetThumbnail(IPlayset playset)
	{
		var customPlayset = playset.GetCustomPlayset();

		customPlayset.SetThumbnail(null);

		ServiceCenter.Get<IPlaysetManager>().Save(customPlayset);
		ServiceCenter.Get<INotifier>().OnRefreshUI(true);
	}

	private void TogglePlaysetFavorite(IPlayset playset)
	{
		var customPlayset = playset.GetCustomPlayset();

		customPlayset.IsFavorite = !customPlayset.IsFavorite;

		ServiceCenter.Get<IPlaysetManager>().Save(customPlayset);
	}

	private void OpenPlaysetPage(IPlayset playset)
	{
		try
		{
			ServiceCenter.Get<IAppInterfaceService>().OpenPlaysetPage(playset);
		}
		catch (Exception ex)
		{
			App.Program.MainForm.TryInvoke(() => MessagePrompt.Show(ex, Locale.FailedToDownloadPlayset, form: App.Program.MainForm));
		}
	}
}
