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
	private readonly INotifier _notifier;
	private readonly ILogger _logger;
	private readonly IContentManager _contentManager;
	private readonly IPlaysetManager _playsetManager;
	private readonly IPackageManager _packageManager;
	private readonly IWorkshopService _workshopService;
	private readonly BackupSettings _backupSettings;
	private readonly DateTime _backupTime;

	private static readonly object _lock = new();
	private static DateTime lastCacheTime;
	private static List<IRestoreItem>? restoreItemsCache;

	public IBackupInstructions BackupInstructions { get; } = new BackupInstructions();
	public IRestoreInstructions RestoreInstructions { get; } = new RestoreInstructions();

	public BackupSystem(ISettings settings, INotifier notifier, ILogger logger, IContentManager contentManager, IPlaysetManager playsetManager, IPackageManager packageManager, IWorkshopService workshopService)
	{
		_settings = settings;
		_notifier = notifier;
		_logger = logger;
		_contentManager = contentManager;
		_playsetManager = playsetManager;
		_packageManager = packageManager;
		_workshopService = workshopService;
		_backupSettings = (settings.BackupSettings as BackupSettings)!;

		_backupTime = DateTime.Now;
	}

	public string[] GetBackupTypes()
	{
		return [
			nameof(BackupItem.ActivePlayset),
			nameof(BackupItem.SettingsFiles),
			nameof(BackupItem.ModsSettingsFiles),
			nameof(BackupItem.SaveGames),
			nameof(BackupItem.Maps),
			nameof(BackupItem.LocalMods),
		];
	}

	public void Save(IBackupMetaData metaData, string[] files, object? itemMetaData)
	{
		metaData.BackupTime = _backupTime;
		metaData.FileCount = files.Length;

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

		_logger.Info("[Backup] Creating backup file for: " + metaData.Name);

		var path = CrossIO.Combine(folder, $"{metaData.Name}_{metaData.ContentTime.Ticks}_{_backupTime.Ticks}.sbak");

		using var fileStream = File.Create(path);
		using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create, false);

		if (itemMetaData is not null)
		{
			metaData.ItemMetaDataType = itemMetaData.GetType().AssemblyQualifiedName;

			CreateEntry(zipArchive, ".backupItemMetaData", JsonConvert.SerializeObject(itemMetaData));
		}

		CreateEntry(zipArchive, ".backupMetaData", JsonConvert.SerializeObject(metaData));

		foreach (var file in files.Where(CrossIO.FileExists))
		{
			zipArchive.CreateEntryFromFile(file
				, string.IsNullOrEmpty(metaData.Root) ? Path.GetFileName(file) : file.Substring(metaData.Root!.Length + 1).Replace("/", "\\")
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

		lock (_lock)
		{
			if (lastCacheTime > DateTime.Now.AddSeconds(-10) && restoreItemsCache is not null)
			{
				return restoreItemsCache;
			}

			var files = Directory.GetFiles(_backupSettings.DestinationFolder, "*.sbak", SearchOption.AllDirectories);
			var items = new List<IRestoreItem>(files.Length);

			foreach (var file in files)
			{
				try
				{
					var restoreItem = LoadBackupFile(file);

					if (restoreItem is not null)
					{
						items.Add(restoreItem);
					}
				}
				catch { }
			}

			lastCacheTime = DateTime.Now;

			return restoreItemsCache = items;
		}
	}

	public IRestoreItem? LoadBackupFile(string fileName)
	{
		try
		{
			using var stream = File.OpenRead(fileName);
			using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read, false);

			using var metaDataStream = zipArchive.GetEntry(".backupMetaData").Open();
			using var reader = new StreamReader(metaDataStream);

			var metaData = JsonConvert.DeserializeObject<BackupMetaData>(reader.ReadToEnd());
			object? itemMetaData = null;

			if (!metaData.IsArchived && metaData.ItemMetaDataType is not null and not "")
			{
				using var itemMetaDataStream = zipArchive.GetEntry(".backupItemMetaData").Open();
				using var reader2 = new StreamReader(itemMetaDataStream);

				itemMetaData = JsonConvert.DeserializeObject(reader2.ReadToEnd(), Type.GetType(metaData.ItemMetaDataType));
			}

			return new BackupItem.Zip(fileName, metaData, itemMetaData);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to load backup file: " + fileName);

			return null;
		}
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
			_notifier.IsBackingUp = true;
			_notifier.OnBackupStarted();

			var availableBackups = GetAllBackups();

			SaveBackupItem(MakeSettingsBackup());
			SaveBackupItem(MakeModsSettingsBackup());

			(await MakePlaysetBackup()).Foreach(SaveBackupItem);

			if (BackupInstructions.DoSavesBackup)
			{
				MakeSavesBackup(availableBackups).Foreach(SaveBackupItem);
			}

			if (BackupInstructions.DoMapsBackup)
			{
				MakeMapsBackup(availableBackups).Foreach(SaveBackupItem);
			}

			if (BackupInstructions.DoLocalModsBackup)
			{
				MakeLocalModsBackup(availableBackups).Foreach(SaveBackupItem);
			}

			DoCleanup();

			restoreItemsCache = null;
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to do backup");

			return false;
		}
		finally
		{
			_notifier.IsBackingUp = false;

			_notifier.OnBackupEnded();
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

	private IEnumerable<IBackupItem> MakeMapsBackup(List<IRestoreItem> availableBackups)
	{
		var mapPackage = _contentManager.GetMapFiles();
		var maps = mapPackage?.LocalData?.Assets ?? [];
		var backupItems = new List<IBackupItem>();

		foreach (var map in maps)
		{
			if (availableBackups.Any(x => x.MetaData.Type is nameof(BackupItem.Maps)
				&& x.MetaData.Name == map.Name
				&& x.MetaData.ContentTime == map.LocalTime))
			{
				continue;
			}

			backupItems.Add(new BackupItem.Maps(map));
		}

		return backupItems;
	}

	private IBackupItem MakeSettingsBackup()
	{
		var settingsFiles = Directory.EnumerateFiles(_settings.FolderSettings.AppDataPath, "*.coc", SearchOption.AllDirectories)
			.Where(item =>
			{
				var lines = File.ReadAllLines(item);

				return lines.Length > 2
					&& lines[1] == "{"
					&& lines[lines.Length - 1] == "}"
					&& !lines.Any(x => x.Contains('\0'))
					&& lines.Count(x => x.Trim().StartsWith("}") || x.Trim().EndsWith("{")) % 2 == 0;
			})
			.ToArray();

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

	private async Task<IEnumerable<IBackupItem>> MakePlaysetBackup()
	{
		var playsets = await _workshopService.GetPlaysets(false);

		if (playsets is null)
		{
			return [];
		}

		var backups = new List<IBackupItem>();

		foreach (var item in playsets)
		{
			var playset = await _playsetManager.GenerateImportPlayset(item);

			backups.Add(new BackupItem.ActivePlayset(playset, item, _playsetManager));
		}

		return backups;
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
		try
		{
			RestoreDelegate method = metaData.RestoreType switch
			{
				RestoreAction.Playset => RestorePlayset,
				RestoreAction.Overwrite => RestoreOverwrite,
				RestoreAction.RestoreIfMissing => RestoreIfMissing,
				RestoreAction.ClearRoot => RestoreClearRoot,
				RestoreAction.ClearRootOfSimilarFileTypes => RestoreClearRootOfSimilarFileTypes,
				_ => throw new NotImplementedException()
			};

			using var stream = File.OpenRead(file);
			using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read, false);

			return await method(zipArchive, metaData);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Failed to restore the {metaData.Type} backup ({metaData.Name}) from '{file}'");

			return false;
		}
	}

	private async Task<bool> RestorePlayset(ZipArchive zipArchive, IBackupMetaData metaData)
	{
		var entry = zipArchive.Entries.FirstOrDefault(x => x.FullName is not ".backupMetaData" and not ".backupItemMetaData");

		var temp = CrossIO.GetTempFileName();

		try
		{
			entry.ExtractToFile(temp);

			var playset = await _playsetManager.ImportPlayset(temp, true);

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
			if (item.FullName is ".backupMetaData" or ".backupItemMetaData")
			{
				continue;
			}

			var path = CrossIO.Combine(metaData.Root, item.FullName);

			Directory.CreateDirectory(Path.GetDirectoryName(path));

			item.ExtractToFile(path, true);
		}

		return Task.FromResult(true);
	}

	private Task<bool> RestoreIfMissing(ZipArchive zipArchive, IBackupMetaData metaData)
	{
		foreach (var item in zipArchive.Entries)
		{
			if (item.FullName is ".backupMetaData" or ".backupItemMetaData")
			{
				continue;
			}

			var path = CrossIO.Combine(metaData.Root, item.FullName);

			if (CrossIO.FileExists(path))
			{
				continue;
			}

			Directory.CreateDirectory(Path.GetDirectoryName(path));

			item.ExtractToFile(path, true);
		}

		return Task.FromResult(true);
	}

	private async Task<bool> RestoreClearRoot(ZipArchive zipArchive, IBackupMetaData metaData)
	{
		new DirectoryInfo(metaData.Root).Delete(true);

		return await RestoreOverwrite(zipArchive, metaData);
	}

	private Task<bool> RestoreClearRootOfSimilarFileTypes(ZipArchive zipArchive, IBackupMetaData metaData)
	{
		var entry = zipArchive.Entries.FirstOrDefault(x => x.FullName is not ".backupMetaData" and not ".backupItemMetaData");

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
			_logger.Info("[Backup] Running Cleanup (Time)");

			availableBackups
				.Where(x => DateTime.Now - x.MetaData.BackupTime > _backupSettings.CleanupSettings.MaxTimespan)
				.Foreach(DoCleanup);
		}

		availableBackups.RemoveAll(x => x.MetaData.IsArchived);

		if (_backupSettings.CleanupSettings.Type.HasFlag(BackupCleanupType.CountBased) && _backupSettings.CleanupSettings.MaxBackups > 5)
		{
			_logger.Info("[Backup] Running Cleanup (Count)");

			availableBackups
				.GroupBy(x => x.MetaData.BackupTime)
				.OrderByDescending(x => x.Key)
				.Skip(_backupSettings.CleanupSettings.MaxBackups)
				.Foreach(x => x.Foreach(DoCleanup));
		}

		availableBackups.RemoveAll(x => x.MetaData.IsArchived);

		if (_backupSettings.CleanupSettings.Type.HasFlag(BackupCleanupType.StorageBased) && _backupSettings.CleanupSettings.MaxStorage > 10_000)
		{
			_logger.Info("[Backup] Running Cleanup (Storage)");

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
		_logger.Info("[Backup] Deleting backup file for: " + restoreItem.MetaData.Name);

		if (!IsLarge(restoreItem.MetaData))
		{
			CrossIO.DeleteFile(restoreItem.BackupFile.FullName);
			return;
		}

		restoreItem.MetaData.IsArchived = true;

		using var stream = File.Create(restoreItem.BackupFile.FullName);
		using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Create, false);

		CreateEntry(zipArchive, ".backupMetaData", JsonConvert.SerializeObject(restoreItem.MetaData));
	}

	public List<string> ListAllFilesInBackup(IRestoreItem restoreItem)
	{
		using var stream = File.OpenRead(restoreItem.BackupFile.FullName);
		using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read, false);

		var files = new List<string>();

		foreach (var item in zipArchive.Entries)
		{
			if (item.FullName is ".backupMetaData" or ".backupItemMetaData")
			{
				continue;
			}

			files.Add(CrossIO.Combine(restoreItem.MetaData.Root, item.FullName));
		}

		return files;
	}

	private bool IsLarge(IBackupMetaData metaData)
	{
		return metaData.Type is nameof(BackupItem.SaveGames) or nameof(BackupItem.LocalMods) or nameof(BackupItem.Maps);
	}
}
