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

	public static Translation ParadoxLoginFailedTitle => _instance.GetText(nameof(ParadoxLoginFailedTitle));
	public static Translation ParadoxLoginFailedNoConnectionTitle => _instance.GetText(nameof(ParadoxLoginFailedNoConnectionTitle));
	public static Translation ParadoxLoginFailedNoConnection => _instance.GetText(nameof(ParadoxLoginFailedNoConnection));
	public static Translation ParadoxLoginFailedBadCredentials => _instance.GetText(nameof(ParadoxLoginFailedBadCredentials));
	public static Translation ParadoxLoginFailedEmpty => _instance.GetText(nameof(ParadoxLoginFailedEmpty));
	public static Translation RunFromAppDataMessage => _instance.GetText(nameof(RunFromAppDataMessage));
	public static Translation RunFromAppDataTitle => _instance.GetText(nameof(RunFromAppDataTitle));
	public static Translation DonwloadComplete => _instance.GetText(nameof(DonwloadComplete));
	public static Translation Downloading => _instance.GetText(nameof(Downloading));
	public static Translation ModDownloadFailed => _instance.GetText(nameof(Downloading));
}