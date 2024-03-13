using Skyve.Domain;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Skyve.Systems.CS2.Domain.Api;
public class User : IKnownUser, IEquatable<IUser?>
{
	public object? Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public bool Retired { get; set; }
	public bool Verified { get; set; }
	public bool Malicious { get; set; }
	public bool Manager { get; set; }
	public string? AvatarUrl { get; }
	public string? ProfileUrl => $"https://mods.paradoxplaza.com/authors/{Id}";

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = null;
		thumbnailUrl = null;
		return true;
	}

	public override bool Equals(object? obj)
	{
		return Equals(obj as IUser);
	}

	public override int GetHashCode()
	{
		return EqualityComparer<string?>.Default.GetHashCode(Id?.ToString());
	}

	public bool Equals(IUser? other)
	{
		return other is not null && Id?.ToString() == other.Id?.ToString();
	}

	public static bool operator ==(User? left, IUser? right)
	{
		return left?.Id?.ToString() == right?.Id?.ToString();
	}

	public static bool operator !=(User? left, IUser? right)
	{
		return !(left == right);
	}
}
