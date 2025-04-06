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
	private IEnumerable<SteamDlc> dlcs = [];

	public IEnumerable<IDlcInfo> Dlcs => dlcs;

	public event Action? DlcsLoaded;

	public DlcManager(ILogger logger, SaveHandler saveHandler, ApiUtil apiUtil)
	{
		_config = saveHandler.Load<DlcConfig>();
		_logger = logger;
		_saveHandler = saveHandler;
		_apiUtil = apiUtil;
	}

	public bool IsAvailable(uint dlcId)
	{
		return false;
	}

	public bool IsIncluded(IDlcInfo dlc)
	{
		return !_config.RemovedDLCs.Contains(dlc.Id);
	}

	public void SetExcludedDlcs(IEnumerable<uint> dlcs)
	{
		_config.RemovedDLCs = dlcs.ToList();

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

	public List<uint> GetExcludedDlcs()
	{
		return new(_config.RemovedDLCs);
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

		var newDlcs = new List<SteamDlc>(dlcs);

		foreach (var dlc in steamAppInfo["949230"].data!.dlc.Where(x => !dlcs.Any(y => y.Id == x && y.Timestamp > DateTime.Now.AddDays(-7))))
		{
			var data = await GetSteamAppInfoAsync(dlc);

			if (data.ContainsKey(dlc.ToString()))
			{
				var info = data[dlc.ToString()].data!;

				newDlcs.RemoveAll(y => y.Id == dlc);

				newDlcs.Add(new SteamDlc
				{
					Timestamp = DateTime.Now,
					Id = dlc,
					Name = info.name!,
					Description = info.short_description!,
					Price = info.price_overview?.final_formatted,
					OriginalPrice = info.price_overview?.initial_formatted,
					Discount = info.price_overview?.discount_percent ?? 0F,
					ReleaseDate = DateTime.TryParseExact(info.release_date?.date, "dd MMM, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt) ? dt : DateTime.MinValue
				});
			}
		}

		_logger.Info($"DLCs ({newDlcs.Count}) loaded..");

		_saveHandler.Save(dlcs = newDlcs, DLC_CACHE_FILE);

		DlcsLoaded?.Invoke();
	}

	public async Task<Dictionary<string, SteamAppInfo>> GetSteamAppInfoAsync(uint steamId)
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
}
