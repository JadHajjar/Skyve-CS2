using Skyve.App.Interfaces;
using Skyve.App.Utilities;
using Skyve.Domain.CS2.Utilities;
using Skyve.Systems.CS2.Utilities;

using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_Options : PanelContent
{
	private readonly ISettings _settings = ServiceCenter.Get<ISettings>();

	public PC_Options()
	{
		InitializeComponent();
		ApplyCurrentSettings();

		foreach (var button in this.GetControls<SlickButton>())
		{
			if (button != B_ChangeLog && button != B_CreateJunction && button != B_DeleteJunction && button is not SlickLabel)
			{
				SlickTip.SetTo(button, LocaleHelper.GetGlobalText($"{button.Text}_Tip"));
			}
		}

		B_CreateShortcut.Visible = CrossIO.CurrentPlatform is not Platform.Windows;

		DD_Language.Items = LocaleHelper.GetAvailableLanguages().Distinct().ToArray();
		DD_Language.SelectedItem = DD_Language.Items.FirstOrDefault(x => x == LocaleHelper.CurrentCulture.IetfLanguageTag) ?? DD_Language.Items[0];
		DD_Language.SelectedItemChanged += DD_Language_SelectedItemChanged;
	}

	public override Color GetTopBarColor()
	{
		return FormDesign.Design.AccentBackColor;
	}

	private void ApplyCurrentSettings()
	{
		foreach (var cb in this.GetControls<SlickCheckbox>())
		{
			if (!string.IsNullOrWhiteSpace(cb.Tag?.ToString()))
			{
				cb.Checked = (bool)_settings.UserSettings.GetType()
					.GetProperty(cb.Tag!.ToString(), BindingFlags.Instance | BindingFlags.Public)
					.GetValue(_settings.UserSettings);

				SlickTip.SetTo(cb, LocaleHelper.GetGlobalText($"{cb.Text}_Tip"));

				if (!IsHandleCreated)
				{
					cb.CheckChanged += CB_CheckChanged;
				}
			}
		}
	}

	protected override void LocaleChanged()
	{
		Text = LocaleSlickUI.Options;
		L_JunctionTitle.Text = LocaleCS2.JunctionTitle;
		L_JunctionDescription.Text = LocaleCS2.JunctionDescription;
		L_JunctionStatusLabel.Text = LocaleCS2.CurrentStatus;
		L_JunctionStatus.Text = LocaleSlickUI.Loading;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		DD_Language.Width = (int)(220 * UI.FontScale);
		TLP_Main.Padding = UI.Scale(new Padding(3, 0, 7, 0), UI.FontScale);
		B_Theme.Margin = TLP_UI.Margin = TLP_Settings.Margin = TLP_Advanced.Margin = B_HelpTranslate.Margin = TLP_HelpLogs.Margin =
			 B_Discord.Margin = B_Guide.Margin = B_Reset.Margin = B_ChangeLog.Margin = B_CreateShortcut.Margin =
			TLP_Preferences.Margin = UI.Scale(new Padding(10), UI.UIScale);
		DD_Language.Margin = UI.Scale(new Padding(10, 7, 10, 5), UI.UIScale);
		slickSpacer5.Height = slickSpacer1.Height = slickSpacer2.Height = (int)(1.5 * UI.FontScale);
		slickSpacer1.Margin = slickSpacer2.Margin = UI.Scale(new Padding(5), UI.UIScale);

		slickSpacer5.Margin = UI.Scale(new Padding(5, 10, 5, 10), UI.UIScale);
		B_CreateJunction.Margin = B_DeleteJunction.Margin = UI.Scale(new Padding(5, 10, 5, 5), UI.UIScale);
		L_JunctionTitle.Margin = L_JunctionStatusLabel.Margin = UI.Scale(new Padding(3), UI.FontScale);
		L_JunctionStatus.Margin = UI.Scale(new Padding(3, 5, 3, 3), UI.FontScale);
		L_JunctionDescription.Margin = UI.Scale(new Padding(10, 5, 3, 15), UI.FontScale);

		L_JunctionTitle.Font = UI.Font(9.5F, FontStyle.Bold);
		L_JunctionDescription.Font = UI.Font(7.75F);
		L_JunctionStatusLabel.Font = UI.Font(8.25F, FontStyle.Bold);
		L_JunctionStatus.Font = UI.Font("Consolas", 7F);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		BackColor = design.AccentBackColor;
		ForeColor = design.ForeColor.MergeColor(design.BackColor, 80);

		foreach (Control item in TLP_Main.Controls)
		{
			item.BackColor = design.BackColor.Tint(Lum: design.IsDarkTheme ? 1 : -1);
		}
	}

	protected override async void OnCreateControl()
	{
		base.OnCreateControl();

		var junctionLocation = await Task.Run(() => JunctionHelper.GetJunctionState(_settings.FolderSettings.AppDataPath));

		if (string.IsNullOrEmpty(junctionLocation))
		{
			L_JunctionStatus.Text = LocaleCS2.DefaultLocation;
		}
		else
		{
			L_JunctionStatus.Text = junctionLocation;
			B_DeleteJunction.Visible = true;
		}
	}

	private void CB_CheckChanged(object sender, EventArgs e)
	{
		try
		{
			if (!IsHandleCreated)
			{
				return;
			}

			var cb = (sender as SlickCheckbox)!;

			_settings.UserSettings.GetType()
				.GetProperty(cb.Tag!.ToString(), BindingFlags.Instance | BindingFlags.Public)
				.SetValue(_settings.UserSettings, cb.Checked);

			_settings.UserSettings.Save();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	private void DD_Language_SelectedItemChanged(object sender, EventArgs e)
	{
		try
		{
			LocaleHelper.SetLanguage(new(DD_Language.SelectedItem));
		}
		catch
		{
			ShowPrompt(Locale.CheckDocumentsFolder, Locale.FailedToSaveLanguage, PromptButtons.OK, PromptIcons.Error);
		}
	}

	private void B_Theme_Click(object sender, EventArgs e)
	{
		try
		{
			Form.PushPanel<PC_ThemeChanger>(null);
		}
		catch
		{
			ShowPrompt(Locale.CheckDocumentsFolder, Locale.FailedToOpenTC, PromptButtons.OK, PromptIcons.Error);
		}
	}

	private void B_HelpTranslate_Click(object sender, EventArgs e)
	{
		try
		{
			PlatformUtil.OpenUrl("https://crowdin.com/project/load-order-mod-2");
		}
		catch { }
	}

	private void B_Discord_Click(object sender, EventArgs e)
	{
		try
		{
			PlatformUtil.OpenUrl("https://discord.gg/E4k8ZEtRxd");
		}
		catch { }
	}

	private void B_Guide_Click(object sender, EventArgs e)
	{
		try
		{
			PlatformUtil.OpenUrl("https://bit.ly/40x93vk");
		}
		catch { }
	}

	private void B_Reset_Click(object sender, EventArgs e)
	{
		_settings.ResetUserSettings();

		ApplyCurrentSettings();
	}

	private void B_ChangeLog_Click(object sender, EventArgs e)
	{
		Form.PushPanel(ServiceCenter.Get<IAppInterfaceService>().ChangelogPanel());
	}

	private void slickScroll1_Scroll(object sender, ScrollEventArgs e)
	{
		slickSpacer3.Visible = slickScroll1.Percentage != 0;
	}

	private void AssumeInternetConnectivity_CheckChanged(object sender, EventArgs e)
	{
		ConnectionHandler.AssumeInternetConnectivity = CB_AssumeInternetConnectivity.Checked;
	}

	private async void B_CreateShortcut_Click(object sender, EventArgs e)
	{
		ServiceCenter.Get<ILocationService>().CreateShortcut();

		B_CreateShortcut.ImageName = "I_Check";

		await Task.Delay(3000);

		B_CreateShortcut.ImageName = "I_Link";
	}

	private void B_CreateJunction_Click(object sender, EventArgs e)
	{
		var dialog = new IOSelectionDialog();

		if (dialog.PromptFolder(Form) == DialogResult.OK)
		{
			if (Directory.Exists(dialog.SelectedPath))
			{
				if (ShowPrompt(LocaleCS2.JunctionRestart, Locale.RestartRequired, PromptButtons.OKCancel, PromptIcons.Info) != DialogResult.OK)
				{
					return;
				}

				ServiceCenter.Get<ICitiesManager>().Kill();
				Application.Exit();
				ServiceCenter.Get<IIOUtil>().Execute(App.Program.ExecutablePath, $"-createJunction \"{_settings.FolderSettings.AppDataPath}\" \"{dialog.SelectedPath}\" -stub", administrator: true);
			}
		}
	}

	private void B_DeleteJunction_Click(object sender, EventArgs e)
	{
		if (ShowPrompt(LocaleCS2.JunctionRestart, Locale.RestartRequired, PromptButtons.OKCancel, PromptIcons.Info) != DialogResult.OK)
		{
			return;
		}

		ServiceCenter.Get<ICitiesManager>().Kill();
		Application.Exit();
		ServiceCenter.Get<IIOUtil>().Execute(App.Program.ExecutablePath, $"-deleteJunction \"{_settings.FolderSettings.AppDataPath}\" -stub", administrator: true);
	}
}
