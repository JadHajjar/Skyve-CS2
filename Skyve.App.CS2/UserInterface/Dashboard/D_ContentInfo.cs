using Skyve.App.UserInterface.Dashboard;
using Skyve.App.UserInterface.Panels;

using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;

internal class D_ContentInfo : IDashboardItem
{
	private readonly INotifier _notifier;
	private readonly IPackageUtil _packageUtil;
	private readonly IPackageManager _packageManager;

	private ContentInfo? info;

	private class ContentInfo
	{
		internal List<IPackageIdentity> RecentlyUpdated = [];

		internal int ModsTotal;
		internal int ModsEnabled;
		internal int ModsDisabled;
		internal int AssetsTotal;
		internal int AssetsEnabled;
		internal int CodeModsTotal;
		internal int CodeModsEnabled;
		internal int RecentlyUpdatedCodeMods;
		internal int OutOfDateMods;
	}

	public D_ContentInfo()
	{
		ServiceCenter.Get(out _notifier, out _packageUtil, out _packageManager);
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (_notifier.IsContentLoaded)
		{
			LoadData();
		}
		else
		{
			Loading = true;
		}

		_notifier.ContentLoaded += LoadData;
		_notifier.WorkshopInfoUpdated += LoadData;
		_notifier.PackageInformationUpdated += LoadData;
		_notifier.PackageInclusionUpdated += LoadData;
		_notifier.PlaysetChanged += LoadData;
	}

	protected override Task<bool> ProcessDataLoad(CancellationToken token)
	{
		if (!_notifier.IsContentLoaded)
		{
			return Task.FromResult(false);
		}

		var contentInfo = new ContentInfo();

		foreach (var mod in _packageManager.Packages)
		{
			contentInfo.ModsTotal++;
			contentInfo.AssetsTotal += mod.LocalData?.AssetCount ?? 0;

			if (mod.GetWorkshopInfo()?.ServerTime > DateTime.UtcNow.AddDays(-7))
			{
				contentInfo.RecentlyUpdated.Add(mod);

				if (mod.IsCodeMod)
				{
					contentInfo.RecentlyUpdatedCodeMods++;
					contentInfo.CodeModsTotal++;
				}
			}
			else if (mod.IsCodeMod)
			{
				contentInfo.CodeModsTotal++;
			}

			if (!_packageUtil.IsIncluded(mod))
			{
				continue;
			}

			if (_packageUtil.IsEnabled(mod))
			{
				contentInfo.ModsEnabled++;
				contentInfo.AssetsEnabled += mod.LocalData?.Assets.Count(x => x.AssetType is not AssetType.SaveGame) ?? 0;

				if (mod.IsCodeMod)
				{
					contentInfo.CodeModsEnabled++;
				}

				switch (_packageUtil.GetStatus(mod, out _))
				{
					case DownloadStatus.OutOfDate:
						contentInfo.OutOfDateMods++;
						break;
					case DownloadStatus.PartiallyDownloaded:
						break;
				}
			}
			else
			{
				contentInfo.ModsDisabled++;
			}
		}

		if (!token.IsCancellationRequested)
		{
			info = contentInfo;

			OnResizeRequested();
		}

		return base.ProcessDataLoad(token);
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		_notifier.ContentLoaded -= LoadData;
		_notifier.WorkshopInfoUpdated -= LoadData;
		_notifier.PackageInformationUpdated -= LoadData;
		_notifier.PackageInclusionUpdated -= LoadData;
		_notifier.PlaysetChanged -= LoadData;
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		if (info is null)
		{
			return DrawLoading;
		}

		//if (width > 450 * UI.FontScale)
		//{
		//	return DrawLandscape;
		//}

		return Draw;
	}

	private void DrawLoading(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawLoadingSection(e, applyDrawing, ref preferredHeight, Locale.ContentSummary);
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, ref preferredHeight, Locale.ContentSummary, "Package");
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, ref preferredHeight, Locale.ContentSummary, "Package");

		if (info is null)
		{
			return;
		}

		var fore = FormDesign.Design.ForeColor;
		var textRect = e.ClipRectangle.Pad(Margin);

		DrawValue(e, textRect, Locale.PackagesEnabled, Locale.OutOf.Format(info.ModsEnabled, info.ModsTotal), applyDrawing, ref preferredHeight, boldValue: false);
		DrawValue(e, textRect, Locale.AssetsEnabled, Locale.OutOf.Format(info.AssetsEnabled, info.AssetsTotal), applyDrawing, ref preferredHeight, boldValue: false);
		DrawValue(e, textRect, Locale.ModsEnabled, Locale.OutOf.Format(info.CodeModsEnabled, info.CodeModsTotal), applyDrawing, ref preferredHeight, boldValue: false);
		DrawValue(e, textRect, Locale.PackagesIncludedDisabled, info.ModsDisabled.ToString(), applyDrawing, ref preferredHeight, boldValue: false);

		preferredHeight += BorderRadius;

		if (info.OutOfDateMods > 0)
		{
			DrawValue(e, textRect, Locale.OutOfDateItem.Format(Locale.Package.Plural.ToLower()), info.OutOfDateMods.ToString(), applyDrawing, ref preferredHeight, "OutOfDate", FormDesign.Design.OrangeColor);
		}

		if (info.RecentlyUpdated.Count > 0)
		{
			preferredHeight += BorderRadius;

			using var font = UI.Font(7.5F);
			using var valueFont = UI.Font(8.75F, FontStyle.Bold);
			using var icon = IconManager.GetIcon("PDXMods", valueFont.Height * 5 / 4);
			DrawValue(e, textRect, Locale.RecentlyUpdatedItem.Format(Locale.Package.Plural), info.RecentlyUpdated.Count.ToString(), applyDrawing, ref preferredHeight, "PDXMods", FormDesign.Design.ActiveColor);
			DrawValue(e, textRect.Pad(icon.Width + (BorderRadius / 2), 0, 0, 0), Locale.RecentlyUpdatedItem.Format(Locale.CodeMod.Plural), info.RecentlyUpdatedCodeMods.ToString(), applyDrawing, ref preferredHeight);

			DrawButton(e, applyDrawing, ref preferredHeight, ViewRecentlyUpdated, new ButtonDrawArgs
			{
				Icon = "Link",
				Font = font,
				ButtonType = ButtonType.Dimmed,
				Size = new Size(0, UI.Scale(20)),
				Text = Locale.ViewRecentlyUpdatedItems.Format(Locale.Package.FormatPlural(info.RecentlyUpdated.Count)),
				Rectangle = e.ClipRectangle.Pad(BorderRadius)
			});

			preferredHeight -= BorderRadius / 2;
		}

		preferredHeight += BorderRadius;
	}

	private void ViewRecentlyUpdated()
	{
		App.Program.MainForm.PushPanel(new PC_ViewSpecificPackages(info!.RecentlyUpdated, Locale.RecentlyUpdatedItem.Format(Locale.Package.Plural)));
	}
}
