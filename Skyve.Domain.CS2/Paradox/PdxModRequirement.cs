using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.Systems;
using Skyve.Systems;

using System.Drawing;

namespace Skyve.Domain.CS2.Paradox;
public class PdxModRequirement : IPackageRequirement
{
    public PdxModRequirement()
    {
		Name = string.Empty;
	}

	public PdxModRequirement(ModDependency x)
	{
		if (x.Id.HasValue)
		{
			Id = (ulong)x.Id.Value;
			Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
		}

		Name = x.DisplayName;
		IsDlc = x.Type is DependencyType.Dlc;
		IsOptional = x.Type is DependencyType.Unknown;
	}

	public bool IsDlc { get; set; }
	public bool IsOptional { get; set; }
	public ulong Id { get; set; }
	public string Name { get; set; }
	public string? Url { get; set; }
}
