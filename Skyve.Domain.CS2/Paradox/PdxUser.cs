using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Skyve.Domain.CS2.Paradox;
public class PdxUser(string authorId) : IUser, IEquatable<PdxUser?>
{
	public string Name { get; } = authorId;
	public string? ProfileUrl => $"https://mods.paradoxplaza.com/authors/{Id}";
	public string? AvatarUrl { get; }
	public object? Id { get; } = authorId;

	public override bool Equals(object? obj)
	{
		return Equals(obj as PdxUser);
	}

	public bool Equals(PdxUser? other)
	{
		return other is not null &&
			   EqualityComparer<object?>.Default.Equals(Id, other.Id);
	}

	public override int GetHashCode()
	{
		return 2108858624 + EqualityComparer<object?>.Default.GetHashCode(Id);
	}

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = null;
		thumbnailUrl = null;
		return false;
	}

	public static bool operator ==(PdxUser left, PdxUser right)
	{
		return EqualityComparer<PdxUser>.Default.Equals(left, right);
	}

	public static bool operator !=(PdxUser left, PdxUser right)
	{
		return !(left == right);
	}
}
