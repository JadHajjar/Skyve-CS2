﻿using Extensions;

using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Configuration;
using PDX.SDK.Contracts.Credential;
using PDX.SDK.Contracts.Enums;
using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;

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

	private IContext? Context { get; set; }
	public bool IsAvailable => Context is not null;
	public bool IsReady => Context is not null && !Context.Mods.SyncOngoing();

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

		if (Enum.TryParse<Language>(LocaleHelper.CurrentCulture.IetfLanguageTag.Substring(0, 2).ToLower(), out var lang))
		{
			config.Language = lang;
		}

		try
		{
			Context = await PDX.SDK.Context.Create(
				platform: platform,
				@namespace: "cities_skylines_2",
				config: config);

			new WorkshopEventsManager(this).RegisterModsCallbacks(Context);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to create PDX Context");
		}
	}

	public async Task Login()
	{
		if (Context is null)
		{
			return;
		}

		var startupResult = await Context.Account.Startup();

		if (!startupResult.IsLoggedIn)
		{
			if (!ConnectionHandler.CheckConnection())
			{
				loginWaitingConnection = true;

				_notificationsService.SendNotification(new ParadoxLoginWaitingConnectionNotification());

				ConnectionHandler.WhenConnected(async () => await Login());

				return;
			}

			if (!_settings.UserSettings.ParadoxLogin.IsValid(KEYS.SALT))
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
		return identity.Id <= 0 ? null : (IWorkshopInfo)_modProcessor.Get((int)identity.Id).Result;
	}

	public async Task<IWorkshopInfo?> GetInfoAsync(IPackageIdentity identity)
	{
		return identity.Id <= 0 ? null : (IWorkshopInfo)await _modProcessor.Get((int)identity.Id, true);
	}

	internal async Task<PdxModDetails?> GetInfoAsync(int id)
	{
		if (Context is null || id <= 0)
		{
			return null;
		}

		var platform = CrossIO.CurrentPlatform switch { Platform.MacOSX => ModPlatform.Osx, Platform.Linux => ModPlatform.Linux, _ => ModPlatform.Windows };
		var result = await Context.Mods.GetDetails(id, null, platform);

		ProcessResult(result);

		if (result?.Mod is not null)
		{
			var ratingResult = await Context.Mods.GetUserRating(id);

			return new PdxModDetails(result.Mod, ratingResult.Rating != 0);
		}

		return null;
	}

	internal async Task<ModDetails?> GetInfoRawAsync(int id)
	{
		if (Context is null || id <= 0)
		{
			return null;
		}

		var platform = CrossIO.CurrentPlatform switch { Platform.MacOSX => ModPlatform.Osx, Platform.Linux => ModPlatform.Linux, _ => ModPlatform.Windows };
		var result = await Context.Mods.GetDetails(id, null, platform);

		ProcessResult(result);

		return result?.Mod is not null ? result.Mod : null;
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

		ProcessResult(result);

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
			pageSize = 100
		});

		ProcessResult(result);

		if (result.Success)
		{
			return result.Mods?.ToList(x => new PdxPackage(x)) ?? [];
		}

		_logger.Error(result.Error.Raw);

		return [];
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

		var gameData = ProcessResult(await Context.Mods.GetGameData());

		return cachedTags = gameData.Tags.ToList(x => (ITag)new TagItem(Domain.CS2.Enums.TagSource.Workshop, x.Value.Id, x.Value.DisplayName));
	}
	
	internal async Task<List<Mod>> GetLocalPackages()
	{
		if (Context is null)
		{
			return [];
		}

		var mods = ProcessResult(await Context.Mods.List());

		return !mods.Success || mods.Mods is null ? (List<Mod>)([]) : mods.Mods;
	}

	public async Task<List<ICustomPlayset>> GetPlaysets(bool localOnly)
	{
		if (Context is null)
		{
			return [];
		}

		var playsets = ProcessResult(await Context.Mods.ListAllPlaysets(!localOnly));

		return !playsets.Success ? (List<ICustomPlayset>)([]) : playsets.AllPlaysets.ToList(playset => (ICustomPlayset)new Domain.CS2.Content.Playset(playset));
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

		hasVoted ??= ProcessResult(await Context.Mods.GetUserRating((int)packageIdentity.Id)).Rating != 0;

		var result = await Context.Mods.Rate((int)packageIdentity.Id, hasVoted.Value ? 0 : 5);

		ProcessResult(result);

		if (!result.Success && packageIdentity.GetWorkshopInfo() is IWorkshopInfo workshopInfo_)
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
		return Context is null ? 0 : ProcessResult(await Context.Mods.GetActivePlayset()).PlaysetId;
	}

	public async Task<IEnumerable<IPackage>> GetModsInPlayset(int playsetId, bool includeOnline = false)
	{
		if (Context is null)
		{
			return [];
		}

		var result = ProcessResult(await Context.Mods.ListModsInPlayset(playsetId));

		return result.Mods?.ToList(x => new PdxPlaysetPackage(x)) ?? [];
	}

	public async Task WaitUntilReady()
	{
		while (!IsReady)
		{
			await Task.Delay(50);
		}
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

	private T ProcessResult<T>(T result) where T : Result
	{
		if (result.Error is not null)
		{
			_logger.Error(result.Error.Raw);
		}

		return result;
	}

	internal async Task<bool> SubscribeBulk(IEnumerable<KeyValuePair<int, string?>> mods, int playset, bool enable)
	{
		if (Context is null)
		{
			return false;
		}

		var result = await Context.Mods.SubscribeBulk(
			mods,
			playset,
			enable);

		return ProcessResult(result).Success;
	}

	internal async Task<bool> UnsubscribeBulk(IEnumerable<int> mods, int playset)
	{
		if (Context is null)
		{
			return false;
		}

		var results = new List<Result>();

		foreach (var id in mods)
		{
			var result = await Context.Mods.Unsubscribe(id, playset);

			results.Add(ProcessResult(result));
		}

		return results.All(x => x.Success);
	}

	public async Task<bool> DeletePlayset(int playset)
	{
		return Context is not null && ProcessResult(await Context.Mods.DeletePlayset(playset)).Success;
	}

	public async Task<bool> ActivatePlayset(int playset)
	{
		return Context is not null && ProcessResult(await Context.Mods.ActivatePlayset(playset)).Success;
	}

	public async Task<bool> RenamePlayset(int playset, string name)
	{
		return Context is not null && ProcessResult(await Context.Mods.RenamePlayset(playset, name)).Success;
	}

	internal async Task<ICustomPlayset?> CreatePlayset(string playsetName)
	{
		if (Context is null)
		{
			return null;
		}

		var result = ProcessResult(await Context.Mods.CreatePlayset(playsetName));

		return result.Success ? new Domain.CS2.Content.Playset(result) { LastEditDate = DateTime.Now } : (ICustomPlayset?)null;
	}

	internal async Task SetLoadOrder(List<ModLoadOrder> orderedMods, int playset)
	{
		if (Context is null)
		{
			return;
		}

		ProcessResult(await Context.Mods.SetLoadOrder(orderedMods, playset));
	}

	internal async Task<bool> SetEnableBulk(List<int> modKeys, int playset, bool enable)
	{
		if (Context is null)
		{
			return false;
		}

		var result = enable
				? await Context.Mods.EnableBulk(modKeys, playset)
				: await Context.Mods.DisableBulk(modKeys, playset);

		ProcessResult(result);

		if (result.Success)
		{
			await Context.Mods.Sync();
		}

		return result.Success;
	}

	internal async Task<ICustomPlayset?> ClonePlayset(int id)
	{
		if (Context is null)
		{
			return null;
		}

		var result = ProcessResult(await Context.Mods.ClonePlayset(id));

		return result.Success ? new Domain.CS2.Content.Playset(result) { LastEditDate = DateTime.Now } : (ICustomPlayset?)null;
	}
}
