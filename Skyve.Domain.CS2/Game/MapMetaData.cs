namespace Skyve.Domain.CS2.Game;

public class MapMetaData
{
	public string? DisplayName { get; set; }
	public string? Thumbnail { get; set; }
	public string? Preview { get; set; }
	public string? Theme { get; set; }
	public float Cloudiness { get; set; }
	public float Precipitation { get; set; }
	public float Latitude { get; set; }
	public float Longitude { get; set; }
	public float BuildableLand { get; set; }
	public float Area { get; set; }
	public float WaterAvailability { get; set; }
	public string[]? ContentPrerequisite { get; set; }
	public bool NameAsCityName { get; set; }
	public int StartingYear { get; set; }
	public string? MapData { get; set; }
	public string? SessionGuid { get; set; }
	public MapTemperatureRange? TemperatureRange { get; set; }
	public MapResources? Resources { get; set; }
	public MapConnections? Connections { get; set; }

	public class MapTemperatureRange
	{
		public float Min { get; set; }
		public float Max { get; set; }
	}

	public class MapResources
	{
		public float Fertile { get; set; }
		public float Forest { get; set; }
		public float Oil { get; set; }
		public float Ore { get; set; }
	}

	public class MapConnections
	{
		public bool Road { get; set; }
		public bool Train { get; set; }
		public bool Air { get; set; }
		public bool Ship { get; set; }
		public bool Electricity { get; set; }
		public bool Water { get; set; }
	}
}
