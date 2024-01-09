using Extensions;

using Skyve.Domain;
using Skyve.Domain.Systems;

using System.Drawing;

namespace Skyve.Systems;
public class PdxModIdentityPackage : IPackage
{
	public PdxModIdentityPackage(IPackageIdentity identity)
	{
		Id = identity.Id;
		Name = identity.Name.IfEmpty(Id.ToString());
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
	}

	public PdxModIdentityPackage(ulong id)
	{
		Id = id;
		Name = id.ToString();
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
	}

	public PdxModIdentityPackage()
	{
		Name = string.Empty;
	}

	public ILocalPackageData? LocalData { get; }
	public bool IsCodeMod { get; }
	public bool IsLocal { get; }
	public ulong Id { get; }
	public string Name { get; }
	public string? Url { get; }
	public string? Version => this.GetWorkshopInfo()?.Version;

	public override string ToString()
	{
		return this.GetWorkshopInfo()?.Name ?? Name;
	}
}
