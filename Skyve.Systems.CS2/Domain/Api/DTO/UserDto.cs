using Skyve.Domain;
using Skyve.Systems.CS2.Domain.Api;

using SkyveApi.Domain.CS2;

namespace Skyve.Systems.CS2.Domain.DTO;

internal class UserDto : IDTO<UserData, User>
{
	public User Convert(UserData? data)
	{
		if (data is null)
		{
			return new();
		}

		return new User
		{
			Id = data.Id,
			Name = data.Name ?? data.Id?.ToString() ?? string.Empty,
			Verified = data.Verified,
			Retired = data.Retired,
			Malicious = data.Malicious,
			Manager = data.Manager,
		};
	}
}