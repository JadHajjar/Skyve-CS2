using Extensions;

using Skyve.Domain.Systems;

namespace Skyve.Systems.CS2.Utilities;
public class LocaleCS2 : LocaleHelper, ILocale
{
	private static readonly LocaleCS2 _instance = new();

	public static void Load() { _ = _instance; }

	public Translation Get(string key)
	{
		return GetGlobalText(key);
	}

	public LocaleCS2() : base($"Skyve.Systems.CS2.Properties.LocaleCS2.json") { }
}