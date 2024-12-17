using Colossal.IO.AssetDatabase;
using Colossal.Json;
using Colossal.Logging;

using Game;
using Game.Modding;
using Game.SceneFlow;

using Skyve.App.CS2.Installer;

using System;
using System.IO;

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

				Log.Info(ModPath);
			}

			GameManager.instance.RegisterUpdater(ListMods);

			updateSystem.UpdateAt<InstallSkyveUISystem>(SystemUpdatePhase.UIUpdate);

			Settings = new SkyveModSettings(this);

			Settings.RegisterInOptionsUI();

			foreach (var item in Locale.GetAvailableSources())
			{
				GameManager.instance.localizationManager.AddSource(item.LocaleId, item);
			}

			AssetDatabase.global.LoadSettings(nameof(SkyveMod), Settings, new SkyveModSettings(this));

			try
			{
				if (File.Exists(FolderSettings.FolderSettingsPath))
				{
					try
					{
						var folderSettings = JSON.MakeInto<FolderSettings>(JSON.Load(File.ReadAllText(FolderSettings.FolderSettingsPath)));

						folderSettings.Save();

						Log.Info("Folder settings updated");
					}
					catch
					{
						new FolderSettings().Save();

						Log.Info("Folder settings created [RECOVERY]");

						throw;
					}
				}
				else
				{
					new FolderSettings().Save();

					Log.Info("Folder settings created");
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex);
			}

			//var assets = AssetDatabase.global.GetAssets<AssetData>();

			//Log.Info("ASSETS " + assets.Count());

			//var dic = new Dictionary<string, int>();

			//foreach (var item in assets)
			//{
			//	var name = item.GetType().Name;

			//	if (dic.ContainsKey(name))
			//	{
			//		dic[name]++;
			//	}
			//	else
			//	{
			//		dic[name] = 1;
			//	}
			//}

			//foreach (var item in dic)
			//{
			//	Log.Info("  " + item.Key + "  " + item.Value);
			//}
		}

		private void ListMods()
		{
			Log.Info("\n======= Enabled Mods =======\n\t" + string.Join("\n\t", GameManager.instance.modManager.ListModsEnabled()) + "\n============================");
		}

		public static bool InstallApp()
		{
			Log.Info(nameof(InstallApp));

			try
			{
				var setupFile = Path.Combine(ModPath, "Skyve Setup.exe");

				InstallHelper.Run(setupFile);

				return true;
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Failed to start the setup app");

				return false;
			}
		}

		public void OnDispose()
		{
			Log.Info(nameof(OnDispose));
		}
	}
}
