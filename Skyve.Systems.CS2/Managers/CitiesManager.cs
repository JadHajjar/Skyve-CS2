using Extensions;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Game;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Timers;

namespace Skyve.Systems.CS2.Managers;
internal class CitiesManager : ICitiesManager
{
	private readonly ISettings _settings;
	private readonly ILogger _logger;
	private readonly IIOUtil _iOUtil;
	private readonly ILocationService _locationManager;
	private readonly IServiceProvider _serviceProvider;

	public event MonitorTickDelegate? MonitorTick;

	public event Action<bool>? LaunchingStatusChanged;

	public string GameVersion { get; }

	public CitiesManager(ILogger logger, ILocationService locationManager, ISettings settings, IIOUtil iOUtil, IServiceProvider serviceProvider, INotificationsService notificationsService)
	{
		_logger = logger;
		_locationManager = locationManager;
		_iOUtil = iOUtil;
		_serviceProvider = serviceProvider;
		_settings = settings;

		var citiesMonitorTimer = new Timer(1000);

		citiesMonitorTimer.Elapsed += CitiesMonitorTimer_Elapsed;
		citiesMonitorTimer.Start();

		var launcherSettings = CrossIO.Combine(settings.FolderSettings.GamePath, "Launcher", "launcher-settings.json");

		if (CrossIO.FileExists(launcherSettings))
		{
			try
			{
				GameVersion = (JsonConvert.DeserializeObject(File.ReadAllText(launcherSettings)) as JObject)?.Value<string>("version") ?? string.Empty;

				if (File.GetLastWriteTime(launcherSettings) > DateTime.Now.AddDays(-7))
				{
					notificationsService.SendNotification(new GamePatchNotification(File.GetLastWriteTime(launcherSettings), GameVersion));
				}
			}
			catch
			{
				GameVersion = string.Empty;
			}
		}
		else
		{
			{
				GameVersion = string.Empty;
			}
		}
	}

	private void CitiesMonitorTimer_Elapsed(object sender, ElapsedEventArgs e)
	{
		MonitorTick?.Invoke(IsAvailable(), IsRunning());
	}

	public bool IsAvailable()
	{
		var playsetManager = _serviceProvider.GetService<IPlaysetManager>();
		var file = IsExeLaunch((playsetManager?.CurrentCustomPlayset as ExtendedPlayset)?.LaunchSettings)
			? _locationManager.CitiesPathWithExe
			: _locationManager.SteamPathWithExe;

		return CrossIO.FileExists(file);
	}

	public async void Launch()
	{
		var workshopService = _serviceProvider.GetService<IWorkshopService>();

		if (!workshopService!.IsReady)
		{
			switch (MessagePrompt.Show(LocaleCS2.SyncOngoingLaunchGame, LocaleCS2.SyncOngoing, PromptButtons.YesNoCancel, PromptIcons.Hand))
			{
				case System.Windows.Forms.DialogResult.Cancel:
					return;
				case System.Windows.Forms.DialogResult.Yes:
					_logger.Info("Waiting for Synchronize to finish before launching the game");
					await workshopService.WaitUntilReady();
					break;
			}
		}
		else if (_settings.UserSettings.SyncBeforeLaunching)
		{
			_logger.Info("Running Synchronize before launching the game");

			await workshopService.RunSync();
		}

		var playsetManager = _serviceProvider.GetService<IPlaysetManager>();

		if (playsetManager!.CurrentPlayset is null)
		{
			switch (MessagePrompt.Show(LocaleCS2.StartingWithNoPlayset, Locale.NoActivePlayset, PromptButtons.OKCancel, PromptIcons.Hand))
			{
				case System.Windows.Forms.DialogResult.Cancel:
					return;
			}
		}

		try
		{
			if (!_settings.UserSettings.AdvancedLaunchOptions)
			{
				CleanupData();
			}
		}
		catch { }

		var args = GetCommandArgs(playsetManager);
		var file = IsExeLaunch((playsetManager?.CurrentCustomPlayset as ExtendedPlayset)?.LaunchSettings)
			? _locationManager.CitiesPathWithExe
			: _locationManager.SteamPathWithExe;

		_iOUtil.Execute(file, string.Join(" ", args));
	}

