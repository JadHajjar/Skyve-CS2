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
using System.Threading;
using System.Threading.Tasks;

using PdxPlatform = PDX.SDK.Contracts.Enums.Platform;
using Platform = Extensions.Platform;

namespace Skyve.Systems.CS2.Services;
public class WorkshopService : IWorkshopService
{
	public event Action? OnLogin;
	event Action? IWorkshopService.OnLogout { add { } remove { } }
	public event Action? OnContextAvailable;

	private readonly ILogger _logger;
	private readonly ISettings _settings;
	private readonly INotifier _notifier;
	private readonly ICitiesManager _citiesManager;
	private readonly INotificationsService _notificationsService;
	private readonly IInterfaceService _interfaceService;
	private readonly IServiceProvider _serviceProvider;
	private readonly IDlcManager _dlcManager;
	private readonly ISkyveDataManager _skyveDataManager;
	private readonly UserService _userService;
	private readonly PdxLogUtil _pdxLogUtil;
	private readonly PdxModProcessor _modProcessor;
	private readonly PdxUserProcessor _userProcessor;
	private readonly AsyncProcessor _processor = new();
	private readonly Regex _modIdRegex = new(@"(\d+)_(\d+)?", RegexOptions.Compiled);

	private bool loginWaitingConnection;
	private bool syncOccurred;
	private List<ITag>? cachedTags;
	private CancellationTokenSource tokenSource = new();

	private IContext? Context { get; set; }
	public bool IsLoggedIn { get; private set; }
	public bool IsLoginPending { get; private set; } = true;
	public bool IsAvailable => Context is not null;
	public bool IsReady => Context is not null && IsLoggedIn && !Context.Mods.SyncOngoing();

	public WorkshopService(ILogger logger, ISettings settings, INotifier notifier, IUserService userService, ICitiesManager citiesManager, INotificationsService notificationsService, IInterfaceService interfaceService, IServiceProvider serviceProvider, PdxLogUtil pdxLogUtil, SaveHandler saveHandler, IDlcManager dlcManager, ISkyveDataManager skyveDataManager)
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
		_dlcManager = dlcManager;
		_skyveDataManager = skyveDataManager;
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

		_logger.Info("Starting PDX SDK..");

		if (!Directory.Exists(_settings.FolderSettings.AppDataPath))
		{
			IsLoginPending = false;

			throw new Exception("FolderSettings AppData folder does not exist");
		}

		var environment = BackendEnvironment.Live;
		var junction = JunctionHelper.GetJunctionState(_settings.FolderSettings.AppDataPath).IfEmpty(_settings.FolderSettings.AppDataPath);
		var @namespace = "cities_skylines_2";
		var pdxSdkPath = CrossIO.Combine(junction, ".pdxsdk");
		var platform = CrossIO.CurrentPlatform switch { Platform.MacOSX => PdxPlatform.MacOS, Platform.Linux => PdxPlatform.Linux, _ => PdxPlatform.Windows };
		var ecoSystem = _settings.FolderSettings.GamingPlatform switch { GamingPlatform.Epic => Ecosystem.Epic, GamingPlatform.Microsoft => Ecosystem.Microsoft_Store, _ => Ecosystem.Steam };

		//// For sandbox testing
		//@namespace = "pdx_sdk_cs";
		//junction = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow", "Paradox", "PDXSDKCSHARP", "Skyve");
		//pdxSdkPath = CrossIO.Combine(junction, ".pdxsdk");
		//environment = BackendEnvironment.Sandbox;

		if (!string.IsNullOrWhiteSpace(_settings.FolderSettings.UserIdentifier))
		{
			pdxSdkPath = CrossIO.Combine(pdxSdkPath, _settings.FolderSettings.UserIdentifier);
		}
		else
		{
			if (CrossIO.FileExists(CrossIO.Combine(pdxSdkPath, "LastUserId.txt")))
			{
				_settings.FolderSettings.UserIdentifier = File.ReadAllText(CrossIO.Combine(pdxSdkPath, "LastUserId.txt"));

				pdxSdkPath = CrossIO.Combine(pdxSdkPath, _settings.FolderSettings.UserIdentifier);
			}
			else
			{
				return;
			}
		}

