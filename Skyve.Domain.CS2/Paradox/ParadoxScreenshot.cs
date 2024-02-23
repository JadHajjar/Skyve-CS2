using Extensions;

using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Domain.CS2.Paradox;
public class ParadoxScreenshot : IThumbnailObject
{
    public string Url { get; set; }
    public string Path { get; set; }
    public ulong ModId { get; set; }
    public string Version { get; set; }

	public ParadoxScreenshot(string path, ulong modId, string version, bool isLocal)
	{
		Url = isLocal ? string.Empty : path;
		Path = !isLocal ? string.Empty : path;
		ModId = modId;
		Version = version;
	}

    public ParadoxScreenshot()
	{
		Url = string.Empty;
		Path = string.Empty;
		Version = string.Empty;
	}

    public bool GetThumbnail(IImageService imageService, out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		thumbnailUrl = Url;
		thumbnail = !CrossIO.FileExists(Path)
			? imageService.GetImage(Url, true, $"{ModId}_{Version}_{System.IO.Path.GetFileName(Url)}").Result
			: imageService.GetImage(Path, true, $"{ModId}_{Version}_{System.IO.Path.GetExtension(Path)}", isFilePath: true).Result;

		return true;
	}
}
