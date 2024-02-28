using Skyve.App.Interfaces;

using System.IO;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PlaysetAdd : PanelContent
{
	private readonly IPlaysetManager _playsetManager = ServiceCenter.Get<IPlaysetManager>();

	public PC_PlaysetAdd()
	{
		InitializeComponent();

		DAD_NewProfile.StartingFolder = ServiceCenter.Get<ISettings>().FolderSettings.AppDataPath;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		B_Cancel.Visible = Form.PanelHistory.Any();

		if (_playsetManager.CurrentPlayset is null)
			newProfileOptionControl2.Parent = null;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		B_Cancel.Font = UI.Font(9.75F);
		DAD_NewProfile.Margin = B_Cancel.Margin = UI.Scale(new Padding(10), UI.UIScale);
	}

	private async void NewProfile_Click(object sender, EventArgs e)
	{
		var newPlayset = await _playsetManager.CreateNewPlayset("New Playset");

		if (newPlayset is null)
		{
			ShowPrompt(Locale.CouldNotCreatePlayset, icon: PromptIcons.Error);
			return;
		}

		await _playsetManager.ActivatePlayset(newPlayset);

		var panel = ServiceCenter.Get<IAppInterfaceService>().PlaysetSettingsPanel(newPlayset);

		if (Form.SetPanel(null, panel))
		{
			panel.EditName();
		}
	}

	private async void CopyProfile_Click(object sender, EventArgs e)
	{
		if (_playsetManager.CurrentPlayset is null)
			return;

		var newPlayset = await _playsetManager.ClonePlayset(_playsetManager.CurrentPlayset);

		if (newPlayset is null)
		{
			ShowPrompt(Locale.CouldNotCreatePlayset, icon: PromptIcons.Error);
			return;
		}

		await _playsetManager.ActivatePlayset(newPlayset);

		var panel = ServiceCenter.Get<IAppInterfaceService>().PlaysetSettingsPanel(newPlayset);

		if (Form.SetPanel(null, panel))
		{
			panel.EditName();
		}
	}

	private void B_Cancel_Click(object sender, EventArgs e)
	{
		PushBack();
	}

	private async void DAD_NewProfile_FileSelected(string obj)
	{
		var newPlayset = _playsetManager.Playsets.FirstOrDefault(x => x.Name!.Equals(Path.GetFileNameWithoutExtension(obj), StringComparison.InvariantCultureIgnoreCase));

		if (newPlayset is null)
		{
			newPlayset = await _playsetManager.ImportPlayset(obj);
		}

		try
		{
			var panel = ServiceCenter.Get<IAppInterfaceService>().PlaysetSettingsPanel(newPlayset);

			if (Form.SetPanel(null, panel))
			{
				panel.LoadPlayset(newPlayset!);
			}
		}
		catch (Exception ex) { ShowPrompt(ex, "Failed to import your playset"); }
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
		catch (Exception ex) { App.Program.MainForm.TryInvoke(() => MessagePrompt.Show(ex, Locale.FailedToDownloadPlayset, form: App.Program.MainForm)); }
	}
}