		var config = new Config
		{
			Language = Language.en,
			GameVersion = _citiesManager.GameVersion,
			Logger = _pdxLogUtil,
			DiskIORoot = pdxSdkPath,
			Environment = environment,
			Ecosystem = ecoSystem,
			UserId = _settings.FolderSettings.UserIdentifier,
			UserIdType = _settings.FolderSettings.UserIdType.IfEmpty("steam"),
			Telemetry = new TelemetryConfig
			{
				StandardTelemetryEnabled = false,
				TelemetryDebugEnabled = false,
				UnityEventTelemetryEnable = false,
			},
#if DEBUG
			LogLevel = LogLevel.L1_Debug,
			DefaultHeaders = new Dictionary<string, string>() { { "User-Agent", $"Skyve/9999" } },
#else
			LogLevel = LogLevel.L2_Warning,
			DefaultHeaders = new Dictionary<string, string>() { { "User-Agent", $"Skyve/{typeof(WorkshopService).Assembly.GetName().Version}" } },
#endif
		};

		config.Mods.UsePatching = true;
		config.Mods.RootPath = CrossIO.Combine(junction, ".cache", "Mods");

		//if (Enum.TryParse<Language>(LocaleHelper.CurrentCulture.IetfLanguageTag.Substring(0, 2).ToLower(), out var lang))
		//{
		//	config.Language = lang;
		//}

		try
		{
			Context = await PDX.SDK.Context.Create(
				platform: platform,
				@namespace: @namespace,
				config: config);

			_logger.Info("SDK Created");

			new WorkshopEventsManager(this, _serviceProvider).RegisterModsCallbacks(Context);

			OnContextAvailable?.Invoke();
		}
		catch (Exception ex)
		{
			_notificationsService.SendNotification(new ParadoxContextFailedNotification(this));

			_logger.Exception(ex, memberName: "Failed to create PDX Context");
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

			_logger.Info("Logging in...");

			var startupResult = ProcessResult(await Context.Account.Startup());

			if (!startupResult.IsLoggedIn)
			{
				_logger.Info("Existing session failed to log in.");

				if (!ConnectionHandler.IsConnected)
				{
					if (loginWaitingConnection)
					{
						return false;
					}

					_logger.Info("Waiting for internet connection to log in...");

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
					_logger.Info("No existing credentials to log in with.");

					_notificationsService.RemoveNotificationsOfType<ParadoxLoginWaitingConnectionNotification>();
					_notificationsService.RemoveNotificationsOfType<ParadoxLoginRequiredNotification>();
					_notificationsService.SendNotification(new ParadoxLoginRequiredNotification(false, _interfaceService));

					return false;
				}

				var loginResult = ProcessResult(await Context.Account.Login(credentials));

				if (!loginResult.Success)
				{
					_logger.Info("Login attempt with existing credentials failed.");

					_notificationsService.RemoveNotificationsOfType<ParadoxLoginWaitingConnectionNotification>();
					_notificationsService.RemoveNotificationsOfType<ParadoxLoginRequiredNotification>();
					_notificationsService.SendNotification(new ParadoxLoginRequiredNotification(true, _interfaceService));

					return false;
				}
			}

			IsLoggedIn = true;

			var userName = (await Context.Profile.Get()).Social?.DisplayName;

			if (string.IsNullOrEmpty(userName))
			{
				userName = (await Context.Account.GetDetails())?.Email.RegexReplace(@"\*+", "***") ?? "N/A";
			}

			_userService.SetLoggedInUser(userName);

			_logger.Info("Logged in with " + userName);

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
		if (identity.Id <= 0)
		{
			return null;
		}

		return _modProcessor.Get($"{identity.Id}_{identity.Version}", false).Result;
	}

	public async Task<IWorkshopInfo?> GetInfoAsync(IPackageIdentity identity)
	{
		if (identity.Id <= 0)
		{
			return null;
		}

		return await _modProcessor.Get($"{identity.Id}_{identity.Version}", true);
	}

