
using Skyve.App.CS2.UserInterface.Forms;
using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Panels;
using Skyve.Domain.CS2.Utilities;

using System.Windows.Forms;

namespace Skyve.App.CS2.Services;
internal class InterfaceService : IAppInterfaceService
{
	PC_Changelog IAppInterfaceService.ChangelogPanel()
	{
		return new PC_SkyveChangeLog();
	}

	PanelContent IAppInterfaceService.UtilitiesPanel()
	{
		return new PC_Utilities();
	}

	INotificationInfo IAppInterfaceService.GetLastVersionNotification()
	{
		return new LastVersionNotification();
	}

	void IInterfaceService.ViewSpecificPackages(List<IPackageIdentity> packages, string title)
	{
		App.Program.MainForm.PushPanel(new PC_ViewSpecificPackages(packages, title));
	}

	void IInterfaceService.OpenOptionsPage()
	{
		App.Program.MainForm.PushPanel<PC_Options>();
	}

	void IInterfaceService.OpenParadoxLogin()
	{
		new ParadoxLoginForm().ShowDialog(App.Program.MainForm);
	}

	PanelContent IAppInterfaceService.NewPlaysetPanel()
	{
		return new PC_PlaysetAdd();
	}

	void IInterfaceService.OpenPackagePage(IPackageIdentity package, bool openCompatibilityPage, bool openCommentsPage)
	{
		App.Program.MainForm.TryInvoke(() => App.Program.MainForm.PushPanel(new PC_PackagePage(package, openCompatibilityPage, openCommentsPage)));
	}

	PanelContent IAppInterfaceService.RequestReviewPanel(IPackageIdentity package)
	{
		return new PC_SendReviewRequest(package);
	}

	void IInterfaceService.OpenPlaysetPage(IPlayset playset, bool settingsTab)
	{
		App.Program.MainForm.TryInvoke(() => App.Program.MainForm.PushPanel(new PC_PlaysetPage(playset, settingsTab)));
	}

	bool IInterfaceService.AskForDependencyConfirmation(List<IPackageIdentity> packages, List<IPackageIdentity> dependencies)
	{
		return DialogResult.Yes == MessagePrompt.Show(LocaleCS2.AddingDependencies.FormatPlural(dependencies.Count, dependencies.ListStrings(x => x.CleanName(true), ", ")), LocaleCS2.ModsYouAreAddingRequireDependencies.FormatPlural(packages.Count, packages.ListStrings(x => x.CleanName(true), ", ")), PromptButtons.YesNo, PromptIcons.Question, App.Program.MainForm);
	}

	void IInterfaceService.OpenLogReport(bool save)
	{
		App.Program.MainForm.TryInvoke(() =>
		{
			var panel = new PC_HelpAndLogs();

			App.Program.MainForm.PushPanel(panel);

			if (save)
			{
				panel.B_SaveZip_Click(panel, EventArgs.Empty);
			}
			else
			{
				panel.B_CopyZip_Click(panel, EventArgs.Empty);
			}
		});
	}

	public void RestoreBackup(string restoreBackup)
	{
		App.Program.MainForm.TryInvoke(() =>
		{
			if (App.Program.MainForm.CurrentPanel is not PC_BackupCenter backupCenter)
			{
				backupCenter = new();

				App.Program.MainForm.PushPanel(backupCenter);
			}

			backupCenter.SelectBackup(restoreBackup);
		});
	}
}
