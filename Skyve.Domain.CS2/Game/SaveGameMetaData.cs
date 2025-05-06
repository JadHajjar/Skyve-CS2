namespace Skyve.Domain.CS2.Game;
public class SaveGameMetaData
{
	public string? Preview { get; set; }
	public string? Theme { get; set; }
	public string? CityName { get; set; }
	public int Population { get; set; }
	public int Money { get; set; }
	public int Xp { get; set; }
	public string? MapName { get; set; }
	public string[]? ModsEnabled { get; set; }
	public string? SaveGameData { get; set; }
	public bool AutoSave { get; set; }
	public string? SessionGuid { get; set; }
	public SaveSimulationDate? SimulationDate { get; set; }
	public SaveOptions? Options { get; set; }

	public class SaveSimulationDate
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public int Hour { get; set; }
		public int Minute { get; set; }
	}

	public class SaveOptions
	{
		public bool LeftHandTraffic { get; set; }
		public bool NaturalDisasters { get; set; }
		public bool UnlockAll { get; set; }
		public bool UnlimitedMoney { get; set; }
	}
}
