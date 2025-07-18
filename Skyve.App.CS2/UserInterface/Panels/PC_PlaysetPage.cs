﻿using Skyve.App.CS2.UserInterface.Generic;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Generic;
using Skyve.App.UserInterface.Panels;
using Skyve.App.Utilities;
using Skyve.Domain.CS2.Content;

using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PlaysetPage : PlaysetSettingsPanel
{
	private readonly bool loadingPlayset;
	private readonly bool _editName;
	private readonly IOSelectionDialog imagePrompt;

	private readonly IPlaysetManager _playsetManager;
	private readonly ISettings _settings;
	private readonly IPackageUtil _packageUtil;
	private readonly INotifier _notifier;

	public IPlayset Playset { get; set; }
	public ContentList LC_Items { get; }
	public PlaysetSideControl SideControl { get; }

	public PC_PlaysetPage(IPlayset playset, bool settingsTab, bool editName) : base(true)
	{
		Playset = playset;

		ServiceCenter.Get(out _packageUtil, out _playsetManager, out _notifier, out _settings);

		InitializeComponent();

		_editName = editName;
		imagePrompt = new IOSelectionDialog
		{
			ValidExtensions = IO.ImageExtensions
		};

		T_Content.LinkedControl = LC_Items = new ContentList(SkyvePage.PlaysetPackages, false, GetContents, () => Locale.Package);

		LC_Items.SelectedPlayset = playset.Id;

		P_Side.Controls.Add(SideControl = new PlaysetSideControl(playset) { Dock = DockStyle.Top });

		foreach (var item in this.GetControls<SlickCheckbox>())
		{
			if (item.Parent != TLP_AdvancedDev)
			{
				SlickTip.SetTo(item, item.Text + "_Tip");
			}
		}

		B_Deactivate.Visible = _playsetManager.CurrentPlayset == playset;

		var customPlayset = playset.GetCustomPlayset();

		FLP_Usage.Controls.Add(new PlaysetUsageSelection((PackageUsage)(-1)) { Selected = customPlayset.Usage <= 0 });
		foreach (PackageUsage item in Enum.GetValues(typeof(PackageUsage)))
		{
			FLP_Usage.Controls.Add(new PlaysetUsageSelection(item) { Selected = customPlayset.Usage == item });
		}

		foreach (PlaysetUsageSelection item in FLP_Usage.Controls)
		{
			item.SelectedChanged += PlaysetUsage_SelectedValueChanged;
		}

		T_LaunchSettings.PreSelected = settingsTab;

		TLP_AdvancedDev.Visible = _settings.UserSettings.AdvancedLaunchOptions;

		TLP_Options.Enabled = true;
		CB_NoBanner.Checked = customPlayset.NoBanner;

		I_Color.Visible = B_ClearColor.Visible = customPlayset.Color.HasValue;
		L_ColorInfo.Text = customPlayset.Color.HasValue ? Locale.PlaysetColorSet : Locale.PlaysetColorNotSet;
		I_Thumbnail.Visible = B_ClearThumbnail.Visible = customPlayset.IsCustomThumbnailSet;
		L_ThumbnailInfo.Text = customPlayset.IsCustomThumbnailSet ? Locale.PlaysetThumbnailSet : Locale.PlaysetThumbnailNotSet;

		if (customPlayset is ExtendedPlayset extendedPlayset)
		{
			CB_HideUserSection.Checked = extendedPlayset.LaunchSettings.HideUserSection;
			CB_NoAssets.Checked = extendedPlayset.LaunchSettings.NoAssets;
			CB_NoMods.Checked = extendedPlayset.LaunchSettings.NoMods;
			CB_LoadSave.Checked = extendedPlayset.LaunchSettings.LoadSaveGame;
			CB_StartNewGame.Checked = extendedPlayset.LaunchSettings.StartNewGame;
			DD_NewMap.SelectedFile = extendedPlayset.LaunchSettings.MapToLoad;
			DD_SaveFile.SelectedFile = extendedPlayset.LaunchSettings.SaveToLoad;

			CB_LogsToPlayerLog.Checked = extendedPlayset.LaunchSettings.LogsToPlayerLog;
			CB_DeveloperMode.Checked = extendedPlayset.LaunchSettings.DeveloperMode;
			CB_UIDeveloperMode.Checked = extendedPlayset.LaunchSettings.UIDeveloperMode;
			CB_UseCitiesExe.Checked = extendedPlayset.LaunchSettings.UseCitiesExe;
			CB_DisableBurstCompile.Checked = extendedPlayset.LaunchSettings.DisableBurstCompile;
			DD_LogLevel.SelectedItem = extendedPlayset.LaunchSettings.LogLevel.IfEmpty("DEFAULT");
			TB_CustomArgs.Text = extendedPlayset.LaunchSettings.CustomArgs;
		}

		_notifier.PlaysetChanged += Notifier_PlaysetChanged;
		_notifier.PlaysetUpdated += Notifier_PlaysetUpdated;
	}

	private void Notifier_PlaysetUpdated()
	{
		Playset = _playsetManager.GetPlayset(Playset.Id) ?? Playset;

		SideControl.Playset = Playset;
		SideControl.Invalidate();
	}

	private void Notifier_PlaysetChanged()
	{
		SideControl.Invalidate();
		B_Deactivate.Visible = _playsetManager.CurrentPlayset == Playset;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (_editName)
		{
			Form.OnNextIdle(EditName);
		}
	}

	protected override async Task<bool> LoadDataAsync()
	{
		await LC_Items.RefreshItems();

		return true;
	}

	private async Task<IEnumerable<IPackageIdentity>> GetContents(CancellationToken cancellationToken)
	{
		var items = await _playsetManager.GetPlaysetContents(Playset);

		return items.Cast<IPackageIdentity>();
	}

	protected override void LocaleChanged()
	{
		L_Usage.Text = Locale.PlaysetUsage;
		L_UsageInfo.Text = Locale.PlaysetUsageInfo;
		L_Color.Text = Locale.CustomColor;
		L_Thumbnail.Text = Locale.CustomThumbnail;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		TLP_Options.Padding = TLP_Side.Padding = UI.Scale(new Padding(10, 0, 10, 10), UI.UIScale);
		TLP_AdvancedDev.Margin = UI.Scale(new Padding(0, 15, 0, 0), UI.UIScale);

		P_Side.Width = UI.Scale(250);
		P_Side.Padding = UI.Scale(new Padding(15, 0, 15, 15));
		slickSpacer1.Margin = B_EditThumbnail.Margin = B_EditColor.Margin = B_ClearThumbnail.Margin = B_ClearColor.Margin = UI.Scale(new Padding(5));
		B_Deactivate.Margin = B_Share.Margin = B_Delete.Margin = UI.Scale(new Padding(17, 12, 17, 0));
		B_Deactivate.Padding = B_Share.Padding = B_Delete.Padding = UI.Scale(new Padding(5));
		B_Deactivate.Font = B_Share.Font = B_Delete.Font = UI.Font(9F);
		slickSpacer1.Height = (int)UI.FontScale;

		I_Color.Size = I_Thumbnail.Size = UI.Scale(new Size(24, 24));
		I_Color.Padding = I_Thumbnail.Padding = UI.Scale(new Padding(4));
		I_Color.Margin = I_Thumbnail.Margin = UI.Scale(new Padding(3));

		L_Usage.Font = L_Thumbnail.Font = L_Color.Font = UI.Font(9F, FontStyle.Bold);
		L_Usage.Margin = L_Thumbnail.Margin = L_Color.Margin = UI.Scale(new Padding(3, 20, 3, 3));
		L_UsageInfo.Font = L_ThumbnailInfo.Font = L_ColorInfo.Font = UI.Font(8F);
		L_UsageInfo.Margin = L_ThumbnailInfo.Margin = L_ColorInfo.Margin = UI.Scale(new Padding(3, 3, 3, 3));
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP_AdvancedDev.BackColor = design.BackColor.MergeColor(design.RedColor, 95);
		I_Thumbnail.ForeColor = I_Color.ForeColor = L_UsageInfo.ForeColor = L_ThumbnailInfo.ForeColor = L_ColorInfo.ForeColor = design.InfoColor;
	}

	internal void Ctrl_LoadPlayset(IPlayset obj)
	{
		TLP_Options.Enabled = false;
		_playsetManager.ActivatePlayset(obj);
	}

	private void ValueChanged(object sender, EventArgs e)
	{
		if (loadingPlayset || !Live)
		{
			return;
		}

		var customPlayset = Playset.GetCustomPlayset();

		customPlayset.Usage = FLP_Usage.Controls.OfType<PlaysetUsageSelection>().FirstOrAny(x => x.Selected).Usage;
		customPlayset.NoBanner = CB_NoBanner.Checked;

		if (customPlayset is ExtendedPlayset extendedPlayset)
		{
			extendedPlayset.LaunchSettings = new Domain.CS2.Game.GameLaunchOptions
			{
				HideUserSection = CB_HideUserSection.Checked,
				NoAssets = CB_NoAssets.Checked,
				NoMods = CB_NoMods.Checked,
				LoadSaveGame = CB_LoadSave.Checked,
				StartNewGame = CB_StartNewGame.Checked,
				MapToLoad = DD_NewMap.SelectedFile,
				SaveToLoad = DD_SaveFile.SelectedFile,

				LogsToPlayerLog = CB_LogsToPlayerLog.Checked,
				DeveloperMode = CB_DeveloperMode.Checked,
				UIDeveloperMode = CB_UIDeveloperMode.Checked,
				UseCitiesExe = CB_UseCitiesExe.Checked,
				DisableBurstCompile = CB_DisableBurstCompile.Checked,
				LogLevel = DD_LogLevel.SelectedItem,
				CustomArgs = TB_CustomArgs.Text,
			};
		}

		_playsetManager.Save(customPlayset);

		SideControl.Invalidate();
	}

	public override void EditName()
	{
		SideControl.EditName();
	}

	private async void PlaysetUsage_SelectedValueChanged(object sender, EventArgs e)
	{
		if (loadingPlayset)
		{
			return;
		}

		var usage = FLP_Usage.Controls.OfType<PlaysetUsageSelection>().FirstOrAny(x => x.Selected).Usage;
		var invalidPackages = _playsetManager.GetInvalidPackages(Playset, usage);

		if (invalidPackages.Any())
		{
			if (ShowPrompt($"{Locale.SomePackagesWillBeDisabled}\r\n\r\n{Locale.AffectedPackagesAre}\r\n• {invalidPackages.Take(8).ListStrings("\r\n• ")}", PromptButtons.OKCancel, PromptIcons.Warning) == DialogResult.Cancel)
			{
				FLP_Usage.Controls.OfType<PlaysetUsageSelection>().Foreach(x =>
				{
					x.Selected = x.Usage == (PackageUsage)(-1);
					x.Invalidate();
				});
			}
			else
			{
				await _packageUtil.SetIncluded(invalidPackages, false);
			}
		}

		ValueChanged(sender, e);
	}

	public override void LoadPlayset(IPlayset customPlayset)
	{
		Ctrl_LoadPlayset(customPlayset);
	}

	private void DD_SaveFile_FileSelected(string obj)
	{

	}

	private void DD_NewMap_FileSelected(string obj)
	{

	}

	private void B_EditColor_Click(object sender, EventArgs e)
	{
		var customPlayset = Playset.GetCustomPlayset();
		var colorDialog = new SlickColorPicker(customPlayset.Color ?? FormDesign.Design.ActiveColor);

		if (colorDialog.ShowDialog(App.Program.MainForm) != DialogResult.OK)
		{
			return;
		}

		customPlayset.Color = colorDialog.Color;

		_playsetManager.Save(customPlayset);

		I_Color.Visible = B_ClearColor.Visible = customPlayset.Color.HasValue;
		L_ColorInfo.Text = customPlayset.Color.HasValue ? Locale.PlaysetColorSet : Locale.PlaysetColorNotSet;

		SideControl.Invalidate();
	}

	private void B_EditThumbnail_Click(object sender, EventArgs e)
	{
		if (imagePrompt.PromptFile(Form) == DialogResult.OK)
		{
			try
			{
				var customPlayset = Playset.GetCustomPlayset();

				customPlayset.SetThumbnail(Image.FromFile(imagePrompt.SelectedPath));

				_playsetManager.Save(customPlayset);

				I_Thumbnail.Visible = B_ClearThumbnail.Visible = customPlayset.IsCustomThumbnailSet;
				L_ThumbnailInfo.Text = customPlayset.IsCustomThumbnailSet ? Locale.PlaysetThumbnailSet : Locale.PlaysetThumbnailNotSet;

				SideControl.Invalidate();
			}
			catch { }
		}
	}

	private void B_ClearThumbnail_Click(object sender, EventArgs e)
	{
		var customPlayset = Playset.GetCustomPlayset();

		customPlayset.SetThumbnail(null);

		_playsetManager.Save(customPlayset);

		I_Thumbnail.Visible = B_ClearThumbnail.Visible = customPlayset.IsCustomThumbnailSet;
		L_ThumbnailInfo.Text = customPlayset.IsCustomThumbnailSet ? Locale.PlaysetThumbnailSet : Locale.PlaysetThumbnailNotSet;

		SideControl.Invalidate();
	}

	private void B_ClearColor_Click(object sender, EventArgs e)
	{
		var customPlayset = Playset.GetCustomPlayset();
		customPlayset.Color = null;

		_playsetManager.Save(customPlayset);

		I_Color.Visible = B_ClearColor.Visible = customPlayset.Color.HasValue;
		L_ColorInfo.Text = customPlayset.Color.HasValue ? Locale.PlaysetColorSet : Locale.PlaysetColorNotSet;

		SideControl.Invalidate();
	}

	private async void B_Share_Click(object sender, EventArgs e)
	{
		B_Share.Loading = true;

		try
		{
			var playset = await _playsetManager.GenerateImportPlayset(Playset, true);
			var path = CrossIO.Combine(ServiceCenter.Get<ILocationService>().SkyveDataPath, "Playsets", "Shared", $"{Playset.Name} {DateTime.Now:yy-MM-dd}.json");

			Directory.CreateDirectory(Path.GetDirectoryName(path));

			File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(playset));

			PlatformUtil.OpenFolder(path);
		}
		catch (Exception ex)
		{
			MessagePrompt.Show(ex, form: Form);
		}

		B_Share.Loading = false;
	}

	private async void B_Deactivate_ClickAsync(object sender, EventArgs e)
	{
		B_Deactivate.Loading = true;
		await _playsetManager.DeactivateActivePlayset();
		B_Deactivate.Loading = false;
	}

	private async void B_Delete_Click(object sender, EventArgs e)
	{
		B_Delete.Loading = true;

		if (MessagePrompt.Show(Locale.AreYouSure + "\r\n\r\n" + Locale.ActionUnreversible, PromptButtons.YesNo, PromptIcons.Hand, form: App.Program.MainForm) == DialogResult.Yes)
		{
			if (await ServiceCenter.Get<IPlaysetManager>().DeletePlayset(Playset))
			{
				PushBack();
			}
			else
			{
				ShowPrompt(Locale.FailedToDeletePlayset, PromptButtons.OK, PromptIcons.Error);
			}
		}

		B_Delete.Loading = false;
	}
}
