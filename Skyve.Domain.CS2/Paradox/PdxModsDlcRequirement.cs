using Skyve.Domain.CS2.Steam;
using Skyve.Domain.Systems;

using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Paradox;

public class PdxModsDlcRequirement : IPackageRequirement, IDlcInfo, IThumbnailObject
{
	public PdxModsDlcRequirement()
	{
		Name = string.Empty;
		Description = string.Empty;
	}

	public PdxModsDlcRequirement(IDlcInfo x)
	{
		Id = x.Id;
		Url = x.Url;
		Name = x.Name;
		IsDlc = true;
		IsFree = x.IsFree;
		ExpectedRelease = x.ExpectedRelease;
		ReleaseDate = x.ReleaseDate;
		Description = x.Description;
		Price = x.Price;
		Discount = x.Discount;
		Creators = x.Creators;
	}

	public bool IsDlc { get; set; }
	public bool IsOptional { get; set; }
	public ulong Id { get; set; }
	public string Name { get; set; }
	public string? Url { get; set; }
	public string? Version { get; set; }
	public DateTime ReleaseDate { get; }
	public string Description { get; }
	public string? Price { get; set; }
	public float Discount { get; set; }
	public string[]? Creators { get; set; }
	public bool IsFree { get; set; }
	public string? ExpectedRelease { get; set; }

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = Id > 10 ? $"https://cdn.akamai.steamstatic.com/steam/apps/{Id}/header.jpg" : null;
		thumbnail = thumbnailUrl is null or "" ? null : imageService.GetImage(thumbnailUrl, true, $"Dlc_{Id}.png", false).Result;

		thumbnail ??= Id == 2427731 ? SteamDlc.Cities2Landmark : SteamDlc.Cities2Dlc;

		return true;
	}
}

