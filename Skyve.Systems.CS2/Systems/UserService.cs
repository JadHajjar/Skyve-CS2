using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Domain.Api;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Skyve.Systems.CS2.Systems;
internal class UserService : IUserService
{
	private string? loggedInUser;
	private Dictionary<string, IKnownUser> knownUsers = [];

	public IKnownUser User { get; private set; }

	public event Action? UserInfoUpdated;

	public UserService()
	{
		User = new User();
	}

	public IKnownUser TryGetAuthor(string? id)
	{
		return id is null or "" ? new User { Id = id } : knownUsers.TryGetValue(id, out var author) ? author : new User { Id = id };
	}

	public bool IsUserVerified(IUser author)
	{
		return TryGetAuthor(User.Id?.ToString())?.Verified ?? false;
	}

	internal void SetKnownUsers(IKnownUser[] users)
	{
		knownUsers = users.ToDictionary(x => x.Id!.ToString());
		User = TryGetAuthor(loggedInUser);

		UserInfoUpdated?.Invoke();
	}

	internal void SetLoggedInUser(string? displayName)
	{
		loggedInUser = displayName;
		User = TryGetAuthor(loggedInUser);

		UserInfoUpdated?.Invoke();
	}
}
