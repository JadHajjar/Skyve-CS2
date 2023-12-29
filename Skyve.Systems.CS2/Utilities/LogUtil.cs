using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Skyve.Systems.CS2.Utilities;
internal class LogUtil : ILogUtil
{
	private readonly ICompatibilityManager _compatibilityManager;
	private readonly ILocationService _locationManager;
	private readonly IPackageManager _contentManager;
	private readonly IPlaysetManager _playsetManager;
	private readonly ILogger _logger;
	private readonly ISettings _settings;

	public LogUtil(ILocationService locationManager, IPackageManager contentManager, IPlaysetManager profileManager, ILogger logger, ICompatibilityManager compatibilityManager, ISettings settings)
	{
		_compatibilityManager = compatibilityManager;
		_locationManager = locationManager;
		_contentManager = contentManager;
		_playsetManager = profileManager;
		_logger = logger;
		_settings = settings;

		try
		{
			foreach (var item in Directory.GetFiles(CrossIO.Combine(_locationManager.SkyveSettingsPath, "Support Logs")))
			{
				if (DateTime.Now - File.GetLastWriteTime(item) > TimeSpan.FromDays(15))
				{
					CrossIO.DeleteFile(item);
				}
			}
		}
		catch { }
	}

	public string GameLogFile => CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Player.log");

	public string GameDataPath => CrossIO.CurrentPlatform switch
	{
		Platform.MacOSX => CrossIO.Combine(_settings.FolderSettings.GamePath, "Cities2.app", "Contents"),
		_ => CrossIO.Combine(_settings.FolderSettings.GamePath, "Cities2_Data")
	};

	public string CreateZipFileAndSetToClipboard(string? folder = null)
	{
		var file = CrossIO.Combine(folder ?? Path.GetTempPath(), $"LogReport_{DateTime.Now:yy-MM-dd_HH-mm}.zip");

		using (var fileStream = File.Create(file))
		{
			CreateZipToStream(fileStream);
		}

		PlatformUtil.SetFileInClipboard(file);

		return file;
	}

	public void CreateZipToStream(Stream fileStream)
	{
		using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create, true);

		AddMainFilesToZip(zipArchive);

