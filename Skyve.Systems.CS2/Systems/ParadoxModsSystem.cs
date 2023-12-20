using Skyve.Domain.CS2.ParadoxMods;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Systems;
internal class ParadoxModsSystem
{
	private const string BASE_API_URL = "https://api.paradox-interactive.com";
	private const string BASE_CONTENT_URL = "https://modscontent.paradox-interactive.com";

	public async Task<ModInfo?> GetModInfo(string modId)
	{
		return (await ApiUtil.Get<ModInfoResponseWrapper>(BASE_API_URL + "/mods"
			, (nameof(modId), modId)
			, ("os", "Windows")))?.modDetail;
	}

	public async Task<ModInfo[]?> GetMods(ListingQueryParams? queryParams = null)
	{
		return (await ApiUtil.Post<ListingQueryParams, ListingResponseWrapper>(BASE_API_URL + "/mods", queryParams ?? new()))?.mods;
	}

	public async Task<ModDetail[]?> GetModRequirements(params string[] modIds)
	{
		return (await ApiUtil.Post<string[], ModDetailsWrapper>(BASE_API_URL + "/mods/dependenciesDetails", modIds))?.mods;
	}

	public async Task<ModInfo[]?> GetInstalledMods()
	{
		return (await ApiUtil.Get<InstalledModsWrapper>(BASE_API_URL + "/mods/installed"
			, ("game", "cities_skylines_2")
			, ("os", "Windows")))?.mods;
	}

	public async Task<byte[]?> DownloadMod(ModInfo mod, int version)
	{
		return await ApiUtil.Get<byte[]>($"{BASE_CONTENT_URL}/cities_skylines_2/{mod.name}/content/sources/{mod.name}_{version}");
	}

	public async Task<ModVersion[]?> GetModVersions(string modId)
	{
		return (await ApiUtil.Get<ModVersionsWrapper>(BASE_API_URL + "/mods/versions"
			, (nameof(modId), modId)))?.modVersions;
	}

	public async Task<Set[]?> GetPlaysets(string modId)
	{
		return (await ApiUtil.Get<PlaysetWrapper>(BASE_API_URL + "/mods/sets/list/cities_skylines_2"
			, (nameof(modId), modId)))?.sets;
	}

	public async Task<bool> AddModToPlayset(string modId, int version, int playsetId)
	{
		return (await ApiUtil.Post<PlaysetWrapper>(BASE_API_URL + "/mods/installed"
			, (nameof(modId), modId)
			, (nameof(version), version)
			, (nameof(playsetId), playsetId)))
	}
}
