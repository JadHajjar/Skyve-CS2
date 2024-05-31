using Extensions;

using Skyve.Domain.Systems;

using SlickControls;

using System.Drawing;
using System.IO;

namespace Skyve.Domain.CS2.Utilities;
internal static class DomainUtils
{
	internal static Bitmap? GetThumbnail(IImageService imageService, string? thumbnailPath, string? thumbnailUrl, ulong id, string version)
	{
		var size = UI.Scale(new Size(200, 200), UI.FontScale);

		if (CrossIO.FileExists(thumbnailPath))
		{
			return imageService.GetImage(thumbnailPath, true, $"{id}_{version}{Path.GetExtension(thumbnailPath)}", isFilePath: true, downscaleTo: size).Result;
		}

		var thumbnail = imageService.GetImage(thumbnailUrl, true, $"{id}_{version}{Path.GetExtension(thumbnailUrl)}", downscaleTo: size).Result;

		if (thumbnail is not null)
		{
			return thumbnail;
		}

		var imageName = imageService.FindImage($"{id}_*{Path.GetExtension(thumbnailUrl)}");

		if (!string.IsNullOrEmpty(imageName))
		{
			return imageService.GetImage(imageName, true, imageName, downscaleTo: size).Result;
		}

		return null;
	}

	internal static Bitmap? GetThumbnail(IImageService imageService, string? thumbnailPath, string? thumbnailUrl, string id)
	{
		var size = UI.Scale(new Size(200, 200), UI.FontScale);

		if (CrossIO.FileExists(thumbnailPath))
		{
			return imageService.GetImage(thumbnailPath, true, $"{id}{Path.GetExtension(thumbnailPath)}", isFilePath: true, downscaleTo: size).Result;
		}

		var thumbnail = imageService.GetImage(thumbnailUrl, true, $"{id}{Path.GetExtension(thumbnailUrl)}", downscaleTo: size).Result;

		if (thumbnail is not null)
		{
			return thumbnail;
		}

		return null;
	}
}
