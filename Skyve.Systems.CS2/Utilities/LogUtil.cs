using Extensions;

using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;

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

	public string GameLogFolder => CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Logs");

	public string CreateZipFile(string? folder = null)
	{
		var file = CrossIO.Combine(folder ?? Path.GetTempPath(), $"LogReport_{DateTime.Now:yy-MM-dd_HH-mm}.zip");

		using (var fileStream = File.Create(file))
		{
			CreateZipToStream(fileStream);
		}

		return file;
	}

	public void CreateZipToStream(Stream fileStream)
	{
		using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create, true);

		AddMainFilesToZip(zipArchive, out var mainLogDate);

		foreach (var filePath in GetFilesForZip(mainLogDate))
		{
			if (CrossIO.FileExists(filePath))
			{
				var tempFile = CrossIO.GetTempFileName();

				CrossIO.CopyFile(filePath, tempFile, true);

				try
				{
					zipArchive.CreateEntryFromFile(tempFile, $"Other Files\\{Path.GetFileName(filePath)}");
				}
				catch { }
			}
		}
	}

	private IEnumerable<string> GetFilesForZip(DateTime mainLogDate)
	{
		yield return GetLastCrashLog(mainLogDate);

		if (!Directory.Exists(CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Logs")))
		{
			yield break;
		}

		var logFiles = new DirectoryInfo(CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Logs")).GetFiles("*.log");

		foreach (var item in logFiles)
		{
			if (Math.Abs(mainLogDate.Ticks - item.LastWriteTime.Ticks) < TimeSpan.FromHours(1).Ticks)
			{
				yield return item.FullName;
			}
		}
	}

	private void AddMainFilesToZip(ZipArchive zipArchive, out DateTime mainLogDate)
	{
		if (CrossIO.FileExists(GameLogFile))
		{
			var tempLogFile = CrossIO.GetTempFileName();
			CrossIO.CopyFile(GameLogFile, tempLogFile, true);
			zipArchive.CreateEntryFromFile(tempLogFile, "log.txt");

			var logTrace = ExtractTrace(GameLogFile, tempLogFile);

			AddErrors(zipArchive, logTrace);

			mainLogDate = File.GetLastWriteTime(GameLogFile);
			CrossIO.DeleteFile(tempLogFile, true);
		}
		else
		{
			mainLogDate = DateTime.Now;
		}

		if (CrossIO.FileExists(_logger.LogFilePath))
		{
			var tempSkyveLogFile = CrossIO.GetTempFileName();
			CrossIO.CopyFile(_logger.LogFilePath, tempSkyveLogFile, true);
			zipArchive.CreateEntryFromFile(tempSkyveLogFile, "Skyve\\SkyveLog.log");
			CrossIO.DeleteFile(tempSkyveLogFile, true);
		}

		if (CrossIO.FileExists(_logger.PreviousLogFilePath))
		{
			var tempPrevSkyveLogFile = CrossIO.GetTempFileName();
			CrossIO.CopyFile(_logger.PreviousLogFilePath, tempPrevSkyveLogFile, true);
			zipArchive.CreateEntryFromFile(tempPrevSkyveLogFile, "Skyve\\SkyveLog_Previous.log");
			CrossIO.DeleteFile(tempPrevSkyveLogFile, true);
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
		reports.RemoveAll(x => x.GetNotification() < Skyve.Compatibility.Domain.Enums.NotificationType.Warning && !(x.IsIncluded(out var partial) || partial));

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

	private string GetLastCrashLog(DateTime mainLogDate)
	{
		if (CrossIO.CurrentPlatform is not Platform.Windows)
		{
			return string.Empty;
		}

		try
		{
			var mainGameDir = new DirectoryInfo(CrossIO.Combine(Path.GetTempPath(), "Colossal Order", "Cities Skylines II", "Crashes"));

			if (mainGameDir.Exists)
			{
				var latest = mainGameDir.GetFiles("crash.dmp", SearchOption.AllDirectories)
					.OrderByDescending(s => s.CreationTime)
					.FirstOrDefault();

				if (latest is not null && Math.Abs(mainLogDate.Ticks - latest.LastWriteTime.Ticks) < TimeSpan.FromHours(1).Ticks)
				{
					return latest.FullName;
				}
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to load the previous crash dump log");
		}

		return string.Empty;
	}

	public List<ILogTrace> ExtractTrace(string originalFile, string log)
	{
		var lines = File.ReadAllLines(log);
		var traces = new List<ILogTrace>();
		LogTrace? currentTrace = null;

		if (!originalFile.EndsWith("Player.log"))
		{
			for (var i = 0; i < lines.Length; i++)
			{
				if (ParseLine(lines[i], out var date, out var type, out var title))
				{
					currentTrace = new LogTrace(type!, title!, date, originalFile);

					traces.Add(currentTrace);
				}
				else
				{
					currentTrace?.AddTrace(lines[i]);
				}
			}
		}
		else
		{
			var stamp = File.GetLastWriteTime(originalFile);

			for (var i = 0; i < lines.Length; i++)
			{
				var current = lines[i];

				if (current.TrimStart().StartsWith("at "))
				{
					if (currentTrace is null)
					{
						traces.Add(currentTrace = new LogTrace(lines[i - 1].Contains("Crash") ? "CRASH" : "EXCEPTION", lines[i - 1], stamp, originalFile));
					}

					currentTrace.AddTrace(current);
				}
				else
				{
					currentTrace = null;
				}
			}
		}

		return traces;
	}

	private bool ParseLine(string line, out DateTime date, out string? info, out string? title)
	{
		var pattern = @"^\[(.+?)\] \[(.+?)\] +(.+)";
		var match = Regex.Match(line, pattern);

		if (match.Success)
		{
			title = match.Groups[3].Value;
			info = match.Groups[2].Value;
			var dateString = match.Groups[1].Value;

			if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss,fff", null, System.Globalization.DateTimeStyles.None, out date))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		title = null;
		info = null;
		date = default;
		return false;
	}

	public List<ILogTrace> GetCurrentLogsTrace()
	{
		DateTime mainLogDate;
		var traces = new List<ILogTrace>();

		if (File.Exists(GameLogFile))
		{
			var tempName = CrossIO.GetTempFileName();

			File.Copy(GameLogFile, tempName, true);

			traces.AddRange(ExtractTrace(GameLogFile, tempName));

			mainLogDate = File.GetLastWriteTime(GameLogFile);

			CrossIO.DeleteFile(tempName, true);
		}
		else
		{
			mainLogDate = DateTime.Now;
		}

		foreach (var filePath in GetFilesForZip(mainLogDate))
		{
			if (CrossIO.FileExists(filePath))
			{
				var tempName = CrossIO.GetTempFileName();

				File.Copy(filePath, tempName, true);

				traces.AddRange(ExtractTrace(filePath, tempName));

				CrossIO.DeleteFile(tempName, true);
			}
		}

		return traces;
	}
}
