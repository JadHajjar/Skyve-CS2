using Extensions;

using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using SlickControls;

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class CentralManager : ICentralManager
{
	private readonly IModLogicManager _modLogicManager;
	private readonly ICompatibilityManager _compatibilityManager;
	private readonly IPlaysetManager _playsetManager;
	private readonly ICitiesManager _citiesManager;
	private readonly ILocationService _locationService;
	private readonly ISubscriptionsManager _subscriptionManager;
	private readonly IPackageManager _packageManager;
	private readonly IContentManager _contentManager;
	private readonly ISettings _settings;
	private readonly ILogger _logger;
	private readonly INotifier _notifier;
	private readonly IModUtil _modUtil;
	private readonly IPackageUtil _packageUtil;
	private readonly IVersionUpdateService _versionUpdateService;
	private readonly INotificationsService _notificationsService;
	private readonly IUpdateManager _updateManager;
	private readonly IAssetUtil _assetUtil;
	private readonly IWorkshopService _workshopService;
	private readonly ISkyveDataManager _skyveDataManager;
	private readonly IDlcManager _dlcManager;
	private readonly IInterfaceService _interfaceService;
	private readonly IBackupService _backupService;

	public CentralManager(IModLogicManager modLogicManager, ICompatibilityManager compatibilityManager, IPlaysetManager profileManager, ICitiesManager citiesManager, ILocationService locationManager, ISubscriptionsManager subscriptionManager, IPackageManager packageManager, IContentManager contentManager, ISettings settings, ILogger logger, INotifier notifier, IModUtil modUtil, IPackageUtil packageUtil, IVersionUpdateService versionUpdateService, INotificationsService notificationsService, IUpdateManager updateManager, IAssetUtil assetUtil, IWorkshopService workshopService, ISkyveDataManager skyveDataManager, IDlcManager dlcManager, IInterfaceService interfaceService, IBackupService backupService)
	{
		_modLogicManager = modLogicManager;
		_compatibilityManager = compatibilityManager;
		_playsetManager = profileManager;
		_citiesManager = citiesManager;
		_locationService = locationManager;
		_subscriptionManager = subscriptionManager;
		_packageManager = packageManager;
		_contentManager = contentManager;
		_settings = settings;
		_logger = logger;
		_notifier = notifier;
		_modUtil = modUtil;
		_packageUtil = packageUtil;
		_versionUpdateService = versionUpdateService;
		_notificationsService = notificationsService;
		_updateManager = updateManager;
		_assetUtil = assetUtil;
		_workshopService = workshopService;
		_skyveDataManager = skyveDataManager;
		_dlcManager = dlcManager;
		_interfaceService = interfaceService;
		_backupService = backupService;
	}

	public async void Start()
	{
		try
		{
			await Initialize();
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Error in Initialization");
		}
	}

	public async Task Initialize()
	{
		if (!_settings.SessionSettings.FirstTimeSetupCompleted)
		{
			try
			{
				RunFirstTimeSetup();
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, "Failed to complete the First Time Setup");

				MessagePrompt.Show(ex, "Failed to complete the First Time Setup", form: SystemsProgram.MainForm as SlickForm);
			}
		}

		_logger.Info("Checking for internet connection..");

		ConnectionHandler.AssumeInternetConnectivity = _settings.UserSettings.AssumeInternetConnectivity;
		ConnectionHandler.Start();

		_logger.Info("Starting PDX SDK..");

		await _workshopService.Initialize();

		await _modUtil.Initialize();

		await _playsetManager.Initialize();

		_logger.Info("Loading packages..");

		var content = await _contentManager.LoadContents();

		_logger.Info($"Loaded {content.Count} packages");

		_versionUpdateService.Run(content);

		_packageManager.SetPackages(content);

		_logger.Info($"Loading and applying Compatibility Data..");

		_skyveDataManager.Start(content);

		_notifier.OnContentLoaded();

		await RunCommands();

		_logger.Info($"Starting Listeners..");

		_contentManager.StartListeners();

		_logger.Info($"Listeners Started");

		_notifier.OnWorkshopInfoUpdated();

		_updateManager.SendUpdateNotifications();

		_logger.Info($"Compatibility report cached");

		_compatibilityManager.DoFirstCache();

		if (!ConnectionHandler.CheckConnection())
		{
			_logger.Warning("Not connected to the internet, delaying remaining loads.");
		}

		await ConnectionHandler.WhenConnected(UpdateCompatibilityCatalogue);

		await _workshopService.Login();

		await ConnectionHandler.WhenConnected(_dlcManager.UpdateDLCs);

		if (_workshopService.IsLoggedIn)
		{
			await _updateManager.SendReviewRequestNotifications();

			await _updateManager.SendUnreadCommentsNotifications();

			await UpdateSkyveVersionsInPlaysets();
		}

		try
		{
			CleanupData();
		}
		catch { }

		_logger.Info($"Finished.");

		if (Process.GetProcessesByName("Skyve.Service.CS2").Length == 0)
			_backupService.Run();
	}

	private async Task UpdateCompatibilityCatalogue()
	{
		_logger.Info($"Downloading compatibility data..");

		await _skyveDataManager.DownloadData();

		_logger.Info($"Compatibility data downloaded");

		_compatibilityManager.DoFirstCache();

		_logger.Info($"Compatibility report cached");
	}

	private void RunFirstTimeSetup()
	{
		_logger.Info("Running First Time Setup");

		_locationService.RunFirstTimeSetup();

		_logger.Info("First Time Setup Completed");

		if (CrossIO.CurrentPlatform is Platform.Windows)
		{
			_locationService.CreateShortcut();
		}

		_settings.SessionSettings.FirstTimeSetupCompleted = true;
		_settings.SessionSettings.Save();

		_logger.Info("Saved Session Settings");
	}

	public async Task<bool> RunCommands()
	{
		if (_playsetManager.CurrentPlayset is not null && CommandUtil.Commands.PreSelectedPlayset == _playsetManager.CurrentPlayset.Name)
		{
			_logger.Info($"[Command] Applying Playset ({_playsetManager.CurrentPlayset.Name})..");
			await _playsetManager.ActivatePlayset(_playsetManager.CurrentPlayset);
		}

		if (CommandUtil.Commands.LaunchOnLoad)
		{
			_logger.Info($"[Command] Launching Cities..");
			_citiesManager.Launch();
		}

		if (CommandUtil.Commands.NoWindow)
		{
			_logger.Info($"[Command] Closing App..");
			return true;
		}

		var actions = CommandUtil.Commands.CommandActions;
		if (actions.Length > 0)
		{
			switch (actions[0].ToLower())
			{
				case "mods":
				case "mod":
					if (ulong.TryParse(actions.TryGet(1), out var id))
					{
						_interfaceService.OpenPackagePage(new GenericPackageIdentity(id));
					}

					break;
				case "logreport":
					_interfaceService.OpenLogReport(actions.TryGet(1) == "save");
					break;
			}
		}

		return false;
	}

	public async Task UpdateSkyveVersionsInPlaysets()
	{
#if Stable
		const ulong MODID = ;
#else
		const ulong MODID = 75804;
#endif
		var package = new GenericPackageIdentity(MODID);

		//await _packageUtil.SetVersion(package, null);
	}

	private void CleanupData()
	{
		var folders = new string[] {
			"Mods/Gooee",
			"ModsData/Gooee",
			"Cities2Modding/Gooee",
			"Mods/LegacyFlavour",
			"ModsData/LegacyFlavour",
			"Cities2Modding/LegacyFlavour",
			"Mods/FindStuff",
			"Mods/MapImageLayer",
			"Mods/RealisticDensity",
			"Mods/UltimateMonitor",
		};

		foreach (var folder in folders)
		{
			var dir = new DirectoryInfo(CrossIO.Combine(_settings.FolderSettings.AppDataPath, folder));

			if (dir.Exists)
			{
				dir.Delete(true);
			}
		}
	}
}
