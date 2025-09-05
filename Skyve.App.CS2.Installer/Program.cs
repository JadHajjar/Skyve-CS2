using Extensions;

using SlickControls;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Skyve.App.CS2.Installer;

internal static class Program
{
	[STAThread]
	private static void Main(string[] args)
	{
		try
		{
#if STEAM
			DoSilentInstall();
#else
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
#endif
		}
		catch (Exception ex)
		{
#if STEAM
			SetProcessDPIAware();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
#endif
			if (ex is KnownException)
			{
				MessagePrompt.Show(ex.InnerException, ex.Message, icon: PromptIcons.Error);
			}
			else
			{
				MessagePrompt.Show(ex, "Something unexpected went wrong while installing Skyve:");
			}
		}
	}

	private static async void DoSilentInstall()
	{
		Installer.SetInstallSettings(Application.StartupPath, false, true);

		if (Path.GetFileNameWithoutExtension(Application.ExecutablePath).Equals("uninstall", StringComparison.InvariantCultureIgnoreCase))
		{
			if (!WinExtensionClass.IsAdministrator)
				throw new KnownException(new AccessViolationException(), "You must run the uninstaller as Administrator");

			await Installer.UnInstall();
		}
		else
		{
			if (!WinExtensionClass.IsAdministrator)
				throw new KnownException(new AccessViolationException(), "You must run the setup as Administrator");

			await Installer.Install();
		}
	}

#if !STEAM
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
#endif

	[System.Runtime.InteropServices.DllImport("user32.dll")]
	private static extern bool SetProcessDPIAware();
}