using Skyve.Domain.CS2.Enums;
using System.Collections.Generic;

namespace Skyve.Domain.CS2.Content;
public struct TagItem(TagSource source, string key, string value) : ITag
{
	public TagSource Source { get; set; } = source;
	public string Key { get; set; } = key;
	public string Value { get; set; } = value;
	public readonly string Icon => Source switch { TagSource.Workshop => "I_Paradox", TagSource.Custom => "I_Search", _ => "I_Tag" };
	public readonly bool IsCustom => Source is TagSource.Custom;

	public override readonly string ToString()
    {
        return Value;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is TagItem item &&
               Value == item.Value;
    }

    public override readonly int GetHashCode()
    {
        return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
    }
}
