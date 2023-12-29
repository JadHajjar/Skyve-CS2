using Extensions;

using Newtonsoft.Json;

using PDX.SDK.Contracts.Service.Mods.Result;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems;
using Skyve.Systems.Compatibility.Domain.Api;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using PdxPlayset = PDX.SDK.Contracts.Service.Mods.Models.Playset;

namespace Skyve.Domain.CS2.Content;
public class Playset : ICustomPlayset, IEquatable<Playset?>
{
    private Bitmap? _banner;
    private byte[]? _bannerBytes;

    [CloneIgnore] public int Id { get; set; }
    [CloneIgnore] public string? Name { get; set; }
    [CloneIgnore] public bool Temporary { get; set; }
    [JsonIgnore] public DateTime LastEditDate { get; set; }
    [JsonIgnore] public DateTime DateCreated { get; set; }
    [JsonIgnore, CloneIgnore] public bool IsMissingItems { get; set; }
    [JsonIgnore] public int AssetCount => Assets.Count;
    [JsonIgnore] public int ModCount => Mods.Count;
    [JsonIgnore] public IEnumerable<IPlaysetEntry> Entries => Assets.Concat(Mods);
    [JsonIgnore] public IEnumerable<IPackage> Packages => Assets.Concat(Mods);

    public Playset(string name)
    {
        Name = name;
        Assets = new();
        Mods = new();
        ExcludedDLCs = new();
        AutoSave = true;
    }

    //public Playset()
    //{
    //	Name = string.Empty;
    //	Assets = new();
    //	Mods = new();
    //	ExcludedDLCs = new();
    //	AutoSave = true;
    //}

    public Playset(PdxPlayset playset)
    {
        Id = playset.PlaysetId;
        Name = playset.Name;
    }

    public Playset(CreatePlaysetResult playset)
    {
        Id = playset.PlaysetId;
        Name = playset.Name;
    }

    public bool Save()
    {
        return true;
        //	var playsetManager = ServiceCenter.Get<IPlaysetManager>();

        //	playsetManager.GatherInformation(this);

        //	UnsavedChanges = false;

        //	return playsetManager.Save(this);
    }

    [CloneIgnore] public List<Asset> Assets { get; set; }
    [CloneIgnore] public List<Mod> Mods { get; set; }
    [CloneIgnore] public List<uint> ExcludedDLCs { get; set; }
    public bool AutoSave { get; set; }
    public bool IsFavorite { get; set; }
    public Color? Color { get; set; }
    public DateTime LastUsed { get; set; }
    public PackageUsage Usage { get; set; }
    public ulong Author { get; set; }
    public int ProfileId { get; set; }
    public bool UnsavedChanges { get; set; }
    public bool DisableWorkshop { get; set; }
    public byte[]? BannerBytes
    {
        get => _bannerBytes; set
        {
            _bannerBytes = value;
            _banner?.Dispose();
            _banner = null;
        }
    }
    public bool Public { get; set; }

    public bool ForAssetEditor
    {
        set
        {
            if (value)
            {
                Usage = PackageUsage.AssetCreation;
            }
        }
    }
    public bool ForGameplay
    {
        set
        {
            if (value)
            {
                Usage = PackageUsage.CityBuilding;
            }
        }
    }
    [JsonIgnore] public int Downloads { get; }
    [JsonIgnore]
    public Bitmap? Banner
    {
        get
        {
            if (_banner is not null)
            {
                return _banner;
            }

            if (BannerBytes is null || BannerBytes.Length == 0)
            {
                return null;
            }

            using var ms = new MemoryStream(BannerBytes);

            return _banner = new Bitmap(ms);
        }
        set
        {
            if (value is null)
            {
                BannerBytes = null;
            }
            else
            {
                BannerBytes = (byte[])new ImageConverter().ConvertTo(value, typeof(byte[]));
            }
        }
    }

    IUser? IPlayset.Author => ServiceCenter.Get<IWorkshopService>().GetUser(Author);
    string? IPlayset.BannerUrl { get; }
    DateTime IPlayset.DateUpdated { get; }
    DateTime IPlayset.DateUsed { get; }

    public class Asset : IPackage, IPlaysetEntry
    {
        private string? _name;

        [JsonIgnore] public string Name { get => ToString(); protected set => _name = value; }
        public string? RelativePath { get; set; }
        public ulong SteamId { get; set; }

