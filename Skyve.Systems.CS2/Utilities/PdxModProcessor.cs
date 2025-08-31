using Extensions;

using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
internal class PdxModProcessor : PeriodicProcessor<string, PdxModDetails>
{
	public const string CACHE_FILE = "PdxModsCache.json";

	private readonly WorkshopService _workshopService;
	private readonly SaveHandler _saveHandler;
	private readonly INotifier _notifier;

	public PdxModProcessor(WorkshopService workshopService, SaveHandler saveHandler, INotifier notifier) : base(100, 1000, GetCachedInfo(saveHandler))
	{
		_workshopService = workshopService;
		_saveHandler = saveHandler;
		_notifier = notifier;
		MaxCacheTime = TimeSpan.FromHours(1);
	}

	protected override bool CanProcess()
	{
		return ConnectionHandler.IsConnected;
	}

	protected override async Task<(Dictionary<string, PdxModDetails>, bool)> ProcessItems(List<string> entities)
	{
		var failed = false;
		var results = new Dictionary<string, PdxModDetails>();

		foreach (var item in entities)
		{
			try
			{
				var package = await _workshopService.GetInfoAsync(item);

				if (package != null)
				{
					results[$"{package.Id}_{package.Version}"] = package;
				}
			}
			catch { failed = true; }
		}

		try
		{
			return (results, failed);
		}
		finally
		{
			_notifier.OnWorkshopInfoUpdated();
		}
	}

	protected override void CacheItems(Dictionary<string, PdxModDetails> results)
	{
		try
		{
			_saveHandler.Save(results, CACHE_FILE);
		}
		catch { }
	}

	private static Dictionary<string, PdxModDetails>? GetCachedInfo(SaveHandler saveHandler)
	{
		try
		{
			var path = saveHandler.GetPath(CACHE_FILE);

			saveHandler.Load(out Dictionary<string, PdxModDetails>? dic, CACHE_FILE);

			foreach (var item in dic?.Keys.AllWhere(x => !x.Contains("_")) ?? [])
			{
				dic!.Remove(item);
			}

			return dic;
		}
		catch
		{
			return null;
		}
	}
}