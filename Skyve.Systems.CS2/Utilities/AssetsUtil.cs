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
using System.Text;
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
	private readonly ISettings _settings;
	private readonly IImageService _imageService;

	public AssetsUtil(IPackageManager contentManager, INotifier notifier, ILogger logger, ISettings settings, IImageService imageService)
	{
		_contentManager = contentManager;
		_notifier = notifier;
		_logger = logger;
		_settings = settings;
		_imageService = imageService;

		//_notifier.ContentLoaded += BuildAssetIndex;
	}

	public IEnumerable<IAsset> GetAssets(string folder, out int assetCount, bool withSubDirectories = true)
	{
		assetCount = 0;

		if (!Directory.Exists(folder))
		{
			return [];
		}

		var files = Directory.EnumerateFiles(folder, $"*.cok", withSubDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
		var assets = new List<IAsset>();

		foreach (var file in files)
		{
			if (Regex.IsMatch(Path.GetFileName(file), ExtensionClass.CharBlackListPattern))
			{
				continue;
			}

#if DEBUG
			_logger.Debug($"Reading asset: {file}..");
#endif

			ZipArchive zip;

			try
			{
				zip = ZipFile.OpenRead(file);
			}
			catch (Exception ex)
			{
				_logger.Exception(ex, memberName: "Failed to load asset: " + file);
				continue;
			}

			using var archive = zip;

#if DEBUG
			_logger.Debug($"Asset contains {archive.Entries.Count} items...");
#endif

			if (getAsset<SaveGameMetaData>(AssetType.SaveGame, ".SaveGameMetadata", out var asset, out var saveData))
			{
#if DEBUG
				_logger.Debug($"SaveGame asset created: {asset!.Name}...");
#endif
				asset!.SaveGameMetaData = saveData;
				asset.Tags = ["Savegame", saveData!.Theme];
				 assets.Add( asset);
			}
			else if (getAsset<MapMetaData>(AssetType.Map, ".MapMetadata", out asset, out var mapData))
			{
#if DEBUG
				_logger.Debug($"Map asset created: {asset!.Name}...");
#endif
				asset!.MapMetaData = mapData;
				asset.Tags = ["Map", mapData!.Theme];
				 assets.Add( asset);
			}
			else
			{
				var assetIconDic = new List<(Asset Asset, string Entry)>();
				var entries = new Dictionary<string, string>();

				foreach (var entry in archive.Entries)
				{
#if DEBUG
					//_logger.Debug($"Working on entry: {entry.Name}...");
#endif
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

					var isBinary = (ushort)stream.ReadByte() != 123;

					var isValidAsset = isBinary
						? GetTypeAndNameFromBinary(stream, out var type, out var name, out var icon)
						: GetTypeAndNameFromJson(stream, out type, out name, out icon);

					if (!isValidAsset || type is "Game.Prefabs.RenderPrefab, Game")
					{
						continue;
					}

					assetCount++;

					if (isBinary)
					{
						continue;
					}

					asset = new Asset(name!.RegexRemove(@"\\u\w{4}").Trim()
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

					assets.Add( asset);
				}

				foreach (var item in assetIconDic)
				{
					if (entries.TryGetValue(item.Entry, out var image))
					{
						if (!CrossIO.FileExists(item.Asset.SetThumbnail(_imageService)))
						{
							archive.GetEntry(image).ExtractToFile(item.Asset.Thumbnail);
						}
					}
				}
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

		return assets;
	}

	private bool GetTypeAndNameFromBinary(Stream stream, out string? type, out string? name, out string? icon)
	{
		using var r = new BinaryReader(stream);

		r.ReadBytes(10); // skip first bytes

		var typeBuilder = new StringBuilder();

		for (var i = 0; i < 500; i++)
		{
			var ch = Encoding.Unicode.GetChars(r.ReadBytes(2))[0];

			if (ch is '\0' or 'ī')
			{
				break;
			}

			typeBuilder.Append(ch);
		}

		r.ReadBytes(21); // skip extra bytes

		var nameBuilder = new StringBuilder();

		for (var i = 0; i < 500; i++)
		{
			var ch = Encoding.Unicode.GetChars(r.ReadBytes(2))[0];

			if (ch is '\0' or 'ī')
			{
				break;
			}

			nameBuilder.Append(ch);
		}

		type = typeBuilder.ToString().RegexRemove("^\\d+\\|");
		name = nameBuilder.ToString();
		icon = null;

		return true;
	}

	public static bool GetTypeAndNameFromJson(Stream stream, out string? type, out string? name, out string? icon)
	{
		using var reader = new StreamReader(stream);
		var jsonString = reader.ReadToEnd();

		// Find matches
		var typeMatch = Regex.Match(jsonString, @"""\$type"":\s*""\d+\|([^""]+)""");
		var nameMatch = Regex.Match(jsonString, @"""name"":\s*""([^""]+)""");
		var iconMatch = Regex.Match(jsonString, @"""assetdb://global/(\w+)""", RegexOptions.IgnoreCase);

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

	public Task SetIncluded(IAsset asset, bool value, int? playsetId = null)
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
			return Task.CompletedTask;
		}

		_notifier.OnInclusionUpdated();
		_notifier.TriggerAutoSave();

		SaveChanges();

		return Task.CompletedTask;
	}

	public void SaveChanges()
	{
		//if (_notifier.IsApplyingPlayset || _notifier.IsBulkUpdating)
		//{
		//	return;
		//}

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

	public void DeleteAsset(IAsset asset)
	{
		CrossIO.DeleteFile(asset.FilePath);

		if (CrossIO.FileExists(asset.FilePath + ".cid"))
		{
			CrossIO.DeleteFile(asset.FilePath + ".cid");
		}

		if (asset.Package?.LocalData is not LocalPackageData packageData)
		{
			return;
		}

		var source = packageData.Assets;
		var dest = new IAsset[source.Length - 1];
		var index = source.IndexOf(asset);

		if (index > 0)
		{
			Array.Copy(source, 0, dest, 0, index);
		}

		if (index < source.Length - 1)
		{
			Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
		}

		packageData.Assets = dest;

		_notifier.OnContentLoaded();
	}
}
