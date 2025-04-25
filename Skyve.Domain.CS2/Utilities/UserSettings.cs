using Extensions;

using Skyve.Domain.Enums;

using System.Collections.Generic;

namespace Skyve.Domain.CS2.Utilities;
[SaveName(nameof(UserSettings) + ".json")]
public class UserSettings : ConfigFile, IUserSettings
{
    public UserSettings()
    {
        AutoRefresh = true;
    }

    bool IUserSettings.AdvancedIncludeEnable { get; set; }
	bool IUserSettings.OpenLinksInBrowser { get; set; }
	bool IUserSettings.ForceDownloadAndDeleteAsSoonAsRequested { get; set; }
	bool IUserSettings.DisablePackageCleanup { get; set; }
	bool IUserSettings.OverrideGameChanges { get; set; }
	bool IUserSettings.HidePseudoMods { get; set; }
	bool IUserSettings.DisableNewModsByDefault { get; set; }
	bool IUserSettings.DisableNewAssetsByDefault { get; set; }

	public Dictionary<SkyvePage, SkyvePageContentSettings> PageSettings { get; set; } = [];

	public bool LinkModAssets { get; set; } = true;
	public bool FadeDisabledItems { get; set; } = true;
	public bool ShowDatesRelatively { get; set; } = true;
	public bool DisableContentCreatorWarnings { get; set; }
	public bool FilterOutPackagesWithOneAsset { get; set; }
	public bool FilterOutPackagesWithMods { get; set; }
	public bool AdvancedLaunchOptions { get; set; }
	public bool ShowFolderSettings { get; set; }
	public bool AlwaysOpenFiltersAndActions { get; set; }
	public bool ResetScrollOnPackageClick { get; set; }
	public bool FlipItemCopyFilterAction { get; set; }
	public bool ShowAllReferencedPackages { get; set; }
	public bool TreatOptionalAsRequired { get; set; }
	public bool AssumeInternetConnectivity { get; set; }
	public bool SnapDashToGrid { get; set; }
	public bool ComplexListUI { get; set; }
	public bool FilterIncludedByDefault { get; set; } = true;
	public bool SyncBeforeLaunching { get; set; } = true;
	public bool ColoredAuthorNames { get; set; } = true;
	public bool DisableLogCleanup { get; set; }
	public DependencyResolveBehavior DependencyResolution { get; set; }

	public void Save()
	{
		Handler?.Save(this);
	}
}
