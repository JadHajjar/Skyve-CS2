using Extensions;

using Skyve.Domain.CS2.Enums;

using System.Collections.Generic;

namespace Skyve.Domain.CS2.Content;
public class TagItem(TagSource source, string key, string value) : IWorkshopTag
{
	public TagSource Source { get; set; } = source;
	public string Key { get; set; } = key;
	public string Value { get; set; } = value;
	public IWorkshopTag[] Children { get; set; } = [];
	public bool IsSelectable { get; set; } = true;
	public int? UsageCount { get; set; }
	public string Icon => Source switch { TagSource.ID or TagSource.Workshop => "PDXMods", TagSource.Custom => "Search", _ => "Tag" };
	public bool IsCustom => Source is TagSource.Custom;
	public bool IsWorkshop => Source is TagSource.Workshop;

	public override string ToString()
	{
		return LocaleHelper.GetGlobalText($"Tag_{Key}", out var translation) ? translation : Value;
	}

	public override bool Equals(object? obj)
	{
		return obj is TagItem item &&
			   Key.Equals(item.Key, System.StringComparison.InvariantCultureIgnoreCase);
	}

	public override int GetHashCode()
	{
		return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Key.ToLower());
	}
}
