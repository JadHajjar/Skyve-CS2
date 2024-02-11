using Extensions;

using System.IO;
using System.ServiceProcess;

using static System.Environment;

namespace Skyve.Service.CS2;

internal static class Program
{
	static Program()
	{
		AppDataPath = Path.Combine(Path.GetDirectoryName(GetFolderPath(SpecialFolder.ApplicationData)), "LocalLow", "Colossal Order", "Cities Skylines II");

		ISave.CustomSaveDirectory = Path.Combine(AppDataPath, "ModsData");
		ISave.AppName = "Skyve";
	}

	public static string AppDataPath { get; }

	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	private static void Main()
	{
		ServiceBase.Run([new SkyveService()]);
	}
}
