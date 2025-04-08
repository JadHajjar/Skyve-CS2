using Extensions;

using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Environment;

namespace Skyve.App.CS2.Installer;
public class Installer
{
	private const string REG_KEY = "SkyveAppCs2";
	private static string INSTALL_PATH = @"C:\Program Files\Skyve CS-II";
	private static bool DesktopShortcut;
	private static bool InstallService;

	public static async Task Install()
	{
		await KillRunningApps();

		var targetFolder = new DirectoryInfo(INSTALL_PATH);
		var originalPath = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), ".App"));
		var shortcutPath = "C:\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Skyve CS-II.lnk";
		var exePath = Path.Combine(targetFolder.FullName, "Skyve.exe");
		var servicePath = Path.Combine(targetFolder.FullName, "Skyve.Service.exe");
		var uninstallPath = Path.Combine(targetFolder.FullName, "Uninstall.exe");

		try
		{
			if (!Directory.Exists(Path.Combine(INSTALL_PATH, "Cities Skylines II")))
			{
				targetFolder.Delete(true);
			}
		}
		catch { }

		await Task.Delay(100);

		try
		{
			originalPath.CopyAll(targetFolder);
		}
		catch (Exception ex)
		{
			throw new KnownException(ex, $"Skyve could not copy the files to this folder:\r\n\"{targetFolder.FullName}\"\r\n\r\nTry selecting another location to install Skyve.");
		}

		await Task.Delay(150);

		File.Copy(Application.ExecutablePath, uninstallPath, true);

		CreateUninstallRegistry(exePath, uninstallPath);

		ExtensionClass.CreateShortcut(shortcutPath, exePath);

		if (DesktopShortcut)
		{
			ExtensionClass.CreateShortcut(Path.Combine(GetFolderPath(SpecialFolder.Desktop), "Skyve CS-II.lnk"), exePath);
		}

		try
		{
			File.WriteAllText(Path.Combine(Path.GetDirectoryName(GetFolderPath(SpecialFolder.ApplicationData)), "LocalLow", "Colossal Order", "Cities Skylines II", "ModsSettings", "Skyve", "InstallPath"), targetFolder.FullName);
		}
		catch { }

		try
		{
			Process.Start(exePath);
		}
		catch (Exception ex)
		{
			throw new KnownException(ex, "The skyve app could not be found. It is likely an anti-virus software removed it.\r\n\r\nApp location: " + exePath);
		}

		try
		{
			await RegisterService(!InstallService);
		}
		catch { }

		RegisterCustomProtocol("Skyve", exePath);

		AssociateFileType(exePath);
	}

	public static async Task RegisterService(bool uninstall)
	{
		var servicePath = Path.Combine(INSTALL_PATH, "Skyve.Service.exe");

		var service = ServiceController.GetServices().FirstOrDefault(x => x.ServiceName == "Skyve.Service");

		if (service != null)
		{
			if (service.CanShutdown)
			{
				service.Stop();

				while (service.Status != ServiceControllerStatus.Stopped)
				{
					service.Refresh();

					await Task.Delay(100);
				}
			}

			// Un-Install Service
			Run($"C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\InstallUtil /u \"{servicePath}\"");

			await Task.Delay(100);

			if (ServiceController.GetServices().FirstOrDefault(x => x.ServiceName == "Skyve.Service") != null)
			{
				return;
			}
		}

		if (uninstall)
		{
			return;
		}

		// Install Service
		Run($"C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\InstallUtil /i \"{servicePath}\"");

		await Task.Delay(100);

		service = ServiceController.GetServices().FirstOrDefault(x => x.ServiceName == "Skyve.Service");

		if (service == null)
		{
			return;
		}

		if (service.CanShutdown)
		{
			service.Stop();

			while (service.Status != ServiceControllerStatus.Stopped)
			{
				service.Refresh();
				Thread.Sleep(100);
			}
		}

		service.Start();
	}

	private static void Run(string command)
	{
		var filePath = Path.ChangeExtension(CrossIO.GetTempFileName(), "bat");

		File.WriteAllText(filePath, command);

		var p = new Process();
		// Redirect the output stream of the child process.
		p.StartInfo.UseShellExecute = false;
		p.StartInfo.RedirectStandardOutput = true;
		p.StartInfo.CreateNoWindow = true;
		p.StartInfo.FileName = filePath;
		p.Start();
		// Do not wait for the child process to exit before
		// reading to the end of its redirected stream.
		// p.WaitForExit();
		// Read the output stream first and then wait.
		var output = p.StandardOutput.ReadToEnd();
		p.WaitForExit();

		try
		{
			File.Delete(filePath);

			File.Delete(Path.Combine(Application.StartupPath, "InstallUtil.InstallLog"));
		}
		catch { }
	}

	public static async Task UnInstall()
	{
		await KillRunningApps();

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

		await RegisterService(true);

		Directory.Delete(INSTALL_PATH, true);

		Process.Start(new ProcessStartInfo()
		{
			Arguments = $"/C choice /C Y /N /D Y /T 2 & rmdir /s /q \"{Application.ExecutablePath}\" & exit",
			WindowStyle = ProcessWindowStyle.Hidden,
			CreateNoWindow = true,
			WorkingDirectory = Path.GetTempPath(),
			FileName = "cmd.exe"
		});
	}

	private static async Task KillRunningApps()
	{
		var service = ServiceController.GetServices().FirstOrDefault(x => x.ServiceName == "Skyve.Service");

		if (service != null && service.CanShutdown)
		{
			service.Stop();

			while (service.Status != ServiceControllerStatus.Stopped)
			{
				service.Refresh();

				await Task.Delay(100);
			}
		}

		if (!Directory.Exists(INSTALL_PATH))
		{
			return;
		}

		for (var i = 0; i < 15; i++)
		{
			foreach (var file in Directory.GetFiles(INSTALL_PATH, "*.exe"))
			{
				foreach (var process in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(file)))
				{
					process.Kill();
				}
			}

			var areFilesLocked = new DirectoryInfo(INSTALL_PATH)
				.GetFiles("*.*", SearchOption.AllDirectories)
				.Any(IsFileLocked);

			if (!areFilesLocked)
			{
				return;
			}

			await Task.Delay(500);
		}
	}

	private static bool IsFileLocked(FileInfo file)
	{
		FileStream? stream = null;

		try
		{
			stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
		}
		catch (IOException)
		{
			//the file is unavailable because it is:
			//still being written to
			//or being processed by another thread
			//or does not exist (has already been processed)
			return true;
		}
		finally
		{
			stream?.Close();
		}

		//file is not locked
		return false;
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
#if STABLE
				key.SetValue("URLInfoAbout", "https://mods.paradoxplaza.com/mods/75804/Windows/");
#else
				key.SetValue("URLInfoAbout", "https://mods.paradoxplaza.com/mods/75804/Windows/");
#endif
				key.SetValue("UninstallString", FormatPath(uninstallPath));
				key.SetValue("InstallBackgroundService", InstallService);

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

	public static string? GetCurrentInstallationPath()
	{
		try
		{
			using var parent = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true) ?? throw new Exception("Uninstall registry key not found.");

			RegistryKey? key = null;

			try
			{
				key = parent.OpenSubKey(REG_KEY, true);

				if (key is null)
				{
					return null;
				}

				var path = key.GetValue("UninstallString")?.ToString();

				if (string.IsNullOrEmpty(path))
				{
					return null;
				}

				InstallService = !bool.TryParse(key.GetValue("InstallBackgroundService")?.ToString(), out var install) || install;

				path = Path.GetDirectoryName(path!.Trim('"').Replace("\\\\", "\\"));

				if (Directory.Exists(path))
				{
					return INSTALL_PATH = path;
				}

				return null;
			}
			finally
			{
				key?.Close();
				key?.Dispose();
			}
		}
		catch
		{
			return null;
		}
	}

	public static void RegisterCustomProtocol(string protocolName, string executablePath)
	{
		try
		{
			// Create the root key for the custom protocol
			using var protocolKey = Registry.ClassesRoot.CreateSubKey(protocolName);
			if (protocolKey == null)
			{
				throw new Exception("Failed to create registry key.");
			}

			protocolKey.SetValue("", $"{protocolName} Protocol");
			protocolKey.SetValue("URL Protocol", "");

			// Create the shell\open\command subkey
			using var shellKey = protocolKey.CreateSubKey("shell");
			using var openKey = shellKey.CreateSubKey("open");
			using var commandKey = openKey.CreateSubKey("command") ?? throw new Exception("Failed to create command registry key.");

			commandKey.SetValue("", $"\"{executablePath}\" -cmd \"%1\"");
		}
		catch
		{
			//throw new Exception(ex, $"Error registering protocol: {ex.Message}");
		}
	}

	public static void AssociateFileType(string executablePath)
	{
		try
		{
			var extension = ".sbak";
			var progId = "Skyve.sbakfile";

			// Register file extension
			using (var key = Registry.ClassesRoot.CreateSubKey(extension))
			{
				key?.SetValue("", progId);
				key?.SetValue("Content Type", "application/x-zip-compressed");
				key?.SetValue("DontCompressInPackage", "");
				key?.SetValue("PerceivedType", "compressed");
			}

			// Register ProgID for the application
			using (var key = Registry.ClassesRoot.CreateSubKey(progId))
			{
				if (key != null)
				{
					key.SetValue("", "Skyve Backup");

					using (var iconKey = key.CreateSubKey("DefaultIcon"))
					{
						iconKey?.SetValue("", $"\"{Path.GetDirectoryName(executablePath)}\\SkyveBackupFile.ico\"");
					}

					using var shellKey = key.CreateSubKey(@"shell\open\command");
					shellKey?.SetValue("", $"\"{executablePath}\" -restoreItem \"%1\"");
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to associate file type: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}

	public static void SetInstallSettings(string path, bool desktopShortcut, bool installService)
	{
		INSTALL_PATH = path;
		DesktopShortcut = desktopShortcut;
		InstallService = installService;
	}

	private static string FormatPath(string path)
	{
		return $"\"{path.Replace("\\", "\\\\").Replace("/", "\\\\")}\"";
	}
}
