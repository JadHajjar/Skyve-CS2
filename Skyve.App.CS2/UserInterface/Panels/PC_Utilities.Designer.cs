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
			SlickControls.DynamicIcon dynamicIcon11 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon13 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon12 = new SlickControls.DynamicIcon();
			this.TLP_Main = new SlickControls.SmartTablePanel();
			this.P_Sync = new SlickControls.RoundedGroupTableLayoutPanel();
			this.smartTablePanel1 = new SlickControls.SmartTablePanel();
			this.L_SyncStatusLabel = new System.Windows.Forms.Label();
			this.B_RunSync = new SlickControls.SlickButton();
			this.L_SyncStatus = new System.Windows.Forms.Label();
			this.L_PdxSyncInfo = new SlickControls.AutoSizeLabel();
			this.P_Troubleshoot = new SlickControls.RoundedGroupTableLayoutPanel();
			this.L_Troubleshoot = new SlickControls.AutoSizeLabel();
			this.B_Troubleshoot = new SlickControls.SlickButton();
			this.P_Reset = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_ReloadAllData = new SlickControls.SlickButton();
			this.B_ResetCompatibilityCache = new SlickControls.SlickButton();
			this.B_ResetModsCache = new SlickControls.SlickButton();
			this.B_ResetSnoozes = new SlickControls.SlickButton();
			this.B_ResetImageCache = new SlickControls.SlickButton();
			this.B_ResetSteamCache = new SlickControls.SlickButton();
			this.P_Text = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_ImportClipboard = new SlickControls.SlickButton();
			this.DD_TextImport = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.P_Container = new System.Windows.Forms.Panel();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.TLP_Main.SuspendLayout();
			this.P_Sync.SuspendLayout();
			this.smartTablePanel1.SuspendLayout();
			this.P_Troubleshoot.SuspendLayout();
			this.P_Reset.SuspendLayout();
			this.P_Text.SuspendLayout();
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
			this.TLP_Main.Controls.Add(this.P_Sync, 0, 0);
			this.TLP_Main.Controls.Add(this.P_Troubleshoot, 0, 2);
			this.TLP_Main.Controls.Add(this.P_Reset, 1, 0);
			this.TLP_Main.Controls.Add(this.P_Text, 0, 1);
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.MinimumSize = new System.Drawing.Size(700, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 3;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.Size = new System.Drawing.Size(1140, 492);
			this.TLP_Main.TabIndex = 17;
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
			dynamicIcon2.Name = "PDXMods";
			this.P_Sync.ImageName = dynamicIcon2;
			this.P_Sync.Info = "";
			this.P_Sync.Location = new System.Drawing.Point(3, 3);
			this.P_Sync.Name = "P_Sync";
			this.P_Sync.Padding = new System.Windows.Forms.Padding(20);
			this.P_Sync.RowCount = 2;
			this.P_Sync.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Sync.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Sync.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.P_Sync.Size = new System.Drawing.Size(678, 122);
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
			this.smartTablePanel1.Location = new System.Drawing.Point(20, 64);
			this.smartTablePanel1.Margin = new System.Windows.Forms.Padding(0);
			this.smartTablePanel1.Name = "smartTablePanel1";
			this.smartTablePanel1.RowCount = 1;
			this.smartTablePanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.smartTablePanel1.Size = new System.Drawing.Size(638, 38);
			this.smartTablePanel1.TabIndex = 22;
			// 
			// L_SyncStatusLabel
			// 
			this.L_SyncStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_SyncStatusLabel.AutoSize = true;
			this.L_SyncStatusLabel.Location = new System.Drawing.Point(3, 9);
			this.L_SyncStatusLabel.Name = "L_SyncStatusLabel";
			this.L_SyncStatusLabel.Size = new System.Drawing.Size(100, 19);
			this.L_SyncStatusLabel.TabIndex = 17;
			this.L_SyncStatusLabel.Text = "autoSizeLabel1";
			// 
			// B_RunSync
			// 
			this.B_RunSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_RunSync.AutoSize = true;
			this.B_RunSync.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "Sync";
			this.B_RunSync.ImageName = dynamicIcon1;
			this.B_RunSync.Location = new System.Drawing.Point(537, 3);
			this.B_RunSync.Name = "B_RunSync";
			this.B_RunSync.Size = new System.Drawing.Size(98, 32);
			this.B_RunSync.SpaceTriggersClick = true;
			this.B_RunSync.TabIndex = 15;
			this.B_RunSync.Text = "RunSync";
			this.B_RunSync.Click += new System.EventHandler(this.B_RunSync_Click);
			// 
			// L_SyncStatus
			// 
			this.L_SyncStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_SyncStatus.AutoSize = true;
			this.L_SyncStatus.Location = new System.Drawing.Point(106, 9);
			this.L_SyncStatus.Margin = new System.Windows.Forms.Padding(0);
			this.L_SyncStatus.Name = "L_SyncStatus";
			this.L_SyncStatus.Size = new System.Drawing.Size(100, 19);
			this.L_SyncStatus.TabIndex = 18;
			this.L_SyncStatus.Text = "autoSizeLabel1";
			// 
			// L_PdxSyncInfo
			// 
			this.L_PdxSyncInfo.AutoSize = true;
			this.L_PdxSyncInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.L_PdxSyncInfo.Location = new System.Drawing.Point(23, 23);
			this.L_PdxSyncInfo.Name = "L_PdxSyncInfo";
			this.L_PdxSyncInfo.Size = new System.Drawing.Size(632, 38);
			this.L_PdxSyncInfo.TabIndex = 16;
			this.L_PdxSyncInfo.Text = "autoSizeLabel1";
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
			dynamicIcon4.Name = "Wrench";
			this.P_Troubleshoot.ImageName = dynamicIcon4;
			this.P_Troubleshoot.Location = new System.Drawing.Point(3, 363);
			this.P_Troubleshoot.Name = "P_Troubleshoot";
			this.P_Troubleshoot.Padding = new System.Windows.Forms.Padding(20, 58, 20, 20);
			this.P_Troubleshoot.RowCount = 1;
			this.P_Troubleshoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Troubleshoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Troubleshoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.P_Troubleshoot.Size = new System.Drawing.Size(678, 126);
			this.P_Troubleshoot.TabIndex = 23;
			this.P_Troubleshoot.Text = "TroubleshootIssues";
			// 
			// L_Troubleshoot
			// 
			this.L_Troubleshoot.AutoSize = true;
			this.L_Troubleshoot.Location = new System.Drawing.Point(23, 68);
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
			dynamicIcon3.Name = "ArrowRight";
			this.B_Troubleshoot.ImageName = dynamicIcon3;
			this.B_Troubleshoot.Location = new System.Drawing.Point(448, 71);
			this.B_Troubleshoot.Name = "B_Troubleshoot";
			this.B_Troubleshoot.Size = new System.Drawing.Size(207, 32);
			this.B_Troubleshoot.SpaceTriggersClick = true;
			this.B_Troubleshoot.TabIndex = 14;
			this.B_Troubleshoot.Text = "ViewTroubleshootOptions";
			this.B_Troubleshoot.Click += new System.EventHandler(this.B_Troubleshoot_Click);
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
			dynamicIcon11.Name = "Undo";
			this.P_Reset.ImageName = dynamicIcon11;
			this.P_Reset.Info = "ResetInfo";
			this.P_Reset.Location = new System.Drawing.Point(687, 3);
			this.P_Reset.Name = "P_Reset";
			this.P_Reset.Padding = new System.Windows.Forms.Padding(20, 58, 20, 20);
			this.P_Reset.RowCount = 3;
			this.TLP_Main.SetRowSpan(this.P_Reset, 3);
			this.P_Reset.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Reset.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Reset.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Reset.Size = new System.Drawing.Size(450, 192);
			this.P_Reset.TabIndex = 22;
			this.P_Reset.Text = "Reset";
			// 
			// B_ReloadAllData
			// 
			this.B_ReloadAllData.AutoSize = true;
			this.B_ReloadAllData.ColorStyle = Extensions.ColorStyle.Yellow;
			this.B_ReloadAllData.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "Refresh";
			this.B_ReloadAllData.ImageName = dynamicIcon5;
			this.B_ReloadAllData.Location = new System.Drawing.Point(23, 61);
			this.B_ReloadAllData.Name = "B_ReloadAllData";
			this.B_ReloadAllData.Size = new System.Drawing.Size(132, 32);
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
			dynamicIcon6.Name = "CompatibilityReport";
			this.B_ResetCompatibilityCache.ImageName = dynamicIcon6;
			this.B_ResetCompatibilityCache.Location = new System.Drawing.Point(228, 99);
			this.B_ResetCompatibilityCache.Name = "B_ResetCompatibilityCache";
			this.B_ResetCompatibilityCache.Size = new System.Drawing.Size(199, 32);
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
			dynamicIcon7.Name = "Mods";
			this.B_ResetModsCache.ImageName = dynamicIcon7;
			this.B_ResetModsCache.Location = new System.Drawing.Point(23, 99);
			this.B_ResetModsCache.Name = "B_ResetModsCache";
			this.B_ResetModsCache.Size = new System.Drawing.Size(151, 32);
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
			dynamicIcon8.Name = "Snooze";
			this.B_ResetSnoozes.ImageName = dynamicIcon8;
			this.B_ResetSnoozes.Location = new System.Drawing.Point(228, 61);
			this.B_ResetSnoozes.Name = "B_ResetSnoozes";
			this.B_ResetSnoozes.Size = new System.Drawing.Size(130, 32);
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
			dynamicIcon9.Name = "RemoveImage";
			this.B_ResetImageCache.ImageName = dynamicIcon9;
			this.B_ResetImageCache.Location = new System.Drawing.Point(23, 137);
			this.B_ResetImageCache.Name = "B_ResetImageCache";
			this.B_ResetImageCache.Size = new System.Drawing.Size(155, 32);
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
			dynamicIcon10.Name = "PDXMods";
			this.B_ResetSteamCache.ImageName = dynamicIcon10;
			this.B_ResetSteamCache.Location = new System.Drawing.Point(228, 137);
			this.B_ResetSteamCache.Name = "B_ResetSteamCache";
			this.B_ResetSteamCache.Size = new System.Drawing.Size(155, 32);
			this.B_ResetSteamCache.SpaceTriggersClick = true;
			this.B_ResetSteamCache.TabIndex = 15;
			this.B_ResetSteamCache.Text = "ResetSteamCache";
			this.B_ResetSteamCache.Click += new System.EventHandler(this.B_ResetSteamCache_Click);
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
			dynamicIcon13.Name = "Text";
			this.P_Text.ImageName = dynamicIcon13;
			this.P_Text.Info = "ImportFromTextInfo";
			this.P_Text.Location = new System.Drawing.Point(3, 131);
			this.P_Text.Name = "P_Text";
			this.P_Text.Padding = new System.Windows.Forms.Padding(20, 58, 20, 20);
			this.P_Text.RowCount = 2;
			this.P_Text.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Text.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Text.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.P_Text.Size = new System.Drawing.Size(678, 226);
			this.P_Text.TabIndex = 20;
			this.P_Text.Text = "ImportFromText";
			// 
			// B_ImportClipboard
			// 
			this.B_ImportClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_ImportClipboard.AutoSize = true;
			this.B_ImportClipboard.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon12.Name = "Copy";
			this.B_ImportClipboard.ImageName = dynamicIcon12;
			this.B_ImportClipboard.Location = new System.Drawing.Point(475, 171);
			this.B_ImportClipboard.Name = "B_ImportClipboard";
			this.B_ImportClipboard.Size = new System.Drawing.Size(180, 32);
			this.B_ImportClipboard.SpaceTriggersClick = true;
			this.B_ImportClipboard.TabIndex = 15;
			this.B_ImportClipboard.Text = "ImportFromClipboard";
			this.B_ImportClipboard.Click += new System.EventHandler(this.B_ImportClipboard_Click);
			// 
			// DD_TextImport
			// 
			this.DD_TextImport.AllowDrop = true;
			this.DD_TextImport.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_TextImport.Location = new System.Drawing.Point(23, 61);
			this.DD_TextImport.Name = "DD_TextImport";
			this.DD_TextImport.Size = new System.Drawing.Size(631, 104);
			this.DD_TextImport.TabIndex = 17;
			this.DD_TextImport.Text = "TextImportMissingInfo";
			this.DD_TextImport.ValidExtensions = new string[] {
        ".txt"};
			this.DD_TextImport.FileSelected += new System.Action<string>(this.DD_TextImport_FileSelected);
			this.DD_TextImport.ValidFile += new System.Func<object, string, bool>(this.DD_TextImport_ValidFile);
			// 
			// slickScroll1
			// 
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.TLP_Main;
			this.slickScroll1.Location = new System.Drawing.Point(1720, 31);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(6, 1046);
			this.slickScroll1.SmallHandle = true;
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 18;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.Text = "slickScroll1";
			this.slickScroll1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.slickScroll1_Scroll);
			// 
			// P_Container
			// 
			this.P_Container.Controls.Add(this.TLP_Main);
			this.P_Container.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Container.Location = new System.Drawing.Point(0, 31);
			this.P_Container.Name = "P_Container";
			this.P_Container.Size = new System.Drawing.Size(1720, 1046);
			this.P_Container.TabIndex = 19;
			// 
			// slickSpacer1
			// 
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(0, 30);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(1726, 1);
			this.slickSpacer1.TabIndex = 20;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// PC_Utilities
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.P_Container);
			this.Controls.Add(this.slickScroll1);
			this.Controls.Add(this.slickSpacer1);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_Utilities";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(1726, 1077);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.slickSpacer1, 0);
			this.Controls.SetChildIndex(this.slickScroll1, 0);
			this.Controls.SetChildIndex(this.P_Container, 0);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.P_Sync.ResumeLayout(false);
			this.P_Sync.PerformLayout();
			this.smartTablePanel1.ResumeLayout(false);
			this.smartTablePanel1.PerformLayout();
			this.P_Troubleshoot.ResumeLayout(false);
			this.P_Troubleshoot.PerformLayout();
			this.P_Reset.ResumeLayout(false);
			this.P_Reset.PerformLayout();
			this.P_Text.ResumeLayout(false);
			this.P_Text.PerformLayout();
			this.P_Container.ResumeLayout(false);
			this.P_Container.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private SmartTablePanel TLP_Main;
	private SlickControls.SlickScroll slickScroll1;
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
}
