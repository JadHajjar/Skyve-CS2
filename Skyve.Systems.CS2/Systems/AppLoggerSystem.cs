using Extensions;

using System;

namespace Skyve.Systems.CS2.Systems;

public class AppLoggerSystem(IServiceProvider serviceProvider, SaveHandler saveHandler) : LoggerSystem("SkyveApp", saveHandler, serviceProvider)
{
}
