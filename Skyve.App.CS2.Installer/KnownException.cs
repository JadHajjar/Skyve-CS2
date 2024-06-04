using System;

namespace Skyve.App.CS2.Installer;
internal class KnownException(Exception exception, string message) : Exception(message, exception)
{
}
