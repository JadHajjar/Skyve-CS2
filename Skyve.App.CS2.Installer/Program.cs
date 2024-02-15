using Extensions;

using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace Skyve.App.CS2.Installer;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		if (Path.GetFileNameWithoutExtension(Application.ExecutablePath).Equals("uninstall", StringComparison.CurrentCultureIgnoreCase))
		{
			Installer.UnInstall().RunSynchronously();
			
			return;
		}

		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		if (Environment.OSVersion.Version.Major >= 6)
		{
			SetProcessDPIAware();
		}

		Application.Run(new InstallingForm());
	}

	[System.Runtime.InteropServices.DllImport("user32.dll")]
	private static extern bool SetProcessDPIAware();
}