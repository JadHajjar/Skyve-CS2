using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Steam;
using Skyve.Domain.Systems;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;

//public static class SteamUtil
//{
//	private const string DLC_CACHE_FILE = "SteamDlcsCache.json";

//	private static readonly SteamUserProcessor _steamUserProcessor;

//	public static List<SteamDlc> Dlcs { get; private set; }

//	public static event Action? DLCsLoaded;

//	static SteamUtil()
//	{
//		ServiceCenter.Get<SaveHandler>().Load(out List<SteamDlc>? cache, DLC_CACHE_FILE);

//		Dlcs = cache ?? new();

//		_steamUserProcessor = new();

//		var notifier = ServiceCenter.Get<INotifier>();

//		_steamUserProcessor.ItemsLoaded += notifier.OnWorkshopUsersInfoLoaded;
//	}

//	public static SteamUser? GetUser(ulong steamId)
//	{
//		return steamId == 0 ? null : _steamUserProcessor.Get(steamId, false).Result;
//	}

//	public static async Task<SteamUser?> GetUserAsync(ulong steamId)
//	{
//		return steamId == 0 ? null : await _steamUserProcessor.Get(steamId, true);
//	}

//	public static bool IsDlcInstalledLocally(uint dlcId)
//	{
//		return false;// _csCache?.AvailableDLCs?.Contains(dlcId) ?? false;
//	}

//	public static ulong GetLoggedInSteamId()
//	{
//		try
//		{
//			using var steam = new SteamBridge();

//			return steam.GetSteamId();
//		}
//		catch
//		{
//			return 0;
//		}
//	}

//	public static bool IsSteamAvailable()
//	{
//		return CrossIO.FileExists(ServiceCenter.Get<ILocationService>().SteamPathWithExe);
//	}

//	public static void ExecuteSteam(string args)
//	{
//		var file = ServiceCenter.Get<ILocationService>().SteamPathWithExe;

//		if (CrossIO.CurrentPlatform is Platform.Windows)
//		{
//			var process = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(file));

//			if (process.Length == 0)
//			{
//				Notification.Create(Locale.SteamNotOpenTo, null, PromptIcons.Info, null).Show(SystemsProgram.MainForm, 10);
//			}
//		}

//		ServiceCenter.Get<IIOUtil>().Execute(file, args);
//	}

//	public static async Task<Dictionary<ulong, SteamUser>> GetSteamUsersAsync(List<ulong> steamId64s)
//	{
//		steamId64s.RemoveAll(x => x == 0);

//		if (steamId64s.Count == 0)
//		{
//			return new();
//		}

//		try
//		{
//			var result = await ServiceCenter.Get<SkyveApiUtil>().Post<List<ulong>, List<SteamUser>>("/GetUsers", steamId64s);

//			return result?.ToDictionary(x => x.SteamId) ?? new();
//		}
//		catch (Exception ex)
//		{
//			ServiceCenter.Get<ILogger>().Exception(ex, "Failed to get steam author information");
//		}

//		return new();
//	}

//	public static async Task<Dictionary<string, SteamAppInfo>> GetSteamAppInfoAsync(uint steamId)
//	{
//		try
//		{
//			var url = $"https://store.steampowered.com/api/appdetails";

//			return await ApiUtil.Get<Dictionary<string, SteamAppInfo>>(url, ("appids", steamId), ("l", "english")) ?? new();
//		}
//		catch (Exception ex)
//		{
//			ServiceCenter.Get<ILogger>().Exception(ex, "Failed to get the steam information for appid " + steamId);
//		}

//		return new();
//	}

//	public static async Task LoadDlcs()
//	{
//		ServiceCenter.Get<ILogger>().Info($"Loading DLCs..");

//		var dlcs = await GetSteamAppInfoAsync(949230);

//		if (!dlcs.ContainsKey("949230"))
//		{
//			ServiceCenter.Get<ILogger>().Info($"Failed to load DLCs, steam info returned invalid content..");
//			return;
//		}

//		var newDlcs = new List<SteamDlc>(Dlcs);

//		try
//		{
//			foreach (var dlc in dlcs["949230"].data?.dlc?.Where(x => !Dlcs.Any(y => y.Id == x && y.Timestamp > DateTime.Now.AddDays(-7))) ?? [])
//			{
//				var data = await GetSteamAppInfoAsync(dlc);

//				if (data.ContainsKey(dlc.ToString()))
//				{
//					var info = data[dlc.ToString()].data!;

//					newDlcs.RemoveAll(y => y.Id == dlc);

//					newDlcs.Add(new SteamDlc
//					{
//						Timestamp = DateTime.Now,
//						Id = dlc,
//						Name = info.name!,
//						Description = info.short_description!,
//						Price = info.price_overview?.final_formatted,
//						OriginalPrice = info.price_overview?.initial_formatted,
//						Discount = info.price_overview?.discount_percent ?? 0F,
//						ReleaseDate = DateTime.TryParseExact(info.release_date?.date, "dd MMM, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt) ? dt : DateTime.MinValue
//					});
//				}
//			}

//			ServiceCenter.Get<ILogger>().Info($"DLCs ({newDlcs.Count}) loaded..");

//			ServiceCenter.Get<SaveHandler>().Save(Dlcs = newDlcs, DLC_CACHE_FILE);
//		}
//		catch (Exception ex)
//		{
//			ServiceCenter.Get<ILogger>().Exception(ex, $"Failed to load DLCs..");
//		}
//		finally
//		{
//			DLCsLoaded?.Invoke();
//		}
//	}

//	public static void ClearCache()
//	{
//		_steamUserProcessor.Clear();

//		try
//		{
//			Dlcs = new();
//			CrossIO.DeleteFile(ServiceCenter.Get<SaveHandler>().GetPath(DLC_CACHE_FILE));
//		}
//		catch (Exception ex)
//		{
//			ServiceCenter.Get<ILogger>().Exception(ex, "Failed to clear DLC_CACHE_FILE");
//		}

//		try
//		{
//			CrossIO.DeleteFile(ServiceCenter.Get<SaveHandler>().GetPath(SteamUserProcessor.STEAM_USER_CACHE_FILE));
//		}
//		catch (Exception ex)
//		{
//			ServiceCenter.Get<ILogger>().Exception(ex, "Failed to clear STEAM_USER_CACHE_FILE");
//		}
//	}
//}