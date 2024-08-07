﻿using Extensions;

using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Skyve.Domain.CS2.Paradox;
public class PdxUser : IAuthor, ITimestamped, IEquatable<IUser?>
{
	public object? Id { get; set; }
	public string Name { get; set; }
	public string? ProfileUrl => $"https://mods.paradoxplaza.com/authors/{Id}";
	public string? AvatarUrl { get; set; }
	public List<ParadoxLink> Links { get; set; }
	public string Bio { get; set; }
	public int FollowerCount { get; set; }
	public DateTime Timestamp { get; set; } = DateTime.Now;
	List<ILink> IAuthor.Links => Links.ToList(x => (ILink)x);

	[Obsolete("JsonOnly", true)]
	public PdxUser()
	{
		Id = Name = Bio = string.Empty;
		Links = [];
	}

	public PdxUser(string authorId)
	{
		Id = Name = authorId;
		Bio = string.Empty;
		Links = [];
	}

	public PdxUser(ModCreator author)
	{
		Id = Name = author.Username;
		AvatarUrl = author.Avatar.Url;
		Bio = Markdig.Markdown.ToPlainText(author.Bio);
		FollowerCount = author.FollowerCount;
		Links = author.ExternalLinks.ToList(x => new ParadoxLink(x));
	}

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = AvatarUrl;
		thumbnail = DomainUtils.GetThumbnail(imageService, null, AvatarUrl, $"User_{Id?.ToString() ?? Name}".EscapeFileName());

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

	public static bool operator ==(PdxUser? left, IUser? right)
	{
		return left?.Id?.ToString() == right?.Id?.ToString();
	}

	public static bool operator !=(PdxUser? left, IUser? right)
	{
		return !(left == right);
	}
}
