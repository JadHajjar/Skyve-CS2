using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Content;
public class SharedPlayset : IOnlinePlayset
{
	public int Id { get; }
	public bool Public { get; set; }
	public int Downloads { get; }
	public IUser? Author { get; }

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		throw new NotImplementedException();
	}
}
