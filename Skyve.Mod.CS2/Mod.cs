using Colossal.Json;
using Colossal.Logging;

using Game;
using Game.Modding;

using System;
using System.IO;

namespace Skyve.Mod.CS2
{
	public class Mod : IMod
	{
		public static ILog Log = LogManager.GetLogger($"{nameof(Skyve)}.{nameof(Mod)}", false);

		public void OnCreateWorld(UpdateSystem updateSystem)
		{
			Log.Info(nameof(OnCreateWorld));
		}

		public void OnDispose()
		{
			Log.Info(nameof(OnDispose));
		}

		public void OnLoad()
		{
			Log.Info(nameof(OnLoad));

			try
			{
				if (File.Exists(FolderSettings.FolderSettingsPath))
				{
					var folderSettings = JSON.MakeInto<FolderSettings>(JSON.Load(File.ReadAllText(FolderSettings.FolderSettingsPath)));

					folderSettings.Save();
				}
				else
				{
					new FolderSettings().Save();
				}
			}catch (Exception ex)
			{
				Log.Error(ex);
			}
		}
	}
}
