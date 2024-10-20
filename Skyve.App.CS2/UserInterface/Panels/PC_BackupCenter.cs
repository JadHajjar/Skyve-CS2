using Skyve.Domain.CS2.Enums;
using Skyve.Domain.CS2.Utilities;

using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_BackupCenter : PanelContent
{
	public PC_BackupCenter()
	{
		InitializeComponent();

		SetUpSliderTicks();

		SetCurrentSettings();
	}

	protected override void LocaleChanged()
	{
		base.LocaleChanged();

		L_BackupInfo.Text = Locale.BackupDescriptionInfo;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		B_AddTime.Margin = UI.Scale(new Padding(4));
	}

	#region Settings Tab
	private void SetCurrentSettings()
	{
		var settings = (BackupSettings)ServiceCenter.Get<ISettings>().BackupSettings;

		slickPathTextBox1.Text = settings.DestinationFolder;
		CB_IncludeAutoSaves.Checked = !settings.IgnoreAutoSaves;
		CB_ScheduleAtTimes.Checked = settings.ScheduleSettings.Type.HasFlag(BackupScheduleType.OnScheduledTimes);
		CB_ScheduleOnGameClose.Checked = settings.ScheduleSettings.Type.HasFlag(BackupScheduleType.OnGameClose);
		CB_ScheduleOnNewSave.Checked = settings.ScheduleSettings.Type.HasFlag(BackupScheduleType.OnNewSaveGame);
		CB_ScheduleIncludeSaves.Checked = settings.ScheduleSettings.BackupSaves;
		CB_ScheduleIncludeLocalMods.Checked = settings.ScheduleSettings.BackupLocalMods;
		CB_CleanupTime.Checked = settings.CleanupSettings.Type.HasFlag(BackupCleanupType.TimeBased);
		CB_CleanupStorage.Checked = settings.CleanupSettings.Type.HasFlag(BackupCleanupType.StorageBased);
		CB_CleanupCount.Checked = settings.CleanupSettings.Type.HasFlag(BackupCleanupType.CountBased);
		SS_CleanupTime.SelectedItem = SS_CleanupTime.Items.FirstOrAny(x => TimeSpan.FromDays((int)x) == settings.CleanupSettings.MaxTimespan);
		SS_Storage.SelectedItem = SS_Storage.Items.FirstOrAny(x => (ulong)(int)x * 1024UL * 1024UL * 1024UL == settings.CleanupSettings.MaxStorage);
		SS_Count.SelectedItem = SS_Count.Items.FirstOrAny(x => (int)x == settings.CleanupSettings.MaxBackups);

		FLP_Times.Controls.Clear(true);

		foreach (var item in settings.ScheduleSettings.ScheduleTimes)
		{
			AddTime(item);
		}
	}

	private void B_AddTime_Click(object sender, EventArgs e)
	{
		AddTime(TimeSpan.FromHours(18));
	}

	private void AddTime(TimeSpan time)
	{
		var dateTimeControl = new SlickDateTime
		{
			DateType = DateType.Time,
			Value = DateTime.Today + time,
			LabelText = string.Empty,
			ShowLabel = false,
			Margin = UI.Scale(new Padding(3))
		};

		var button = new SlickButton
		{
			ImageName = "Trash",
			ColorStyle = ColorStyle.Red,
			ButtonType = ButtonType.Hidden,
			Anchor = AnchorStyles.Left,
			Tag = dateTimeControl,
			Margin = UI.Scale(new Padding(3))
		};

		FLP_Times.Controls.AddRange([dateTimeControl, button]);
		FLP_Times.SetFlowBreak(button, true);

		button.Click += RemoveTimeButton_Click;
	}

	private void RemoveTimeButton_Click(object sender, EventArgs e)
	{
		((SlickDateTime)((SlickButton)sender).Tag).Dispose();
		((SlickButton)sender).Dispose();
	}

	private void SetUpSliderTicks()
	{
		SS_Storage.TextConversion = x => $"{x}GB";
		SS_Storage.Items = [1, 3, 5, 10, 15, 25, 50, 75, 100, 150, 250];

		SS_Count.Items = [10, 25, 50, 75, 100, 200, 300, 500, 1000];

		SS_CleanupTime.TextConversion = x => TimeSpan.FromDays((int)x).ToReadableBigString();
		SS_CleanupTime.Items = [7, 14, 30, 30 * 2, 30 * 3, 30 * 6, 365];
	}

	private void CB_ScheduleAtTimes_CheckChanged(object sender, EventArgs e)
	{
		B_AddTime.Visible = FLP_Times.Visible = CB_ScheduleAtTimes.Checked;
	}

	private void CB_CleanupTime_CheckChanged(object sender, EventArgs e)
	{
		SS_CleanupTime.Visible = CB_CleanupTime.Checked;
	}

	private void CB_CleanupStorage_CheckChanged(object sender, EventArgs e)
	{
		SS_Storage.Visible = CB_CleanupStorage.Checked;
	}

	private void CB_CleanupCount_CheckChanged(object sender, EventArgs e)
	{
		SS_Count.Visible = CB_CleanupCount.Checked;
	}

	public override bool CanExit(bool toBeDisposed)
	{
		if (toBeDisposed)
		{
			Save();
		}

		return base.CanExit(toBeDisposed);
	}

	private void SettingsTabDeselected(object sender, EventArgs e)
	{
		Save();
	}

	private void Save()
	{
		var settings = (BackupSettings)ServiceCenter.Get<ISettings>().BackupSettings;

		settings.DestinationFolder = slickPathTextBox1.Text;
		settings.IgnoreAutoSaves = !CB_IncludeAutoSaves.Checked;

		settings.ScheduleSettings.BackupSaves = CB_ScheduleIncludeSaves.Checked;
		settings.ScheduleSettings.BackupLocalMods = CB_ScheduleIncludeLocalMods.Checked;
		settings.ScheduleSettings.ScheduleTimes = FLP_Times.Controls.OfType<SlickDateTime>().Select(x => x.Value.TimeOfDay).ToArray();
		settings.ScheduleSettings.Type = BackupScheduleType.None;

		if (CB_ScheduleAtTimes.Checked)
		{
			settings.ScheduleSettings.Type |= BackupScheduleType.OnScheduledTimes;
		}

		if (CB_ScheduleOnGameClose.Checked)
		{
			settings.ScheduleSettings.Type |= BackupScheduleType.OnGameClose;
		}

		if (CB_ScheduleOnNewSave.Checked)
		{
			settings.ScheduleSettings.Type |= BackupScheduleType.OnNewSaveGame;
		}

		settings.CleanupSettings.MaxTimespan = TimeSpan.FromDays((int)SS_CleanupTime.SelectedItem);
		settings.CleanupSettings.MaxStorage = (ulong)(int)SS_Storage.SelectedItem * 1024UL * 1024UL * 1024UL;
		settings.CleanupSettings.MaxBackups = (int)SS_Count.SelectedItem;
		settings.CleanupSettings.Type = BackupCleanupType.None;

		if (CB_CleanupTime.Checked)
		{
			settings.CleanupSettings.Type |= BackupCleanupType.TimeBased;
		}

		if (CB_CleanupStorage.Checked)
		{
			settings.CleanupSettings.Type |= BackupCleanupType.StorageBased;
		}

		if (CB_CleanupCount.Checked)
		{
			settings.CleanupSettings.Type |= BackupCleanupType.CountBased;
		}

		settings.Save();
	}
	#endregion

	private async void B_Backup_Click(object sender, EventArgs e)
	{
		if (B_Backup.Loading)
		{
			return;
		}

		B_Backup.Loading = true;

		var backupSystem=ServiceCenter.Get<IBackupSystem>();

		await backupSystem.DoBackup();

		B_Backup.Loading = false;
	}
}
