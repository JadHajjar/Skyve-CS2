using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Skyve.Domain.CS2.Paradox;
public class PdxUser(string authorId) : IUser, IEquatable<IUser?>
{
	public string Name { get; } = authorId;
	public string? ProfileUrl => $"https://mods.paradoxplaza.com/authors/{Id}";
	public string? AvatarUrl { get; }
	public object? Id { get; } = authorId;

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = null;
		thumbnailUrl = null;
		return false;
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

	public static bool operator ==(PdxUser? left, IUser? right)
	{
		return left?.Id?.ToString() == right?.Id?.ToString();
	}

	public static bool operator !=(PdxUser? left, IUser? right)
	{
		return !(left == right);
	}
}
