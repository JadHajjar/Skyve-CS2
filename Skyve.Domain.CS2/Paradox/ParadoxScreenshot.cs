using Extensions;

using Skyve.Domain.Systems;

using System.Drawing;

namespace Skyve.Domain.CS2.Paradox;

public class ParadoxScreenshot : IThumbnailObject
{
	public string Url { get; set; }
	public string Path { get; set; }
	public string ModId { get; set; }
	public string Version { get; set; }

	public ParadoxScreenshot(string path, string modId, string version, bool isLocal)
	{
		Url = isLocal ? string.Empty : path;
		Path = !isLocal ? string.Empty : path;
		ModId = modId;
		Version = version;
	}

	public ParadoxScreenshot()
	{
		ModId = string.Empty;
		Url = string.Empty;
		Path = string.Empty;
		Version = string.Empty;
	}

	public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		if (CrossIO.FileExists(Path))
		{
			thumbnailUrl = Path;
			thumbnail = imageService.GetImage(Path, true, $"{ModId}_{Version}_{System.IO.Path.GetFileName(Path)}", isFilePath: true).Result;
		}
		else
		{
			thumbnailUrl = Url;
			thumbnail = imageService.GetImage(Url, true, $"{ModId}_{Version}_{System.IO.Path.GetFileName(Url)}").Result;
		}

		return true;
	}
}
