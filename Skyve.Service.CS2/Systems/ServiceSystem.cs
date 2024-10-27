using Skyve.Domain.Systems;

using System;
using System.Threading.Tasks;

namespace Skyve.Service.CS2.Systems;
internal class ServiceSystem
{
	private readonly UpdateSystem _updateSystem;
	private readonly IWorkshopService _workshopService;
	private readonly ILogger _logger;
	private readonly IBackupService _backupService;
	private readonly ISettings _settings;

	public ServiceSystem(UpdateSystem updateSystem, IWorkshopService workshopService, ILogger logger, IBackupService backupService, ISettings settings)
	{
		_updateSystem = updateSystem;
		_workshopService = workshopService;
		_logger = logger;
		_backupService = backupService;
		_settings = settings;
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
					await _updateSystem.RunUpdate();
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

	public async void RunBackup()
	{
		try
		{
			while (true)
			{
				await _workshopService.Initialize();

				await _workshopService.Login();

				_settings.BackupSettings.Reload();

				await _backupService.Run();

				await _workshopService.Shutdown();
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Unexpected error during Backup Loop");
		}
	}
}
