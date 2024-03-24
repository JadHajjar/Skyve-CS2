using Skyve.Domain.CS2.Enums;

using System.Collections.Generic;

namespace Skyve.Domain.CS2.Content;
public struct TagItem(TagSource source, string key, string value) : ITag
{
	public TagSource Source { get; set; } = source;
	public string Key { get; set; } = key;
	public string Value { get; set; } = value;
	public readonly string Icon => Source switch { TagSource.ID => "Paradox", TagSource.Custom => "Search", _ => "Tag" };
	public readonly bool IsCustom => Source is TagSource.Custom;

	public override readonly string ToString()
	{
		return Value;
	}

	public override readonly bool Equals(object? obj)
	{
		return obj is TagItem item &&
			   Value.Equals(item.Value, System.StringComparison.InvariantCultureIgnoreCase);
	}

	public override readonly int GetHashCode()
	{
		return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value.ToLower());
	}
}
