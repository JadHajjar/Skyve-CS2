using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Generic;
using Skyve.App.UserInterface.Panels;
using Skyve.Domain.CS2.Content;

using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PlaysetPage : PlaysetSettingsPanel
{
	private readonly bool loadingPlayset;
	private readonly IOSelectionDialog imagePrompt;

	private readonly IPlaysetManager _playsetManager;
	private readonly ISettings _settings;
	private readonly IPackageUtil _packageUtil;
	private readonly INotifier _notifier;

	public IPlayset Playset { get; }
	public ContentList LC_Items { get; }
	public PlaysetSideControl SideControl { get; }

	public PC_PlaysetPage(IPlayset playset, bool settingsTab) : base(true)
	{
		Playset = playset;

		ServiceCenter.Get(out _packageUtil, out _playsetManager, out _notifier, out _settings);

		InitializeComponent();

		imagePrompt = new IOSelectionDialog
		{
			ValidExtensions = IO.ImageExtensions
		};

		T_Content.LinkedControl = LC_Items = new ContentList(SkyvePage.Playset, false, GetContents, () => Locale.Package, GetCountText);

		P_Side.Controls.Add(SideControl = new PlaysetSideControl(playset) { Dock = DockStyle.Top });

		foreach (var item in this.GetControls<SlickCheckbox>())
		{
			if (item.Parent != TLP_AdvancedDev)
			{
				SlickTip.SetTo(item, item.Text + "_Tip");
			}
		}

		T_LaunchSettings.PreSelected = settingsTab;

		TLP_AdvancedDev.Visible = _settings.UserSettings.AdvancedLaunchOptions;

		var customPlayset = playset.GetCustomPlayset();

		TLP_Options.Enabled = true;
		DD_PlaysetUsage.SelectedItem = customPlayset.Usage > 0 ? customPlayset.Usage : (PackageUsage)(-1);
		CB_NoBanner.Checked =customPlayset.NoBanner  ;

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
			CB_DeveloperMode.Checked = extendedPlayset.LaunchSettings.UIDeveloperMode;
			CB_UIDeveloperMode.Checked = extendedPlayset.LaunchSettings.DeveloperMode;
			CB_UseCitiesExe.Checked = extendedPlayset.LaunchSettings.UseCitiesExe;
			DD_LogLevel.SelectedItem = extendedPlayset.LaunchSettings.LogLevel;
			TB_CustomArgs.Text = extendedPlayset.LaunchSettings.CustomArgs;
		}
	}

	protected override async Task<bool> LoadDataAsync()
	{
		await LC_Items.RefreshItems();

		return true;
	}

	private Task<IEnumerable<IPackageIdentity>> GetContents()
	{
		return _playsetManager.GetPlaysetContents(Playset);
	}

	protected string GetCountText()
	{
		int packagesIncluded = 0, modsIncluded = 0, modsEnabled = 0;

		foreach (var item in LC_Items!.Items)
		{
			if (item?.IsIncluded() == true)
			{
				packagesIncluded++;

				if (item.GetPackage()?.IsCodeMod == true)
				{
					modsIncluded++;

					if (item.IsEnabled())
					{
						modsEnabled++;
					}
				}
			}
		}

		var total = LC_Items!.ItemCount;

		if (!_settings.UserSettings.AdvancedIncludeEnable)
		{
			return string.Format(Locale.PackageIncludedTotal, packagesIncluded, total);
		}

		if (modsIncluded == modsEnabled)
		{
			return string.Format(Locale.PackageIncludedAndEnabledTotal, packagesIncluded, total);
		}

		return string.Format(Locale.PackageIncludedEnabledTotal, packagesIncluded, modsIncluded, modsEnabled, total);
	}

	protected override void LocaleChanged()
	{
		DD_PlaysetUsage.Text = Locale.PlaysetUsage;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		TLP_Options.Padding = TLP_Side.Padding= UI.Scale(new Padding(10, 0, 10, 10), UI.UIScale);
		TLP_AdvancedDev.Margin = UI.Scale(new Padding(0, 15, 0, 0), UI.UIScale);

		P_Side.Width = (int)(250 * UI.FontScale);
		P_Side.Padding = UI.Scale(new Padding(15, 0, 15, 15), UI.FontScale);
		slickSpacer1.Margin = B_EditThumbnail.Margin = B_EditColor.Margin = UI.Scale(new Padding(5), UI.FontScale);
		slickSpacer1.Height = (int)UI.FontScale;
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP_AdvancedDev.BackColor = design.BackColor.MergeColor(design.RedColor, 95);
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

		customPlayset.Usage = DD_PlaysetUsage.SelectedItem;
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
				UIDeveloperMode = CB_DeveloperMode.Checked,
				DeveloperMode = CB_UIDeveloperMode.Checked,
				UseCitiesExe = CB_UseCitiesExe.Checked,
				LogLevel = DD_LogLevel.SelectedItem,
				CustomArgs = TB_CustomArgs.Text,
			};
		}

		_playsetManager.Save(customPlayset);
	}

	public override void EditName()
	{
		SideControl.EditName();
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

				SideControl.Invalidate();
			}
			catch { }
		}
	}
}
