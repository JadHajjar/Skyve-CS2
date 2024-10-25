using Extensions;

using SlickControls;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Skyve.App.CS2.Installer;

internal static class Program
{
	[STAThread]
	private static void Main(string[] args)
	{
		try
		{
			if (RedirectIfNeeded(args, out var uninstall))
			{
				return;
			}

			SlickCursors.Initialize();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (Environment.OSVersion.Version.Major >= 6)
			{
				SetProcessDPIAware();
			}

			Application.Run(new InstallingForm(uninstall));
		}
		catch (Exception ex)
		{
			MessagePrompt.Show(ex, "Something went wrong while starting the installer");
		}
	}

	private static bool RedirectIfNeeded(string[] args, out bool uninstall)
	{
		var isAdmin = WinExtensionClass.IsAdministrator;
		var setupFile = Application.ExecutablePath;
		var fileName = Path.GetFileNameWithoutExtension(setupFile).ToLower();

		uninstall = args.ElementAtOrDefault(0) == "uninstall";

		if (!uninstall && fileName == "uninstall")
		{
			var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), $"Skyve Uninstall.exe");

			Directory.CreateDirectory(Path.GetDirectoryName(tempPath));

			File.Copy(Application.ExecutablePath, tempPath, true);

			Process.Start(new ProcessStartInfo(tempPath) 
			{
				Verb = isAdmin ? "" : "runas",
				Arguments = "uninstall" 
			});

			return true;
		}

		if (!isAdmin)
		{
			Process.Start(new ProcessStartInfo(setupFile) { Verb = "runas" });

			return true;
		}

		return false;
	}

	[System.Runtime.InteropServices.DllImport("user32.dll")]
	private static extern bool SetProcessDPIAware();
}