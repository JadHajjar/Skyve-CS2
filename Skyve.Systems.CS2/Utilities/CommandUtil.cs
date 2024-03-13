using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			JunctionHelper.CreateJunction(arguments[0], arguments[1]);
		}

		if (isCommand("deleteJunction"))
		{
			JunctionHelper.DeleteJunction(arguments[0]);
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
}
