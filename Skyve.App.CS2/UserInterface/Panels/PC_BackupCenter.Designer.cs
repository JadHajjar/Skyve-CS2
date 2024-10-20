namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_BackupCenter
{
	/// <summary> 
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary> 
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Component Designer generated code

	/// <summary> 
	/// Required method for Designer support - do not modify 
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			this.slickTabControl1 = new SlickControls.SlickTabControl();
			this.tab1 = new SlickControls.SlickTabControl.Tab();
			this.tab2 = new SlickControls.SlickTabControl.Tab();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.P_SafeMode = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_Backup = new SlickControls.SlickButton();
			this.L_BackupInfo = new System.Windows.Forms.Label();
			this.tab3 = new SlickControls.SlickTabControl.Tab();
			this.TLP_Settings = new System.Windows.Forms.TableLayoutPanel();
			this.roundedGroupTableLayoutPanel2 = new SlickControls.RoundedGroupTableLayoutPanel();
			this.SS_Count = new SlickControls.Controls.Advanced.SlickStepSlider();
			this.SS_Storage = new SlickControls.Controls.Advanced.SlickStepSlider();
			this.CB_CleanupTime = new SlickControls.SlickCheckbox();
			this.CB_CleanupStorage = new SlickControls.SlickCheckbox();
			this.CB_CleanupCount = new SlickControls.SlickCheckbox();
			this.SS_CleanupTime = new SlickControls.Controls.Advanced.SlickStepSlider();
			this.TLP_Schedule = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_AddTime = new SlickControls.SlickButton();
			this.CB_ScheduleAtTimes = new SlickControls.SlickCheckbox();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.FLP_Times = new SlickControls.SmartFlowPanel();
			this.CB_ScheduleOnGameClose = new SlickControls.SlickCheckbox();
			this.CB_ScheduleOnNewSave = new SlickControls.SlickCheckbox();
			this.CB_ScheduleIncludeSaves = new SlickControls.SlickCheckbox();
			this.CB_ScheduleIncludeLocalMods = new SlickControls.SlickCheckbox();
			this.TLP_General = new SlickControls.RoundedGroupTableLayoutPanel();
			this.slickPathTextBox1 = new SlickControls.SlickPathTextBox();
			this.CB_IncludeAutoSaves = new SlickControls.SlickCheckbox();
			this.tableLayoutPanel1.SuspendLayout();
			this.P_SafeMode.SuspendLayout();
			this.TLP_Settings.SuspendLayout();
			this.roundedGroupTableLayoutPanel2.SuspendLayout();
			this.TLP_Schedule.SuspendLayout();
			this.TLP_General.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(150, 39);
			this.base_Text.Text = "BackupCenter";
			// 
			// slickTabControl1
			// 
			this.slickTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl1.Location = new System.Drawing.Point(5, 30);
			this.slickTabControl1.Name = "slickTabControl1";
			this.slickTabControl1.Size = new System.Drawing.Size(921, 833);
			this.slickTabControl1.TabIndex = 2;
			this.slickTabControl1.Tabs = new SlickControls.SlickTabControl.Tab[] {
        this.tab1,
        this.tab2,
        this.tab3};
			// 
			// tab1
			// 
			this.tab1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.tab1.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon1.Name = "Dashboard";
			this.tab1.IconName = dynamicIcon1;
			this.tab1.LinkedControl = null;
			this.tab1.Location = new System.Drawing.Point(0, 5);
			this.tab1.Name = "tab1";
			this.tab1.Size = new System.Drawing.Size(140, 67);
			this.tab1.TabIndex = 2;
			this.tab1.TabStop = false;
			this.tab1.Text = "Dashboard";
			// 
			// tab2
			// 
			this.tab2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.tab2.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon2.Name = "SafeShield";
			this.tab2.IconName = dynamicIcon2;
			this.tab2.LinkedControl = this.tableLayoutPanel1;
			this.tab2.Location = new System.Drawing.Point(140, 5);
			this.tab2.Name = "tab2";
			this.tab2.Size = new System.Drawing.Size(140, 67);
			this.tab2.TabIndex = 1;
			this.tab2.TabStop = false;
			this.tab2.Text = "BackupRestore";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.P_SafeMode, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(582, 383);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// P_SafeMode
			// 
			this.P_SafeMode.AddOutline = true;
			this.P_SafeMode.AddShadow = true;
			this.P_SafeMode.AutoSize = true;
			this.P_SafeMode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_SafeMode.ColorStyle = Extensions.ColorStyle.Green;
			this.P_SafeMode.ColumnCount = 2;
			this.P_SafeMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_SafeMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.P_SafeMode.Controls.Add(this.B_Backup, 1, 0);
			this.P_SafeMode.Controls.Add(this.L_BackupInfo, 0, 0);
			this.P_SafeMode.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon4.Name = "Shield";
			this.P_SafeMode.ImageName = dynamicIcon4;
			this.P_SafeMode.Location = new System.Drawing.Point(3, 3);
			this.P_SafeMode.Name = "P_SafeMode";
			this.P_SafeMode.Padding = new System.Windows.Forms.Padding(16, 53, 16, 16);
			this.P_SafeMode.RowCount = 1;
			this.P_SafeMode.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_SafeMode.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_SafeMode.Size = new System.Drawing.Size(285, 101);
			this.P_SafeMode.TabIndex = 26;
			this.P_SafeMode.Text = "Backup";
			// 
			// B_Backup
			// 
			this.B_Backup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Backup.AutoSize = true;
			this.B_Backup.ColorStyle = Extensions.ColorStyle.Green;
			this.B_Backup.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon3.Name = "ArrowRight";
			this.B_Backup.ImageName = dynamicIcon3;
			this.B_Backup.Location = new System.Drawing.Point(153, 56);
			this.B_Backup.Name = "B_Backup";
			this.B_Backup.Size = new System.Drawing.Size(113, 26);
			this.B_Backup.SpaceTriggersClick = true;
			this.B_Backup.TabIndex = 14;
			this.B_Backup.Text = "DoBackupNow";
			this.B_Backup.Click += new System.EventHandler(this.B_Backup_Click);
			// 
			// L_BackupInfo
			// 
			this.L_BackupInfo.AutoSize = true;
			this.L_BackupInfo.Location = new System.Drawing.Point(19, 53);
			this.L_BackupInfo.Name = "L_BackupInfo";
			this.L_BackupInfo.Size = new System.Drawing.Size(38, 13);
			this.L_BackupInfo.TabIndex = 15;
			this.L_BackupInfo.Text = "label1";
			// 
			// tab3
			// 
			this.tab3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.tab3.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon5.Name = "Cog";
			this.tab3.IconName = dynamicIcon5;
			this.tab3.LinkedControl = this.TLP_Settings;
			this.tab3.Location = new System.Drawing.Point(280, 5);
			this.tab3.Name = "tab3";
			this.tab3.Size = new System.Drawing.Size(140, 67);
			this.tab3.TabIndex = 0;
			this.tab3.TabStop = false;
			this.tab3.Text = "Settings";
			this.tab3.TabDeselected += new System.EventHandler(this.SettingsTabDeselected);
			// 
			// TLP_Settings
			// 
			this.TLP_Settings.AutoSize = true;
			this.TLP_Settings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Settings.ColumnCount = 2;
			this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
			this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.TLP_Settings.Controls.Add(this.roundedGroupTableLayoutPanel2, 0, 2);
			this.TLP_Settings.Controls.Add(this.TLP_Schedule, 0, 1);
			this.TLP_Settings.Controls.Add(this.TLP_General, 0, 0);
			this.TLP_Settings.Location = new System.Drawing.Point(0, 0);
			this.TLP_Settings.Name = "TLP_Settings";
			this.TLP_Settings.RowCount = 2;
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.Size = new System.Drawing.Size(1023, 683);
			this.TLP_Settings.TabIndex = 3;
			// 
			// roundedGroupTableLayoutPanel2
			// 
			this.roundedGroupTableLayoutPanel2.AddOutline = true;
			this.roundedGroupTableLayoutPanel2.AddShadow = true;
			this.roundedGroupTableLayoutPanel2.AutoSize = true;
			this.roundedGroupTableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedGroupTableLayoutPanel2.ColumnCount = 2;
			this.roundedGroupTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.roundedGroupTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
			this.roundedGroupTableLayoutPanel2.Controls.Add(this.SS_Count, 1, 5);
			this.roundedGroupTableLayoutPanel2.Controls.Add(this.SS_Storage, 1, 3);
			this.roundedGroupTableLayoutPanel2.Controls.Add(this.CB_CleanupTime, 0, 0);
			this.roundedGroupTableLayoutPanel2.Controls.Add(this.CB_CleanupStorage, 0, 2);
			this.roundedGroupTableLayoutPanel2.Controls.Add(this.CB_CleanupCount, 0, 4);
			this.roundedGroupTableLayoutPanel2.Controls.Add(this.SS_CleanupTime, 1, 1);
			this.roundedGroupTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon6.Name = "Broom";
			this.roundedGroupTableLayoutPanel2.ImageName = dynamicIcon6;
			this.roundedGroupTableLayoutPanel2.Location = new System.Drawing.Point(3, 408);
			this.roundedGroupTableLayoutPanel2.Name = "roundedGroupTableLayoutPanel2";
			this.roundedGroupTableLayoutPanel2.Padding = new System.Windows.Forms.Padding(16);
			this.roundedGroupTableLayoutPanel2.RowCount = 6;
			this.roundedGroupTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel2.Size = new System.Drawing.Size(676, 272);
			this.roundedGroupTableLayoutPanel2.TabIndex = 7;
			this.roundedGroupTableLayoutPanel2.Text = "BackupCleanup";
			// 
			// SS_Count
			// 
			this.SS_Count.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SS_Count.Dock = System.Windows.Forms.DockStyle.Top;
			this.SS_Count.Items = new object[0];
			this.SS_Count.Location = new System.Drawing.Point(83, 219);
			this.SS_Count.Name = "SS_Count";
			this.SS_Count.Size = new System.Drawing.Size(574, 34);
			this.SS_Count.TabIndex = 4;
			this.SS_Count.Visible = false;
			// 
			// SS_Storage
			// 
			this.SS_Storage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SS_Storage.Dock = System.Windows.Forms.DockStyle.Top;
			this.SS_Storage.Items = new object[0];
			this.SS_Storage.Location = new System.Drawing.Point(83, 139);
			this.SS_Storage.Name = "SS_Storage";
			this.SS_Storage.Size = new System.Drawing.Size(574, 34);
			this.SS_Storage.TabIndex = 3;
			this.SS_Storage.Visible = false;
			// 
			// CB_CleanupTime
			// 
			this.CB_CleanupTime.AutoSize = true;
			this.CB_CleanupTime.Checked = false;
			this.CB_CleanupTime.CheckedText = null;
			this.roundedGroupTableLayoutPanel2.SetColumnSpan(this.CB_CleanupTime, 2);
			this.CB_CleanupTime.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_CleanupTime.DefaultValue = false;
			this.CB_CleanupTime.EnterTriggersClick = false;
			this.CB_CleanupTime.Location = new System.Drawing.Point(19, 19);
			this.CB_CleanupTime.Name = "CB_CleanupTime";
			this.CB_CleanupTime.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_CleanupTime.Size = new System.Drawing.Size(155, 34);
			this.CB_CleanupTime.SpaceTriggersClick = true;
			this.CB_CleanupTime.TabIndex = 1;
			this.CB_CleanupTime.Tag = "";
			this.CB_CleanupTime.Text = "CleanupTimeBased";
			this.CB_CleanupTime.UncheckedText = null;
			this.CB_CleanupTime.CheckChanged += new System.EventHandler(this.CB_CleanupTime_CheckChanged);
			// 
			// CB_CleanupStorage
			// 
			this.CB_CleanupStorage.AutoSize = true;
			this.CB_CleanupStorage.Checked = false;
			this.CB_CleanupStorage.CheckedText = null;
			this.roundedGroupTableLayoutPanel2.SetColumnSpan(this.CB_CleanupStorage, 2);
			this.CB_CleanupStorage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_CleanupStorage.DefaultValue = false;
			this.CB_CleanupStorage.EnterTriggersClick = false;
			this.CB_CleanupStorage.Location = new System.Drawing.Point(19, 99);
			this.CB_CleanupStorage.Name = "CB_CleanupStorage";
			this.CB_CleanupStorage.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_CleanupStorage.Size = new System.Drawing.Size(169, 34);
			this.CB_CleanupStorage.SpaceTriggersClick = true;
			this.CB_CleanupStorage.TabIndex = 1;
			this.CB_CleanupStorage.Tag = "";
			this.CB_CleanupStorage.Text = "CleanupStorageBased";
			this.CB_CleanupStorage.UncheckedText = null;
			this.CB_CleanupStorage.CheckChanged += new System.EventHandler(this.CB_CleanupStorage_CheckChanged);
			// 
			// CB_CleanupCount
			// 
			this.CB_CleanupCount.AutoSize = true;
			this.CB_CleanupCount.Checked = false;
			this.CB_CleanupCount.CheckedText = null;
			this.roundedGroupTableLayoutPanel2.SetColumnSpan(this.CB_CleanupCount, 2);
			this.CB_CleanupCount.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_CleanupCount.DefaultValue = false;
			this.CB_CleanupCount.EnterTriggersClick = false;
			this.CB_CleanupCount.Location = new System.Drawing.Point(19, 179);
			this.CB_CleanupCount.Name = "CB_CleanupCount";
			this.CB_CleanupCount.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_CleanupCount.Size = new System.Drawing.Size(160, 34);
			this.CB_CleanupCount.SpaceTriggersClick = true;
			this.CB_CleanupCount.TabIndex = 1;
			this.CB_CleanupCount.Tag = "";
			this.CB_CleanupCount.Text = "CleanupCountBased";
			this.CB_CleanupCount.UncheckedText = null;
			this.CB_CleanupCount.CheckChanged += new System.EventHandler(this.CB_CleanupCount_CheckChanged);
			// 
			// SS_CleanupTime
			// 
			this.SS_CleanupTime.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SS_CleanupTime.Dock = System.Windows.Forms.DockStyle.Top;
			this.SS_CleanupTime.Items = new object[0];
			this.SS_CleanupTime.Location = new System.Drawing.Point(83, 59);
			this.SS_CleanupTime.Name = "SS_CleanupTime";
			this.SS_CleanupTime.Size = new System.Drawing.Size(574, 34);
			this.SS_CleanupTime.TabIndex = 2;
			this.SS_CleanupTime.Visible = false;
			// 
			// TLP_Schedule
			// 
			this.TLP_Schedule.AddOutline = true;
			this.TLP_Schedule.AddShadow = true;
			this.TLP_Schedule.AutoSize = true;
			this.TLP_Schedule.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Schedule.ColumnCount = 2;
			this.TLP_Schedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.TLP_Schedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
			this.TLP_Schedule.Controls.Add(this.B_AddTime, 0, 1);
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleAtTimes, 0, 0);
			this.TLP_Schedule.Controls.Add(this.slickSpacer2, 0, 4);
			this.TLP_Schedule.Controls.Add(this.FLP_Times, 1, 1);
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleOnGameClose, 0, 2);
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleOnNewSave, 0, 3);
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleIncludeSaves, 0, 5);
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleIncludeLocalMods, 0, 6);
			this.TLP_Schedule.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon8.Name = "ClockSettings";
			this.TLP_Schedule.ImageName = dynamicIcon8;
			this.TLP_Schedule.Location = new System.Drawing.Point(3, 131);
			this.TLP_Schedule.Name = "TLP_Schedule";
			this.TLP_Schedule.Padding = new System.Windows.Forms.Padding(16);
			this.TLP_Schedule.RowCount = 7;
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.Size = new System.Drawing.Size(676, 271);
			this.TLP_Schedule.TabIndex = 6;
			this.TLP_Schedule.Text = "BackupSchedule";
			// 
			// B_AddTime
			// 
			this.B_AddTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_AddTime.AutoSize = true;
			this.B_AddTime.ButtonType = SlickControls.ButtonType.Active;
			this.B_AddTime.ColorStyle = Extensions.ColorStyle.Green;
			this.B_AddTime.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon7.Name = "Add";
			this.B_AddTime.ImageName = dynamicIcon7;
			this.B_AddTime.Location = new System.Drawing.Point(51, 59);
			this.B_AddTime.Name = "B_AddTime";
			this.B_AddTime.Size = new System.Drawing.Size(26, 26);
			this.B_AddTime.SpaceTriggersClick = true;
			this.B_AddTime.TabIndex = 0;
			this.B_AddTime.Visible = false;
			this.B_AddTime.Click += new System.EventHandler(this.B_AddTime_Click);
			// 
			// CB_ScheduleAtTimes
			// 
			this.CB_ScheduleAtTimes.AutoSize = true;
			this.CB_ScheduleAtTimes.Checked = false;
			this.CB_ScheduleAtTimes.CheckedText = null;
			this.TLP_Schedule.SetColumnSpan(this.CB_ScheduleAtTimes, 2);
			this.CB_ScheduleAtTimes.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_ScheduleAtTimes.DefaultValue = false;
			this.CB_ScheduleAtTimes.EnterTriggersClick = false;
			this.CB_ScheduleAtTimes.Location = new System.Drawing.Point(19, 19);
			this.CB_ScheduleAtTimes.Name = "CB_ScheduleAtTimes";
			this.CB_ScheduleAtTimes.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_ScheduleAtTimes.Size = new System.Drawing.Size(188, 34);
			this.CB_ScheduleAtTimes.SpaceTriggersClick = true;
			this.CB_ScheduleAtTimes.TabIndex = 1;
			this.CB_ScheduleAtTimes.Tag = "";
			this.CB_ScheduleAtTimes.Text = "ScheduleOnSpecificTimes";
			this.CB_ScheduleAtTimes.UncheckedText = null;
			this.CB_ScheduleAtTimes.CheckChanged += new System.EventHandler(this.CB_ScheduleAtTimes_CheckChanged);
			// 
			// slickSpacer2
			// 
			this.TLP_Schedule.SetColumnSpan(this.slickSpacer2, 2);
			this.slickSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer2.Location = new System.Drawing.Point(19, 171);
			this.slickSpacer2.Name = "slickSpacer2";
			this.slickSpacer2.Size = new System.Drawing.Size(638, 1);
			this.slickSpacer2.TabIndex = 3;
			this.slickSpacer2.TabStop = false;
			this.slickSpacer2.Text = "slickSpacer2";
			// 
			// FLP_Times
			// 
			this.FLP_Times.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Times.Location = new System.Drawing.Point(80, 56);
			this.FLP_Times.Margin = new System.Windows.Forms.Padding(0);
			this.FLP_Times.Name = "FLP_Times";
			this.FLP_Times.Size = new System.Drawing.Size(580, 0);
			this.FLP_Times.TabIndex = 4;
			this.FLP_Times.Visible = false;
			// 
			// CB_ScheduleOnGameClose
			// 
			this.CB_ScheduleOnGameClose.AutoSize = true;
			this.CB_ScheduleOnGameClose.Checked = false;
			this.CB_ScheduleOnGameClose.CheckedText = null;
			this.TLP_Schedule.SetColumnSpan(this.CB_ScheduleOnGameClose, 2);
			this.CB_ScheduleOnGameClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_ScheduleOnGameClose.DefaultValue = false;
			this.CB_ScheduleOnGameClose.EnterTriggersClick = false;
			this.CB_ScheduleOnGameClose.Location = new System.Drawing.Point(19, 91);
			this.CB_ScheduleOnGameClose.Name = "CB_ScheduleOnGameClose";
			this.CB_ScheduleOnGameClose.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_ScheduleOnGameClose.Size = new System.Drawing.Size(177, 34);
			this.CB_ScheduleOnGameClose.SpaceTriggersClick = true;
			this.CB_ScheduleOnGameClose.TabIndex = 1;
			this.CB_ScheduleOnGameClose.Tag = "";
			this.CB_ScheduleOnGameClose.Text = "ScheduleOnGameClose";
			this.CB_ScheduleOnGameClose.UncheckedText = null;
			// 
			// CB_ScheduleOnNewSave
			// 
			this.CB_ScheduleOnNewSave.AutoSize = true;
			this.CB_ScheduleOnNewSave.Checked = false;
			this.CB_ScheduleOnNewSave.CheckedText = null;
			this.TLP_Schedule.SetColumnSpan(this.CB_ScheduleOnNewSave, 2);
			this.CB_ScheduleOnNewSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_ScheduleOnNewSave.DefaultValue = false;
			this.CB_ScheduleOnNewSave.EnterTriggersClick = false;
			this.CB_ScheduleOnNewSave.Location = new System.Drawing.Point(19, 131);
			this.CB_ScheduleOnNewSave.Name = "CB_ScheduleOnNewSave";
			this.CB_ScheduleOnNewSave.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_ScheduleOnNewSave.Size = new System.Drawing.Size(166, 34);
			this.CB_ScheduleOnNewSave.SpaceTriggersClick = true;
			this.CB_ScheduleOnNewSave.TabIndex = 1;
			this.CB_ScheduleOnNewSave.Tag = "";
			this.CB_ScheduleOnNewSave.Text = "ScheduleOnNewSave";
			this.CB_ScheduleOnNewSave.UncheckedText = null;
			// 
			// CB_ScheduleIncludeSaves
			// 
			this.CB_ScheduleIncludeSaves.AutoSize = true;
			this.CB_ScheduleIncludeSaves.Checked = false;
			this.CB_ScheduleIncludeSaves.CheckedText = null;
			this.TLP_Schedule.SetColumnSpan(this.CB_ScheduleIncludeSaves, 2);
			this.CB_ScheduleIncludeSaves.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_ScheduleIncludeSaves.DefaultValue = false;
			this.CB_ScheduleIncludeSaves.EnterTriggersClick = false;
			this.CB_ScheduleIncludeSaves.Location = new System.Drawing.Point(19, 178);
			this.CB_ScheduleIncludeSaves.Name = "CB_ScheduleIncludeSaves";
			this.CB_ScheduleIncludeSaves.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_ScheduleIncludeSaves.Size = new System.Drawing.Size(192, 34);
			this.CB_ScheduleIncludeSaves.SpaceTriggersClick = true;
			this.CB_ScheduleIncludeSaves.TabIndex = 1;
			this.CB_ScheduleIncludeSaves.Tag = "";
			this.CB_ScheduleIncludeSaves.Text = "BackupIncludeSaveGames";
			this.CB_ScheduleIncludeSaves.UncheckedText = null;
			// 
			// CB_ScheduleIncludeLocalMods
			// 
			this.CB_ScheduleIncludeLocalMods.AutoSize = true;
			this.CB_ScheduleIncludeLocalMods.Checked = false;
			this.CB_ScheduleIncludeLocalMods.CheckedText = null;
			this.TLP_Schedule.SetColumnSpan(this.CB_ScheduleIncludeLocalMods, 2);
			this.CB_ScheduleIncludeLocalMods.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_ScheduleIncludeLocalMods.DefaultValue = false;
			this.CB_ScheduleIncludeLocalMods.EnterTriggersClick = false;
			this.CB_ScheduleIncludeLocalMods.Location = new System.Drawing.Point(19, 218);
			this.CB_ScheduleIncludeLocalMods.Name = "CB_ScheduleIncludeLocalMods";
			this.CB_ScheduleIncludeLocalMods.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_ScheduleIncludeLocalMods.Size = new System.Drawing.Size(184, 34);
			this.CB_ScheduleIncludeLocalMods.SpaceTriggersClick = true;
			this.CB_ScheduleIncludeLocalMods.TabIndex = 1;
			this.CB_ScheduleIncludeLocalMods.Tag = "";
			this.CB_ScheduleIncludeLocalMods.Text = "BackupIncludeLocalMods";
			this.CB_ScheduleIncludeLocalMods.UncheckedText = null;
			// 
			// TLP_General
			// 
			this.TLP_General.AddOutline = true;
			this.TLP_General.AddShadow = true;
			this.TLP_General.AutoSize = true;
			this.TLP_General.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_General.ColumnCount = 1;
			this.TLP_General.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_General.Controls.Add(this.slickPathTextBox1, 0, 0);
			this.TLP_General.Controls.Add(this.CB_IncludeAutoSaves, 0, 1);
			this.TLP_General.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon9.Name = "Preferences";
			this.TLP_General.ImageName = dynamicIcon9;
			this.TLP_General.Location = new System.Drawing.Point(3, 3);
			this.TLP_General.Name = "TLP_General";
			this.TLP_General.Padding = new System.Windows.Forms.Padding(16);
			this.TLP_General.RowCount = 2;
			this.TLP_General.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_General.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_General.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_General.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_General.Size = new System.Drawing.Size(676, 122);
			this.TLP_General.TabIndex = 5;
			this.TLP_General.Text = "BasicSettings";
			// 
			// slickPathTextBox1
			// 
			this.slickPathTextBox1.Folder = true;
			this.slickPathTextBox1.LabelText = "BackupPath";
			this.slickPathTextBox1.Location = new System.Drawing.Point(19, 19);
			this.slickPathTextBox1.Name = "slickPathTextBox1";
			this.slickPathTextBox1.Padding = new System.Windows.Forms.Padding(5, 16, 5, 5);
			this.slickPathTextBox1.Placeholder = "BackupPathPlaceholder";
			this.slickPathTextBox1.SelectedText = "";
			this.slickPathTextBox1.SelectionLength = 0;
			this.slickPathTextBox1.SelectionStart = 0;
			this.slickPathTextBox1.Size = new System.Drawing.Size(499, 44);
			this.slickPathTextBox1.TabIndex = 2;
			// 
			// CB_IncludeAutoSaves
			// 
			this.CB_IncludeAutoSaves.AutoSize = true;
			this.CB_IncludeAutoSaves.Checked = false;
			this.CB_IncludeAutoSaves.CheckedText = null;
			this.CB_IncludeAutoSaves.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_IncludeAutoSaves.DefaultValue = false;
			this.CB_IncludeAutoSaves.EnterTriggersClick = false;
			this.CB_IncludeAutoSaves.Location = new System.Drawing.Point(19, 69);
			this.CB_IncludeAutoSaves.Name = "CB_IncludeAutoSaves";
			this.CB_IncludeAutoSaves.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_IncludeAutoSaves.Size = new System.Drawing.Size(184, 34);
			this.CB_IncludeAutoSaves.SpaceTriggersClick = true;
			this.CB_IncludeAutoSaves.TabIndex = 1;
			this.CB_IncludeAutoSaves.Tag = "";
			this.CB_IncludeAutoSaves.Text = "IncludeAutoSavesBackup";
			this.CB_IncludeAutoSaves.UncheckedText = null;
			// 
			// PC_BackupCenter
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.slickTabControl1);
			this.Name = "PC_BackupCenter";
			this.Size = new System.Drawing.Size(931, 868);
			this.Text = "BackupCenter";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.slickTabControl1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.P_SafeMode.ResumeLayout(false);
			this.P_SafeMode.PerformLayout();
			this.TLP_Settings.ResumeLayout(false);
			this.TLP_Settings.PerformLayout();
			this.roundedGroupTableLayoutPanel2.ResumeLayout(false);
			this.roundedGroupTableLayoutPanel2.PerformLayout();
			this.TLP_Schedule.ResumeLayout(false);
			this.TLP_Schedule.PerformLayout();
			this.TLP_General.ResumeLayout(false);
			this.TLP_General.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private SlickTabControl slickTabControl1;
	private SlickTabControl.Tab tab1;
	private SlickTabControl.Tab tab2;
	private SlickTabControl.Tab tab3;
	private System.Windows.Forms.TableLayoutPanel TLP_Settings;
	private RoundedGroupTableLayoutPanel TLP_General;
	private SlickPathTextBox slickPathTextBox1;
	private SlickCheckbox CB_IncludeAutoSaves;
	private RoundedGroupTableLayoutPanel roundedGroupTableLayoutPanel2;
	private SlickCheckbox CB_CleanupTime;
	private SlickCheckbox CB_CleanupStorage;
	private RoundedGroupTableLayoutPanel TLP_Schedule;
	private SlickCheckbox CB_ScheduleAtTimes;
	private SlickCheckbox CB_ScheduleOnNewSave;
	private SlickSpacer slickSpacer2;
	private SmartFlowPanel FLP_Times;
	private SlickCheckbox CB_ScheduleOnGameClose;
	private SlickCheckbox CB_ScheduleIncludeSaves;
	private SlickCheckbox CB_ScheduleIncludeLocalMods;
	private SlickCheckbox CB_CleanupCount;
	private SlickControls.Controls.Advanced.SlickStepSlider SS_CleanupTime;
	private SlickControls.Controls.Advanced.SlickStepSlider SS_Count;
	private SlickControls.Controls.Advanced.SlickStepSlider SS_Storage;
	private SlickButton B_AddTime;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private RoundedGroupTableLayoutPanel P_SafeMode;
	private SlickButton B_Backup;
	private System.Windows.Forms.Label L_BackupInfo;
}