	internal async Task<PdxModDetails?> GetInfoAsync(string id)
	{
		var rgx = _modIdRegex.Match(id);

		if (!rgx.Success)
		{
			return null;
		}

		if (Context is not null)
		{
			var result = await Context.Mods.GetLocalModDetails(int.Parse(rgx.Groups[1].Value), rgx.Groups[2].Value.IfEmpty(null));

			if (!result.Success || result?.Mod is null)
			{
				var platform = CrossIO.CurrentPlatform switch { Platform.MacOSX => ModPlatform.Osx, Platform.Linux => ModPlatform.Linux, _ => ModPlatform.Windows };
				result = await Context.Mods.GetDetails(int.Parse(rgx.Groups[1].Value), rgx.Groups[2].Value.IfEmpty(null), platform);
			}
			else
			{
				result.Mod.HasLiked = _modProcessor.Get(id, false).Result?.HasVoted ?? false;
			}

			if (result.Success && result?.Mod is not null)
			{
				return new PdxModDetails(result.Mod)
				{
					Requirements = result.Mod.Dependencies?.Where(x => x.Type is not DependencyType.Dlc).ToArray(x => new PdxModsRequirement(x)) ?? [],
					DlcRequirements = result.Mod.Dependencies?.Where(x => x.Type is DependencyType.Dlc && x.DisplayName != "Beach Properties").ToArray(x => new PdxModsDlcRequirement(_dlcManager.TryGetDlc(x.DisplayName))) ?? []
				};
			}
		}

		return new PdxBannedMod(int.Parse(rgx.Groups[1].Value));
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

	public IEnumerable<IUser> GetKnownUsers()
	{
		return _userProcessor.GetCache();
	}

	public IPackage GetPackage(IPackageIdentity identity)
	{
		return GetInfo(identity) as IPackage ?? new PdxModIdentityPackage(identity);
	}

	public async Task<IPackage> GetPackageAsync(IPackageIdentity identity)
	{
		return await GetInfoAsync(identity) as IPackage ?? new PdxModIdentityPackage(identity);
	}

	public async Task<(IEnumerable<IWorkshopInfo> Mods, int TotalCount)> GetWorkshopItemsByUserAsync(object userId, WorkshopQuerySorting sorting = WorkshopQuerySorting.DateCreated, string? query = null, string[]? requiredTags = null, bool all = false, int? limit = null, int? page = null)
	{
		if (Context is null)
		{
			return ([], 0);
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

		return (result.Success ? result.Mods?.ToList(x => new PdxPackage(x)) ?? [] : (IEnumerable<IWorkshopInfo>)[], result.TotalCount);
	}

	public async Task<(IEnumerable<IWorkshopInfo> Mods, int TotalCount)> QueryFilesAsync(WorkshopQuerySorting sorting, WorkshopSearchTime searchTime = WorkshopSearchTime.AllTime, string? query = null, string[]? requiredTags = null, bool all = false, int? limit = null, int? page = null)
	{
		if (Context is null)
		{
			return ([], 0);
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

		return (result.Success ? result.Mods?.ToList(x => new PdxPackage(x)) ?? [] : (IEnumerable<IWorkshopInfo>)[], result.TotalCount);
	}

	public async Task<(IEnumerable<IWorkshopInfo> Mods, int TotalCount)> GetAllFilesAsync(WorkshopQuerySorting sorting, string? query = null, string[]? requiredTags = null, object? userId = null)
	{
		if (Context is null)
		{
			return ([], 0);
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
				return (items, items.Count);
			}

			var mods = result.Mods?.ToList(x => new PdxPackage(x)) ?? [];

			items.AddRange(mods);

			if ((page + 1) * 100 >= result.TotalCount)
			{
				return (items, items.Count);
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

		return !playsets.Success ? [] : playsets.AllPlaysets.ToList(playset => (IPlayset)new Skyve.Domain.CS2.Content.Playset(playset));
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
			await _modProcessor.Get($"{packageIdentity.Id}_{packageIdentity.Version}", true);
		}

		_modProcessor.CacheItems();

		return result.Success;
	}

	public async Task<int> GetActivePlaysetId()
	{
		return Context is null ? 0 : (await Context.Mods.GetActivePlayset()).PlaysetId;
	}

	public async Task<IEnumerable<IPlaysetPackage>> GetModsInPlayset(int playsetId, bool includeOnline = false)
	{
		if (Context is null)
		{
			return [];
		}

		return await _processor.Queue(async () =>
		{
			var result = ProcessResult(await Context.Mods.ListModsInPlayset(playsetId, 10_000, 1, includeOnline: includeOnline));

			if (result.Mods is not null)
			{
				return result.Mods.Select(x => new PdxPlaysetPackage(x)).ToList();
			}

			return [];
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
		mods = mods.Where(x => x.Key > 0);

		if (Context is null || playset <= 0 || !mods.Any())
		{
			return false;
		}

		syncOccurred = true;

		try
		{
			var result = await _processor.Queue(async () => await Context.Mods.SubscribeBulk(mods, playset, tokenSource.Token));

			await Task.Delay(1000);

			return ProcessResult(result).Success;
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: "Catastrophic error during SubscribeBulk");

			return false;
		}
	}

	internal async Task<bool> UnsubscribeBulk(IEnumerable<int> mods, int playset)
	{
		if (Context is null || playset <= 0 || !mods.Any())
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
			_logger.Exception(ex, memberName: "Catastrophic error during UnsubscribeBulk");

			return false;
		}
	}

	internal async Task<bool> SubscribeBulk(IEnumerable<string> mods, int playset)
	{
		if (Context is null || playset <= 0 || !mods.Any())
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
					var result = await Context.Mods.Subscribe(name, playset, tokenSource.Token);

					results.Add(ProcessResult(result));

					await Task.Delay(1000);
				}

				return results;
			});

			return results.All(x => x.Success);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: "Catastrophic error during UnsubscribeBulk");