	private void CleanupData()
	{
		var logDir = new DirectoryInfo(CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Logs"));

		if (logDir.Exists)
		{
			logDir.Delete(true);
		}
	}

	private IEnumerable<string> GetCommandArgs(IPlaysetManager? playsetManager)
	{
		var launchOptions = (playsetManager?.CurrentCustomPlayset as ExtendedPlayset)?.LaunchSettings;

		if (!IsExeLaunch(launchOptions))
		{
			yield return "-applaunch 949230";
		}

		if (launchOptions is null)
		{
			yield break;
		}

		var launchSettings = launchOptions.Value;

		if (launchSettings.HideUserSection)
		{
			yield return "--disableUserSection";
		}

		if (launchSettings.NoAssets)
		{
			yield return "--disableAssets";
		}

		if (launchSettings.NoMods)
		{
			yield return "--disableModding";
		}

		if (launchSettings.DisableBurstCompile)
		{
			yield return "--burst-disable-compilation";
		}

		if (launchSettings.DeveloperMode)
		{
			yield return "--developerMode";
		}

		if (launchSettings.UIDeveloperMode)
		{
			yield return "--uiDeveloperMode";
		}

		if (!string.IsNullOrEmpty(launchSettings.LogLevel) && !launchSettings.LogLevel.Equals("default", StringComparison.InvariantCultureIgnoreCase))
		{
			yield return "--logsEffectiveness=" + launchSettings.LogLevel;
		}

		if (launchSettings.LogsToPlayerLog)
		{
			yield return "--duplicateLogToDefault";
		}

		if (!string.IsNullOrWhiteSpace(launchSettings.CustomArgs))
		{
			yield return launchSettings.CustomArgs!;
		}
	}

	private bool IsExeLaunch(GameLaunchOptions? launchOptions)
	{
		return launchOptions?.UseCitiesExe == true || _settings.FolderSettings.GamingPlatform != Skyve.Domain.Enums.GamingPlatform.Steam;
	}

	public void RunStub()
	{
		string[] args = [.. GetCommandArgs(null), "--stub"];
		var file = IsExeLaunch(null)
			? _locationManager.CitiesPathWithExe
			: _locationManager.SteamPathWithExe;

		_iOUtil.Execute(file, string.Join(" ", args));
	}

	public void RunSafeMode()
	{
		var playsetManager = _serviceProvider.GetService<IPlaysetManager>();
		string[] args = [.. GetCommandArgs(playsetManager), "--burst-disable-compilation", "--logsEffectiveness=DEBUG"];
		var file = IsExeLaunch(null)
			? _locationManager.CitiesPathWithExe
			: _locationManager.SteamPathWithExe;

		_iOUtil.Execute(file, string.Join(" ", args));
	}

	public bool IsRunning()
	{
		try
		{
			return CrossIO.CurrentPlatform is Platform.Windows && Process.GetProcessesByName("Cities2").Length > 0;
		}
		catch
		{
			return false;
		}
	}

	public void Kill()
	{
		try
		{
			foreach (var proc in Process.GetProcessesByName("Cities2"))
			{
				KillProcess(proc);
			}

			_logger.Info("Kill C:S II successful");
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to kill C:S II");
		}
	}

	private void KillProcess(Process proc)
	{
		if ((DateTime.Now - proc.StartTime).TotalSeconds < 30)
		{
			proc.Kill();
			return;
		}

		proc.CloseMainWindow();

		System.Threading.Thread.Sleep(10_000);

		if (!proc.HasExited)
		{
			proc.Kill();
		}
	}

	public void SetLaunchingStatus(bool launching)
	{
		LaunchingStatusChanged?.Invoke(launching);
	}
}