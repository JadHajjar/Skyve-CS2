using Skyve.Domain.Systems;

using System.Diagnostics;
using System.Threading.Tasks;

namespace Skyve.Service.CS2.Systems;
internal class UpdateSystem
{
	private readonly IWorkshopService _workshopService;
	private readonly ILogger _logger;

	public UpdateSystem(IWorkshopService workshopService, ILogger logger)
	{
		_workshopService = workshopService;
		_logger = logger;
	}

	internal async Task RunUpdate()
	{
		_logger.Info("UpdateSystem > RunUpdate");

		if (_workshopService.IsReady)
		{
			if (IsSkyveAppRunning())
			{
				_logger.Info("Sync Skipped, App Running");
				return;
			}

			_logger.Info("Sync Started");
			await _workshopService.RunSync();
			_logger.Info("Sync Ended");
		}
		else
		{
			_logger.Info("Logging in");
			await _workshopService.Login();
		}
	}

	private bool IsSkyveAppRunning()
	{
		return Process.GetProcessesByName("Skyve").Length > 0;
	}
}
