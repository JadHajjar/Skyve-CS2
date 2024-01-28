using Extensions;

using Skyve.Domain;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
internal class AssetsUtil : IAssetUtil
{
	private Dictionary<string, IAsset> assetIndex = new();

	public HashSet<string> ExcludedHashSet { get; }

	private readonly IPackageManager _contentManager;
	private readonly INotifier _notifier;

	public AssetsUtil(IPackageManager contentManager, INotifier notifier)
	{
		_contentManager = contentManager;
		_notifier = notifier;

		_notifier.ContentLoaded += BuildAssetIndex;
	}

	public IEnumerable<IAsset> GetAssets(string folder, bool withSubDirectories = true)
	{
		if (!Directory.Exists(folder))
		{
#if DEBUG
			ServiceCenter.Get<ILogger>().Debug("Getting assets failed, directory not found: " + folder);
#endif
			yield break;
		}

		//var files = Directory.GetFiles(package.Folder, $"*.crp", withSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

		//foreach (var file in files)
		//{
		//	if (Regex.IsMatch(Path.GetFileName(file), ExtensionClass.CharBlackListPattern))
		//		continue;

		//	yield return new Asset(package, fileName, asset);
		//}
	}

	public bool IsIncluded(IAsset asset, int? playsetId = null)
	{
		return !ExcludedHashSet.Contains(asset.FilePath.ToLower());
	}

	public async Task SetIncluded(IAsset asset, bool value, int? playsetId = null)
	{
		if (value)
		{
			ExcludedHashSet.Remove(asset.FilePath.ToLower());
		}
		else
		{
			ExcludedHashSet.Add(asset.FilePath.ToLower());
		}

		if (_notifier.ApplyingPlayset || _notifier.BulkUpdating)
		{
			return;
		}

		_notifier.OnInclusionUpdated();
		_notifier.TriggerAutoSave();

		SaveChanges();
	}

	public void SaveChanges()
	{
		if (_notifier.ApplyingPlayset || _notifier.BulkUpdating)
		{
			return;
		}

		//_config.ExcludedAssets = ExcludedHashSet.ToList();

		//_config.Serialize();
	}

	internal void SetExcludedAssets(IEnumerable<string> excludedAssets)
	{
		ExcludedHashSet.Clear();

		foreach (var item in excludedAssets)
		{
			ExcludedHashSet.Add(item);
		}

		SaveChanges();
	}

	public IAsset? GetAssetByFile(string? v)
	{
		return assetIndex.TryGet(v ?? string.Empty);
	}

	public void BuildAssetIndex()
	{
		assetIndex = _contentManager.Assets.ToDictionary(x => x.FilePath.FormatPath(), StringComparer.OrdinalIgnoreCase);
	}
}
