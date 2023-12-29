using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Content;

public class LocalPackageData : ILocalPackageData
{
    public IPackage Package { get; }
    public long LocalSize { get; }
    public DateTime LocalTime { get; }
    public string Version { get; }
    public IAsset[] Assets { get; set; }
    public string Folder { get; }
    public string FilePath { get; }

    public LocalPackageData(IPackage package, IAsset[] assets, string folder, long localSize, DateTime localTime, string version, string filePath)
    {
        Package = package;
        LocalSize = localSize;
        LocalTime = localTime;
        Version = version;
        Assets = assets;
        Folder = folder;
        FilePath = filePath;
    }

    public bool GetThumbnail(out Bitmap? thumbnail, out string? thumbnailUrl)
    {
        return Package.GetThumbnail(out thumbnail, out thumbnailUrl);
    }

    bool ILocalPackageData.IsCodeMod => Package.IsCodeMod;
    ulong IPackageIdentity.Id => Package.Id;
    string IPackageIdentity.Name => Package.Name;
    string? IPackageIdentity.Url => Package.Url;
}
