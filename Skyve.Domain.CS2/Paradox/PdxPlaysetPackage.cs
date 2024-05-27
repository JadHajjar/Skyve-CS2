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
		IsEnabled = mod.IsEnabled;
		LoadOrder = mod.LoadOrder;
		Url = $"https://mods.paradoxplaza.com/mods/{Id}/Windows";
	}

	public ulong Id { get; }
	public string Name { get; }
	public string? Url { get; }
	public string? Version { get; }
	public bool IsCodeMod { get; }
	public bool IsLocal { get; }
	public bool IsEnabled { get; }
	public int LoadOrder { get; }
	public ILocalPackageData? LocalData { get; }
}
