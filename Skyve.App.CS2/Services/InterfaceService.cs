using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.Interfaces;

namespace Skyve.App.CS2.Services;
internal class InterfaceService : IInterfaceService
{
	PlaysetSettingsPanel IInterfaceService.PlaysetSettingsPanel()
	{
		return new PC_PlaysetSettings();
	}

	PC_Changelog IInterfaceService.ChangelogPanel()
	{
		return new PC_SkyveChangeLog();
	}

	PanelContent IInterfaceService.UtilitiesPanel()
	{
		return new PC_Utilities();
	}
}
