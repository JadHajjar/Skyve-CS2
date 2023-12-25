using Extensions;

using Newtonsoft.Json;

using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Skyve.Domain.CS2.Utilities;
public class SessionSettings : ConfigFile, ISessionSettings
{
	#region Implementation
	private const string FILE_NAME = nameof(SessionSettings) + ".json";

	public SessionSettings() : base(FILE_NAME)
	{ }

	public static SessionSettings Load(string appDataPath)
	{
		var path = CrossIO.Combine(appDataPath, FILE_NAME);

		var settings = Load<SessionSettings>(path) ?? new();

		settings.FilePath = path;

		return settings;
	}
	#endregion

	public bool FirstTimeSetupCompleted { get; set; }
	public string? CurrentPlayset { get; set; }
	public Rectangle? LastWindowsBounds { get; set; }
	public bool WindowWasMaximized { get; set; }
	public bool SubscribeFirstTimeShown { get; set; }
	public bool CleanupFirstTimeShown { get; set; }
	public bool FpsBoosterLogWarning { get; set; }
	public string? LastVersionNotification { get; set; }
	public int LastVersioningNumber { get; set; }
	public bool DashboardFirstTimeShown { get; set; }
}
