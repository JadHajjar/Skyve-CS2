using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Enums;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.IO;
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
	private readonly WorkshopService _workshopService;

	public TagsService(INotifier notifier, IWorkshopService workshopService, ILogger logger)
	{
		_assetTagsDictionary = new(new PathEqualityComparer());
		_customTagsDictionary = new(new PathEqualityComparer());
		_tagsCache = new(StringComparer.InvariantCultureIgnoreCase);
		_notifier = notifier;
		_logger = logger;
		_workshopService =(WorkshopService)workshopService;
		_assetTags = new HashSet<string>();
		_workshopTags = new Dictionary<string, int>();

		ISave.Load(out Dictionary<string, string[]> customTags, "CustomTags.json");

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

		if (package.GetLocalPackageIdentity() is ILocalPackageData localPackage)
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

		if (!ignoreParent && package.GetLocalPackageIdentity() is ILocalPackageData lp && _customTagsDictionary.TryGetValue(lp.Folder, out var customParentTags))
		{
			foreach (var item in customParentTags)
			{
				if (!returned.Contains(item))
				{
					returned.Add(item);
					yield return new TagItem(TagSource.Custom, item, item);
				}
			}
		}
	}

	public void SetTags(IPackageIdentity package, IEnumerable<string> value)
	{
		if (package is IAsset asset)
		{
			_customTagsDictionary[asset.FilePath] = value.WhereNotEmpty().ToArray();

			ISave.Save(_customTagsDictionary, "CustomTags.json");
		}
		else if (package.GetLocalPackageIdentity() is ILocalPackageData lp)
		{
			_customTagsDictionary[lp.Folder] = value.WhereNotEmpty().ToArray();

			ISave.Save(_customTagsDictionary, "CustomTags.json");
		}

		_notifier.OnRefreshUI(true);
	}

	public bool HasAllTags(IPackageIdentity package, IEnumerable<ITag> tags)
	{
		var workshopTags = package.GetWorkshopInfo()?.Tags;

		if (package is ILocalPackageData localPackage)
		{
			foreach (var tag in tags)
			{
				if (_tagsCache.TryGetValue(tag.Value, out var hash) && hash.Contains(localPackage.FilePath))
				{
					continue;
				}

				if (workshopTags?.Any(x => x.Value.Equals(tag.Value, StringComparison.InvariantCultureIgnoreCase)) ?? false)
				{
					continue;
				}

				return false;
			}

			return true;
		}

		foreach (var tag in tags)
		{
			if (workshopTags?.Any(x => x.Value.Equals(tag.Value, StringComparison.InvariantCultureIgnoreCase)) ?? false)
			{
				continue;
			}

			return false;
		}

		return true;
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
		return new TagItem(TagSource.Workshop, text	, text);
	}
}
