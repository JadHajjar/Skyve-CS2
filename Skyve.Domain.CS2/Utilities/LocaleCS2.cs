using Extensions;

using Skyve.Domain.Systems;

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

	/// <summary>
	/// <para>Copy PDX Mods ID</para>
	/// <para>Plural: Copy the selected packages' PDX Mods IDs</para>
	/// </summary>
	public static Translation CopyWorkshopId => _instance.GetText("CopyWorkshopId");

	/// <summary>
	/// <para>Copy PDX Mods link</para>
	/// <para>Plural: Copy the selected packages' PDX Mods links</para>
	/// </summary>
	public static Translation CopyWorkshopLink => _instance.GetText("CopyWorkshopLink");

	/// <summary>
	/// Download completed
	/// </summary>
	public static Translation DownloadComplete => _instance.GetText("DownloadComplete");

	/// <summary>
	/// Downloading...
	/// </summary>
	public static Translation Downloading => _instance.GetText("Downloading");

	/// <summary>
	/// Drop or select a playset's file here to import it
	/// </summary>
	public static Translation DropNewPlayset => _instance.GetText("DropNewPlayset");

	/// <summary>
	/// Remove all items from your active playset
	/// </summary>
	public static Translation ExcludeAll => _instance.GetText("ExcludeAll");

	/// <summary>
	/// Remove all disabled items from your active playset
	/// </summary>
	public static Translation ExcludeAllDisabled => _instance.GetText("ExcludeAllDisabled");

	/// <summary>
	/// Remove filtered &amp; disabled items from your active playset
	/// </summary>
	public static Translation ExcludeAllDisabledFiltered => _instance.GetText("ExcludeAllDisabledFiltered");

	/// <summary>
	/// Remove selected &amp; disabled items from your active playset
	/// </summary>
	public static Translation ExcludeAllDisabledSelected => _instance.GetText("ExcludeAllDisabledSelected");

	/// <summary>
	/// Remove filtered items from your active playset
	/// </summary>
	public static Translation ExcludeAllFiltered => _instance.GetText("ExcludeAllFiltered");

	/// <summary>
	/// Remove selected items from your active playset
	/// </summary>
	public static Translation ExcludeAllSelected => _instance.GetText("ExcludeAllSelected");

	/// <summary>
	/// Remove from your active playset
	/// </summary>
	public static Translation ExcludeItem => _instance.GetText("ExcludeItem");

	/// <summary>
	/// <para>Remove from all your playsets</para>
	/// <para>Plural: Remove the selected items from all your playsets</para>
	/// </summary>
	public static Translation ExcludeThisItemInAllPlaysets => _instance.GetText("ExcludeThisItemInAllPlaysets");

	/// <summary>
	/// Enable 'Included' filter for local content by default
	/// </summary>
	public static Translation FilterIncludedByDefault => _instance.GetText("FilterIncludedByDefault");

	/// <summary>
	/// Only shows local content that are included in your active playset by default.
	/// </summary>
	public static Translation FilterIncludedByDefaultTip => _instance.GetText("FilterIncludedByDefault_Tip");

	/// <summary>
	/// Add all items to your active playset
	/// </summary>
	public static Translation IncludeAll => _instance.GetText("IncludeAll");

	/// <summary>
	/// Add filtered items to your active playset
	/// </summary>
	public static Translation IncludeAllFiltered => _instance.GetText("IncludeAllFiltered");

	/// <summary>
	/// Add selected items to your active playset
	/// </summary>
	public static Translation IncludeAllSelected => _instance.GetText("IncludeAllSelected");

	/// <summary>
	/// Add to your active playset
	/// </summary>
	public static Translation IncludeItem => _instance.GetText("IncludeItem");

	/// <summary>
	/// <para>Add to all your playsets</para>
	/// <para>Plural: Add the selected items to all your playsets</para>
	/// </summary>
	public static Translation IncludeThisItemInAllPlaysets => _instance.GetText("IncludeThisItemInAllPlaysets");

	/// <summary>
	/// Launch through Cities2.exe
	/// </summary>
	public static Translation LaunchThroughCities => _instance.GetText("LaunchThroughCities");

	/// <summary>
	/// The login process is securely handled through Paradox services and no data is shared with Skyve.
	/// </summary>
	public static Translation LoginDisclaimer => _instance.GetText("LoginDisclaimer");

	/// <summary>
	/// Login failed, make sure your e-mail and password are correct. Or try again later.
	/// </summary>
	public static Translation LoginFailed => _instance.GetText("LoginFailed");

	/// <summary>
	/// Log in to Paradox
	/// </summary>
	public static Translation LoginToParadox => _instance.GetText("LoginToParadox");

	/// <summary>
	/// '{0}' failed to download
	/// </summary>
	public static Translation ModDownloadFailed => _instance.GetText("ModDownloadFailed");

	/// <summary>
	/// Disable workshop packages
	/// </summary>
	public static Translation NoWorkshop => _instance.GetText("NoWorkshop");

	/// <summary>
	/// Open C:S II's AppData folder
	/// </summary>
	public static Translation OpenCitiesAppData => _instance.GetText("OpenCitiesAppData");

	/// <summary>
	/// The local size of '{0}' ({1}) is different than PDX Mods' {2}
	/// </summary>
	public static Translation PackageIsIncomplete => _instance.GetText("PackageIsIncomplete");

	/// <summary>
	/// '{0}' was removed from PDX Mods
	/// </summary>
	public static Translation PackageIsRemoved => _instance.GetText("PackageIsRemoved");

	/// <summary>
	/// The information from PDX Mods hasn't loaded for '{0}' yet
	/// </summary>
	public static Translation PackageIsUnknown => _instance.GetText("PackageIsUnknown");

	/// <summary>
	/// Your saved username/password failed to log in. Click here to update your information.
	/// </summary>
	public static Translation ParadoxLoginFailedBadCredentials => _instance.GetText("ParadoxLoginFailedBadCredentials");

	/// <summary>
	/// Your session has likely expired. Try launching the game to log in again, or click here to log in through Skyve.
	/// </summary>
	public static Translation ParadoxLoginFailedEmpty => _instance.GetText("ParadoxLoginFailedEmpty");

	/// <summary>
	/// You're not connected to the internet, please check your connection and click here to try again.
	/// </summary>
	public static Translation ParadoxLoginFailedNoConnection => _instance.GetText("ParadoxLoginFailedNoConnection");

	/// <summary>
	/// Could not log you in to PDX Mods
	/// </summary>
	public static Translation ParadoxLoginFailedTitle => _instance.GetText("ParadoxLoginFailedTitle");

	/// <summary>
	/// * Your credentials will be encrypted and saved to your computer.
	/// </summary>
	public static Translation RememberMeInfo => _instance.GetText("RememberMeInfo");

	/// <summary>
	/// Clear PDX Mods cache
	/// </summary>
	public static Translation ResetSteamCache => _instance.GetText("ResetSteamCache");

	/// <summary>
	/// 
	/// </summary>
	public static Translation RunSetupOrRunApp => _instance.GetText("RunSetupOrRunApp");

	/// <summary>
	/// Launch Cities: Skylines II
	/// </summary>
	public static Translation StartCities => _instance.GetText("StartCities");

	/// <summary>
	/// Stop Cities: Skylines II
	/// </summary>
	public static Translation StopCities => _instance.GetText("StopCities");

	/// <summary>
	/// <para>Add to your active playset</para>
	/// <para>Plural: Add these to your active playset</para>
	/// </summary>
	public static Translation SubscribeToItem => _instance.GetText("SubscribeToItem");

	/// <summary>
	/// View on PDX Mods
	/// </summary>
	public static Translation ViewOnWorkshop => _instance.GetText("ViewOnWorkshop");
}
