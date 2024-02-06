using Microsoft.Extensions.DependencyInjection;

using Skyve.App.CS2.Services;
using Skyve.App.Interfaces;
using Skyve.Domain.CS2.Utilities;
using Skyve.Systems.CS2;
using Skyve.Systems.CS2.Utilities;

using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using static System.Environment;

namespace Skyve.App.CS2;
#nullable disable
internal static class Program
{
	static Program()
	{
		App.Program.IsRunning = true;
		App.Program.CurrentDirectory = Application.StartupPath;
		App.Program.ExecutablePath = Application.ExecutablePath;
		App.Program.AppDataPath = Path.Combine(Path.GetDirectoryName(GetFolderPath(SpecialFolder.ApplicationData)), "LocalLow", "Colossal Order", "Cities Skylines II");

		ISave.CustomSaveDirectory = Path.Combine(App.Program.AppDataPath, "ModsData");
		ISave.AppName = "Skyve";

		ServiceCenter.Provider = BuildServices();
	}

	private static IServiceProvider BuildServices()
	{
		var services = new ServiceCollection();

		services.AddSkyveSystems();

		services.AddCs2SkyveSystems();

		services.AddSingleton<IInterfaceService, InterfaceService>();
		services.AddSingleton<IAppInterfaceService, InterfaceService>();
		services.AddSingleton<IRightClickService, RightClickService>();

		return services.BuildServiceProvider();
	}

	[STAThread]
	private static void Main(string[] args)
	{
		try
		{
			if (CommandUtil.Parse(args))
			{
				return;
			}

			try
			{
				var openTools = !CommandUtil.NoWindow && !Debugger.IsAttached && Process.GetProcessesByName(Path.GetFileNameWithoutExtension(App.Program.ExecutablePath)).Length > 1;

				if (openTools && !CrossIO.FileExists(CrossIO.Combine(CurrentDirectory, "Wake")))
				{
					File.WriteAllText(Path.Combine(CurrentDirectory, "Wake"), "It's time to wake up");

					return;
				}

				CrossIO.DeleteFile(CrossIO.Combine(CurrentDirectory, "Wake"));
			}
			catch { }

			BackgroundAction.BackgroundTaskError += BackgroundAction_BackgroundTaskError;

			try
			{
				Locale.Load();
				LocaleCR.Load();
				LocaleCRNotes.Load();
				LocaleSlickUI.Load();
				LocaleCS2.Load();
			}
			catch (Exception ex)
			{
#if DEBUG
				throw ex;
#else
				ServiceCenter.Get<ILogger>().Exception(ex, "Localization Failed to Initialize");
#endif
			}

			if (CommandUtil.NoWindow)
			{
				ServiceCenter.Get<ILogger>().Info("[Console] Running without UI window");
				ServiceCenter.Get<ICentralManager>().Start();
				return;
			}

			SlickCursors.Initialize();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (OSVersion.Version.Major >= 6)
			{
				SetProcessDPIAware();
			}

			Application.Run(SystemsProgram.MainForm = App.Program.MainForm = new MainForm());
		}
		catch (Exception ex)
		{
			MessagePrompt.GetError(ex, "App failed to start", out var message, out var details);
			MessageBox.Show(details, message);
		}
	}

	private static void BackgroundAction_BackgroundTaskError(BackgroundAction b, Exception e)
	{
		ServiceCenter.Get<ILogger>().Exception(e, $"The background action ({b}) failed");
	}

	[System.Runtime.InteropServices.DllImport("user32.dll")]
	private static extern bool SetProcessDPIAware();
}
#nullable enable
