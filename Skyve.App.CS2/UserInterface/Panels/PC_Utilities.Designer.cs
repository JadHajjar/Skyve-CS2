using Skyve.App.UserInterface.Generic;

namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_Utilities
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
			_notifier.WorkshopSyncStarted -= Notifier_WorkshopSync;
			_notifier.WorkshopSyncEnded -= Notifier_WorkshopSync;

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
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon13 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon11 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon12 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon15 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon14 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon17 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon16 = new SlickControls.DynamicIcon();
			this.TLP_Main = new SlickControls.SmartTablePanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.P_Troubleshoot = new SlickControls.RoundedGroupTableLayoutPanel();
			this.L_Troubleshoot = new SlickControls.AutoSizeLabel();
			this.B_Troubleshoot = new SlickControls.SlickButton();
			this.P_Text = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_ImportClipboard = new SlickControls.SlickButton();
			this.DD_TextImport = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.P_Sync = new SlickControls.RoundedGroupTableLayoutPanel();
			this.smartTablePanel1 = new SlickControls.SmartTablePanel();
			this.L_SyncStatusLabel = new System.Windows.Forms.Label();
			this.B_RunSync = new SlickControls.SlickButton();
			this.L_SyncStatus = new System.Windows.Forms.Label();
			this.L_PdxSyncInfo = new SlickControls.AutoSizeLabel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.P_Reset = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_ReloadAllData = new SlickControls.SlickButton();
			this.B_ResetCompatibilityCache = new SlickControls.SlickButton();
			this.B_ResetModsCache = new SlickControls.SlickButton();
			this.B_ResetSnoozes = new SlickControls.SlickButton();
			this.B_ResetImageCache = new SlickControls.SlickButton();
			this.B_ResetSteamCache = new SlickControls.SlickButton();
			this.P_Issues = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_FixAllIssues = new SlickControls.SlickButton();
			this.outOfDatePackagesControl1 = new Skyve.App.CS2.UserInterface.Content.OutOfDatePackagesControl();
			this.P_SafeMode = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_SafeMode = new SlickControls.SlickButton();
			this.L_SafeMode = new System.Windows.Forms.Label();
			this.P_Container = new System.Windows.Forms.Panel();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.TLP_Main.SuspendLayout();
			this.panel1.SuspendLayout();
			this.P_Troubleshoot.SuspendLayout();
			this.P_Text.SuspendLayout();
			this.P_Sync.SuspendLayout();
			this.smartTablePanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.P_Reset.SuspendLayout();
			this.P_Issues.SuspendLayout();
			this.P_SafeMode.SuspendLayout();
			this.P_Container.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(150, 39);
			// 
			// TLP_Main
			// 
			this.TLP_Main.AutoSize = true;
			this.TLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.TLP_Main.Controls.Add(this.panel1, 0, 0);
			this.TLP_Main.Controls.Add(this.panel2, 1, 0);
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.MinimumSize = new System.Drawing.Size(700, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 1;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.Size = new System.Drawing.Size(700, 447);
			this.TLP_Main.TabIndex = 17;
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.Controls.Add(this.P_Troubleshoot);
			this.panel1.Controls.Add(this.P_Text);
			this.panel1.Controls.Add(this.P_Sync);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(414, 385);
			this.panel1.TabIndex = 18;
			// 
			// P_Troubleshoot
			// 
			this.P_Troubleshoot.AddOutline = true;
			this.P_Troubleshoot.AddShadow = true;
			this.P_Troubleshoot.AutoSize = true;
			this.P_Troubleshoot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Troubleshoot.ColorStyle = Extensions.ColorStyle.Yellow;
			this.P_Troubleshoot.ColumnCount = 1;
			this.P_Troubleshoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_Troubleshoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.P_Troubleshoot.Controls.Add(this.L_Troubleshoot, 0, 0);
			this.P_Troubleshoot.Controls.Add(this.B_Troubleshoot, 0, 1);
			this.P_Troubleshoot.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon2.Name = "Wrench";
			this.P_Troubleshoot.ImageName = dynamicIcon2;
			this.P_Troubleshoot.Location = new System.Drawing.Point(0, 311);
			this.P_Troubleshoot.Name = "P_Troubleshoot";
			this.P_Troubleshoot.Padding = new System.Windows.Forms.Padding(16);
			this.P_Troubleshoot.RowCount = 1;
			this.P_Troubleshoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Troubleshoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Troubleshoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.P_Troubleshoot.Size = new System.Drawing.Size(414, 74);
			this.P_Troubleshoot.TabIndex = 23;
			this.P_Troubleshoot.Text = "TroubleshootIssues";
			// 
			// L_Troubleshoot
			// 
			this.L_Troubleshoot.AutoSize = true;
			this.L_Troubleshoot.Location = new System.Drawing.Point(19, 26);
			this.L_Troubleshoot.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			this.L_Troubleshoot.Name = "L_Troubleshoot";
			this.L_Troubleshoot.Size = new System.Drawing.Size(0, 0);
			this.L_Troubleshoot.TabIndex = 17;
			// 
			// B_Troubleshoot
			// 
			this.B_Troubleshoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Troubleshoot.AutoSize = true;
			this.B_Troubleshoot.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "ArrowRight";
			this.B_Troubleshoot.ImageName = dynamicIcon1;
			this.B_Troubleshoot.Location = new System.Drawing.Point(226, 29);
			this.B_Troubleshoot.Name = "B_Troubleshoot";
			this.B_Troubleshoot.Size = new System.Drawing.Size(169, 26);
			this.B_Troubleshoot.SpaceTriggersClick = true;
			this.B_Troubleshoot.TabIndex = 14;
			this.B_Troubleshoot.Text = "ViewTroubleshootOptions";
			this.B_Troubleshoot.Click += new System.EventHandler(this.B_Troubleshoot_Click);
			// 
			// P_Text
			// 
			this.P_Text.AddOutline = true;
			this.P_Text.AddShadow = true;
			this.P_Text.AutoSize = true;
			this.P_Text.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Text.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_Text.Controls.Add(this.B_ImportClipboard, 0, 1);
			this.P_Text.Controls.Add(this.DD_TextImport, 0, 0);
			this.P_Text.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon4.Name = "Text";
			this.P_Text.ImageName = dynamicIcon4;
			this.P_Text.Info = "ImportFromTextInfo";
			this.P_Text.Location = new System.Drawing.Point(0, 137);
			this.P_Text.Name = "P_Text";
			this.P_Text.Padding = new System.Windows.Forms.Padding(16);
			this.P_Text.RowCount = 2;
			this.P_Text.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Text.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Text.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.P_Text.Size = new System.Drawing.Size(414, 174);
			this.P_Text.TabIndex = 20;
			this.P_Text.Text = "ImportFromText";
			// 
			// B_ImportClipboard
			// 
			this.B_ImportClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_ImportClipboard.AutoSize = true;
			this.B_ImportClipboard.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon3.Name = "Copy";
			this.B_ImportClipboard.ImageName = dynamicIcon3;
			this.B_ImportClipboard.Location = new System.Drawing.Point(268, 129);
			this.B_ImportClipboard.Name = "B_ImportClipboard";
			this.B_ImportClipboard.Size = new System.Drawing.Size(127, 26);
			this.B_ImportClipboard.SpaceTriggersClick = true;
			this.B_ImportClipboard.TabIndex = 15;
			this.B_ImportClipboard.Text = "ImportFromClipboard";
			this.B_ImportClipboard.Click += new System.EventHandler(this.B_ImportClipboard_Click);
			// 
			// DD_TextImport
			// 
			this.DD_TextImport.AllowDrop = true;
			this.DD_TextImport.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_TextImport.Location = new System.Drawing.Point(19, 19);
			this.DD_TextImport.Name = "DD_TextImport";
			this.DD_TextImport.Size = new System.Drawing.Size(372, 104);
			this.DD_TextImport.TabIndex = 17;
			this.DD_TextImport.Text = "TextImportMissingInfo";
			this.DD_TextImport.ValidExtensions = new string[] {
        ".txt"};
			this.DD_TextImport.FileSelected += new System.Action<string>(this.DD_TextImport_FileSelected);
			this.DD_TextImport.ValidFile += new System.Func<object, string, bool>(this.DD_TextImport_ValidFile);
			// 
			// P_Sync
			// 
			this.P_Sync.AddOutline = true;
			this.P_Sync.AddShadow = true;
			this.P_Sync.AutoSize = true;
			this.P_Sync.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Sync.ColumnCount = 1;
			this.P_Sync.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.P_Sync.Controls.Add(this.smartTablePanel1, 0, 1);
			this.P_Sync.Controls.Add(this.L_PdxSyncInfo, 0, 0);
			this.P_Sync.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon6.Name = "PDXMods";
			this.P_Sync.ImageName = dynamicIcon6;
			this.P_Sync.Info = "";
			this.P_Sync.Location = new System.Drawing.Point(0, 0);
			this.P_Sync.Name = "P_Sync";
			this.P_Sync.Padding = new System.Windows.Forms.Padding(16, 53, 16, 16);
			this.P_Sync.RowCount = 2;
			this.P_Sync.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Sync.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Sync.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.P_Sync.Size = new System.Drawing.Size(414, 137);
			this.P_Sync.TabIndex = 24;
			this.P_Sync.Text = "PdxSync";
			// 
			// smartTablePanel1
			// 
			this.smartTablePanel1.ColumnCount = 3;
			this.smartTablePanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.smartTablePanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.smartTablePanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.smartTablePanel1.Controls.Add(this.L_SyncStatusLabel, 0, 0);
			this.smartTablePanel1.Controls.Add(this.B_RunSync, 2, 0);
			this.smartTablePanel1.Controls.Add(this.L_SyncStatus, 1, 0);
			this.smartTablePanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smartTablePanel1.Location = new System.Drawing.Point(16, 89);
			this.smartTablePanel1.Margin = new System.Windows.Forms.Padding(0);
			this.smartTablePanel1.Name = "smartTablePanel1";
			this.smartTablePanel1.RowCount = 1;
			this.smartTablePanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.smartTablePanel1.Size = new System.Drawing.Size(382, 32);
			this.smartTablePanel1.TabIndex = 22;
			// 
			// L_SyncStatusLabel
			// 
			this.L_SyncStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_SyncStatusLabel.AutoSize = true;
			this.L_SyncStatusLabel.Location = new System.Drawing.Point(3, 9);
			this.L_SyncStatusLabel.Name = "L_SyncStatusLabel";
			this.L_SyncStatusLabel.Size = new System.Drawing.Size(84, 13);
			this.L_SyncStatusLabel.TabIndex = 17;
			this.L_SyncStatusLabel.Text = "autoSizeLabel1";
			// 
			// B_RunSync
			// 
			this.B_RunSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_RunSync.AutoSize = true;
			this.B_RunSync.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "Sync";
			this.B_RunSync.ImageName = dynamicIcon5;
			this.B_RunSync.Location = new System.Drawing.Point(318, 3);
			this.B_RunSync.Name = "B_RunSync";
			this.B_RunSync.Size = new System.Drawing.Size(61, 26);
			this.B_RunSync.SpaceTriggersClick = true;
			this.B_RunSync.TabIndex = 15;
			this.B_RunSync.Text = "RunSync";
			this.B_RunSync.Click += new System.EventHandler(this.B_RunSync_Click);
			// 
			// L_SyncStatus
			// 
			this.L_SyncStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_SyncStatus.AutoSize = true;
			this.L_SyncStatus.Location = new System.Drawing.Point(90, 9);
			this.L_SyncStatus.Margin = new System.Windows.Forms.Padding(0);
			this.L_SyncStatus.Name = "L_SyncStatus";
			this.L_SyncStatus.Size = new System.Drawing.Size(84, 13);
			this.L_SyncStatus.TabIndex = 18;
			this.L_SyncStatus.Text = "autoSizeLabel1";
			// 
			// L_PdxSyncInfo
			// 
			this.L_PdxSyncInfo.AutoSize = true;
			this.L_PdxSyncInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.L_PdxSyncInfo.Location = new System.Drawing.Point(19, 56);
			this.L_PdxSyncInfo.Name = "L_PdxSyncInfo";
			this.L_PdxSyncInfo.Size = new System.Drawing.Size(376, 30);
			this.L_PdxSyncInfo.TabIndex = 16;
			this.L_PdxSyncInfo.Text = "autoSizeLabel1";
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel2.Controls.Add(this.P_Reset);
			this.panel2.Controls.Add(this.P_Issues);
			this.panel2.Controls.Add(this.P_SafeMode);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(423, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(274, 441);
			this.panel2.TabIndex = 19;
			// 
			// P_Reset
			// 
			this.P_Reset.AddOutline = true;
			this.P_Reset.AddShadow = true;
			this.P_Reset.AutoSize = true;
			this.P_Reset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Reset.ColorStyle = Extensions.ColorStyle.Red;
			this.P_Reset.ColumnCount = 2;
			this.P_Reset.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.P_Reset.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.P_Reset.Controls.Add(this.B_ReloadAllData, 0, 0);
			this.P_Reset.Controls.Add(this.B_ResetCompatibilityCache, 1, 1);
			this.P_Reset.Controls.Add(this.B_ResetModsCache, 0, 1);
			this.P_Reset.Controls.Add(this.B_ResetSnoozes, 1, 0);
			this.P_Reset.Controls.Add(this.B_ResetImageCache, 0, 2);
			this.P_Reset.Controls.Add(this.B_ResetSteamCache, 1, 2);
			this.P_Reset.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon13.Name = "Undo";
			this.P_Reset.ImageName = dynamicIcon13;
			this.P_Reset.Info = "ResetInfo";
			this.P_Reset.Location = new System.Drawing.Point(0, 276);
			this.P_Reset.Name = "P_Reset";
			this.P_Reset.Padding = new System.Windows.Forms.Padding(16, 53, 16, 16);
			this.P_Reset.RowCount = 3;
			this.P_Reset.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Reset.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Reset.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Reset.Size = new System.Drawing.Size(274, 165);
			this.P_Reset.TabIndex = 22;
			this.P_Reset.Text = "Reset";
			// 
			// B_ReloadAllData
			// 
			this.B_ReloadAllData.AutoSize = true;
			this.B_ReloadAllData.ColorStyle = Extensions.ColorStyle.Yellow;
			this.B_ReloadAllData.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon7.Name = "Refresh";
			this.B_ReloadAllData.ImageName = dynamicIcon7;
			this.B_ReloadAllData.Location = new System.Drawing.Point(19, 56);
			this.B_ReloadAllData.Name = "B_ReloadAllData";
			this.B_ReloadAllData.Size = new System.Drawing.Size(110, 26);
			this.B_ReloadAllData.SpaceTriggersClick = true;
			this.B_ReloadAllData.TabIndex = 15;
			this.B_ReloadAllData.Text = "ReloadAllData";
			this.B_ReloadAllData.Click += new System.EventHandler(this.B_ReloadAllData_Click);
			// 
			// B_ResetCompatibilityCache
			// 
			this.B_ResetCompatibilityCache.AutoSize = true;
			this.B_ResetCompatibilityCache.ColorStyle = Extensions.ColorStyle.Orange;
			this.B_ResetCompatibilityCache.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon8.Name = "CompatibilityReport";
			this.B_ResetCompatibilityCache.ImageName = dynamicIcon8;
			this.B_ResetCompatibilityCache.Location = new System.Drawing.Point(140, 88);
			this.B_ResetCompatibilityCache.Name = "B_ResetCompatibilityCache";
			this.B_ResetCompatibilityCache.Size = new System.Drawing.Size(115, 26);
			this.B_ResetCompatibilityCache.SpaceTriggersClick = true;
			this.B_ResetCompatibilityCache.TabIndex = 15;
			this.B_ResetCompatibilityCache.Text = "ResetCompatibilityCache";
			this.B_ResetCompatibilityCache.Click += new System.EventHandler(this.B_ResetCompatibilityCache_Click);
			// 
			// B_ResetModsCache
			// 
			this.B_ResetModsCache.AutoSize = true;
			this.B_ResetModsCache.ColorStyle = Extensions.ColorStyle.Orange;
			this.B_ResetModsCache.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon9.Name = "Mods";
			this.B_ResetModsCache.ImageName = dynamicIcon9;
			this.B_ResetModsCache.Location = new System.Drawing.Point(19, 88);
			this.B_ResetModsCache.Name = "B_ResetModsCache";
			this.B_ResetModsCache.Size = new System.Drawing.Size(104, 26);
			this.B_ResetModsCache.SpaceTriggersClick = true;
			this.B_ResetModsCache.TabIndex = 15;
			this.B_ResetModsCache.Text = "ResetModsCache";
			this.B_ResetModsCache.Click += new System.EventHandler(this.B_ResetModsCache_Click);
			// 
			// B_ResetSnoozes
			// 
			this.B_ResetSnoozes.AutoSize = true;
			this.B_ResetSnoozes.ColorStyle = Extensions.ColorStyle.Yellow;
			this.B_ResetSnoozes.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon10.Name = "Snooze";
			this.B_ResetSnoozes.ImageName = dynamicIcon10;
			this.B_ResetSnoozes.Location = new System.Drawing.Point(140, 56);
			this.B_ResetSnoozes.Name = "B_ResetSnoozes";
			this.B_ResetSnoozes.Size = new System.Drawing.Size(87, 26);
			this.B_ResetSnoozes.SpaceTriggersClick = true;
			this.B_ResetSnoozes.TabIndex = 15;
			this.B_ResetSnoozes.Text = "ResetSnoozes";
			this.B_ResetSnoozes.Click += new System.EventHandler(this.B_ResetSnoozes_Click);
			// 
			// B_ResetImageCache
			// 
			this.B_ResetImageCache.AutoSize = true;
			this.B_ResetImageCache.ColorStyle = Extensions.ColorStyle.Red;
			this.B_ResetImageCache.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon11.Name = "RemoveImage";
			this.B_ResetImageCache.ImageName = dynamicIcon11;
			this.B_ResetImageCache.Location = new System.Drawing.Point(19, 120);
			this.B_ResetImageCache.Name = "B_ResetImageCache";
			this.B_ResetImageCache.Size = new System.Drawing.Size(107, 26);
			this.B_ResetImageCache.SpaceTriggersClick = true;
			this.B_ResetImageCache.TabIndex = 15;
			this.B_ResetImageCache.Text = "ResetImageCache";
			this.B_ResetImageCache.Click += new System.EventHandler(this.B_ResetImageCache_Click);
			// 
			// B_ResetSteamCache
			// 
			this.B_ResetSteamCache.AutoSize = true;
			this.B_ResetSteamCache.ColorStyle = Extensions.ColorStyle.Red;
			this.B_ResetSteamCache.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon12.Name = "PDXMods";
			this.B_ResetSteamCache.ImageName = dynamicIcon12;
			this.B_ResetSteamCache.Location = new System.Drawing.Point(140, 120);
			this.B_ResetSteamCache.Name = "B_ResetSteamCache";
			this.B_ResetSteamCache.Size = new System.Drawing.Size(115, 26);
			this.B_ResetSteamCache.SpaceTriggersClick = true;
			this.B_ResetSteamCache.TabIndex = 15;
			this.B_ResetSteamCache.Text = "ResetSteamCache";
			this.B_ResetSteamCache.Click += new System.EventHandler(this.B_ResetSteamCache_Click);
			// 
			// P_Issues
			// 
			this.P_Issues.AddOutline = true;
			this.P_Issues.AddShadow = true;
			this.P_Issues.AutoSize = true;
			this.P_Issues.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Issues.ColorStyle = Extensions.ColorStyle.Yellow;
			this.P_Issues.ColumnCount = 1;
			this.P_Issues.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_Issues.Controls.Add(this.B_FixAllIssues, 0, 1);
			this.P_Issues.Controls.Add(this.outOfDatePackagesControl1, 0, 0);
			this.P_Issues.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon15.Name = "ModWarning";
			this.P_Issues.ImageName = dynamicIcon15;
			this.P_Issues.Location = new System.Drawing.Point(0, 114);
			this.P_Issues.Name = "P_Issues";
			this.P_Issues.Padding = new System.Windows.Forms.Padding(16);
			this.P_Issues.RowCount = 1;
			this.P_Issues.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Issues.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Issues.Size = new System.Drawing.Size(274, 162);
			this.P_Issues.TabIndex = 24;
			this.P_Issues.Text = "DetectedIssues";
			// 
			// B_FixAllIssues
			// 
			this.B_FixAllIssues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_FixAllIssues.AutoSize = true;
			this.B_FixAllIssues.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon14.Name = "Tools";
			this.B_FixAllIssues.ImageName = dynamicIcon14;
			this.B_FixAllIssues.Location = new System.Drawing.Point(175, 117);
			this.B_FixAllIssues.Name = "B_FixAllIssues";
			this.B_FixAllIssues.Size = new System.Drawing.Size(80, 26);
			this.B_FixAllIssues.SpaceTriggersClick = true;
			this.B_FixAllIssues.TabIndex = 14;
			this.B_FixAllIssues.Text = "FixAllIssues";
			this.B_FixAllIssues.Click += new System.EventHandler(this.B_FixAllIssues_Click);
			// 
			// outOfDatePackagesControl1
			// 
			this.outOfDatePackagesControl1.AutoInvalidate = false;
			this.outOfDatePackagesControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.outOfDatePackagesControl1.Location = new System.Drawing.Point(19, 19);
			this.outOfDatePackagesControl1.Name = "outOfDatePackagesControl1";
			this.outOfDatePackagesControl1.Size = new System.Drawing.Size(236, 92);
			this.outOfDatePackagesControl1.TabIndex = 18;
			// 
			// P_SafeMode
			// 
			this.P_SafeMode.AddOutline = true;
			this.P_SafeMode.AddShadow = true;
			this.P_SafeMode.AutoSize = true;
			this.P_SafeMode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_SafeMode.ColorStyle = Extensions.ColorStyle.Green;
			this.P_SafeMode.ColumnCount = 1;
			this.P_SafeMode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_SafeMode.Controls.Add(this.B_SafeMode, 0, 1);
			this.P_SafeMode.Controls.Add(this.L_SafeMode, 0, 0);
			this.P_SafeMode.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon17.Name = "Shield";
			this.P_SafeMode.ImageName = dynamicIcon17;
			this.P_SafeMode.Location = new System.Drawing.Point(0, 0);
			this.P_SafeMode.Name = "P_SafeMode";
			this.P_SafeMode.Padding = new System.Windows.Forms.Padding(16, 53, 16, 16);
			this.P_SafeMode.RowCount = 1;
			this.P_SafeMode.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_SafeMode.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_SafeMode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.P_SafeMode.Size = new System.Drawing.Size(274, 114);
			this.P_SafeMode.TabIndex = 25;
			this.P_SafeMode.Text = "SafeMode";
			// 
			// B_SafeMode
			// 
			this.B_SafeMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_SafeMode.AutoSize = true;
			this.B_SafeMode.ColorStyle = Extensions.ColorStyle.Green;
			this.B_SafeMode.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon16.Name = "CS";
			this.B_SafeMode.ImageName = dynamicIcon16;
			this.B_SafeMode.Location = new System.Drawing.Point(120, 69);
			this.B_SafeMode.Name = "B_SafeMode";
			this.B_SafeMode.Size = new System.Drawing.Size(135, 26);
			this.B_SafeMode.SpaceTriggersClick = true;
			this.B_SafeMode.TabIndex = 14;
			this.B_SafeMode.Text = "LaunchInSafeMode";
			this.B_SafeMode.Click += new System.EventHandler(this.B_SafeMode_Click);
			// 
			// L_SafeMode
			// 
			this.L_SafeMode.AutoSize = true;
			this.L_SafeMode.Location = new System.Drawing.Point(19, 53);
			this.L_SafeMode.Name = "L_SafeMode";
			this.L_SafeMode.Size = new System.Drawing.Size(38, 13);
			this.L_SafeMode.TabIndex = 15;
			this.L_SafeMode.Text = "label1";
			// 
			// P_Container
			// 
			this.P_Container.Controls.Add(this.TLP_Main);
			this.P_Container.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Container.Location = new System.Drawing.Point(0, 31);
			this.P_Container.Name = "P_Container";
			this.P_Container.Size = new System.Drawing.Size(763, 396);
			this.P_Container.TabIndex = 19;
			// 
			// slickSpacer1
			// 
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(0, 30);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(763, 1);
			this.slickSpacer1.TabIndex = 20;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// slickScroll1
			// 
			this.slickScroll1.AnimatedValue = 8;
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.TLP_Main;
			this.slickScroll1.Location = new System.Drawing.Point(763, 30);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(16, 397);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 21;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.TargetAnimationValue = 8;
			this.slickScroll1.Text = "slickScroll1";
			this.slickScroll1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SlickScroll_Scroll);
			// 
			// PC_Utilities
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.P_Container);
			this.Controls.Add(this.slickSpacer1);
			this.Controls.Add(this.slickScroll1);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_Utilities";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(779, 427);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.slickScroll1, 0);
			this.Controls.SetChildIndex(this.slickSpacer1, 0);
			this.Controls.SetChildIndex(this.P_Container, 0);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.P_Troubleshoot.ResumeLayout(false);
			this.P_Troubleshoot.PerformLayout();
			this.P_Text.ResumeLayout(false);
			this.P_Text.PerformLayout();
			this.P_Sync.ResumeLayout(false);
			this.P_Sync.PerformLayout();
			this.smartTablePanel1.ResumeLayout(false);
			this.smartTablePanel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.P_Reset.ResumeLayout(false);
			this.P_Reset.PerformLayout();
			this.P_Issues.ResumeLayout(false);
			this.P_Issues.PerformLayout();
			this.P_SafeMode.ResumeLayout(false);
			this.P_SafeMode.PerformLayout();
			this.P_Container.ResumeLayout(false);
			this.P_Container.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private SmartTablePanel TLP_Main;
	private System.Windows.Forms.Panel P_Container;
	private SlickControls.RoundedGroupTableLayoutPanel P_Text;
	private DragAndDropControl DD_TextImport;
	private SlickControls.SlickButton B_ImportClipboard;
	private SlickControls.SlickSpacer slickSpacer1;
	private SlickControls.RoundedGroupTableLayoutPanel P_Reset;
	private SlickControls.SlickButton B_ResetImageCache;
	private SlickControls.SlickButton B_ResetSnoozes;
	private SlickControls.SlickButton B_ReloadAllData;
	private SlickControls.SlickButton B_ResetSteamCache;
	private SlickControls.SlickButton B_ResetCompatibilityCache;
	private SlickControls.SlickButton B_ResetModsCache;
	private RoundedGroupTableLayoutPanel P_Troubleshoot;
	private SlickButton B_Troubleshoot;
	private AutoSizeLabel L_Troubleshoot;
	private RoundedGroupTableLayoutPanel P_Sync;
	private AutoSizeLabel L_PdxSyncInfo;
	private SlickButton B_RunSync;
	private SmartTablePanel smartTablePanel1;
	private System.Windows.Forms.Label L_SyncStatusLabel;
	private System.Windows.Forms.Label L_SyncStatus;
	private System.Windows.Forms.Panel panel1;
	private System.Windows.Forms.Panel panel2;
	private RoundedGroupTableLayoutPanel P_Issues;
	private SlickButton B_FixAllIssues;
	private Content.OutOfDatePackagesControl outOfDatePackagesControl1;
	private SlickScroll slickScroll1;
	private RoundedGroupTableLayoutPanel P_SafeMode;
	private SlickButton B_SafeMode;
	private System.Windows.Forms.Label L_SafeMode;
}