			return false;
		}
	}

	internal async Task<bool> UnsubscribeBulk(IEnumerable<string> mods, int playset)
	{
		if (Context is null || playset <= 0 || !mods.Any())
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
			_logger.Exception(ex, memberName: "Catastrophic error during UnsubscribeBulk");

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
			_logger.Exception(ex, memberName: "Catastrophic error during UnsubscribeBulk");

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

		var result = await _processor.Queue(async () =>
		{
			var createPlaysetResult = ProcessResult(await Context.Mods.CreatePlayset(playsetName));

			if (!createPlaysetResult.Success)
			{
				return null;
			}

			var playsets = ProcessResult(await Context.Mods.ListAllPlaysets(true));

			return playsets.AllPlaysets?.FirstOrDefault(x => x.PlaysetId == createPlaysetResult.PlaysetId);
		});

		return result is null ? (IPlayset?)null : new Skyve.Domain.CS2.Content.Playset(result);
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
			_logger.Exception(ex, memberName: $"Catastrophic error during SetEnableBulk({enable})");

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
			_logger.Exception(ex, memberName: $"Catastrophic error during SetEnableBulk({enable})");

			return false;
		}
	}

	internal async Task<IPlayset?> ClonePlayset(int id)
	{
		if (Context is null)
		{
			return null;
		}

		var result = await _processor.Queue(async () =>
		{
			if (!ProcessResult(await Context.Mods.ClonePlayset(id)).Success)
			{
				return null;
			}

			var playsets = ProcessResult(await Context.Mods.ListAllPlaysets(true));

			return playsets.AllPlaysets?.OrderBy(x => x.PlaysetId).LastOrDefault();
		});

		return result is null ? (IPlayset?)null : new Skyve.Domain.CS2.Content.Playset(result);
	}

	public Task RunUpSync()
	{
		return RunSync(SyncDirection.Upstream);
	}

	public Task RunDownSync()
	{
		return RunSync(SyncDirection.Downstream);
	}

	public Task RunSync()
	{
		return RunSync(SyncDirection.Default);
	}

	private async Task RunSync(SyncDirection direction)
	{
		if (Context is null || Context.Mods.SyncOngoing())
		{
			return;
		}

		syncOccurred = true;

		try
		{
			var result = await _processor.Queue(async () =>
			{
				try
				{
					_notifier.IsWorkshopSyncInProgress = true;
					_notifier.OnWorkshopSyncStarted();

					return ProcessResult(await Context.Mods.Sync(direction, cancellationToken: tokenSource.Token));
				}
				finally
				{
					_notifier.IsWorkshopSyncInProgress = false;
					//_notifier.OnWorkshopSyncEnded();
				}
			});

			if (result.Error == Mods.PromptNeeded)
			{
				var conflicts = await Context.Mods.GetSyncConflicts();

				_interfaceService.OpenSyncConflictPrompt(conflicts.ToArray(x => new SyncConflictInfo(x)));
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: "Failed to sync mods");
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

	public void CancelActions()
	{
		tokenSource.Cancel();
		tokenSource = new();
	}

	public async void RepairContext()
	{
		try
		{
			_notificationsService.RemoveNotificationsOfType<ParadoxContextFailedNotification>();

			var playsetFolder = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".cache", "Mods", "playsets_metadata");
			var playsetSettingsFile = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".cache", "Mods", "PlaysetSettings");
			var databaseFile = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".pdxsdk", _settings.FolderSettings.UserIdentifier, "database.json");
			var cacheJournalFolder = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".pdxsdk", _settings.FolderSettings.UserIdentifier, "cache_journal");
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

			if (CrossIO.FileExists(databaseFile))
			{
				CrossIO.DeleteFile(databaseFile, true);
			}

			if (Directory.Exists(cacheJournalFolder))
			{
				new DirectoryInfo(cacheJournalFolder).Delete(true);
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: "Failed to repair context");
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
			_logger.Error($"[PDX] [{result.Error.Category}] [{result.Error.SubCategory}] {result.Error.Details}"
#if DEBUG
				+ $"\r\n{new StackTrace()}"
#endif
				);
		}

		return result;
	}
}
