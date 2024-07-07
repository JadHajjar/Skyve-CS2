using Extensions;

using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Skyve.Systems.CS2.Utilities;

public class ModDllManager : IModDllManager
{
	private const string CACHE_FILENAME = "ModDllCache.json";
	private readonly Dictionary<string, ModDllCache> _dllCache = new(StringComparer.OrdinalIgnoreCase);
	private readonly ILogger _logger;
	private readonly INotifier _notifier;
	private readonly SaveHandler _saveHandler;

	public ModDllManager(ILogger logger, INotifier notifier, SaveHandler saveHandler)
	{
		_logger = logger;
		_notifier = notifier;
		_saveHandler = saveHandler;

		saveHandler.Load(out List<ModDllCache> cache, CACHE_FILENAME);

		if (cache != null)
		{
			foreach (var dll in cache)
			{
				if (dll.Path is not null or "")
				{
					_dllCache[dll.Path] = dll;
				}
			}
		}

		_notifier.ContentLoaded += SaveDllCache;
	}

	public bool? GetDllModCache(string path, out Version? version)
	{
		if (_dllCache.TryGetValue(path, out var dll))
		{
			var currentDate = File.GetLastWriteTimeUtc(path);

			if (currentDate == dll.Date)
			{
				version = dll.Version;

				return dll.IsMod;
			}
		}

		version = null;
		return null;
	}

	public void SetDllModCache(string path, bool isMod, Version? version)
	{
		try
		{
			lock (_dllCache)
			{
				_dllCache[path] = new()
				{
					Path = path,
					Date = File.GetLastWriteTimeUtc(path),
					Version = version,
					IsMod = isMod,
				};
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to save DLL cache");
		}
	}

	public void SaveDllCache()
	{
		try
		{
			var dlls = _dllCache.Values.ToList();

			_saveHandler.Save(dlls, CACHE_FILENAME);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to save DLL cache");
		}
	}

	public void ClearDllCache()
	{
		_dllCache.Clear();

		try
		{
			CrossIO.DeleteFile(_saveHandler.GetPath(CACHE_FILENAME));
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to clear DLL cache");
		}
	}
}