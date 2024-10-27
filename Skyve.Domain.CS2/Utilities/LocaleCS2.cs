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
	/// <para>'{1}' is required for the mod(s) you're adding.  Would you like to also add '{1}' to your playset?</para>
	/// <para>Plural: The mod(s) you're adding have {0} dependencies: {1}  Would you like to also add them to your playset?</para>
	/// </summary>
	public static Translation AddingDependencies => _instance.GetText("AddingDependencies");

	/// <summary>
	/// <para>Playset</para>
	/// <para>Plural: {0} playsets</para>
	/// </summary>
	public static Translation BackupActivePlayset => _instance.GetText("Backup_ActivePlayset");

	/// <summary>
	/// <para>Local mod</para>
	/// <para>Plural: {0} local mods</para>
	/// </summary>
	public static Translation BackupLocalMods => _instance.GetText("Backup_LocalMods");

	/// <summary>
	/// <para>Mod settings folder</para>
	/// <para>Plural: {0} mod settings folders</para>
	/// </summary>
	public static Translation BackupModsSettingsFiles => _instance.GetText("Backup_ModsSettingsFiles");

	/// <summary>
	/// <para>Savegame</para>
	/// <para>Plural: {0} savegames</para>
	/// </summary>
	public static Translation BackupSaveGames => _instance.GetText("Backup_SaveGames");

	/// <summary>
	/// <para>Settings files</para>
	/// <para>Plural: {0} settings files</para>
	/// </summary>
	public static Translation BackupSettingsFiles => _instance.GetText("Backup_SettingsFiles");

	/// <summary>
	/// You can't run Skyve from this folder.  Either run the app from your Programs list, or run the Skyve Setup from the mod's folder.
	/// </summary>
	public static Translation CantRunAppFromHere => _instance.GetText("CantRunAppFromHere");

	/// <summary>
	/// Change Content Location
	/// </summary>
	public static Translation ChangeLocation => _instance.GetText("ChangeLocation");

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
	/// <para>Copy PDX Mods link as Markdown</para>
	/// <para>Plural: Copy the selected packages' PDX Mods links as Markdown</para>
	/// </summary>
	public static Translation CopyWorkshopMarkdownLink => _instance.GetText("CopyWorkshopMarkdownLink");

	/// <summary>
	/// Could not retrieve the mods' data
	/// </summary>
	public static Translation CouldNotRetrieveMods => _instance.GetText("CouldNotRetrieveMods");

	/// <summary>
	/// Current Status:
	/// </summary>
	public static Translation CurrentStatus => _instance.GetText("CurrentStatus");

	/// <summary>
	/// Default User Data Location
	/// </summary>
	public static Translation DefaultLocation => _instance.GetText("DefaultLocation");

	/// <summary>
	/// Ask every time
	/// </summary>
	public static Translation DependencyAsk => _instance.GetText("Dependency_Ask");

	/// <summary>
	/// Automatically add dependencies
	/// </summary>
	public static Translation DependencyAutomatic => _instance.GetText("Dependency_Automatic");

	/// <summary>
	/// Do nothing
	/// </summary>
	public static Translation DependencyNone => _instance.GetText("Dependency_None");

	/// <summary>
	/// Dependency Resolution
	/// </summary>
	public static Translation DependencyResolution => _instance.GetText("DependencyResolution");

	/// <summary>
	/// Choose what will happen when you add a mod with dependencies that you do not have.
	/// </summary>
	public static Translation DependencyResolutionTip => _instance.GetText("DependencyResolution_Tip");

	/// <summary>
	/// Enable Developer Mode
	/// </summary>
	public static Translation DeveloperMode => _instance.GetText("DeveloperMode");

	/// <summary>
	/// Disable Burst-Compilation
	/// </summary>
	public static Translation DisableBurstCompile => _instance.GetText("DisableBurstCompile");

	/// <summary>
	/// Disk Status
	/// </summary>
	public static Translation DiskStatus => _instance.GetText("DiskStatus");

	/// <summary>
	/// Download completed
	/// </summary>
	public static Translation DownloadComplete => _instance.GetText("DownloadComplete");

	/// <summary>
	/// Download failed
	/// </summary>
	public static Translation DownloadFailed => _instance.GetText("DownloadFailed");

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
	/// Forum Page
	/// </summary>
	public static Translation ForumPage => _instance.GetText("ForumPage");

	/// <summary>
	/// {0} free
	/// </summary>
	public static Translation FreeSpace => _instance.GetText("FreeSpace");

	/// <summary>
	/// Hide your profile in the main menu
	/// </summary>
	public static Translation HideUserSection => _instance.GetText("HideUserSection");

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
	/// Initial Release
	/// </summary>
	public static Translation InitialRelease => _instance.GetText("InitialRelease");

	/// <summary>
	/// Invalid setup detected
	/// </summary>
	public static Translation InvalidFolderSettings => _instance.GetText("InvalidFolderSettings");

	/// <summary>
	/// The selected path is not correct. Please select the 'Cities Skylines II' folder in your app data.
	/// </summary>
	public static Translation InvalidFolderSettingsFail => _instance.GetText("InvalidFolderSettingsFail");

	/// <summary>
	/// Skyve was not able to properly detect Cities: Skylines II's data folder.  Click here to manually select the data folder.
	/// </summary>
	public static Translation InvalidFolderSettingsInfo => _instance.GetText("InvalidFolderSettingsInfo");

	/// <summary>
	/// Change the location where the game stores its content. This includes any mods, assets, and settings.  Skyve will create a link between your desired location and the default app data folder in this process.
	/// </summary>
	public static Translation JunctionDescription => _instance.GetText("JunctionDescription");

	/// <summary>
	/// The selected folder is not a valid destination. Please try another one.
	/// </summary>
	public static Translation JunctionInvalidFolder => _instance.GetText("JunctionInvalidFolder");

	/// <summary>
	/// Skyve and Cities: Skylines II will close during this process.  Skyve will restart automatically once finished.
	/// </summary>
	public static Translation JunctionRestart => _instance.GetText("JunctionRestart");

	/// <summary>
	/// Custom Content Location
	/// </summary>
	public static Translation JunctionTitle => _instance.GetText("JunctionTitle");

	/// <summary>
	/// Launch in Safe Mode
	/// </summary>
	public static Translation LaunchInSafeMode => _instance.GetText("LaunchInSafeMode");

	/// <summary>
	/// Launch through Cities2.exe
	/// </summary>
	public static Translation LaunchThroughCities => _instance.GetText("LaunchThroughCities");

	/// <summary>
	/// Logging in...
	/// </summary>
	public static Translation LoggingIn => _instance.GetText("LoggingIn");

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
	/// Log Level
	/// </summary>
	public static Translation LogLevel => _instance.GetText("LogLevel");

	/// <summary>
	/// Copy logs to Player.log
	/// </summary>
	public static Translation LogsToPlayerLog => _instance.GetText("LogsToPlayerLog");

	/// <summary>
	/// <para>You're running out of disk space, go to the options to change the location where the game's content is saved.</para>
	/// <para>Zero: You're running out of disk space, you need to clear up some storage or get an additional hard disk.</para>
	/// </summary>
	public static Translation LowSpaceCreateJunction => _instance.GetText("LowSpaceCreateJunction");

	/// <summary>
	/// <para>A mod failed to download</para>
	/// <para>Plural: Some mods failed to download</para>
	/// </summary>
	public static Translation ModsDownloadFailed => _instance.GetText("ModsDownloadFailed");

	/// <summary>
	/// <para>'{1}' has dependencies</para>
	/// <para>Plural: The packages you're adding have dependencies</para>
	/// </summary>
	public static Translation ModsYouAreAddingRequireDependencies => _instance.GetText("ModsYouAreAddingRequireDependencies");

	/// <summary>
	/// New Cities: Skylines II Update
	/// </summary>
	public static Translation NewGameUpdate => _instance.GetText("NewGameUpdate");

	/// <summary>
	/// Patch {0} is now available. Be sure to check your compatibility report before starting your game.
	/// </summary>
	public static Translation NewGameUpdateInfo => _instance.GetText("NewGameUpdateInfo");

	/// <summary>
	/// You're not logged in to Paradox, check your notifications for more information.
	/// </summary>
	public static Translation NotLoggedInCheckNotification => _instance.GetText("NotLoggedInCheckNotification");

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
	/// Paradox Account
	/// </summary>
	public static Translation ParadoxAccount => _instance.GetText("ParadoxAccount");

	/// <summary>
	/// Paradox backend failed to load
	/// </summary>
	public static Translation ParadoxContextFailed => _instance.GetText("ParadoxContextFailed");

	/// <summary>
	/// Something is causing the paradox backend to fail. Click this message to try and repair it.  If the repair is not successful, ask for help on tech support forums.
	/// </summary>
	public static Translation ParadoxContextFailedInfo => _instance.GetText("ParadoxContextFailedInfo");

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
	/// New on PDX Mods
	/// </summary>
	public static Translation PDXModsNew => _instance.GetText("PDXModsNew");

	/// <summary>
	/// Popular mods from last week
	/// </summary>
	public static Translation PDXModsPopularWeek => _instance.GetText("PDXModsPopularWeek");

	/// <summary>
	/// Recently updated on PDX Mods
	/// </summary>
	public static Translation PDXModsUpdated => _instance.GetText("PDXModsUpdated");

	/// <summary>
	/// PDX Mods Sync
	/// </summary>
	public static Translation PdxSync => _instance.GetText("PdxSync");

	/// <summary>
	/// The PDX Synchronization process handles downloading and updating your mods, as well as synchronizing the settings and playsets you have between your local computer and PDX Mods services.
	/// </summary>
	public static Translation PdxSyncInfo => _instance.GetText("PdxSyncInfo");

	/// <summary>
	/// This playset is already present in your list of playsets.
	/// </summary>
	public static Translation PlaysetAlreadyImported => _instance.GetText("PlaysetAlreadyImported");

	/// <summary>
	/// * Your credentials will be encrypted and saved to your computer.
	/// </summary>
	public static Translation RememberMeInfo => _instance.GetText("RememberMeInfo");

	/// <summary>
	/// Reset Location
	/// </summary>
	public static Translation ResetLocation => _instance.GetText("ResetLocation");

	/// <summary>
	/// Clear PDX Mods cache
	/// </summary>
	public static Translation ResetSteamCache => _instance.GetText("ResetSteamCache");

	/// <summary>
	/// You can't run Skyve from this folder. If you've installed it already, run it from your Programs list.  If not, click on 'Ok' to run the setup.
	/// </summary>
	public static Translation RunSetupOrRunApp => _instance.GetText("RunSetupOrRunApp");

	/// <summary>
	/// Start Synchronization
	/// </summary>
	public static Translation RunSync => _instance.GetText("RunSync");

	/// <summary>
	/// Safe Mode
	/// </summary>
	public static Translation SafeMode => _instance.GetText("SafeMode");

	/// <summary>
	/// Launch the game in 'Safe Mode' to help identify issues with crashes.  It is hard to identify the source of a crash, and makes recovering a save impossible sometimes. Safe mode allows the game to avoid crashes in most cases which lets you recover a save, and properly logs errors to be analysed by tech support.
	/// </summary>
	public static Translation SafeModeInfo => _instance.GetText("SafeModeInfo");

	/// <summary>
	/// Time Period
	/// </summary>
	public static Translation SearchTime => _instance.GetText("SearchTime");

	/// <summary>
	/// All Time
	/// </summary>
	public static Translation SearchTimeAllTime => _instance.GetText("SearchTime_AllTime");

	/// <summary>
	/// 24 hours
	/// </summary>
	public static Translation SearchTimeDay => _instance.GetText("SearchTime_Day");

	/// <summary>
	/// 1 Month
	/// </summary>
	public static Translation SearchTimeMonth => _instance.GetText("SearchTime_Month");

	/// <summary>
	/// 3 Months
	/// </summary>
	public static Translation SearchTimeQuarter => _instance.GetText("SearchTime_Quarter");

	/// <summary>
	/// 6 Months
	/// </summary>
	public static Translation SearchTimeSixMonths => _instance.GetText("SearchTime_SixMonths");

	/// <summary>
	/// 7 Days
	/// </summary>
	public static Translation SearchTimeWeek => _instance.GetText("SearchTime_Week");

	/// <summary>
	/// 1 Year
	/// </summary>
	public static Translation SearchTimeYear => _instance.GetText("SearchTime_Year");

	/// <summary>
	/// Skyve was not set up yet. Please launch Cities: Skylines II with Skyve enabled to complete the setup.  Make sure mods load successfully, and if not, try creating a new playset with only Skyve included in it.
	/// </summary>
	public static Translation SkyvenotSetUpInfo => _instance.GetText("SkyvenotSetUpInfo");

	/// <summary>
	/// Most Popular
	/// </summary>
	public static Translation SortingBest => _instance.GetText("Sorting_Best");

	/// <summary>
	/// Most Recent
	/// </summary>
	public static Translation SortingDateCreated => _instance.GetText("Sorting_DateCreated");

	/// <summary>
	/// Last Updated
	/// </summary>
	public static Translation SortingDateUpdated => _instance.GetText("Sorting_DateUpdated");

	/// <summary>
	/// Most Subscribed
	/// </summary>
	public static Translation SortingPopularity => _instance.GetText("Sorting_Popularity");

	/// <summary>
	/// Alphabetical
	/// </summary>
	public static Translation SortingWorkshopName => _instance.GetText("Sorting_WorkshopName");

	/// <summary>
	/// Launch Cities: Skylines II
	/// </summary>
	public static Translation StartCities => _instance.GetText("StartCities");

	/// <summary>
	/// You're about to launch the game with no active playset. This means that no mods will be enabled in the game.
	/// </summary>
	public static Translation StartingWithNoPlayset => _instance.GetText("StartingWithNoPlayset");

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
	/// Synchronize mods before launching the game
	/// </summary>
	public static Translation SyncBeforeLaunching => _instance.GetText("SyncBeforeLaunching");

	/// <summary>
	/// Runs a mod sync before launching to make sure all mods are up to date.
	/// </summary>
	public static Translation SyncBeforeLaunchingTip => _instance.GetText("SyncBeforeLaunching_Tip");

	/// <summary>
	/// Sync is currently ongoing
	/// </summary>
	public static Translation SyncOngoing => _instance.GetText("SyncOngoing");

	/// <summary>
	/// Skyve is still synchronizing your mods, would you like to launch the game once your mods are ready? Select 'No' to start the game anyway.
	/// </summary>
	public static Translation SyncOngoingLaunchGame => _instance.GetText("SyncOngoingLaunchGame");

	/// <summary>
	/// Asset Pack
	/// </summary>
	public static Translation TagAssetPack => _instance.GetText("Tag_Asset Pack");

	/// <summary>
	/// Building
	/// </summary>
	public static Translation TagBuilding => _instance.GetText("Tag_Building");

	/// <summary>
	/// Building Extension
	/// </summary>
	public static Translation TagBuildingExtension => _instance.GetText("Tag_Building Extension");

	/// <summary>
	/// Prop
	/// </summary>
	public static Translation TagStaticObject => _instance.GetText("Tag_Static Object");

	/// <summary>
	/// Zone
	/// </summary>
	public static Translation TagZone => _instance.GetText("Tag_Zone");

	/// <summary>
	/// Total Backups Size
	/// </summary>
	public static Translation TotalBackupSize => _instance.GetText("TotalBackupSize");

	/// <summary>
	/// Total C:S II User Data Stored
	/// </summary>
	public static Translation TotalCitiesSize => _instance.GetText("TotalCitiesSize");

	/// <summary>
	/// Other Files' Size
	/// </summary>
	public static Translation TotalOtherSize => _instance.GetText("TotalOtherSize");

	/// <summary>
	/// Savegames' Size
	/// </summary>
	public static Translation TotalSavesSize => _instance.GetText("TotalSavesSize");

	/// <summary>
	/// Subscriptions' Size
	/// </summary>
	public static Translation TotalSubbedSize => _instance.GetText("TotalSubbedSize");

	/// <summary>
	/// Enable UI Developer Mode
	/// </summary>
	public static Translation UIDeveloperMode => _instance.GetText("UIDeveloperMode");

	/// <summary>
	/// New comments on '{0}'
	/// </summary>
	public static Translation UnreadComment => _instance.GetText("UnreadComment");

	/// <summary>
	/// Un-Like this mod
	/// </summary>
	public static Translation UnVoteMod => _instance.GetText("UnVoteMod");

	/// <summary>
	/// Update available
	/// </summary>
	public static Translation UpdateAvailable => _instance.GetText("UpdateAvailable");

	/// <summary>
	/// Click here to update Skyve
	/// </summary>
	public static Translation UpdateAvailableInfo => _instance.GetText("UpdateAvailableInfo");

	/// <summary>
	/// out of the {0} on drive {1}
	/// </summary>
	public static Translation UsedOutOfSpace => _instance.GetText("UsedOutOfSpace");

	/// <summary>
	/// View on PDX Mods
	/// </summary>
	public static Translation ViewOnWorkshop => _instance.GetText("ViewOnWorkshop");

	/// <summary>
	/// Like this mod
	/// </summary>
	public static Translation VoteMod => _instance.GetText("VoteMod");
}
