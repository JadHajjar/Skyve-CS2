using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.Systems;
using Skyve.Systems;

using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Paradox;
internal class PdxModRequirement : IPackageRequirement
{
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

	public bool IsDlc { get; }
	public bool IsOptional { get; }
	public ulong Id { get; }
	public string Name { get; }
	public string? Url { get; }

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		if (!IsDlc)
		{
			var info = this.GetWorkshopInfo();

			if (info is not null)
			{
				return info.GetThumbnail(imageService, out thumbnail, out thumbnailUrl);
			}
		}

		thumbnail = null;
		thumbnailUrl = null;
		return false;
	}
}
