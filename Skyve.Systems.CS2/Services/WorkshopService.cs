using Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Configuration;
using PDX.SDK.Contracts.Credential;
using PDX.SDK.Contracts.Enums;
using PDX.SDK.Contracts.Enums.Errors;
using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;
using PDX.SDK.Contracts.Service.Mods.Result;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Managers;
using Skyve.Systems.CS2.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using PdxPlatform = PDX.SDK.Contracts.Enums.Platform;
using Platform = Extensions.Platform;

namespace Skyve.Systems.CS2.Services;
public class WorkshopService : IWorkshopService
{
	public event Action? OnLogin;
	public event Action? OnLogout;
	public event Action? OnContextAvailable;

	private readonly ILogger _logger;
	private readonly ISettings _settings;
	private readonly INotifier _notifier;
	private readonly ICitiesManager _citiesManager;
	private readonly INotificationsService _notificationsService;
	private readonly IInterfaceService _interfaceService;
	private readonly IServiceProvider _serviceProvider;
	private readonly UserService _userService;
	private readonly PdxLogUtil _pdxLogUtil;
	private readonly PdxModProcessor _modProcessor;
	private readonly PdxUserProcessor _userProcessor;
	private readonly AsyncProcessor _processor = new();

	private bool loginWaitingConnection;
	private bool syncOccurred;
	private List<ITag>? cachedTags;

	private IContext? Context { get; set; }
	public bool IsLoggedIn { get; private set; }
	public bool IsLoginPending { get; private set; } = true;
	public bool IsAvailable => Context is not null;
	public bool IsReady => Context is not null && IsLoggedIn && !Context.Mods.SyncOngoing();

	public WorkshopService(ILogger logger, ISettings settings, INotifier notifier, IUserService userService, ICitiesManager citiesManager, INotificationsService notificationsService, IInterfaceService interfaceService, IServiceProvider serviceProvider, PdxLogUtil pdxLogUtil, SaveHandler saveHandler)
	{
		_logger = logger;
		_settings = settings;
		_notifier = notifier;
		_citiesManager = citiesManager;
		_notificationsService = notificationsService;
		_interfaceService = interfaceService;
		_serviceProvider = serviceProvider;
		_pdxLogUtil = pdxLogUtil;
		_userService = (UserService)userService;
		_modProcessor = new PdxModProcessor(this, saveHandler, _notifier);
		_userProcessor = new PdxUserProcessor(this, saveHandler, _notifier);

		_processor.TasksCompleted += Processor_TasksCompleted;
	}

	private void Processor_TasksCompleted()
	{
		if (syncOccurred)
		{
			syncOccurred = false;
			_notifier.OnWorkshopSyncEnded();
		}
	}

	public async Task Initialize()
	{
		if (Context is not null)
		{
			return;
		}

		if (!Directory.Exists(_settings.FolderSettings.AppDataPath))
		{
			throw new Exception("FolderSettings AppData folder does not exist");
		}

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
			Logger = _pdxLogUtil,
			DiskIORoot = pdxSdkPath,
			Environment = BackendEnvironment.Live,
			TelemetryDebugEnabled = false,
			Ecosystem = ecoSystem,
			UserIdType = _settings.FolderSettings.UserIdType.IfEmpty("steam"),
#if DEBUG
			LogLevel = LogLevel.L1_Debug,
			DefaultHeaders = new Dictionary<string, string>() { { "User-Agent", $"Skyve/{typeof(WorkshopService).Assembly.GetName().Version}-test" } },
#else
			LogLevel = LogLevel.L2_Warning,
			DefaultHeaders = new Dictionary<string, string>() { { "User-Agent", $"Skyve/{typeof(WorkshopService).Assembly.GetName().Version}" } },
#endif
		};

