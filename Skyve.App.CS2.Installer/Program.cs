using Extensions;

using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Skyve.App.CS2.Installer;

internal static class Program
{
	private const string REG_KEY = "SkyveAppCs2";

	[STAThread]
	private static void Main()
	{
		try
		{
			if (Path.GetFileNameWithoutExtension(Application.ExecutablePath).Equals("uninstall", StringComparison.CurrentCultureIgnoreCase))
			{
				UnInstall();
				return;
			}

			var targetFolder = new DirectoryInfo(@"C:\Program Files\Skyve CS-II");
			var originalPath = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ".App"));

			originalPath.CopyAll(targetFolder);

			var shortcutPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Skyve CS-II.lnk";
			var exePath = Path.Combine(targetFolder.FullName, "Skyve.exe");
			var uninstallPath = Path.Combine(targetFolder.FullName, "Uninstall.exe");

			File.Copy(Application.ExecutablePath, uninstallPath, true);

			ExtensionClass.CreateShortcut(shortcutPath, exePath);

			CreateUninstallRegistry(exePath, uninstallPath);

			Process.Start(exePath);
		}
		catch (Exception ex) { MessageBox.Show(ex.ToString()); }
	}

	private static void UnInstall()
	{
		try
		{
			foreach (var item in Process.GetProcessesByName("Skyve"))
			{
				item.Kill();
			}

			var shortcutPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Skyve CS-II.lnk";

			File.Delete(shortcutPath);

			foreach (var item in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "*.lnk"))
			{
				if (ExtensionClass.GetShortcutPath(item).PathEquals(Application.ExecutablePath))
				{
					File.Delete(item);
				}
			}

			using var parent = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);

			parent?.DeleteSubKey(REG_KEY);
		}
		catch { }

		Process.Start(new ProcessStartInfo()
		{
			Arguments = $"/C choice /C Y /N /D Y /T 1 & rmdir /s /q \"{Path.GetDirectoryName(Application.ExecutablePath)}\" & exit",
			WindowStyle = ProcessWindowStyle.Hidden,
			CreateNoWindow = true,
			WorkingDirectory = "C:\\Program Files",
			FileName = "cmd.exe"
		});
	}

	private static void CreateUninstallRegistry(string appPath, string uninstallPath)
	{
		using var parent = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true) ?? throw new Exception("Uninstall registry key not found.");

		try
		{
			RegistryKey? key = null;

			try
			{
				key = parent.OpenSubKey(REG_KEY, true);

				var isUpdate = key is not null;
				var version = Assembly.GetExecutingAssembly().GetName().Version;

				key ??= parent.CreateSubKey(REG_KEY) ?? throw new Exception(string.Format("Unable to create uninstaller '{0}'", REG_KEY));

				key.SetValue("DisplayName", "Skyve - Cities: Skylines II");
				key.SetValue("ApplicationVersion", version.ToString());
				key.SetValue("Publisher", "T. D. W.");
				key.SetValue("DisplayIcon", FormatPath(appPath));
				key.SetValue("DisplayVersion", version.ToString(2));
				key.SetValue("URLInfoAbout", "");
				key.SetValue("UninstallString", FormatPath(uninstallPath));

				if (!isUpdate)
				{
					key.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
				}
			}
			finally
			{
				key?.Close();
				key?.Dispose();
			}
		}
		catch (Exception ex)
		{
			throw new Exception("An error occurred writing uninstall information to the registry.  The service is fully installed but can only be uninstalled manually through the command line.", ex);
		}
	}

	private static string FormatPath(string path) => $"\"{path.Replace("\\", "\\\\").Replace("/", "\\\\")}\"";
}