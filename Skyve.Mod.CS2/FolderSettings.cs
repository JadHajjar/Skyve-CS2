using Colossal.Json;
using Colossal.PSI.Common;
using Colossal.PSI.Environment;

using Extensions;

using Skyve.Domain;
using Skyve.Domain.Enums;

using System;
using System.IO;

using UnityEngine;
using UnityEngine.Rendering;

namespace Skyve.Mod.CS2
{
	public class FolderSettings : IFolderSettings
	{
		public static string ContentFolder { get; }
		public static string SettingsFolder { get; }
		public static string FolderSettingsPath { get; }

		public string AppDataPath { get; set; }
		public string GamePath { get; set; }
		public string SteamPath { get; set; }
		public Platform Platform { get; set; }
		public GamingPlatform GamingPlatform { get; set; }
		public string UserIdentifier { get; set; }

        static FolderSettings()
		{
			ContentFolder = Path.Combine(EnvPath.kUserDataPath, "ModsData", nameof(Skyve));

			SettingsFolder = Path.Combine(EnvPath.kUserDataPath, "ModsSettings", nameof(Skyve));

			FolderSettingsPath = Path.Combine(SettingsFolder, $"{nameof(FolderSettings)}.json");

			Directory.CreateDirectory(ContentFolder);
			Directory.CreateDirectory(SettingsFolder);
		}

		public void Save()
		{
			AppDataPath = EnvPath.kUserDataPath;
			GamePath = Path.GetDirectoryName(EnvPath.kGameDataPath);
			UserIdentifier = PlatformManager.instance.userSpecificPath;
			Platform = GetPlatform();

			Mod.Log.Info(FolderSettingsPath);
			Mod.Log.Info(JSON.Dump(this));

			File.WriteAllText(FolderSettingsPath, JSON.Dump(this));
		}

		public static Platform GetPlatform()
		{
			switch (Application.platform)
			{
				case RuntimePlatform.LinuxPlayer:
				case RuntimePlatform.LinuxEditor:
					return Platform.Linux;
				case RuntimePlatform.OSXEditor:
				case RuntimePlatform.OSXPlayer:
					return Platform.MacOSX;
				default:
					return Platform.Windows;
			}
		}
	}
}
