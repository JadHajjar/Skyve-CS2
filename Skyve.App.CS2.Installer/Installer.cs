﻿using Extensions;

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

namespace Skyve.App.CS2.Installer;
public class Installer
{
	private const string REG_KEY = "SkyveAppCs2";
	private const string INSTALL_PATH = @"C:\Program Files\Skyve CS-II";

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
			targetFolder.Delete(true);
		}
		catch { }

		await Task.Delay(100);

		originalPath.CopyAll(targetFolder);

		await Task.Delay(150);

		File.Copy(Application.ExecutablePath, uninstallPath, true);

		ExtensionClass.CreateShortcut(shortcutPath, exePath);

		CreateUninstallRegistry(exePath, uninstallPath);

		Process.Start(exePath);

		try
		{
			await RegisterService(false);
		}
		catch { }
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
		var filePath = Path.Combine(Path.GetTempPath(), "SkyveBatch.bat");

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

		File.Delete(filePath);

		File.Delete(Path.Combine(Application.StartupPath, "InstallUtil.InstallLog"));
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

	private static string FormatPath(string path)
	{
		return $"\"{path.Replace("\\", "\\\\").Replace("/", "\\\\")}\"";
	}
}
