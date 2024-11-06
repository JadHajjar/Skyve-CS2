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
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon11 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon12 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon14 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon13 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon15 = new SlickControls.DynamicIcon();
			this.slickTabControl1 = new SlickControls.SlickTabControl();
			this.T_Dashboard = new SlickControls.SlickTabControl.Tab();
			this.T_BackupRestore = new SlickControls.SlickTabControl.Tab();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.P_RestoreSelect = new SlickControls.RoundedGroupTableLayoutPanel();
			this.backupListControl = new Skyve.App.CS2.UserInterface.Generic.BackupListControl();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.DD_BackupType = new Skyve.App.CS2.UserInterface.Generic.BackupTypeDropdown();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.backupViewControl = new Skyve.App.CS2.UserInterface.Generic.BackupViewControl();
			this.spacerBackup = new SlickControls.SlickSpacer();
			this.P_Backup = new SlickControls.RoundedGroupTableLayoutPanel();
			this.L_BackupInfo = new System.Windows.Forms.Label();
			this.B_Backup = new SlickControls.SlickButton();
			this.L_FinishSetup = new System.Windows.Forms.Label();
			this.T_Restore = new SlickControls.SlickTabControl.Tab();
			this.panel1 = new System.Windows.Forms.Panel();
			this.TLP_Restore = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_Restore = new SlickControls.SlickButton();
			this.L_RestoreInfo = new System.Windows.Forms.Label();
			this.T_Settings = new SlickControls.SlickTabControl.Tab();
			this.TLP_Settings = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_ContentTypes = new SlickControls.RoundedGroupTableLayoutPanel();
			this.L_ContentInfo = new System.Windows.Forms.Label();
			this.P_ContentTypes = new SlickControls.SlickStackedPanel();
			this.TLP_Cleanup = new SlickControls.RoundedGroupTableLayoutPanel();
			this.SS_Count = new SlickControls.Controls.Advanced.SlickStepSlider();
			this.SS_Storage = new SlickControls.Controls.Advanced.SlickStepSlider();
			this.CB_CleanupTime = new SlickControls.SlickCheckbox();
			this.CB_CleanupStorage = new SlickControls.SlickCheckbox();
			this.CB_CleanupCount = new SlickControls.SlickCheckbox();
			this.SS_CleanupTime = new SlickControls.Controls.Advanced.SlickStepSlider();
			this.TLP_Schedule = new SlickControls.RoundedGroupTableLayoutPanel();
			this.CB_ScheduleIncludeMaps = new SlickControls.SlickCheckbox();
			this.B_AddTime = new SlickControls.SlickButton();
			this.CB_ScheduleAtTimes = new SlickControls.SlickCheckbox();
			this.spacerSettings = new SlickControls.SlickSpacer();
			this.FLP_Times = new SlickControls.SmartFlowPanel();
			this.CB_ScheduleOnGameClose = new SlickControls.SlickCheckbox();
			this.CB_ScheduleOnNewSave = new SlickControls.SlickCheckbox();
			this.CB_ScheduleIncludeSaves = new SlickControls.SlickCheckbox();
			this.CB_ScheduleIncludeLocalMods = new SlickControls.SlickCheckbox();
			this.TLP_General = new SlickControls.RoundedGroupTableLayoutPanel();
			this.TB_DestinationFolder = new SlickControls.SlickPathTextBox();
			this.CB_IncludeAutoSaves = new SlickControls.SlickCheckbox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel4.SuspendLayout();
			this.P_RestoreSelect.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.P_Backup.SuspendLayout();
			this.panel1.SuspendLayout();
			this.TLP_Restore.SuspendLayout();
			this.TLP_Settings.SuspendLayout();
			this.TLP_ContentTypes.SuspendLayout();
			this.TLP_Cleanup.SuspendLayout();
			this.TLP_Schedule.SuspendLayout();
			this.TLP_General.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(-2, 3);
			this.base_Text.Size = new System.Drawing.Size(150, 39);
			this.base_Text.Text = "BackupCenter";
			// 
			// slickTabControl1
			// 
			this.slickTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl1.Location = new System.Drawing.Point(0, 30);
			this.slickTabControl1.Name = "slickTabControl1";
			this.slickTabControl1.Size = new System.Drawing.Size(931, 838);
			this.slickTabControl1.TabIndex = 2;
			this.slickTabControl1.Tabs = new SlickControls.SlickTabControl.Tab[] {
        this.T_Dashboard,
        this.T_BackupRestore,
        this.T_Restore,
        this.T_Settings};
			// 
			// T_Dashboard
			// 
			this.T_Dashboard.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Dashboard.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Dashboard.FillTab = true;
			dynamicIcon1.Name = "Dashboard";
			this.T_Dashboard.IconName = dynamicIcon1;
			this.T_Dashboard.LinkedControl = null;
			this.T_Dashboard.Location = new System.Drawing.Point(0, 5);
			this.T_Dashboard.Name = "T_Dashboard";
			this.T_Dashboard.Size = new System.Drawing.Size(154, 67);
			this.T_Dashboard.TabIndex = 2;
			this.T_Dashboard.TabStop = false;
			this.T_Dashboard.Text = "Dashboard";
			// 
			// T_BackupRestore
			// 
			this.T_BackupRestore.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_BackupRestore.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_BackupRestore.FillTab = true;
			dynamicIcon2.Name = "SafeShield";
			this.T_BackupRestore.IconName = dynamicIcon2;
			this.T_BackupRestore.LinkedControl = this.tableLayoutPanel4;
			this.T_BackupRestore.Location = new System.Drawing.Point(154, 5);
			this.T_BackupRestore.Name = "T_BackupRestore";
			this.T_BackupRestore.Size = new System.Drawing.Size(154, 67);
			this.T_BackupRestore.TabIndex = 1;
			this.T_BackupRestore.TabStop = false;
			this.T_BackupRestore.Text = "BackupRestore";
			this.T_BackupRestore.TabSelected += new System.EventHandler(this.T_Backups_TabSelected);
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel4.Controls.Add(this.P_RestoreSelect, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.P_Backup, 0, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(931, 766);
			this.tableLayoutPanel4.TabIndex = 28;
			// 
			// P_RestoreSelect
			// 
			this.P_RestoreSelect.AddOutline = true;
			this.P_RestoreSelect.AddShadow = true;
			this.P_RestoreSelect.AutoSize = true;
			this.P_RestoreSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_RestoreSelect.ColorStyle = Extensions.ColorStyle.Orange;
			this.P_RestoreSelect.ColumnCount = 2;
			this.tableLayoutPanel4.SetColumnSpan(this.P_RestoreSelect, 2);
			this.P_RestoreSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_RestoreSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.P_RestoreSelect.Controls.Add(this.backupListControl, 0, 2);
			this.P_RestoreSelect.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.P_RestoreSelect.Controls.Add(this.spacerBackup, 0, 1);
			this.P_RestoreSelect.Dock = System.Windows.Forms.DockStyle.Fill;
			dynamicIcon4.Name = "RestoreBackup";
			this.P_RestoreSelect.ImageName = dynamicIcon4;
			this.P_RestoreSelect.Info = "SelectRestoreInfo";
			this.P_RestoreSelect.Location = new System.Drawing.Point(3, 123);
			this.P_RestoreSelect.Name = "P_RestoreSelect";
			this.P_RestoreSelect.Padding = new System.Windows.Forms.Padding(16);
			this.P_RestoreSelect.RowCount = 3;
			this.P_RestoreSelect.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_RestoreSelect.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_RestoreSelect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_RestoreSelect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.P_RestoreSelect.Size = new System.Drawing.Size(925, 640);
			this.P_RestoreSelect.TabIndex = 27;
			this.P_RestoreSelect.Text = "BackupHistory";
			// 
			// backupListControl
			// 
			this.backupListControl.AutoInvalidate = false;
			this.backupListControl.AutoScroll = true;
			this.P_RestoreSelect.SetColumnSpan(this.backupListControl, 2);
			this.backupListControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.backupListControl.HighlightOnHover = true;
			this.backupListControl.ItemHeight = 40;
			this.backupListControl.Location = new System.Drawing.Point(16, 56);
			this.backupListControl.Margin = new System.Windows.Forms.Padding(0);
			this.backupListControl.Name = "backupListControl";
			this.backupListControl.SeparateWithLines = true;
			this.backupListControl.Size = new System.Drawing.Size(893, 568);
			this.backupListControl.TabIndex = 27;
			this.backupListControl.ItemMouseClick += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.BackupListControl_ItemMouseClick);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 3;
			this.P_RestoreSelect.SetColumnSpan(this.tableLayoutPanel2, 2);
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.DD_BackupType, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.TB_Search, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.backupViewControl, 2, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(16, 16);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(893, 39);
			this.tableLayoutPanel2.TabIndex = 30;
			// 
			// DD_BackupType
			// 
			this.DD_BackupType.AccentBackColor = true;
			this.DD_BackupType.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.DD_BackupType.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_BackupType.ItemHeight = 24;
			this.DD_BackupType.Location = new System.Drawing.Point(277, 4);
			this.DD_BackupType.Name = "DD_BackupType";
			this.DD_BackupType.Size = new System.Drawing.Size(350, 31);
			this.DD_BackupType.TabIndex = 3;
			this.DD_BackupType.SelectedItemChanged += new System.EventHandler(this.TB_Search_TextChanged);
			// 
			// TB_Search
			// 
			this.TB_Search.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dynamicIcon3.Name = "Search";
			this.TB_Search.ImageName = dynamicIcon3;
			this.TB_Search.LabelText = "Search";
			this.TB_Search.Location = new System.Drawing.Point(3, 3);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Padding = new System.Windows.Forms.Padding(5, 5, 26, 5);
			this.TB_Search.Placeholder = "Search";
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.ShowLabel = false;
			this.TB_Search.Size = new System.Drawing.Size(268, 33);
			this.TB_Search.TabIndex = 4;
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
			this.TB_Search.IconClicked += new System.EventHandler(this.TB_Search_IconClicked);
			// 
			// backupViewControl
			// 
			this.backupViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.backupViewControl.Cursor = System.Windows.Forms.Cursors.Hand;
			this.backupViewControl.Location = new System.Drawing.Point(740, 3);
			this.backupViewControl.Name = "backupViewControl";
			this.backupViewControl.Size = new System.Drawing.Size(150, 31);
			this.backupViewControl.TabIndex = 3;
			this.backupViewControl.RestorePointClicked += new System.EventHandler(this.RestorePointClicked);
			this.backupViewControl.IndividualItemClicked += new System.EventHandler(this.IndividualItemClicked);
			// 
			// spacerBackup
			// 
			this.P_RestoreSelect.SetColumnSpan(this.spacerBackup, 2);
			this.spacerBackup.Dock = System.Windows.Forms.DockStyle.Top;
			this.spacerBackup.Location = new System.Drawing.Point(16, 55);
			this.spacerBackup.Margin = new System.Windows.Forms.Padding(0);
			this.spacerBackup.Name = "spacerBackup";
			this.spacerBackup.Size = new System.Drawing.Size(893, 1);
			this.spacerBackup.Style = Extensions.ColorStyle.Orange;
			this.spacerBackup.TabIndex = 32;
			this.spacerBackup.TabStop = false;
			this.spacerBackup.Text = "slickSpacer3";
			// 
			// P_Backup
			// 
			this.P_Backup.AddOutline = true;
			this.P_Backup.AddShadow = true;
			this.P_Backup.AutoSize = true;
			this.P_Backup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Backup.ColorStyle = Extensions.ColorStyle.Green;
			this.P_Backup.ColumnCount = 2;
			this.tableLayoutPanel4.SetColumnSpan(this.P_Backup, 2);
			this.P_Backup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_Backup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.P_Backup.Controls.Add(this.L_BackupInfo, 0, 0);
			this.P_Backup.Controls.Add(this.B_Backup, 1, 2);
			this.P_Backup.Controls.Add(this.L_FinishSetup, 1, 1);
			this.P_Backup.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon6.Name = "Shield";
			this.P_Backup.ImageName = dynamicIcon6;
			this.P_Backup.Location = new System.Drawing.Point(3, 3);
			this.P_Backup.Name = "P_Backup";
			this.P_Backup.Padding = new System.Windows.Forms.Padding(16, 53, 16, 16);
			this.P_Backup.RowCount = 3;
			this.P_Backup.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Backup.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Backup.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Backup.Size = new System.Drawing.Size(925, 114);
			this.P_Backup.TabIndex = 26;
			this.P_Backup.Text = "Backup";
			// 
			// L_BackupInfo
			// 
			this.L_BackupInfo.AutoSize = true;
			this.L_BackupInfo.Location = new System.Drawing.Point(19, 53);
			this.L_BackupInfo.Name = "L_BackupInfo";
			this.P_Backup.SetRowSpan(this.L_BackupInfo, 3);
			this.L_BackupInfo.Size = new System.Drawing.Size(35, 13);
			this.L_BackupInfo.TabIndex = 15;
			this.L_BackupInfo.Text = "label1";
			// 
			// B_Backup
			// 
			this.B_Backup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Backup.AutoSize = true;
			this.B_Backup.ColorStyle = Extensions.ColorStyle.Green;
			this.B_Backup.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "ArrowRight";
			this.B_Backup.ImageName = dynamicIcon5;
			this.B_Backup.Location = new System.Drawing.Point(776, 69);
			this.B_Backup.Name = "B_Backup";
			this.B_Backup.Size = new System.Drawing.Size(130, 26);
			this.B_Backup.SpaceTriggersClick = true;
			this.B_Backup.TabIndex = 14;
			this.B_Backup.Text = "DoBackupNow";
			this.B_Backup.Click += new System.EventHandler(this.B_Backup_Click);
			// 
			// L_FinishSetup
			// 
			this.L_FinishSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.L_FinishSetup.AutoSize = true;
			this.L_FinishSetup.Location = new System.Drawing.Point(871, 53);
			this.L_FinishSetup.Name = "L_FinishSetup";
			this.L_FinishSetup.Size = new System.Drawing.Size(35, 13);
			this.L_FinishSetup.TabIndex = 16;
			this.L_FinishSetup.Text = "label1";
			// 
			// T_Restore
			// 
			this.T_Restore.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Restore.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon7.Name = "RestoreBackup";
			this.T_Restore.IconName = dynamicIcon7;
			this.T_Restore.LinkedControl = this.panel1;
			this.T_Restore.Location = new System.Drawing.Point(308, 5);
			this.T_Restore.Name = "T_Restore";
			this.T_Restore.Size = new System.Drawing.Size(154, 67);
			this.T_Restore.TabIndex = 1;
			this.T_Restore.TabStop = false;
			this.T_Restore.Text = "Restore";
			this.T_Restore.Visible = false;
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Controls.Add(this.TLP_Restore);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(0, 64);
			this.panel1.TabIndex = 3;
			// 
			// TLP_Restore
			// 
			this.TLP_Restore.AddOutline = true;
			this.TLP_Restore.AddShadow = true;
			this.TLP_Restore.AutoSize = true;
			this.TLP_Restore.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Restore.ColorStyle = Extensions.ColorStyle.Active;
			this.TLP_Restore.ColumnCount = 2;
			this.TLP_Restore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Restore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Restore.Controls.Add(this.B_Restore, 1, 0);
			this.TLP_Restore.Controls.Add(this.L_RestoreInfo, 0, 0);
			this.TLP_Restore.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon9.Name = "RestoreBackup";
			this.TLP_Restore.ImageName = dynamicIcon9;
			this.TLP_Restore.Info = "";
			this.TLP_Restore.Location = new System.Drawing.Point(0, 0);
			this.TLP_Restore.Name = "TLP_Restore";
			this.TLP_Restore.Padding = new System.Windows.Forms.Padding(16);
			this.TLP_Restore.RowCount = 2;
			this.TLP_Restore.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Restore.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Restore.Size = new System.Drawing.Size(0, 64);
			this.TLP_Restore.TabIndex = 3;
			this.TLP_Restore.Text = "Restore";
			// 
			// B_Restore
			// 
			this.B_Restore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Restore.AutoSize = true;
			this.B_Restore.ButtonType = SlickControls.ButtonType.Active;
			this.B_Restore.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon8.Name = "ArrowRight";
			this.B_Restore.ImageName = dynamicIcon8;
			this.B_Restore.Location = new System.Drawing.Point(-136, 19);
			this.B_Restore.Name = "B_Restore";
			this.B_Restore.Size = new System.Drawing.Size(150, 26);
			this.B_Restore.SpaceTriggersClick = true;
			this.B_Restore.TabIndex = 0;
			this.B_Restore.Text = "StartRestore";
			this.B_Restore.Click += new System.EventHandler(this.B_Restore_Click);
			// 
			// L_RestoreInfo
			// 
			this.L_RestoreInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_RestoreInfo.AutoSize = true;
			this.L_RestoreInfo.Location = new System.Drawing.Point(19, 25);
			this.L_RestoreInfo.Name = "L_RestoreInfo";
			this.L_RestoreInfo.Size = new System.Drawing.Size(1, 13);
			this.L_RestoreInfo.TabIndex = 1;
			this.L_RestoreInfo.Text = "label1";
			// 
			// T_Settings
			// 
			this.T_Settings.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Settings.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon10.Name = "Cog";
			this.T_Settings.IconName = dynamicIcon10;
			this.T_Settings.LinkedControl = this.TLP_Settings;
			this.T_Settings.Location = new System.Drawing.Point(462, 5);
			this.T_Settings.Name = "T_Settings";
			this.T_Settings.Size = new System.Drawing.Size(154, 67);
			this.T_Settings.TabIndex = 0;
			this.T_Settings.TabStop = false;
			this.T_Settings.Text = "Settings";
			this.T_Settings.TabDeselected += new System.EventHandler(this.SettingsTabDeselected);
			// 
			// TLP_Settings
			// 
			this.TLP_Settings.AutoSize = true;
			this.TLP_Settings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Settings.ColumnCount = 2;
			this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.TLP_Settings.Controls.Add(this.TLP_ContentTypes, 1, 0);
			this.TLP_Settings.Controls.Add(this.TLP_Cleanup, 0, 2);
			this.TLP_Settings.Controls.Add(this.TLP_Schedule, 0, 1);
			this.TLP_Settings.Controls.Add(this.TLP_General, 0, 0);
			this.TLP_Settings.Location = new System.Drawing.Point(0, 0);
			this.TLP_Settings.Name = "TLP_Settings";
			this.TLP_Settings.RowCount = 2;
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.Size = new System.Drawing.Size(600, 723);
			this.TLP_Settings.TabIndex = 3;
			// 
			// TLP_ContentTypes
			// 
			this.TLP_ContentTypes.AddOutline = true;
			this.TLP_ContentTypes.AddShadow = true;
			this.TLP_ContentTypes.AutoSize = true;
			this.TLP_ContentTypes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_ContentTypes.ColumnCount = 1;
			this.TLP_ContentTypes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_ContentTypes.Controls.Add(this.L_ContentInfo);
			this.TLP_ContentTypes.Controls.Add(this.P_ContentTypes, 0, 1);
			this.TLP_ContentTypes.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon11.Name = "Cog";
			this.TLP_ContentTypes.ImageName = dynamicIcon11;
			this.TLP_ContentTypes.Location = new System.Drawing.Point(423, 3);
			this.TLP_ContentTypes.Name = "TLP_ContentTypes";
			this.TLP_ContentTypes.Padding = new System.Windows.Forms.Padding(16);
			this.TLP_ContentTypes.RowCount = 2;
			this.TLP_Settings.SetRowSpan(this.TLP_ContentTypes, 3);
			this.TLP_ContentTypes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ContentTypes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ContentTypes.Size = new System.Drawing.Size(174, 51);
			this.TLP_ContentTypes.TabIndex = 3;
			this.TLP_ContentTypes.Text = "ContentToBackup";
			// 
			// L_ContentInfo
			// 
			this.L_ContentInfo.AutoSize = true;
			this.L_ContentInfo.Location = new System.Drawing.Point(19, 16);
			this.L_ContentInfo.Name = "L_ContentInfo";
			this.L_ContentInfo.Size = new System.Drawing.Size(38, 13);
			this.L_ContentInfo.TabIndex = 16;
			this.L_ContentInfo.Text = "label1";
			// 
			// P_ContentTypes
			// 
			this.P_ContentTypes.Center = true;
			this.P_ContentTypes.ColumnWidth = 100;
			this.P_ContentTypes.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_ContentTypes.Location = new System.Drawing.Point(19, 32);
			this.P_ContentTypes.Name = "P_ContentTypes";
			this.P_ContentTypes.Size = new System.Drawing.Size(136, 0);
			this.P_ContentTypes.SmartAutoSize = true;
			this.P_ContentTypes.TabIndex = 17;
			// 
			// TLP_Cleanup
			// 
			this.TLP_Cleanup.AddOutline = true;
			this.TLP_Cleanup.AddShadow = true;
			this.TLP_Cleanup.AutoSize = true;
			this.TLP_Cleanup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Cleanup.ColorStyle = Extensions.ColorStyle.Yellow;
			this.TLP_Cleanup.ColumnCount = 1;
			this.TLP_Cleanup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Cleanup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Cleanup.Controls.Add(this.SS_Count, 0, 5);
			this.TLP_Cleanup.Controls.Add(this.SS_Storage, 0, 3);
			this.TLP_Cleanup.Controls.Add(this.CB_CleanupTime, 0, 0);
			this.TLP_Cleanup.Controls.Add(this.CB_CleanupStorage, 0, 2);
			this.TLP_Cleanup.Controls.Add(this.CB_CleanupCount, 0, 4);
			this.TLP_Cleanup.Controls.Add(this.SS_CleanupTime, 0, 1);
			this.TLP_Cleanup.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon12.Name = "Broom";
			this.TLP_Cleanup.ImageName = dynamicIcon12;
			this.TLP_Cleanup.Location = new System.Drawing.Point(3, 448);
			this.TLP_Cleanup.Name = "TLP_Cleanup";
			this.TLP_Cleanup.Padding = new System.Windows.Forms.Padding(16);
			this.TLP_Cleanup.RowCount = 6;
			this.TLP_Cleanup.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Cleanup.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Cleanup.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Cleanup.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Cleanup.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Cleanup.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Cleanup.Size = new System.Drawing.Size(414, 272);
			this.TLP_Cleanup.TabIndex = 2;
			this.TLP_Cleanup.Text = "BackupCleanup";
			// 
			// SS_Count
			// 
			this.SS_Count.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SS_Count.Dock = System.Windows.Forms.DockStyle.Top;
			this.SS_Count.Items = new object[0];
			this.SS_Count.Location = new System.Drawing.Point(19, 219);
			this.SS_Count.Name = "SS_Count";
			this.SS_Count.Progressive = true;
			this.SS_Count.Size = new System.Drawing.Size(376, 34);
			this.SS_Count.TabIndex = 5;
			this.SS_Count.TabStop = false;
			this.SS_Count.Visible = false;
			// 
			// SS_Storage
			// 
			this.SS_Storage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.SS_Storage.Dock = System.Windows.Forms.DockStyle.Top;
			this.SS_Storage.Items = new object[0];
			this.SS_Storage.Location = new System.Drawing.Point(19, 139);
			this.SS_Storage.Name = "SS_Storage";
			this.SS_Storage.Progressive = true;
			this.SS_Storage.Size = new System.Drawing.Size(376, 34);
			this.SS_Storage.TabIndex = 3;
			this.SS_Storage.TabStop = false;
			this.SS_Storage.Visible = false;
			// 
			// CB_CleanupTime
			// 
			this.CB_CleanupTime.AutoSize = true;
			this.CB_CleanupTime.Checked = false;
			this.CB_CleanupTime.CheckedText = null;
			this.CB_CleanupTime.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_CleanupTime.DefaultValue = false;
			this.CB_CleanupTime.EnterTriggersClick = false;
			this.CB_CleanupTime.Location = new System.Drawing.Point(19, 19);
			this.CB_CleanupTime.Name = "CB_CleanupTime";
			this.CB_CleanupTime.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_CleanupTime.Size = new System.Drawing.Size(208, 34);
			this.CB_CleanupTime.SpaceTriggersClick = true;
			this.CB_CleanupTime.TabIndex = 0;
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
			this.CB_CleanupStorage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_CleanupStorage.DefaultValue = false;
			this.CB_CleanupStorage.EnterTriggersClick = false;
			this.CB_CleanupStorage.Location = new System.Drawing.Point(19, 99);
			this.CB_CleanupStorage.Name = "CB_CleanupStorage";
			this.CB_CleanupStorage.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_CleanupStorage.Size = new System.Drawing.Size(336, 34);
			this.CB_CleanupStorage.SpaceTriggersClick = true;
			this.CB_CleanupStorage.TabIndex = 2;
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
			this.CB_CleanupCount.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_CleanupCount.DefaultValue = false;
			this.CB_CleanupCount.EnterTriggersClick = false;
			this.CB_CleanupCount.Location = new System.Drawing.Point(19, 179);
			this.CB_CleanupCount.Name = "CB_CleanupCount";
			this.CB_CleanupCount.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_CleanupCount.Size = new System.Drawing.Size(368, 34);
			this.CB_CleanupCount.SpaceTriggersClick = true;
			this.CB_CleanupCount.TabIndex = 4;
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
			this.SS_CleanupTime.Location = new System.Drawing.Point(19, 59);
			this.SS_CleanupTime.Name = "SS_CleanupTime";
			this.SS_CleanupTime.Progressive = true;
			this.SS_CleanupTime.Size = new System.Drawing.Size(376, 34);
			this.SS_CleanupTime.TabIndex = 1;
			this.SS_CleanupTime.TabStop = false;
			this.SS_CleanupTime.Visible = false;
			// 
			// TLP_Schedule
			// 
			this.TLP_Schedule.AddOutline = true;
			this.TLP_Schedule.AddShadow = true;
			this.TLP_Schedule.AutoSize = true;
			this.TLP_Schedule.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Schedule.ColorStyle = Extensions.ColorStyle.Green;
			this.TLP_Schedule.ColumnCount = 2;
			this.TLP_Schedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Schedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleIncludeMaps, 0, 7);
			this.TLP_Schedule.Controls.Add(this.B_AddTime, 0, 1);
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleAtTimes, 0, 0);
			this.TLP_Schedule.Controls.Add(this.spacerSettings, 0, 4);
			this.TLP_Schedule.Controls.Add(this.FLP_Times, 1, 1);
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleOnGameClose, 0, 2);
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleOnNewSave, 0, 3);
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleIncludeSaves, 0, 5);
			this.TLP_Schedule.Controls.Add(this.CB_ScheduleIncludeLocalMods, 0, 6);
			this.TLP_Schedule.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon14.Name = "ClockSettings";
			this.TLP_Schedule.ImageName = dynamicIcon14;
			this.TLP_Schedule.Location = new System.Drawing.Point(3, 131);
			this.TLP_Schedule.Name = "TLP_Schedule";
			this.TLP_Schedule.Padding = new System.Windows.Forms.Padding(16);
			this.TLP_Schedule.RowCount = 8;
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Schedule.Size = new System.Drawing.Size(414, 311);
			this.TLP_Schedule.TabIndex = 1;
			this.TLP_Schedule.Text = "BackupScheduleSettings";
			// 
			// CB_ScheduleIncludeMaps
			// 
			this.CB_ScheduleIncludeMaps.AutoSize = true;
			this.CB_ScheduleIncludeMaps.Checked = false;
			this.CB_ScheduleIncludeMaps.CheckedText = null;
			this.TLP_Schedule.SetColumnSpan(this.CB_ScheduleIncludeMaps, 2);
			this.CB_ScheduleIncludeMaps.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_ScheduleIncludeMaps.DefaultValue = false;
			this.CB_ScheduleIncludeMaps.EnterTriggersClick = false;
			this.CB_ScheduleIncludeMaps.Location = new System.Drawing.Point(19, 258);
			this.CB_ScheduleIncludeMaps.Name = "CB_ScheduleIncludeMaps";
			this.CB_ScheduleIncludeMaps.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_ScheduleIncludeMaps.Size = new System.Drawing.Size(155, 34);
			this.CB_ScheduleIncludeMaps.SpaceTriggersClick = true;
			this.CB_ScheduleIncludeMaps.TabIndex = 7;
			this.CB_ScheduleIncludeMaps.Tag = "";
			this.CB_ScheduleIncludeMaps.Text = "BackupIncludeMaps";
			this.CB_ScheduleIncludeMaps.UncheckedText = null;
			// 
			// B_AddTime
			// 
			this.B_AddTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_AddTime.AutoSize = true;
			this.B_AddTime.ColorStyle = Extensions.ColorStyle.Green;
			this.B_AddTime.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon13.Name = "Add";
			this.B_AddTime.ImageName = dynamicIcon13;
			this.B_AddTime.Location = new System.Drawing.Point(19, 59);
			this.B_AddTime.Name = "B_AddTime";
			this.B_AddTime.Size = new System.Drawing.Size(26, 26);
			this.B_AddTime.SpaceTriggersClick = true;
			this.B_AddTime.TabIndex = 1;
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
			this.CB_ScheduleAtTimes.Size = new System.Drawing.Size(227, 34);
			this.CB_ScheduleAtTimes.SpaceTriggersClick = true;
			this.CB_ScheduleAtTimes.TabIndex = 0;
			this.CB_ScheduleAtTimes.Tag = "";
			this.CB_ScheduleAtTimes.Text = "ScheduleOnSpecificTimes";
			this.CB_ScheduleAtTimes.UncheckedText = null;
			this.CB_ScheduleAtTimes.CheckChanged += new System.EventHandler(this.CB_ScheduleAtTimes_CheckChanged);
			// 
			// spacerSettings
			// 
			this.TLP_Schedule.SetColumnSpan(this.spacerSettings, 2);
			this.spacerSettings.Dock = System.Windows.Forms.DockStyle.Top;
			this.spacerSettings.Location = new System.Drawing.Point(19, 171);
			this.spacerSettings.Name = "spacerSettings";
			this.spacerSettings.Size = new System.Drawing.Size(376, 1);
			this.spacerSettings.TabIndex = 3;
			this.spacerSettings.TabStop = false;
			this.spacerSettings.Text = "slickSpacer2";
			// 
			// FLP_Times
			// 
			this.FLP_Times.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Times.Location = new System.Drawing.Point(48, 56);
			this.FLP_Times.Margin = new System.Windows.Forms.Padding(0);
			this.FLP_Times.Name = "FLP_Times";
			this.FLP_Times.Size = new System.Drawing.Size(350, 0);
			this.FLP_Times.TabIndex = 2;
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
			this.CB_ScheduleOnGameClose.Size = new System.Drawing.Size(266, 34);
			this.CB_ScheduleOnGameClose.SpaceTriggersClick = true;
			this.CB_ScheduleOnGameClose.TabIndex = 3;
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
			this.CB_ScheduleOnNewSave.Size = new System.Drawing.Size(242, 34);
			this.CB_ScheduleOnNewSave.SpaceTriggersClick = true;
			this.CB_ScheduleOnNewSave.TabIndex = 4;
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
			this.CB_ScheduleIncludeSaves.Size = new System.Drawing.Size(278, 34);
			this.CB_ScheduleIncludeSaves.SpaceTriggersClick = true;
			this.CB_ScheduleIncludeSaves.TabIndex = 5;
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
			this.CB_ScheduleIncludeLocalMods.Size = new System.Drawing.Size(259, 34);
			this.CB_ScheduleIncludeLocalMods.SpaceTriggersClick = true;
			this.CB_ScheduleIncludeLocalMods.TabIndex = 6;
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
			this.TLP_General.ColumnCount = 2;
			this.TLP_General.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
			this.TLP_General.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.TLP_General.Controls.Add(this.TB_DestinationFolder, 0, 0);
			this.TLP_General.Controls.Add(this.CB_IncludeAutoSaves, 0, 1);
			this.TLP_General.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon15.Name = "Preferences";
			this.TLP_General.ImageName = dynamicIcon15;
			this.TLP_General.Location = new System.Drawing.Point(3, 3);
			this.TLP_General.Name = "TLP_General";
			this.TLP_General.Padding = new System.Windows.Forms.Padding(16);
			this.TLP_General.RowCount = 2;
			this.TLP_General.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_General.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_General.Size = new System.Drawing.Size(414, 122);
			this.TLP_General.TabIndex = 0;
			this.TLP_General.Text = "BasicSettings";
			// 
			// TB_DestinationFolder
			// 
			this.TB_DestinationFolder.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_DestinationFolder.Folder = true;
			this.TB_DestinationFolder.LabelText = "BackupPath";
			this.TB_DestinationFolder.Location = new System.Drawing.Point(19, 19);
			this.TB_DestinationFolder.Name = "TB_DestinationFolder";
			this.TB_DestinationFolder.Padding = new System.Windows.Forms.Padding(5, 16, 5, 5);
			this.TB_DestinationFolder.Placeholder = "BackupPathPlaceholder";
			this.TB_DestinationFolder.SelectedText = "";
			this.TB_DestinationFolder.SelectionLength = 0;
			this.TB_DestinationFolder.SelectionStart = 0;
			this.TB_DestinationFolder.Size = new System.Drawing.Size(248, 44);
			this.TB_DestinationFolder.TabIndex = 0;
			// 
			// CB_IncludeAutoSaves
			// 
			this.CB_IncludeAutoSaves.AutoSize = true;
			this.CB_IncludeAutoSaves.Checked = false;
			this.CB_IncludeAutoSaves.CheckedText = null;
			this.TLP_General.SetColumnSpan(this.CB_IncludeAutoSaves, 2);
			this.CB_IncludeAutoSaves.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_IncludeAutoSaves.DefaultValue = false;
			this.CB_IncludeAutoSaves.EnterTriggersClick = false;
			this.CB_IncludeAutoSaves.Location = new System.Drawing.Point(19, 69);
			this.CB_IncludeAutoSaves.Name = "CB_IncludeAutoSaves";
			this.CB_IncludeAutoSaves.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_IncludeAutoSaves.Size = new System.Drawing.Size(368, 34);
			this.CB_IncludeAutoSaves.SpaceTriggersClick = true;
			this.CB_IncludeAutoSaves.TabIndex = 1;
			this.CB_IncludeAutoSaves.Tag = "";
			this.CB_IncludeAutoSaves.Text = "IncludeAutoSavesBackup";
			this.CB_IncludeAutoSaves.UncheckedText = null;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(931, 766);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// PC_BackupCenter
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.slickTabControl1);
			this.Name = "PC_BackupCenter";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(931, 868);
			this.Text = "BackupCenter";
			this.Controls.SetChildIndex(this.slickTabControl1, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.P_RestoreSelect.ResumeLayout(false);
			this.P_RestoreSelect.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.P_Backup.ResumeLayout(false);
			this.P_Backup.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.TLP_Restore.ResumeLayout(false);
			this.TLP_Restore.PerformLayout();
			this.TLP_Settings.ResumeLayout(false);
			this.TLP_Settings.PerformLayout();
			this.TLP_ContentTypes.ResumeLayout(false);
			this.TLP_ContentTypes.PerformLayout();
			this.TLP_Cleanup.ResumeLayout(false);
			this.TLP_Cleanup.PerformLayout();
			this.TLP_Schedule.ResumeLayout(false);
			this.TLP_Schedule.PerformLayout();
			this.TLP_General.ResumeLayout(false);
			this.TLP_General.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private SlickTabControl slickTabControl1;
	private SlickTabControl.Tab T_Dashboard;
	private SlickTabControl.Tab T_BackupRestore;
	private SlickTabControl.Tab T_Settings;
	private System.Windows.Forms.TableLayoutPanel TLP_Settings;
	private RoundedGroupTableLayoutPanel TLP_General;
	private SlickPathTextBox TB_DestinationFolder;
	private SlickCheckbox CB_IncludeAutoSaves;
	private RoundedGroupTableLayoutPanel TLP_Cleanup;
	private SlickCheckbox CB_CleanupTime;
	private SlickCheckbox CB_CleanupStorage;
	private RoundedGroupTableLayoutPanel TLP_Schedule;
	private SlickCheckbox CB_ScheduleAtTimes;
	private SlickCheckbox CB_ScheduleOnNewSave;
	private SlickSpacer spacerSettings;
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
	private RoundedGroupTableLayoutPanel P_Backup;
	private SlickButton B_Backup;
	private System.Windows.Forms.Label L_BackupInfo;
	private Generic.BackupListControl backupListControl;
	private Generic.BackupViewControl backupViewControl;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	public SlickTextBox TB_Search;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
	private RoundedGroupTableLayoutPanel P_RestoreSelect;
	private SlickSpacer spacerBackup;
	private SlickTabControl.Tab T_Restore;
	private RoundedGroupTableLayoutPanel TLP_Restore;
	private SlickButton B_Restore;
	private System.Windows.Forms.Label L_RestoreInfo;
	private System.Windows.Forms.Panel panel1;
	private RoundedGroupTableLayoutPanel TLP_ContentTypes;
	private System.Windows.Forms.Label L_ContentInfo;
	private SlickStackedPanel P_ContentTypes;
	private System.Windows.Forms.Label L_FinishSetup;
	private Generic.BackupTypeDropdown DD_BackupType;
	private SlickCheckbox CB_ScheduleIncludeMaps;
}
