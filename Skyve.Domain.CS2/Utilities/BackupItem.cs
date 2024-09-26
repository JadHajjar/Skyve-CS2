using System;
using System.Collections.Generic;

namespace Skyve.Domain.CS2.Utilities;
public abstract class BackupItem(string name) : IBackupItem
{
	public DateTime Time { get; }
	public string Name { get; } = name;

	public virtual bool CanSave()
	{
		return true;
	}

	public abstract void Save();

	public class SaveGame(IAsset save) : BackupItem(save.Name)
	{
		public override void Save()
		{
			throw new NotImplementedException();
		}
	}

	public class SettingsFiles(string[] settingsFiles, string[] modSettingFolders) : BackupItem("SettingsFiles")
	{
		public override void Save()
		{
			throw new NotImplementedException();
		}
	}

	public class ActivePlayset(List<IPlaysetPackage> playsetPackages) : BackupItem("ActivePlayset")
	{
		public override void Save()
		{
			throw new NotImplementedException();
		}
	}

	public class LocalMods(List<IPackage> packages) : BackupItem("LocalMods")
	{
		public override void Save()
		{
			throw new NotImplementedException();
		}
	}
}
