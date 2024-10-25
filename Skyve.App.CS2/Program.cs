using Microsoft.Extensions.DependencyInjection;

using Skyve.App.CS2.Services;
using Skyve.App.Interfaces;
using Skyve.Domain.CS2.Utilities;
using Skyve.Systems.CS2;
using Skyve.Systems.CS2.Systems;
using Skyve.Systems.CS2.Utilities;

using System.Diagnostics;
using System.IO;
using System.Threading;
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

		if (File.Exists(Path.Combine(GetFolderPath(SpecialFolder.LocalApplicationData), "SkyveDataPathHelper.txt")))
		{
			App.Program.AppDataPath = File.ReadAllText(Path.Combine(GetFolderPath(SpecialFolder.LocalApplicationData), "SkyveDataPathHelper.txt"));
		}
		else
		{
			App.Program.AppDataPath = Path.Combine(GetFolderPath(SpecialFolder.UserProfile), "AppData", "LocalLow", "Colossal Order", "Cities Skylines II");
		}

		SaveHandler.AppName = "Skyve";

		ServiceCenter.Provider = BuildServices();

		SystemExtensions.Initialize(ServiceCenter.Provider);
	}

	private static IServiceProvider BuildServices()
	{
		var services = new ServiceCollection();

		services.AddSkyveSystems();

		services.AddCs2SkyveSystems();

		services.AddSingleton(new SaveHandler(Path.Combine(App.Program.AppDataPath, "ModsData")));
		services.AddSingleton<ILogger, AppLoggerSystem>();
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
			HandleIncorrectLaunchLocation();

			if (args.Any(x => x.StartsWith("-createJunction") || x.StartsWith("-deleteJunction")))
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				if (OSVersion.Version.Major >= 6)
				{
					SetProcessDPIAware();
				}

				Application.Run(new PleaseWaitForm(args));

				return;
			}

			if (CommandUtil.Parse(args))
			{
				return;
			}

			if (!CommandUtil.Commands.NoWindow && !Debugger.IsAttached && IsAlreadyRunning())
			{
				if (ServiceCenter.Provider.GetService<NamedPipelineUtil>().SendToRunningInstance(args))
				{
					return;
				}
			}

			BackgroundAction.BackgroundTaskError += BackgroundAction_BackgroundTaskError;
			AppDomain.CurrentDomain.UnhandledException += GlobalUnhandledExceptionHandler;
			Application.ThreadException += GlobalThreadExceptionHandler;


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
				ServiceCenter.Provider.GetService<ILogger>().Exception(ex, "Localization failed to Initialize");
#endif
			}

			if (CommandUtil.Commands.NoWindow)
			{
				ServiceCenter.Provider.GetService<ILogger>().Info("[Console] Running without UI window");
				ServiceCenter.Provider.GetService<ICentralManager>().Start();
				return;
			}

			ServiceCenter.Provider.GetService<NamedPipelineUtil>().StartNamedPipeServer();

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
			if (OSVersion.Version.Major >= 6)
			{
				SetProcessDPIAware();
			}

			MessagePrompt.Show(ex, "App failed to start");
		}
	}

	private static void HandleIncorrectLaunchLocation()
	{
		if (App.Program.CurrentDirectory.PathContains(App.Program.AppDataPath))
		{
			if (OSVersion.Version.Major >= 6)
			{
				SetProcessDPIAware();
			}

			var setupFile = CrossIO.Combine(Path.GetDirectoryName(App.Program.CurrentDirectory), "Skyve Setup.exe");

			if (CrossIO.FileExists(setupFile))
			{
				if (MessagePrompt.Show(LocaleCS2.RunSetupOrRunApp, PromptButtons.OKCancel, PromptIcons.Hand) == DialogResult.OK)
				{
					Process.Start(new ProcessStartInfo(setupFile)
					{
						Verb = WinExtensionClass.IsAdministrator ? string.Empty : "runas"
					});
				}
			}
			else
			{
				MessagePrompt.Show(LocaleCS2.CantRunAppFromHere, PromptButtons.OK, PromptIcons.Hand);
			}

			return;
		}
	}

	private static bool IsAlreadyRunning()
	{
		return Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1;
	}

	private static void BackgroundAction_BackgroundTaskError(BackgroundAction b, Exception e)
	{
		ServiceCenter.Provider.GetService<ILogger>().Exception(e, $"The background action ({b}) failed");
	}

	private static void GlobalThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
	{
		ServiceCenter.Provider.GetService<ILogger>().Exception(e.Exception, $"CRASH");
	}

	private static void GlobalUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
	{
		ServiceCenter.Provider.GetService<ILogger>().Exception((Exception)e.ExceptionObject, $"CRASH");
	}

	[System.Runtime.InteropServices.DllImport("user32.dll")]
	private static extern bool SetProcessDPIAware();
}
#nullable enable
