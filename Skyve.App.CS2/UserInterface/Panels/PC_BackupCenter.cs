using Skyve.App.CS2.UserInterface.Generic;
using Skyve.App.UserInterface.Panels;
using Skyve.Domain.CS2.Enums;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Skyve.App.UserInterface.Panels.DashboardPanelControl;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_BackupCenter : PanelContent
{
	private TableLayoutPanel? TLP_RestoreGroups;

	public PC_BackupCenter()
	{
		InitializeComponent();

		T_Dashboard.LinkedControl = new DashboardPanelControl("BackupCenter", GetDefaultLayout);

		SetUpSliderTicks();

		SetCurrentSettings();

		backupListControl.CanDrawItem += BackupListControl_CanDrawItem;
		L_FinishSetup.Visible = !(B_Backup.Enabled = !string.IsNullOrWhiteSpace(ServiceCenter.Get<ISettings>().BackupSettings.DestinationFolder));
	}

	protected override void LocaleChanged()
	{
		base.LocaleChanged();

		L_BackupInfo.Text = Locale.BackupDescriptionInfo;
		L_RestoreInfo.Text = Locale.RestoreDescriptionInfo;
		L_ContentInfo.Text = Locale.BackupContentInfo;
		L_FinishSetup.Text = Locale.SetupSettingsFirst;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		TB_Search.Width = UI.Scale(200);
		L_FinishSetup.Margin = TB_Search.Margin = DD_BackupType.Margin = backupViewControl.Margin = UI.Scale(new Padding(3));
		spacerSettings.Height = spacerBackup.Height = UI.Scale(2);
		spacerSettings.Margin = UI.Scale(new Padding(6));
		L_BackupInfo.Margin = UI.Scale(new Padding(3, 3, 3, 6));
		panel1.Padding = P_Backup.Margin = P_RestoreSelect.Margin = TLP_ContentTypes.Margin = TLP_General.Margin = TLP_Schedule.Margin = TLP_Cleanup.Margin = UI.Scale(new Padding(6));
		B_AddTime.Margin = SS_Count.Margin = SS_Storage.Margin = SS_CleanupTime.Margin = UI.Scale(new Padding(32, 4, 4, 4));
		SS_Count.Height = SS_Storage.Height = SS_CleanupTime.Height = UI.Scale(32);
		B_Restore.Font = UI.Font(9.75F, System.Drawing.FontStyle.Bold);
		L_ContentInfo.Margin = L_RestoreInfo.Margin = B_Restore.Margin = UI.Scale(new Padding(10, 3, 10, 10));
		TB_DestinationFolder.Margin = B_Restore.Padding = UI.Scale(new Padding(6));
		L_FinishSetup.Font = UI.Font(7f, System.Drawing.FontStyle.Italic);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		L_FinishSetup.ForeColor = design.InfoColor.MergeColor(design.OrangeColor, 25);
		L_BackupInfo.ForeColor = L_RestoreInfo.ForeColor = L_ContentInfo.ForeColor = design.InfoColor;
		P_Backup.BackColor = P_RestoreSelect.BackColor = TLP_ContentTypes.BackColor = TLP_General.BackColor = TLP_Schedule.BackColor = TLP_Cleanup.BackColor = design.BackColor;
	}

	public override Color GetTopBarColor()
	{
		return FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? 2 : -5);
	}

	#region Dashboard

	private Dictionary<string, DashboardSetting> GetDefaultLayout()
	{
		return new()
		{
		  { "D_CompatibilityInfo", new(new(0, 0, 4500, 100), false) },
		  { "D_ContentInfo",  new(new(0, 100, 2250, 100), false) },
		  { "D_DiskInfo",  new(new(2250, 100, 2250, 100), false) },
		  { "D_PdxModsNew",  new(new(4500, 0, 3500, 100), false) },
		  { "D_PdxUser",  new(new(8000, 0, 2000, 100), false) },
		  { "D_Playsets",  new(new(8000, 100, 2000, 100), false) },
		  { "D_NotificationCenter",  new(new(8000, 200, 2000, 100), false) },
		};
	}

	#endregion

	#region Settings Tab
	private void SetCurrentSettings()
	{
		var settings = (BackupSettings)ServiceCenter.Get<ISettings>().BackupSettings;

		TB_DestinationFolder.Text = settings.DestinationFolder;
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

		foreach (var item in settings.ScheduleSettings.ScheduleTimes.OrderBy(x => x.Ticks))
		{
			AddTime(item);
		}

		var backupSystem = ServiceCenter.Get<IBackupSystem>();

		P_ContentTypes.Controls.Clear(true);

		foreach (var item in backupSystem.GetBackupTypes())
		{
			var meta = new BackupMetaData { Type = item };

			P_ContentTypes.Controls.Add(new SlickToggleTile
			{
				Text = meta.GetTypeTranslation(),
				Tag = item,
				ImageName = meta.GetIcon(),
				Selected = settings.ContentTypes is null || settings.ContentTypes.Contains(item)
			});
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
		SS_Storage.Items = [5, 10, 15, 25, 50, 75, 100, 150, 250, 350];
		SS_Storage.SetDefaultProgressiveColorPoints();

		SS_Count.Items = [10, 25, 50, 75, 100, 200, 300, 500, 1000];
		SS_Count.SetDefaultProgressiveColorPoints();

		SS_CleanupTime.TextConversion = x => TimeSpan.FromDays((int)x).ToReadableBigString();
		SS_CleanupTime.Items = [7, 14, 30, 30 * 2, 30 * 3, 30 * 6, 365];
		SS_CleanupTime.SetDefaultProgressiveColorPoints();
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

		settings.DestinationFolder = TB_DestinationFolder.Text;
		settings.IgnoreAutoSaves = !CB_IncludeAutoSaves.Checked;

		settings.ContentTypes = P_ContentTypes.Controls.OfType<SlickToggleTile>().Where(x => x.Selected).Select(x => x.Tag.ToString()).ToArray();

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

		L_FinishSetup.Visible = !(B_Backup.Enabled = !string.IsNullOrWhiteSpace(ServiceCenter.Get<ISettings>().BackupSettings.DestinationFolder));
	}
	#endregion

	#region Backup & Restore
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

		if (!result)
		{
			B_Backup.Loading = false;

			ShowPrompt(Locale.CheckLogsAndTryAgain, Locale.BackupFailed, PromptButtons.OK, PromptIcons.Error);

			return;
		}

		await Task.Run(LoadBackupItems);

		B_Backup.Loading = false;
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
		if (DD_BackupType.SelectedItem is not null)
		{
			e.DoNotDraw = !e.Item.RestoreItems.Any(x => x.MetaData.Type == DD_BackupType.SelectedItem);
		}

		if (string.IsNullOrWhiteSpace(TB_Search.Text))
		{
			return;
		}

		if (e.Item.Time.ToString("d").Contains(TB_Search.Text))
		{
			return;
		}

		if (backupListControl.RestorePoint)
		{
			e.DoNotDraw = !e.Item.RestoreItems.Any(x => TB_Search.Text.SearchCheck(x.MetaData.Name) || TB_Search.Text.SearchCheck(x.MetaData.GetTypeTranslation()))
				&& !TB_Search.Text.SearchCheck(e.Item.Time.ToString("d MMM yyyy - h:mm tt"));
		}
		else
		{
			e.DoNotDraw = !TB_Search.Text.SearchCheck(e.Item.Name)
				&& !TB_Search.Text.SearchCheck(e.Item.RestoreItems.First().MetaData.GetTypeTranslation())
				&& !TB_Search.Text.SearchCheck(e.Item.Time.ToString("d MMM yyyy - h:mm tt"));
		}
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == (Keys.Control | Keys.F) && T_BackupRestore.Selected)
		{
			TB_Search.Focus();
			TB_Search.SelectAll();
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void BackupListControl_ItemMouseClick(object sender, MouseEventArgs e)
	{
		TLP_RestoreGroups?.Dispose();
		TLP_RestoreGroups = new TableLayoutPanel()
		{
			Dock = DockStyle.Top,
			AutoSize = true,
			AutoSizeMode = AutoSizeMode.GrowAndShrink
		};

		TLP_Restore.Controls.Add(TLP_RestoreGroups, 0, 1);
		TLP_Restore.SetColumnSpan(TLP_RestoreGroups, 2);

		var item = (BackupListControl.RestoreGroup)sender;

		foreach (var backupGroup in item.RestoreItems.GroupBy(x => x.MetaData.Type))
		{
			TLP_RestoreGroups.ColumnStyles.Add(new() { SizeType = SizeType.Percent, Width = 100 });

			var panel = new RoundedGroupFlowLayoutPanel
			{
				Text = backupGroup.First().MetaData.GetTypeTranslation(),
				ImageName = backupGroup.First().MetaData.GetIcon(),
				AddShadow = true,
				BackColor = FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? -4 : 4),
				Dock = DockStyle.Top,
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink
			};

			foreach (var backup in backupGroup.OrderByDescending(x => x.MetaData.BackupTime))
			{
				var control = new RestoreItemControl(backup, backupListControl.RestorePoint)
				{
					Selected = backupListControl.RestorePoint || backup.MetaData.BackupTime == backupGroup.Max(x => x.MetaData.BackupTime)
				};

				panel.Controls.Add(control);
			}

			TLP_RestoreGroups.Controls.Add(panel, TLP_RestoreGroups.ColumnStyles.Count - 1, 0);
		}

		T_Restore.Visible = true;
		T_Restore.Selected = true;
	}

	public void SelectBackup(string restoreBackup)
	{
		var backupSystem = ServiceCenter.Get<IBackupSystem>();
		var restoreItem = backupSystem.LoadBackupFile(restoreBackup);

		if (restoreItem == null)
		{
			return;
		}

		if (ShowPrompt(Locale.RestoreFileConfirm.Format(1, LocaleHelper.GetGlobalText("Backup_" + restoreItem.MetaData.Name, out var translation) ? translation : restoreItem.MetaData.Name)
			, PromptButtons.YesNo
			, PromptIcons.Question) != DialogResult.Yes)
		{
			return;
		}

		Task.Run(async () =>
		{
			var success = await restoreItem.Restore(backupSystem);

			ShowPrompt(success ? Locale.RestoreSuccessful : Locale.RestoreFailed
				, PromptButtons.OK
				, success ? PromptIcons.Ok : PromptIcons.Error);
		});
	}

	private void B_Restore_Click(object sender, EventArgs e)
	{
		var restores = TLP_RestoreGroups
			.GetControls<RestoreItemControl>()
			.Where(x => x.Selected)
			.ToList(x => x.RestoreItem);

		if (restores.Count == 0)
		{
			return;
		}

		if (ShowPrompt(Locale.RestoreFileConfirm.FormatPlural(restores.Count, LocaleHelper.GetGlobalText("Backup_" + restores[0].MetaData.Name, out var translation) ? translation : restores[0].MetaData.Name)
			, PromptButtons.YesNo
			, PromptIcons.Question) != DialogResult.Yes)
		{
			return;
		}

		TLP_RestoreGroups!.Enabled = false;
		B_Restore.Loading = true;

		Task.Run(async () =>
		{
			var backupSystem = ServiceCenter.Get<IBackupSystem>();
			var success = true;

			await backupSystem.DoBackup();

			foreach (var restoreItem in restores)
			{
				success &= await restoreItem.Restore(backupSystem);
			}

			Invoke(() =>
			{
				B_Restore.Loading = false;

				ShowPrompt(success ? Locale.RestoreSuccessful : Locale.RestoreFailed
					, PromptButtons.OK
					, success ? PromptIcons.Ok : PromptIcons.Error);

				T_BackupRestore.Selected = true;
				T_Restore.Visible = false;
			});
		});
	}

	internal void DoBackup()
	{
		T_BackupRestore.Selected = true;

		B_Backup_Click(this, EventArgs.Empty);
	}

	#endregion
}
