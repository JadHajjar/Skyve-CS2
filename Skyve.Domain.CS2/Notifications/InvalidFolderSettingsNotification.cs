using Extensions;

using Skyve.Domain.CS2.Utilities;

using SlickControls;

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using static System.Environment;

namespace Skyve.Domain.CS2.Notifications;
public class InvalidFolderSettingsNotification : INotificationInfo
{
	public InvalidFolderSettingsNotification()
	{
		Time = DateTime.Now;
		Title = LocaleCS2.InvalidFolderSettings;
		Description = LocaleCS2.InvalidFolderSettingsInfo;
		Icon = "Hazard";
		HasAction = true;
	}

	public DateTime Time { get; }
	public string Title { get; }
	public string? Description { get; }
	public string Icon { get; }
	public Color? Color => FormDesign.Design.RedColor;
	public bool HasAction { get; }

	public void OnClick()
	{
		var dialog = new IOSelectionDialog()
		{
			StartingFolder = Path.Combine(Path.GetDirectoryName(GetFolderPath(SpecialFolder.ApplicationData)), "LocalLow"),
		};

		if (dialog.PromptFolder() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
		{
			if (!File.Exists(Path.Combine(dialog.SelectedPath, "ModsSettings", "Skyve", "FolderSettings.json")))
			{
				MessagePrompt.Show(LocaleCS2.InvalidFolderSettingsFail, LocaleCS2.InvalidFolderSettings, PromptButtons.OK, PromptIcons.Error);
			}
			else
			{
				File.WriteAllText(Path.Combine(GetFolderPath(SpecialFolder.LocalApplicationData), "SkyveDataPathHelper.txt"), dialog.SelectedPath);

				Process.Start(Application.ExecutablePath);
				Application.Exit();
			}
		}
	}

	public void OnRightClick()
	{
	}
}
