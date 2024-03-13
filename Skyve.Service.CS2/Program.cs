using Extensions;

using System.ServiceProcess;
using System.Threading;

namespace Skyve.Service.CS2;

internal static class Program
{
	static Program()
	{
		SaveHandler.AppName = "Skyve";
	}

	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	private static void Main()
	{
#if DEBUG
		if (System.Diagnostics.Debugger.IsAttached)
		{
			new SkyveService().TestRun();

			while (true)
				Thread.Sleep(1000);
		}
#endif
		ServiceBase.Run([new SkyveService()]);
	}
}
