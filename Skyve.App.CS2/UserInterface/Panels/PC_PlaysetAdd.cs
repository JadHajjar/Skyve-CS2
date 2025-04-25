using Skyve.App.Interfaces;

using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PlaysetAdd : PanelContent
{
	private readonly IPlaysetManager _playsetManager = ServiceCenter.Get<IPlaysetManager>();

	public PC_PlaysetAdd()
	{
		InitializeComponent();

		DAD_NewPlayset.StartingFolder = ServiceCenter.Get<ISettings>().FolderSettings.AppDataPath;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		B_Cancel.Visible = Form.PanelHistory.Any();

		if (_playsetManager.CurrentPlayset is null)
		{
			B_ClonePlayset.Enabled=false;
		}
	}

	protected override void LocaleChanged()
	{
		base.LocaleChanged();

		L_Title.Text = Locale.NewPlaysetTip;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		L_Title.Font = UI.Font(12.75F, System.Drawing.FontStyle.Bold);
		L_Title.Margin = UI.Scale(new Padding(6));
		B_Cancel.Font = UI.Font(9.75F);
		DAD_NewPlayset.Margin = B_Cancel.Margin = UI.Scale(new Padding(10), UI.UIScale);
	}

	private async void NewPlayset_Click(object sender, EventArgs e)
	{
		B_NewPlayset.Loading = true;
		var newPlayset = await _playsetManager.CreateNewPlayset("New Playset");

		if (newPlayset is null)
		{
			B_NewPlayset.Loading = false;
			ShowPrompt(Locale.CouldNotCreatePlayset, icon: PromptIcons.Error);
			return;
		}

		await _playsetManager.ActivatePlayset(newPlayset);

		Dispose();
		ServiceCenter.Get<IAppInterfaceService>().OpenPlaysetPage(newPlayset);
	}

	private async void CopyPlayset_Click(object sender, EventArgs e)
	{
		if (_playsetManager.CurrentPlayset is null)
		{
			return;
		}

		B_ClonePlayset.Loading = true;
		var newPlayset = await _playsetManager.ClonePlayset(_playsetManager.CurrentPlayset);

		if (newPlayset is null)
		{
			B_ClonePlayset.Loading = false;
			ShowPrompt(Locale.CouldNotCreatePlayset, icon: PromptIcons.Error);
			return;
		}

		await _playsetManager.ActivatePlayset(newPlayset);

		Dispose();
		ServiceCenter.Get<IAppInterfaceService>().OpenPlaysetPage(newPlayset);
	}

	private void B_Cancel_Click(object sender, EventArgs e)
	{
		PushBack();
	}

	private async void DAD_NewPlayset_FileSelected(string obj)
	{
		try
		{
			DAD_NewPlayset.Loading = true;

			var newPlayset = await _playsetManager.ImportPlayset(obj, true);

			if (newPlayset is not null)
			{
				Dispose();

				ServiceCenter.Get<IAppInterfaceService>().OpenPlaysetPage(newPlayset);
			}
		}
		catch (Exception ex)
		{
			ShowPrompt(ex, "Failed to import your playset");
		}

		DAD_NewPlayset.Loading = false;
	}

	private async void B_ImportLink_Click(object sender, EventArgs e)
	{
		var result = ShowInputPrompt(Locale.PastePlaysetId);

		if (result.DialogResult != DialogResult.OK)
		{
			return;
		}

		try
		{
			await ServiceCenter.Get<IOnlinePlaysetUtil>().DownloadPlayset(result.Input);
		}
		catch (Exception ex)
		{
			App.Program.MainForm.TryInvoke(() => MessagePrompt.Show(ex, Locale.FailedToDownloadPlayset, form: App.Program.MainForm));
		}
	}
}
