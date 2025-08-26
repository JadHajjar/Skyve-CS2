using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Skyve.Systems.CS2.Utilities;
public static class JunctionHelper
{
	public static void CreateJunction(string appDataFolder, string targetFolder)
	{
		DeleteJunction(appDataFolder, false);

		targetFolder = Path.Combine(targetFolder, "Cities Skylines II");

		Process.Start(new ProcessStartInfo()
		{
			Arguments = $"/C xcopy /E /I /Y \"{appDataFolder}\" \"{targetFolder}\" && rmdir /S /Q \"{appDataFolder}\" && mklink /J \"{appDataFolder}\" \"{targetFolder}\" & exit",
			WindowStyle = ProcessWindowStyle.Hidden,
			UseShellExecute = false,
			CreateNoWindow = true,
			FileName = "cmd.exe"
		}).WaitForExit();

		new DirectoryInfo(targetFolder).Attributes |= FileAttributes.System | FileAttributes.ReadOnly;

		StartService();
	}

	public static void DeleteJunction(string appDataFolder, bool startService = true)
	{
		var currentTarget = GetJunctionState(appDataFolder);

		KillRunningApps();

		if (!string.IsNullOrEmpty(currentTarget))
		{
			Process.Start(new ProcessStartInfo()
			{
				Arguments = $"/C rd \"{appDataFolder}\" && xcopy /E /I /Y \"{currentTarget}\" \"{appDataFolder}\" && rmdir /S /Q \"{currentTarget}\" & exit",
				WindowStyle = ProcessWindowStyle.Hidden,
				UseShellExecute = false,
				CreateNoWindow = true,
				FileName = "cmd.exe"
			}).WaitForExit();
		}

		if (startService)
		{
			StartService();
		}
	}

	public static string GetJunctionState(string appDataFolder)
	{
		if (!Directory.Exists(appDataFolder))
		{
			return string.Empty;
		}

		var p = Process.Start(new ProcessStartInfo()
		{
			Arguments = $"/C dir \"{Path.GetDirectoryName(appDataFolder)}\" /al /s & exit",
			WindowStyle = ProcessWindowStyle.Hidden,
			RedirectStandardOutput = true,
			UseShellExecute = false,
			CreateNoWindow = true,
			FileName = "cmd.exe"
		});

		var output = p.StandardOutput.ReadToEnd();

		p.WaitForExit();

		var matches = Regex.Matches(output, @"<junction>\s+(.+?) \[(.+?)\]", RegexOptions.IgnoreCase);

		foreach (Match item in matches)
		{
			if (item.Groups[1].Value.Equals("Cities Skylines II", StringComparison.InvariantCultureIgnoreCase))
			{
				return item.Groups[2].Value;
			}
		}

		return string.Empty;
	}

	public static void KillRunningApps()
	{
		foreach (var proc in Process.GetProcessesByName("Cities2"))
		{
			proc.Kill();
		}

		var service = ServiceController.GetServices().FirstOrDefault(x => x.ServiceName == "Skyve.Service");

		if (service != null && service.CanShutdown)
		{
			service.Stop();

			while (service.Status != ServiceControllerStatus.Stopped)
			{
				service.Refresh();

				Thread.Sleep(100);
			}
		}

		foreach (var file in Directory.GetFiles(Application.StartupPath, "*.exe"))
		{
			foreach (var process in Process.GetProcessesByName(Path.GetFileNameWithoutExtension(file)))
			{
				if (process.Id != Process.GetCurrentProcess().Id)
				{
					process.Kill();
				}
			}
		}

		Thread.Sleep(500);
	}

	private static void StartService()
	{
		var service = ServiceController.GetServices().FirstOrDefault(x => x.ServiceName == "Skyve.Service");

		if (service != null && !service.CanShutdown)
		{
			service.Start();
		}
	}
}
