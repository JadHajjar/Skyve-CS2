using Extensions;

using Skyve.Domain.Systems;

using System.Collections.Generic;

namespace Skyve.Domain.CS2.Utilities;

[SaveName(nameof(DlcConfig) + ".json")]
public class DlcConfig : ConfigFile
{
	public List<uint> RemovedDLCs { get; set; } = [];
}

//public class ModConfig : ConfigFile
//{
//    public const string FILE_NAME = "ModConfig.json";

//    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
//    public List<SavedModInfo> SavedModsInfo { get; set; } = new();
//    public Dictionary<string, ModInfo> GetModsInfo()
//    {
//        var dictionary = new Dictionary<string, ModInfo>(new PathEqualityComparer());

//        foreach (var item in SavedModsInfo)
//        {
//            dictionary[item.Path ?? string.Empty] = item;
//        }

//        return dictionary;
//    }

//    public void SetModsInfo(Dictionary<string, ModInfo> value)
//    {
//        var list = new List<SavedModInfo>();

//        foreach (var item in value)
//        {
//            list.Add(new()
//            {
//                Path = item.Key,
//                Excluded = item.Value.Excluded,
//                LoadOrder = item.Value.LoadOrder,
//            });
//        }

//        SavedModsInfo = list;
//    }

//    public ModConfig() : base(GetFilePath())
//    { }

//    private static string GetFilePath()
//    {
//        return CrossIO.Combine(ServiceCenter.Get<ILocationService>().SkyveSettingsPath, FILE_NAME);
//    }

//    public static ModConfig Load()
//    {
//        return Load<ModConfig>(GetFilePath()) ?? new();
//    }

//    public class SavedModInfo : ModInfo
//    {
//        public string? Path { get; set; }
//    }

//    public class ModInfo
//    {
//        public bool Excluded { get; set; }
//        public int LoadOrder { get; set; }
//    }
//}

[SaveName(nameof(AssetConfig) + ".json")]
public class AssetConfig : ConfigFile
{
}