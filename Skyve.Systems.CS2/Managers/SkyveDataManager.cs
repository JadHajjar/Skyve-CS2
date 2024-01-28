using Extensions;

using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.Compatibility.Domain;
using Skyve.Systems.CS2.Domain;
using Skyve.Systems.CS2.Systems;
using Skyve.Systems.CS2.Utilities;

using SkyveApi.Domain.CS2;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class SkyveDataManager(ILogger _logger, INotifier _notifier, ISkyveApiUtil _skyveApiUtil, ICompatibilityUtil _compatibilityUtil) : ISkyveDataManager
{
	private const string DATA_CACHE_FILE = "CompatibilityDataCache.json";

	private readonly Dictionary<IPackageIdentity, CompatibilityInfo> _cache = new(new IPackageEqualityComparer());
	private readonly Regex _bracketsRegex = new(@"[\[\(](.+?)[\]\)]", RegexOptions.Compiled);
	private readonly Regex _urlRegex = new(@"(https?|ftp)://(?:www\.)?([\w-]+(?:\.[\w-]+)*)(?:/[^?\s]*)?(?:\?[^#\s]*)?(?:#.*)?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

	public IndexedCompatibilityData CompatibilityData { get; private set; } = new(null);

	public void Start(List<IPackage> packages)
	{
		try
		{
			var path = ISave.GetPath(DATA_CACHE_FILE);

			ISave.Load(out CompatibilityData? data, DATA_CACHE_FILE);

			CompatibilityData = new IndexedCompatibilityData(data);
		}
		catch { }
	}

	public void ResetCache()
	{
		_cache.Clear();

		try
		{
			CrossIO.DeleteFile(ISave.GetPath(DATA_CACHE_FILE));
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to clear CR cache");
		}

		_notifier.OnContentLoaded();
	}

	public async Task DownloadData()
	{
		try
		{
			var data = await ((SkyveApiUtil)_skyveApiUtil).Catalogue();

			if (data is not null)
			{
				ISave.Save(data, DATA_CACHE_FILE);

				CompatibilityData = new IndexedCompatibilityData(data);

				_notifier.OnCompatibilityDataLoaded();

#if DEBUG
				if (System.Diagnostics.Debugger.IsAttached)
				{
					var dic = await _skyveApiUtil.Translations();

					if (dic is not null)
					{
						File.WriteAllText("../../../../SkyveApp.Systems/Properties/CompatibilityNotes.json", Newtonsoft.Json.JsonConvert.SerializeObject(dic, Newtonsoft.Json.Formatting.Indented));
					}
				}
#endif
				return;
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to get compatibility data");
		}

		CompatibilityData ??= new IndexedCompatibilityData(new());
	}

	public bool IsBlacklisted(IPackageIdentity package)
	{
		return CompatibilityData.BlackListedIds.Contains(package.Id)
			|| CompatibilityData.BlackListedNames.Contains(package.Name ?? string.Empty)
			|| (package.GetWorkshopInfo()?.IsIncompatible ?? false);
	}

	public ulong GetIdFromModName(string fileName)
	{
		return CompatibilityData.PackageNames.TryGet(fileName);
	}

	public IIndexedPackageCompatibilityInfo? GetPackageCompatibilityInfo(IPackageIdentity identity)
	{
		if (identity.Id > 0)
		{
			var packageData = CompatibilityData.Packages.TryGet(identity.Id);

			if (packageData is null)
			{
				packageData = new IndexedPackage(GetAutomatedReport(identity));

				packageData.Load(CompatibilityData.Packages);
			}

			return packageData;
		}

		return null;
	}

	public CompatibilityPackageData GetAutomatedReport(IPackageIdentity package)
	{
		var info = new CompatibilityPackageData
		{
			Stability = package.GetPackage()?.IsCodeMod == true ? PackageStability.NotReviewed : PackageStability.AssetNotReviewed,
			Id = package.Id,
			Name = package.Name,
			FileName = package.GetLocalPackageIdentity()?.FilePath,
			Links = [],
			Interactions = [],
			Statuses = [],
		};

		var workshopInfo = package.GetWorkshopInfo();

		if (workshopInfo?.Requirements.Any() ?? false)
		{
			info.Interactions.AddRange(workshopInfo.Requirements.GroupBy(x => x.IsOptional).Select(o =>
				new PackageInteraction
				{
					Type = o.Key ? InteractionType.OptionalPackages : InteractionType.RequiredPackages,
					Action = StatusAction.SubscribeToPackages,
					Packages = o.ToArray(x => x.Id)
				}));
		}

		var tagMatches = _bracketsRegex.Matches(workshopInfo?.Name ?? string.Empty);

		foreach (Match match in tagMatches)
		{
			var tag = match.Value.ToLower();

			if (tag.ToLower() is "broken")
			{
				info.Stability = PackageStability.Broken;
			}
			else if (tag.ToLower() is "obsolete" or "deprecated" or "abandoned")
			{
				info.Statuses.Add(new(StatusType.Deprecated));
			}
			else if (tag.ToLower() is "alpha" or "experimental" or "beta" or "test" or "testing")
			{
				info.Statuses.Add(new(StatusType.TestVersion));
			}
		}

		((CompatibilityUtil)_compatibilityUtil).PopulateAutomaticPackageInfo(info, package, workshopInfo);

		if (workshopInfo?.Description is not null)
		{
			var matches = _urlRegex.Matches(workshopInfo.Description);

			foreach (Match match in matches)
			{
				var type = match.Groups[2].Value.ToLower() switch
				{
					"youtube.com" or "youtu.be" => LinkType.YouTube,
					"github.com" => LinkType.Github,
					"discord.com" or "discord.gg" => LinkType.Discord,
					"crowdin.com" => LinkType.Crowdin,
					"buymeacoffee.com" or "patreon.com" or "ko-fi.com" or "paypal.com" => LinkType.Donation,
					_ => LinkType.Other
				};

				if (type is not LinkType.Other)
				{
					info.Links.Add(new PackageLink
					{
						Url = match.Value,
						Type = type,
					});
				}
			}
		}

		return info;
	}

	public IKnownUser TryGetAuthor(string? id)
	{
		return CompatibilityData.Authors.TryGetValue(id, out var author) ? author : new();
	}

	public IIndexedPackageCompatibilityInfo TryGetPackageInfo(ulong id)
	{
		return CompatibilityData.Packages.TryGet(id);
	}

	public IEnumerable<ulong> GetPackagesKeys()
	{
		return CompatibilityData.Packages.Keys;
	}
}
