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
			new(Locale.ViewOnWorkshop, "I_Link", () => PlatformUtil.OpenUrl(list[0].Url), visible: list.Count == 1 && !string.IsNullOrWhiteSpace(list[0].Url)),
			new(Locale.OpenPackageFolder.FormatPlural(list.Count), "I_Folder", () => list.Select(x => x.GetLocalPackageIdentity()?.FilePath).WhereNotEmpty().Foreach(PlatformUtil.OpenFolder), visible: anyInstalled),
			new(Locale.MovePackageToLocalFolder.FormatPlural(list.Count), "I_PC", () => list.SelectWhereNotNull(x => x.GetLocalPackage()).Foreach(x => ServiceCenter.Get<IPackageManager>().MoveToLocalFolder(x!.Package)), visible: settings.UserSettings.ComplexListUI && anyWorkshopAndInstalled),
			new((anyLocal && list[0] is IAsset ? Locale.DeleteAsset : Locale.DeletePackage).FormatPlural(list.Count), "I_Disposable", () => AskThenDelete(list), visible: settings.UserSettings.ComplexListUI && anyLocal),

			SlickStripItem.Empty,

			new(Locale.Manage, "I_Wrench", disabled: true)
			{
				SubItems = [
					new(list.Count == 1 ? Locale.EnableItem : Locale.EnableAllSelected, "I_Ok", async () => await packageUtil.SetEnabled(items, true), visible: anyDisabled),
					new(list.Count == 1 ? Locale.DisableItem : Locale.DisableAllSelected, "I_Enabled", async () => await packageUtil.SetEnabled(items, false), visible: anyEnabled && anyNotRequired),
					new(list.Count == 1 ? Locale.IncludeItem : Locale.IncludeAllSelected, "I_Add", async () => await packageUtil.SetIncluded(items, true), visible: anyExcluded),
					new(list.Count == 1 ? Locale.ExcludeItem : Locale.ExcludeAllSelected, "I_X", async () => await packageUtil.SetIncluded(items, false), visible: anyIncluded && anyNotRequired),
					SlickStripItem.Empty,
					new(Locale.EditTags.FormatPlural(list.Count), "I_Tag", () => EditTags(list)),
					new(Locale.EditCompatibility.FormatPlural(list.Count), "I_CompatibilityReport", () => { App.Program.MainForm.PushPanel(null, new PC_CompatibilityManagement(items.Select(x => x.Id))); }, visible: (userService.User.Manager || list.Any(item => userService.User.Equals(item.GetWorkshopInfo()?.Author))) && anyWorkshop),
				]
			},

			new(Locale.OtherPlaysets, "I_Playsets", disabled: true, visible: anyWorkshop)
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

	public SlickStripItem[] GetRightClickMenuItems(IPlayset playset, bool isLocal)
	{
		var isCurrent = playset == ServiceCenter.Get<IPlaysetManager>().CurrentPlayset;
		var customPlayset = playset.GetCustomPlayset();

		return [
			new(Locale.ViewThisPlaysetsPackages, "I_ViewFile", () => OpenPlaysetPage(playset)),
			new(Locale.ChangePlaysetSettings, "I_PlaysetSettings", action: () => OpenPlaysetSettings(playset), visible: isLocal),
			new(Locale.ActivatePlayset, "I_Check", action: () => ActivatePlayset(playset), visible: isLocal && !isCurrent),
			SlickStripItem.Empty,
			new(Locale.BulkActions, "I_Actions", visible: isLocal && !isCurrent, disabled: true)
			{
				SubItems = [
					new(Locale.PlaysetMerge, "I_Merge", action: () => MergePlayset(playset)),
					new(Locale.PlaysetExclude, "I_Exclude", action: () => ExcludePlayset(playset)),
				]
			},
			new(Locale.Manage, "I_Wrench", visible: isLocal, disabled: true)
			{
				SubItems = [
					new(customPlayset.IsFavorite ? Locale.UnFavoriteThisPlayset : Locale.FavoriteThisPlayset, "I_Star", () => TogglePlaysetFavorite(playset)),
					new(Locale.ChangePlaysetColor, "I_Paint", () => ChangeColor(playset)),
					new(Locale.EditPlaysetThumbnail, "I_EditImage", () => ChangeThumbnail(playset)),
					SlickStripItem.Empty,
					new(Locale.ResetPlaysetImage, "I_Select", () => ResetColor(playset), visible: customPlayset.Color.HasValue),
					new(Locale.ResetPlaysetImage, "I_RemoveImage", () => ResetThumbnail(playset), visible: customPlayset.IsCustomThumbnailSet),
				]
			},
			SlickStripItem.Empty,
			new(Locale.PlaysetDelete, "I_Disposable", () => DeletePlayset(playset))
		];

		//var items = new SlickStripItem[]
		//{
		//	  new (Locale.DownloadPlayset, "I_Install", !local, action: () => DownloadProfile(item))
		//	, new (Locale.ViewThisPlaysetsPackages, "I_ViewFile", action: () => ShowProfileContents(item))
		////	, new (item.IsFavorite ? Locale.UnFavoriteThisPlayset : Locale.FavoriteThisPlayset, "I_Star", local, action: () => { item.IsFavorite = !item.IsFavorite; _profileManager.Save(item); })
		//	, new (Locale.ChangePlaysetColor, "I_Paint", local, action: () => this.TryBeginInvoke(() => ChangeColor(item)))
		//	, new (Locale.CreateShortcutPlayset, "I_Link", local && CrossIO.CurrentPlatform is Platform.Windows, action: () => _profileManager.CreateShortcut(item!))
		//	, new (Locale.OpenPlaysetFolder, "I_Folder", local, action: () => PlatformUtil.OpenFolder(_profileManager.GetFileName(item!)))
		//	, new (string.Empty, show: local)
		//	, new (Locale.SharePlayset, "I_Share", local && item.ProfileId == 0 && _userService.User.Id is not null && downloading != item, action: async () => await ShareProfile(item))
		//	, new (item.Public ? Locale.MakePrivate : Locale.MakePublic, item.Public ? "I_UserSecure" : "I_People", local && item.ProfileId != 0 && _userService.User.Equals(item.Author), action: async () => await ServiceCenter.Get<IOnlinePlaysetUtil>().SetVisibility((item as IPlayset)!, !item.Public))
		//	, new (Locale.UpdatePlayset, "I_Share", local && item.ProfileId != 0 && _userService.User.Equals(item.Author), action: async () => await ShareProfile(item))
		//	, new (Locale.DownloadPlayset, "I_Refresh", local && item.ProfileId != 0 && item.Author != _userService.User.Id, action: () => DownloadProfile(item))
		//	, new (Locale.CopyPlaysetLink, "I_LinkChain", local && item.ProfileId != 0, action: () => Clipboard.SetText(IdHasher.HashToShortString(item.ProfileId)))
		//	, new (string.Empty, show: local)
		//	, new (Locale.ActivatePlayset, "I_Install", local, action: () => LoadProfile?.Invoke(item!))
		//	, new (Locale.PlaysetMerge, "I_Merge", local, action: () => MergeProfile?.Invoke(item!))
		//	, new (Locale.PlaysetExclude, "I_Exclude", local, action: () => ExcludeProfile?.Invoke(item!))
		//	, new (string.Empty)
		//	, new (Locale.PlaysetDelete, "I_Disposable", local || _userService.User.Equals(item.Author), action: async () => { if(local) { DisposeProfile?.Invoke(item!); } else if(await ServiceCenter.Get<IOnlinePlaysetUtil>().DeleteOnlinePlayset((item as IOnlinePlayset)!)) { base.Remove(item); } })
		//};
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
			App.Program.MainForm.PushPanel(new PC_PlaysetContents(playset));
		}
		catch (Exception ex)
		{
			App.Program.MainForm.TryInvoke(() => MessagePrompt.Show(ex, Locale.FailedToDownloadPlayset, form: App.Program.MainForm));
		}
	}

	private void OpenPlaysetSettings(IPlayset playset)
	{
		try
		{
			App.Program.MainForm.PushPanel(ServiceCenter.Get<IAppInterfaceService>().PlaysetSettingsPanel(playset));
		}
		catch (Exception ex)
		{
			App.Program.MainForm.TryInvoke(() => MessagePrompt.Show(ex, Locale.FailedToDownloadPlayset, form: App.Program.MainForm));
		}
	}
}
