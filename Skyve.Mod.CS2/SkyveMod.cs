using Colossal.IO.AssetDatabase;
using Colossal.Json;
using Colossal.Logging;
using Colossal.PSI.Common;
using Colossal.PSI.PdxSdk;

using Game;
using Game.Modding;
using Game.SceneFlow;

using PDX.SDK.Contracts;

using Skyve.App.CS2.Installer;
using Skyve.Domain.CS2.Utilities;

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
		private static System.Version ModVer;

		public static ILog Log = LogManager.GetLogger(nameof(SkyveMod)).SetShowsErrorsInUI(false);

		public SkyveModSettings Settings { get; private set; }
		public static bool IsUpdateAvailable
		{
			get
			{
				CheckIfInstalled(out var isInstalled, out var isUpToDate);

				return isInstalled && !isUpToDate;
			}
		}

		public void OnLoad(UpdateSystem updateSystem)
		{
			Log.Info(nameof(OnLoad));

			if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
			{
				ModPath = Path.GetDirectoryName(asset.path);
				ModVer = asset.version;

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

			UpdateFolderSettings();

			UpdateDlcInfo();

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

		private static void UpdateFolderSettings()
		{
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
		}

		private static void UpdateDlcInfo()
		{
			try
			{
				var config = new DlcConfig();

				try
				{
					if (File.Exists(FolderSettings.DlcConfigPath))
					{
						config = JSON.MakeInto<DlcConfig>(JSON.Load(File.ReadAllText(FolderSettings.DlcConfigPath)));
					}
				}
				catch (Exception ex)
				{
					Log.Error(ex);
				}

				config.AvailableDLCs = new();

				foreach (var item in PlatformManager.instance.EnumerateDLCs())
				{
					Log.Info($"{item.backendId} {item.backendName}");
					if (PlatformManager.instance.IsDlcOwned(item) && uint.TryParse(item.backendId, out var id))
					{
						config.AvailableDLCs.Add(id);
					}
				}

				Log.Info(JSON.Dump(config));

				File.WriteAllText(FolderSettings.DlcConfigPath, JSON.Dump(config));
			}
			catch (Exception ex)
			{
				Log.Error(ex);
			}
		}

		private async void ListMods()
		{
			var pdxPlatform = PlatformManager.instance.GetPSI<PdxSdkPlatform>("PdxSdk");
			var context = typeof(PdxSdkPlatform).GetField("m_SDKContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(pdxPlatform) as IContext;

			var activePlayset = await context.Mods.GetActivePlayset();
			var enabledMods = await context.Mods.GetActivePlaysetEnabledMods();
			var list = new List<string>() { "N/A" };

			if (!activePlayset.Success)
			{
				Log.Warn(activePlayset.Error.ToString());
			}

			if (!enabledMods.Success)
			{
				Log.Warn(enabledMods.Error.ToString());
			}

			if (enabledMods.Mods?.Count > 0)
			{
				list = enabledMods.Mods.Select(x => $"{x.Name} - v{x.UserModVersion} ({x.Id})").ToList();
			}

			Log.Info("\n======= Current User =======\n\t" + context.Config.UserId +
					"\n======= Active Playset =======\n\t" + activePlayset.PlaysetId +
					"\n======= Enabled Mods =======\n\t" + string.Join("\n\t", list) + "\n============================" +
					"\n======= Loaded Assemblies =======\n\t" + string.Join("\n\t", GameManager.instance.modManager.ListModsEnabled()) + "\n============================");
		}

		public static bool InstallApp()
		{
			Log.Info(nameof(InstallApp));

			try
			{
				var setupFile = Path.Combine(ModPath, "Setup.exe");

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

		public static void CheckIfInstalled(out bool isInstalled, out bool isUpToDate)
		{
			var installedPath = GetCurrentInstallationPath();
			var skyvePath = Path.Combine(installedPath ?? string.Empty, "Skyve.exe");

			if (!File.Exists(skyvePath))
			{
				isInstalled = isUpToDate = false;
				return;
			}

			var skyveVersion = FileVersionInfo.GetVersionInfo(skyvePath).FileVersion;

			if (System.Version.TryParse(skyveVersion, out var skyveVer))
			{
				isInstalled = true;
				isUpToDate = skyveVer.Major == ModVer.Major
					&& skyveVer.Minor == ModVer.Minor
					&& Math.Max(0, skyveVer.Build) == Math.Max(0, ModVer.Build)
					&& Math.Max(0, skyveVer.Revision) == Math.Max(0, ModVer.Revision);
			}
			else
			{
				isInstalled = isUpToDate = false;
			}
		}

		private static string GetCurrentInstallationPath()
		{
			try
			{
				return File.ReadAllText(Path.Combine(FolderSettings.SettingsFolder, "InstallPath"));
			}
			catch
			{
				return @"C:\Program Files (x86)\Skyve CS-II";
			}
		}
	}
}
