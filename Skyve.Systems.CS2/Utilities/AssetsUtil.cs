using Extensions;

using Newtonsoft.Json;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Game;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Utilities;
internal class AssetsUtil : IAssetUtil
{
	private Dictionary<string, IAsset> assetIndex = [];

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

		var files = Directory.GetFiles(folder, $"*.cok", withSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

		foreach (var file in files)
		{
			if (Regex.IsMatch(Path.GetFileName(file), ExtensionClass.CharBlackListPattern))
			{
				continue;
			}

			using var archive = ZipFile.OpenRead(file);

			if (getAsset<SaveGameMetaData>(AssetType.SaveGame, ".SaveGameMetadata", out var asset, out var saveData))
			{
				asset!.SaveGameMetaData = saveData;
				yield return asset;
			}
			else if (getAsset<MapMetaData>(AssetType.Map, ".MapMetadata", out asset, out var mapData))
			{
				asset!.MapMetaData = mapData;
				yield return asset;
			}
			else
			{
				yield return new Asset(AssetType.Generic, folder, file);
			}

			bool getAsset<T>(AssetType assetType, string metaDataName, out Asset? asset, out T? data)
			{
				var metaData = archive.Entries.FirstOrDefault(x => x.FullName.EndsWith(metaDataName));

				if (metaData is null)
				{
					asset = null;
					data = default;
					return false;
				}

				using var stream = metaData.Open();
				using var reader = new StreamReader(stream);

				data = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
				asset = new Asset(assetType, folder, file);
				return true;
			}
		}
	}

	public bool IsIncluded(IAsset asset, int? playsetId = null)
	{
		return true;// !ExcludedHashSet.Contains(asset.FilePath.ToLower());
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

		if (_notifier.IsApplyingPlayset || _notifier.IsBulkUpdating)
		{
			return;
		}

		_notifier.OnInclusionUpdated();
		_notifier.TriggerAutoSave();

		SaveChanges();
	}

	public void SaveChanges()
	{
		if (_notifier.IsApplyingPlayset || _notifier.IsBulkUpdating)
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
