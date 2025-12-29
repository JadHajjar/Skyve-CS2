using PDX.SDK.Contracts.Enums;
using PDX.SDK.Contracts.Logging;

namespace Skyve.Systems.CS2.Utilities;
public class PdxLogUtil(Skyve.Domain.Systems.ILogger logger) : ILogger
{
	public void Log(string msg, LogLevel logLevel = LogLevel.L1_Debug)
	{
		switch (logLevel)
		{
			case LogLevel.L0_Info:
				logger.Info($"[PDX] {msg}", null, null);
				break;
#if DEBUG
			case LogLevel.L1_Debug:
				logger.Debug($"[PDX] {msg}", null, null);
				break;
#endif
			case LogLevel.L2_Warning:
				logger.Warning($"[PDX] {msg}", null, null);
				break;
			case LogLevel.L3_Error:
				logger.Error($"[PDX] {msg}", null, null);
				break;
			case LogLevel.L4_Fatal:
				logger.Exception($"[PDX] {msg}", null, null);
				break;
		}
	}
}
