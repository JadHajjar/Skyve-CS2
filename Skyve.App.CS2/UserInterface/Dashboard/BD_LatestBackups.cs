using Skyve.App.CS2.UserInterface.Generic;
using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.UserInterface.Dashboard;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;

[DashboardItem("BackupCenter")]
internal class BD_LatestBackups : IDashboardItem
{
	private readonly ILogger _logger;
	private readonly ISettings _settings;
	private readonly INotifier _notifier;
	private readonly IBackupSystem _backupSystem;

	private ContentInfo? info;
	private readonly BackupSettings _backupSettings;

	private class ContentInfo
	{
		internal List<BackupListControl.RestoreGroup> LastBackups = [];
	}

	public BD_LatestBackups()
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
		var backups = _backupSystem.GetAllBackups();

		contentInfo.LastBackups = backups
				.Where(x => !x.MetaData.IsArchived)
				.GroupBy(x => x.MetaData.BackupTime)
				.OrderByDescending(x => x.Key)
				.Take(5)
				.ToList(x => new BackupListControl.RestoreGroup(x.Key, new List<IRestoreItem>(x)));

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
		return Draw;
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		if (Loading)
		{
			DrawLoadingSection(e, applyDrawing, ref preferredHeight, LocaleCS2.RecentBackups);
		}
		else
		{
			DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.RecentBackups, "History");
		}
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawHeader(e, applyDrawing, ref preferredHeight);

		if (info is null || info.LastBackups.Count == 0)
		{
			if (!Loading)
			{
				e.Graphics.DrawStringItem(LocaleSlickUI.NothingToSeeHere
					, Font
					, FormDesign.Design.InfoColor
					, e.ClipRectangle.Pad(Padding)
					, ref preferredHeight
					, applyDrawing);

				preferredHeight += BorderRadius;
			}

			return;
		}

		if (info.LastBackups[0].Time < DateTime.Now.AddDays(-7))
		{
			e.Graphics.DrawStringItem($"{LocaleCS2.LastSettingsBackup.Format(info.LastBackups[0].Time.ToRelatedString(true).ToLower())}!\r\n{Locale.DoBackupNow}."
				, Font
				, FormDesign.Design.RedColor
				, e.ClipRectangle.Pad(Padding)
				, ref preferredHeight
				, applyDrawing
				, "Warning");
		}
		else
		{
			e.Graphics.DrawStringItem(LocaleCS2.LastSettingsBackup.Format(info.LastBackups[0].Time.ToRelatedString(true).ToLower())
				, Font
				, FormDesign.Design.OrangeColor.MergeColor(FormDesign.Design.GreenColor, (int)((DateTime.Now - info.LastBackups[0].Time).TotalDays * 100 / 7))
				, e.ClipRectangle.Pad(Padding)
				, ref preferredHeight
				, applyDrawing
				, "Check");
		}

		preferredHeight += BorderRadius;

		foreach (var item in info.LastBackups)
		{
			var title = item.Time.ToString("d MMM yyyy - h:mm tt");
			var subTitle = $"{item.TotalSize.SizeString(0)} • {item.RestoreItems.GroupBy(x => x.MetaData.Type).ListStrings(x => x.First().MetaData.GetTypeTranslation().FormatPlural(x.Count()), ", ")}";

			var rectangle = new Rectangle(e.ClipRectangle.X + Margin.Left, preferredHeight + (int)UI.Scale(1f) + BorderRadius / 2, e.ClipRectangle.Width - Margin.Horizontal, UI.Scale(32));
			var backColor = rectangle.Contains(CursorLocation) ? HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveColor : HoverState.HasFlag(HoverState.Hovered) ? Color.FromArgb(50, FormDesign.Design.ActiveColor) : Color.Empty : Color.Empty;
			var foreColor = rectangle.Contains(CursorLocation) ? HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveForeColor : FormDesign.Design.ForeColor : FormDesign.Design.ForeColor;

			using var backBrush = new SolidBrush(backColor);
			using var brush = new SolidBrush(foreColor);
			using var brush2 = new SolidBrush(foreColor.MergeColor(backColor, 70));
			using var fontTitle = UI.Font(8.75F, FontStyle.Bold);
			using var fontSubTitle = UI.Font(7.5F);
			using var format = new StringFormat { LineAlignment = StringAlignment.Far, Trimming = StringTrimming.EllipsisCharacter };
			using var pen = new Pen(FormDesign.Design.AccentColor, UI.Scale(1f));

			e.Graphics.DrawLine(pen, rectangle.X, preferredHeight, rectangle.Right, preferredHeight);

			preferredHeight = rectangle.Y;

			e.Graphics.FillRoundedRectangle(backBrush, rectangle.Pad(-BorderRadius / 4), BorderRadius / 2);
			e.Graphics.DrawString(title, fontTitle, brush, rectangle);
			e.Graphics.DrawString(subTitle, fontSubTitle, brush2, rectangle.Pad(0, rectangle.Height / 2, 0, 0), format);

			preferredHeight += rectangle.Height + BorderRadius / 2;

			_buttonActions[rectangle] = () => SelectBackup(item);
		}

		preferredHeight += BorderRadius;
	}

	private void SelectBackup(BackupListControl.RestoreGroup item)
	{
		(PanelContent.GetParentPanel(this) as PC_BackupCenter)?.SelectRestoreGroup(item);
	}
}