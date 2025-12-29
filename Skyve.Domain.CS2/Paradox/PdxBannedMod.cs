using PDX.SDK.Contracts.Service.Mods.Models;

namespace Skyve.Domain.CS2.Paradox;

public class PdxBannedMod : PdxModDetails
{
	public PdxBannedMod(string id)
	{
		Id = ulong.Parse(id);
		Name = id.ToString();
		IsBanned = true;
	}
}
