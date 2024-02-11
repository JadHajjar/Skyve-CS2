using Skyve.Domain;
using Skyve.Domain.Systems;

using SkyveApi.Domain.CS2;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Domain;
public class User : IKnownUser
{
    public User()
    {
        
    }

    public User(Author x)
	{
		Id = x.Id;
		Retired = x.Retired;
		Verified = x.Verified;
		Malicious = x.Malicious;
		Name = x.Name ?? Locale.UnknownUser;
		Manager = false;
	}

	public bool Retired { get; set; }
	public bool Verified { get; set; }
	public bool Malicious { get; set; }
	public bool Manager { get; set; }
	public string Name { get; set; } = "";
	public string ProfileUrl { get; set; } = "";
	public string AvatarUrl { get; set; } = "";
	public object? Id { get; set; }

	public override bool Equals(object? obj)
	{
		return obj is IUser user && (Id?.Equals(user.Id) ?? false);
	}

	public override int GetHashCode()
	{
		return 2139390487 + Id?.GetHashCode() ?? 0;
	}

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = null;
		thumbnailUrl = null;
		return false;
	}
}
