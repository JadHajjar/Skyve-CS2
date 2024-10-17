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
	//private Dictionary<string, IAsset> assetIndex = [];

	public HashSet<string> ExcludedHashSet { get; } = [];

	private readonly IPackageManager _contentManager;
	private readonly INotifier _notifier;
	private readonly ILogger _logger;

	public AssetsUtil(IPackageManager contentManager, INotifier notifier, ILogger logger)
	{
		_contentManager = contentManager;
		_notifier = notifier;
		_logger = logger;

		//_notifier.ContentLoaded += BuildAssetIndex;
	}

	public IEnumerable<IAsset> GetAssets(string folder, bool withSubDirectories = true)
	{
		if (!Directory.Exists(folder))
		{
			yield break;
		}

		var files = Directory.EnumerateFiles(folder, $"*.cok", withSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

		foreach (var file in files)
		{
			if (Regex.IsMatch(Path.GetFileName(file), ExtensionClass.CharBlackListPattern))
			{
				continue;
			}

			ZipArchive archive;

			try
			{
				archive = ZipFile.OpenRead(file);
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, "Failed to load asset: " + file);
				continue;
			}

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
				var assetIconDic = new List<(Asset, string)>();
				var entries = new Dictionary<string, string>();

				foreach (var entry in archive.Entries)
				{
					if (entry.FullName.EndsWith(".cid", StringComparison.InvariantCultureIgnoreCase))
					{
						using var cidStream = entry.Open();
						using var cidReader = new StreamReader(cidStream);

						entries[cidReader.ReadToEnd()] = entry.FullName.TrimEnd(4);

						continue;
					}

					if (!entry.FullName.EndsWith(".prefab", StringComparison.InvariantCultureIgnoreCase))
					{
						continue;
					}

					using var stream = entry.Open();
					using var reader = new StreamReader(stream);

					if (GetTypeAndNameFromJson(reader.ReadToEnd(), out var type, out var name, out var icon))
					{
						if (type is "0|Game.Prefabs.RenderPrefab, Game")
						{
							continue;
						}

						asset = new Asset(name!
							, AssetType.Generic
							, folder
							, file
							, calculateTotalSize(entry)
							, entry.LastWriteTime.ToUniversalTime().Date
							, [Regex.Match(type, @"\.(\w+?)(Prefab)?,").Groups[1].Value.FormatWords()]);

						if (icon is not null and not "")
						{
							assetIconDic.Add((asset, icon));
						}

						yield return asset;
					}
				}

				foreach (var item in assetIconDic)
				{
					if (entries.TryGetValue(item.Item2, out var image))
					{
						var tempFile = CrossIO.GetTempFileName();

						archive.GetEntry(image).ExtractToFile(tempFile);

						item.Item1.Thumbnail = tempFile;
					}
				}
			}

			archive.Dispose();

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

			long calculateTotalSize(ZipArchiveEntry entry)
			{
				var size = 0L;

				var start = Path.GetFileNameWithoutExtension(entry.FullName);
				var vtStart = "StreamingData~/VT/" + Path.GetFileNameWithoutExtension(entry.FullName);

				foreach (var item in archive.Entries)
				{
					if (item.FullName.StartsWith(start, StringComparison.InvariantCultureIgnoreCase)
						|| item.FullName.StartsWith(vtStart, StringComparison.InvariantCultureIgnoreCase))
					{
						size += item.Length;
					}
				}

				return size;
			}
		}
	}

	public static bool GetTypeAndNameFromJson(string jsonString, out string? type, out string? name, out string? icon)
	{
		// Find matches
		var typeMatch = Regex.Match(jsonString, @"""\$type"":\s*""([^""]+)""");
		var nameMatch = Regex.Match(jsonString, @"""name"":\s*""([^""]+)""");
		var iconMatch = Regex.Match(jsonString, @"""assetdb://Global/(\w+)""");

		// If both type and name are found, assign them to the out parameters
		if (typeMatch.Success && nameMatch.Success)
		{
			type = typeMatch.Groups[1].Value;
			name = nameMatch.Groups[1].Value;
			icon = iconMatch.Groups[1].Value;
			return true;
		}

		// Initialize out parameters
		type = null;
		name = null;
		icon = null;

		// Return false if not found
		return false;
	}

	public bool IsIncluded(IAsset asset, int? playsetId = null)
	{
		return true;// !ExcludedHashSet.Contains(asset.FilePath.ToLower());
	}

	public async Task SetIncluded(IAsset asset, bool value, int? playsetId = null)
	{
		if (value)
		{
			ExcludedHashSet?.Remove(asset.FilePath.ToLower());
		}
		else
		{
			ExcludedHashSet?.Add(asset.FilePath.ToLower());
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
		throw new NotImplementedException();
		//return assetIndex.TryGet(v ?? string.Empty);
	}

	public void BuildAssetIndex()
	{
		//assetIndex = _contentManager.Assets.ToDictionary(x => x.FilePath.FormatPath(), StringComparer.OrdinalIgnoreCase);
	}
}
