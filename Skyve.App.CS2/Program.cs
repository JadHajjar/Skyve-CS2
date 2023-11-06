using Microsoft.Extensions.DependencyInjection;

using Skyve.App.CS2.Services;
using Skyve.App.Interfaces;
using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Systems;
using Skyve.Systems.CS2;
using Skyve.Systems.CS2.Utilities;

using System.Configuration;
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

		ISave.AppName = "Skyve-CS2";
		ISave.CustomSaveDirectory = CurrentDirectory;

		ServiceCenter.Provider = BuildServices();
	}

	private static IServiceProvider BuildServices()
	{
		var services = new ServiceCollection();

		services.AddSkyveSystems();

		services.AddCs2SkyveSystems();

		services.AddSingleton<IInterfaceService, InterfaceService>();

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
				var folder = GetFolderPath(SpecialFolder.LocalApplicationData);

				Directory.CreateDirectory(Path.Combine(folder, ISave.AppName));

				if (Directory.Exists(Path.Combine(folder, ISave.AppName)))
				{
					ISave.CustomSaveDirectory = folder;
				}
			}
			catch { }

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

			if (CommandUtil.NoWindow)
			{
				ServiceCenter.Get<ILogger>().Info("[Console] Running without UI window");
				ServiceCenter.Get<ICentralManager>().Start();
				return;
			}

			SlickCursors.Initialize();
			Locale.Load();
			LocaleCR.Load();
			LocaleSlickUI.Load();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (OSVersion.Version.Major == 6)
			{
				SetProcessDPIAware();
			}

			if (!ServiceCenter.Get<ISettings>().SessionSettings.FirstTimeSetupCompleted && string.IsNullOrEmpty(ConfigurationManager.AppSettings[nameof(ILocationManager.GamePath)]))
			{
				if (MessagePrompt.Show(Locale.FirstSetupInfo, Locale.SetupIncomplete, PromptButtons.OKIgnore, PromptIcons.Hand) == DialogResult.OK)
				{
					return;
				}
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
