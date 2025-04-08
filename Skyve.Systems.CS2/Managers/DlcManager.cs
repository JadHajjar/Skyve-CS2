using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Steam;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
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
		return _config.AvailableDLCs.Contains(dlc.Id);
	}

	public bool IsAvailable(ulong dlc)
	{
		return _config.AvailableDLCs.Contains(dlc);
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

		var newDlcs = new Dictionary<ulong, SteamDlc>(dlcs);

		foreach (var dlc in steamAppInfo["949230"].data?.dlc ?? [])
		{
			if (dlcs.TryGetValue(dlc, out var dlcInfo) && dlcInfo.Timestamp > DateTime.Now.AddDays(-7))
			{
				continue;
			}

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
					Price = info.price_overview?.final_formatted,
					OriginalPrice = info.price_overview?.initial_formatted,
					Discount = info.price_overview?.discount_percent ?? 0F,
					Creators = info.developers?.Where(x => x is not "Colossal Order Ltd." and not "Paradox Interactive").ToArray(),
					ReleaseDate = DateTime.TryParseExact(info.release_date?.date, "dd MMM, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt) ? dt : DateTime.MinValue
				};
			}
		}

		_logger.Info($"DLCs ({newDlcs.Count}) loaded..");

		_saveHandler.Save(dlcs = newDlcs, DLC_CACHE_FILE);

		DlcsLoaded?.Invoke();
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
			_logger.Exception(ex, "Failed to get the steam information for appid " + steamId);
		}

		return [];
	}

	public IDlcInfo TryGetDlc(string displayName)
	{
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
}