		foreach (var filePath in GetFilesForZip())
		{
			if (CrossIO.FileExists(filePath))
			{
				var tempFile = Path.GetTempFileName();

				CrossIO.CopyFile(filePath, tempFile, true);

				try
				{
					zipArchive.CreateEntryFromFile(tempFile, $"Other Files\\{Path.GetFileName(filePath)}");
				}
				catch { }
			}
		}
	}

	private IEnumerable<string> GetFilesForZip()
	{
		yield return GetLastCrashLog();

		if (!Directory.Exists(GameDataPath))
		{
			yield break;
		}

		var logFiles = new DirectoryInfo(CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Logs")).GetFiles("*.log");
		var maxDate = logFiles.Max(x => x.LastWriteTime);

		foreach (var item in logFiles)
		{
			if (maxDate - item.LastWriteTime < TimeSpan.FromDays(1))
			{
				yield return item.FullName;
			}
		}
	}

	private void AddMainFilesToZip(ZipArchive zipArchive)
	{
		if (CrossIO.FileExists(GameLogFile))
		{
			var tempLogFile = Path.GetTempFileName();
			CrossIO.CopyFile(GameLogFile, tempLogFile, true);
			zipArchive.CreateEntryFromFile(tempLogFile, "log.txt");

			var logTrace = SimplifyLog(tempLogFile, out var simpleLogText);

			AddSimpleLog(zipArchive, simpleLogText);

			AddErrors(zipArchive, logTrace);
		}

		if (CrossIO.FileExists(_logger.LogFilePath))
		{
			var tempSkyveLogFile = Path.GetTempFileName();
			CrossIO.CopyFile(_logger.LogFilePath, tempSkyveLogFile, true);
			zipArchive.CreateEntryFromFile(tempSkyveLogFile, "Skyve\\SkyveLog.log");
		}

		if (CrossIO.FileExists(_logger.PreviousLogFilePath))
		{
			var tempPrevSkyveLogFile = Path.GetTempFileName();
			CrossIO.CopyFile(_logger.PreviousLogFilePath, tempPrevSkyveLogFile, true);
			zipArchive.CreateEntryFromFile(tempPrevSkyveLogFile, "Skyve\\SkyveLog_Previous.log");
		}

		AddCompatibilityReport(zipArchive);

		AddProfile(zipArchive);
	}

	private void AddCompatibilityReport(ZipArchive zipArchive)
	{
		//var culture = LocaleHelper.CurrentCulture;
		//LocaleHelper.CurrentCulture = new System.Globalization.CultureInfo("en-US");

		var profileEntry = zipArchive.CreateEntry("Skyve\\CompatibilityReport.json");
		using var writer = new StreamWriter(profileEntry.Open());

		//_compatibilityManager.CacheReport();
		var reports = _contentManager.Packages.ToList(x => x.GetCompatibilityInfo());
		reports.RemoveAll(x => x.GetNotification() < NotificationType.Warning && !(x.IsIncluded(out _, out var partial) || partial));

		writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(reports, Newtonsoft.Json.Formatting.Indented));

		//LocaleHelper.CurrentCulture = culture;
		//_compatibilityManager.CacheReport();
	}

	private void AddProfile(ZipArchive zipArchive)
	{
		if (_playsetManager.CurrentPlayset is null)
		{
			return;
		}

		var profileEntry = zipArchive.CreateEntry("Skyve\\CurrentPlayset.json");
		using var writer = new StreamWriter(profileEntry.Open());
		writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(_playsetManager.CurrentPlayset, Newtonsoft.Json.Formatting.Indented));
	}

	private static void AddErrors(ZipArchive zipArchive, List<ILogTrace> logTrace)
	{
		if (logTrace.Count == 0)
		{
			return;
		}

		var errorsEntry = zipArchive.CreateEntry("log_errors.txt");
		using var writer = new StreamWriter(errorsEntry.Open());
		var errors = logTrace.Select(e => e.ToString()).ListStrings("\r\n*********************************************\r\n");

		writer.Write(errors);
	}

	private static void AddSimpleLog(ZipArchive zipArchive, string simpleLogText)
	{
		var simpleLogEntry = zipArchive.CreateEntry("log_simple.txt");
		using var writer = new StreamWriter(simpleLogEntry.Open());
		writer.Write(simpleLogText);
	}

	private string GetLastCrashLog()
	{
		if (CrossIO.CurrentPlatform is not Platform.Windows)
		{
			return string.Empty;
		}

		try
		{
			var mainGameDir = new DirectoryInfo(GameDataPath).Parent;
			var directories = mainGameDir.GetDirectories($"*-*-*");
			var latest = directories
				.Where(s => DateTime.Now - s.LastWriteTime < TimeSpan.FromDays(1))
				.OrderByDescending(s => s.CreationTime)
				.FirstOrDefault();

			if (latest != null)
			{
				return CrossIO.Combine(latest.FullName, "error.log");
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to load the previous crash dump log");
		}

		return string.Empty;
	}

	public List<ILogTrace> SimplifyLog(string log, out string simpleLog)
	{
		var lines = File.ReadAllLines(log).ToList();

		// decruft the log file
		for (var i = lines.Count - 1; i > 0; i--)
		{
			var current = lines[i];
			if (current.IndexOf("DebugBindings.gen.cpp Line: 51") != -1 ||
				current.StartsWith("Fallback handler") ||
				current.Contains("[PlatformService, Native - public]") ||
				current.Contains("m_SteamUGCRequestMap error") ||
				current.IndexOf("(this message is harmless)") != -1 ||
				current.IndexOf("PopsApi:") != -1 ||
				current.IndexOf("GfxDevice") != -1 ||
				current.StartsWith("Assembly ") ||
				current.StartsWith("No source files found:") ||
				current.StartsWith("d3d11: failed") ||
				current.StartsWith("(Filename:  Line: ") ||
				current.Contains("SteamHelper+DLC_BitMask") ||
				current.EndsWith(" [Packer - public]") ||
				current.EndsWith(" [Mods - public]"))
			{
				lines.RemoveAt(i);

				if (i < lines.Count && string.IsNullOrWhiteSpace(lines[i]))
				{
					lines.RemoveAt(i);
				}
			}
		}

		// clear excess blank lines

		var blank = false;

		for (var i = lines.Count - 1; i > 0; i--)
		{
			if (blank)
			{
				if (string.IsNullOrWhiteSpace(lines[i]))
				{
					lines.RemoveAt(i);
				}
				else
				{
					blank = false;
				}
			}
			else
			{
				blank = string.IsNullOrWhiteSpace(lines[i]);
			}
		}

		simpleLog = string.Join("\r\n", lines);

		// now split out errors

		LogTrace? currentTrace = null;
		var traces = new List<ILogTrace>();

		for (var i = 0; i < lines.Count; i++)
		{
			var current = lines[i];

			if (!current.StartsWith("Crash!!!") && !current.TrimStart().StartsWith("at ") && !(current.TrimStart().StartsWith("--") && currentTrace is not null))
			{
				if (currentTrace is not null)
				{
					if (!currentTrace.Title.Contains("System.Environment.get_StackTrace()"))
					{
						traces.Add(currentTrace);
					}

					currentTrace = null;
				}

				if (current.Contains("[Warning]") || current.Contains("[Error]"))
				{
					traces.Add(new LogTrace(lines, i + 1, false));
				}

				continue;
			}

			currentTrace ??= new(lines, i, current.StartsWith("Crash!!!"));

			currentTrace.AddTrace(current);
		}

		if (currentTrace is not null)
		{
			traces.Add(currentTrace);
		}

		return traces;
	}
}
