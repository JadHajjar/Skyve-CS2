using Skyve.App.CS2.UserInterface.Generic;
using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.UserInterface.Dashboard;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
[DashboardItem("BackupCenter")]
internal class BD_DiskInfo : IDashboardItem
{
	private readonly ILogger _logger;
	private readonly ISettings _settings;
	private readonly INotifier _notifier;
	private readonly IBackupSystem _backupSystem;

	private ContentInfo? info;
	private readonly BackupSettings _backupSettings;

	private class ContentInfo
	{
		internal long AvailableSpace;
		internal long TotalSpace;
		internal long TotalBackupSize;
		internal bool CriticalSpace;
		internal bool LowSpace;
		internal string? DriveLetter;
		internal long ArchivedBackupSize;
		internal Dictionary<IBackupMetaData, long> BackupTypeSizes = [];
	}

	public BD_DiskInfo()
	{
		ServiceCenter.Get(out _logger, out _settings, out _notifier, out _backupSystem);

		_backupSettings = (BackupSettings)_settings.BackupSettings;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		_notifier.BackupEnded += LoadData;

		LoadData();
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		_notifier.BackupEnded -= LoadData;
	}

	protected override void OnDataLoadError(Exception ex)
	{
		OnResizeRequested();
	}

	protected override Task<bool> ProcessDataLoad(CancellationToken token)
	{
		if (string.IsNullOrWhiteSpace(_backupSettings.DestinationFolder))
		{
			return base.ProcessDataLoad(token);
		}

		var contentInfo = new ContentInfo();
		var drive = new DriveInfo(_backupSettings.DestinationFolder?.Substring(0, 1));

		contentInfo.DriveLetter = drive.Name;
		contentInfo.AvailableSpace = drive.AvailableFreeSpace;
		contentInfo.TotalSpace = drive.TotalSize;
		contentInfo.CriticalSpace = contentInfo.AvailableSpace < 15L * 1024L * 1024L * 1024L;
		contentInfo.LowSpace = contentInfo.AvailableSpace < 75L * 1024L * 1024L * 1024L;

		var backups = _backupSystem.GetAllBackups();

		contentInfo.TotalBackupSize = backups.Sum(x => x.BackupFile.Length);
		contentInfo.ArchivedBackupSize = backups.Where(x => x.MetaData.IsArchived).Sum(x => x.BackupFile.Length);
		contentInfo.BackupTypeSizes = backups.Where(x => !x.MetaData.IsArchived).GroupBy(x => x.MetaData.Type).ToDictionary(x => x.First().MetaData, x => x.Sum(x => x.BackupFile.Length));

		if (token.IsCancellationRequested)
		{
			return Task.FromResult(false);
		}

		info = contentInfo;

		OnResizeRequested();

		return Task.FromResult(true);
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		if (width > 500 * UI.FontScale)
		{
			return DrawLandscape;
		}

		return Draw;
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		if (Loading)
		{
			DrawLoadingSection(e, applyDrawing, ref preferredHeight, LocaleCS2.DiskStatus);
		}
		else
		{
			DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.DiskStatus, "Drive");
		}
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawHeader(e, applyDrawing, ref preferredHeight);

		if (info is null)
		{
			if (!Loading)
			{
				e.Graphics.DrawStringItem(Locale.SetupSettingsFirst
					, Font
					, FormDesign.Design.OrangeColor
					, e.ClipRectangle.Pad(Padding)
					, ref preferredHeight
					, applyDrawing);

				preferredHeight += BorderRadius;
			}

			return;
		}

		var fadedColor = FormDesign.Design.ForeColor.MergeColor(FormDesign.Design.BackColor, 75);
		var valueRect = e.ClipRectangle.Pad(Margin);

		foreach (var item in info.BackupTypeSizes.OrderByDescending(x => x.Value))
		{
			DrawValue(e, valueRect, item.Key.GetTypeTranslation(), item.Value.SizeString(), applyDrawing, ref preferredHeight, item.Key.GetIcon(), fadedColor, false);
		}

		DrawValue(e, valueRect, Locale.Archived, info.ArchivedBackupSize.SizeString(), applyDrawing, ref preferredHeight, "Archived", fadedColor, false);

