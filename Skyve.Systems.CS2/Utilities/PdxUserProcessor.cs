using Extensions;

using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
internal class PdxUserProcessor : PeriodicProcessor<string, PdxUser>
{
	public const string CACHE_FILE = "PdxUsersCache.json";

	private readonly WorkshopService _workshopService;
	private readonly SaveHandler _saveHandler;
	private readonly INotifier _notifier;

	public PdxUserProcessor(WorkshopService workshopService, SaveHandler saveHandler, INotifier notifier) : base(1, 5000, GetCachedInfo(saveHandler))
	{
		_workshopService = workshopService;
		_saveHandler = saveHandler;
		_notifier = notifier;
		MaxCacheTime = TimeSpan.FromDays(5);
	}

	protected override bool CanProcess()
	{
		return ConnectionHandler.IsConnected;
	}

	protected override async Task<(Dictionary<string, PdxUser>, bool)> ProcessItems(List<string> entities)
	{
		var failed = false;
		var results = new Dictionary<string, PdxUser>();

		foreach (var item in entities)
		{
			try
			{
				var user = await _workshopService.GetUserAsync(item);

				if (user != null)
				{
					results[item] = user;
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
			_notifier.OnWorkshopUsersInfoLoaded();
		}
	}

	protected override void CacheItems(Dictionary<string, PdxUser> results)
	{
		try
		{
			_saveHandler.Save(results, CACHE_FILE);
		}
		catch { }
	}

	private static Dictionary<string, PdxUser>? GetCachedInfo(SaveHandler saveHandler)
	{
		try
		{
			var path = saveHandler.GetPath(CACHE_FILE);

			saveHandler.Load(out Dictionary<string, PdxUser>? dic, CACHE_FILE);

			return dic;
		}
		catch
		{
			return null;
		}
	}
}