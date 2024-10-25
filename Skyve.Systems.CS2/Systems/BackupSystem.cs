using Extensions;

using Newtonsoft.Json;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Enums;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Systems;
internal class BackupSystem : IBackupSystem
{
	private delegate Task<bool> RestoreDelegate(ZipArchive zipArchive, IBackupMetaData metaData);

	private readonly ISettings _settings;
	private readonly ILogger _logger;
	private readonly IContentManager _contentManager;
	private readonly IPlaysetManager _playsetManager;
	private readonly IPackageManager _packageManager;
	private readonly BackupSettings _backupSettings;
	private readonly DateTime _backupTime;

	public IBackupInstructions BackupInstructions { get; } = new BackupInstructions();
	public IRestoreInstructions RestoreInstructions { get; } = new RestoreInstructions();

	public BackupSystem(ISettings settings, ILogger logger, IContentManager contentManager, IPlaysetManager playsetManager, IPackageManager packageManager)
	{
		_settings = settings;
		_logger = logger;
		_contentManager = contentManager;
		_playsetManager = playsetManager;
		_packageManager = packageManager;

		_backupSettings = (settings.BackupSettings as BackupSettings)!;

		_backupTime = DateTime.Now;
	}

	public void Save(IBackupMetaData metaData, string[] files)
	{
		metaData.BackupTime = _backupTime;

		var folder = CrossIO.Combine(_backupSettings.DestinationFolder
			, _backupTime.ToString("yyyy")
			, _backupTime.ToString("MM (MMMM)")
			, _backupTime.ToString("dd (dddd)")
			, metaData.Type);

		try
		{
			Directory.CreateDirectory(folder);
		}
		catch
		{
			return;
		}

		var path = CrossIO.Combine(folder, $"{metaData.Name}_{metaData.ContentTime.Ticks}_{_backupTime.Ticks}.sbak");

		using var fileStream = File.Create(path);
		using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create, false);

		CreateEntry(zipArchive, ".metaData", JsonConvert.SerializeObject(metaData));

