using Extensions;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
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
		var file = (playsetManager?.CurrentCustomPlayset as ExtendedPlayset)?.LaunchSettings.UseCitiesExe == true || _settings.FolderSettings.GamingPlatform != Skyve.Domain.Enums.GamingPlatform.Steam
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
					await workshopService.WaitUntilReady();
					break;
			}
		}
		else if (_settings.UserSettings.SyncBeforeLaunching)
		{
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

		var args = GetCommandArgs(playsetManager);
		var file = (playsetManager?.CurrentCustomPlayset as ExtendedPlayset)?.LaunchSettings.UseCitiesExe == true || _settings.FolderSettings.GamingPlatform != Skyve.Domain.Enums.GamingPlatform.Steam
			? _locationManager.CitiesPathWithExe
			: _locationManager.SteamPathWithExe;

		_iOUtil.Execute(file, string.Join(" ", args));
	}

	private IEnumerable<string> GetCommandArgs(IPlaysetManager? playsetManager)
	{
		var launchOptions = (playsetManager?.CurrentCustomPlayset as ExtendedPlayset)?.LaunchSettings;

		if (!(launchOptions?.UseCitiesExe == true || _settings.FolderSettings.GamingPlatform != Skyve.Domain.Enums.GamingPlatform.Steam))
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
			yield return "--disableCodeModding";
		}

		if (launchSettings.DeveloperMode)
		{
			yield return "--developerMode";
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

	public void RunStub()
	{
		var command = $"-applaunch 949230 -stub";

		var file = _locationManager.SteamPathWithExe;

		_iOUtil.Execute(file, command);
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
				KillProcessAndChildren(proc);
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to kill C:S II");
		}
	}

	private void KillProcessAndChildren(Process proc)
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