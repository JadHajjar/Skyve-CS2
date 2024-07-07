using Extensions;

using Microsoft.Extensions.DependencyInjection;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Managers;
using Skyve.Systems.CS2.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Systems;
internal class TagsService : ITagsService
{
	private readonly HashSet<string> _assetTags;
	private readonly Dictionary<string, int> _workshopTags;
	private readonly Dictionary<string, string[]> _assetTagsDictionary;
	private readonly Dictionary<string, string[]> _customTagsDictionary;
	private readonly Dictionary<string, HashSet<string>> _tagsCache;

	private readonly INotifier _notifier;
	private readonly ILogger _logger;
	private readonly ISkyveDataManager _skyveDataManager;
	private readonly SaveHandler _saveHandler;
	private readonly WorkshopService _workshopService;

	public TagsService(INotifier notifier, IWorkshopService workshopService, ILogger logger, SaveHandler saveHandler, ISkyveDataManager skyveDataManager)
	{
		_assetTagsDictionary = new(new PathEqualityComparer());
		_customTagsDictionary = new(new PathEqualityComparer());
		_tagsCache = new(StringComparer.InvariantCultureIgnoreCase);
		_notifier = notifier;
		_logger = logger;
		_saveHandler = saveHandler;
		_skyveDataManager = skyveDataManager;
		_workshopService = (WorkshopService)workshopService;
		_assetTags = [];
		_workshopTags = [];

		_saveHandler.Load(out Dictionary<string, string[]> customTags, "CustomTags.json");

		if (customTags is not null)
		{
			foreach (var tag in customTags)
			{
				_customTagsDictionary[tag.Key] = tag.Value;
			}
		}

		//_notifier.WorkshopInfoUpdated += UpdateWorkshopTags;
		_notifier.ContentLoaded += GenerateCache;

		Task.Run(UpdateWorkshopTags);
		Task.Run(GenerateCache);
	}

	private void GenerateCache()
	{
		_tagsCache.Clear();

		foreach (var kvp in _assetTagsDictionary)
		{
			foreach (var item in kvp.Value)
			{
				if (!_tagsCache.ContainsKey(item))
				{
					_tagsCache[item] = new(new PathEqualityComparer());
				}

				_tagsCache[item].Add(kvp.Key);
			}
		}

		foreach (var kvp in _customTagsDictionary)
		{
			foreach (var item in kvp.Value)
			{
				if (!_tagsCache.ContainsKey(item))
				{
					_tagsCache[item] = new(new PathEqualityComparer());
				}

				_tagsCache[item].Add(kvp.Key);
			}
		}

		var assetDictionary = new Dictionary<string, IAsset>(StringComparer.CurrentCultureIgnoreCase);

		//foreach (var asset in ServiceCenter.Get<IPackageManager>().Assets)
		//{
		//	assetDictionary[(asset as Asset)!.FullName] = asset;
		//}
	}

	private async void UpdateWorkshopTags()
	{
		try
		{
			var tags = await _workshopService.GetAvailableTags();
			var packages = await _workshopService.GetLocalPackages();

			lock (_workshopTags)
			{
				_workshopTags.Clear();

				foreach (var item in tags)
				{
					_workshopTags[item.Value] = 0;
				}

				foreach (var package in packages)
				{
					foreach (var tag in package.Tags ?? [])
					{
						_workshopTags[tag.DisplayName] = _workshopTags.GetOrAdd(tag.DisplayName) + 1;
					}
				}
			}
		}
		catch (Exception ex) { _logger.Warning("Failed to update workshop tags: " + ex.Message); }
	}

	public IEnumerable<ITag> GetDistinctTags()
	{
		var returned = new List<string>();

		lock (_workshopTags)
		{
			foreach (var item in _workshopTags)
			{
				if (!returned.Contains(item.Key))
				{
					returned.Add(item.Key);
					yield return new TagItem(TagSource.Workshop, item.Key, item.Key);
				}
			}
		}

		foreach (var item in _assetTags)
		{
			if (!returned.Contains(item))
			{
				returned.Add(item);
				yield return new TagItem(TagSource.InGame, item, item);
			}
		}

		foreach (var kvp in _customTagsDictionary)
		{
			foreach (var item in kvp.Value)
			{
				if (!returned.Contains(item))
				{
					returned.Add(item);

					yield return new TagItem(TagSource.Custom, item, item);
				}
			}
		}

		foreach (var package in ((SkyveDataManager)_skyveDataManager).CompatibilityData.Packages.Values.ToList())
		{
			if (package.Tags is not null)
			{
				foreach (var item in package.Tags)
				{
					if (!returned.Contains(item))
					{
						returned.Add(item);

						yield return new TagItem(TagSource.Global, item, item);
					}
				}
			}
		}
	}

