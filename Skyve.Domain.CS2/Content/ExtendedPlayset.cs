using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Drawing;
using System.IO;

namespace Skyve.Domain.CS2.Content;
public class ExtendedPlayset : ICustomPlayset
{
	private Bitmap? _banner;
	private byte[]? _bannerBytes;

	public ExtendedPlayset()
	{

	}

	public ExtendedPlayset(IPlayset playset)
	{
		Playset = playset;
		Id = playset.Id;
	}

	public IPlayset? Playset { get; set; }
	public int Id { get; set; }
	public DateTime DateUsed { get; set; }
	public DateTime DateCreated { get; set; }
	public PackageUsage Usage { get; set; }
	public Color? Color { get; set; }
	public bool IsFavorite { get; set; }
	public IOnlinePlayset? OnlineInfo { get; set; }
	public byte[]? BannerBytes
	{
		get => _bannerBytes;
		set
		{
			_bannerBytes = value;
			_banner?.Dispose();
			_banner = null;
		}
	}

	bool IThumbnailObject.GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = null;
		thumbnailUrl = null;

		if (_banner is not null)
		{
			thumbnail = _banner;
		}
		else if (BannerBytes is not null && BannerBytes.Length != 0)
		{
			using var ms = new MemoryStream(BannerBytes);

			thumbnail = _banner = new Bitmap(ms);

		}
		else if (Playset is not null)
		{
			return Playset.GetThumbnail(imageService, out thumbnail, out thumbnailUrl);
		}

		return true;
	}
}
