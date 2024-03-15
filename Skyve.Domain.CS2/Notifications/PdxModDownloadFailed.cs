using Extensions;

using Skyve.Domain.CS2.Utilities;
using Skyve.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Skyve.Domain.CS2.Notifications;
public class PdxModDownloadFailed : INotificationInfo
{
	public DateTime Time { get; set; }
	public string Title => LocaleCS2.ModDownloadFailed.FormatPlural(Mods.Count);
	public string? Description => Mods.ListStrings(x => new GenericPackageIdentity(x).GetWorkshopInfo()?.CleanName() ?? x.ToString(), ", ");
	public string Icon { get; } = "ReDownload";
	public Color? Color => FormDesign.Design.OrangeColor;
	public bool HasAction { get; }
	public List<ulong> Mods { get; } = [];

    public void OnClick()
	{
	}

	public void OnRightClick()
	{
	}
}
