using Extensions;

using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Skyve.Domain.CS2.Notifications;

public class CorruptedSettingsFilesNotification : INotificationInfo
{
	private readonly IBackupSystem _backupSystem;
	private readonly INotificationsService _notificationsService;
	private readonly List<string> _corruptedFiles;
	private readonly List<IRestoreItem> _backupsToRestore;

	public CorruptedSettingsFilesNotification(List<string> corruptedFiles, IBackupSystem backupSystem, INotificationsService notificationsService)
	{
		_backupSystem = backupSystem;
		_notificationsService = notificationsService;
		_corruptedFiles = corruptedFiles;
		_backupsToRestore = FindBackups();

		Time = DateTime.Now;
		Title = LocaleCS2.CorruptedSettingsFiles.FormatPlural(corruptedFiles.Count);
		Description = _backupsToRestore.Count > 0 ? LocaleCS2.CorruptedSettingsFilesBackupAvailable : LocaleCS2.CorruptedSettingsFilesNoBackup;
		Icon = _backupsToRestore.Count > 0 ? "SafeShield" : "Broken";
		HasAction = true;
	}

	public DateTime Time { get; }
	public string Title { get; }
	public string? Description { get; }
	public string Icon { get; }
	public Color? Color => FormDesign.Design.RedColor;
	public bool HasAction { get; }
	public bool CanBeRead { get; }

	public async void OnClick()
	{
		foreach (var file in _corruptedFiles)
		{
			CrossIO.DeleteFile(file, true);
		}

		foreach (var backup in _backupsToRestore)
		{
			await backup.Restore(_backupSystem);
		}

		_notificationsService.RemoveNotification(this);
	}

	public void OnRead()
	{
	}

	public void OnRightClick()
	{
	}

	private List<IRestoreItem> FindBackups()
	{
		var allBackups = _backupSystem.GetAllBackups();
		var filesNotFound = new List<string>(_corruptedFiles);
		var validBackups = new List<IRestoreItem>();

		foreach (var backup in allBackups.OrderByDescending(x => x.MetaData.BackupTime))
		{
			if (backup.MetaData.Type != nameof(BackupItem.SettingsFiles))
			{
				continue;
			}

			foreach (var path in _backupSystem.ListAllFilesInBackup(backup))
			{
				for (var i = filesNotFound.Count - 1; i >= 0; i--)
				{
					if (CrossIO.PathEquals(path, filesNotFound[i]))
					{
						filesNotFound.RemoveAt(i);

						backup.MetaData.RestoreType = Domain.Enums.RestoreAction.RestoreIfMissing;

						validBackups.AddIfNotExist(backup);

						if (filesNotFound.Count == 0)
						{
							return validBackups;
						}
					}
				}
			}
		}

		return validBackups;
	}
}
