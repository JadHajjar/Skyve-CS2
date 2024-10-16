using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain.Systems;
using Skyve.Systems;

using System;
using System.Threading.Tasks;

namespace Skyve.Service.CS2.Systems;
internal class ServiceSystem
{
	private readonly UpdateSystem _updateSystem;
	private readonly IWorkshopService _workshopService;
	private readonly ILogger _logger;

	public ServiceSystem(UpdateSystem updateSystem, IWorkshopService workshopService, ILogger logger)
	{
		_updateSystem = updateSystem;
		_workshopService = workshopService;
		_logger = logger;
	}

	public async void Run()
	{
		_logger.Info("Update Loop Started");

		while (true)
		{
			try
			{
				await Task.Delay(TimeSpan.FromMinutes(15));

				try
				{
					await _workshopService.Initialize();

					await _workshopService.Login();

					await _updateSystem.RunUpdate();

					await _workshopService.Shutdown();
				}
				catch (Exception ex)
				{
					_logger.Exception(ex, "Error during Update cycle");
				}
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, "Unexpected error during Update Loop");
			}
		}
	}
}
