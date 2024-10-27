using Extensions;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Skyve.Systems.CS2.Utilities;
public static class CommandUtil
{
	public static Options Commands { get; set; } = new();

	public static bool Parse(string[] args)
	{
		Commands = new();

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
			JunctionHelper.CreateJunction(arguments[0], arguments[1]);
		}

		if (isCommand("deleteJunction"))
		{
			JunctionHelper.DeleteJunction(arguments[0]);
		}

		if (isCommand("playset"))
		{
			Commands.PreSelectedPlayset = arguments[0];
		}

		if (isCommand("launch"))
		{
			Commands.LaunchOnLoad = true;
		}

		if (isCommand("closeWhenFinished"))
		{
			Commands.CloseWhenFinished = true;
		}

		if (isCommand("noWindow"))
		{
			Commands.NoWindow = true;
		}

		if (isCommand("restoreItem") && arguments.Count > 0)
		{
			Commands.RestoreBackup = arguments[0];
		}

		if (isCommand("cmd") && arguments.Count > 0)
		{
			Commands.CommandActions = arguments[0].Remove("skyve://").Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);
		}

		return false;

		bool isCommand(string cmd) => command.Equals(cmd, StringComparison.InvariantCultureIgnoreCase);
	}

	public class Options
	{
		public string? PreSelectedPlayset { get; set; }
		public bool LaunchOnLoad { get; set; }
		public bool CloseWhenFinished { get; set; }
		public bool NoWindow { get; set; }
		public string[] CommandActions { get; set; } = [];
		public string? RestoreBackup { get; set; }
	}
}
