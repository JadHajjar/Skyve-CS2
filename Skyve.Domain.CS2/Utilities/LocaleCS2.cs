using Extensions;

using Skyve.Domain.Systems;

using System;

namespace Skyve.Domain.CS2.Utilities;
public class LocaleCS2 : LocaleHelper, ILocale
{
	private static readonly LocaleCS2 _instance = new();

	public static void Load() { _ = _instance; }

	public Translation Get(string key)
	{
		return GetGlobalText(key);
	}

	public LocaleCS2() : base($"Skyve.Domain.CS2.Properties.LocaleCS2.json") { }

	public static Translation RunFromAppDataMessage => _instance.GetText(nameof(RunFromAppDataMessage));
	public static Translation RunFromAppDataTitle => _instance.GetText(nameof(RunFromAppDataTitle));
}