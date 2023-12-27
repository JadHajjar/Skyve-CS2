using Extensions;

using PDX.SDK;
using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Configuration;
using PDX.SDK.Contracts.Credential;
using PDX.SDK.Contracts.Enums;

using Skyve.Domain;
using Skyve.Domain.CS2;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.CS2.ParadoxMods;
using Skyve.Domain.CS2.Steam;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PdxPlatform = PDX.SDK.Contracts.Enums.Platform;
using Platform = Extensions.Platform;

namespace Skyve.Systems.CS2.Services;
internal class WorkshopService : IWorkshopService
{
	private readonly ILogger _logger;
    private readonly ISettings _settings;
    private readonly ICitiesManager _citiesManager;
    private readonly INotificationsService _notificationsService;
	private bool loginWaitingConnection;

	internal IContext? Context { get; private set; }

	public WorkshopService(ILogger logger, ISettings settings, ICitiesManager citiesManager, INotificationsService notificationsService)
	{
		_logger = logger;
		_settings = settings;
		_citiesManager = citiesManager;
		_notificationsService = notificationsService;
	}

	public async Task Initialize()
	{
		var pdxSdkPath = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".pdxsdk");
		var platform = CrossIO.CurrentPlatform switch { Platform.MacOSX => PdxPlatform.MacOS, Platform.Linux => PdxPlatform.Linux, _ => PdxPlatform.Windows };
        var ecoSystem = _settings.FolderSettings.GamingPlatform switch { GamingPlatform.Epic => Ecosystem.Epic, GamingPlatform.Microsoft => Ecosystem.Microsoft_Store, _ => Ecosystem.Steam };

		if (!string.IsNullOrWhiteSpace(_settings.FolderSettings.UserIdentifier))
		{
			pdxSdkPath = CrossIO.Combine(pdxSdkPath, _settings.FolderSettings.UserIdentifier);
		}

		var config = new Config
		{
			Language = Language.en,
			GameVersion = _citiesManager.GameVersion,
			Logger = _logger as CS2LoggerSystem,
			DiskIORoot = pdxSdkPath,
			Environment = BackendEnvironment.Live,
			TelemetryDebugEnabled = false,
			Ecosystem = ecoSystem,
			UserIdType = "steam",
#if DEBUG
			LogLevel = LogLevel.L1_Debug,
#else
			LogLevel = LogLevel.L2_Warning,
#endif
		};

		config.Mods.RootPath = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".cache", "Mods");

		if (Enum.TryParse<Language>(LocaleHelper.CurrentCulture.IetfLanguageTag.Substring(0, 2).ToLower(), out var result))
		{
			config.Language = result;
		}

		Context = await PDX.SDK.Context.Create(
			platform: platform,
			@namespace: "cities_skylines_2",
			config: config);
	}

    public async Task Login()
    {
        var startupResult = await Context!.Account.Startup();

        if (startupResult.IsLoggedIn)
        {
            return;
        }

        if (!ConnectionHandler.CheckConnection())
        {
            loginWaitingConnection = true;

            _notificationsService.SendNotification(new ParadoxLoginWaitingConnectionNotification());

            ConnectionHandler.WhenConnected(async () => await Login());
        }

        if (!_settings.UserSettings.ParadoxLogin.IsValid())
        {
            _notificationsService.SendNotification(new ParadoxLoginRequiredNotification(false));

            return;
        }

        var loginResult = await Context.Account.Login(GetCredentials());

        if (!loginResult.Success)
        {
            _notificationsService.SendNotification(new ParadoxLoginRequiredNotification(true));

            return;
        }

        await Context.Mods.Sync();
    }

	private ICredential GetCredentials()
	{
        try
        {
            var email = Encryption.Decrypt(_settings.UserSettings.ParadoxLogin.Email, KEYS.SALT);
            var password = Encryption.Decrypt(_settings.UserSettings.ParadoxLogin.Password, KEYS.SALT);

            return new EmailAndPasswordCredential(email, password);
        }
        catch
		{
            return new EmailAndPasswordCredential(string.Empty, string.Empty);
		}
	}

	public void CleanDownload(List<ILocalPackageData> packages)
    {
        PackageWatcher.Pause();
        foreach (var item in packages)
        {
            try
            {
                CrossIO.DeleteFolder(item.Folder);
            }
            catch (Exception ex)
            {
                ServiceCenter.Get<ILogger>().Exception(ex, $"Failed to delete the folder '{item.Folder}'");
            }
        }

        PackageWatcher.Resume();

        SteamUtil.Download(packages);
    }

    public void ClearCache()
    {
        SteamUtil.ClearCache();
    }

    public IEnumerable<IWorkshopInfo> GetAllPackages()
    {
        yield break;
    }

    public IWorkshopInfo? GetInfo(IPackageIdentity identity)
    {
        return null;
    }

    public async Task<IWorkshopInfo?> GetInfoAsync(IPackageIdentity identity)
	{
		if (Context is null)
		{
			return null;
		}

		var result = await Context.Mods.GetDetails((int)identity.Id);

        if (result?.Mod is not null)
        {
            return new PdxModDetails(result.Mod);
        }

        return null;
    }

    public IPackage GetPackage(IPackageIdentity identity)
    {
        var info = identity is IWorkshopInfo inf ? inf : GetInfo(identity);

        if (info is not null)
        {
            return new WorkshopPackage(info);
        }

        return new GenericWorkshopPackage(identity);
    }

    public async Task<IPackage> GetPackageAsync(IPackageIdentity identity)
    {
        var info = await GetInfoAsync(identity);

        if (info is not null)
        {
            return new WorkshopPackage(info);
        }

        return new GenericWorkshopPackage(identity);
    }

    public IUser? GetUser(object userId)
    {
        return SteamUtil.GetUser(ulong.TryParse(userId?.ToString() ?? string.Empty, out var id) ? id : 0);
    }

    public async Task<IEnumerable<IWorkshopInfo>> GetWorkshopItemsByUserAsync(object userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IWorkshopInfo>> QueryFilesAsync(PackageSorting sorting, string? query = null, string[]? requiredTags = null, string[]? excludedTags = null, (DateTime, DateTime)? dateRange = null, bool all = false)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ILocalPackageData>> GetInstalledPackages()
    {
        if (Context is null)
        {
            return new();
        }

        var mods = await Context.Mods.List();

        if (!mods.Success)
        {
            return new();
        }

        return mods.Mods.ToList(mod => (ILocalPackageData)new LocalPdxPackage(mod));
    }

    public async Task<List<ICustomPlayset>> GetAllPlaysets(bool localOnly)
    {
        if (Context is null)
        {
            return new();
        }

        var playsets = await Context.Mods.ListAllPlaysets(!localOnly);

        if (!playsets.Success)
        {
            return new();
        }

        return playsets.AllPlaysets.ToList(playset => (ICustomPlayset)new Playset(playset));
    }
}
