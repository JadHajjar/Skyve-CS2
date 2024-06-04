using PDX.SDK.Contracts.Service.Mods.Models;

using System;

namespace Skyve.Domain.CS2.Paradox;

public class PdxForumPost : IModComment
{
	public PdxForumPost(ForumPost post)
	{
		Username = post.Username;
		UserId = post.UserId;
		UserTitle = post.UserTitle;
		Url = post.Url;
		PostId = post.PostId;
		Message = post.Message;
		Created = post.Created;
	}

	public string Username { get; set; }
	public int UserId { get; set; }
	public string UserTitle { get; set; }
	public string Url { get; set; }
	public int PostId { get; set; }
	public string Message { get; set; }
	public DateTime Created { get; set; }
}