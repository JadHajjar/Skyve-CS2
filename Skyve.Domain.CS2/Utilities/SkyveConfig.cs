using Extensions;
using Skyve.Domain.Systems;

using System.Collections.Generic;
using System.ComponentModel;

namespace Skyve.Domain.CS2.Utilities;

public class DlcConfig : ConfigFile
{
    private const string FILE_NAME = "DlcConfig.json";

    public List<uint> RemovedDLCs { get; set; } = new();

    public DlcConfig() : base(GetFilePath())
    { }

    private static string GetFilePath()
    {
        return CrossIO.Combine(ServiceCenter.Get<ILocationService>().SkyveSettingsPath, FILE_NAME);
    }

    public static DlcConfig Load()
    {
        return Load<DlcConfig>(GetFilePath()) ?? new();
    }
}

public class ModConfig : ConfigFile
{
    public const string FILE_NAME = "ModConfig.json";

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public List<SavedModInfo> SavedModsInfo { get; set; } = new();
    public Dictionary<string, ModInfo> GetModsInfo()
    {
        var dictionary = new Dictionary<string, ModInfo>(new PathEqualityComparer());

        foreach (var item in SavedModsInfo)
        {
            dictionary[item.Path ?? string.Empty] = item;
        }

        return dictionary;
    }

    public void SetModsInfo(Dictionary<string, ModInfo> value)
    {
        var list = new List<SavedModInfo>();

        foreach (var item in value)
        {
            list.Add(new()
            {
                Path = item.Key,
                Excluded = item.Value.Excluded,
                LoadOrder = item.Value.LoadOrder,
            });
        }

        SavedModsInfo = list;
    }

    public ModConfig() : base(GetFilePath())
    { }

    private static string GetFilePath()
    {
        return CrossIO.Combine(ServiceCenter.Get<ILocationService>().SkyveSettingsPath, FILE_NAME);
    }

    public static ModConfig Load()
    {
        return Load<ModConfig>(GetFilePath()) ?? new();
    }

    public class SavedModInfo : ModInfo
    {
        public string? Path { get; set; }
    }

    public class ModInfo
    {
        public bool Excluded { get; set; }
        public int LoadOrder { get; set; }
    }
}

public class AssetConfig : ConfigFile
{
    public const string FILE_NAME = "AssetConfig.json";

    public List<string> ExcludedAssets { get; set; } = new();

    public AssetConfig() : base(GetFilePath())
    { }

    private static string GetFilePath()
    {
        return CrossIO.Combine(ServiceCenter.Get<ILocationService>().SkyveSettingsPath, FILE_NAME);
    }

    public static AssetConfig Load()
    {
        return Load<AssetConfig>(GetFilePath()) ?? new();
    }
}