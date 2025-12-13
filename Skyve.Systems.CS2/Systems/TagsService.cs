using Extensions;

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
	private readonly Dictionary<string, int> _workshopTagsUsage;
	private readonly Dictionary<string, string[]> _assetTagsDictionary;
	private readonly Dictionary<string, string[]> _customTagsDictionary;
	private readonly Dictionary<string, HashSet<string>> _tagsCache;
	private readonly IServiceProvider _serviceProvider;
	private readonly INotifier _notifier;
	private readonly ILogger _logger;
	private readonly ISkyveDataManager _skyveDataManager;
	private readonly SaveHandler _saveHandler;
	private readonly WorkshopService _workshopService;
	private Dictionary<string, IWorkshopTag> _workshopTags = [];

	public TagsService(IServiceProvider serviceProvider, INotifier notifier, IWorkshopService workshopService, ILogger logger, SaveHandler saveHandler, ISkyveDataManager skyveDataManager)
	{
		_assetTagsDictionary = new(new PathEqualityComparer());
		_customTagsDictionary = new(new PathEqualityComparer());
		_tagsCache = new(StringComparer.InvariantCultureIgnoreCase);
		_serviceProvider = serviceProvider;
		_notifier = notifier;
		_logger = logger;
		_saveHandler = saveHandler;
		_skyveDataManager = skyveDataManager;
		_workshopService = (WorkshopService)workshopService;
		_assetTags = [];
		_workshopTagsUsage = [];

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

		if (_workshopService.IsAvailable)
		{
			Task.Run(UpdateWorkshopTags);
		}
		else
		{
			_workshopService.OnContextAvailable += UpdateWorkshopTags;
		}

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

		//foreach (var asset in _serviceProvider.GetService<IPackageManager>()!.Assets)
		//{
		//	foreach (var item in asset.Tags)
		//	{
		//		_assetTags.Add(item);
		//	}
		//}

		//var assetDictionary = new Dictionary<string, IAsset>(StringComparer.CurrentCultureIgnoreCase);

		//foreach (var asset in ServiceCenter.Get<IPackageManager>().Assets)
		//{
		//	assetDictionary[(asset as Asset)!.FullName] = asset;
		//}
	}

	private async void UpdateWorkshopTags()
	{
		try
		{
			var tags = _workshopService.GetAvailableTags();

			_workshopTags = Combine(tags);

			var packages = await _workshopService.GetLocalPackages();

			lock (_workshopTagsUsage)
			{
				_workshopTagsUsage.Clear();

				foreach (var item in tags)
				{
					_workshopTagsUsage[item.Value] = 0;
				}

				foreach (var package in packages)
				{
					foreach (var tag in package.Tags ?? [])
					{
						_workshopTagsUsage[tag.DisplayName] = _workshopTagsUsage.GetOrAdd(tag.DisplayName) + 1;
					}
				}
			}
		}
		catch (Exception ex)
		{
			_logger.Warning("Failed to update workshop tags: " + ex.Message);
		}
	}

	private Dictionary<string, IWorkshopTag> Combine(IEnumerable<IWorkshopTag> tags)
	{
		var dic = new Dictionary<string, IWorkshopTag>();

		foreach (var tag in tags)
		{
			dic[tag.Key] = tag;

			foreach (var item in Combine(tag.Children ?? []))
			{
				dic[item.Key] = item.Value;
			}
		}

		return dic;
	}

	public IEnumerable<ITag> GetDistinctTags()
	{
		var returned = new List<string>();

		lock (_workshopTagsUsage)
		{
			foreach (var item in _workshopTagsUsage)
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

	public IEnumerable<ITag> GetTags(IPackageIdentity package, bool ignoreParent = false, bool ignoreSubTags = false)
	{
		var returned = new List<string>() { string.Empty };
		var isAsset = package is IAsset;

		if (isAsset)
		{
			foreach (var item in (package as IAsset)!.Tags)
			{
				if (!returned.Contains(item))
				{
					returned.Add(item);
					yield return new TagItem(TagSource.InGame, item, item);
				}
			}

			//if (_assetTagsDictionary.TryGetValue(asset.FilePath, out var assetTags))
			//{
			//	foreach (var item in assetTags)
			//	{
			//		if (!returned.Contains(item))
			//		{
			//			returned.Add(item);
			//			yield return new TagItem(TagSource.InGame, item, item);
			//		}
			//	}
			//}
		}

		if (!ignoreParent || !isAsset)
		{
			var tags = package.GetWorkshopInfo()?.Tags?.Select(x => _workshopTags.TryGet(x.Key) ?? CreateWorkshopTag(x.Value, x.Key)) ?? [];
			foreach (var item in tags.OrderBy(x => x.Order))
			{
				if (ignoreSubTags && item.Depth > 1)
				{
					continue;
				}

				if (returned.Contains(item.Value))
				{
					continue;
				}

				returned.Add(item.Value);

				yield return new TagItem(TagSource.Workshop, item.Key, item.Value);
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
		var matchedTags = tags.ToList(x => x.Key);
		var identity = package.GetLocalPackageIdentity();
		var workshopTags = package.GetWorkshopInfo()?.Tags ?? [];

		matchedTags.RemoveAll(y => workshopTags.Any(x => x.Key.Equals(y, StringComparison.InvariantCultureIgnoreCase)));

		if (matchedTags.Count == 0)
		{
			return true;
		}

		var assetTags = package is IAsset asset ? asset.Tags ?? [] : []; // identity is not null && _assetTagsDictionary.TryGetValue(identity.FilePath, out var tags1) ? tags1 : [];

		matchedTags.RemoveAll(y => assetTags.Any(x => x.Equals(y, StringComparison.InvariantCultureIgnoreCase)));

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
		lock (_workshopTagsUsage)
		{
			return
				(_workshopTagsUsage.TryGetValue(tag.Value, out var count) ? count : 0) +
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

	public IWorkshopTag CreateWorkshopTag(string text, string? key)
	{
		return new TagItem(TagSource.Workshop, key ?? text, text);
	}

	public IWorkshopTag GetWorkshopTag(string key)
	{
		if (_workshopTags.TryGetValue(key, out var tag))
		{
			return tag;
		}

		return CreateWorkshopTag(key, key);
	}

	public ITag CreateIdTag(string text)
	{
		return new TagItem(TagSource.ID, text, text);
	}
}
