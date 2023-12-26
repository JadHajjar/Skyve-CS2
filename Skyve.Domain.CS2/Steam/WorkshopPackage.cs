using Skyve.Domain.CS2.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems;

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Skyve.Domain.CS2.Steam;
public class WorkshopPackage : IPackage
{
	private readonly IWorkshopInfo? _info;

	public WorkshopPackage(ulong id)
	{
		Id = id;
		Url = $"https://steamcommunity.com/workshop/filedetails/?id={Id}";
	}

	public WorkshopPackage(IWorkshopInfo info) : this(info.Id)
	{
		_info = info;
	}

	public ulong Id { get; }
	public string Url { get; }
	public bool IsLocal { get; }
	public bool IsBuiltIn { get; }
	public string Name => GetInfo()?.Name ?? ServiceCenter.Get<ILocale>().Get("UnknownPackage");
	public bool IsCodeMod => GetInfo()?.IsMod ?? false;
	public ILocalPackageData? LocalParentPackage => ServiceCenter.Get<IPackageManager>().GetPackageById(this);
	public ILocalPackageData? LocalPackage => ServiceCenter.Get<IPackageManager>().GetPackageById(this);
	public IEnumerable<IPackageRequirement> Requirements => GetInfo()?.Requirements ?? Enumerable.Empty<IPackageRequirement>();
	public IEnumerable<ITag> Tags => GetInfo()?.Tags.Select(x => (ITag)new TagItem(TagSource.Workshop, x.Value)) ?? Enumerable.Empty<ITag>();

	public bool GetThumbnail(out Bitmap? thumbnail, out string? thumbnailUrl)
	{
		var info = GetInfo();

		if (info is not null)
		{
			return info.GetThumbnail(out thumbnail, out thumbnailUrl);
		}

		thumbnail = null;
		thumbnailUrl = null;
		return false;
	}

	private IWorkshopInfo? GetInfo()
	{
		return _info ?? this.GetWorkshopInfo();
	}
}
