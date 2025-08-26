using Extensions;

using Microsoft.Win32;

using Skyve.Domain;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using static System.Environment;

namespace Skyve.Systems.CS2.Services;
internal class LocationService : ILocationService
{
	public const string APP_DATA_PATH = "%APPDATA%";
	public const string CITIES_PATH = "%CITIES%";

	private readonly ILogger _logger;
	private readonly ISettings _settings;
	public string DataPath => CrossIO.Combine(_settings.FolderSettings.GamePath, "Cities2_Data");
	public string ManagedDLL => CrossIO.Combine(DataPath, "Managed");
	public string SkyveSettingsPath => CrossIO.Combine(_settings.FolderSettings.AppDataPath, "ModsSettings", "Skyve");
	public string SkyveDataPath => CrossIO.Combine(_settings.FolderSettings.AppDataPath, "ModsData", "Skyve");

	public string SteamPathWithExe => CrossIO.Combine(_settings.FolderSettings.SteamPath, SteamExe);
	public string CitiesPathWithExe => CrossIO.Combine(_settings.FolderSettings.GamePath, CitiesExe);

	private string CitiesExe => CrossIO.CurrentPlatform switch
	{
		Platform.MacOSX => "Cities2_Loader.sh",
		Platform.Linux => "Cities2.x64",
		Platform.Windows or _ => "Cities2.exe",
	};

	private string SteamExe => CrossIO.CurrentPlatform switch
	{
		Platform.MacOSX => "steam_osx",
		Platform.Linux => string.Empty,
		Platform.Windows or _ => "Steam.exe",
	};

	public LocationService(ILogger logger, ISettings settings, INotificationsService notificationsService)
	{
		_logger = logger;
		_settings = settings;

		if (!Directory.Exists(_settings.FolderSettings.SteamPath))
		{
			_settings.FolderSettings.SteamPath = FindSteamPath(_settings.FolderSettings);
		}

		if (!TryGenerateFolderSettings())
		{
			_settings.FolderSettings.Reload();
		}

		_settings.FolderSettings.GamePath = _settings.FolderSettings.GamePath?.FormatPath() ?? string.Empty;
		_settings.FolderSettings.AppDataPath = _settings.FolderSettings.AppDataPath?.FormatPath() ?? string.Empty;
		_settings.FolderSettings.SteamPath = _settings.FolderSettings.SteamPath?.FormatPath() ?? string.Empty;

		SetCorrectPathSeparator();

		_logger.Info("Folder Settings:\r\n" +
			$"Platform: {CrossIO.CurrentPlatform}\r\n" +
			$"UserIdType: {_settings.FolderSettings.UserIdType}\r\n" +
			$"UserIdentifier: {_settings.FolderSettings.UserIdentifier}\r\n" +
			$"GamingPlatform: {_settings.FolderSettings.GamingPlatform}\r\n" +
			$"GamePath: {_settings.FolderSettings.GamePath}\r\n" +
			$"AppDataPath: {_settings.FolderSettings.AppDataPath}\r\n" +
			$"SteamPath: {_settings.FolderSettings.SteamPath}");

		if (!CheckFolderSettingsValidity(out var onlyAppDataError))
		{
			if (onlyAppDataError)
			{
				notificationsService.SendNotification(new InvalidFolderSettingsNotification());
			}
			else
			{
#if STEAM
				notificationsService.SendNotification(new SkyveNotSetupNotification());
#else
				notificationsService.SendNotification(new SkyveNotSetupNotification());
#endif
			}
		}
	}

	private bool TryGenerateFolderSettings()
	{
#if STEAM
		try
		{
			if (!SteamUtil.IsSteamRunning())
			{
				throw new Exception("Steam is not running.");
			}

			if (!SteamUtil.InitSteamAPI())
			{
				throw new Exception("Failed to initialize Steam API connection.");
			}

			if (!SteamUtil.TryGetSteamUserId(out var userId))
			{
				throw new Exception("Failed to get the game's install folder. Is it actually installed?");
			}

			if (!SteamUtil.TryGetAppInstallDir(949230, out var gamePath))
			{
				throw new Exception("Failed to get the game's install folder. Is it actually installed?");
			}

			_settings.FolderSettings.SteamPath = FindSteamPath(_settings.FolderSettings);
			_settings.FolderSettings.UserIdType = "steam";
			_settings.FolderSettings.UserIdentifier = userId.ToString();
			_settings.FolderSettings.GamingPlatform = Skyve.Domain.Enums.GamingPlatform.Steam;
			_settings.FolderSettings.AppDataPath = Path.Combine(GetFolderPath(SpecialFolder.UserProfile), "AppData", "LocalLow", "Colossal Order", "Cities Skylines II");
			_settings.FolderSettings.GamePath = gamePath;

			_settings.FolderSettings.Save();

			return true;
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to generate folder settings");

			return false;
		}
		finally
		{
			SteamUtil.ShutdownSteamAPI();
		}
#else
		if (!_settings.SessionSettings.FirstTimeSetupCompleted)
		{
			RunFirstTimeSetup();
		}
		else
		{
			SetCorrectPathSeparator();

			_logger.Info("Folder Settings:\r\n" +
				$"Platform: {CrossIO.CurrentPlatform}\r\n" +
				$"UserIdType: {_settings.FolderSettings.UserIdType}\r\n" +
				$"UserIdentifier: {_settings.FolderSettings.UserIdentifier}\r\n" +
				$"GamingPlatform: {_settings.FolderSettings.GamingPlatform}\r\n" +
				$"GamePath: {_settings.FolderSettings.GamePath}\r\n" +
				$"AppDataPath: {_settings.FolderSettings.AppDataPath}\r\n" +
				$"SteamPath: {_settings.FolderSettings.SteamPath}");
		}

		return true;
#endif
	}

