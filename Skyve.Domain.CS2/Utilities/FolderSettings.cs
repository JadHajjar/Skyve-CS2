using Extensions;

using Skyve.Domain.Systems;
using System.Collections.Generic;


#nullable disable

namespace Skyve.Domain.CS2.Utilities;
public class FolderSettings : ConfigFile, IFolderSettings
{
	#region Implementation
	private const string FILE_NAME = nameof(FolderSettings) + ".json";

	public List<uint> RemovedDLCs { get; set; } = new();

	public FolderSettings() : base(GetFilePath())
	{ }

	private static string GetFilePath()
	{
		return CrossIO.Combine(ServiceCenter.Get<ILocationManager>().SkyveAppDataPath, FILE_NAME);
	}

	public static FolderSettings Load()
	{
		return Load<FolderSettings>(GetFilePath()) ?? new();
	}
	#endregion

	public string GamePath { get; set; }
	public string AppDataPath { get; set; }
	public string SteamPath { get; set; }
	public Platform Platform { get; set; }
}
#nullable enable