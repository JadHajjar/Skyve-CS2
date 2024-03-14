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
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon11 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon12 = new SlickControls.DynamicIcon();
			this.TLP_Main = new SlickControls.SmartTablePanel();
			this.P_Troubleshoot = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_Troubleshoot = new SlickControls.SlickButton();
			this.L_Troubleshoot = new System.Windows.Forms.Label();
			this.P_Reset = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_ResetImageCache = new SlickControls.SlickButton();
			this.B_ResetSnoozes = new SlickControls.SlickButton();
			this.B_ReloadAllData = new SlickControls.SlickButton();
			this.B_ResetSteamCache = new SlickControls.SlickButton();
			this.B_ResetCompatibilityCache = new SlickControls.SlickButton();
			this.B_ResetModsCache = new SlickControls.SlickButton();
			this.P_Text = new SlickControls.RoundedGroupPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.DD_TextImport = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.B_ImportClipboard = new SlickControls.SlickButton();
			this.P_BOB = new SlickControls.RoundedGroupPanel();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.DD_BOB = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.P_Container = new System.Windows.Forms.Panel();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.TLP_Main.SuspendLayout();
			this.P_Troubleshoot.SuspendLayout();
			this.P_Reset.SuspendLayout();
			this.P_Text.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.P_BOB.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.P_Container.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(150, 32);
			// 
			// TLP_Main
			// 
			this.TLP_Main.AutoSize = true;
			this.TLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.Controls.Add(this.P_Troubleshoot, 0, 1);
			this.TLP_Main.Controls.Add(this.P_Reset, 0, 2);
			this.TLP_Main.Controls.Add(this.P_Text, 0, 0);
			this.TLP_Main.Controls.Add(this.P_BOB, 1, 0);
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.MinimumSize = new System.Drawing.Size(700, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 3;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Size = new System.Drawing.Size(1056, 373);
			this.TLP_Main.TabIndex = 17;
			// 
			// P_Troubleshoot
			// 
			this.P_Troubleshoot.AddOutline = true;
			this.P_Troubleshoot.AutoSize = true;
			this.P_Troubleshoot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Troubleshoot.ColorStyle = Extensions.ColorStyle.Yellow;
			this.P_Troubleshoot.ColumnCount = 2;
			this.TLP_Main.SetColumnSpan(this.P_Troubleshoot, 2);
			this.P_Troubleshoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_Troubleshoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.P_Troubleshoot.Controls.Add(this.B_Troubleshoot, 1, 0);
			this.P_Troubleshoot.Controls.Add(this.L_Troubleshoot, 0, 1);
			this.P_Troubleshoot.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon2.Name = "Wrench";
			this.P_Troubleshoot.ImageName = dynamicIcon2;
			this.P_Troubleshoot.Location = new System.Drawing.Point(3, 193);
			this.P_Troubleshoot.Name = "P_Troubleshoot";
			this.P_Troubleshoot.Padding = new System.Windows.Forms.Padding(6);
			this.P_Troubleshoot.RowCount = 2;
			this.P_Troubleshoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.P_Troubleshoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Troubleshoot.Size = new System.Drawing.Size(1050, 65);
			this.P_Troubleshoot.TabIndex = 23;
			this.P_Troubleshoot.Text = "TroubleshootIssues";
			this.P_Troubleshoot.UseFirstRowForPadding = true;
			// 
			// B_Troubleshoot
			// 
			this.B_Troubleshoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Troubleshoot.AutoSize = true;
			this.B_Troubleshoot.ColorShade = null;
			this.B_Troubleshoot.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "ArrowRight";
			this.B_Troubleshoot.ImageName = dynamicIcon1;
			this.B_Troubleshoot.Location = new System.Drawing.Point(864, 30);
			this.B_Troubleshoot.Name = "B_Troubleshoot";
			this.P_Troubleshoot.SetRowSpan(this.B_Troubleshoot, 2);
			this.B_Troubleshoot.Size = new System.Drawing.Size(177, 26);
			this.B_Troubleshoot.SpaceTriggersClick = true;
			this.B_Troubleshoot.TabIndex = 14;
			this.B_Troubleshoot.Text = "ViewTroubleshootOptions";
			this.B_Troubleshoot.Click += new System.EventHandler(this.B_Troubleshoot_Click);
			// 
			// L_Troubleshoot
			// 
			this.L_Troubleshoot.AutoSize = true;
			this.L_Troubleshoot.Location = new System.Drawing.Point(9, 46);
			this.L_Troubleshoot.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
			this.L_Troubleshoot.Name = "L_Troubleshoot";
			this.L_Troubleshoot.Size = new System.Drawing.Size(38, 13);
			this.L_Troubleshoot.TabIndex = 16;
			this.L_Troubleshoot.Text = "label1";
			// 
			// P_Reset
			// 
			this.P_Reset.AddOutline = true;
			this.P_Reset.AutoSize = true;
			this.P_Reset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Reset.ColorStyle = Extensions.ColorStyle.Red;
			this.P_Reset.ColumnCount = 3;
			this.TLP_Main.SetColumnSpan(this.P_Reset, 2);
			this.P_Reset.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.P_Reset.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.P_Reset.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.P_Reset.Controls.Add(this.B_ResetImageCache, 2, 0);
			this.P_Reset.Controls.Add(this.B_ResetSnoozes, 0, 1);
			this.P_Reset.Controls.Add(this.B_ReloadAllData, 0, 0);
			this.P_Reset.Controls.Add(this.B_ResetSteamCache, 2, 1);
			this.P_Reset.Controls.Add(this.B_ResetCompatibilityCache, 1, 1);
			this.P_Reset.Controls.Add(this.B_ResetModsCache, 1, 0);
			this.P_Reset.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon9.Name = "Undo";
			this.P_Reset.ImageName = dynamicIcon9;
			this.P_Reset.Info = "ResetInfo";
			this.P_Reset.Location = new System.Drawing.Point(3, 264);
			this.P_Reset.Name = "P_Reset";
			this.P_Reset.Padding = new System.Windows.Forms.Padding(6, 36, 6, 6);
			this.P_Reset.RowCount = 2;
			this.P_Reset.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Reset.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Reset.Size = new System.Drawing.Size(1050, 106);
			this.P_Reset.TabIndex = 22;
			this.P_Reset.Text = "Reset";
			// 
			// B_ResetImageCache
			// 
			this.B_ResetImageCache.AutoSize = true;
			this.B_ResetImageCache.ColorShade = null;
			this.B_ResetImageCache.ColorStyle = Extensions.ColorStyle.Red;
			this.B_ResetImageCache.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon3.Name = "RemoveImage";
			this.B_ResetImageCache.ImageName = dynamicIcon3;
			this.B_ResetImageCache.Location = new System.Drawing.Point(701, 39);
			this.B_ResetImageCache.Name = "B_ResetImageCache";
			this.B_ResetImageCache.Size = new System.Drawing.Size(132, 26);
			this.B_ResetImageCache.SpaceTriggersClick = true;
			this.B_ResetImageCache.TabIndex = 15;
			this.B_ResetImageCache.Text = "ResetImageCache";
			this.B_ResetImageCache.Click += new System.EventHandler(this.B_ResetImageCache_Click);
			// 
			// B_ResetSnoozes
			// 
			this.B_ResetSnoozes.AutoSize = true;
			this.B_ResetSnoozes.ColorShade = null;
			this.B_ResetSnoozes.ColorStyle = Extensions.ColorStyle.Yellow;
			this.B_ResetSnoozes.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "Snooze";
			this.B_ResetSnoozes.ImageName = dynamicIcon4;
			this.B_ResetSnoozes.Location = new System.Drawing.Point(9, 71);
			this.B_ResetSnoozes.Name = "B_ResetSnoozes";
			this.B_ResetSnoozes.Size = new System.Drawing.Size(110, 26);
			this.B_ResetSnoozes.SpaceTriggersClick = true;
			this.B_ResetSnoozes.TabIndex = 15;
			this.B_ResetSnoozes.Text = "ResetSnoozes";
			this.B_ResetSnoozes.Click += new System.EventHandler(this.B_ResetSnoozes_Click);
			// 
			// B_ReloadAllData
			// 
			this.B_ReloadAllData.AutoSize = true;
			this.B_ReloadAllData.ColorShade = null;
			this.B_ReloadAllData.ColorStyle = Extensions.ColorStyle.Yellow;
			this.B_ReloadAllData.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "Refresh";
			this.B_ReloadAllData.ImageName = dynamicIcon5;
			this.B_ReloadAllData.Location = new System.Drawing.Point(9, 39);
			this.B_ReloadAllData.Name = "B_ReloadAllData";
			this.B_ReloadAllData.Size = new System.Drawing.Size(112, 26);
			this.B_ReloadAllData.SpaceTriggersClick = true;
			this.B_ReloadAllData.TabIndex = 15;
			this.B_ReloadAllData.Text = "ReloadAllData";
			this.B_ReloadAllData.Click += new System.EventHandler(this.B_ReloadAllData_Click);
			// 
			// B_ResetSteamCache
			// 
			this.B_ResetSteamCache.AutoSize = true;
			this.B_ResetSteamCache.ColorShade = null;
			this.B_ResetSteamCache.ColorStyle = Extensions.ColorStyle.Red;
			this.B_ResetSteamCache.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon6.Name = "RemoveSteam";
			this.B_ResetSteamCache.ImageName = dynamicIcon6;
			this.B_ResetSteamCache.Location = new System.Drawing.Point(701, 71);
			this.B_ResetSteamCache.Name = "B_ResetSteamCache";
			this.B_ResetSteamCache.Size = new System.Drawing.Size(132, 26);
			this.B_ResetSteamCache.SpaceTriggersClick = true;
			this.B_ResetSteamCache.TabIndex = 15;
			this.B_ResetSteamCache.Text = "ResetSteamCache";
			this.B_ResetSteamCache.Click += new System.EventHandler(this.B_ResetSteamCache_Click);
			// 
			// B_ResetCompatibilityCache
			// 
			this.B_ResetCompatibilityCache.AutoSize = true;
			this.B_ResetCompatibilityCache.ColorShade = null;
			this.B_ResetCompatibilityCache.ColorStyle = Extensions.ColorStyle.Orange;
			this.B_ResetCompatibilityCache.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon7.Name = "CompatibilityReport";
			this.B_ResetCompatibilityCache.ImageName = dynamicIcon7;
			this.B_ResetCompatibilityCache.Location = new System.Drawing.Point(355, 71);
			this.B_ResetCompatibilityCache.Name = "B_ResetCompatibilityCache";
			this.B_ResetCompatibilityCache.Size = new System.Drawing.Size(170, 26);
			this.B_ResetCompatibilityCache.SpaceTriggersClick = true;
			this.B_ResetCompatibilityCache.TabIndex = 15;
			this.B_ResetCompatibilityCache.Text = "ResetCompatibilityCache";
			this.B_ResetCompatibilityCache.Click += new System.EventHandler(this.B_ResetCompatibilityCache_Click);
			// 
			// B_ResetModsCache
			// 
			this.B_ResetModsCache.AutoSize = true;
			this.B_ResetModsCache.ColorShade = null;
			this.B_ResetModsCache.ColorStyle = Extensions.ColorStyle.Orange;
			this.B_ResetModsCache.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon8.Name = "Mods";
			this.B_ResetModsCache.ImageName = dynamicIcon8;
			this.B_ResetModsCache.Location = new System.Drawing.Point(355, 39);
			this.B_ResetModsCache.Name = "B_ResetModsCache";
			this.B_ResetModsCache.Size = new System.Drawing.Size(128, 26);
			this.B_ResetModsCache.SpaceTriggersClick = true;
			this.B_ResetModsCache.TabIndex = 15;
			this.B_ResetModsCache.Text = "ResetModsCache";
			this.B_ResetModsCache.Click += new System.EventHandler(this.B_ResetModsCache_Click);
			// 
			// P_Text
			// 
			this.P_Text.AddOutline = true;
			this.P_Text.AutoSize = true;
			this.P_Text.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Text.Controls.Add(this.tableLayoutPanel3);
			this.P_Text.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon11.Name = "Text";
			this.P_Text.ImageName = dynamicIcon11;
			this.P_Text.Info = "ImportFromTextInfo";
			this.P_Text.Location = new System.Drawing.Point(3, 3);
			this.P_Text.Name = "P_Text";
			this.P_Text.Padding = new System.Windows.Forms.Padding(6, 36, 6, 6);
			this.P_Text.Size = new System.Drawing.Size(522, 184);
			this.P_Text.TabIndex = 20;
			this.P_Text.Text = "ImportFromText";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Controls.Add(this.DD_TextImport, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.B_ImportClipboard, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(6, 36);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(510, 142);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// DD_TextImport
			// 
			this.DD_TextImport.AllowDrop = true;
			this.DD_TextImport.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_TextImport.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_TextImport.Location = new System.Drawing.Point(3, 3);
			this.DD_TextImport.Name = "DD_TextImport";
			this.DD_TextImport.Size = new System.Drawing.Size(504, 104);
			this.DD_TextImport.TabIndex = 17;
			this.DD_TextImport.Text = "TextImportMissingInfo";
			this.DD_TextImport.ValidExtensions = new string[] {
        ".txt"};
			this.DD_TextImport.FileSelected += new System.Action<string>(this.DD_TextImport_FileSelected);
			this.DD_TextImport.ValidFile += new System.Func<object, string, bool>(this.DD_TextImport_ValidFile);
			// 
			// B_ImportClipboard
			// 
			this.B_ImportClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_ImportClipboard.AutoSize = true;
			this.B_ImportClipboard.ColorShade = null;
			this.B_ImportClipboard.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon10.Name = "Copy";
			this.B_ImportClipboard.ImageName = dynamicIcon10;
			this.B_ImportClipboard.Location = new System.Drawing.Point(353, 113);
			this.B_ImportClipboard.Name = "B_ImportClipboard";
			this.B_ImportClipboard.Size = new System.Drawing.Size(154, 26);
			this.B_ImportClipboard.SpaceTriggersClick = true;
			this.B_ImportClipboard.TabIndex = 15;
			this.B_ImportClipboard.Text = "ImportFromClipboard";
			this.B_ImportClipboard.Click += new System.EventHandler(this.B_ImportClipboard_Click);
			// 
			// P_BOB
			// 
			this.P_BOB.AddOutline = true;
			this.P_BOB.AutoSize = true;
			this.P_BOB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_BOB.Controls.Add(this.tableLayoutPanel6);
			this.P_BOB.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon12.Name = "XML";
			this.P_BOB.ImageName = dynamicIcon12;
			this.P_BOB.Info = "XMLImportInfo";
			this.P_BOB.Location = new System.Drawing.Point(531, 3);
			this.P_BOB.Name = "P_BOB";
			this.P_BOB.Padding = new System.Windows.Forms.Padding(6, 36, 6, 6);
			this.P_BOB.Size = new System.Drawing.Size(522, 155);
			this.P_BOB.TabIndex = 19;
			this.P_BOB.Text = "XMLImport";
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.AutoSize = true;
			this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel6.ColumnCount = 1;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel6.Controls.Add(this.DD_BOB, 0, 0);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(6, 36);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel6.Size = new System.Drawing.Size(510, 113);
			this.tableLayoutPanel6.TabIndex = 0;
			// 
			// DD_BOB
			// 
			this.DD_BOB.AllowDrop = true;
			this.DD_BOB.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_BOB.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_BOB.Location = new System.Drawing.Point(3, 3);
			this.DD_BOB.Name = "DD_BOB";
			this.DD_BOB.Size = new System.Drawing.Size(504, 107);
			this.DD_BOB.TabIndex = 16;
			this.DD_BOB.Text = "XMLImportMissingInfo";
			this.DD_BOB.ValidExtensions = new string[] {
        ".xml"};
			this.DD_BOB.FileSelected += new System.Action<string>(this.DD_BOB_FileSelected);
			this.DD_BOB.ValidFile += new System.Func<object, string, bool>(this.DD_BOB_ValidFile);
			// 
			// slickScroll1
			// 
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.TLP_Main;
			this.slickScroll1.Location = new System.Drawing.Point(1719, 31);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(7, 1046);
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
			this.P_Container.Size = new System.Drawing.Size(1719, 1046);
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
			this.P_Troubleshoot.ResumeLayout(false);
			this.P_Troubleshoot.PerformLayout();
			this.P_Reset.ResumeLayout(false);
			this.P_Reset.PerformLayout();
			this.P_Text.ResumeLayout(false);
			this.P_Text.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.P_BOB.ResumeLayout(false);
			this.P_BOB.PerformLayout();
			this.tableLayoutPanel6.ResumeLayout(false);
			this.P_Container.ResumeLayout(false);
			this.P_Container.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private SmartTablePanel TLP_Main;
	private SlickControls.SlickScroll slickScroll1;
	private System.Windows.Forms.Panel P_Container;
	private SlickControls.RoundedGroupPanel P_BOB;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
	private DragAndDropControl DD_BOB;
	private SlickControls.RoundedGroupPanel P_Text;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
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
	private System.Windows.Forms.Label L_Troubleshoot;
	private SlickButton B_Troubleshoot;
}