	private bool CheckFolderSettingsValidity(out bool onlyAppDataError)
	{
		onlyAppDataError =
			Directory.Exists(_settings.FolderSettings.SteamPath) &&
			!string.IsNullOrEmpty(_settings.FolderSettings.UserIdType) &&
			!string.IsNullOrEmpty(_settings.FolderSettings.UserIdentifier) &&
			Directory.Exists(_settings.FolderSettings.GamePath);

		return onlyAppDataError && Directory.Exists(_settings.FolderSettings.AppDataPath);
	}

	private void SetCorrectPathSeparator()
	{
		var field = typeof(Path).GetField(nameof(Path.DirectorySeparatorChar), BindingFlags.Static | BindingFlags.Public);
		field.SetValue(null, CrossIO.PathSeparator[0]);
	}

	public void CreateShortcut()
	{
		try
		{
			var shortcutPath = CrossIO.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Skyve CS-II.lnk");
			var exePath = Application.ExecutablePath;

			_logger.Info($"Creating shortcut for '{exePath}' at '{shortcutPath}'");

			ExtensionClass.CreateShortcut(shortcutPath, exePath);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: "Failed to create shortcut");
		}
	}

	public string ToRelativePath(string? localPath)
	{
		return localPath is null or ""
			? string.Empty
			: localPath
			.Replace(_settings.FolderSettings.AppDataPath, APP_DATA_PATH)
			.Replace(_settings.FolderSettings.GamePath, CITIES_PATH)
			.FormatPath();
	}

	public string ToLocalPath(string? relativePath)
	{
		return relativePath is null or ""
			? string.Empty
			: relativePath
			.Replace(APP_DATA_PATH, _settings.FolderSettings.AppDataPath)
			.Replace(CITIES_PATH, _settings.FolderSettings.GamePath)
			.FormatPath();
	}

	public void RunFirstTimeSetup()
	{
#if !STEAM
		_logger.Info("First time setup Folder settings:\r\n" +
			$"Platform: {_settings.FolderSettings.Platform}\r\n" +
			$"UserIdType: {_settings.FolderSettings.UserIdType}\r\n" +
			$"UserIdentifier: {_settings.FolderSettings.UserIdentifier}\r\n" +
			$"GamingPlatform: {_settings.FolderSettings.GamingPlatform}\r\n" +
			$"GamePath: {_settings.FolderSettings.GamePath}\r\n" +
			$"AppDataPath: {_settings.FolderSettings.AppDataPath}\r\n" +
			$"SteamPath: {_settings.FolderSettings.SteamPath}");

		if (string.IsNullOrEmpty(_settings.FolderSettings.UserIdentifier) && Directory.Exists(CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".pdxsdk")))
		{
			var folders = Directory.GetDirectories(CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".pdxsdk")).AllWhere(x => ulong.TryParse(x, out var val) && val > 1000000000000);

			if (folders.Count == 1)
			{
				_settings.FolderSettings.UserIdentifier = Path.GetFileName(folders[0]);
				_settings.FolderSettings.UserIdType = "steam";
			}
		}

		try
		{
			if (_settings.FolderSettings.Platform is Platform.MacOSX)
			{
				_logger.Info("Matching macOS Paths");

				_settings.FolderSettings.GamePath = Path.GetDirectoryName(Path.GetDirectoryName(_settings.FolderSettings.GamePath)).Replace('\\', '/');
				_settings.FolderSettings.AppDataPath = _settings.FolderSettings.AppDataPath.Replace('\\', '/');
			}
		}
		catch { }
		finally
		{
			try
			{
				_settings.FolderSettings.SteamPath = FindSteamPath(_settings.FolderSettings);
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, memberName: "Failed to find steam's installation folder");
			}

			if (_settings.FolderSettings.Platform is not Platform.Windows)
			{
				_settings.FolderSettings.GamePath = _settings.FolderSettings.GamePath.Replace('\\', '/').TrimEnd('/', '\\');
				_settings.FolderSettings.AppDataPath = _settings.FolderSettings.AppDataPath.Replace('\\', '/').TrimEnd('/', '\\');
				_settings.FolderSettings.SteamPath = _settings.FolderSettings.SteamPath.Replace('\\', '/').TrimEnd('/', '\\');
			}

			_settings.FolderSettings.Save();

			SetCorrectPathSeparator();
		}
#endif
	}

	private string FindSteamPath(IFolderSettings settings)
	{
		_logger.Info("Finding steam's path");

		if (settings.Platform is Platform.Windows)
		{
			const string steamPathSubKey_ = @"Software\Valve\Steam";
			const string steamPathKey_ = "SteamPath";

			using var key = Registry.CurrentUser.OpenSubKey(steamPathSubKey_);
			var path = key?.GetValue(steamPathKey_) as string;

			if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
			{
				return path![0].ToString().ToUpper() + path!.FormatPath().Substring(1);
			}
		}

		if (settings.Platform is Platform.MacOSX)
		{
			var basePath = "/Applications/Steam.app";

			if (Directory.Exists(basePath))
			{
				var file = Directory.GetFiles(basePath, "steam_osx", SearchOption.AllDirectories);

				if (file.Length > 0)
				{
					return Path.GetDirectoryName(file[0]).FormatPath();
				}
			}
		}

		return settings.SteamPath?.FormatPath() ?? string.Empty;
	}
}
