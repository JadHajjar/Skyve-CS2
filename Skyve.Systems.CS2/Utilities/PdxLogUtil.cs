using PDX.SDK.Contracts.Enums;
using PDX.SDK.Contracts.Internal;
using PDX.SDK.Contracts.Logging;

using System.Runtime.CompilerServices;

namespace Skyve.Systems.CS2.Utilities;
public class PdxLogUtil(Skyve.Domain.Systems.ILogger logger) : ILogger
{
	public bool IsLogLevelEnabled(LogLevel logLevel)
	{
#if DEBUG
		return true;
#else
		return logLevel >= LogLevel.L2_Warning;
#endif
	}

	public void Log(string msg, LogLevel logLevel = LogLevel.L1_Debug, FlowData? flowData = null, [CallerMemberName] string? memberName = null)
	{
		switch (logLevel)
		{
#if DEBUG
			case LogLevel.L0_Info:
				logger.Info($"[PDX:{memberName}] {msg}", null, null);
				break;
			case LogLevel.L1_Debug:
				logger.Debug($"[PDX:{memberName}] {msg}", null, null);
				break;
#endif
			case LogLevel.L2_Warning:
				logger.Warning($"[PDX:{memberName}] {msg}", null, null);
				break;
			case LogLevel.L3_Error:
				logger.Error($"[PDX:{memberName}] {msg}", null, null);
				break;
			case LogLevel.L4_Fatal:
				logger.Exception($"[PDX:{memberName}] {msg}", null, null);
				break;
		}
	}
}
