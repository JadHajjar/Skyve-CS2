using Extensions;

using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain.Systems;
using Skyve.Systems;

using System;
using System.Threading.Tasks;

namespace Skyve.Service.CS2.Systems;
internal class ServiceSystem
{
	private readonly UpdateSystem _updateSystem;
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

	public ServiceSystem(UpdateSystem updateSystem, IWorkshopService workshopService, ILogger logger)
	{
		_updateSystem = updateSystem;
		_workshopService = workshopService;
		_logger = logger;
	}

	internal async void Start()
	{
		await _workshopService.Initialize();

		await _workshopService.Login();

		new BackgroundAction(UpdateLoop).Run();
	}

	private async void UpdateLoop()
	{
		while (true)
		{
			await Task.Delay(TimeSpan.FromMinutes(15));

			try
			{
				await _updateSystem.RunUpdate();
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, "Error during Update cycle");
			}
		}
	}
}
