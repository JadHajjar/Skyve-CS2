using Extensions;

using Newtonsoft.Json;

using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Skyve.Domain.CS2.Utilities;
public class SessionSettings : ConfigFile, ISessionSettings
{
	#region Implementation
	private const string FILE_NAME = nameof(SessionSettings) + ".json";

	public SessionSettings() : base(GetFilePath())
	{ }

	private static string GetFilePath()
	{
		return CrossIO.Combine(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Cities Skylines II")), "Cities Skylines II", "ModSettings", FILE_NAME);
	}

	public static SessionSettings Load()
	{
		return Load<SessionSettings>(GetFilePath()) ?? new();
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
