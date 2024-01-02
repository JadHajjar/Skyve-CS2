﻿using Extensions;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.IO;

namespace Skyve.Domain.CS2.Utilities;
public class UserSettings : ConfigFile, IUserSettings
{
    #region Implementation
    private const string FILE_NAME = nameof(UserSettings) + ".json";

    public UserSettings() : base(FILE_NAME)
    { }

    public static UserSettings Load(string appDataPath)
    {
        var path = CrossIO.Combine(appDataPath, FILE_NAME);

        var settings = Load<UserSettings>(path) ?? new();

        settings.FilePath = path;

        return settings;
    }
	#endregion

	bool IUserSettings.AdvancedIncludeEnable { get; set; }

	public bool LinkModAssets { get; set; } = true;
    public bool ShowDatesRelatively { get; set; } = true;
    public bool DisableNewModsByDefault { get; set; }
    public bool DisableNewAssetsByDefault { get; set; }
    public bool OverrideGameChanges { get; set; }
    public bool FilterOutPackagesWithOneAsset { get; set; }
    public bool FilterOutPackagesWithMods { get; set; }
    public bool AdvancedLaunchOptions { get; set; }
    public bool HidePseudoMods { get; set; }
    public bool ShowFolderSettings { get; set; }
    public bool AlwaysOpenFiltersAndActions { get; set; }
    public bool OpenLinksInBrowser { get; set; }
    public bool ResetScrollOnPackageClick { get; set; }
    public bool FlipItemCopyFilterAction { get; set; }
    public bool DisablePackageCleanup { get; set; }
    public bool ShowAllReferencedPackages { get; set; }
    public bool TreatOptionalAsRequired { get; set; }
    public bool ForceDownloadAndDeleteAsSoonAsRequested { get; set; }
    public bool AssumeInternetConnectivity { get; set; }
    public bool SnapDashToGrid { get; set; }
    public Dictionary<SkyvePage, SkyvePageContentSettings> PageSettings { get; set; } = new();
    public ParadoxLoginInfo ParadoxLogin { get; set; }
    public bool ExtendedListInfo { get; set; }
}
