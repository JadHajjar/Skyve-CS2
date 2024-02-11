using Colossal.IO.AssetDatabase;
using Colossal.Json;
using Colossal.Logging;
using Colossal.PSI.Environment;

using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.Settings;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Skyve.Mod.CS2
{
	public class SkyveMod : IMod
	{
		public static ILog Log = LogManager.GetLogger(nameof(SkyveMod), false);

		public SkyveModSettings Settings { get; private set; }

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

			Settings = new SkyveModSettings(this);

			Settings.RegisterInOptionsUI();

			foreach (var item in Locale.GetAvailableSources())
			{
				GameManager.instance.localizationManager.AddSource(item.LocaleId, item);
			}

			AssetDatabase.global.LoadSettings(nameof(SkyveModSettings), Settings, new SkyveModSettings(this));

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

		public void InstallApp()
		{
			Log.Info(nameof(InstallApp));

			Process.Start(Path.Combine(EnvPath.kUserDataPath, "Mods", "Skyve Mod", "Skyve Setup.exe"));
		}
	}
}