		config.Mods.RootPath = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".cache", "Mods");

		//if (Enum.TryParse<Language>(LocaleHelper.CurrentCulture.IetfLanguageTag.Substring(0, 2).ToLower(), out var lang))
		//{
		//	config.Language = lang;
		//}

		try
		{
			Context = await PDX.SDK.Context.Create(
				platform: platform,
				@namespace: "cities_skylines_2",
				config: config);

			new WorkshopEventsManager(this, _serviceProvider).RegisterModsCallbacks(Context);

			OnContextAvailable?.Invoke();
		}
		catch (Exception ex)
		{
			_notificationsService.SendNotification(new ParadoxContextFailedNotification(this));

			_logger.Exception(ex, "Failed to create PDX Context");
		}
	}

	public async Task<bool> Login()
	{
		try
		{
			if (Context is null || IsLoggedIn)
			{
				return false;
			}

			var startupResult = ProcessResult(await Context.Account.Startup());

			if (!startupResult.IsLoggedIn)
			{
				if (!ConnectionHandler.IsConnected)
				{
					if (loginWaitingConnection)
					{
						return false;
					}

					loginWaitingConnection = true;

					_notificationsService.RemoveNotificationsOfType<ParadoxLoginWaitingConnectionNotification>();
					_notificationsService.RemoveNotificationsOfType<ParadoxLoginRequiredNotification>();
					_notificationsService.SendNotification(new ParadoxLoginWaitingConnectionNotification(this));

					await ConnectionHandler.WhenConnected(Login);

					return false;
				}

				loginWaitingConnection = false;

				if (!TryGetCredentials(out var credentials))
				{
					_notificationsService.RemoveNotificationsOfType<ParadoxLoginWaitingConnectionNotification>();
					_notificationsService.RemoveNotificationsOfType<ParadoxLoginRequiredNotification>();
					_notificationsService.SendNotification(new ParadoxLoginRequiredNotification(false, _interfaceService));

					return false;
				}

				var loginResult = ProcessResult(await Context.Account.Login(credentials));

				if (!loginResult.Success)
				{
					_notificationsService.RemoveNotificationsOfType<ParadoxLoginWaitingConnectionNotification>();
					_notificationsService.RemoveNotificationsOfType<ParadoxLoginRequiredNotification>();
					_notificationsService.SendNotification(new ParadoxLoginRequiredNotification(true, _interfaceService));

					return false;
				}
			}

			IsLoggedIn = true;

			_userService.SetLoggedInUser((await Context.Profile.Get()).Social?.DisplayName);

			_notificationsService.RemoveNotificationsOfType<ParadoxLoginWaitingConnectionNotification>();
			_notificationsService.RemoveNotificationsOfType<ParadoxLoginRequiredNotification>();

			OnLogin?.Invoke();
		}
		finally
		{
			IsLoginPending = false;
		}

		return true;
	}

	public async Task<bool> Login(string email, string password, bool rememberMe)
	{
		if (Context is null)
		{
			return false;
		}

		var loginResult = ProcessResult(await Context.Account.Login(new EmailAndPasswordCredential(email, password)));

		if (rememberMe && loginResult.Success)
		{
			using var key = Registry.CurrentUser.CreateSubKey("Software\\Skyve");

			key.SetValue("Email", Encryption.Encrypt(email, KEYS.SALT));
			key.SetValue("Password", Encryption.Encrypt(password, KEYS.SALT));
		}

		if (loginResult.Success)
		{
			_notificationsService.RemoveNotificationsOfType<ParadoxLoginWaitingConnectionNotification>();
			_notificationsService.RemoveNotificationsOfType<ParadoxLoginRequiredNotification>();

			IsLoggedIn = true;

			_userService.SetLoggedInUser((await Context.Profile.Get()).Social?.DisplayName);

			OnLogin?.Invoke();
		}

		return loginResult.Success;
	}

	private bool TryGetCredentials(out ICredential credential)
	{
		try
		{
			using var key = Registry.CurrentUser.OpenSubKey("Software\\Skyve");

			credential = new EmailAndPasswordCredential(Encryption.Decrypt(key.GetValue("Email").ToString(), KEYS.SALT), Encryption.Decrypt(key.GetValue("Password").ToString(), KEYS.SALT));

			return true;
		}
		catch
		{
			credential = new EmailAndPasswordCredential(string.Empty, string.Empty);
			return false;
		}
	}

	public void ClearCache()
	{
		_modProcessor.Clear();
		_userProcessor.Clear();
	}

	public IWorkshopInfo? GetInfo(IPackageIdentity identity)
	{
		return identity.Id <= 0 ? null : _modProcessor.Get((int)identity.Id).Result;
	}

	public async Task<IWorkshopInfo?> GetInfoAsync(IPackageIdentity identity)
	{
		return identity.Id <= 0 ? null : await _modProcessor.Get((int)identity.Id, true);
	}

	internal async Task<PdxModDetails?> GetInfoAsync(int id)
	{
		if (Context is null || id <= 0 || !IsLoggedIn)
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
		else if (result?.Error.Category is BaseCategory.NotFound)
		{
			return new PdxBannedMod(id);
		}

		return null;
	}

	public IAuthor? GetUser(IUser? user)
	{
		return string.IsNullOrEmpty(user?.Id?.ToString()) ? null : _userProcessor.Get(user!.Id!.ToString()).Result;
	}

	public async Task<IAuthor?> GetUserAsync(IUser? user)
	{
		return string.IsNullOrEmpty(user?.Id?.ToString()) ? null : await _userProcessor.Get(user!.Id!.ToString());
	}

	internal async Task<PdxUser?> GetUserAsync(string username)
	{
		if (Context is null || string.IsNullOrEmpty(username) || !IsLoggedIn)
		{
			return null;
		}

		var result = await Context.Mods.GetModCreatorProfile(username);

		ProcessResult(result);

		return result?.CreatorProfile is not null ? new PdxUser(result.CreatorProfile) : new PdxUser(username);
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
		return string.IsNullOrWhiteSpace(authorId?.ToString()) ? null : (IUser)new PdxUser(authorId!.ToString());
	}

	public async Task<IEnumerable<IWorkshopInfo>> GetWorkshopItemsByUserAsync(object userId, WorkshopQuerySorting sorting = WorkshopQuerySorting.DateCreated, string? query = null, string[]? requiredTags = null, bool all = false, int? limit = null, int? page = null)
	{
		if (Context is null)
		{
			return [];
		}

		if (all)
		{
			return await GetAllFilesAsync(sorting, query, requiredTags, userId);
		}

		var result = await Context.Mods.Search(new SearchData
		{
			author = userId.ToString(),
			sortBy = GetPdxSorting(sorting),
			searchQuery = query,
			tags = requiredTags?.ToList(),
			orderBy = GetPdxOrder(sorting),
			page = page,
			pageSize = limit ?? 100
		});

		ProcessResult(result);

		return result.Success ? result.Mods?.ToList(x => new PdxPackage(x)) ?? [] : (IEnumerable<IWorkshopInfo>)([]);
	}

	public async Task<IEnumerable<IWorkshopInfo>> QueryFilesAsync(WorkshopQuerySorting sorting, WorkshopSearchTime searchTime = WorkshopSearchTime.AllTime, string? query = null, string[]? requiredTags = null, bool all = false, int? limit = null, int? page = null)
	{
		if (Context is null)
		{
			return [];
		}

		if (all)
		{
			return await GetAllFilesAsync(sorting, query, requiredTags);
		}

		var result = await Context.Mods.Search(new SearchData
		{
			sortBy = GetPdxSorting(sorting),
			searchQuery = query,
			tags = requiredTags?.ToList(),
			time = sorting == WorkshopQuerySorting.Best ? (SearchTime)(int)searchTime : null,
			orderBy = GetPdxOrder(sorting),
			page = page,
			pageSize = limit ?? 100
		});

		ProcessResult(result);

		return result.Success ? result.Mods?.ToList(x => new PdxPackage(x)) ?? [] : (IEnumerable<IWorkshopInfo>)([]);
	}

	public async Task<IEnumerable<IWorkshopInfo>> GetAllFilesAsync(WorkshopQuerySorting sorting, string? query = null, string[]? requiredTags = null, object? userId = null)
	{
		if (Context is null)
		{
			return [];
		}

		var items = new List<IWorkshopInfo>();

		for (var page = 0; ; page++)
		{
			var result = await Context.Mods.Search(new SearchData
			{
				author = userId?.ToString(),
				sortBy = GetPdxSorting(sorting),
				searchQuery = query,
				tags = requiredTags?.ToList(),
				orderBy = GetPdxOrder(sorting),
				page = page,
				pageSize = 100
			});

			ProcessResult(result);

			if (!result.Success)
			{
				return items;
			}

			var mods = result.Mods?.ToList(x => new PdxPackage(x)) ?? [];

			items.AddRange(mods);

			if (mods.Count < 100)
			{
				return items;
			}
		}
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

		return cachedTags = gameData.Tags.ToList(x => (ITag)new TagItem(Skyve.Domain.CS2.Enums.TagSource.Workshop, x.Value.Id, x.Value.DisplayName));
	}

	internal async Task<List<Mod>> GetLocalPackages()
	{
		if (Context is null)
		{
			return [];
		}

		var mods = ProcessResult(await _processor.Queue(async () => await Context.Mods.List()));

		return !mods.Success || mods.Mods is null ? [] : mods.Mods;
	}

	public async Task<List<IPlayset>> GetPlaysets(bool localOnly)
	{
		if (Context is null)
		{
			return [];
		}

		var playsets = ProcessResult(await _processor.Queue(async () => await Context.Mods.ListAllPlaysets(!localOnly)));

		return !playsets.Success ? (List<IPlayset>)([]) : playsets.AllPlaysets.ToList(playset => (IPlayset)new Skyve.Domain.CS2.Content.Playset(playset));
	}

	public async Task<IPlayset?> GetCurrentPlayset()
	{
		if (Context is null)
		{
			return null;
		}

		var playsetId = await GetActivePlaysetId();
		var playsets = await GetPlaysets(false);

		return playsets.FirstOrDefault(x => x.Id == playsetId);
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

	public async Task<IEnumerable<IPlaysetPackage>> GetModsInPlayset(int playsetId, bool includeOnline = false)
	{
		if (Context is null)
		{
			return [];
		}

		return await _processor.Queue(async () =>
		{
			var list = new List<PdxPlaysetPackage>();
			var page = 1;

			while (true)
			{
				var result = ProcessResult(await Context.Mods.ListModsInPlayset(playsetId, 100, page++, includeOnline: includeOnline));

				if (result.Mods is not null)
				{
					list.AddRange(result.Mods.Select(x => new PdxPlaysetPackage(x)).Distinct(x => x.Id));
				}

				if (!result.Success || result.Mods?.Count < 100)
				{
					break;
				}
			}

			return list;
		});
	}

	public ILink? GetCommentsPageUrl(IPackageIdentity packageIdentity)
	{
		if (Context is null)
		{
			return null;
		}

		var info = GetInfo(packageIdentity);

		return info is not PdxModDetails modDetails || string.IsNullOrEmpty(modDetails.ForumLink)
			? null
			: (ILink)new ParadoxLink
			{
				Title = LocaleCS2.ForumPage,
				Type = LinkType.Paradox,
				Url = modDetails.ForumLink + "latest"
			};
	}

	public async Task<IModCommentsInfo?> GetComments(IPackageIdentity packageIdentity, int page = 1, int limit = 20)
	{
		if (Context is null)
		{
			return null;
		}

		var info = GetInfo(packageIdentity);

		if (info is not PdxModDetails modDetails || string.IsNullOrEmpty(modDetails.ForumLink))
		{
			return null;
		}

		var regex = Regex.Match(modDetails.ForumLink, @"forum/threads/[^\.]+\.(\d+)", RegexOptions.IgnoreCase);

		if (!regex.Success)
		{
			return null;
		}

		var result = ProcessResult(await Context.Mods.GetForumThread((int)info.Id, modDetails.Version, int.Parse(regex.Groups[1].Value), page, limit));

		return !result.Success
			? null
			: (IModCommentsInfo)new PdxForumThreadInfo
			{
				HasMore = result.Posts.Length == limit,
				CanPost = result.CanPost,
				Page = page,
				Posts = result.Posts.ToList(x => (IModComment)new PdxForumPost(x))
			};
	}

	public async Task<IModComment?> PostNewComment(IPackageIdentity packageIdentity, string comment)
	{
		if (Context is null)
		{
			return null;
		}

		var info = GetInfo(packageIdentity);

		if (info is not PdxModDetails modDetails || string.IsNullOrEmpty(modDetails.ForumLink))
		{
			return null;
		}

		var regex = Regex.Match(modDetails.ForumLink, @"forum/threads/[^\.]+\.(\d+)", RegexOptions.IgnoreCase);

		if (!regex.Success)
		{
			return null;
		}

		var result = ProcessResult(await Context.Mods.CreateForumPost((int)info.Id, modDetails.Version, int.Parse(regex.Groups[1].Value), comment.Replace("\r", "").Replace("\n", "\\n").Replace("\t", "\\t").Replace("\"", "'")));

		return !result.Success ? null : (IModComment)new PdxForumPost(result.Post);
	}

	internal async Task<bool> SubscribeBulk(IEnumerable<KeyValuePair<int, string?>> mods, int playset)
	{
		if (Context is null || playset <= 1 || !mods.Any())
		{
			return false;
		}

		syncOccurred = true;

		try
		{
			var result = await _processor.Queue(async () => await Context.Mods.SubscribeBulk(mods, playset));

			await Task.Delay(1000);

			return ProcessResult(result).Success;
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Catastrophic error during SubscribeBulk");

			return false;
		}
	}

	internal async Task<bool> UnsubscribeBulk(IEnumerable<int> mods, int playset)
	{
		if (Context is null || playset <= 1 || !mods.Any())
		{
			return false;
		}

		syncOccurred = true;

		try
		{
			var results = await _processor.Queue(async () =>
			{
				var results = new List<Result>();

				foreach (var id in mods)
				{
					var result = await Context.Mods.Unsubscribe(id, playset);

					results.Add(ProcessResult(result));

					await Task.Delay(1000);
				}

				return results;
			});

			return results.All(x => x.Success);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Catastrophic error during UnsubscribeBulk");

			return false;
		}
	}

	internal async Task<bool> SubscribeBulk(IEnumerable<string> mods, int playset)
	{
		if (Context is null || playset <= 1 || !mods.Any())
		{
			return false;
		}

		syncOccurred = true;

		try
		{
			var results = await _processor.Queue(async () =>
			{
				var results = new List<Result>();

				foreach (var name in mods)
				{
					var result = await Context.Mods.Subscribe(name, playset);

					results.Add(ProcessResult(result));

					await Task.Delay(1000);
				}

				return results;
			});

			return results.All(x => x.Success);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Catastrophic error during UnsubscribeBulk");

			return false;
		}
	}

	internal async Task<bool> UnsubscribeBulk(IEnumerable<string> mods, int playset)
	{
		if (Context is null || playset <= 1 || !mods.Any())
		{
			return false;
		}

		syncOccurred = true;

		try
		{
			var results = await _processor.Queue(async () =>
			{
				var results = new List<Result>();

				foreach (var name in mods)
				{
					var result = await Context.Mods.Unsubscribe(name, playset);

					results.Add(ProcessResult(result));

					await Task.Delay(1000);
				}

				return results;
			});

			return results.All(x => x.Success);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Catastrophic error during UnsubscribeBulk");

			return false;
		}
	}

	internal async Task<bool> UnsubscribeBulkCompletely(IEnumerable<int> mods)
	{
		if (Context is null)
		{
			return false;
		}

		syncOccurred = true;

		try
		{
			var results = await _processor.Queue(async () =>
			{
				var results = new List<Result>();

				foreach (var id in mods)
				{
					var result = await Context.Mods.Unsubscribe(id);

					results.Add(ProcessResult(result));

					await Task.Delay(1000);
				}

				return results;
			});

			return results.All(x => x.Success);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Catastrophic error during UnsubscribeBulk");

			return false;
		}
	}

	public async Task<bool> DeletePlayset(int playset)
	{
		if (Context is null)
		{
			return false;
		}

		return ProcessResult(await _processor.Queue(async () => await Context.Mods.DeletePlayset(playset))).Success;
	}

	public async Task<bool> ActivatePlayset(int playset)
	{
		if (Context is null)
		{
			return false;
		}

		return ProcessResult(await _processor.Queue(async () => await Context.Mods.ActivatePlayset(playset))).Success;
	}

	public async Task<bool> RenamePlayset(int playset, string name)
	{
		if (Context is null)
		{
			return false;
		}

		return ProcessResult(await _processor.Queue(async () => await Context.Mods.RenamePlayset(playset, name))).Success;
	}

	internal async Task<IPlayset?> CreatePlayset(string playsetName)
	{
		if (Context is null)
		{
			return null;
		}

		var result = ProcessResult(await _processor.Queue(async () => await Context.Mods.CreatePlayset(playsetName)));

		return result.Success ? new Skyve.Domain.CS2.Content.Playset(result) : (IPlayset?)null;
	}

	internal async Task SetLoadOrder(List<ModLoadOrder> orderedMods, int playset)
	{
		if (Context is null)
		{
			return;
		}

		ProcessResult(await _processor.Queue(async () => await Context.Mods.SetLoadOrder(orderedMods, playset)));
	}

	internal async Task<bool> SetEnableBulk(List<int> modKeys, int playset, bool enable)
	{
		if (Context is null)
		{
			return false;
		}

		try
		{
			var result = await _processor.Queue(async () => enable
				? await Context.Mods.EnableBulk(modKeys, playset)
				: await Context.Mods.DisableBulk(modKeys, playset));

			ProcessResult(result);

			return result.Success;
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Catastrophic error during SetEnableBulk({enable})");

			return false;
		}
	}

	internal async Task<bool> SetEnableBulk(List<string> modNames, int playset, bool enable)
	{
		if (Context is null)
		{
			return false;
		}

		try
		{
			var result = await _processor.Queue(async () => enable
				? await Context.Mods.EnableBulk(modNames, playset)
				: await Context.Mods.DisableBulk(modNames, playset));

			ProcessResult(result);

			return result.Success;
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Catastrophic error during SetEnableBulk({enable})");

			return false;
		}
	}

	internal async Task<IPlayset?> ClonePlayset(int id)
	{
		if (Context is null)
		{
			return null;
		}

		var result = ProcessResult(await _processor.Queue(async () => await Context.Mods.ClonePlayset(id)));

		return result.Success ? new Skyve.Domain.CS2.Content.Playset(result) : (IPlayset?)null;
	}

	public async Task RunSync()
	{
		if (Context is null || Context.Mods.SyncOngoing())
		{
			return;
		}

		syncOccurred = true;

		try
		{
			await _processor.Queue(async () =>
			{
				_notifier.IsWorkshopSyncInProgress = true;
				_notifier.OnWorkshopSyncStarted();

				var result = ProcessResult(await Context.Mods.Sync());

				if (result.Error != null && result.Error == Mods.PromptNeeded)
				{
					var conflicts = await Context.Mods.GetSyncConflicts();

					ProcessResult(await Context.Mods.Sync(SyncDirection.Downstream));
				}

				_notifier.IsWorkshopSyncInProgress = false;
			});
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to sync mods");
		}

	}

	public async Task DeactivateActivePlayset()
	{
		if (Context is null)
		{
			return;
		}

		ProcessResult(await _processor.Queue(Context.Mods.DeactivateActivePlayset));
	}

	public async Task Shutdown()
	{
		if (Context is not null)
		{
			await _processor.Queue(Context.Shutdown);

			Context = null;
			IsLoggedIn = false;
			IsLoginPending = false;
		}
	}

	public async void RepairContext()
	{
		try
		{
			_notificationsService.RemoveNotificationsOfType<ParadoxContextFailedNotification>();

			var playsetFolder = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".cache", "Mods", "playsets_metadata");
			var playsetSettingsFile = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".cache", "Mods", "PlaysetSettings");
			var tempFolder = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".pdxsdk", _settings.FolderSettings.UserIdentifier, "temp");

			Process.Start(new ProcessStartInfo()
			{
				Arguments = "/C taskkill /F /IM Skyve.Service.exe & exit",
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				WorkingDirectory = Path.GetTempPath(),
				FileName = "cmd.exe",
				Verb = WinExtensionClass.IsAdministrator ? "" : "runas"
			}).WaitForExit();

			if (Directory.Exists(tempFolder))
			{
				new DirectoryInfo(tempFolder).Delete(true);
			}

			if (CrossIO.FileExists(playsetSettingsFile))
			{
				CrossIO.DeleteFile(playsetSettingsFile, true);
			}

			if (Directory.Exists(playsetFolder))
			{
				new DirectoryInfo(playsetFolder).Delete(true);
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to repair context");
		}

		await _serviceProvider.GetService<ICentralManager>()!.Initialize();
	}

	public bool IsLocal(IPackageIdentity identity)
	{
		return identity.Id <= 0 && identity is not LocalPdxPackage;
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
			WorkshopQuerySorting.Best => SortMethod.Best,
			_ => SortMethod.Best,
		};
	}

	private T ProcessResult<T>(T result) where T : Result
	{
		if (result.Error is not null)
		{
			_logger.Error($"[PDX] [{result.Error.Category}] [{result.Error.SubCategory}] {result.Error.Details}");
		}

		return result;
	}

	#region Collection Test
	public async Task<int> CreateCollection(string folder, string name, string desc, string thumbnail, List<IPackageIdentity>? list = null)
	{
		if (Context is null)
		{
			return 0;
		}

		var config = new BaseOptionSet
		{
			DisplayName = name,
			ShortDescription = desc,
			LongDescription = desc,
			Dependencies = list?.ToDictionary(x => (int)x.Id, x => new ModDependency
			{
				Id = (int)x.Id,
				DisplayName = x.Name,
				Type = DependencyType.Mod
			}) ?? [],
			Tags = ["Code Mod"],
			ModVersion = "1",
			GameVersion = "*.*.*",
			Thumbnail = thumbnail,
		};

		var wipInfo = await Context.Mods.RegisterWIP(config.DisplayName, config.ShortDescription, config.LongDescription, 100UL);
		PrepareContent(config, wipInfo, folder);
		var updateWipData = new UpdateWipData
		{
			guid = wipInfo.Guid,
			contentFileOrFolderName = "Content",
			displayName = config.DisplayName,
			shortDescription = config.ShortDescription,
			longDescription = config.LongDescription,
			thumbnailFilename = Path.GetFileName(config.Thumbnail),
			screenshotsFilenames = config.Screenshots.Select((string s) => Path.GetFileName(s)).ToList<string>()
		};
		var updateWipData2 = updateWipData;
		var result = await Context.Mods.UpdateWIP(updateWipData2);

		var publishWipData = new PublishWipData
		{
			wipGuid = wipInfo.Guid,
			os = ModPlatform.Windows,
			recommendedGameVersion = config.GameVersion,
			userModVersion = config.ModVersion,
			forumLink = config.ForumLink,
			dependencies = config.Dependencies.Values.ToList<ModDependency>(),
			tags = config.Tags.ToList<string>()
		};
		var publishResult = await Context.Mods.PublishWIP(publishWipData);

		return publishResult.ModId;
	}

	private static void PrepareContent(BaseOptionSet config, RegisterResult wipInfo, string folder)
	{
		var text = Path.Combine(wipInfo.Path, "Content");
		if (Directory.Exists(text))
		{
			Directory.Delete(text, true);
		}

		Directory.CreateDirectory(text);
		new DirectoryInfo(folder).CopyAll(new(text));
		//File.WriteAllLines(Path.Combine(text, "ModList.txt"), config.Dependencies.ToArray(x => $"{x.Key} - {x.Value.DisplayName}"));
		var text2 = Path.Combine(wipInfo.Path, ".metadata");
		var text3 = Path.Combine(text2, Path.GetFileName(config.Thumbnail));
		if (!string.IsNullOrEmpty(config.Thumbnail) && File.Exists(config.Thumbnail))
		{
			File.Copy(config.Thumbnail, text3, true);
		}
		else
		{
		}

		if (config.Screenshots.Count > 0)
		{
			foreach (var text4 in config.Screenshots)
			{
				File.Copy(text4, Path.Combine(text2, Path.GetFileName(text4)), true);
			}
		}
	}

	public class BaseOptionSet
	{
		public string? DisplayName { get; set; }
		public string? ShortDescription { get; set; }
		public string? LongDescription { get; set; }
		public string? Thumbnail { get; set; }
		public string? ForumLink { get; set; }
		public string? ModVersion { get; set; }
		public string? GameVersion { get; set; }
		public HashSet<string>? Tags { get; set; }
		public HashSet<string> Screenshots { get; set; } = [];
		public Dictionary<int, ModDependency> Dependencies { get; set; } = [];
	}
	#endregion
}
