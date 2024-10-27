using Skyve.App.CS2.UserInterface.Generic;
using Skyve.Domain.CS2.Enums;
using Skyve.Domain.CS2.Utilities;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_BackupCenter : PanelContent
{
	public PC_BackupCenter()
	{
		InitializeComponent();

		SetUpSliderTicks();

		SetCurrentSettings();

		backupListControl.CanDrawItem += BackupListControl_CanDrawItem;
	}

	protected override void LocaleChanged()
	{
		base.LocaleChanged();

		L_BackupInfo.Text = Locale.BackupDescriptionInfo;
		L_RestoreInfo.Text = Locale.SelectRestoreInfo;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		TB_Search.Width = UI.Scale(200);
		TB_Search.Margin = backupViewControl.Margin = UI.Scale(new Padding(3));
		spacerBackup1.Height = spacerSettings.Height = spacerBackup2.Height = UI.Scale(2);
		spacerBackup1.Margin = UI.Scale(new Padding(0, 6, 0, 0));
		spacerBackup2.Margin = UI.Scale(new Padding(0, 0, 0, 6));
		spacerSettings.Margin = UI.Scale(new Padding(6));
		L_BackupInfo.Margin = L_RestoreInfo.Margin = UI.Scale(new Padding(3, 3, 3, 6));
		P_Backup.Margin = P_Restore.Margin = P_RestoreSelect.Margin = UI.Scale(new Padding(6));
		B_AddTime.Margin = SS_Count.Margin = SS_Storage.Margin = SS_CleanupTime.Margin = UI.Scale(new Padding(32, 4, 4, 4));
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		tableLayoutPanel2.BackColor = design.AccentBackColor;
		L_BackupInfo.ForeColor = L_RestoreInfo.ForeColor = design.InfoColor;
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

		var result = await Task.Run(async () =>
		{
			var backupSystem = ServiceCenter.Get<IBackupSystem>();

			return await backupSystem.DoBackup();
		});

		B_Backup.Loading = false;

		if (!result)
		{
			ShowPrompt(Locale.CheckLogsAndTryAgain, Locale.BackupFailed, PromptButtons.OK, PromptIcons.Error);

			return;
		}

		B_Backup.ImageName = "Check";

		await Task.Delay(5_000);

		B_Backup.ImageName = "ArrowRight";
	}

	private void T_Backups_TabSelected(object sender, EventArgs e)
	{
		if (backupListControl.ItemCount == 0)
		{
			backupListControl.Loading = true;
		}

		Task.Run(LoadBackupItems);
	}

	private void LoadBackupItems()
	{
		var backups = ServiceCenter.Get<IBackupSystem>().GetAllBackups();

		if (backupViewControl.RestorePoint)
		{
			backupListControl.SetItems(backups
				.Where(x => !x.MetaData.IsArchived)
				.GroupBy(x => x.MetaData.BackupTime)
				.Select(x => new BackupListControl.RestoreGroup(x.Key, new List<IRestoreItem>(x))));
		}
		else
		{
			backupListControl.SetItems(backups
				.Where(x => !x.MetaData.IsArchived)
				.GroupBy(x => $"{x.MetaData.Type}_{x.MetaData.Name}")
				.Select(x => new BackupListControl.RestoreGroup(x.First().MetaData.Name ?? "", new List<IRestoreItem>(x))));
		}

		backupListControl.Loading = false;
	}

	private void IndividualItemClicked(object sender, EventArgs e)
	{
		backupListControl.IndividualItem = backupViewControl.IndividualItem = true;
		backupListControl.RestorePoint = backupViewControl.RestorePoint = false;

		backupListControl.Clear();
		backupListControl.Loading = true;

		Task.Run(LoadBackupItems);
	}

	private void RestorePointClicked(object sender, EventArgs e)
	{
		backupListControl.IndividualItem = backupViewControl.IndividualItem = false;
		backupListControl.RestorePoint = backupViewControl.RestorePoint = true;

		backupListControl.Clear();
		backupListControl.Loading = true;

		Task.Run(LoadBackupItems);
	}

	private void TB_Search_TextChanged(object sender, EventArgs e)
	{
		TB_Search.ImageName = string.IsNullOrWhiteSpace(TB_Search.Text) ? "Search" : "ClearSearch";

		backupListControl.FilterChanged();
	}

	private void TB_Search_IconClicked(object sender, EventArgs e)
	{
		TB_Search.Text = string.Empty;
	}

	private void BackupListControl_CanDrawItem(object sender, CanDrawItemEventArgs<BackupListControl.RestoreGroup> e)
	{
		if (string.IsNullOrWhiteSpace(TB_Search.Text))
		{
			return;
		}

		e.DoNotDraw = backupListControl.RestorePoint
			? !e.Item.RestoreItems.Any(x => TB_Search.Text.SearchCheck(x.MetaData.Name))
				&& !TB_Search.Text.SearchCheck(e.Item.Time.ToString("d MMM yyyy - h:mm tt"))
			: !TB_Search.Text.SearchCheck(e.Item.Name) && !TB_Search.Text.SearchCheck(e.Item.Time.ToString("d MMM yyyy - h:mm tt"));
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == (Keys.Control | Keys.F))
		{
			TB_Search.Focus();
			TB_Search.SelectAll();
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void B_SelectRestore_Click(object sender, EventArgs e)
	{
		T_Backups.Selected = true;
	}

	private void BackupListControl_ItemMouseClick(object sender, MouseEventArgs e)
	{
		smartFlowPanel1.Controls.Clear(true);

		var item = (BackupListControl.RestoreGroup)sender;

		if (backupListControl.RestorePoint)
		{
			foreach (var backup in item.RestoreItems)
			{
				smartFlowPanel1.Controls.Add(new SlickCheckbox
				{
					Text = $"{backup.MetaData.Name} - {backup.MetaData.GetTypeTranslation()}",
					Tag = backup,
					Checked = true,
				});
			}
		}
		else
		{
			foreach (var backup in item.RestoreItems.OrderByDescending(x => x.MetaData.BackupTime))
			{
				smartFlowPanel1.Controls.Add(new SlickRadioButton
				{
					Text = backup.MetaData.BackupTime.ToString("d MMM yyyy - h:mm tt"),
					Tag = backup,
					Checked = backup.MetaData.BackupTime == item.Time,
				});
			}
		}

		foreach (Control ctrl in smartFlowPanel1.Controls)
		{
			smartFlowPanel1.SetFlowBreak(ctrl, true);
		}

		T_BackupRestore.Selected = true;
		P_Restore.Visible = true;
		P_RestoreSelect.Visible = false;
	}

	private async void slickButton2_Click(object sender, EventArgs e)
	{
		var system = ServiceCenter.Get<IBackupSystem>();
		foreach (SlickCheckbox ctrl in smartFlowPanel1.Controls)
		{
			if (ctrl.Checked)
			{
				await ((IRestoreItem)ctrl.Tag).Restore(system);
			}
		}
	}

	internal void SelectBackup(string restoreBackup)
	{
		throw new NotImplementedException();
	}
}
