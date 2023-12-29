using System.Drawing;

namespace Skyve.Domain.CS2.Paradox;
internal class PdxUser(string authorId) : IUser
{
	public string Name { get; } = authorId;
	public string? ProfileUrl { get; }
	public string? AvatarUrl { get; }
	public object? Id { get; } = authorId;

	public bool GetThumbnail(out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnail = null;
		thumbnailUrl = null;
		return false;
	}
}
