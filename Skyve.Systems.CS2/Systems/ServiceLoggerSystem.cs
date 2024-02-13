using Extensions;

namespace Skyve.Systems.CS2.Systems;

public class ServiceLoggerSystem(Skyve.Domain.Systems.ISettings _, SaveHandler saveHandler) : LoggerSystem("SkyveService", saveHandler)
{
}
