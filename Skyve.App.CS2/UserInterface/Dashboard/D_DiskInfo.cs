using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.UserInterface.Dashboard;
using Skyve.Domain.CS2.Utilities;
using Skyve.Systems.CS2.Utilities;

using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_DiskInfo : IDashboardItem
{
	private readonly ILogger _logger;
	private readonly ISettings _settings;
	private readonly INotifier _notifier;

	private ContentInfo? info;

	private class ContentInfo
	{
		internal bool Error;
		internal long AvailableSpace;
		internal long TotalSpace;
		internal bool IsJunctionSet;
		internal long TotalCitiesSize;
		internal long TotalSavesSize;
		internal long TotalSubbedSize;
		internal long TotalOtherSize;
		internal bool CriticalSpace;
		internal bool LowSpace;
		internal string? DriveLetter;
		internal bool HasMultipleDrives;
	}

	public D_DiskInfo()
	{
		ServiceCenter.Get(out _logger, out _settings, out _notifier);
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		_notifier.WorkshopSyncEnded += LoadData;

		LoadData();
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		_notifier.WorkshopSyncEnded -= LoadData;
	}

	protected override void OnDataLoadError(Exception ex)
	{
		info = new ContentInfo { Error = true };
		Loading = false;

		OnResizeRequested();

		_logger.Exception(ex, "Failed to get Disk Info Summary");
	}

	protected override async Task ProcessDataLoad(CancellationToken token)
	{
		var contentInfo = new ContentInfo();
		var junctionLocation = JunctionHelper.GetJunctionState(_settings.FolderSettings.AppDataPath);
		var drive = new DriveInfo(junctionLocation.IfEmpty(_settings.FolderSettings.AppDataPath).Substring(0, 1));

		contentInfo.HasMultipleDrives = DriveInfo.GetDrives().Count(x => x.DriveType is DriveType.Removable or DriveType.Fixed && x.TotalSize > 150 * 1024L * 1024L * 1024L) > 1;
		contentInfo.DriveLetter = drive.Name;
		contentInfo.AvailableSpace = drive.AvailableFreeSpace;
		contentInfo.TotalSpace = drive.TotalSize;
		contentInfo.IsJunctionSet = !string.IsNullOrEmpty(junctionLocation);

		if (token.IsCancellationRequested)
		{
			return;
		}

		var savesFolder = CrossIO.Combine(_settings.FolderSettings.AppDataPath, "Saves");
		var subbedFolder = CrossIO.Combine(_settings.FolderSettings.AppDataPath, ".cache", "Mods", "mods_subscribed");

		foreach (var item in new DirectoryInfo(_settings.FolderSettings.AppDataPath).EnumerateFiles("*", SearchOption.AllDirectories))
		{
			contentInfo.TotalCitiesSize += item.Length;

			if (item.FullName.PathContains(savesFolder))
			{
				contentInfo.TotalSavesSize += item.Length;
			}
			else if (item.FullName.PathContains(subbedFolder))
			{
				contentInfo.TotalSubbedSize += item.Length;
			}
		}

		contentInfo.TotalOtherSize = contentInfo.TotalCitiesSize - contentInfo.TotalSavesSize - contentInfo.TotalSubbedSize;
		contentInfo.CriticalSpace = contentInfo.AvailableSpace < 10L * 1024L * 1024L * 1024L;
		contentInfo.LowSpace = contentInfo.AvailableSpace < 20L * 1024L * 1024L * 1024L;

		if (token.IsCancellationRequested)
		{
			return;
		}

		info = contentInfo;

		OnResizeRequested();

		await Task.CompletedTask;
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		return Draw;
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.DiskStatus, "Drive");
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		if (Loading)
		{
			DrawLoadingSection(e, applyDrawing, ref preferredHeight, LocaleCS2.DiskStatus);
		}
		else
		{
			DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.DiskStatus, "Drive");
		}

		if (info is null)
		{
			return;
		}

		var fadedColor = FormDesign.Design.ForeColor.MergeColor(FormDesign.Design.BackColor, 75);
		DrawValue(e, e.ClipRectangle.Pad(Margin), LocaleCS2.TotalSubbedSize, info.TotalSubbedSize.SizeString(), applyDrawing, ref preferredHeight, "PDXMods", fadedColor, false);
		DrawValue(e, e.ClipRectangle.Pad(Margin), LocaleCS2.TotalSavesSize, info.TotalSavesSize.SizeString(), applyDrawing, ref preferredHeight, "City", fadedColor, false);
		DrawValue(e, e.ClipRectangle.Pad(Margin), LocaleCS2.TotalOtherSize, info.TotalOtherSize.SizeString(), applyDrawing, ref preferredHeight, "Folder", fadedColor, false);
		preferredHeight += BorderRadius;
		DrawValue(e, e.ClipRectangle.Pad(Margin), LocaleCS2.TotalCitiesSize, info.TotalCitiesSize.SizeString(), applyDrawing, ref preferredHeight, "CS");

		preferredHeight += BorderRadius;

		var graphSize = Math.Min((e.ClipRectangle.Width / 2) - (Margin.Horizontal * 2), (int)(60 * UI.FontScale));
		using var pen = new Pen(FormDesign.Design.AccentColor, graphSize / 5f);
		using var activePen = new Pen(info.CriticalSpace ? FormDesign.Design.RedColor : info.LowSpace ? FormDesign.Design.OrangeColor : FormDesign.Design.ActiveColor, graphSize / 5f) { StartCap = System.Drawing.Drawing2D.LineCap.Round, EndCap = System.Drawing.Drawing2D.LineCap.Round };

		preferredHeight += (int)pen.Width;

		var graphRect = new Rectangle(e.ClipRectangle.X + (((e.ClipRectangle.Width / 2) - graphSize) / 2), preferredHeight, graphSize, graphSize);

		e.Graphics.DrawEllipse(pen, graphRect);
		e.Graphics.DrawArc(activePen, graphRect, -90F, 360f - (360f * (float)((double)info.AvailableSpace / info.TotalSpace)));

		if (info.LowSpace)
		{
			using var warning = IconManager.GetIcon("Warning", graphSize / 2).Color(activePen.Color);

			e.Graphics.DrawImage(warning, graphRect.CenterR(warning.Size));
		}

		var sideRect = new Rectangle(e.ClipRectangle.X + (e.ClipRectangle.Width / 2), preferredHeight, e.ClipRectangle.Width / 2, graphSize / 2);
		var text1 = LocaleCS2.FreeSpace.Format(info.AvailableSpace.SizeString(0));
		var text2 = LocaleCS2.UsedOutOfSpace.Format(info.TotalSpace.SizeString(0), info.DriveLetter);

		using var bigFont = UI.Font(10.5F, FontStyle.Bold).FitToWidth(text1, sideRect, e.Graphics);
		using var brush1 = new SolidBrush(info.CriticalSpace ? FormDesign.Design.RedColor : info.LowSpace ? FormDesign.Design.OrangeColor : FormDesign.Design.ForeColor);
		using var format1 = new StringFormat { LineAlignment = StringAlignment.Far };

		e.Graphics.DrawString(text1, bigFont, brush1, sideRect, format1);

		sideRect.Y += sideRect.Height;
		sideRect.Height -= BorderRadius / 2;

		using var smallFont = UI.Font(8.5F).FitTo(text2, sideRect, e.Graphics);
		using var brush2 = new SolidBrush(FormDesign.Design.ForeColor.MergeColor(FormDesign.Design.BackColor));

		e.Graphics.DrawString(text2, smallFont, brush2, sideRect);

		preferredHeight += BorderRadius + graphSize + (int)pen.Width;

		if (info.CriticalSpace && !info.IsJunctionSet)
		{
			using var font = UI.Font(7.5F);

			e.Graphics.DrawStringItem(info.HasMultipleDrives ? LocaleCS2.LowSpaceCreateJunction.One : LocaleCS2.LowSpaceCreateJunction.Zero, font, ForeColor, e.ClipRectangle.Pad(Margin), ref preferredHeight, applyDrawing, "Info");

			if (info.HasMultipleDrives)
			{
				DrawButton(e, applyDrawing, ref preferredHeight, OpenOptionsPanel, new ButtonDrawArgs
				{
					Icon = "Cog",
					Font = font,
					Size = new Size(0, (int)(20 * UI.FontScale)),
					Text = LocaleCS2.ChangeLocation,
					Rectangle = e.ClipRectangle.Pad(BorderRadius)
				});
			}

			preferredHeight += BorderRadius / 2;
		}
	}

	private void OpenOptionsPanel()
	{
		App.Program.MainForm.PushPanel<PC_Options>((App.Program.MainForm as MainForm)!.PI_Options);
	}
}
