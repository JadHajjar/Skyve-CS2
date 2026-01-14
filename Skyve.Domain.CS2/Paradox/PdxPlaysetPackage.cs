using PDX.SDK.Contracts.Service.Mods.Interfaces;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.CS2.Content;

namespace Skyve.Domain.CS2.Paradox;
public class PdxPlaysetPackage : IPlaysetPackage
{
	public PdxPlaysetPackage(ILocalPlaysetMod mod, string playsetId)
	{
		IsEnabled = mod.IsEnabled;
		LoadOrder = mod.LoadOrder;
		PlaysetId = playsetId;
		IsVersionLocked = mod.PreferredVersion is not null;

		if (mod is not IMod subscribedMod)
		{
			Name = mod.Id;
			LocalData = string.IsNullOrEmpty(mod.LocalData.FolderAbsolutePath) ? null
				: new LocalPackageData(this, [], 0, [], mod.LocalData.FolderAbsolutePath, null, string.Empty, null);
			return;
		}

		Id = ulong.Parse(subscribedMod.Id);
		Name = subscribedMod.DisplayName;
		Version = subscribedMod.Version;
		VersionName = subscribedMod.UserModVersion;
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
		LocalData = string.IsNullOrEmpty(mod.LocalData.FolderAbsolutePath) ? null
			: new LocalPackageData(this, [], 0, [], mod.LocalData.FolderAbsolutePath, subscribedMod.UserModVersion, string.Empty, null);
	}

	public ulong Id { get; }
	public string Name { get; }
	public string? Url { get; }
	public string? Version { get; set; }
	public string? VersionName { get; }
	public bool IsCodeMod { get; }
	public bool IsLocal { get; }
	public bool IsBuiltIn { get; }
	public bool IsVersionLocked { get; set; }
	public bool IsEnabled { get; }
	public int LoadOrder { get; }
	public string PlaysetId { get; }
	public ILocalPackageData? LocalData { get; }
}
