using Extensions;

using Newtonsoft.Json;

using Skyve.Domain.CS2.Content;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.IO;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Utilities;
public static class BackupItem
{
	public class SaveGames : IBackupItem
	{
		private readonly IAsset _save;
		public IBackupMetaData MetaData { get; }

		public SaveGames(IAsset save)
		{
			_save = save;

			MetaData = new BackupMetaData
			{
				Name = _save.Name,
				ContentTime = _save.LocalTime,
				Root = _save.Folder,
				Type = nameof(SaveGames),
				RestoreType = RestoreAction.Overwrite
			};
		}

		public bool CanSave()
		{
			return true;
		}

		public void Save(IBackupSystem backupManager)
		{
			backupManager.Save(MetaData, [_save.FilePath, _save.FilePath + ".cid"], ((Asset)_save).SaveGameMetaData);
		}
	}

	public class SettingsFiles : IBackupItem
	{
		private readonly string[] _settingsFiles;
		private readonly string _appData;
		public IBackupMetaData MetaData { get; }

		public SettingsFiles(string[] settingsFiles, string appData)
		{
			_settingsFiles = settingsFiles;
			_appData = appData;

			MetaData = new BackupMetaData
			{
				Name = "SettingsFiles",
				ContentTime = DateTime.Now,
				Root = _appData,
				Type = nameof(SettingsFiles),
				RestoreType = RestoreAction.ClearRootOfSimilarFileTypes
			};
		}

		public bool CanSave()
		{
			return true;
		}

		public void Save(IBackupSystem backupManager)
		{
			backupManager.Save(MetaData, _settingsFiles, null);
		}
	}

	public class ModsSettingsFiles : IBackupItem
	{
		private readonly string[] _modSettingFolders;
		private readonly string _appData;
		public IBackupMetaData MetaData { get; }

		public ModsSettingsFiles(string[] modSettingFolders, string appData)
		{
			_modSettingFolders = modSettingFolders;
			_appData = appData;

			MetaData = new BackupMetaData
			{
				Name = "ModsSettingsFiles",
				ContentTime = DateTime.Now,
				Root = _appData,
				Type = nameof(ModsSettingsFiles),
				RestoreType = RestoreAction.ClearRoot
			};
		}

		public bool CanSave()
		{
			return true;
		}

		public void Save(IBackupSystem backupManager)
		{
			backupManager.Save(MetaData, _modSettingFolders, null);
		}
	}

	public class ActivePlayset : IBackupItem
	{
		private readonly object _playset;
		public IBackupMetaData MetaData { get; }

		public ActivePlayset(object playset)
		{
			_playset = playset;

			MetaData = new BackupMetaData
			{
				Name = "ActivePlayset",
				ContentTime = DateTime.Now,
				Root = string.Empty,
				Type = nameof(ActivePlayset),
				RestoreType = RestoreAction.Playset
			};
		}

		public bool CanSave()
		{
			return true;
		}

		public void Save(IBackupSystem backupManager)
		{
			var tempPath = CrossIO.GetTempFileName();

			File.WriteAllText(tempPath, JsonConvert.SerializeObject(_playset));

			backupManager.Save(MetaData, [tempPath], null);

			CrossIO.DeleteFile(tempPath, true);
		}
	}

	public class LocalMods : IBackupItem
	{
		private readonly IPackage _package;
		public IBackupMetaData MetaData { get; }

		public LocalMods(IPackage package)
		{
			_package = package;

			MetaData = new BackupMetaData
			{
				Name = package.Name,
				ContentTime = package.LocalData!.LocalTime,
				Root = package.LocalData!.Folder,
				Type = nameof(LocalMods),
				RestoreType = RestoreAction.ClearRoot
			};
		}

		public bool CanSave()
		{
			return true;
		}

		public void Save(IBackupSystem backupManager)
		{
			backupManager.Save(MetaData, Directory.GetFiles(_package.LocalData!.Folder, "*", SearchOption.AllDirectories), null);
		}
	}

	public class Zip(string file, IBackupMetaData metaData, object? itemMetaData) : IRestoreItem
	{
		public IBackupMetaData MetaData { get; } = metaData;
		public object? ItemMetaData { get; } = itemMetaData;
		public FileInfo BackupFile => new(file);

		public async Task<bool> Restore(IBackupSystem backupManager)
		{
			return await backupManager.Restore(MetaData, file);
		}
	}
}
