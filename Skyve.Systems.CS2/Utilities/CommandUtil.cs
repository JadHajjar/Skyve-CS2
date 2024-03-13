using Extensions;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Skyve.Systems.CS2.Utilities;
public static class CommandUtil
{
	public static string? PreSelectedPlayset { get; private set; }
	public static bool LaunchOnLoad { get; private set; }
	public static bool CloseWhenFinished { get; private set; }
	public static bool NoWindow { get; private set; }

	public static bool Parse(string[] args)
	{
		var exit = false;

		for (var i = 0; i < args.Length; i++)
		{
			if (args[i][0] != '-')
			{
				continue;
			}

			var command = args[i].Substring(1);
			var arguments = new List<string>();

			for (var j = i + 1; j < args.Length; j++)
			{
				if (args[j][0] == '-')
				{
					break;
				}

				i++;

				arguments.Add(args[j]);
			}

			exit |= RunCommand(command, arguments);
		}

		return exit;
	}

	private static bool RunCommand(string command, List<string> arguments)
	{
		if (isCommand("stub"))
		{
			Process.Start(Application.ExecutablePath);

			return true;
		}

		if (isCommand("createJunction"))
		{
			CreateJunction(arguments[0], arguments[1]);
		}

		if (isCommand("deleteJunction"))
		{
			DeleteJunction(arguments[0]);
		}

		if (isCommand("playset"))
		{
			PreSelectedPlayset = arguments[0];
		}

		if (isCommand("launch"))
		{
			LaunchOnLoad = true;
		}

		if (isCommand("closeWhenFinished"))
		{
			CloseWhenFinished = true;
		}

		if (isCommand("noWindow"))
		{
			NoWindow = true;
		}

		return false;

		bool isCommand(string cmd) => command.Equals(cmd, StringComparison.InvariantCultureIgnoreCase);
	}

	private static void CreateJunction(string appDataFolder, string targetFolder)
	{
		targetFolder = Path.Combine(targetFolder, "Cities Skylines II");

		Process.Start(new ProcessStartInfo()
		{
			Arguments = $"/C move /Y \"{appDataFolder}\" \"{targetFolder}\" & mklink /J \"{appDataFolder}\" \"{targetFolder}\" & exit",
			WindowStyle = ProcessWindowStyle.Hidden,
			UseShellExecute = false,
			CreateNoWindow = true,
			FileName = "cmd.exe"
		}).WaitForExit();
	}

	private static void DeleteJunction(string appDataFolder)
	{
		var currentTarget = GetJunctionState(appDataFolder);

		Process.Start(new ProcessStartInfo()
		{
			Arguments = $"/C rd \"{appDataFolder}\" & move /Y \"{currentTarget}\" \"{appDataFolder}\" & exit",
			WindowStyle = ProcessWindowStyle.Hidden,
			UseShellExecute = false,
			CreateNoWindow = true,
			FileName = "cmd.exe"
		}).WaitForExit();
	}

	public static string GetJunctionState(string appDataFolder)
	{
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
}
