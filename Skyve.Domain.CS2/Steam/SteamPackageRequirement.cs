using Skyve.Domain.Systems;
using Skyve.Systems;

using System.Drawing;

namespace Skyve.Domain.CS2.Steam;

internal class SteamPackageRequirement : IPackageRequirement
{

	public SteamPackageRequirement(ulong id, bool optional)
	{
		Id = id;
		IsOptional = optional;
		Url = $"https://steamcommunity.com/workshop/filedetails/?id={Id}";
	}

	public bool IsOptional { get; }
	public ulong Id { get; }
	public string? Url { get; }
	public string Name => this.GetWorkshopInfo()?.Name ?? Id.ToString();
	public bool IsDlc { get; }

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		var info = this.GetWorkshopInfo();

		if (info is not null)
		{
			return info.GetThumbnail(imageService, out thumbnail, out thumbnailUrl);
		}

		thumbnail = null;
		thumbnailUrl = null;
		return false;
	}
}