using Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Skyve.Domain;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Data;
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

	public event MonitorTickDelegate? MonitorTick;

	public event Action<bool>? LaunchingStatusChanged;

	public string GameVersion { get; }

	public CitiesManager(ILogger logger, ILocationService locationManager, ISettings settings, IIOUtil iOUtil)
	{
		_logger = logger;
		_locationManager = locationManager;
		_iOUtil = iOUtil;

		var citiesMonitorTimer = new Timer(1000);

		//if (CrossIO.CurrentPlatform is Platform.Windows)
		{
			citiesMonitorTimer.Elapsed += CitiesMonitorTimer_Elapsed;
			citiesMonitorTimer.Start();
		}

		var launcherSettings = CrossIO.Combine(settings.FolderSettings.GamePath, "Launcher", "launcher-settings.json");

		if (CrossIO.FileExists(launcherSettings))
		{
			GameVersion = (JsonConvert.DeserializeObject(File.ReadAllText(launcherSettings)) as JObject)?.Value<string>("version") ?? "1.0";
		}
		else
		{
			GameVersion = string.Empty;
		}
	}

	private void CitiesMonitorTimer_Elapsed(object sender, ElapsedEventArgs e)
	{
		MonitorTick?.Invoke(IsAvailable(), IsRunning());
	}

	public bool IsAvailable()
	{
		var file =/* (_profileManager.CurrentPlayset as Playset)!.LaunchSettings.UseCitiesExe
			? _locationManager.CitiesPathWithExe
			:*/ _locationManager.SteamPathWithExe;

		return CrossIO.FileExists(file);
	}

	public void Launch()
	{
		var args = GetCommandArgs();
		var file = /*(_profileManager.CurrentPlayset as Playset)!.LaunchSettings.UseCitiesExe
			? _locationManager.CitiesPathWithExe
			:*/ _locationManager.SteamPathWithExe;

		_iOUtil.Execute(file, string.Join(" ", args));
	}

	private string[] GetCommandArgs()
	{
		var args = new List<string>();

		//var launchSettings = (_profileManager.CurrentPlayset as Playset)!.LaunchSettings;

		//if (!launchSettings.UseCitiesExe)
		{
			args.Add("-applaunch 949230");
		}

		//if (launchSettings.NoWorkshop)
		//{
		//	args.Add("-noWorkshop");
		//}

		//if (launchSettings.ResetAssets)
		//{
		//	args.Add("-reset-assets");
		//}

		//if (launchSettings.NoAssets)
		//{
		//	args.Add("-noAssets");
		//}

		//if (launchSettings.NoMods)
		//{
		//	args.Add("-disableMods");
		//}

		//if (launchSettings.LHT)
		//{
		//	args.Add("-LHT");
		//}

		//if (launchSettings.DevUi)
		//{
		//	args.Add("-enable-dev-ui");
		//}

		//if (launchSettings.RefreshWorkshop)
		//{
		//	args.Add("-refreshWorkshop");
		//}

		//if (launchSettings.NewAsset)
		//{
		//	args.Add("-newAsset");
		//}

		//if (launchSettings.LoadAsset)
		//{
		//	args.Add("-loadAsset");
		//}

		//if (launchSettings.LoadSaveGame)
		//{
		//	if (CrossIO.FileExists(launchSettings.SaveToLoad))
		//	{
		//		args.Add("--loadSave=" + quote(launchSettings.SaveToLoad!));
		//	}
		//	else
		//	{
		//		args.Add("-continuelastsave");
		//	}
		//}
		//else if (launchSettings.StartNewGame)
		//{
		//	if (CrossIO.FileExists(launchSettings.MapToLoad))
		//	{
		//		args.Add("--newGame=" + quote(launchSettings.MapToLoad!));
		//	}
		//	else
		//	{
		//		args.Add("-newGame");
		//	}
		//}

		//if (!string.IsNullOrWhiteSpace(launchSettings.CustomArgs))
		//{
		//	args.Add(launchSettings.CustomArgs!);
		//}

		return args.ToArray();
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
		catch { return false; }
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
		catch (Exception ex) { _logger.Exception(ex, "Failed to kill C:S"); }
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