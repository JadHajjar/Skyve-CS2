
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
	private readonly IContentManager _contentManager;
	private readonly ICitiesManager _citiesManager;
	private readonly ISettings _settings;
	private readonly ILogger _logger;
	private readonly BackupSettings _backupSettings;
	private bool isCitiesRunning;

	public Func<Task>? PreBackupTask { get; set; }
	public Func<Task>? PostBackupTask { get; set; }

	public BackupService(IServiceProvider serviceProvider, IContentManager contentManager, ICitiesManager citiesManager, ISettings settings, ILogger logger)
	{
		_serviceProvider = serviceProvider;
		_contentManager = contentManager;
		_citiesManager = citiesManager;
		_settings = settings;
		_logger = logger;

		_backupSettings = (BackupSettings)_settings.BackupSettings;

		isCitiesRunning = _citiesManager.IsRunning();
	}

	public async Task<bool> Run()
	{
		var startBackup = false;
		var currentTime = (int)DateTime.Now.TimeOfDay.TotalMinutes;

		if (_backupSettings.ScheduleSettings.Type.HasFlag(BackupScheduleType.OnScheduledTimes))
		{
			startBackup |= _backupSettings.ScheduleSettings.ScheduleTimes.Any(x => currentTime == (int)x.TotalMinutes);
		}

		if (isCitiesRunning != _citiesManager.IsRunning())
		{
			isCitiesRunning = !isCitiesRunning;

			startBackup |= !isCitiesRunning && _backupSettings.ScheduleSettings.Type.HasFlag(BackupScheduleType.OnGameClose);
		}

		if (_backupSettings.ScheduleSettings.Type.HasFlag(BackupScheduleType.OnNewSaveGame))
		{
			var savePackage = _contentManager.GetSaveFiles();

			startBackup |= savePackage?.LocalData?.Assets?.Any(x => currentTime == (int)x.LocalTime.TimeOfDay.TotalMinutes) ?? false;
		}

		if (startBackup)
		{
			if (PreBackupTask is not null)
			{
				await PreBackupTask();
			}

			if (!await DoBackup())
			{
				return true;
			}

			if (PostBackupTask is not null)
			{
				await PostBackupTask();
			}
		}

		await Task.Delay(59_000);

		return false;
	}

	private async Task<bool> DoBackup()
	{
		try
		{
			var backupSystem = _serviceProvider.GetService<IBackupSystem>()!;

			backupSystem.BackupInstructions.DoSavesBackup = _backupSettings.ScheduleSettings.BackupSaves;
			backupSystem.BackupInstructions.DoLocalModsBackup = _backupSettings.ScheduleSettings.BackupLocalMods;
			backupSystem.BackupInstructions.DoMapsBackup = _backupSettings.ScheduleSettings.BackupMaps;

			return await backupSystem.DoBackup();
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: "Fatal error during backup. Disabling system...");

			return false;
		}
	}
}