		preferredHeight += BorderRadius;

		DrawValue(e, valueRect, LocaleCS2.TotalBackupSize, info.TotalBackupSize.SizeString(), applyDrawing, ref preferredHeight, "SafeShield");

		preferredHeight += BorderRadius;

		var graphSize = Math.Min((e.ClipRectangle.Width / 2) - (Margin.Horizontal * 2), UI.Scale(60));
		using var pen = new Pen(FormDesign.Design.AccentColor, graphSize / 5f);
		using var activePen = new Pen(info.CriticalSpace ? FormDesign.Design.RedColor : info.LowSpace ? FormDesign.Design.OrangeColor : FormDesign.Design.ActiveColor, graphSize / 5f)
		{
			StartCap = info.LowSpace ? default : System.Drawing.Drawing2D.LineCap.Round,
			EndCap = info.LowSpace ? default : System.Drawing.Drawing2D.LineCap.Round
		};

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
	}

	private void DrawLandscape(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawHeader(e, applyDrawing, ref preferredHeight);

		if (info is null)
		{
			if (!Loading)
			{
				e.Graphics.DrawStringItem(Locale.SetupSettingsFirst
					, Font
					, FormDesign.Design.OrangeColor
					, e.ClipRectangle.Pad(Padding)
					, ref preferredHeight
					, applyDrawing);

				preferredHeight += BorderRadius;
			}

			return;
		}

		var startingY = preferredHeight;
		var fadedColor = FormDesign.Design.ForeColor.MergeColor(FormDesign.Design.BackColor, 75);
		var valueRect = e.ClipRectangle.Pad(Margin);

		valueRect.Width -= e.ClipRectangle.Width / 2;

		foreach (var item in info.BackupTypeSizes.OrderByDescending(x => x.Value))
		{
			DrawValue(e, valueRect, item.Key.GetTypeTranslation(), item.Value.SizeString(), applyDrawing, ref preferredHeight, item.Key.GetIcon(), fadedColor, false);

			preferredHeight += BorderRadius / 2;
		}

		DrawValue(e, valueRect, Locale.Archived, info.ArchivedBackupSize.SizeString(), applyDrawing, ref preferredHeight, "Archived", fadedColor, false);

		preferredHeight += BorderRadius;

		DrawValue(e, valueRect, LocaleCS2.TotalBackupSize, info.TotalBackupSize.SizeString(), applyDrawing, ref preferredHeight, "SafeShield");

		preferredHeight += BorderRadius;

		var graphSize = Math.Min((e.ClipRectangle.Width / 2) - (Margin.Horizontal * 2), UI.Scale(60));
		using var pen = new Pen(FormDesign.Design.AccentColor, graphSize / 5f);
		using var activePen = new Pen(info.CriticalSpace ? FormDesign.Design.RedColor : info.LowSpace ? FormDesign.Design.OrangeColor : FormDesign.Design.ActiveColor, graphSize / 5f)
		{
			StartCap = info.LowSpace ? default : System.Drawing.Drawing2D.LineCap.Round,
			EndCap = info.LowSpace ? default : System.Drawing.Drawing2D.LineCap.Round
		};

		var leftHeight = preferredHeight;
		preferredHeight = startingY;

		var graphRect = new Rectangle(valueRect.Right + ((e.ClipRectangle.Right - valueRect.Right - graphSize) / 2), preferredHeight, graphSize, graphSize);

		e.Graphics.DrawEllipse(pen, graphRect);
		e.Graphics.DrawArc(activePen, graphRect, -90F, 360f - (360f * (float)((double)info.AvailableSpace / info.TotalSpace)));

		if (info.LowSpace)
		{
			using var warning = IconManager.GetIcon("Warning", graphSize / 2).Color(activePen.Color);

			e.Graphics.DrawImage(warning, graphRect.CenterR(warning.Size));
		}

		preferredHeight += graphRect.Height + Padding.Vertical;

		var sideRect = new Rectangle(valueRect.Right + Padding.Horizontal, preferredHeight, e.ClipRectangle.Right - valueRect.Right - (Padding.Horizontal * 2), graphSize / 2);
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

		preferredHeight = Math.Max(preferredHeight, leftHeight);
	}
}
