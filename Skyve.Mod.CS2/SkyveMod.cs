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

namespace Skyve.Mod.CS2
{
	public class SkyveMod : IMod
	{
		private static string ModPath;

		public static ILog Log = LogManager.GetLogger(nameof(SkyveMod)).SetShowsErrorsInUI(false);

		public SkyveModSettings Settings { get; private set; }

		public void OnLoad(UpdateSystem updateSystem)
		{
			Log.Info(nameof(OnLoad));

			if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
			{
				ModPath = Path.GetDirectoryName(asset.path);
			}

			updateSystem.UpdateAt<InstallSkyveUISystem>(SystemUpdatePhase.UIUpdate);

			//Settings = new SkyveModSettings(this);

			//Settings.RegisterInOptionsUI();

			foreach (var item in Locale.GetAvailableSources())
			{
				GameManager.instance.localizationManager.AddSource(item.LocaleId, item);
			}

			//AssetDatabase.global.LoadSettings(nameof(SkyveModSettings), Settings, new SkyveModSettings(this));

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

		public static void InstallApp()
		{
			Log.Info(nameof(InstallApp));

			Process.Start(Path.Combine(ModPath, "Skyve Setup.exe"));
		}

		public void OnDispose()
		{
			Log.Info(nameof(OnDispose));
		}
	}
}
