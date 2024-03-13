using Extensions;

using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using SlickControls;

using System;
using System.Collections.Generic;
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

	public CentralManager(IModLogicManager modLogicManager, ICompatibilityManager compatibilityManager, IPlaysetManager profileManager, ICitiesManager citiesManager, ILocationService locationManager, ISubscriptionsManager subscriptionManager, IPackageManager packageManager, IContentManager contentManager, ISettings settings, ILogger logger, INotifier notifier, IModUtil modUtil, IPackageUtil packageUtil, IVersionUpdateService versionUpdateService, INotificationsService notificationsService, IUpdateManager updateManager, IAssetUtil assetUtil, IWorkshopService workshopService, ISkyveDataManager skyveDataManager, IDlcManager dlcManager)
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

	private async Task Initialize()
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

		if (_playsetManager.CurrentPlayset is not null && CommandUtil.PreSelectedPlayset == _playsetManager.CurrentPlayset.Name)
		{
			_logger.Info($"[Command] Applying Playset ({_playsetManager.CurrentPlayset.Name})..");
			await _playsetManager.ActivatePlayset(_playsetManager.CurrentPlayset);
		}

		if (CommandUtil.LaunchOnLoad)
		{
			_logger.Info($"[Command] Launching Cities..");
			_citiesManager.Launch();
		}

		if (CommandUtil.NoWindow)
		{
			_logger.Info($"[Command] Closing App..");
			return;
		}

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

		_logger.Info($"Finished.");
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
}
