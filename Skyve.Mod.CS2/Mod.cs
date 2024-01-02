using Colossal.IO.AssetDatabase;
using Colossal.Json;
using Colossal.Logging;

using Game;
using Game.Modding;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
			}
			catch (Exception ex)
			{
				Log.Error(ex);
			}

			var assets = AssetDatabase.global.GetAssets<AssetData>();

			Log.Info("ASSETS " + assets.Count());

			var dic = new Dictionary<string, int>();

			foreach (var item in assets)
			{
				var name = item.GetType().Name;

				if (dic.ContainsKey(name))
					dic[name]++;
				else
					dic[name] = 1;
			}

			foreach (var item in dic)
			{
				Log.Info("  " + item.Key + "  " + item.Value);

			}
		}
	}
}
