using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
internal class PdxModProcessor : PeriodicProcessor<int, PdxModDetails>
{
	public const string CACHE_FILE = "PdxModsCache.json";

	private readonly WorkshopService _workshopService;

	public PdxModProcessor(WorkshopService workshopService) : base(1, 5000, GetCachedInfo())
	{
		_workshopService = workshopService;

		MaxCacheTime = TimeSpan.FromHours(1);
	}

	protected override bool CanProcess()
	{
		return ConnectionHandler.IsConnected;
	}

	protected override async Task<(Dictionary<int, PdxModDetails>, bool)> ProcessItems(List<int> entities)
	{
		var failed = false;
		var results = new Dictionary<int, PdxModDetails>();

		foreach (var item in entities)
		{
			try
			{
				var package = await _workshopService.GetInfoAsync(item);

				if (package != null)
					results[item] = package;
			}
			catch { failed = true; }
		}

		try
		{
			return (results, failed);
		}
		finally
		{
			ServiceCenter.Get<INotifier>().OnWorkshopInfoUpdated();
		}
	}

	protected override void CacheItems(Dictionary<int, PdxModDetails> results)
	{
		try
		{
			ISave.Save(results, CACHE_FILE);
		}
		catch { }
	}

	private static Dictionary<int, PdxModDetails>? GetCachedInfo()
	{
		try
		{
			var path = ISave.GetPath(CACHE_FILE);

			if (DateTime.Now - File.GetLastWriteTime(path) > TimeSpan.FromDays(7) && ConnectionHandler.IsConnected)
			{
				return null;
			}

			ISave.Load(out Dictionary<int, PdxModDetails>? dic, CACHE_FILE);

			return dic;
		}
		catch
		{
			return null;
		}
	}
}