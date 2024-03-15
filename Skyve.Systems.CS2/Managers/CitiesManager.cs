using Extensions;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Timers;

namespace Skyve.Systems.CS2.Managers;

internal class CitiesManager : ICitiesManager
{
	private readonly ILogger _logger;
	private readonly ILocationService _locationManager;
	private readonly IIOUtil _iOUtil;
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

		var citiesMonitorTimer = new Timer(1000);

		//if (CrossIO.CurrentPlatform is Platform.Windows)
		{
			citiesMonitorTimer.Elapsed += CitiesMonitorTimer_Elapsed;
			citiesMonitorTimer.Start();
		}

		var launcherSettings = CrossIO.Combine(settings.FolderSettings.GamePath, "Launcher", "launcher-settings.json");

		if (CrossIO.FileExists(launcherSettings))
		{
			try
			{
				GameVersion = (JsonConvert.DeserializeObject(File.ReadAllText(launcherSettings)) as JObject)?.Value<string>("version") ?? string.Empty;

				if(File.GetLastWriteTime(launcherSettings) > DateTime.Now.AddDays(-7))
				{
					notificationsService.SendNotification(new GamePatchNotification())
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
		var _playsetManager = _serviceProvider.GetService<IPlaysetManager>();
		var file = (_playsetManager?.CurrentCustomPlayset as ExtendedPlayset)?.LaunchSettings.UseCitiesExe == true
			? _locationManager.CitiesPathWithExe
			: _locationManager.SteamPathWithExe;

		return CrossIO.FileExists(file);
	}

	public void Launch()
	{
		var _playsetManager = _serviceProvider.GetService<IPlaysetManager>();
		var args = GetCommandArgs(_playsetManager);
		var file = (_playsetManager?.CurrentCustomPlayset as ExtendedPlayset)?.LaunchSettings.UseCitiesExe == true
			? _locationManager.CitiesPathWithExe
			: _locationManager.SteamPathWithExe;

		_iOUtil.Execute(file, string.Join(" ", args));
	}

	private IEnumerable<string> GetCommandArgs(IPlaysetManager? _playsetManager)
	{
		var launchOptions = (_playsetManager?.CurrentCustomPlayset as ExtendedPlayset)?.LaunchSettings;

		if (!(launchOptions?.UseCitiesExe == true))
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
			_logger.Exception(ex, "Failed to kill C:S");
		}
	}

	private void KillProcessAndChildren(Process proc)
	{
		//foreach (var childProc in GetChildProcesses(proc))
		//{
		//	KillProcessAndChildren(childProc);
		//}

		proc.Kill();
	}

	private List<Process> GetChildProcesses(Process proc)
	{
		var childProcs = new List<Process>();

		try
		{
			var mos = new ManagementObjectSearcher(
			$"Select * From Win32_Process Where ParentProcessID={proc.Id}");

			foreach (var mo in mos.Get().Cast<ManagementObject>())
			{
				var childPid = Convert.ToInt32(mo["ProcessID"]);
				var childProc = Process.GetProcessById(childPid);
				childProcs.Add(childProc);
			}
		}
		catch { }

		return childProcs;
	}

	public void SetLaunchingStatus(bool launching)
	{
		LaunchingStatusChanged?.Invoke(launching);
	}
}