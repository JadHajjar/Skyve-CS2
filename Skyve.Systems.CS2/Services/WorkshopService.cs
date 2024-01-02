using Extensions;

using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Configuration;
using PDX.SDK.Contracts.Credential;
using PDX.SDK.Contracts.Enums;
using PDX.SDK.Contracts.Events.Download;
using PDX.SDK.Contracts.Events.Mods;
using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;
using PDX.SDK.Internal.Events.Internal.Mods;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Managers;
using Skyve.Systems.CS2.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
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
	private readonly PdxModProcessor _modProcessor;

	private bool loginWaitingConnection;
	private List<ITag>? cachedTags;

	internal IContext? Context { get; private set; }

	public WorkshopService(ILogger logger, ISettings settings, ICitiesManager citiesManager, INotificationsService notificationsService)
	{
		_logger = logger;
		_settings = settings;
		_citiesManager = citiesManager;
		_notificationsService = notificationsService;
		_modProcessor = new PdxModProcessor(this);
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

		new WorkshopEventsManager(this).RegisterModsCallbacks(Context);
	}

	public async Task Login()
	{
		var startupResult = await Context!.Account.Startup();

		if (!startupResult.IsLoggedIn)
		{

			if (!ConnectionHandler.CheckConnection())
			{
				loginWaitingConnection = true;

				_notificationsService.SendNotification(new ParadoxLoginWaitingConnectionNotification());

				ConnectionHandler.WhenConnected(async () => await Login());

				return;
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
		}

		await Context.Mods.Sync(SyncDirection.Downstream);
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

	public void ClearCache()
	{
		_modProcessor.Clear();
	}

	public IWorkshopInfo? GetInfo(IPackageIdentity identity)
	{
		if (identity.Id <= 0)
			return null;

		return _modProcessor.Get((int)identity.Id).Result;
	}

	public async Task<IWorkshopInfo?> GetInfoAsync(IPackageIdentity identity)
	{
		if (identity.Id <= 0)
			return null;

		return await _modProcessor.Get((int)identity.Id, true);
	}

	internal async Task<PdxModDetails?> GetInfoAsync(int id)
	{
		if (Context is null || id <= 0)
		{
			return null;
		}

		var platform = CrossIO.CurrentPlatform switch { Platform.MacOSX => ModPlatform.Osx, Platform.Linux => ModPlatform.Linux, _ => ModPlatform.Windows };
		var result = await Context.Mods.GetDetails(id, null, platform);

		if (result?.Mod is not null)
		{
			var ratingResult = await Context.Mods.GetUserRating(id);

			return new PdxModDetails(result.Mod, ratingResult.Rating != 0);
		}

		return null;
	}

	public IPackage GetPackage(IPackageIdentity identity)
	{
		return GetInfo(identity) as IPackage ?? new PdxModIdentityPackage(identity);
	}

	public async Task<IPackage> GetPackageAsync(IPackageIdentity identity)
	{
		return await GetInfoAsync(identity) as IPackage ?? new PdxModIdentityPackage(identity);
	}

	public IUser? GetUser(object authorId)
	{
		return new PdxUser(authorId.ToString());
	}

	public async Task<IEnumerable<IWorkshopInfo>> GetWorkshopItemsByUserAsync(object userId)
	{
		if (Context is null)
		{
			return [];
		}

		var result = await Context.Mods.Search(new SearchData
		{
			author = userId.ToString(),
			pageSize = 9999
		});

		if (result.Success)
		{
			return result.Mods.ToList(x => new PdxPackage(x));
		}

		_logger.Error(result.Error.Raw);

		return [];
	}

	public async Task<IEnumerable<IWorkshopInfo>> QueryFilesAsync(WorkshopQuerySorting sorting, string? query = null, string[]? requiredTags = null, bool all = false)
	{
		if (Context is null)
		{
			return [];
		}

		var result = await Context.Mods.Search(new SearchData
		{
			sortBy = GetPdxSorting(sorting),
			searchQuery = query,
			tags = requiredTags?.ToList(),
			orderBy = GetPdxOrder(sorting),
			pageSize = 9999
		});

		if (result.Success)
		{
			return result.Mods.ToList(x => new PdxPackage(x));
		}

		_logger.Error(result.Error.Raw);

		return [];
	}

	private SearchOrder GetPdxOrder(WorkshopQuerySorting sorting)
	{
		return sorting switch
		{
			WorkshopQuerySorting.Name => SearchOrder.Ascending,
			_ => SearchOrder.Descending,
		};
	}

	private SortMethod GetPdxSorting(WorkshopQuerySorting sorting)
	{
		return sorting switch
		{
			WorkshopQuerySorting.Name => SortMethod.DisplayName,
			WorkshopQuerySorting.DateCreated => SortMethod.Created,
			WorkshopQuerySorting.DateUpdated => SortMethod.Updated,
			WorkshopQuerySorting.Rating => SortMethod.Rating,
			WorkshopQuerySorting.Popularity => SortMethod.Popularity,
			WorkshopQuerySorting.ActivationOrder => SortMethod.ActivationOrder,
			WorkshopQuerySorting.Best => SortMethod.Best,
			_ => SortMethod.Best,
		};
	}

	public async Task<IEnumerable<ITag>> GetAvailableTags()
	{
		if (cachedTags is not null)
		{
			return cachedTags;
		}

		if (Context is null)
		{
			return [];
		}

		var gameData = await Context.Mods.GetGameData();

		return cachedTags = gameData.Tags.ToList(x => (ITag)new TagItem(Domain.CS2.Enums.TagSource.Workshop, x.Value.Id, x.Value.DisplayName));
	}

	public async Task<List<IPackage>> GetLocalPackages()
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

		return mods.Mods.Where(x => x.LocalData is not null).ToList(mod => (IPackage)(mod.LocalData is null ? new PdxPackage(mod) : new LocalPdxPackage(mod)));
	}

	public async Task<List<ICustomPlayset>> GetPlaysets(bool localOnly)
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

		return playsets.AllPlaysets.ToList(playset => (ICustomPlayset)new Domain.CS2.Content.Playset(playset));
	}

	public async Task<bool> ToggleVote(IPackageIdentity packageIdentity)
	{
		if (Context is null)
		{
			return false;
		}

		bool? hasVoted = null;

		if (packageIdentity.GetWorkshopInfo() is IWorkshopInfo workshopInfo)
		{
			hasVoted = workshopInfo.HasVoted;

			workshopInfo.HasVoted = !workshopInfo.HasVoted;
			workshopInfo.VoteCount += workshopInfo.HasVoted ? 1 : -1;
		}

		hasVoted ??= (await Context.Mods.GetUserRating((int)packageIdentity.Id)).Rating != 0;

		var result = await Context.Mods.Rate((int)packageIdentity.Id, hasVoted.Value ? 0 : 5);

		if (!result.Success&& packageIdentity.GetWorkshopInfo() is IWorkshopInfo workshopInfo_)
		{
			workshopInfo_.HasVoted = !workshopInfo_.HasVoted;
			workshopInfo_.VoteCount -= workshopInfo_.HasVoted ? 1 : -1;
		}
		else
		{
			await _modProcessor.Get((int)packageIdentity.Id, true);
		}

		_modProcessor.CacheItems();

		return result.Success;
	}

	public async Task<int> GetActivePlaysetId()
	{
		if (Context is null)
		{
			return 0;
		}

		return (await Context.Mods.GetActivePlayset()).PlaysetId;
	}
}
