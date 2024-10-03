
using Microsoft.Extensions.DependencyInjection;

using Skyve.Domain.CS2.Enums;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Services;
internal class BackupService : IBackupService
{
	private readonly IServiceProvider _serviceProvider;
	private readonly IWorkshopService _workshopService;
	private readonly IContentManager _contentManager;
	private readonly ICitiesManager _citiesManager;
	private readonly ISettings _settings;
	private readonly ILogger _logger;
	private readonly BackupSettings _backupSettings;

	public BackupService(IServiceProvider serviceProvider, IWorkshopService workshopService, IContentManager contentManager, ICitiesManager citiesManager, ISettings settings, ILogger logger)
	{
		_serviceProvider = serviceProvider;
		_workshopService = workshopService;
		_contentManager = contentManager;
		_citiesManager = citiesManager;
		_settings = settings;
		_logger = logger;

		_backupSettings = (BackupSettings)_settings.BackupSettings;
	}

	public async void Run()
	{
		var isCitiesRunning = _citiesManager.IsRunning();

		while (true)
		{
			var startBackup = false;
			var currentTime = (int)DateTime.Now.TimeOfDay.TotalMinutes;

			if (_backupSettings.Schedule.HasFlag(BackupSchedule.OnScheduledTimes))
			{
				startBackup = _backupSettings.ScheduleTimes.Any(x => currentTime == (int)x.TotalMinutes);
			}

			if (isCitiesRunning != _citiesManager.IsRunning())
			{
				isCitiesRunning = !isCitiesRunning;

				startBackup = !isCitiesRunning && _backupSettings.Schedule.HasFlag(BackupSchedule.OnGameClose);
			}

			if (_backupSettings.Schedule.HasFlag(BackupSchedule.OnNewSaveGame))
			{
				var savePackage = _contentManager.GetSaveFiles();

				startBackup = savePackage?.LocalData?.Assets?.Any(x => currentTime == (int)x.LocalTime.TimeOfDay.TotalMinutes) ?? false;
			}

			if (startBackup)
			{
				if (!await DoBackup())
				{
					return;
				}
			}

			await Task.Delay(59_000);
		}
	}

	private async Task<bool> DoBackup()
	{
		try
		{
			var backupSystem = _serviceProvider.GetService<IBackupSystem>()!;

			backupSystem.BackupInstructions.DoSavesBackup = _backupSettings.BackupSavesOnSchedule;
			backupSystem.BackupInstructions.DoLocalModsBackup = _backupSettings.BackupLocalModsOnSchedule;

			return await backupSystem.DoBackup();
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Fatal error during backup. Disabling system...");

			return false;
		}
	}
}
