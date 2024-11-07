using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.UserInterface.Dashboard;
using Skyve.Domain.CS2.Enums;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
[DashboardItem("BackupCenter")]
internal class BD_NextBackup : B_NextBackup { }

internal class B_NextBackup : IDashboardItem
{
	private readonly ISettings _settings;
	private readonly INotifier _notifier;
	private readonly BackupSettings _backupSettings;
	private readonly bool _serviceUnavailable;

	public B_NextBackup()
	{
		ServiceCenter.Get(out _settings, out _notifier);

		_backupSettings = (BackupSettings)_settings.BackupSettings;
		_serviceUnavailable = ServiceController.GetServices().FirstOrDefault(x => x.ServiceName == "Skyve.Service")?.StartType is null or ServiceStartMode.Disabled;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		_notifier.BackupStarted += Invalidate;
		_notifier.BackupEnded += Invalidate;
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		_notifier.BackupStarted -= Invalidate;
		_notifier.BackupEnded -= Invalidate;
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		return Draw;
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		if (Loading)
		{
			DrawLoadingSection(e, applyDrawing, ref preferredHeight, Locale.BackupSchedule);
		}
		else
		{
			DrawSection(e, applyDrawing, ref preferredHeight, Locale.BackupSchedule, this is BD_NextBackup ? "Clock" : "SafeShield");
		}
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawHeader(e, applyDrawing, ref preferredHeight);

		if (_backupSettings.ScheduleSettings.Type.HasFlag(BackupScheduleType.OnScheduledTimes))
		{
			var currentTime = (int)DateTime.Now.TimeOfDay.TotalMinutes;

			var loading = _backupSettings.ScheduleSettings.ScheduleTimes.Any(x => currentTime == (int)x.TotalMinutes);

			if (loading != Loading)
			{
				Loading = loading;
				OnResizeRequested();
			}
		}

		var textRect = e.ClipRectangle.Pad(Margin);

		if (Loading)
		{
			e.Graphics.DrawStringItem(LocaleCS2.BackupStartingNow
				, Font
				, FormDesign.Design.ForeColor
				, textRect
				, ref preferredHeight
				, applyDrawing);

			preferredHeight += Padding.Top / 2;

			return;
		}

		var dotRect = new Rectangle(textRect.X, preferredHeight - (Margin.Top / 2), 0, 0);
		using var dotBrush = new SolidBrush(_serviceUnavailable ? FormDesign.Design.RedColor : FormDesign.Design.GreenColor);

		e.Graphics.DrawStringItem(_serviceUnavailable ? LocaleCS2.BackupServiceUnavailable : LocaleCS2.BackupServiceAvailable
			, Font
			, FormDesign.Design.ForeColor
			, textRect.Pad(UI.Scale(16), 0, 0, 0)
			, ref preferredHeight
			, applyDrawing);

		dotRect.Height = preferredHeight - dotRect.Y;

		e.Graphics.FillEllipse(dotBrush, dotRect.Align(UI.Scale(new Size(10, 10)), ContentAlignment.MiddleLeft));

		preferredHeight += Padding.Top / 2;

		if (_backupSettings.ScheduleSettings.Type.HasFlag(BackupScheduleType.OnScheduledTimes) && _backupSettings.ScheduleSettings.ScheduleTimes.Length > 0)
		{
			var currentTime = (int)DateTime.Now.TimeOfDay.TotalMinutes;

			var nextBackup = _backupSettings.ScheduleSettings.ScheduleTimes.OrderBy(x => x.Ticks).Cast<TimeSpan?>().FirstOrDefault(x => currentTime < (int)x!.Value.TotalMinutes);
			var time = nextBackup is null ? DateTime.Today.AddDays(1).Add(_backupSettings.ScheduleSettings.ScheduleTimes[0]) : DateTime.Today.Add(nextBackup.Value);

			e.Graphics.DrawStringItem(LocaleCS2.NextScheduledBackup.Format(time.ToRelatedString(true).ToLower())
				, Font
				, FormDesign.Design.ForeColor
				, textRect
				, ref preferredHeight
				, applyDrawing);

			preferredHeight += Padding.Top / 2;
		}

		using var font = UI.Font(7.5F);

		DrawButton(e, applyDrawing, ref preferredHeight, DoBackup, new ButtonDrawArgs
		{
			Icon = "SafeShield",
			Font = font,
			Size = new Size(0, UI.Scale(20)),
			Text = Locale.DoBackupNow,
			Enabled = !_notifier.IsBackingUp && !string.IsNullOrWhiteSpace(_backupSettings.DestinationFolder),
			Rectangle = e.ClipRectangle.Pad(BorderRadius)
		});

		preferredHeight += BorderRadius / 2;
	}

	private void DoBackup()
	{
		(PanelContent.GetParentPanel(this) as PC_BackupCenter)?.DoBackup();
	}
}
