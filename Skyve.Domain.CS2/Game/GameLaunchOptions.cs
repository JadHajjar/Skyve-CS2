using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Game;
public struct GameLaunchOptions
{
	public bool HideUserSection { get; set; }
	public bool NoAssets { get; set; }
	public bool NoMods { get; set; }
	public bool LoadSaveGame { get; set; }
	public bool StartNewGame { get; set; }
	public string? MapToLoad { get; set; }
	public string? SaveToLoad { get; set; }
	public bool UIDeveloperMode { get; set; }
	public bool DeveloperMode { get; set; }
	public bool UseCitiesExe { get; set; }
	public string? CustomArgs { get; set; }
	public string LogLevel { get; set; }
	public bool LogsToPlayerLog { get; set; }
}
