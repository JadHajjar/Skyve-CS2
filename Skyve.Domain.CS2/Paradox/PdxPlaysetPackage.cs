using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Domain.Systems;

using System;
using System.Drawing;

namespace Skyve.Domain.CS2.Paradox;
public class PdxPlaysetPackage : IPlaysetPackage
{
	public PdxPlaysetPackage(IPlaysetMod mod)
	{
		if (mod is not PlaysetSubscribedMod subscribedMod)
		{
			Name = mod.DisplayName;
			return;
		}

		Id = (ulong)subscribedMod.Id;
		Name = subscribedMod.DisplayName;
		Version = subscribedMod.Version;
		VersionName = subscribedMod.UserModVersion;
		IsVersionLocked = subscribedMod.PreferredVersion is not null;
		IsEnabled = mod.IsEnabled;
		LoadOrder = mod.LoadOrder;
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
	}

	public ulong Id { get; }
	public string Name { get; }
	public string? Url { get; }
	public string? Version { get; set; }
	public string? VersionName { get; }
	public bool IsCodeMod { get; }
	public bool IsLocal { get; }
	public bool IsBuiltIn { get; }
	public bool IsVersionLocked { get; }
	public bool IsEnabled { get; }
	public int LoadOrder { get; }
	public ILocalPackageData? LocalData { get; }
}
