using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using SlickControls;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

	public CentralManager(IModLogicManager modLogicManager, ICompatibilityManager compatibilityManager, IPlaysetManager profileManager, ICitiesManager citiesManager, ILocationService locationManager, ISubscriptionsManager subscriptionManager, IPackageManager packageManager, IContentManager contentManager, ISettings settings, ILogger logger, INotifier notifier, IModUtil modUtil, IPackageUtil packageUtil, IVersionUpdateService versionUpdateService, INotificationsService notificationsService, IUpdateManager updateManager, IAssetUtil assetUtil, IWorkshopService workshopService)
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
	}

	public async void Start()
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

		_logger.Info($"Loading and applying CR Data..");

		_compatibilityManager.Start(content);

		_logger.Info($"Analyzing packages..");

		try
		{ AnalyzePackages(content); }
		catch (Exception ex) { _logger.Exception(ex, "Failed to analyze packages"); }

		_logger.Info($"Finished analyzing packages..");

		_notifier.OnContentLoaded();

		await _workshopService.Login();

		if (_playsetManager.CurrentPlayset is not null && CommandUtil.PreSelectedPlayset == _playsetManager.CurrentPlayset.Name)
		{
			_logger.Info($"[Command] Applying Playset ({_playsetManager.CurrentPlayset.Name})..");
			_playsetManager.SetCurrentPlayset(_playsetManager.CurrentPlayset);
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

		if (ConnectionHandler.CheckConnection())
		{
			LoadDlcAndCR();

			_notifier.OnWorkshopInfoUpdated();

			_updateManager.SendUpdateNotifications();
		}
		else
		{
			_logger.Warning("Not connected to the internet, delaying remaining loads.");

			_notifier.OnWorkshopInfoUpdated();
			
			_updateManager.SendUpdateNotifications();

			_logger.Info($"Compatibility report cached");

			_compatibilityManager.DoFirstCache();

			ConnectionHandler.WhenConnected(() => new BackgroundAction(LoadDlcAndCR).Run());
		}

		_logger.Info($"Finished.");
	}

	private void LoadDlcAndCR()
	{
		try
		{ SteamUtil.LoadDlcs(); }
		catch { }

		_logger.Info($"Downloading compatibility data..");

		_compatibilityManager.DownloadData();

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

	private void AnalyzePackages(List<IPackage> content)
	{
		var blackList = new List<IPackage>();
		var firstTime = _updateManager.IsFirstTime();

		_notifier.BulkUpdating = true;

		foreach (var package in content)
		{
			if (_compatibilityManager.IsBlacklisted(package))
			{
				blackList.Add(package);
				continue;
			}

			if (package.IsCodeMod)
			{
				if (!_settings.UserSettings.AdvancedIncludeEnable)
				{
					if (!firstTime && !_modUtil.IsEnabled(package) && _modUtil.IsIncluded(package))
					{
						_modUtil.SetIncluded(package, false);
					}
				}

				if (_settings.UserSettings.LinkModAssets && package.LocalData is not null)
				{
					_packageUtil.SetIncluded(package.LocalData.Assets, _modUtil.IsIncluded(package));
				}

				_modLogicManager.Analyze(package, _modUtil);

				if (!firstTime && !_updateManager.IsPackageKnown(package.LocalData!))
				{
					_modUtil.SetEnabled(package, _modUtil.IsIncluded(package));
				}
			}
		}

		_notifier.BulkUpdating = false;
		_modUtil.SaveChanges();
		_assetUtil.SaveChanges();

		content.RemoveAll(x => blackList.Contains(x));

		//if (blackList.Count > 0)
		//{
		//	BlackListTransfer.SendList(blackList.Select(x => x.Id), false);
		//}
		//else if (CrossIO.FileExists(BlackListTransfer.FilePath))
		//{
		//	CrossIO.DeleteFile(BlackListTransfer.FilePath);
		//}

		foreach (var item in blackList)
		{
			_packageManager.DeleteAll(item.LocalData!.Folder);
		}

		_logger.Info($"Applying analysis results..");

		_modLogicManager.ApplyRequiredStates(_modUtil);
	}
}
