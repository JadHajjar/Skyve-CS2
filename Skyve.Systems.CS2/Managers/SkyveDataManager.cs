using Extensions;

using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Systems.Compatibility.Domain;
using Skyve.Systems.CS2.Domain;
using Skyve.Systems.CS2.Domain.Api;
using Skyve.Systems.CS2.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
public class SkyveDataManager(ILogger _logger, INotifier _notifier, IUserService _userService, SkyveApiUtil _skyveApiUtil, ICompatibilityUtil _compatibilityUtil, INotificationsService _notificationsService, SaveHandler saveHandler) : ISkyveDataManager
{
	private readonly Dictionary<IPackageIdentity, CompatibilityInfo> _cache = new(new IPackageEqualityComparer());
	private readonly Regex _bracketsRegex = new(@"[\[\(](.+?)[\]\)]", RegexOptions.Compiled);
	private readonly Regex _urlRegex = new(@"(https?|ftp)://(?:www\.)?([\w-]+(?:\.[\w-]+)*)(?:/[^?\s]*)?(?:\?[^#\s]*)?(?:#.*)?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

	public IndexedCompatibilityData CompatibilityData { get; private set; } = new();

	public void Start(List<IPackage> packages)
	{
		try
		{
			var data = saveHandler.Load<CompatibilityData>();

			CompatibilityData = new IndexedCompatibilityData(data.Packages, data.BlackListedIds, data.BlackListedNames);
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to load compatibility data cache");
		}
	}

	public void ResetCache()
	{
		_cache.Clear();

		try
		{
			saveHandler.Delete<CompatibilityData>();
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to clear compatibility data cache");
		}

		_notifier.OnContentLoaded();
	}

	public async Task DownloadData()
	{
		try
		{
			var blackList = await _skyveApiUtil.GetBlacklist();
			var packages = await _skyveApiUtil.GetPackageData();
			var users = await _skyveApiUtil.GetUsers();

			if (packages.Length > 0)
			{
				saveHandler.Save(new CompatibilityData
				{
					Packages = packages,
					BlackListedIds = blackList.BlackListedIds,
					BlackListedNames = blackList.BlackListedNames,
				});
			}

			((UserService)_userService).SetKnownUsers(users);

			CompatibilityData = new IndexedCompatibilityData(packages, blackList.BlackListedIds, blackList.BlackListedNames);

			_notifier.OnCompatibilityDataLoaded();

			var announcements = await _skyveApiUtil.GetAnnouncements();

			_notificationsService.RemoveNotificationsOfType<AnnouncementNotification>();
			foreach (var item in announcements ?? [])
			{
				if (item.Title is not null and not "")
				{
					_notificationsService.SendNotification(item);
				}
			}

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
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to get compatibility data");
		}

		CompatibilityData ??= new IndexedCompatibilityData([], []);
	}

	public bool IsBlacklisted(IPackageIdentity package)
	{
		return CompatibilityData.BlackListedIds.Contains(package.Id)
			|| CompatibilityData.BlackListedNames.Contains(package.Name ?? string.Empty);
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

	public PackageData GetAutomatedReport(IPackageIdentity package)
	{
		var workshopInfo = package.GetWorkshopInfo();
		var info = new PackageData
		{
			Stability = package.IsCodeMod() ? PackageStability.NotReviewed : PackageStability.AssetNotReviewed,
			Id = package.Id,
			Name = package.Name,
			AuthorId = workshopInfo?.Author?.Id?.ToString() ?? string.Empty,
			FileName = package.GetLocalPackageIdentity()?.FilePath,
			Links = [],
			Tags = []
		};

		if (workshopInfo?.Requirements.Any() ?? false)
		{
			foreach (var grp in workshopInfo.Requirements.GroupBy(x => (x.IsDlc, x.IsOptional)))
			{
				if (grp.Key.IsDlc)
				{
					info.RequiredDLCs.AddRange(grp.Select(x => x.Id));
				}
				else
				{
					info.Interactions.Add(new PackageInteraction
					{
						Type = grp.Key.IsOptional ? InteractionType.OptionalPackages : InteractionType.RequiredPackages,
						Action = StatusAction.SubscribeToPackages,
						Packages = grp.ToArray(x => x.Id)
					});
				}
			}
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

		//if (workshopInfo?.Description is not null)
		//{
		//	var matches = _urlRegex.Matches(workshopInfo.Description);

		//	foreach (Match match in matches)
		//	{
		//		var type = match.Groups[2].Value.ToLower() switch
		//		{
		//			"youtube.com" or "youtu.be" => LinkType.YouTube,
		//			"github.com" => LinkType.Github,
		//			"discord.com" or "discord.gg" => LinkType.Discord,
		//			"crowdin.com" => LinkType.Crowdin,
		//			"buymeacoffee.com" or "patreon.com" or "ko-fi.com" or "paypal.com" => LinkType.Donation,
		//			_ => LinkType.Other
		//		};

		//		if (type is not LinkType.Other && !(workshopInfo?.Links.Any(x => x.Url?.Equals(match.Value, StringComparison.InvariantCultureIgnoreCase) ?? false) ?? false))
		//		{
		//			info.Links.Add(new PackageLink
		//			{
		//				Url = match.Value,
		//				Type = type,
		//			});
		//		}
		//	}
		//}

		return info;
	}

	public IIndexedPackageCompatibilityInfo TryGetPackageInfo(ulong id)
	{
		return CompatibilityData.Packages.TryGet(id);
	}

	public IEnumerable<ulong> GetPackagesKeys()
	{
		return CompatibilityData.Packages.Keys;
	}

	public Task<ReviewReply?> GetReviewStatus(IPackageIdentity package)
	{
		return _skyveApiUtil.GetReviewStatus(package.Id);
	}
}
