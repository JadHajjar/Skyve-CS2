using Colossal.Json;
using Colossal.PSI.Common;
using Colossal.PSI.Environment;

using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;

using System.IO;

using UnityEngine;

namespace Skyve.Mod.CS2
{
	public class FolderSettings : IFolderSettings
	{
		public static string ContentFolder { get; }
		public static string SettingsFolder { get; }
		public static string FolderSettingsPath { get; }
		public static string DlcConfigPath { get; }

		public string AppDataPath { get; set; }
		public string GamePath { get; set; }
		public string SteamPath { get; set; }
		public Platform Platform { get; set; }
		public GamingPlatform GamingPlatform { get; set; }
		public string UserIdentifier { get; set; }
		public string UserIdType { get; set; }

		static FolderSettings()
		{
			ContentFolder = Path.Combine(EnvPath.kUserDataPath, "ModsData", nameof(Skyve));

			SettingsFolder = Path.Combine(EnvPath.kUserDataPath, "ModsSettings", nameof(Skyve));

			FolderSettingsPath = Path.Combine(SettingsFolder, $"{nameof(FolderSettings)}.json");

			DlcConfigPath = Path.Combine(ContentFolder, $"{nameof(DlcConfig)}.json");

			Directory.CreateDirectory(ContentFolder);
			Directory.CreateDirectory(SettingsFolder);
		}

		public void Save()
		{
			Colossal.PSI.PdxSdk.PdxSdkExtensions.GetUserIdType(out var userType);

			UserIdType = userType;
			AppDataPath = EnvPath.kUserDataPath;
			GamePath = Path.GetDirectoryName(EnvPath.kGameDataPath);
			UserIdentifier = PlatformManager.instance.userSpecificPath;
			GamingPlatform = GetGamingPlatform();
			Platform = GetPlatform();

			SkyveMod.Log.Info(FolderSettingsPath);
			SkyveMod.Log.Info(JSON.Dump(this));

			File.WriteAllText(FolderSettingsPath, JSON.Dump(this));
		}

		private GamingPlatform GetGamingPlatform()
		{
			return UserIdType switch
			{
				"xbox_xsts" => GamingPlatform.Microsoft,
				"steam" => GamingPlatform.Steam,
				_ => GamingPlatform.Unknown
			};
		}

		public static Platform GetPlatform()
		{
			return Application.platform switch
			{
				RuntimePlatform.LinuxPlayer or RuntimePlatform.LinuxEditor => Platform.Linux,
				RuntimePlatform.OSXEditor or RuntimePlatform.OSXPlayer => Platform.MacOSX,
				_ => Platform.Windows,
			};
		}

		public void Reload()
		{
			throw new System.NotImplementedException();
		}
	}
}
