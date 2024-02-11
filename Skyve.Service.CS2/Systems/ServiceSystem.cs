using Extensions;

using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Systems;
using Skyve.Systems.CS2.Utilities;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skyve.Service.CS2.Systems;
internal class ServiceSystem
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

	internal async void Start()
	{
		await _workshopService.Initialize();

		await _workshopService.Login();


		await RunUpdate();
	}

	private async Task RunUpdate()
	{
		await Task.Delay(TimeSpan.FromMinutes(15));

		if (_workshopService.IsReady)
		{
			await _workshopService.RunSync();
		}
		else
		{
			await _workshopService.Login();
		}
	}
}
