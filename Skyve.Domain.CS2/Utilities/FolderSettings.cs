using Extensions;

using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable

namespace Skyve.Domain.CS2.Utilities;
public class FolderSettings : ConfigFile, IFolderSettings
{
	#region Implementation
	private const string FILE_NAME = nameof(FolderSettings) + ".json";

	public FolderSettings() : base(FILE_NAME)
	{ }

	public static FolderSettings Load(string appDataPath)
	{
		var path = CrossIO.Combine(appDataPath, FILE_NAME);

		var settings = Load<FolderSettings>(path) ?? new();

		settings.AppDataPath = Path.GetDirectoryName(Path.GetDirectoryName(appDataPath));
		settings.FilePath = path;

		return settings;
	}
	#endregion

	public string GamePath { get; set; }
	public string AppDataPath { get; set; }
	public string SteamPath { get; set; }
	public GamingPlatform GamingPlatform { get; set; }
	public Platform Platform { get; set; }
	public string UserIdentifier { get; set; }
}
#nullable enable