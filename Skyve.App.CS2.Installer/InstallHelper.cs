using System;
using System.Diagnostics;
using System.IO;

namespace Skyve.App.CS2.Installer
{
	public static class InstallHelper
	{
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
	}
}