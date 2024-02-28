using Skyve.App.CS2.UserInterface.Forms;
using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Panels;

namespace Skyve.App.CS2.Services;
internal class InterfaceService : IAppInterfaceService
{
	PlaysetSettingsPanel IAppInterfaceService.PlaysetSettingsPanel(IPlayset playset)
	{
		return new PC_PlaysetSettings(playset);
	}

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

	void IInterfaceService.OpenPackagePage(IPackageIdentity package, bool openCompatibilityPage)
	{
		App.Program.MainForm.PushPanel(new PC_PackagePage(package, openCompatibilityPage));
	}

	PanelContent IAppInterfaceService.RequestReviewPanel(IPackageIdentity package)
	{
		return new PC_SendReviewRequest(package);
	}
}
