using Skyve.App.UserInterface.Dropdowns;

namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_Options
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
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon12 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon11 = new SlickControls.DynamicIcon();
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_HelpLogs = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_CreateShortcut = new SlickControls.SlickButton();
			this.B_ChangeLog = new SlickControls.SlickButton();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.B_Reset = new SlickControls.SlickButton();
			this.B_Discord = new SlickControls.SlickButton();
			this.B_Guide = new SlickControls.SlickButton();
			this.TLP_Advanced = new SlickControls.RoundedGroupTableLayoutPanel();
			this.slickCheckbox9 = new SlickControls.SlickCheckbox();
			this.TLP_Settings = new SlickControls.RoundedGroupTableLayoutPanel();
			this.CB_AssumeInternetConnectivity = new SlickControls.SlickCheckbox();
			this.slickCheckbox10 = new SlickControls.SlickCheckbox();
			this.CB_LinkModAssets = new SlickControls.SlickCheckbox();
			this.slickCheckbox5 = new SlickControls.SlickCheckbox();
			this.slickCheckbox17 = new SlickControls.SlickCheckbox();
			this.TLP_Preferences = new SlickControls.RoundedGroupTableLayoutPanel();
			this.slickCheckbox3 = new SlickControls.SlickCheckbox();
			this.slickCheckbox1 = new SlickControls.SlickCheckbox();
			this.slickCheckbox13 = new SlickControls.SlickCheckbox();
			this.slickCheckbox8 = new SlickControls.SlickCheckbox();
			this.slickCheckbox7 = new SlickControls.SlickCheckbox();
			this.slickCheckbox2 = new SlickControls.SlickCheckbox();
			this.slickCheckbox11 = new SlickControls.SlickCheckbox();
			this.slickCheckbox14 = new SlickControls.SlickCheckbox();
			this.slickCheckbox16 = new SlickControls.SlickCheckbox();
			this.TLP_UI = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_HelpTranslate = new SlickControls.SlickButton();
			this.DD_Language = new Skyve.App.UserInterface.Dropdowns.LanguageDropDown();
			this.B_Theme = new SlickControls.SlickButton();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.slickSpacer3 = new SlickControls.SlickSpacer();
			this.TLP_Main.SuspendLayout();
			this.TLP_HelpLogs.SuspendLayout();
			this.TLP_Advanced.SuspendLayout();
			this.TLP_Settings.SuspendLayout();
			this.TLP_Preferences.SuspendLayout();
			this.TLP_UI.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(-2, 3);
			this.base_Text.Size = new System.Drawing.Size(150, 32);
			this.base_Text.Text = "Language";
			// 
			// TLP_Main
			// 
			this.TLP_Main.AutoSize = true;
			this.TLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Main.ColumnCount = 4;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.TLP_Main.Controls.Add(this.TLP_HelpLogs, 3, 0);
			this.TLP_Main.Controls.Add(this.TLP_Advanced, 2, 1);
			this.TLP_Main.Controls.Add(this.TLP_Settings, 0, 2);
			this.TLP_Main.Controls.Add(this.TLP_Preferences, 0, 0);
			this.TLP_Main.Controls.Add(this.TLP_UI, 2, 0);
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.MaximumSize = new System.Drawing.Size(1100, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 3;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.Size = new System.Drawing.Size(1100, 754);
			this.TLP_Main.TabIndex = 13;
			// 
			// TLP_HelpLogs
			// 
			this.TLP_HelpLogs.AddOutline = true;
			this.TLP_HelpLogs.AutoSize = true;
			this.TLP_HelpLogs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_HelpLogs.ColumnCount = 1;
			this.TLP_HelpLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_HelpLogs.Controls.Add(this.B_CreateShortcut, 0, 3);
			this.TLP_HelpLogs.Controls.Add(this.B_ChangeLog, 0, 2);
			this.TLP_HelpLogs.Controls.Add(this.slickSpacer2, 0, 4);
			this.TLP_HelpLogs.Controls.Add(this.B_Reset, 0, 5);
			this.TLP_HelpLogs.Controls.Add(this.B_Discord, 0, 0);
			this.TLP_HelpLogs.Controls.Add(this.B_Guide, 0, 1);
			this.TLP_HelpLogs.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon6.Name = "I_AskHelp";
			this.TLP_HelpLogs.ImageName = dynamicIcon6;
			this.TLP_HelpLogs.Location = new System.Drawing.Point(828, 3);
			this.TLP_HelpLogs.Name = "TLP_HelpLogs";
			this.TLP_HelpLogs.Padding = new System.Windows.Forms.Padding(9, 54, 9, 9);
			this.TLP_HelpLogs.RowCount = 6;
			this.TLP_HelpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_HelpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_HelpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_HelpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_HelpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_HelpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_HelpLogs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_HelpLogs.Size = new System.Drawing.Size(269, 282);
			this.TLP_HelpLogs.TabIndex = 2;
			this.TLP_HelpLogs.Text = "HelpReset";
			// 
			// B_CreateShortcut
			// 
			this.B_CreateShortcut.AutoSize = true;
			this.B_CreateShortcut.ColorShade = null;
			this.B_CreateShortcut.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_CreateShortcut.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon1.Name = "I_Link";
			this.B_CreateShortcut.ImageName = dynamicIcon1;
			this.B_CreateShortcut.Location = new System.Drawing.Point(12, 171);
			this.B_CreateShortcut.Name = "B_CreateShortcut";
			this.B_CreateShortcut.Size = new System.Drawing.Size(245, 32);
			this.B_CreateShortcut.SpaceTriggersClick = true;
			this.B_CreateShortcut.TabIndex = 19;
			this.B_CreateShortcut.Text = "CreateShortcut";
			this.B_CreateShortcut.Click += new System.EventHandler(this.B_CreateShortcut_Click);
			// 
			// B_ChangeLog
			// 
			this.B_ChangeLog.AutoSize = true;
			this.B_ChangeLog.ColorShade = null;
			this.B_ChangeLog.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_ChangeLog.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon2.Name = "I_Versions";
			this.B_ChangeLog.ImageName = dynamicIcon2;
			this.B_ChangeLog.Location = new System.Drawing.Point(12, 133);
			this.B_ChangeLog.Name = "B_ChangeLog";
			this.B_ChangeLog.Size = new System.Drawing.Size(245, 32);
			this.B_ChangeLog.SpaceTriggersClick = true;
			this.B_ChangeLog.TabIndex = 2;
			this.B_ChangeLog.Text = "OpenChangelog";
			this.B_ChangeLog.Click += new System.EventHandler(this.B_ChangeLog_Click);
			// 
			// slickSpacer2
			// 
			this.slickSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer2.Location = new System.Drawing.Point(12, 209);
			this.slickSpacer2.Name = "slickSpacer2";
			this.slickSpacer2.Size = new System.Drawing.Size(245, 23);
			this.slickSpacer2.TabIndex = 18;
			this.slickSpacer2.TabStop = false;
			this.slickSpacer2.Text = "slickSpacer2";
			// 
			// B_Reset
			// 
			this.B_Reset.AutoSize = true;
			this.B_Reset.ButtonType = SlickControls.ButtonType.Hidden;
			this.B_Reset.ColorShade = null;
			this.B_Reset.ColorStyle = Extensions.ColorStyle.Red;
			this.B_Reset.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Reset.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon3.Name = "I_Undo";
			this.B_Reset.ImageName = dynamicIcon3;
			this.B_Reset.Location = new System.Drawing.Point(12, 238);
			this.B_Reset.Name = "B_Reset";
			this.B_Reset.Size = new System.Drawing.Size(245, 32);
			this.B_Reset.SpaceTriggersClick = true;
			this.B_Reset.TabIndex = 3;
			this.B_Reset.Text = "ResetButton";
			this.B_Reset.Click += new System.EventHandler(this.B_Reset_Click);
			// 
			// B_Discord
			// 
			this.B_Discord.AutoSize = true;
			this.B_Discord.ColorShade = null;
			this.B_Discord.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Discord.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon4.Name = "I_Discord";
			this.B_Discord.ImageName = dynamicIcon4;
			this.B_Discord.Location = new System.Drawing.Point(12, 57);
			this.B_Discord.Name = "B_Discord";
			this.B_Discord.Size = new System.Drawing.Size(245, 32);
			this.B_Discord.SpaceTriggersClick = true;
			this.B_Discord.TabIndex = 0;
			this.B_Discord.Text = "JoinDiscord";
			this.B_Discord.Click += new System.EventHandler(this.B_Discord_Click);
			// 
			// B_Guide
			// 
			this.B_Guide.AutoSize = true;
			this.B_Guide.ColorShade = null;
			this.B_Guide.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Guide.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon5.Name = "I_Guide";
			this.B_Guide.ImageName = dynamicIcon5;
			this.B_Guide.Location = new System.Drawing.Point(12, 95);
			this.B_Guide.Name = "B_Guide";
			this.B_Guide.Size = new System.Drawing.Size(245, 32);
			this.B_Guide.SpaceTriggersClick = true;
			this.B_Guide.TabIndex = 1;
			this.B_Guide.Text = "OpenGuide";
			this.B_Guide.Click += new System.EventHandler(this.B_Guide_Click);
			// 
			// TLP_Advanced
			// 
			this.TLP_Advanced.AddOutline = true;
			this.TLP_Advanced.AutoSize = true;
			this.TLP_Advanced.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Advanced.ColorStyle = Extensions.ColorStyle.Red;
			this.TLP_Advanced.ColumnCount = 1;
			this.TLP_Main.SetColumnSpan(this.TLP_Advanced, 2);
			this.TLP_Advanced.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Advanced.Controls.Add(this.slickCheckbox9, 0, 0);
			this.TLP_Advanced.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon7.Name = "I_Hazard";
			this.TLP_Advanced.ImageName = dynamicIcon7;
			this.TLP_Advanced.Location = new System.Drawing.Point(553, 291);
			this.TLP_Advanced.Name = "TLP_Advanced";
			this.TLP_Advanced.Padding = new System.Windows.Forms.Padding(9, 54, 9, 9);
			this.TLP_Advanced.RowCount = 2;
			this.TLP_Main.SetRowSpan(this.TLP_Advanced, 2);
			this.TLP_Advanced.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Advanced.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Advanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Advanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Advanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Advanced.Size = new System.Drawing.Size(544, 107);
			this.TLP_Advanced.TabIndex = 4;
			this.TLP_Advanced.Text = "AdvancedSettings";
			// 
			// slickCheckbox9
			// 
			this.slickCheckbox9.AutoSize = true;
			this.slickCheckbox9.Checked = false;
			this.slickCheckbox9.CheckedText = null;
			this.slickCheckbox9.ColorStyle = Extensions.ColorStyle.Red;
			this.slickCheckbox9.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox9.DefaultValue = false;
			this.slickCheckbox9.EnterTriggersClick = false;
			this.slickCheckbox9.Location = new System.Drawing.Point(12, 57);
			this.slickCheckbox9.Name = "slickCheckbox9";
			this.slickCheckbox9.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox9.Size = new System.Drawing.Size(288, 38);
			this.slickCheckbox9.SpaceTriggersClick = true;
			this.slickCheckbox9.TabIndex = 3;
			this.slickCheckbox9.Tag = "AdvancedLaunchOptions";
			this.slickCheckbox9.Text = "AdvancedLaunchOptions";
			this.slickCheckbox9.UncheckedText = null;
			// 
			// TLP_Settings
			// 
			this.TLP_Settings.AddOutline = true;
			this.TLP_Settings.AutoSize = true;
			this.TLP_Settings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Settings.ColorStyle = Extensions.ColorStyle.Yellow;
			this.TLP_Settings.ColumnCount = 1;
			this.TLP_Main.SetColumnSpan(this.TLP_Settings, 2);
			this.TLP_Settings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Settings.Controls.Add(this.CB_AssumeInternetConnectivity, 0, 6);
			this.TLP_Settings.Controls.Add(this.slickCheckbox10, 0, 3);
			this.TLP_Settings.Controls.Add(this.CB_LinkModAssets, 0, 0);
			this.TLP_Settings.Controls.Add(this.slickCheckbox5, 0, 4);
			this.TLP_Settings.Controls.Add(this.slickCheckbox17, 0, 1);
			this.TLP_Settings.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon8.Name = "I_Cog";
			this.TLP_Settings.ImageName = dynamicIcon8;
			this.TLP_Settings.Location = new System.Drawing.Point(3, 468);
			this.TLP_Settings.Name = "TLP_Settings";
			this.TLP_Settings.Padding = new System.Windows.Forms.Padding(9, 54, 9, 9);
			this.TLP_Settings.RowCount = 7;
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Settings.Size = new System.Drawing.Size(544, 283);
			this.TLP_Settings.TabIndex = 3;
			this.TLP_Settings.Text = "Settings";
			// 
			// CB_AssumeInternetConnectivity
			// 
			this.CB_AssumeInternetConnectivity.AutoSize = true;
			this.CB_AssumeInternetConnectivity.Checked = false;
			this.CB_AssumeInternetConnectivity.CheckedText = null;
			this.CB_AssumeInternetConnectivity.ColorStyle = Extensions.ColorStyle.Yellow;
			this.CB_AssumeInternetConnectivity.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_AssumeInternetConnectivity.DefaultValue = false;
			this.CB_AssumeInternetConnectivity.EnterTriggersClick = false;
			this.CB_AssumeInternetConnectivity.Location = new System.Drawing.Point(12, 233);
			this.CB_AssumeInternetConnectivity.Name = "CB_AssumeInternetConnectivity";
			this.CB_AssumeInternetConnectivity.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_AssumeInternetConnectivity.Size = new System.Drawing.Size(318, 38);
			this.CB_AssumeInternetConnectivity.SpaceTriggersClick = true;
			this.CB_AssumeInternetConnectivity.TabIndex = 6;
			this.CB_AssumeInternetConnectivity.Tag = "AssumeInternetConnectivity";
			this.CB_AssumeInternetConnectivity.Text = "AssumeInternetConnectivity";
			this.CB_AssumeInternetConnectivity.UncheckedText = null;
			this.CB_AssumeInternetConnectivity.CheckChanged += new System.EventHandler(this.AssumeInternetConnectivity_CheckChanged);
			// 
			// slickCheckbox10
			// 
			this.slickCheckbox10.AutoSize = true;
			this.slickCheckbox10.Checked = false;
			this.slickCheckbox10.CheckedText = null;
			this.slickCheckbox10.ColorStyle = Extensions.ColorStyle.Yellow;
			this.slickCheckbox10.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox10.DefaultValue = false;
			this.slickCheckbox10.EnterTriggersClick = false;
			this.slickCheckbox10.Location = new System.Drawing.Point(12, 145);
			this.slickCheckbox10.Name = "slickCheckbox10";
			this.slickCheckbox10.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox10.Size = new System.Drawing.Size(218, 38);
			this.slickCheckbox10.SpaceTriggersClick = true;
			this.slickCheckbox10.TabIndex = 3;
			this.slickCheckbox10.Tag = "HidePseudoMods";
			this.slickCheckbox10.Text = "HidePseudoMods";
			this.slickCheckbox10.UncheckedText = null;
			// 
			// CB_LinkModAssets
			// 
			this.CB_LinkModAssets.AutoSize = true;
			this.CB_LinkModAssets.Checked = false;
			this.CB_LinkModAssets.CheckedText = null;
			this.CB_LinkModAssets.ColorStyle = Extensions.ColorStyle.Yellow;
			this.CB_LinkModAssets.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_LinkModAssets.DefaultValue = false;
			this.CB_LinkModAssets.EnterTriggersClick = false;
			this.CB_LinkModAssets.Location = new System.Drawing.Point(12, 57);
			this.CB_LinkModAssets.Name = "CB_LinkModAssets";
			this.CB_LinkModAssets.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.CB_LinkModAssets.Size = new System.Drawing.Size(193, 38);
			this.CB_LinkModAssets.SpaceTriggersClick = true;
			this.CB_LinkModAssets.TabIndex = 0;
			this.CB_LinkModAssets.Tag = "LinkModAssets";
			this.CB_LinkModAssets.Text = "LinkModAssets";
			this.CB_LinkModAssets.UncheckedText = null;
			// 
			// slickCheckbox5
			// 
			this.slickCheckbox5.AutoSize = true;
			this.slickCheckbox5.Checked = false;
			this.slickCheckbox5.CheckedText = null;
			this.slickCheckbox5.ColorStyle = Extensions.ColorStyle.Yellow;
			this.slickCheckbox5.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox5.DefaultValue = false;
			this.slickCheckbox5.EnterTriggersClick = false;
			this.slickCheckbox5.Location = new System.Drawing.Point(12, 189);
			this.slickCheckbox5.Name = "slickCheckbox5";
			this.slickCheckbox5.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox5.Size = new System.Drawing.Size(310, 38);
			this.slickCheckbox5.SpaceTriggersClick = true;
			this.slickCheckbox5.TabIndex = 4;
			this.slickCheckbox5.Tag = "DisableNewModsByDefault";
			this.slickCheckbox5.Text = "DisableNewModsByDefault";
			this.slickCheckbox5.UncheckedText = null;
			// 
			// slickCheckbox17
			// 
			this.slickCheckbox17.AutoSize = true;
			this.slickCheckbox17.Checked = false;
			this.slickCheckbox17.CheckedText = null;
			this.slickCheckbox17.ColorStyle = Extensions.ColorStyle.Yellow;
			this.slickCheckbox17.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox17.DefaultValue = false;
			this.slickCheckbox17.EnterTriggersClick = false;
			this.slickCheckbox17.Location = new System.Drawing.Point(12, 101);
			this.slickCheckbox17.Name = "slickCheckbox17";
			this.slickCheckbox17.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox17.Size = new System.Drawing.Size(291, 38);
			this.slickCheckbox17.SpaceTriggersClick = true;
			this.slickCheckbox17.TabIndex = 1;
			this.slickCheckbox17.Tag = "TreatOptionalAsRequired";
			this.slickCheckbox17.Text = "TreatOptionalAsRequired";
			this.slickCheckbox17.UncheckedText = null;
			// 
			// TLP_Preferences
			// 
			this.TLP_Preferences.AddOutline = true;
			this.TLP_Preferences.AutoSize = true;
			this.TLP_Preferences.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Preferences.ColumnCount = 1;
			this.TLP_Main.SetColumnSpan(this.TLP_Preferences, 2);
			this.TLP_Preferences.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Preferences.Controls.Add(this.slickCheckbox3, 0, 0);
			this.TLP_Preferences.Controls.Add(this.slickCheckbox1, 0, 2);
			this.TLP_Preferences.Controls.Add(this.slickCheckbox13, 0, 6);
			this.TLP_Preferences.Controls.Add(this.slickCheckbox8, 0, 8);
			this.TLP_Preferences.Controls.Add(this.slickCheckbox7, 0, 7);
			this.TLP_Preferences.Controls.Add(this.slickCheckbox2, 0, 1);
			this.TLP_Preferences.Controls.Add(this.slickCheckbox11, 0, 3);
			this.TLP_Preferences.Controls.Add(this.slickCheckbox14, 0, 5);
			this.TLP_Preferences.Controls.Add(this.slickCheckbox16, 0, 9);
			this.TLP_Preferences.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon9.Name = "I_Preferences";
			this.TLP_Preferences.ImageName = dynamicIcon9;
			this.TLP_Preferences.Location = new System.Drawing.Point(3, 3);
			this.TLP_Preferences.Name = "TLP_Preferences";
			this.TLP_Preferences.Padding = new System.Windows.Forms.Padding(9, 54, 9, 9);
			this.TLP_Preferences.RowCount = 10;
			this.TLP_Main.SetRowSpan(this.TLP_Preferences, 2);
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Preferences.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Preferences.Size = new System.Drawing.Size(544, 459);
			this.TLP_Preferences.TabIndex = 0;
			this.TLP_Preferences.Text = "Preferences";
			// 
			// slickCheckbox3
			// 
			this.slickCheckbox3.AutoSize = true;
			this.slickCheckbox3.Checked = false;
			this.slickCheckbox3.CheckedText = null;
			this.slickCheckbox3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox3.DefaultValue = false;
			this.slickCheckbox3.EnterTriggersClick = false;
			this.slickCheckbox3.Location = new System.Drawing.Point(12, 57);
			this.slickCheckbox3.Name = "slickCheckbox3";
			this.slickCheckbox3.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox3.Size = new System.Drawing.Size(207, 38);
			this.slickCheckbox3.SpaceTriggersClick = true;
			this.slickCheckbox3.TabIndex = 9;
			this.slickCheckbox3.Tag = "ExtendedListInfo";
			this.slickCheckbox3.Text = "ExtendedListInfo";
			this.slickCheckbox3.UncheckedText = null;
			// 
			// slickCheckbox1
			// 
			this.slickCheckbox1.AutoSize = true;
			this.slickCheckbox1.Checked = false;
			this.slickCheckbox1.CheckedText = null;
			this.slickCheckbox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox1.DefaultValue = false;
			this.slickCheckbox1.EnterTriggersClick = false;
			this.slickCheckbox1.Location = new System.Drawing.Point(12, 145);
			this.slickCheckbox1.Name = "slickCheckbox1";
			this.slickCheckbox1.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox1.Size = new System.Drawing.Size(209, 38);
			this.slickCheckbox1.SpaceTriggersClick = true;
			this.slickCheckbox1.TabIndex = 2;
			this.slickCheckbox1.Tag = "SnapDashToGrid";
			this.slickCheckbox1.Text = "SnapDashToGrid";
			this.slickCheckbox1.UncheckedText = null;
			// 
			// slickCheckbox13
			// 
			this.slickCheckbox13.AutoSize = true;
			this.slickCheckbox13.Checked = false;
			this.slickCheckbox13.CheckedText = null;
			this.slickCheckbox13.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox13.DefaultValue = false;
			this.slickCheckbox13.EnterTriggersClick = false;
			this.slickCheckbox13.Location = new System.Drawing.Point(12, 277);
			this.slickCheckbox13.Name = "slickCheckbox13";
			this.slickCheckbox13.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox13.Size = new System.Drawing.Size(307, 38);
			this.slickCheckbox13.SpaceTriggersClick = true;
			this.slickCheckbox13.TabIndex = 5;
			this.slickCheckbox13.Tag = "ResetScrollOnPackageClick";
			this.slickCheckbox13.Text = "ResetScrollOnPackageClick";
			this.slickCheckbox13.UncheckedText = null;
			// 
			// slickCheckbox8
			// 
			this.slickCheckbox8.AutoSize = true;
			this.slickCheckbox8.Checked = false;
			this.slickCheckbox8.CheckedText = null;
			this.slickCheckbox8.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox8.DefaultValue = false;
			this.slickCheckbox8.EnterTriggersClick = false;
			this.slickCheckbox8.Location = new System.Drawing.Point(12, 365);
			this.slickCheckbox8.Name = "slickCheckbox8";
			this.slickCheckbox8.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox8.Size = new System.Drawing.Size(321, 38);
			this.slickCheckbox8.SpaceTriggersClick = true;
			this.slickCheckbox8.TabIndex = 7;
			this.slickCheckbox8.Tag = "FilterOutPackagesWithMods";
			this.slickCheckbox8.Text = "FilterOutPackagesWithMods";
			this.slickCheckbox8.UncheckedText = null;
			// 
			// slickCheckbox7
			// 
			this.slickCheckbox7.AutoSize = true;
			this.slickCheckbox7.Checked = false;
			this.slickCheckbox7.CheckedText = null;
			this.slickCheckbox7.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox7.DefaultValue = false;
			this.slickCheckbox7.EnterTriggersClick = false;
			this.slickCheckbox7.Location = new System.Drawing.Point(12, 321);
			this.slickCheckbox7.Name = "slickCheckbox7";
			this.slickCheckbox7.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox7.Size = new System.Drawing.Size(358, 38);
			this.slickCheckbox7.SpaceTriggersClick = true;
			this.slickCheckbox7.TabIndex = 6;
			this.slickCheckbox7.Tag = "FilterOutPackagesWithOneAsset";
			this.slickCheckbox7.Text = "FilterOutPackagesWithOneAsset";
			this.slickCheckbox7.UncheckedText = null;
			// 
			// slickCheckbox2
			// 
			this.slickCheckbox2.AutoSize = true;
			this.slickCheckbox2.Checked = false;
			this.slickCheckbox2.CheckedText = null;
			this.slickCheckbox2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox2.DefaultValue = false;
			this.slickCheckbox2.EnterTriggersClick = false;
			this.slickCheckbox2.Location = new System.Drawing.Point(12, 101);
			this.slickCheckbox2.Name = "slickCheckbox2";
			this.slickCheckbox2.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox2.Size = new System.Drawing.Size(246, 38);
			this.slickCheckbox2.SpaceTriggersClick = true;
			this.slickCheckbox2.TabIndex = 1;
			this.slickCheckbox2.Tag = "ShowDatesRelatively";
			this.slickCheckbox2.Text = "ShowDatesRelatively";
			this.slickCheckbox2.UncheckedText = null;
			// 
			// slickCheckbox11
			// 
			this.slickCheckbox11.AutoSize = true;
			this.slickCheckbox11.Checked = false;
			this.slickCheckbox11.CheckedText = null;
			this.slickCheckbox11.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox11.DefaultValue = false;
			this.slickCheckbox11.EnterTriggersClick = false;
			this.slickCheckbox11.Location = new System.Drawing.Point(12, 189);
			this.slickCheckbox11.Name = "slickCheckbox11";
			this.slickCheckbox11.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox11.Size = new System.Drawing.Size(335, 38);
			this.slickCheckbox11.SpaceTriggersClick = true;
			this.slickCheckbox11.TabIndex = 3;
			this.slickCheckbox11.Tag = "AlwaysOpenFiltersAndActions";
			this.slickCheckbox11.Text = "AlwaysOpenFiltersAndActions";
			this.slickCheckbox11.UncheckedText = null;
			// 
			// slickCheckbox14
			// 
			this.slickCheckbox14.AutoSize = true;
			this.slickCheckbox14.Checked = false;
			this.slickCheckbox14.CheckedText = null;
			this.slickCheckbox14.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox14.DefaultValue = false;
			this.slickCheckbox14.EnterTriggersClick = false;
			this.slickCheckbox14.Location = new System.Drawing.Point(12, 233);
			this.slickCheckbox14.Name = "slickCheckbox14";
			this.slickCheckbox14.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox14.Size = new System.Drawing.Size(285, 38);
			this.slickCheckbox14.SpaceTriggersClick = true;
			this.slickCheckbox14.TabIndex = 4;
			this.slickCheckbox14.Tag = "FlipItemCopyFilterAction";
			this.slickCheckbox14.Text = "FlipItemCopyFilterAction";
			this.slickCheckbox14.UncheckedText = null;
			// 
			// slickCheckbox16
			// 
			this.slickCheckbox16.AutoSize = true;
			this.slickCheckbox16.Checked = false;
			this.slickCheckbox16.CheckedText = null;
			this.slickCheckbox16.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox16.DefaultValue = false;
			this.slickCheckbox16.EnterTriggersClick = false;
			this.slickCheckbox16.Location = new System.Drawing.Point(12, 409);
			this.slickCheckbox16.Name = "slickCheckbox16";
			this.slickCheckbox16.Padding = new System.Windows.Forms.Padding(7, 5, 7, 5);
			this.slickCheckbox16.Size = new System.Drawing.Size(323, 38);
			this.slickCheckbox16.SpaceTriggersClick = true;
			this.slickCheckbox16.TabIndex = 8;
			this.slickCheckbox16.Tag = "ShowAllReferencedPackages";
			this.slickCheckbox16.Text = "ShowAllReferencedPackages";
			this.slickCheckbox16.UncheckedText = null;
			// 
			// TLP_UI
			// 
			this.TLP_UI.AddOutline = true;
			this.TLP_UI.AutoSize = true;
			this.TLP_UI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_UI.ColumnCount = 1;
			this.TLP_UI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_UI.Controls.Add(this.B_HelpTranslate, 0, 1);
			this.TLP_UI.Controls.Add(this.DD_Language, 0, 0);
			this.TLP_UI.Controls.Add(this.B_Theme, 0, 3);
			this.TLP_UI.Controls.Add(this.slickSpacer1, 0, 2);
			this.TLP_UI.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon12.Name = "I_UserInterface";
			this.TLP_UI.ImageName = dynamicIcon12;
			this.TLP_UI.Location = new System.Drawing.Point(553, 3);
			this.TLP_UI.Name = "TLP_UI";
			this.TLP_UI.Padding = new System.Windows.Forms.Padding(9, 54, 9, 9);
			this.TLP_UI.RowCount = 4;
			this.TLP_UI.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_UI.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_UI.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_UI.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_UI.Size = new System.Drawing.Size(269, 259);
			this.TLP_UI.TabIndex = 1;
			this.TLP_UI.Text = "User Interface";
			// 
			// B_HelpTranslate
			// 
			this.B_HelpTranslate.AutoSize = true;
			this.B_HelpTranslate.ColorShade = null;
			this.B_HelpTranslate.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_HelpTranslate.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon10.Name = "I_Translate";
			this.B_HelpTranslate.ImageName = dynamicIcon10;
			this.B_HelpTranslate.Location = new System.Drawing.Point(12, 148);
			this.B_HelpTranslate.Name = "B_HelpTranslate";
			this.B_HelpTranslate.Size = new System.Drawing.Size(245, 32);
			this.B_HelpTranslate.SpaceTriggersClick = true;
			this.B_HelpTranslate.TabIndex = 1;
			this.B_HelpTranslate.Text = "HelpTranslate";
			this.B_HelpTranslate.Click += new System.EventHandler(this.B_HelpTranslate_Click);
			// 
			// DD_Language
			// 
			this.DD_Language.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Language.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Language.Font = new System.Drawing.Font("Segoe UI", 12.75F);
			this.DD_Language.Location = new System.Drawing.Point(16, 61);
			this.DD_Language.Margin = new System.Windows.Forms.Padding(7);
			this.DD_Language.Name = "DD_Language";
			this.DD_Language.Padding = new System.Windows.Forms.Padding(9);
			this.DD_Language.Size = new System.Drawing.Size(237, 77);
			this.DD_Language.TabIndex = 0;
			this.DD_Language.Text = "Language";
			// 
			// B_Theme
			// 
			this.B_Theme.AutoSize = true;
			this.B_Theme.ColorShade = null;
			this.B_Theme.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Theme.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon11.Name = "I_Paint";
			this.B_Theme.ImageName = dynamicIcon11;
			this.B_Theme.Location = new System.Drawing.Point(12, 215);
			this.B_Theme.Name = "B_Theme";
			this.B_Theme.Size = new System.Drawing.Size(245, 32);
			this.B_Theme.SpaceTriggersClick = true;
			this.B_Theme.TabIndex = 2;
			this.B_Theme.Text = "ThemeUIScale";
			this.B_Theme.Click += new System.EventHandler(this.B_Theme_Click);
			// 
			// slickSpacer1
			// 
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(12, 186);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(245, 23);
			this.slickSpacer1.TabIndex = 17;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.TLP_Main);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 31);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1106, 1192);
			this.panel1.TabIndex = 14;
			// 
			// slickScroll1
			// 
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.TLP_Main;
			this.slickScroll1.Location = new System.Drawing.Point(1095, 31);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(11, 1192);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 15;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.Text = "slickScroll1";
			this.slickScroll1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.slickScroll1_Scroll);
			// 
			// slickSpacer3
			// 
			this.slickSpacer3.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer3.Location = new System.Drawing.Point(0, 30);
			this.slickSpacer3.Name = "slickSpacer3";
			this.slickSpacer3.Size = new System.Drawing.Size(1106, 1);
			this.slickSpacer3.TabIndex = 16;
			this.slickSpacer3.TabStop = false;
			this.slickSpacer3.Text = "slickSpacer3";
			this.slickSpacer3.Visible = false;
			// 
			// PC_Options
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.slickScroll1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.slickSpacer3);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.LabelBounds = new System.Drawing.Point(-2, 3);
			this.Name = "PC_Options";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(1106, 1223);
			this.Text = "Language";
			this.Controls.SetChildIndex(this.slickSpacer3, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.slickScroll1, 0);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.TLP_HelpLogs.ResumeLayout(false);
			this.TLP_HelpLogs.PerformLayout();
			this.TLP_Advanced.ResumeLayout(false);
			this.TLP_Advanced.PerformLayout();
			this.TLP_Settings.ResumeLayout(false);
			this.TLP_Settings.PerformLayout();
			this.TLP_Preferences.ResumeLayout(false);
			this.TLP_Preferences.PerformLayout();
			this.TLP_UI.ResumeLayout(false);
			this.TLP_UI.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel TLP_Main;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_Preferences;
	private SlickControls.SlickCheckbox CB_LinkModAssets;
	private SlickControls.SlickCheckbox slickCheckbox2;
	private LanguageDropDown DD_Language;
	private SlickControls.SlickCheckbox slickCheckbox5;
	private SlickControls.SlickCheckbox slickCheckbox7;
	private SlickControls.SlickCheckbox slickCheckbox8;
	private SlickControls.SlickCheckbox slickCheckbox9;
	private SlickControls.SlickCheckbox slickCheckbox10;
	private System.Windows.Forms.Panel panel1;
	private SlickControls.SlickScroll slickScroll1;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_UI;
	private SlickControls.SlickButton B_Theme;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_Advanced;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_Settings;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_HelpLogs;
	private SlickControls.SlickButton B_Guide;
	private SlickControls.SlickButton B_HelpTranslate;
	private SlickControls.SlickSpacer slickSpacer1;
	private SlickControls.SlickSpacer slickSpacer2;
	private SlickControls.SlickButton B_Reset;
	private SlickControls.SlickButton B_Discord;
	private SlickControls.SlickCheckbox slickCheckbox11;
	private SlickControls.SlickButton B_ChangeLog;
	private SlickControls.SlickCheckbox slickCheckbox13;
	private SlickControls.SlickCheckbox slickCheckbox14;
	private SlickControls.SlickSpacer slickSpacer3;
	private SlickControls.SlickCheckbox slickCheckbox16;
	private SlickControls.SlickCheckbox slickCheckbox17;
	private SlickControls.SlickCheckbox CB_AssumeInternetConnectivity;
	private SlickCheckbox slickCheckbox1;
	private SlickButton B_CreateShortcut;
	private SlickCheckbox slickCheckbox3;
}
