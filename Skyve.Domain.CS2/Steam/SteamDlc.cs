using Skyve.Domain.Systems;

using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Skyve.Domain.CS2.Steam;
public class SteamDlc : IDlcInfo, IThumbnailObject
{
	public static readonly Bitmap Cities2Dlc = GetEmbeddedBitmap("Skyve.Domain.CS2.Steam.Cities2Dlc.png");
	public static readonly Bitmap Cities2Landmark = GetEmbeddedBitmap("Skyve.Domain.CS2.Steam.Cities2Landmark.png");

	public ulong Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public DateTime ReleaseDate { get; set; }
	public string? ExpectedRelease { get; set; }
	public bool IsFree { get; set; }
	public string? Price { get; set; }
	public string? OriginalPrice { get; set; }
	public string[]? Creators { get; set; }
	public float Discount { get; set; }
	public DateTime Timestamp { get; set; }

	string? IDlcInfo.Url => Id > 10 ? $"https://store.steampowered.com/app/{Id}" : null;

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = Id > 10 ? $"https://cdn.akamai.steamstatic.com/steam/apps/{Id}/header.jpg" : null;
		thumbnail = thumbnailUrl is null or "" ? null : imageService.GetImage(thumbnailUrl, true, $"Dlc_{Id}.png", false).Result;

		thumbnail ??= Id == 2427731 ? Cities2Landmark : Cities2Dlc;

		return true;
	}

	private static Bitmap GetEmbeddedBitmap(string resourceName)
	{
		var assembly = typeof(SteamDlc).Assembly;

		using var stream = assembly.GetManifestResourceStream(resourceName);

		return stream == null ? throw new FileNotFoundException("Resource not found: " + resourceName) : new Bitmap(stream);
	}
}
