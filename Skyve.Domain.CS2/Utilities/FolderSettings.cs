using Extensions;

using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;


#nullable disable

namespace Skyve.Domain.CS2.Utilities;
public class FolderSettings : ConfigFile, IFolderSettings
{
	#region Implementation
	private const string FILE_NAME = nameof(FolderSettings) + ".json";

	public FolderSettings() : base(GetFilePath())
	{ }

	private static string GetFilePath()
	{
		return CrossIO.Combine(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Cities Skylines II")), "Cities Skylines II", "ModSettings", FILE_NAME);
	}

	public static FolderSettings Load()
	{
		return Load<FolderSettings>(GetFilePath()) ?? new();
	}
	#endregion

	public string GamePath { get; set; }
	public string AppDataPath { get; set; }
	public string SteamPath { get; set; }
	public GamingPlatform GamingPlatform { get; set; }
	public Platform Platform { get; set; }
}
#nullable enable