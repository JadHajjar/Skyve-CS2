using Extensions;

using System;

namespace Skyve.Systems.CS2.Systems;

public class ServiceLoggerSystem(IServiceProvider serviceProvider, SaveHandler saveHandler) : LoggerSystem("SkyveService", saveHandler, serviceProvider)
{
}
