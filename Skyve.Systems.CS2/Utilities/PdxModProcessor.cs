using Extensions;

using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

	protected override async Task<(ConcurrentDictionary<string, PdxModDetails>, bool)> ProcessItems(List<string> entities)
	{
		var failed = false;
		var results = new ConcurrentDictionary<string, PdxModDetails>();

		foreach (var item in entities)
		{
			try
			{
				var package = await _workshopService.GetInfoAsync(item);

				if (package != null)
				{
					results[item] = package;
					results[$"{package.Id}_{package.Version}"] = package;
				}
			}
			catch
			{
				failed = true;
			}
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

	protected override bool TryGetEntityFromCache(string entity, out PdxModDetails result)
	{
		if (base.TryGetEntityFromCache(entity, out result))
		{
			return true;
		}

		if (!ulong.TryParse(entity.Substring(0, entity.Length - 1), out var id))
		{
			result = null!;
			return false;
		}

		foreach (var item in GetCache())
		{
			if (item.Id == id)
			{
				result = item;
				return true;
			}
		}

		result = null!;
		return false;
	}

	protected override void CacheItems(ConcurrentDictionary<string, PdxModDetails> results)
	{
		try
		{
			_saveHandler.Save(results, CACHE_FILE);
		}
		catch { }
	}

	private static ConcurrentDictionary<string, PdxModDetails>? GetCachedInfo(SaveHandler saveHandler)
	{
		try
		{
			var path = saveHandler.GetPath(CACHE_FILE);

			saveHandler.Load(out ConcurrentDictionary<string, PdxModDetails>? dic, CACHE_FILE);

			foreach (var item in dic?.Keys.AllWhere(x => !x.Contains("_")) ?? [])
			{
				dic!.TryRemove(item, out _);
			}

			return dic;
		}
		catch
		{
			return null;
		}
	}
}