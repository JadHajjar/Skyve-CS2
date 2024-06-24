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
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
internal class LogUtil : ILogUtil
{
	private readonly ICompatibilityManager _compatibilityManager;
	private readonly ILocationService _locationManager;
	private readonly IPackageManager _packageManager;
	private readonly IPlaysetManager _playsetManager;
	private readonly IPackageUtil _packageUtil;
	private readonly ILogger _logger;
	private readonly ISettings _settings;

	public LogUtil(ILocationService locationManager, IPackageManager packageManager, IPlaysetManager profileManager, ILogger logger, ICompatibilityManager compatibilityManager, ISettings settings, IPackageUtil packageUtil)
	{
		_compatibilityManager = compatibilityManager;
		_locationManager = locationManager;
		_packageManager = packageManager;
		_packageUtil = packageUtil;
		_playsetManager = profileManager;
		_logger = logger;
		_settings = settings;

		try
		{
			foreach (var item in Directory.GetFiles(CrossIO.Combine(_locationManager.SkyveDataPath, ".SupportLogs")))
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
	public string PreviousGameLogFile => CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Player-prev.log");

	public string GameDataPath => CrossIO.CurrentPlatform switch
	{
		Platform.MacOSX => CrossIO.Combine(_settings.FolderSettings.GamePath, "Cities2.app", "Contents"),
		_ => CrossIO.Combine(_settings.FolderSettings.GamePath, "Cities2_Data")
	};

	public string GameLogFolder => CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Logs");

	public async Task<string> CreateZipFile(string? folder = null)
	{
		var file = CrossIO.Combine(folder ?? Path.GetTempPath(), $"LogReport_{DateTime.Now:yy-MM-dd_HH-mm}.zip");

		using (var fileStream = File.Create(file))
		{
			await CreateZipToStream(fileStream);
		}

		return file;
	}

	public async Task CreateZipToStream(Stream fileStream)
	{
		using var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create, true);

		AddMainFilesToZip(zipArchive, out var mainLogDate, out var logTrace);

		foreach (var filePath in GetLogFilesForZip(mainLogDate))
		{
			CreateFileEntry(zipArchive, $"Logs\\{Path.GetFileName(filePath)}", filePath);
		}

		await AddProfile(zipArchive);

		AddErrors(zipArchive, logTrace);
	}

	private IEnumerable<string> GetLogFilesForZip(DateTime mainLogDate)
	{
		yield return GetLastCrashLog(mainLogDate);

		if (!Directory.Exists(CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Logs")))
		{
			yield break;
		}

		var logFiles = new DirectoryInfo(CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Logs")).GetFiles("*.log");

		foreach (var item in logFiles)
		{
			if (Math.Abs(mainLogDate.Ticks - item.LastWriteTime.Ticks) < TimeSpan.FromDays(1).Ticks)
			{
				yield return item.FullName;
			}
		}
	}

	private void AddMainFilesToZip(ZipArchive zipArchive, out DateTime mainLogDate, out List<ILogTrace> logTrace)
	{
		if (CrossIO.FileExists(GameLogFile))
		{
			var tempLogFile = CrossIO.GetTempFileName();
			CrossIO.CopyFile(GameLogFile, tempLogFile, true);
			CreateEntry(zipArchive, "Log.log", File.ReadAllText(tempLogFile));

			logTrace = ExtractTrace(GameLogFile, tempLogFile);
			mainLogDate = File.GetLastWriteTime(GameLogFile);
			CrossIO.DeleteFile(tempLogFile, true);
		}
		else
		{
			logTrace = [];
			mainLogDate = DateTime.Now;
		}

		if (CrossIO.FileExists(PreviousGameLogFile)) 
		{
			var tempLogFile = CrossIO.GetTempFileName();
			CrossIO.CopyFile(PreviousGameLogFile, tempLogFile, true);
			CreateEntry(zipArchive, "Log_Previous.log", File.ReadAllText(tempLogFile));
		}

		if (CrossIO.FileExists(_logger.LogFilePath))
		{
			CreateFileEntry(zipArchive, "Skyve\\SkyveLog.log", _logger.LogFilePath);
		}

		if (CrossIO.FileExists(_logger.PreviousLogFilePath))
		{
			CreateFileEntry(zipArchive, "Skyve\\SkyveLog_Previous.log", _logger.PreviousLogFilePath);
		}

		CreateEntry(zipArchive, "Mods List.txt", _packageManager.Packages.Where(x => _packageUtil.IsIncludedAndEnabled(x)).ListStrings(x => x.IsLocal() ? $"Local: {x.Name} {x.Version}" : $"{x.Id}: {x.Name} {x.Version}", CrossIO.NewLine));

		AddCompatibilityReport(zipArchive);
	}

	private void AddCompatibilityReport(ZipArchive zipArchive)
	{
		//var culture = LocaleHelper.CurrentCulture;
		//LocaleHelper.CurrentCulture = new System.Globalization.CultureInfo("en-US");

		//_compatibilityManager.CacheReport();
		var reports = _packageManager.Packages.Where(x => x.IsEnabled()).ToList(x => x.GetCompatibilityInfo());
		reports.RemoveAll(x => x.GetNotification() <= Skyve.Compatibility.Domain.Enums.NotificationType.Warning && !_packageUtil.IsIncludedAndEnabled(x));

		CreateEntry(zipArchive, "Skyve\\CompatibilityReport.json", reports);

		//LocaleHelper.CurrentCulture = culture;
		//_compatibilityManager.CacheReport();
	}

	private async Task AddProfile(ZipArchive zipArchive)
	{
		if (_playsetManager.CurrentPlayset is null)
		{
			return;
		}

		CreateEntry(zipArchive, "Skyve\\CurrentPlayset.json", await _playsetManager.GetLogPlayset());
	}

	private static void AddErrors(ZipArchive zipArchive, List<ILogTrace> logTrace)
	{
		logTrace = logTrace.AllWhere(x => x.Type is not "INFO" and not "DEBUG");

		if (logTrace.Count == 0)
		{
			return;
		}

		CreateEntry(zipArchive, "Log_Errors.log", logTrace.ListStrings(e => e.ToString() + "\r\n\r\n"));
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

		if (!originalFile.EndsWith("Player.log") && !originalFile.EndsWith("Player-prev.log"))
		{
			for (var i = 0; i < lines.Length; i++)
			{
				var line = lines[i];

				if (!IsValid(line))
				{
					continue;
				}

				if (ParseLine(line, out var date, out var type, out var title))
				{
					currentTrace = new LogTrace(type!, title!, date, originalFile);

					traces.Add(currentTrace);
				}
				else
				{
					currentTrace?.AddTrace(line);
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

	private bool IsValid(string line)
	{
		if (line.Contains("uses compression method Deflated that is not supported"))
		{
			return false;
		}

		return true;
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

		foreach (var filePath in GetLogFilesForZip(mainLogDate))
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

	private static void CreateFileEntry(ZipArchive zipArchive, string entry, string filename)
	{
		if (!CrossIO.FileExists(filename))
		{
			return;
		}

		var tempFile = CrossIO.GetTempFileName();
		CrossIO.CopyFile(filename, tempFile, true);

		CreateEntry(zipArchive, entry, File.ReadAllText(tempFile));

		CrossIO.DeleteFile(tempFile, true);
	}

	private static void CreateEntry<T>(ZipArchive zipArchive, string entry, T obj)
	{
		CreateEntry(zipArchive, entry, Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented));
	}

	private static void CreateEntry(ZipArchive zipArchive, string entry, string content)
	{
		var profileEntry = zipArchive.CreateEntry(entry);
		using var writer = new StreamWriter(profileEntry.Open());

		writer.Write(content.RegexReplace(@"(users[/\\]).+?([/\\])", x => $"{x.Groups[1].Value}%username%{x.Groups[2].Value}"));
	}
}