		foreach (var file in files.Where(CrossIO.FileExists))
		{
			zipArchive.CreateEntryFromFile(file
				, file.Substring((metaData.Root?.Length ?? -1) + 1).Replace("/", "\\")
				, CompressionLevel.Optimal);
		}
	}

	private static void CreateEntry(ZipArchive zipArchive, string entry, string content)
	{
		var profileEntry = zipArchive.CreateEntry(entry);

		using var writer = new StreamWriter(profileEntry.Open());

		writer.Write(content);
	}

	public List<IRestoreItem> GetAllBackups()
	{
		if (!Directory.Exists(_backupSettings.DestinationFolder))
		{
			return [];
		}

		var files = Directory.GetFiles(_backupSettings.DestinationFolder, "*.sbak", SearchOption.AllDirectories);
		var items = new List<IRestoreItem>(files.Length);

		foreach (var file in files)
		{
			try
			{
				using var stream = File.OpenRead(file);
				using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read, false);

				using var metaDataStream = zipArchive.GetEntry(".metaData").Open();
				using var reader = new StreamReader(metaDataStream);

				var metaData = JsonConvert.DeserializeObject<BackupMetaData>(reader.ReadToEnd());

				items.Add(new BackupItem.Zip(file, metaData));
			}
			catch { }
		}

		return items;
	}

	public long GetBackupsSizeOnDisk()
	{
		if (!Directory.Exists(_backupSettings.DestinationFolder))
		{
			return 0;
		}

		var totalSize = 0L;

		foreach (var file in new DirectoryInfo(_backupSettings.DestinationFolder).GetFiles("*.sbak", SearchOption.AllDirectories))
		{
			totalSize += file.Length;
		}

		return totalSize;
	}

	public async Task<bool> DoBackup()
	{
		try
		{

			var availableBackups = GetAllBackups();

			SaveBackupItem(MakeSettingsBackup());
			SaveBackupItem(MakeModsSettingsBackup());
			SaveBackupItem(await MakePlaysetBackup());

			if (BackupInstructions.DoSavesBackup)
			{
				MakeSavesBackup(availableBackups).Foreach(SaveBackupItem);
			}

			if (BackupInstructions.DoLocalModsBackup)
			{
				MakeLocalModsBackup(availableBackups).Foreach(SaveBackupItem);
			}

			DoCleanup();
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
		{
			return;
		}

		item.Save(this);
	}

	private IEnumerable<IBackupItem> MakeSavesBackup(List<IRestoreItem> availableBackups)
	{
		var savePackage = _contentManager.GetSaveFiles();
		var saves = savePackage?.LocalData?.Assets ?? [];
		var backupItems = new List<IBackupItem>();

		foreach (var save in saves)
		{
			if (availableBackups.Any(x => x.MetaData.Type is nameof(BackupItem.SaveGames)
				&& x.MetaData.Name == save.Name
				&& x.MetaData.ContentTime == save.LocalTime))
			{
				continue;
			}

			if (_backupSettings.IgnoreAutoSaves && (((Asset)save).SaveGameMetaData?.AutoSave ?? false))
			{
				continue;
			}

			backupItems.Add(new BackupItem.SaveGames(save));
		}

		return backupItems;
	}

	private IBackupItem MakeSettingsBackup()
	{
		var settingsFiles = Directory.GetFiles(_settings.FolderSettings.AppDataPath, "*.coc", SearchOption.AllDirectories);

		return new BackupItem.SettingsFiles(settingsFiles, _settings.FolderSettings.AppDataPath);
	}

	private IBackupItem? MakeModsSettingsBackup()
	{
		var appData = CrossIO.Combine(_settings.FolderSettings.AppDataPath, "ModsSettings");

		if (Directory.Exists(appData))
		{
			var modSettingFiles = Directory.GetFiles(appData, "*", SearchOption.AllDirectories);

			return new BackupItem.ModsSettingsFiles(modSettingFiles, appData);
		}

		return null;
	}

	private async Task<IBackupItem?> MakePlaysetBackup()
	{
		if (_playsetManager.CurrentPlayset is null)
		{
			return null;
		}

		var playset = await _playsetManager.GetLogPlayset();

		return new BackupItem.ActivePlayset(playset);
	}

	private IEnumerable<IBackupItem> MakeLocalModsBackup(List<IRestoreItem> availableBackups)
	{
		var backupItems = new List<IBackupItem>();

		foreach (var package in _packageManager.Packages.Where(x => x.IsLocal))
		{
			if (availableBackups.Any(x => x.MetaData.Type is nameof(BackupItem.LocalMods)
				&& x.MetaData.Name == package.Name
				&& x.MetaData.ContentTime == package.LocalData!.LocalTime))
			{
				continue;
			}

			backupItems.Add(new BackupItem.LocalMods(package));
		}

		return backupItems;
	}

	public async Task<bool> Restore(IBackupMetaData metaData, string file)
	{
		RestoreDelegate method = metaData.RestoreType switch
		{
			RestoreAction.Playset => RestorePlayset,
			RestoreAction.Overwrite => RestoreOverwrite,
			RestoreAction.ClearRoot => RestoreClearRoot,
			RestoreAction.ClearRootOfSimilarFileTypes => RestoreClearRootOfSimilarFileTypes,
			_ => throw new NotImplementedException()
		};

		using var stream = File.OpenRead(file);
		using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read, false);

		return await method(zipArchive, metaData);
	}

	private async Task<bool> RestorePlayset(ZipArchive zipArchive, IBackupMetaData metaData)
	{
		var entry = zipArchive.Entries.FirstOrDefault(x => x.FullName is not ".metaData");

		var temp = CrossIO.GetTempFileName();

		try
		{
			entry.ExtractToFile(temp);

			var playset = await _playsetManager.ImportPlayset(temp);

			if (playset is null)
			{
				return false;
			}

			await _playsetManager.RenamePlayset(playset, "[Recovered] " + playset.Name);

			await _playsetManager.ActivatePlayset(playset);
		}
		finally
		{
			CrossIO.DeleteFile(temp, true);
		}

		return true;
	}

	private Task<bool> RestoreOverwrite(ZipArchive zipArchive, IBackupMetaData metaData)
	{
		foreach (var item in zipArchive.Entries)
		{
			if (item.FullName is ".metaData")
			{
				continue;
			}

			var path = CrossIO.Combine(metaData.Root, item.FullName);

			Directory.CreateDirectory(Path.GetDirectoryName(path));

			item.ExtractToFile(path, true);
		}

		return Task.FromResult(true);
	}

	private Task<bool> RestoreClearRoot(ZipArchive zipArchive, IBackupMetaData metaData)
	{
		new DirectoryInfo(metaData.Root).Delete(true);

		RestoreOverwrite(zipArchive, metaData);

		return Task.FromResult(true);
	}

	private Task<bool> RestoreClearRootOfSimilarFileTypes(ZipArchive zipArchive, IBackupMetaData metaData)
	{
		var entry = zipArchive.Entries.FirstOrDefault(x => x.FullName is not ".metaData");

		if (entry is null)
		{
			return Task.FromResult(false);
		}

		foreach (var item in Directory.GetFiles(metaData.Root, "*" + Path.GetExtension(entry.FullName), SearchOption.AllDirectories))
		{
			CrossIO.DeleteFile(item);
		}

		RestoreOverwrite(zipArchive, metaData);

		return Task.FromResult(true);
	}

	public void DoCleanup()
	{
		var availableBackups = GetAllBackups();

		availableBackups.RemoveAll(x => x.MetaData.IsArchived);

		if (_backupSettings.CleanupSettings.Type.HasFlag(BackupCleanupType.TimeBased) && _backupSettings.CleanupSettings.MaxTimespan.TotalDays >= 1)
		{
			availableBackups
				.Where(x => DateTime.Now - x.MetaData.ContentTime > _backupSettings.CleanupSettings.MaxTimespan)
				.Foreach(DoCleanup);
		}

		availableBackups.RemoveAll(x => x.MetaData.IsArchived);

		if (_backupSettings.CleanupSettings.Type.HasFlag(BackupCleanupType.CountBased) && _backupSettings.CleanupSettings.MaxBackups > 5)
		{
			availableBackups
				.Where(x => IsLarge(x.MetaData))
				.OrderByDescending(x => x.MetaData.BackupTime)
				.Skip(_backupSettings.CleanupSettings.MaxBackups)
				.Foreach(DoCleanup);
		}

		availableBackups.RemoveAll(x => x.MetaData.IsArchived);

		if (_backupSettings.CleanupSettings.Type.HasFlag(BackupCleanupType.StorageBased) && _backupSettings.CleanupSettings.MaxStorage > 10_000)
		{
			var totalSize = 0UL;

			foreach (var backup in availableBackups.OrderByDescending(x => x.MetaData.BackupTime))
			{
				if (totalSize < _backupSettings.CleanupSettings.MaxStorage)
				{
					totalSize += (ulong)backup.BackupFile.Length;
				}
				else
				{
					DoCleanup(backup);
				}
			}
		}
	}

	private void DoCleanup(IRestoreItem restoreItem)
	{
		if (!IsLarge(restoreItem.MetaData))
		{
			CrossIO.DeleteFile(restoreItem.BackupFile.FullName);
			return;
		}

		restoreItem.MetaData.IsArchived = true;

		using var stream = File.Create(restoreItem.BackupFile.FullName);
		using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, false);

		CreateEntry(zipArchive, ".metaData", JsonConvert.SerializeObject(restoreItem.MetaData));
	}

	private bool IsLarge(IBackupMetaData metaData)
	{
		return metaData.Type is nameof(BackupItem.SaveGames) or nameof(BackupItem.LocalMods);
	}
}
