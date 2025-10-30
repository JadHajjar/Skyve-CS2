using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.IO;

namespace Skyve.App.CS2.Installer
{
	public static class InstallHelper
	{
		public const string REG_KEY = "SkyveAppCs2";

		public static void Run(string setupPath)
		{
			var folder = Path.Combine(Path.GetTempPath(), $"Skyve-Setup-{Guid.NewGuid()}");
			var workingDirectory = Path.GetDirectoryName(setupPath);

			Directory.CreateDirectory(folder);

			File.Copy(setupPath, Path.Combine(folder, Path.GetFileName(setupPath)));

			CopyAll(new(Path.Combine(workingDirectory, ".App")), new(Path.Combine(folder, ".App")));

			Process.Start(Path.Combine(folder, Path.GetFileName(setupPath)));
		}

		private static void CopyAll(this DirectoryInfo directory, DirectoryInfo target)
		{
			if (!directory.Exists)
			{
				return;
			}

			target.Create();

			//Now Create all of the directories
			foreach (var dirPath in Directory.GetDirectories(directory.FullName, "*", SearchOption.AllDirectories))
			{
				Directory.CreateDirectory(dirPath.Replace(directory.FullName, target.FullName));
			}

			//Copy all the files & Replaces any files with the same name
			foreach (var newPath in Directory.GetFiles(directory.FullName, "*.*", SearchOption.AllDirectories))
			{
				File.Copy(newPath, newPath.Replace(directory.FullName, target.FullName), true);
			}
		}

#nullable enable
		public static string? GetCurrentInstallationPath(out bool serviceInstalled)
		{
			serviceInstalled = false;

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

					serviceInstalled = !bool.TryParse(key.GetValue("InstallBackgroundService")?.ToString(), out var install) || install;

					path = Path.GetDirectoryName(path!.Trim('"').Replace("\\\\", "\\"));

					if (Directory.Exists(path))
					{
						return path;
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
#nullable restore
	}
}