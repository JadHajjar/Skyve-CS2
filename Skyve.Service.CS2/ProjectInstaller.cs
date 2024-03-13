using System.ComponentModel;
using System.Configuration.Install;

namespace Skyve.Service.CS2;

[RunInstaller(true)]
public partial class ProjectInstaller : Installer
{
	public ProjectInstaller()
	{
		InitializeComponent();
	}
}
