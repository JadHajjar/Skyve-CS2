using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;

namespace Skyve.Domain.CS2.Paradox;
public class PdxModsRequirement : IPackageRequirement
{
	public PdxModsRequirement()
	{
		Name = string.Empty;
	}

	public PdxModsRequirement(ModDependency x)
	{
		if (x.Id.HasValue)
		{
			Id = (ulong)x.Id.Value;
			Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
		}

		Name = x.DisplayName;
		IsDlc = x.Type is DependencyType.Dlc;
		IsOptional = x.Type is DependencyType.Unknown;
		Version = x.Version;
	}

	public bool IsDlc { get; set; }
	public bool IsOptional { get; set; }
	public ulong Id { get; set; }
	public string Name { get; set; }
	public string? Url { get; set; }
	public string? Version { get; set; }
}