using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;

namespace Skyve.Domain.CS2.Paradox;

public class PdxModsRequirement : IPackageRequirement
{
	public PdxModsRequirement()
	{
		Id = Name = string.Empty;
	}

	public PdxModsRequirement(ModDependency x)
	{
		if (string.IsNullOrEmpty(x.Id))
		{
			Id = x.Id;
			Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
		}
		else
		{ 
			Id = x.DisplayName;
		}

		Name = x.DisplayName;
		IsDlc = x.Type is DependencyType.Dlc;
		IsOptional = x.Type is DependencyType.Unknown;
		Version = x.Version;
	}

	public bool IsDlc { get; set; }
	public bool IsOptional { get; set; }
	public string Source { get; } = Defaults.WORKSHOP_SOURCE;
	public string Id { get; set; }
	public string Name { get; set; }
	public string? Url { get; set; }
	public string? Version { get; set; }
}