using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.UserInterface.Dashboard;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;

[DashboardItem("BackupCenter")]
internal class BD_QuickRestore : IDashboardItem
{
	private readonly ISettings _settings;
	private readonly INotifier _notifier;
	private readonly IBackupSystem _backupSystem;
	private readonly bool _serviceUnavailable;
	private IRestoreItem? lastBackup;

	public BD_QuickRestore()
	{
		ServiceCenter.Get(out _settings, out _notifier, out _backupSystem);

		_serviceUnavailable = ServiceController.GetServices().FirstOrDefault(x => x.ServiceName == "Skyve.Service")?.StartType is null or ServiceStartMode.Disabled;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		_notifier.BackupEnded += LoadData;

		LoadData();
	}

	protected override Task<bool> ProcessDataLoad(CancellationToken token)
	{
		lastBackup = _backupSystem.GetAllBackups().Where(x => x.MetaData.Type == nameof(BackupItem.SettingsFiles)).OrderByDescending(x => x.MetaData.BackupTime).FirstOrDefault();

		return base.ProcessDataLoad(token);
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		_notifier.BackupEnded -= LoadData;
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		return Draw;
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		if (Loading)
		{
			DrawLoadingSection(e, applyDrawing, ref preferredHeight, LocaleCS2.BackupSettingsFiles);
		}
		else
		{
			DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.BackupSettingsFiles, "UserOptions");
		}
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawHeader(e, applyDrawing, ref preferredHeight);

		if (Loading)
		{
			return;
		}

		var textRect = e.ClipRectangle.Pad(Margin);

		e.Graphics.DrawStringItem(lastBackup is null ? LocaleCS2.NoSettingsBackup : LocaleCS2.LastSettingsBackup.Format(lastBackup.MetaData.BackupTime.ToRelatedString(true).ToLower())
			, Font
			, FormDesign.Design.ForeColor
			, textRect
			, ref preferredHeight
			, applyDrawing);

		preferredHeight += Padding.Top / 2;

		using var font = UI.Font(7.5F);

		DrawButton(e, applyDrawing, ref preferredHeight, RestoreSettings, new ButtonDrawArgs
		{
			Icon = lastBackup is null ? "SafeShield" : "RestoreBackup",
			Font = font,
			Size = new Size(0, UI.Scale(20)),
			Text = lastBackup is null ? Locale.DoBackupNow : LocaleCS2.RestoreLatestSettings,
			Enabled = !_notifier.IsBackingUp && !string.IsNullOrWhiteSpace(_settings.BackupSettings.DestinationFolder),
			Rectangle = e.ClipRectangle.Pad(BorderRadius)
		});

		preferredHeight += BorderRadius / 2;
	}

	private void RestoreSettings()
	{
		if (lastBackup is null)
		{
			(PanelContent.GetParentPanel(this) as PC_BackupCenter)?.DoBackup();
		}
		else
		{
			(PanelContent.GetParentPanel(this) as PC_BackupCenter)?.SelectBackup(lastBackup.BackupFile.FullName, false);
		}
	}
}
