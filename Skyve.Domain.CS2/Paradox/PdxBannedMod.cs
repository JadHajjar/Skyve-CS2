using PDX.SDK.Contracts.Service.Mods.Models;

namespace Skyve.Domain.CS2.Paradox;

public class PdxBannedMod : PdxModDetails
{
	public PdxBannedMod(string id) : base(new ModDetails { Id = id, Name = id.ToString() })
	{
		IsBanned = true;
	}

	public bool HasComments()
	{
		return false;
	}
}
