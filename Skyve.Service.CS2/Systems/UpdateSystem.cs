using Skyve.Domain.Systems;

using System.Diagnostics;
using System.Threading.Tasks;

namespace Skyve.Service.CS2.Systems;
internal class UpdateSystem
{
	private readonly IWorkshopService _workshopService;
	private readonly ILogger _logger;
	private readonly ICitiesManager _citiesManager;

	public UpdateSystem(IWorkshopService workshopService, ILogger logger, ICitiesManager citiesManager)
	{
		_workshopService = workshopService;
		_logger = logger;
		_citiesManager = citiesManager;
	}

	internal async Task RunUpdate()
	{
		if (IsSkyveAppRunning())
		{
			_logger.Info("Sync Skipped, App Running");
			return;
		}

		if (_citiesManager.IsRunning())
		{
			_logger.Info("Sync Skipped, Game Running");
			return;
		}

		_logger.Info("UpdateSystem > RunUpdate");

		await _workshopService.Initialize();

		if (await _workshopService.Login())
		{
			await _workshopService.RunSync();
		}
		else
		{
			_logger.Warning("Login failed, Sync not started");
		}

		await _workshopService.Shutdown();
	}

	private bool IsSkyveAppRunning()
	{
		return Process.GetProcessesByName("Skyve").Length > 0;
	}
}
