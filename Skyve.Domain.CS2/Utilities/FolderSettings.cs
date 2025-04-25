using Extensions;

using Skyve.Domain.Enums;

#nullable disable

namespace Skyve.Domain.CS2.Utilities;

[SaveName(nameof(FolderSettings) + ".json")]
public class FolderSettings : ConfigFile, IFolderSettings
{
	public string GamePath { get; set; }
	public string AppDataPath { get; set; }
	public string SteamPath { get; set; }
	public GamingPlatform GamingPlatform { get; set; }
	public Platform Platform { get; set; }
	public string UserIdentifier { get; set; }
	public string UserIdType { get; set; }

	public void Save()
	{
		Handler?.Save(this);
	}
}
#nullable enable