        [JsonIgnore] public bool IsCodeMod { get; set; }
        [JsonIgnore, CloneIgnore] public bool IsLocal => SteamId == 0;
        [JsonIgnore, CloneIgnore] public bool IsBuiltIn { get; }
        [JsonIgnore, CloneIgnore] public virtual ILocalPackageData? LocalParentPackage => null;// ServiceCenter.Get<IPlaysetManager>().GetAsset(this)?.GetLocalPackage();
        [JsonIgnore, CloneIgnore] public virtual ILocalPackageData? LocalPackage => null;// ServiceCenter.Get<IPlaysetManager>().GetAsset(this);
        [JsonIgnore, CloneIgnore] public ulong Id => SteamId;
        [JsonIgnore, CloneIgnore] public string? Url => SteamId == 0 ? null : $"https://steamcommunity.com/workshop/filedetails/?id={Id}";
        [JsonIgnore, CloneIgnore] public string FilePath => RelativePath!;

        [JsonIgnore, CloneIgnore] public ILocalPackageData? LocalData { get; }
        [JsonIgnore, CloneIgnore] public IWorkshopInfo? WorkshopInfo { get; }
        [JsonIgnore, CloneIgnore] public string Folder { get; }

        public Asset(IAsset asset)
        {
            SteamId = asset.Id;
            Name = asset.Name;
            RelativePath = ServiceCenter.Get<ILocationService>().ToRelativePath(asset.FilePath);
        }

        public Asset()
        {

        }

        public Asset(UserProfileContent content)
        {
            RelativePath = content.RelativePath;
            SteamId = content.SteamId;
        }

        public override string ToString()
        {
            var name = this.GetWorkshopInfo()?.Name;

            return name is not null
                ? name
                : _name is not null
                ? _name
                : !string.IsNullOrEmpty(RelativePath)
                ? Path.GetFileNameWithoutExtension(RelativePath)
                : (string)LocaleHelper.GetGlobalText("UnknownPackage");
        }

        public virtual UserProfileContent AsProfileContent()
        {
            return new UserProfileContent
            {
                RelativePath = RelativePath,
                SteamId = SteamId,
            };
        }

        public bool GetThumbnail(out Bitmap? thumbnail, out string? thumbnailUrl)
        {
            var info = this.GetWorkshopInfo();

            if (info is not null)
            {
                return info.GetThumbnail(out thumbnail, out thumbnailUrl);
            }

            thumbnail = null;
            thumbnailUrl = null;
            return false;
        }
    }

    public class Mod : Asset
    {
        public bool Enabled { get; set; }
        [JsonIgnore, CloneIgnore] public override ILocalPackageData? LocalParentPackage => null; //ServiceCenter.Get<IPlaysetManager>().GetMod(this)?.GetLocalPackage();
        [JsonIgnore, CloneIgnore] public override ILocalPackageData? LocalPackage => null; //ServiceCenter.Get<IPlaysetManager>().GetMod(this);

        //public Mod(IMod mod)
        //{
        //	IsCodeMod = true;
        //	SteamId = mod.Id;
        //	Name = mod.Name;
        //	Enabled = ServiceCenter.Get<IModUtil>().IsEnabled(mod);
        //	RelativePath = ServiceCenter.Get<ILocationService>().ToRelativePath(mod.Folder);
        //}

        public Mod(IPackage package)
        {
            IsCodeMod = true;
            SteamId = package.Id;
            Name = package.Name;
            Enabled = true;
            RelativePath = CrossIO.Combine("%WORKSHOP%", package.Id.ToString());
        }

        public Mod()
        {
            IsCodeMod = true;
        }

        public Mod(UserProfileContent content)
        {
            IsCodeMod = true;
            Enabled = content.Enabled;
            RelativePath = content.RelativePath;
            SteamId = content.SteamId;
        }

        public override UserProfileContent AsProfileContent()
        {
            return new UserProfileContent
            {
                Enabled = Enabled,
                IsCodeMod = true,
                RelativePath = RelativePath,
                SteamId = SteamId,
            };
        }
    }

    public override string ToString()
    {
        return Name ?? base.ToString();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Playset);
    }

    public bool Equals(Playset? other)
    {
        return other is not null &&
               Id == other.Id;
    }

    public override int GetHashCode()
    {
        return 2108858624 + Id.GetHashCode();
    }

    public static bool operator ==(Playset? left, Playset? right)
    {
        return EqualityComparer<Playset>.Default.Equals(left, right);
    }

    public static bool operator !=(Playset? left, Playset? right)
    {
        return !(left == right);
    }
}
