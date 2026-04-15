using Extensions;

using Skyve.Domain;

namespace Skyve.Systems;
public class PdxModIdentityPackage : IPackage
{
	public PdxModIdentityPackage(IPackageIdentity identity)
	{
		Id = identity.Id;
		Name = identity.Name.IfEmpty(Id.ToString());
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
		Version = identity.Version;
	}

	public PdxModIdentityPackage(string id)
	{
		Id = id;
		Name = id;
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
	}

	public PdxModIdentityPackage()
	{
		Id = Name = string.Empty;
	}

	public ILocalPackageData? LocalData { get; }
	public bool IsCodeMod { get; }
	public bool IsLocal { get; }
	public bool IsBuiltIn { get; }
	public string Source { get; } = Defaults.WORKSHOP_SOURCE;
	public string Id { get; }
	public string Name { get; }
	public string? Url { get; }
	public string? Version { get; set; }
	public string? VersionName { get; }

	public override string ToString()
	{
		return this.GetWorkshopInfo()?.Name ?? Name;
	}
}