	public IEnumerable<ITag> GetTags(IPackageIdentity package, bool ignoreParent = false)
	{
		var returned = new List<string>();

		if (!ignoreParent && package.GetWorkshopInfo()?.Tags?.Values is IEnumerable<string> workshopTags)
		{
			foreach (var item in workshopTags)
			{
				if (!returned.Contains(item))
				{
					returned.Add(item);
					yield return new TagItem(TagSource.Workshop, item, item);
				}
			}
		}

		if (package is IAsset asset)
		{
			if (_assetTagsDictionary.TryGetValue(asset.FilePath, out var assetTags))
			{
				foreach (var item in assetTags)
				{
					if (!returned.Contains(item))
					{
						returned.Add(item);
						yield return new TagItem(TagSource.InGame, item, item);
					}
				}
			}
		}

		var skyveTags = _skyveDataManager.TryGetPackageInfo(package.Id)?.Tags;

		if (skyveTags is not null)
		{
			foreach (var item in skyveTags)
			{
				if (!returned.Contains(item))
				{
					returned.Add(item);
					yield return new TagItem(TagSource.Global, item, item);
				}
			}
		}

		if (package.GetLocalPackageIdentity() is ILocalPackageIdentity localPackage)
		{
			if (_customTagsDictionary.TryGetValue(localPackage.FilePath, out var customTags))
			{
				foreach (var item in customTags)
				{
					if (!returned.Contains(item))
					{
						returned.Add(item);
						yield return new TagItem(TagSource.Custom, item, item);
					}
				}
			}
		}
	}

	public void SetTags(IPackageIdentity package, IEnumerable<string> value)
	{
		if (package.GetLocalPackageIdentity() is ILocalPackageData lp)
		{
			_customTagsDictionary[lp.Folder] = value.WhereNotEmpty().ToArray();

			try
			{
				_saveHandler.Save(_customTagsDictionary, "CustomTags.json");
			}
			catch (Exception ex)
			{
				_logger.Exception(ex);
			}
		}

		_notifier.OnRefreshUI(true);
	}

	public bool HasAllTags(IPackageIdentity package, IEnumerable<ITag> tags)
	{
		var matchedTags = tags.ToList(x => x.Value);
		var identity = package.GetLocalPackageIdentity();
		var workshopTags = package.GetWorkshopInfo()?.Tags.Values.ToList() ?? [];

		matchedTags.RemoveAll(y => workshopTags.Any(x => x.Equals(y, StringComparison.InvariantCultureIgnoreCase)));

		if (matchedTags.Count == 0 )
		{
			return true;
		}

		var assetTags = identity is not null && _assetTagsDictionary.TryGetValue(identity.FilePath, out var tags1) ? tags1 : [];

		matchedTags.RemoveAll(x => assetTags.Any(x => x.Equals(x, StringComparison.InvariantCultureIgnoreCase)));

		if (matchedTags.Count == 0)
		{
			return true;
		}

		var customTags = identity is not null && _customTagsDictionary.TryGetValue(identity.FilePath, out var tags2) ? tags2 : [];

		matchedTags.RemoveAll(y => customTags.Any(x => x.Equals(y, StringComparison.InvariantCultureIgnoreCase)));

		if (matchedTags.Count == 0)
		{
			return true;
		}

		var skyveTags = _skyveDataManager.TryGetPackageInfo(package.Id)?.Tags ?? [];

		matchedTags.RemoveAll(y => skyveTags.Any(x => x.Equals(y, StringComparison.InvariantCultureIgnoreCase)));

		if (matchedTags.Count == 0)
		{
			return true;
		}

		return false;
	}

	public int GetTagUsage(ITag tag)
	{
		lock (_workshopTags)
		{
			return
				(_workshopTags.TryGetValue(tag.Value, out var count) ? count : 0) +
				(_tagsCache.TryGetValue(tag.Value, out var hash) ? hash.Count : 0);
		}
	}

	public ITag CreateGlobalTag(string text)
	{
		return new TagItem(TagSource.Global, text, text);
	}

	public ITag CreateCustomTag(string text)
	{
		return new TagItem(TagSource.Custom, text, text);
	}

	public ITag CreateWorkshopTag(string text)
	{
		return new TagItem(TagSource.Workshop, text, text);
	}

	public ITag CreateIdTag(string text)
	{
		return new TagItem(TagSource.ID, text, text);
	}
}
