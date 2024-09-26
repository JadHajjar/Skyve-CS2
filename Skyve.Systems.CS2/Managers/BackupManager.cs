using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class BackupManager
{
	private readonly ISettings _settings;
	private readonly ILogger _logger;
	private readonly IContentManager _contentManager;
	private readonly IPlaysetManager _playsetManager;
	private readonly IPackageManager _packageManager;

	public BackupManager(ISettings settings, ILogger logger, IContentManager contentManager, IPlaysetManager playsetManager, IPackageManager packageManager)
	{
		_settings = settings;
		_logger = logger;
		_contentManager = contentManager;
		_playsetManager = playsetManager;
		_packageManager = packageManager;
	}

	public List<IBackupItem> GetBackups()
	{
		return [];
	}

	public async Task<bool> DoBackup()
	{
		try
		{
			var availableBackups = GetBackups();

			MakeSavesBackup(availableBackups).Foreach(SaveBackupItem);

			SaveBackupItem(MakeSettingsBackup());
			SaveBackupItem(await MakePlaysetBackup());
			SaveBackupItem(MakeLocalModsBackup());
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to do backup");

			return false;
		}

		return true;
	}

	private void SaveBackupItem(IBackupItem? item)
	{
		if (item is null || !item.CanSave())
			return;

		item.Save();
	}

	private IEnumerable<IBackupItem> MakeSavesBackup(List<IBackupItem> availableBackups)
	{
		var savePackage = _contentManager.GetSaveFiles();
		var saves = savePackage?.LocalData?.Assets ?? [];
		var backupItems = new List<IBackupItem>();

		foreach (var save in saves)
		{
			if (availableBackups.Any(x => x is BackupItem.SaveGame s
				&& x.Name == save.Name
				&& x.Time == save.LocalTime))
			{
				continue;
			}

			backupItems.Add(new BackupItem.SaveGame(save));
		}

		return backupItems;
	}

	private IBackupItem MakeSettingsBackup()
	{
		var settingsFiles = Directory.GetFiles(_settings.FolderSettings.AppDataPath, "*.coc");
		var modSettingFolders = new string[0];

		if (Directory.Exists(CrossIO.Combine(_settings.FolderSettings.AppDataPath, "ModsSettings")))
		{
			modSettingFolders = Directory.GetDirectories(CrossIO.Combine(_settings.FolderSettings.AppDataPath, "ModsSettings"));
		}

		return new BackupItem.SettingsFiles(settingsFiles, modSettingFolders);
	}

	private async Task<IBackupItem?> MakePlaysetBackup()
	{
		if (_playsetManager.CurrentPlayset is null)
		{
			return null;
		}

		var contents = await _playsetManager.GetPlaysetContents(_playsetManager.CurrentPlayset);

		return new BackupItem.ActivePlayset(contents.ToList());
	}

	private IBackupItem MakeLocalModsBackup()
	{
		return new BackupItem.LocalMods(_packageManager.Packages.AllWhere(x => x.IsLocal));
	}
}
