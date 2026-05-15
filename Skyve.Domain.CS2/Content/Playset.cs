using PDX.SDK.Contracts.Service.Mods.Results;

using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Skyve.Domain.CS2.Content;

public class Playset : IPlayset
{
	public Playset()
	{
	}

	public Playset(PDX.SDK.Contracts.Service.Mods.Models.Playset playset)
	{
		Id = playset.Id;
		Name = playset.Name;
		ModCount = playset.ModsCount;
		ModSize = playset.ModsSize;
		DateUpdated = playset.Updated ?? DateTime.MinValue;
		ThumbnailUrl = playset.DisplayImagePath;
		Ownership = (PlaysetOwnership)(int)playset.Ownership;
		OnlineInfo = playset.State == PDX.SDK.Contracts.Service.Mods.Enums.PlaysetState.Public ? new SharedPlayset(playset) : null;
	}

	public Playset(ICreatePlaysetResult createdPlayset)
	{
		Id = createdPlayset.Id;
		Name = createdPlayset.Name;
	}

	public string? Id { get; }
	public string? Name { get; set; }
	public DateTime DateUpdated { get; }
	public int ModCount { get; set; }
	public ulong ModSize { get; set; }
	public string? ThumbnailUrl { get; }
	public PlaysetOwnership Ownership { get; }
	public IOnlinePlayset? OnlineInfo { get; }

	public override bool Equals(object? obj)
	{
		return obj is IPlayset playset &&
			   Id == playset.Id;
	}

	public override int GetHashCode()
	{
		return 2108858624 + EqualityComparer<string?>.Default.GetHashCode(Id);
	}

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		if (Id is null)
		{
			thumbnail = null;
			thumbnailUrl = null;
			return false;
		}

		thumbnailUrl = ThumbnailUrl;
		thumbnail = DomainUtils.GetThumbnail(imageService, null, thumbnailUrl, Id, "Playset");

		return true;
	}

	public static bool operator ==(Playset? left, Playset? right)
	{
		return left?.Id == right?.Id;
	}

	public static bool operator !=(Playset? left, Playset? right)
	{
		return !(left?.Id == right?.Id);
	}
}