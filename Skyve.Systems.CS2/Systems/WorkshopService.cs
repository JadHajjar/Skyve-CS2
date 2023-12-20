using Extensions;

using PDX.SDK;
using PDX.SDK.Contracts;
using PDX.SDK.Contracts.Configuration;
using PDX.SDK.Contracts.Enums;

using Skyve.Domain;
using Skyve.Domain.CS2.ParadoxMods;
using Skyve.Domain.CS2.Steam;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using PdxPlatform = PDX.SDK.Contracts.Enums.Platform;
using Platform = Extensions.Platform;

namespace Skyve.Systems.CS2.Systems;
internal class WorkshopService : IWorkshopService
{
	private readonly IContext _context;

	public WorkshopService(ILogger logger, ILocationManager locationManager)
	{
		var pdxSdkPath = CrossIO.Combine(locationManager.AppDataPath, ".pdxsdk");
		var platform = CrossIO.CurrentPlatform switch { Platform.MacOSX => PdxPlatform.MacOS, Platform.Linux => PdxPlatform.Linux, _ => PdxPlatform.Windows };

		if (SteamUtil.GetLoggedInSteamId() != 0)
		{
			pdxSdkPath = CrossIO.Combine(pdxSdkPath, SteamUtil.GetLoggedInSteamId().ToString());
		}

		var config = new Config
		{
			Language = Language.en,
			//config.GameVersion = m_Configuration.gameVersion;
			Logger = logger as CS2LoggerSystem,
			LogLevel = LogLevel.L0_Info,
			DiskIORoot = pdxSdkPath,
			Environment = BackendEnvironment.Live,
			TelemetryDebugEnabled = false,
			Ecosystem = Ecosystem.Steam,
			UserIdType = "steam"
		};

		config.Mods.RootPath = CrossIO.Combine(locationManager.AppDataPath, ".cache", "Mods");

		if (Enum.TryParse<Language>(LocaleHelper.CurrentCulture.IetfLanguageTag.Substring(0, 2).ToLower(), out var result))
		{
			config.Language = result;
		}

		_context = Context.Create(
			platform: platform,
			@namespace: "cities_skylines_2",
			config: config).Result;
	}

	public void CleanDownload(List<ILocalPackageWithContents> packages)
	{
		PackageWatcher.Pause();
		foreach (var item in packages)
		{
			try
			{
				CrossIO.DeleteFolder(item.Folder);
			}
			catch (Exception ex)
			{
				ServiceCenter.Get<ILogger>().Exception(ex, $"Failed to delete the folder '{item.Folder}'");
			}
		}

		PackageWatcher.Resume();

		SteamUtil.Download(packages);
	}

	public void ClearCache()
	{
		SteamUtil.ClearCache();
	}

	public IEnumerable<IWorkshopInfo> GetAllPackages()
	{
		return SteamUtil.Packages;
	}

	public IWorkshopInfo? GetInfo(IPackageIdentity identity)
	{
		return SteamUtil.GetItem(identity?.Id ?? 0);
	}

	public async Task<IWorkshopInfo?> GetInfoAsync(IPackageIdentity identity)
	{
		var result = await _context.Mods.GetDetails((int)identity.Id);

		if (result?.Mod is not null)
		{
			return new PdxModDetails(result.Mod);
		}

		return null;
	}

	public IPackage GetPackage(IPackageIdentity identity)
	{
		var info = identity is IWorkshopInfo inf ? inf : GetInfo(identity);

		if (info is not null)
		{
			return new WorkshopPackage(info);
		}

		return new GenericWorkshopPackage(identity);
	}

	public async Task<IPackage> GetPackageAsync(IPackageIdentity identity)
	{
		var info = await GetInfoAsync(identity);

		if (info is not null)
		{
			return new WorkshopPackage(info);
		}

		return new GenericWorkshopPackage(identity);
	}

	public IUser? GetUser(object userId)
	{
		return SteamUtil.GetUser(ulong.TryParse(userId?.ToString() ?? string.Empty, out var id) ? id : 0);
	}

	public async Task<IEnumerable<IWorkshopInfo>> GetWorkshopItemsByUserAsync(object userId)
	{
		return (await SteamUtil.GetWorkshopItemsByUserAsync(ulong.TryParse(userId?.ToString() ?? string.Empty, out var id) ? id : 0, true)).Values;
	}

	public async Task<IEnumerable<IWorkshopInfo>> QueryFilesAsync(PackageSorting sorting, string? query = null, string[]? requiredTags = null, string[]? excludedTags = null, (DateTime, DateTime)? dateRange = null, bool all = false)
	{
		var steamSorting = sorting switch
		{
			PackageSorting.UpdateTime => SteamQueryOrder.RankedByLastUpdatedDate,
			_ => SteamQueryOrder.RankedByTrend
		};

		return (await SteamUtil.QueryFilesAsync(steamSorting, query, requiredTags, excludedTags, dateRange, all)).Values;
	}
}
