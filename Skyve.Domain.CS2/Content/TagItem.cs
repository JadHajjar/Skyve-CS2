using Extensions;

using Skyve.Domain.CS2.Enums;

using System.Collections.Generic;

namespace Skyve.Domain.CS2.Content;
public class TagItem(TagSource source, string key, string value) : ITag
{
	public TagSource Source { get; set; } = source;
	public string Key { get; set; } = key;
	public string Value { get; set; } = value;
	public string Icon => Source switch { TagSource.ID => "Paradox", TagSource.Custom => "Search", _ => "Tag" };
	public bool IsCustom => Source is TagSource.Custom;
	public bool IsWorkshop => Source is TagSource.Workshop;

	public override string ToString()
	{
		return LocaleHelper.GetGlobalText($"Tag_{Value}", out var translation) ? translation : Value;
	}

	public override bool Equals(object? obj)
	{
		return obj is TagItem item &&
			   Value.Equals(item.Value, System.StringComparison.InvariantCultureIgnoreCase);
	}

	public override int GetHashCode()
	{
		return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value.ToLower());
	}
}
