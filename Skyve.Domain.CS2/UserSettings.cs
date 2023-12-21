using Extensions;

using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;

namespace Skyve.Domain.CS2;
public class UserSettings : ConfigFile, IUserSettings
{
	#region Implementation
	private const string FILE_NAME = nameof(UserSettings) + ".json";

	public UserSettings() : base(GetFilePath())
	{ }

	private static string GetFilePath()
	{
		return CrossIO.Combine(Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf("Cities Skylines II")), "Cities Skylines II", "ModSettings", FILE_NAME);
	}

	public static UserSettings Load()
	{
		return Load<UserSettings>(GetFilePath()) ?? new();
	}
	#endregion

	public bool LinkModAssets { get; set; } = true;
	public bool ShowDatesRelatively { get; set; } = true;
	public bool AdvancedIncludeEnable { get; set; }
	public bool DisableNewModsByDefault { get; set; } = true;
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
}
