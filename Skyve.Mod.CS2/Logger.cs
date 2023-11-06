using Colossal.Logging;

using System;
using System.Diagnostics;

namespace Skyve.Mod.CS2;

public static class Logger
{
	public static ILog ILogger = LogManager.GetLogger("Enter the name of your mods log file here"); //Log file location %AppData%\..\LocalLow\Colossal Order\Cities Skylines II\Logs
	[Conditional("DEBUG")]
	public static void LogDebugInfo(string message)
	{
		ILogger.Log(Colossal.Logging.Level.Debug, message, null);
	}
	public static void LogInfo(string message)
	{
		ILogger.Log(Colossal.Logging.Level.Debug, message, null);
	}
	public static void LogException(string message, Exception e)
	{
		ILogger.Log(Colossal.Logging.Level.Error, message, e);
	}
}