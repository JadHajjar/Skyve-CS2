﻿using Skyve.App.UserInterface.Dropdowns;
using Skyve.App.UserInterface.Generic;

namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_PlaysetPage
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
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			this.TLP_Options = new System.Windows.Forms.TableLayoutPanel();
			this.DD_SaveFile = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.DD_NewMap = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.CB_LoadSave = new SlickControls.SlickCheckbox();
			this.CB_NoMods = new SlickControls.SlickCheckbox();
			this.CB_NoAssets = new SlickControls.SlickCheckbox();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.CB_StartNewGame = new SlickControls.SlickCheckbox();
			this.TLP_AdvancedDev = new SlickControls.RoundedGroupTableLayoutPanel();
			this.CB_UIDeveloperMode = new SlickControls.SlickCheckbox();
			this.TB_CustomArgs = new SlickControls.SlickTextBox();
			this.CB_DeveloperMode = new SlickControls.SlickCheckbox();
			this.CB_UseCitiesExe = new SlickControls.SlickCheckbox();
			this.CB_LogsToPlayerLog = new SlickControls.SlickCheckbox();
			this.DD_LogLevel = new Skyve.App.CS2.UserInterface.Content.LogLevelDropdown();
			this.CB_HideUserSection = new SlickControls.SlickCheckbox();
			this.DD_PlaysetUsage = new Skyve.App.UserInterface.Dropdowns.PackageUsageSingleDropDown();
			this.P_Side = new System.Windows.Forms.Panel();
			this.slickTabControl1 = new SlickControls.SlickTabControl();
			this.T_Content = new SlickControls.SlickTabControl.Tab();
			this.T_Settings = new SlickControls.SlickTabControl.Tab();
			this.TLP_Side = new SlickControls.RoundedTableLayoutPanel();
			this.CB_NoBanner = new SlickControls.SlickCheckbox();
			this.B_EditColor = new SlickControls.SlickButton();
			this.B_EditThumbnail = new SlickControls.SlickButton();
			this.T_LaunchSettings = new SlickControls.SlickTabControl.Tab();
			this.TLP_Options.SuspendLayout();
			this.TLP_AdvancedDev.SuspendLayout();
			this.TLP_Side.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.ButtonType = SlickControls.ButtonType.Normal;
			this.base_Text.ColorStyle = Extensions.ColorStyle.Active;
			this.base_Text.Location = new System.Drawing.Point(8, 3);
			this.base_Text.Size = new System.Drawing.Size(150, 41);
			this.base_Text.Text = "Back";
			// 
			// TLP_Options
			// 
			this.TLP_Options.AutoSize = true;
			this.TLP_Options.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Options.ColumnCount = 2;
			this.TLP_Options.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Options.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Options.Controls.Add(this.DD_SaveFile, 0, 6);
			this.TLP_Options.Controls.Add(this.DD_NewMap, 0, 4);
			this.TLP_Options.Controls.Add(this.CB_LoadSave, 0, 5);
			this.TLP_Options.Controls.Add(this.CB_NoMods, 0, 1);
			this.TLP_Options.Controls.Add(this.CB_NoAssets, 1, 1);
			this.TLP_Options.Controls.Add(this.slickSpacer1, 0, 2);
			this.TLP_Options.Controls.Add(this.CB_StartNewGame, 0, 3);
			this.TLP_Options.Controls.Add(this.TLP_AdvancedDev, 0, 7);
			this.TLP_Options.Controls.Add(this.CB_HideUserSection, 0, 0);
			this.TLP_Options.Location = new System.Drawing.Point(0, 0);
			this.TLP_Options.MaximumSize = new System.Drawing.Size(876, 0);
			this.TLP_Options.MinimumSize = new System.Drawing.Size(876, 0);
			this.TLP_Options.Name = "TLP_Options";
			this.TLP_Options.RowCount = 6;
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.Size = new System.Drawing.Size(876, 687);
			this.TLP_Options.TabIndex = 0;
			// 
			// DD_SaveFile
			// 
			this.DD_SaveFile.AllowDrop = true;
			this.TLP_Options.SetColumnSpan(this.DD_SaveFile, 2);
			this.DD_SaveFile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_SaveFile.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_SaveFile.Location = new System.Drawing.Point(3, 315);
			this.DD_SaveFile.Name = "DD_SaveFile";
			this.DD_SaveFile.Size = new System.Drawing.Size(870, 63);
			this.DD_SaveFile.TabIndex = 5;
			this.DD_SaveFile.Text = "SaveFileInfo";
			this.DD_SaveFile.ValidExtensions = new string[] {
        ".crp"};
			this.DD_SaveFile.Visible = false;
			this.DD_SaveFile.FileSelected += new System.Action<string>(this.DD_SaveFile_FileSelected);
			// 
			// DD_NewMap
			// 
			this.DD_NewMap.AllowDrop = true;
			this.TLP_Options.SetColumnSpan(this.DD_NewMap, 2);
			this.DD_NewMap.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_NewMap.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_NewMap.Location = new System.Drawing.Point(3, 196);
			this.DD_NewMap.Name = "DD_NewMap";
			this.DD_NewMap.Size = new System.Drawing.Size(870, 63);
			this.DD_NewMap.TabIndex = 3;
			this.DD_NewMap.Text = "MapFileInfo";
			this.DD_NewMap.ValidExtensions = new string[] {
        ".crp"};
			this.DD_NewMap.Visible = false;
			this.DD_NewMap.FileSelected += new System.Action<string>(this.DD_NewMap_FileSelected);
			// 
			// CB_LoadSave
			// 
			this.CB_LoadSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.CB_LoadSave.AutoSize = true;
			this.CB_LoadSave.Checked = false;
			this.CB_LoadSave.CheckedText = null;
			this.CB_LoadSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_LoadSave.DefaultValue = false;
			this.CB_LoadSave.EnterTriggersClick = false;
			this.CB_LoadSave.Location = new System.Drawing.Point(3, 265);
			this.CB_LoadSave.Name = "CB_LoadSave";
			this.CB_LoadSave.Size = new System.Drawing.Size(249, 44);
			this.CB_LoadSave.SpaceTriggersClick = true;
			this.CB_LoadSave.TabIndex = 4;
			this.CB_LoadSave.Text = "LoadSaveGame";
			this.CB_LoadSave.UncheckedText = null;
			this.CB_LoadSave.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_NoMods
			// 
			this.CB_NoMods.AutoSize = true;
			this.CB_NoMods.Checked = false;
			this.CB_NoMods.CheckedText = null;
			this.CB_NoMods.ColorStyle = Extensions.ColorStyle.Yellow;
			this.CB_NoMods.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_NoMods.DefaultValue = false;
			this.CB_NoMods.EnterTriggersClick = false;
			this.CB_NoMods.Location = new System.Drawing.Point(3, 67);
			this.CB_NoMods.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_NoMods.Name = "CB_NoMods";
			this.CB_NoMods.Size = new System.Drawing.Size(196, 44);
			this.CB_NoMods.SpaceTriggersClick = true;
			this.CB_NoMods.TabIndex = 0;
			this.CB_NoMods.Text = "NoMods";
			this.CB_NoMods.UncheckedText = null;
			this.CB_NoMods.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_NoAssets
			// 
			this.CB_NoAssets.AutoSize = true;
			this.CB_NoAssets.Checked = false;
			this.CB_NoAssets.CheckedText = null;
			this.CB_NoAssets.ColorStyle = Extensions.ColorStyle.Yellow;
			this.CB_NoAssets.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_NoAssets.DefaultValue = false;
			this.CB_NoAssets.EnterTriggersClick = false;
			this.CB_NoAssets.Location = new System.Drawing.Point(441, 67);
			this.CB_NoAssets.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_NoAssets.Name = "CB_NoAssets";
			this.CB_NoAssets.Size = new System.Drawing.Size(167, 44);
			this.CB_NoAssets.SpaceTriggersClick = true;
			this.CB_NoAssets.TabIndex = 1;
			this.CB_NoAssets.Text = "NoAssets";
			this.CB_NoAssets.UncheckedText = null;
			this.CB_NoAssets.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// slickSpacer1
			// 
			this.TLP_Options.SetColumnSpan(this.slickSpacer1, 2);
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(3, 117);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(870, 23);
			this.slickSpacer1.TabIndex = 10;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// CB_StartNewGame
			// 
			this.CB_StartNewGame.AutoSize = true;
			this.CB_StartNewGame.Checked = false;
			this.CB_StartNewGame.CheckedText = null;
			this.CB_StartNewGame.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_StartNewGame.DefaultValue = false;
			this.CB_StartNewGame.EnterTriggersClick = false;
			this.CB_StartNewGame.Location = new System.Drawing.Point(3, 146);
			this.CB_StartNewGame.Name = "CB_StartNewGame";
			this.CB_StartNewGame.Size = new System.Drawing.Size(164, 44);
			this.CB_StartNewGame.SpaceTriggersClick = true;
			this.CB_StartNewGame.TabIndex = 2;
			this.CB_StartNewGame.Text = "NewGame";
			this.CB_StartNewGame.UncheckedText = null;
			this.CB_StartNewGame.Visible = false;
			this.CB_StartNewGame.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// TLP_AdvancedDev
			// 
			this.TLP_AdvancedDev.AutoSize = true;
			this.TLP_AdvancedDev.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_AdvancedDev.ColorStyle = Extensions.ColorStyle.Red;
			this.TLP_AdvancedDev.ColumnCount = 2;
			this.TLP_Options.SetColumnSpan(this.TLP_AdvancedDev, 2);
			this.TLP_AdvancedDev.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_AdvancedDev.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_AdvancedDev.Controls.Add(this.CB_UIDeveloperMode, 1, 0);
			this.TLP_AdvancedDev.Controls.Add(this.TB_CustomArgs, 0, 4);
			this.TLP_AdvancedDev.Controls.Add(this.CB_DeveloperMode, 0, 0);
			this.TLP_AdvancedDev.Controls.Add(this.CB_UseCitiesExe, 1, 1);
			this.TLP_AdvancedDev.Controls.Add(this.CB_LogsToPlayerLog, 0, 1);
			this.TLP_AdvancedDev.Controls.Add(this.DD_LogLevel, 0, 2);
			this.TLP_AdvancedDev.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon7.Name = "I_Author";
			this.TLP_AdvancedDev.ImageName = dynamicIcon7;
			this.TLP_AdvancedDev.Location = new System.Drawing.Point(3, 384);
			this.TLP_AdvancedDev.Name = "TLP_AdvancedDev";
			this.TLP_AdvancedDev.Padding = new System.Windows.Forms.Padding(8, 46, 8, 8);
			this.TLP_AdvancedDev.RowCount = 5;
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_AdvancedDev.Size = new System.Drawing.Size(870, 270);
			this.TLP_AdvancedDev.TabIndex = 3;
			this.TLP_AdvancedDev.Text = "DevOptions";
			// 
			// CB_UIDeveloperMode
			// 
			this.CB_UIDeveloperMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.CB_UIDeveloperMode.AutoSize = true;
			this.CB_UIDeveloperMode.Checked = false;
			this.CB_UIDeveloperMode.CheckedText = null;
			this.CB_UIDeveloperMode.ColorStyle = Extensions.ColorStyle.Red;
			this.CB_UIDeveloperMode.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_UIDeveloperMode.DefaultValue = false;
			this.CB_UIDeveloperMode.EnterTriggersClick = false;
			this.CB_UIDeveloperMode.Location = new System.Drawing.Point(438, 49);
			this.CB_UIDeveloperMode.Name = "CB_UIDeveloperMode";
			this.CB_UIDeveloperMode.Size = new System.Drawing.Size(168, 44);
			this.CB_UIDeveloperMode.SpaceTriggersClick = true;
			this.CB_UIDeveloperMode.TabIndex = 5;
			this.CB_UIDeveloperMode.Text = "UIDeveloperMode";
			this.CB_UIDeveloperMode.UncheckedText = null;
			this.CB_UIDeveloperMode.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// TB_CustomArgs
			// 
			this.TB_CustomArgs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.TLP_AdvancedDev.SetColumnSpan(this.TB_CustomArgs, 2);
			this.TB_CustomArgs.LabelText = "CustomLaunchArguments";
			this.TB_CustomArgs.Location = new System.Drawing.Point(11, 201);
			this.TB_CustomArgs.Name = "TB_CustomArgs";
			this.TB_CustomArgs.Padding = new System.Windows.Forms.Padding(6, 19, 6, 6);
			this.TB_CustomArgs.Placeholder = "LaunchArgsInfo";
			this.TB_CustomArgs.SelectedText = "";
			this.TB_CustomArgs.SelectionLength = 0;
			this.TB_CustomArgs.SelectionStart = 0;
			this.TB_CustomArgs.Size = new System.Drawing.Size(848, 58);
			this.TB_CustomArgs.TabIndex = 2;
			this.TB_CustomArgs.TextChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_DeveloperMode
			// 
			this.CB_DeveloperMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.CB_DeveloperMode.AutoSize = true;
			this.CB_DeveloperMode.Checked = false;
			this.CB_DeveloperMode.CheckedText = null;
			this.CB_DeveloperMode.ColorStyle = Extensions.ColorStyle.Red;
			this.CB_DeveloperMode.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_DeveloperMode.DefaultValue = false;
			this.CB_DeveloperMode.EnterTriggersClick = false;
			this.CB_DeveloperMode.Location = new System.Drawing.Point(11, 49);
			this.CB_DeveloperMode.Name = "CB_DeveloperMode";
			this.CB_DeveloperMode.Size = new System.Drawing.Size(154, 44);
			this.CB_DeveloperMode.SpaceTriggersClick = true;
			this.CB_DeveloperMode.TabIndex = 2;
			this.CB_DeveloperMode.Text = "DeveloperMode";
			this.CB_DeveloperMode.UncheckedText = null;
			this.CB_DeveloperMode.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_UseCitiesExe
			// 
			this.CB_UseCitiesExe.AutoSize = true;
			this.CB_UseCitiesExe.Checked = false;
			this.CB_UseCitiesExe.CheckedText = null;
			this.CB_UseCitiesExe.ColorStyle = Extensions.ColorStyle.Red;
			this.CB_UseCitiesExe.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_UseCitiesExe.DefaultValue = false;
			this.CB_UseCitiesExe.EnterTriggersClick = false;
			this.CB_UseCitiesExe.Location = new System.Drawing.Point(438, 99);
			this.CB_UseCitiesExe.Name = "CB_UseCitiesExe";
			this.CB_UseCitiesExe.Size = new System.Drawing.Size(187, 44);
			this.CB_UseCitiesExe.SpaceTriggersClick = true;
			this.CB_UseCitiesExe.TabIndex = 4;
			this.CB_UseCitiesExe.Text = "LaunchThroughCities";
			this.CB_UseCitiesExe.UncheckedText = null;
			this.CB_UseCitiesExe.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_LogsToPlayerLog
			// 
			this.CB_LogsToPlayerLog.AutoSize = true;
			this.CB_LogsToPlayerLog.Checked = false;
			this.CB_LogsToPlayerLog.CheckedText = null;
			this.CB_LogsToPlayerLog.ColorStyle = Extensions.ColorStyle.Red;
			this.CB_LogsToPlayerLog.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_LogsToPlayerLog.DefaultValue = false;
			this.CB_LogsToPlayerLog.EnterTriggersClick = false;
			this.CB_LogsToPlayerLog.Location = new System.Drawing.Point(11, 99);
			this.CB_LogsToPlayerLog.Name = "CB_LogsToPlayerLog";
			this.CB_LogsToPlayerLog.Size = new System.Drawing.Size(165, 44);
			this.CB_LogsToPlayerLog.SpaceTriggersClick = true;
			this.CB_LogsToPlayerLog.TabIndex = 4;
			this.CB_LogsToPlayerLog.Text = "LogsToPlayerLog";
			this.CB_LogsToPlayerLog.UncheckedText = null;
			this.CB_LogsToPlayerLog.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// DD_LogLevel
			// 
			this.DD_LogLevel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_LogLevel.Items = new string[] {
        "DEFAULT",
        "DISABLED",
        "EMERGENCY",
        "FATAL",
        "CRITICAL",
        "ERROR",
        "WARN",
        "INFO",
        "DEBUG",
        "TRACE",
        "VERBOSE",
        "ALL"};
			this.DD_LogLevel.Location = new System.Drawing.Point(11, 149);
			this.DD_LogLevel.Name = "DD_LogLevel";
			this.DD_LogLevel.Size = new System.Drawing.Size(266, 46);
			this.DD_LogLevel.TabIndex = 6;
			this.DD_LogLevel.Text = "LogLevel";
			// 
			// CB_HideUserSection
			// 
			this.CB_HideUserSection.AutoSize = true;
			this.CB_HideUserSection.Checked = false;
			this.CB_HideUserSection.CheckedText = null;
			this.CB_HideUserSection.ColorStyle = Extensions.ColorStyle.Yellow;
			this.CB_HideUserSection.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_HideUserSection.DefaultValue = false;
			this.CB_HideUserSection.EnterTriggersClick = false;
			this.CB_HideUserSection.Location = new System.Drawing.Point(3, 10);
			this.CB_HideUserSection.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_HideUserSection.Name = "CB_HideUserSection";
			this.CB_HideUserSection.Size = new System.Drawing.Size(160, 44);
			this.CB_HideUserSection.SpaceTriggersClick = true;
			this.CB_HideUserSection.TabIndex = 0;
			this.CB_HideUserSection.Text = "HideUserSection";
			this.CB_HideUserSection.UncheckedText = null;
			this.CB_HideUserSection.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// DD_PlaysetUsage
			// 
			this.TLP_Side.SetColumnSpan(this.DD_PlaysetUsage, 2);
			this.DD_PlaysetUsage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PlaysetUsage.Location = new System.Drawing.Point(3, 3);
			this.DD_PlaysetUsage.Name = "DD_PlaysetUsage";
			this.DD_PlaysetUsage.Size = new System.Drawing.Size(325, 51);
			this.DD_PlaysetUsage.TabIndex = 2;
			this.DD_PlaysetUsage.Text = "Usage";
			this.DD_PlaysetUsage.Visible = false;
			this.DD_PlaysetUsage.SelectedItemChanged += new System.EventHandler(this.T_PlaysetUsage_SelectedValueChanged);
			// 
			// P_Side
			// 
			this.P_Side.Dock = System.Windows.Forms.DockStyle.Right;
			this.P_Side.Location = new System.Drawing.Point(883, 30);
			this.P_Side.Name = "P_Side";
			this.P_Side.Size = new System.Drawing.Size(294, 930);
			this.P_Side.TabIndex = 15;
			// 
			// slickTabControl1
			// 
			this.slickTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl1.Location = new System.Drawing.Point(5, 30);
			this.slickTabControl1.Name = "slickTabControl1";
			this.slickTabControl1.Size = new System.Drawing.Size(878, 930);
			this.slickTabControl1.TabIndex = 16;
			this.slickTabControl1.Tabs = new SlickControls.SlickTabControl.Tab[] {
        this.T_Content,
        this.T_Settings,
        this.T_LaunchSettings};
			// 
			// T_Content
			// 
			this.T_Content.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Content.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Content.FillTab = true;
			dynamicIcon8.Name = "I_Assets";
			this.T_Content.IconName = dynamicIcon8;
			this.T_Content.LinkedControl = null;
			this.T_Content.Location = new System.Drawing.Point(0, 5);
			this.T_Content.Name = "T_Content";
			this.T_Content.Size = new System.Drawing.Size(162, 78);
			this.T_Content.TabIndex = 1;
			this.T_Content.TabStop = false;
			this.T_Content.Text = "Content";
			// 
			// T_Settings
			// 
			this.T_Settings.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Settings.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon4.Name = "I_PlaysetSettings";
			this.T_Settings.IconName = dynamicIcon4;
			this.T_Settings.LinkedControl = this.TLP_Side;
			this.T_Settings.Location = new System.Drawing.Point(162, 5);
			this.T_Settings.Name = "T_Settings";
			this.T_Settings.Size = new System.Drawing.Size(162, 78);
			this.T_Settings.TabIndex = 1;
			this.T_Settings.TabStop = false;
			this.T_Settings.Text = "Settings";
			// 
			// TLP_Side
			// 
			this.TLP_Side.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Side.ColumnCount = 2;
			this.TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Side.Controls.Add(this.CB_NoBanner, 0, 2);
			this.TLP_Side.Controls.Add(this.DD_PlaysetUsage, 0, 0);
			this.TLP_Side.Controls.Add(this.B_EditColor, 0, 1);
			this.TLP_Side.Controls.Add(this.B_EditThumbnail, 0, 2);
			this.TLP_Side.Location = new System.Drawing.Point(0, 0);
			this.TLP_Side.Name = "TLP_Side";
			this.TLP_Side.RowCount = 3;
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.Size = new System.Drawing.Size(878, 349);
			this.TLP_Side.TabIndex = 1;
			// 
			// CB_NoBanner
			// 
			this.CB_NoBanner.AutoSize = true;
			this.CB_NoBanner.Checked = false;
			this.CB_NoBanner.CheckedText = null;
			this.CB_NoBanner.ColorStyle = Extensions.ColorStyle.Yellow;
			this.CB_NoBanner.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_NoBanner.DefaultValue = false;
			this.CB_NoBanner.EnterTriggersClick = false;
			this.CB_NoBanner.Location = new System.Drawing.Point(442, 103);
			this.CB_NoBanner.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_NoBanner.Name = "CB_NoBanner";
			this.CB_NoBanner.Size = new System.Drawing.Size(124, 46);
			this.CB_NoBanner.SpaceTriggersClick = true;
			this.CB_NoBanner.TabIndex = 27;
			this.CB_NoBanner.Text = "NoBanner";
			this.CB_NoBanner.UncheckedText = null;
			this.CB_NoBanner.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// B_EditColor
			// 
			this.B_EditColor.AutoSize = true;
			this.B_EditColor.ColorStyle = Extensions.ColorStyle.Green;
			this.B_EditColor.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "I_Paint";
			this.B_EditColor.ImageName = dynamicIcon5;
			this.B_EditColor.Location = new System.Drawing.Point(3, 60);
			this.B_EditColor.MatchBackgroundColor = true;
			this.B_EditColor.Name = "B_EditColor";
			this.B_EditColor.Size = new System.Drawing.Size(171, 30);
			this.B_EditColor.SpaceTriggersClick = true;
			this.B_EditColor.TabIndex = 26;
			this.B_EditColor.Text = "ChangePlaysetColor";
			this.B_EditColor.Click += new System.EventHandler(this.B_EditColor_Click);
			// 
			// B_EditThumbnail
			// 
			this.B_EditThumbnail.AutoSize = true;
			this.B_EditThumbnail.ColorStyle = Extensions.ColorStyle.Green;
			this.B_EditThumbnail.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon9.Name = "I_EditImage";
			this.B_EditThumbnail.ImageName = dynamicIcon9;
			this.B_EditThumbnail.Location = new System.Drawing.Point(3, 96);
			this.B_EditThumbnail.MatchBackgroundColor = true;
			this.B_EditThumbnail.Name = "B_EditThumbnail";
			this.B_EditThumbnail.Size = new System.Drawing.Size(178, 30);
			this.B_EditThumbnail.SpaceTriggersClick = true;
			this.B_EditThumbnail.TabIndex = 26;
			this.B_EditThumbnail.Text = "EditPlaysetThumbnail";
			this.B_EditThumbnail.Click += new System.EventHandler(this.B_EditThumbnail_Click);
			// 
			// T_LaunchSettings
			// 
			this.T_LaunchSettings.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_LaunchSettings.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon10.Name = "I_Launch";
			this.T_LaunchSettings.IconName = dynamicIcon10;
			this.T_LaunchSettings.LinkedControl = this.TLP_Options;
			this.T_LaunchSettings.Location = new System.Drawing.Point(324, 5);
			this.T_LaunchSettings.Name = "T_LaunchSettings";
			this.T_LaunchSettings.Size = new System.Drawing.Size(162, 78);
			this.T_LaunchSettings.TabIndex = 0;
			this.T_LaunchSettings.TabStop = false;
			this.T_LaunchSettings.Text = "Launch Settings";
			// 
			// PC_PlaysetPage
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.slickTabControl1);
			this.Controls.Add(this.P_Side);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_PlaysetPage";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 5, 0);
			this.Size = new System.Drawing.Size(1182, 960);
			this.Text = "Back";
			this.Controls.SetChildIndex(this.P_Side, 0);
			this.Controls.SetChildIndex(this.slickTabControl1, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.TLP_Options.ResumeLayout(false);
			this.TLP_Options.PerformLayout();
			this.TLP_AdvancedDev.ResumeLayout(false);
			this.TLP_AdvancedDev.PerformLayout();
			this.TLP_Side.ResumeLayout(false);
			this.TLP_Side.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel TLP_Options;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_AdvancedDev;
	private SlickControls.SlickCheckbox CB_UseCitiesExe;
	private SlickControls.SlickCheckbox CB_DeveloperMode;
	private SlickControls.SlickTextBox TB_CustomArgs;
	private PackageUsageSingleDropDown DD_PlaysetUsage;
	private SlickCheckbox CB_NoMods;
	private DragAndDropControl DD_NewMap;
	private SlickCheckbox CB_NoAssets;
	private DragAndDropControl DD_SaveFile;
	private SlickSpacer slickSpacer1;
	private SlickCheckbox CB_LoadSave;
	private SlickCheckbox CB_StartNewGame;
	private SlickCheckbox CB_UIDeveloperMode;
	private Content.LogLevelDropdown DD_LogLevel;
	private SlickCheckbox CB_HideUserSection;
	private System.Windows.Forms.Panel P_Side;
	private SlickTabControl slickTabControl1;
	private SlickTabControl.Tab T_Content;
	private SlickTabControl.Tab T_LaunchSettings;
	private SlickTabControl.Tab T_Settings;
	private RoundedTableLayoutPanel TLP_Side;
	private SlickButton B_EditColor;
	private SlickButton B_EditThumbnail;
	private SlickCheckbox CB_LogsToPlayerLog;
	private SlickCheckbox CB_NoBanner;
}