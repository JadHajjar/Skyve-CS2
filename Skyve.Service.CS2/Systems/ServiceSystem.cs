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
	private readonly IPlaysetManager _playsetManager;

	public ServiceSystem(UpdateSystem updateSystem, IWorkshopService workshopService, ILogger logger, IBackupService backupService, ISettings settings, IPlaysetManager playsetManager)
	{
		_updateSystem = updateSystem;
		_workshopService = workshopService;
		_logger = logger;
		_backupService = backupService;
		_settings = settings;
		_playsetManager = playsetManager;

		_backupService.PreBackupTask = PreBackupTask;
		_backupService.PostBackupTask = PostBackupTask;
	}

	public async void Run()
	{
		_logger.Info("Update Loop Started");

		await Task.Delay(TimeSpan.FromMinutes(5));

		while (true)
		{
			try
			{
				await _updateSystem.RunUpdate();
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, memberName: "Unexpected error during Update Loop");
			}

			await Task.Delay(TimeSpan.FromMinutes(30));
		}
	}

	public async void RunBackup()
	{
		_logger.Info("Backup Loop Started");

		try
		{
			while (true)
			{
				_settings.BackupSettings.Reload();

				if (await _backupService.Run())
				{
					_logger.Error("Backup failed, disabling backup loop");
					return;
				}
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: "Unexpected error during Backup Loop");
		}
	}

	private async Task PreBackupTask()
	{
		_logger.Info("Starting Backup");

		await _workshopService.Initialize();

		await _workshopService.Login();
	}

	private async Task PostBackupTask()
	{
		await _workshopService.Shutdown();

		_logger.Info("Backup Finished");
	}
}
