using Skyve.Domain;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Skyve.Systems.CS2.Domain.Api;
public class User : IKnownUser, IEquatable<IUser?>
{
	public object? Id { get; set; }
	public string Name { get; set; }
	public bool Retired { get; set; }
	public bool Verified { get; set; }
	public bool Malicious { get; set; }
	public bool Manager { get; set; }
	public string? AvatarUrl { get; }
	public string? ProfileUrl => $"https://mods.paradoxplaza.com/authors/{Id}";

	public User()
	{
		Name = string.Empty;
	}

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
		return 2108858624 + EqualityComparer<object?>.Default.GetHashCode(Id);
	}

	public bool Equals(IUser? other)
	{
		return other is not null && Id == other.Id;
	}

	public static bool operator ==(User? left, User? right)
	{
		return left?.Id == right?.Id;
	}

	public static bool operator !=(User? left, User? right)
	{
		return !(left == right);
	}
}
