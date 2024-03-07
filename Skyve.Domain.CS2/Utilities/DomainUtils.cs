using Extensions;

using Skyve.Domain.Systems;

using System.Drawing;
using System.IO;

namespace Skyve.Domain.CS2.Utilities;
internal static class DomainUtils
{
	internal static Bitmap? GetThumbnail(IImageService imageService, string? thumbnailPath, string? thumbnailUrl, ulong id, string version)
	{
		if (CrossIO.FileExists(thumbnailPath))
		{
			return imageService.GetImage(thumbnailPath, true, $"{id}_{version}_{Path.GetExtension(thumbnailPath)}", isFilePath: true).Result;
		}

		var thumbnail = imageService.GetImage(thumbnailUrl, true, $"{id}_{version}{Path.GetExtension(thumbnailUrl)}").Result;

		if (thumbnail is not null)
		{
			return thumbnail;
		}

		var imageName = imageService.FindImage($"{id}_*{Path.GetExtension(thumbnailUrl)}");

		if (!string.IsNullOrEmpty(imageName))
		{
			return imageService.GetImage(imageName, true, imageName).Result;
		}

		return null;
	}
}
