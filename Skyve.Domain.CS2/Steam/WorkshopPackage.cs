using Skyve.Domain.Systems;
using Skyve.Systems;

using System.Drawing;

namespace Skyve.Domain.CS2.Steam;
public class WorkshopPackage : IPackage
{
	private readonly IWorkshopInfo? _info;

	public WorkshopPackage(ulong id)
	{
		Id = id;
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
	}

	public WorkshopPackage(IWorkshopInfo info) : this(info.Id)
	{
		_info = info;
	}

	public bool IsCodeMod => WorkshopInfo?.IsMod ?? false;
	public bool IsLocal { get; }
	public ILocalPackageData? LocalData => ServiceCenter.Get<IPackageManager>().GetPackageById(this)?.LocalData;
	public IWorkshopInfo? WorkshopInfo => _info ?? this.GetWorkshopInfo();
	public ulong Id { get; }
	public string Name => WorkshopInfo?.Name ?? ServiceCenter.Get<ILocale>().Get("UnknownPackage");
	public string? Url { get; }

	public bool GetThumbnail(out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		var info = WorkshopInfo;

		if (info is not null)
		{
			return info.GetThumbnail(out thumbnail, out thumbnailUrl);
		}

		thumbnail = null;
		thumbnailUrl = null;
		return false;
	}
}
