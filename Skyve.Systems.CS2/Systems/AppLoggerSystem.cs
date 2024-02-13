using Extensions;

namespace Skyve.Systems.CS2.Systems;

public class AppLoggerSystem(Skyve.Domain.Systems.ISettings _, SaveHandler saveHandler) : LoggerSystem("SkyveApp", saveHandler)
{
}
