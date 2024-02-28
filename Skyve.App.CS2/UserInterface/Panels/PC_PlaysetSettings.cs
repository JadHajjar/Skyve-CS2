using Skyve.App.Interfaces;
using Skyve.Domain.CS2.Content;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PlaysetSettings : PlaysetSettingsPanel
{
	private bool loadingPlayset;
	private readonly SlickCheckbox[] _launchOptions;
	private readonly IOSelectionDialog imagePrompt;

	private readonly IPlaysetManager _playsetManager;
	private readonly ILocationService _locationManager;
	private readonly IPackageManager _packageManager;
	private readonly ISettings _settings;
	private readonly IPackageUtil _packageUtil;
	private readonly IIOUtil _iOUtil;
	private readonly INotifier _notifier;
	private readonly ITagsService _tagsService;

	public IPlayset Playset { get; }

	public PC_PlaysetSettings(IPlayset playset)
	{
		Playset = playset;

		ServiceCenter.Get(out _packageUtil, out _iOUtil, out _locationManager, out _playsetManager, out _packageManager, out _notifier, out _settings, out _tagsService);

		InitializeComponent();

		imagePrompt = new IOSelectionDialog
		{
			ValidExtensions = IO.ImageExtensions
		};

		PB_Icon.Playset = playset;

		SlickTip.SetTo(L_CurrentPlayset, "EditPlaysetName");

		foreach (var item in this.GetControls<SlickCheckbox>())
		{
			if (item.Parent != TLP_AdvancedDev)
			{
				SlickTip.SetTo(item, item.Text + "_Tip");
			}
		}

		TLP_AdvancedDev.Visible = _settings.UserSettings.AdvancedLaunchOptions;

		_notifier.PlaysetChanged += ProfileManager_ProfileChanged;

		var customPlayset = playset.GetCustomPlayset();

		I_Favorite.ImageName = customPlayset.IsFavorite ? "I_StarFilled" : "I_Star";
		TLP_Options.Enabled = true;

		SlickTip.SetTo(I_Favorite, customPlayset.IsFavorite ? "UnFavoriteThisPlayset" : "FavoriteThisPlayset");

		B_Activate.Visible = _playsetManager.CurrentPlayset != playset;
		LI_ModCount.ValueText = Locale.ItemsCount.FormatPlural(playset.ModCount);
		LI_ModSize.ValueText = playset.ModSize.SizeString(0);
		L_CurrentPlayset.Text = playset.Name;
		DD_PlaysetUsage.SelectedItem = customPlayset.Usage > 0 ? customPlayset.Usage : (PackageUsage)(-1);
	}

	private void ProfileManager_ProfileChanged()
	{
		this.TryInvoke(() => LoadPlayset(Playset as Playset));
	}

	protected override void LocaleChanged()
	{
		Text = Locale.ActivePlayset;
		DD_PlaysetUsage.Text = Locale.PlaysetUsage;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		I_More.Size = I_Favorite.Size = UI.Scale(new Size(28, 28), UI.FontScale);
		L_CurrentPlayset.Font = UI.Font(11.5F, FontStyle.Bold);
		P_Name.Height = (int)(32 * UI.FontScale);
		TLP_AdvancedDev.Margin = TLP_LaunchSettings.Margin = UI.Scale(new Padding(10), UI.UIScale);

		P_Side.Width = (int)(260 * UI.FontScale);
		roundedPanel1.Padding = UI.Scale(new Padding(10), UI.FontScale);
		PB_Icon.Size = UI.Scale(new Size(160, 160), UI.FontScale);
		PB_Icon.Margin = UI.Scale(new Padding(0, 0, 0, 10), UI.FontScale);
		slickSpacer1.Margin = slickSpacer2.Margin = B_Activate.Margin = B_EditThumbnail.Margin = B_EditColor.Margin = L_CurrentPlayset.Padding = UI.Scale(new Padding(5), UI.FontScale);
		slickSpacer1.Height = slickSpacer2.Height = (int)UI.FontScale;
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP_AdvancedDev.BackColor = TLP_LaunchSettings.BackColor = design.BackColor.Tint(Lum: design.IsDarkTheme ? 1 : -1);

		if (Playset.GetCustomPlayset().Color is Color color)
		{
			roundedPanel1.BackColor = design.AccentBackColor.MergeColor(color, 85);
		}
		else
		{
			roundedPanel1.BackColor = design.BackColor.Tint(Lum: design.IsDarkTheme ? 6 : -6);
		}
	}

	public override bool CanExit(bool toBeDisposed)
	{
		if (TB_Name.Visible)
		{
			if (toBeDisposed)
			{
				Notification.Create(Locale.ApplyPlaysetNameBeforeExit, null, PromptIcons.Hand, null).Show(Form, 10);
			}

			return false;
		}

		return true;
	}

	internal void Ctrl_LoadPlayset(IPlayset obj)
	{
		L_CurrentPlayset.Text = obj.Name;
		TLP_Options.Enabled = false;
		_playsetManager.ActivatePlayset(obj);
	}

	private void LoadPlayset(Playset? playset)
	{
		if (playset == null)
		{
			return;
		}

		loadingPlayset = true;

		//CB_NoWorkshop.Checked = profile.LaunchSettings.NoWorkshop;
		//CB_NoAssets.Checked = profile.LaunchSettings.NoAssets;
		//CB_NoMods.Checked = profile.LaunchSettings.NoMods;
		//CB_LHT.Checked = profile.LaunchSettings.LHT;
		//CB_UseCitiesExe.Checked = profile.LaunchSettings.UseCitiesExe;
		//CB_UnityProfiler.Checked = profile.LaunchSettings.UnityProfiler;
		//CB_DebugMono.Checked = profile.LaunchSettings.DebugMono;
		//CB_LoadSave.Checked = profile.LaunchSettings.LoadSaveGame;
		//CB_StartNewGame.Checked = profile.LaunchSettings.StartNewGame;
		//CB_DevUI.Checked = profile.LaunchSettings.DevUi;
		//CB_RefreshWorkshop.Checked = profile.LaunchSettings.RefreshWorkshop;
		//DD_NewMap.SelectedFile = profile.LaunchSettings.MapToLoad;
		//DD_SaveFile.SelectedFile = profile.LaunchSettings.SaveToLoad;
		//TB_CustomArgs.Text = profile.LaunchSettings.CustomArgs;
		//CB_NewAsset.Checked = profile.LaunchSettings.NewAsset;
		//CB_LoadAsset.Checked = profile.LaunchSettings.LoadAsset;

		//CB_LoadUsed.Checked = profile.LsmSettings.LoadUsed;
		//CB_LoadEnabled.Checked = profile.LsmSettings.LoadEnabled;
		//CB_SkipFile.Checked = profile.LsmSettings.UseSkipFile;
		//DD_SkipFile.SelectedFile = profile.LsmSettings.SkipFile;

		loadingPlayset = false;
	}

	private void ValueChanged(object sender, EventArgs e)
	{
		if (loadingPlayset || !Live)
		{
			return;
		}

		if (_launchOptions.Contains(sender) && (sender as SlickCheckbox)!.Checked)
		{
			foreach (var item in _launchOptions)
			{
				if (item == sender)
				{
					continue;
				}

				item.Checked = false;
			}
		}

		var playset = (Playset as Playset)!;

		//playset.AutoSave = CB_AutoSave.Checked;
		//playset.Usage = DD_PlaysetUsage.SelectedItem;

		//playset.LaunchSettings.NoWorkshop = CB_NoWorkshop.Checked;
		//playset.LaunchSettings.NoAssets = CB_NoAssets.Checked;
		//playset.LaunchSettings.NoMods = CB_NoMods.Checked;
		//playset.LaunchSettings.LHT = CB_LHT.Checked;
		//playset.LaunchSettings.StartNewGame = CB_StartNewGame.Checked;
		//playset.LaunchSettings.MapToLoad = _iOUtil.ToRealPath(DD_NewMap.SelectedFile);
		//playset.LaunchSettings.SaveToLoad = _iOUtil.ToRealPath(DD_SaveFile.SelectedFile);
		//playset.LaunchSettings.LoadSaveGame = CB_LoadSave.Checked;
		//playset.LaunchSettings.UseCitiesExe = CB_UseCitiesExe.Checked;
		//playset.LaunchSettings.UnityProfiler = CB_UnityProfiler.Checked;
		//playset.LaunchSettings.DebugMono = CB_DebugMono.Checked;
		//playset.LaunchSettings.RefreshWorkshop = CB_RefreshWorkshop.Checked;
		//playset.LaunchSettings.DevUi = CB_DevUI.Checked;
		//playset.LaunchSettings.CustomArgs = TB_CustomArgs.Text;
		//playset.LaunchSettings.NewAsset = CB_NewAsset.Checked;
		//playset.LaunchSettings.LoadAsset = CB_LoadAsset.Checked;

		//playset.LsmSettings.SkipFile = _iOUtil.ToRealPath(DD_SkipFile.SelectedFile);
		//playset.LsmSettings.LoadEnabled = CB_LoadEnabled.Checked;
		//playset.LsmSettings.LoadUsed = CB_LoadUsed.Checked;
		//playset.LsmSettings.UseSkipFile = CB_SkipFile.Checked;

		//_playsetManager.Save(playset);
	}

	public override void EditName()
	{
		B_EditName_Click(this, EventArgs.Empty);
	}

	internal void B_EditName_Click(object sender, EventArgs e)
	{
		TB_Name.Visible = true;
		L_CurrentPlayset.Visible = false;
		TB_Name.Text = L_CurrentPlayset.Text;

		this.TryBeginInvoke(() =>
		{
			TB_Name.Focus();
			TB_Name.SelectAll();
		});
	}

	private void TB_Name_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode is Keys.Enter or Keys.Escape)
		{
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
	}

	private void TB_Name_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			e.IsInputKey = true;

			TB_Name.Visible = false;
		}

		if (e.KeyCode == Keys.Escape)
		{
			e.IsInputKey = true;

			TB_Name.Text = string.Empty;
			TB_Name.Visible = false;
		}
	}

	private void TB_Name_IconClicked(object sender, EventArgs e)
	{
		TB_Name.Visible = false;
	}

	private async void TB_Name_Leave(object sender, EventArgs e)
	{
		if (!TB_Name.Visible)
		{
			return;
		}

		TB_Name.Visible = false;
		L_CurrentPlayset.Visible = true;

		var text = TB_Name.Text.Trim();

		if (string.IsNullOrWhiteSpace(text))
		{
			return;
		}

		L_CurrentPlayset.Text = text;

		if (!await _playsetManager.RenamePlayset(Playset, text))
		{
			TB_Name.SetError();
			return;
		}
	}

	private void T_PlaysetUsage_SelectedValueChanged(object sender, EventArgs e)
	{
		if (loadingPlayset)
		{
			return;
		}

		var invalidPackages = _playsetManager.GetInvalidPackages(DD_PlaysetUsage.SelectedItem);

		if (invalidPackages.Any())
		{
			if (ShowPrompt($"{Locale.SomePackagesWillBeDisabled}\r\n{Locale.AffectedPackagesAre}\r\n• {invalidPackages.ListStrings("\r\n• ")}", PromptButtons.OKCancel, PromptIcons.Warning) == DialogResult.Cancel)
			{
				DD_PlaysetUsage.SelectedItem = (PackageUsage)(-1);

				return;
			}

			_packageUtil.SetIncluded(invalidPackages.Select(x => x.LocalData)!, false);
		}

		ValueChanged(sender, e);
	}

	private void LsmSettingsChanged(object sender, EventArgs e)
	{
		if (loadingPlayset)
		{
			return;
		}

		ValueChanged(sender, e);

		//_playsetManager.SaveLsmSettings(Playset);
	}

	private void B_Save_Click(object sender, EventArgs e)
	{
		//if (Playset.Save())
		//{
		//	B_Save.ImageName = "I_Check";

		//	new BackgroundAction(() =>
		//	{
		//		B_Save.ImageName = "I_Save";
		//	}).RunIn(2000);
		//}
		//else
		//{
		//	ShowPrompt(Locale.CouldNotCreatePlayset, icon: PromptIcons.Error);
		//}
	}

	private void B_TempProfile_Click(object sender, EventArgs e)
	{
		//_playsetManager.SetCurrentPlayset(Playset.TemporaryPlayset);
	}

	private void I_Favorite_Click(object sender, EventArgs e)
	{
		if (Playset.Temporary)
		{
			return;
		}

		_playsetManager.CurrentCustomPlayset.IsFavorite = !_playsetManager.CurrentCustomPlayset.IsFavorite;
		//_playsetManager.Save(Playset);

		I_Favorite.ImageName = _playsetManager.CurrentCustomPlayset.IsFavorite ? "I_StarFilled" : "I_Star";
		SlickTip.SetTo(I_Favorite, _playsetManager.CurrentCustomPlayset.IsFavorite ? "UnFavoriteThisPlayset" : "FavoriteThisPlayset");
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
		_notifier.OnRefreshUI(true);

		DesignChanged(FormDesign.Design);
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

				PB_Icon.Invalidate();
			}
			catch { }
		}
	}

	private async void B_Activate_Click(object sender, EventArgs e)
	{
		await _playsetManager.ActivatePlayset(Playset);

		B_Activate.Hide();
	}

	private void I_More_Click(object sender, EventArgs e)
	{
		SlickToolStrip.Show(Form, ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems(Playset, true));
	}

	private void PB_Icon_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			I_More_Click(sender, e);
		}
	}

	private void L_CurrentPlayset_Paint(object sender, PaintEventArgs e)
	{
		if (L_CurrentPlayset.HoverState.HasFlag(HoverState.Hovered))
		{
			using var brush = new SolidBrush(Color.FromArgb(200, L_CurrentPlayset.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? 10 : -8)));
			using var icon = IconManager.GetIcon("I_Edit", L_CurrentPlayset.Height * 3 / 4).Color(L_CurrentPlayset.ForeColor);

			e.Graphics.FillRoundedRectangle(brush, L_CurrentPlayset.ClientRectangle.Pad(1), L_CurrentPlayset.Padding.Left);

			e.Graphics.DrawImage(icon, L_CurrentPlayset.ClientRectangle.Pad(L_CurrentPlayset.Padding).Align(icon.Size, ContentAlignment.MiddleRight));
		}
	}
}
