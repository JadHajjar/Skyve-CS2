using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Steam;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class DlcManager : IDlcManager
{
	private const string DLC_CACHE_FILE = "DlcCache.json";

	private readonly DlcConfig _config;
	private readonly ILogger _logger;
	private readonly SaveHandler _saveHandler;
	private readonly ApiUtil _apiUtil;
	private Dictionary<ulong, SteamDlc> dlcs = [];

	public IEnumerable<IDlcInfo> Dlcs => dlcs.Values;

	public event Action? DlcsLoaded;

	public DlcManager(ILogger logger, SaveHandler saveHandler, ApiUtil apiUtil)
	{
		_logger = logger;
		_saveHandler = saveHandler;
		_apiUtil = apiUtil;

		try
		{
			_config = _saveHandler.Load<DlcConfig>();
			_saveHandler.Load(out dlcs, DLC_CACHE_FILE);
		}
		catch
		{ }

		_config ??= new();
		dlcs ??= [];
	}

	public bool IsAvailable(IDlcInfo dlc)
	{
#if STEAM
		return SteamUtil.IsDlcOwned((uint)dlc.Id);
#else
		return _config.AvailableDLCs.Contains(dlc.Id);
#endif
	}

	public bool IsAvailable(ulong dlc)
	{
#if STEAM
		return SteamUtil.IsDlcOwned((uint)dlc);
#else
		return _config.AvailableDLCs.Contains(dlc);
#endif
	}

	public bool IsIncluded(IDlcInfo dlc)
	{
		return !_config.RemovedDLCs.Contains(dlc.Id);
	}

	public void SetExcludedDlcs(IEnumerable<IDlcInfo> dlcs)
	{
		_config.RemovedDLCs = dlcs.ToList(x => x.Id);

		_config.Save();
	}

	public void SetIncluded(IDlcInfo dlc, bool value)
	{
		if (value)
		{
			_config.RemovedDLCs.Remove(dlc.Id);
		}
		else
		{
			_config.RemovedDLCs.AddIfNotExist(dlc.Id);
		}

		_config.Save();
	}

	public List<IDlcInfo> GetExcludedDlcs()
	{
		return _config.RemovedDLCs.Select(x => (IDlcInfo)(dlcs.TryGetValue(x, out var dlc) ? dlc : new SteamDlc() { Id = x })).ToList();
	}

	public async Task UpdateDLCs()
	{
		_logger.Info($"Loading DLCs..");

		var steamAppInfo = await GetSteamAppInfoAsync(949230);

		if (!steamAppInfo.ContainsKey("949230"))
		{
			_logger.Info($"Failed to load DLCs, steam info returned invalid content..");
			return;
		}

		var newDlcs = new ConcurrentDictionary<ulong, SteamDlc>(dlcs);

		var allDlcs = steamAppInfo["949230"].data?.dlc.ToList() ?? [];

		allDlcs.RemoveAll(dlc => dlcs.TryGetValue(dlc, out var dlcInfo) && dlcInfo.Timestamp < DateTime.Now.AddDays(-7));

		await Task.WhenAll(allDlcs.Select(new Func<ulong, Task>(async (dlc) =>
		{
			var data = await GetSteamAppInfoAsync(dlc);

			if (data.ContainsKey(dlc.ToString()))
			{
				var info = data[dlc.ToString()].data!;

				newDlcs[dlc] = new SteamDlc
				{
					Timestamp = DateTime.Now,
					Id = dlc,
					Name = info.name!,
					Description = info.short_description!,
					IsFree = info.is_free,
					Price = info.price_overview?.final_formatted,
					OriginalPrice = info.price_overview?.initial_formatted,
					Discount = info.price_overview?.discount_percent ?? 0F,
					Creators = info.developers?.Where(x => x is not "Colossal Order Ltd." and not "Paradox Interactive").ToArray(),
					ExpectedRelease = ((info.release_date?.coming_soon ?? true) ? info.release_date?.date : string.Empty) ?? "TBD",
					ReleaseDate = DateTime.TryParseExact(info.release_date?.date, "d MMM, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt) ? dt : DateTime.MinValue
				};
			}
		})).ToArray());

		newDlcs[2427731] = GetLandmarkDlc();

		_logger.Info($"DLCs ({newDlcs.Count}) loaded..");

		_saveHandler.Save(dlcs = new(newDlcs), DLC_CACHE_FILE);

		DlcsLoaded?.Invoke();
	}

	private SteamDlc GetLandmarkDlc()
	{
		return new SteamDlc
		{
			Timestamp = DateTime.MaxValue,
			Id = 2427731,
			Name = "Cities: Skylines II - Landmark Buildings",
			Description = "The Cities: Skylines II Pre-Order Pack contains nine Unique Landmark Buildings and also a map based on the geography of Tampere, home of Colossal Order."!,
			IsFree = false,
			Price = null,
			OriginalPrice = null,
			Discount = 0f,
			Creators = [],
			ExpectedRelease = string.Empty,
			ReleaseDate = new DateTime(2023, 10, 24)
		};
	}

	public async Task<Dictionary<string, SteamAppInfo>> GetSteamAppInfoAsync(ulong steamId)
	{
		try
		{
			const string url = "https://store.steampowered.com/api/appdetails";

			return await _apiUtil.Get<Dictionary<string, SteamAppInfo>>(url, ("appids", steamId), ("l", "english")) ?? [];
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: "Failed to get the steam information for appid " + steamId);
		}

		return [];
	}

	public IDlcInfo TryGetDlc(string displayName)
	{
		if (displayName == "San Fransisco Set")
		{
			displayName = "San Francisco Set";
		}

		foreach (var item in Dlcs)
		{
			var text = item.Name.RegexRemove("^.+?- ").RegexRemove("(Content )?Creator Pack: ");

			if (text == displayName)
			{
				return item;
			}
		}

		return new SteamDlc { Name = displayName };
	}

	public IDlcInfo TryGetDlc(ulong dlc)
	{
		foreach (var item in Dlcs)
		{
			if (item.Id == dlc)
			{
				return item;
			}
		}

		return new SteamDlc { Id = dlc, Name = dlc.ToString() };
	}
}
