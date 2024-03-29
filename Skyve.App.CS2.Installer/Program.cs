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

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		var fileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath).ToLower();
		
		if (fileName == "uninstall")
		{
			var tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.exe");

			File.Copy(Application.ExecutablePath, tempPath, true);

			Process.Start(tempPath);

			return;
		}

		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		if (Environment.OSVersion.Version.Major >= 6)
		{
			SetProcessDPIAware();
		}

		Application.Run(new InstallingForm(Guid.TryParse(fileName, out _)));
	}

	[System.Runtime.InteropServices.DllImport("user32.dll")]
	private static extern bool SetProcessDPIAware();
}