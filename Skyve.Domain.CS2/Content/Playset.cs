using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Content;
public class Playset : IPlayset
{
	public Playset()
	{

	}

	public Playset(PDX.SDK.Contracts.Service.Mods.Models.Playset playset)
	{
		Id = playset.PlaysetId;
		Name = playset.Name;
		DateUpdated = playset.Updated ?? DateTime.MinValue;
		ModCount = playset.ModsCount;
		ModSize = playset.ModsSize;
		ThumbnailUrl = playset.DisplayImagePath;
	}

	public Playset(PDX.SDK.Contracts.Service.Mods.Result.CreatePlaysetResult createdPlayset)
	{
		Id = createdPlayset.PlaysetId;
		Name = createdPlayset.Name;
	}

	public int Id { get; }
	public string? Name { get; set; }
	public DateTime DateUpdated { get; }
	public int ModCount { get; }
	public ulong ModSize { get; }
	public string? ThumbnailUrl { get; }
	public bool Temporary { get; }

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = ThumbnailUrl;
		thumbnail = DomainUtils.GetThumbnail(imageService, null, thumbnailUrl, (ulong)Id, "Playset");

		return true;
	}
}