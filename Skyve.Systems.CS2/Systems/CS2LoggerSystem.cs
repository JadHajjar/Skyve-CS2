using PDX.SDK.Contracts.Enums;
using PDX.SDK.Contracts.Internal;
using PDX.SDK.Contracts.Logging;

namespace Skyve.Systems.CS2.Systems;
internal class CS2LoggerSystem : LoggerSystem, ILogger
{
	public CS2LoggerSystem(Domain.Systems.ISettings settings) : base(settings)
	{ }

	public void Log(string msg, LogLevel logLevel = LogLevel.L1_Debug, FlowData? flowData = null)
	{
		switch (logLevel)
		{
#if DEBUG
			case LogLevel.L0_Info:
				Info($"[PDX] {msg}");
				break;
			case LogLevel.L1_Debug:
				Debug($"[PDX] {msg}");
				break;
#endif
			case LogLevel.L2_Warning:
				Warning($"[PDX] {msg}");
				break;
			case LogLevel.L3_Error:
				Error($"[PDX] {msg}");
				break;
			case LogLevel.L4_Fatal:
				ProcessLog("FATAL", $"[PDX] {msg}");
				break;
		}
	}
}
