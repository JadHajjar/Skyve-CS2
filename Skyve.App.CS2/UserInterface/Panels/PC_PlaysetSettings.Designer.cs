using Skyve.App.UserInterface.Dropdowns;
using Skyve.App.UserInterface.Generic;

namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_PlaysetSettings
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
			_notifier.PlaysetChanged -= ProfileManager_ProfileChanged;
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
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			this.I_Favorite = new SlickControls.SlickIcon();
			this.TB_Name = new SlickControls.SlickTextBox();
			this.P_ScrollPanel = new System.Windows.Forms.Panel();
			this.slickScroll = new SlickControls.SlickScroll();
			this.TLP_Options = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_LaunchSettings = new SlickControls.RoundedGroupTableLayoutPanel();
			this.CB_NoMods = new SlickControls.SlickCheckbox();
			this.DD_NewMap = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.CB_NoAssets = new SlickControls.SlickCheckbox();
			this.DD_SaveFile = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.CB_LoadSave = new SlickControls.SlickCheckbox();
			this.CB_StartNewGame = new SlickControls.SlickCheckbox();
			this.slickCheckbox2 = new SlickControls.SlickCheckbox();
			this.TLP_AdvancedDev = new SlickControls.RoundedGroupTableLayoutPanel();
			this.slickCheckbox1 = new SlickControls.SlickCheckbox();
			this.TB_CustomArgs = new SlickControls.SlickTextBox();
			this.CB_DevUI = new SlickControls.SlickCheckbox();
			this.CB_UseCitiesExe = new SlickControls.SlickCheckbox();
			this.logLevelDropdown1 = new Skyve.App.CS2.UserInterface.Content.LogLevelDropdown();
			this.DD_PlaysetUsage = new Skyve.App.UserInterface.Dropdowns.PackageUsageSingleDropDown();
			this.P_Side = new System.Windows.Forms.Panel();
			this.roundedPanel1 = new SlickControls.RoundedPanel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.TLP_Side = new System.Windows.Forms.TableLayoutPanel();
			this.P_Name = new System.Windows.Forms.Panel();
			this.L_CurrentPlayset = new SlickControls.AutoSizeLabel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.I_More = new SlickControls.SlickIcon();
			this.PB_Icon = new Skyve.App.UserInterface.Content.PlaysetIcon();
			this.LI_ModCount = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_ModSize = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.B_Activate = new SlickControls.SlickButton();
			this.B_EditColor = new SlickControls.SlickButton();
			this.B_EditThumbnail = new SlickControls.SlickButton();
			this.P_ScrollPanel.SuspendLayout();
			this.TLP_Options.SuspendLayout();
			this.TLP_LaunchSettings.SuspendLayout();
			this.TLP_AdvancedDev.SuspendLayout();
			this.P_Side.SuspendLayout();
			this.roundedPanel1.SuspendLayout();
			this.TLP_Side.SuspendLayout();
			this.P_Name.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(150, 33);
			// 
			// I_Favorite
			// 
			this.I_Favorite.ActiveColor = null;
			this.I_Favorite.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_Favorite.Location = new System.Drawing.Point(0, 0);
			this.I_Favorite.Margin = new System.Windows.Forms.Padding(0);
			this.I_Favorite.Name = "I_Favorite";
			this.I_Favorite.Padding = new System.Windows.Forms.Padding(5);
			this.I_Favorite.Size = new System.Drawing.Size(20, 32);
			this.I_Favorite.TabIndex = 0;
			this.I_Favorite.TabStop = false;
			this.I_Favorite.Click += new System.EventHandler(this.I_Favorite_Click);
			// 
			// TB_Name
			// 
			this.TB_Name.Dock = System.Windows.Forms.DockStyle.Fill;
			dynamicIcon1.Name = "I_Ok";
			this.TB_Name.ImageName = dynamicIcon1;
			this.TB_Name.LabelText = "";
			this.TB_Name.Location = new System.Drawing.Point(0, 0);
			this.TB_Name.Name = "TB_Name";
			this.TB_Name.Padding = new System.Windows.Forms.Padding(6, 6, 66, 6);
			this.TB_Name.Placeholder = "RenamePlayset";
			this.TB_Name.SelectedText = "";
			this.TB_Name.SelectionLength = 0;
			this.TB_Name.SelectionStart = 0;
			this.TB_Name.ShowLabel = false;
			this.TB_Name.Size = new System.Drawing.Size(250, 39);
			this.TB_Name.TabIndex = 2;
			this.TB_Name.Visible = false;
			this.TB_Name.IconClicked += new System.EventHandler(this.TB_Name_IconClicked);
			this.TB_Name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Name_KeyDown);
			this.TB_Name.Leave += new System.EventHandler(this.TB_Name_Leave);
			this.TB_Name.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.TB_Name_PreviewKeyDown);
			// 
			// P_ScrollPanel
			// 
			this.P_ScrollPanel.Controls.Add(this.slickScroll);
			this.P_ScrollPanel.Controls.Add(this.TLP_Options);
			this.P_ScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_ScrollPanel.Location = new System.Drawing.Point(0, 30);
			this.P_ScrollPanel.Margin = new System.Windows.Forms.Padding(5);
			this.P_ScrollPanel.Name = "P_ScrollPanel";
			this.P_ScrollPanel.Size = new System.Drawing.Size(883, 930);
			this.P_ScrollPanel.TabIndex = 0;
			// 
			// slickScroll
			// 
			this.slickScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll.LinkedControl = this.TLP_Options;
			this.slickScroll.Location = new System.Drawing.Point(875, 0);
			this.slickScroll.Name = "slickScroll";
			this.slickScroll.Size = new System.Drawing.Size(8, 930);
			this.slickScroll.Style = SlickControls.StyleType.Vertical;
			this.slickScroll.TabIndex = 16;
			this.slickScroll.TabStop = false;
			this.slickScroll.Text = "slickScroll1";
			// 
			// TLP_Options
			// 
			this.TLP_Options.AutoSize = true;
			this.TLP_Options.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Options.ColumnCount = 1;
			this.TLP_Options.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Options.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Options.Controls.Add(this.TLP_LaunchSettings, 0, 0);
			this.TLP_Options.Controls.Add(this.TLP_AdvancedDev, 0, 2);
			this.TLP_Options.Location = new System.Drawing.Point(0, 0);
			this.TLP_Options.MaximumSize = new System.Drawing.Size(876, 0);
			this.TLP_Options.MinimumSize = new System.Drawing.Size(876, 0);
			this.TLP_Options.Name = "TLP_Options";
			this.TLP_Options.RowCount = 1;
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Options.Size = new System.Drawing.Size(876, 661);
			this.TLP_Options.TabIndex = 0;
			// 
			// TLP_LaunchSettings
			// 
			this.TLP_LaunchSettings.AddOutline = true;
			this.TLP_LaunchSettings.AutoSize = true;
			this.TLP_LaunchSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_LaunchSettings.ColumnCount = 2;
			this.TLP_LaunchSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_LaunchSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_LaunchSettings.Controls.Add(this.CB_NoMods, 0, 1);
			this.TLP_LaunchSettings.Controls.Add(this.DD_NewMap, 0, 4);
			this.TLP_LaunchSettings.Controls.Add(this.CB_NoAssets, 1, 1);
			this.TLP_LaunchSettings.Controls.Add(this.DD_SaveFile, 0, 6);
			this.TLP_LaunchSettings.Controls.Add(this.slickSpacer1, 0, 2);
			this.TLP_LaunchSettings.Controls.Add(this.CB_LoadSave, 0, 5);
			this.TLP_LaunchSettings.Controls.Add(this.CB_StartNewGame, 0, 3);
			this.TLP_LaunchSettings.Controls.Add(this.slickCheckbox2, 0, 0);
			this.TLP_LaunchSettings.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon2.Name = "I_Launch";
			this.TLP_LaunchSettings.ImageName = dynamicIcon2;
			this.TLP_LaunchSettings.Location = new System.Drawing.Point(3, 3);
			this.TLP_LaunchSettings.Name = "TLP_LaunchSettings";
			this.TLP_LaunchSettings.Padding = new System.Windows.Forms.Padding(8, 46, 8, 8);
			this.TLP_LaunchSettings.RowCount = 8;
			this.TLP_Options.SetRowSpan(this.TLP_LaunchSettings, 2);
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LaunchSettings.Size = new System.Drawing.Size(870, 435);
			this.TLP_LaunchSettings.TabIndex = 1;
			this.TLP_LaunchSettings.Text = "LaunchSettings";
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
			this.CB_NoMods.Location = new System.Drawing.Point(11, 113);
			this.CB_NoMods.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_NoMods.Name = "CB_NoMods";
			this.CB_NoMods.Size = new System.Drawing.Size(103, 44);
			this.CB_NoMods.SpaceTriggersClick = true;
			this.CB_NoMods.TabIndex = 0;
			this.CB_NoMods.Text = "NoMods";
			this.CB_NoMods.UncheckedText = null;
			this.CB_NoMods.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// DD_NewMap
			// 
			this.DD_NewMap.AllowDrop = true;
			this.TLP_LaunchSettings.SetColumnSpan(this.DD_NewMap, 2);
			this.DD_NewMap.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_NewMap.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_NewMap.Location = new System.Drawing.Point(11, 242);
			this.DD_NewMap.Name = "DD_NewMap";
			this.DD_NewMap.Size = new System.Drawing.Size(848, 63);
			this.DD_NewMap.TabIndex = 3;
			this.DD_NewMap.Text = "MapFileInfo";
			this.DD_NewMap.ValidExtensions = new string[] {
        ".crp"};
			this.DD_NewMap.Visible = false;
			this.DD_NewMap.FileSelected += new System.Action<string>(this.DD_NewMap_FileSelected);
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
			this.CB_NoAssets.Location = new System.Drawing.Point(438, 113);
			this.CB_NoAssets.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.CB_NoAssets.Name = "CB_NoAssets";
			this.CB_NoAssets.Size = new System.Drawing.Size(106, 44);
			this.CB_NoAssets.SpaceTriggersClick = true;
			this.CB_NoAssets.TabIndex = 1;
			this.CB_NoAssets.Text = "NoAssets";
			this.CB_NoAssets.UncheckedText = null;
			this.CB_NoAssets.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// DD_SaveFile
			// 
			this.DD_SaveFile.AllowDrop = true;
			this.TLP_LaunchSettings.SetColumnSpan(this.DD_SaveFile, 2);
			this.DD_SaveFile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_SaveFile.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_SaveFile.Location = new System.Drawing.Point(11, 361);
			this.DD_SaveFile.Name = "DD_SaveFile";
			this.DD_SaveFile.Size = new System.Drawing.Size(848, 63);
			this.DD_SaveFile.TabIndex = 5;
			this.DD_SaveFile.Text = "SaveFileInfo";
			this.DD_SaveFile.ValidExtensions = new string[] {
        ".crp"};
			this.DD_SaveFile.Visible = false;
			this.DD_SaveFile.FileSelected += new System.Action<string>(this.DD_SaveFile_FileSelected);
			// 
			// slickSpacer1
			// 
			this.TLP_LaunchSettings.SetColumnSpan(this.slickSpacer1, 2);
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(11, 163);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(848, 23);
			this.slickSpacer1.TabIndex = 10;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// CB_LoadSave
			// 
			this.CB_LoadSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.CB_LoadSave.AutoSize = true;
			this.CB_LoadSave.Checked = false;
			this.CB_LoadSave.CheckedText = null;
			this.TLP_LaunchSettings.SetColumnSpan(this.CB_LoadSave, 2);
			this.CB_LoadSave.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_LoadSave.DefaultValue = false;
			this.CB_LoadSave.EnterTriggersClick = false;
			this.CB_LoadSave.Location = new System.Drawing.Point(11, 311);
			this.CB_LoadSave.Name = "CB_LoadSave";
			this.CB_LoadSave.Size = new System.Drawing.Size(136, 44);
			this.CB_LoadSave.SpaceTriggersClick = true;
			this.CB_LoadSave.TabIndex = 4;
			this.CB_LoadSave.Text = "LoadSaveGame";
			this.CB_LoadSave.UncheckedText = null;
			this.CB_LoadSave.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_StartNewGame
			// 
			this.CB_StartNewGame.AutoSize = true;
			this.CB_StartNewGame.Checked = false;
			this.CB_StartNewGame.CheckedText = null;
			this.TLP_LaunchSettings.SetColumnSpan(this.CB_StartNewGame, 2);
			this.CB_StartNewGame.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_StartNewGame.DefaultValue = false;
			this.CB_StartNewGame.EnterTriggersClick = false;
			this.CB_StartNewGame.Location = new System.Drawing.Point(11, 192);
			this.CB_StartNewGame.Name = "CB_StartNewGame";
			this.CB_StartNewGame.Size = new System.Drawing.Size(111, 44);
			this.CB_StartNewGame.SpaceTriggersClick = true;
			this.CB_StartNewGame.TabIndex = 2;
			this.CB_StartNewGame.Text = "NewGame";
			this.CB_StartNewGame.UncheckedText = null;
			this.CB_StartNewGame.Visible = false;
			this.CB_StartNewGame.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// slickCheckbox2
			// 
			this.slickCheckbox2.AutoSize = true;
			this.slickCheckbox2.Checked = false;
			this.slickCheckbox2.CheckedText = null;
			this.slickCheckbox2.ColorStyle = Extensions.ColorStyle.Yellow;
			this.slickCheckbox2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox2.DefaultValue = false;
			this.slickCheckbox2.EnterTriggersClick = false;
			this.slickCheckbox2.Location = new System.Drawing.Point(11, 56);
			this.slickCheckbox2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.slickCheckbox2.Name = "slickCheckbox2";
			this.slickCheckbox2.Size = new System.Drawing.Size(142, 44);
			this.slickCheckbox2.SpaceTriggersClick = true;
			this.slickCheckbox2.TabIndex = 0;
			this.slickCheckbox2.Text = "HideUserSection";
			this.slickCheckbox2.UncheckedText = null;
			this.slickCheckbox2.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// TLP_AdvancedDev
			// 
			this.TLP_AdvancedDev.AddOutline = true;
			this.TLP_AdvancedDev.AutoSize = true;
			this.TLP_AdvancedDev.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_AdvancedDev.ColorStyle = Extensions.ColorStyle.Red;
			this.TLP_AdvancedDev.ColumnCount = 2;
			this.TLP_AdvancedDev.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_AdvancedDev.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_AdvancedDev.Controls.Add(this.slickCheckbox1, 1, 0);
			this.TLP_AdvancedDev.Controls.Add(this.TB_CustomArgs, 0, 4);
			this.TLP_AdvancedDev.Controls.Add(this.CB_DevUI, 0, 0);
			this.TLP_AdvancedDev.Controls.Add(this.CB_UseCitiesExe, 0, 1);
			this.TLP_AdvancedDev.Controls.Add(this.logLevelDropdown1, 1, 1);
			this.TLP_AdvancedDev.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon3.Name = "I_Author";
			this.TLP_AdvancedDev.ImageName = dynamicIcon3;
			this.TLP_AdvancedDev.Location = new System.Drawing.Point(3, 444);
			this.TLP_AdvancedDev.Name = "TLP_AdvancedDev";
			this.TLP_AdvancedDev.Padding = new System.Windows.Forms.Padding(8, 46, 8, 8);
			this.TLP_AdvancedDev.RowCount = 5;
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_AdvancedDev.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_AdvancedDev.Size = new System.Drawing.Size(870, 214);
			this.TLP_AdvancedDev.TabIndex = 3;
			this.TLP_AdvancedDev.Text = "DevOptions";
			// 
			// slickCheckbox1
			// 
			this.slickCheckbox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.slickCheckbox1.AutoSize = true;
			this.slickCheckbox1.Checked = false;
			this.slickCheckbox1.CheckedText = null;
			this.slickCheckbox1.ColorStyle = Extensions.ColorStyle.Red;
			this.slickCheckbox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox1.DefaultValue = false;
			this.slickCheckbox1.EnterTriggersClick = false;
			this.slickCheckbox1.Location = new System.Drawing.Point(438, 49);
			this.slickCheckbox1.Name = "slickCheckbox1";
			this.slickCheckbox1.Size = new System.Drawing.Size(122, 44);
			this.slickCheckbox1.SpaceTriggersClick = true;
			this.slickCheckbox1.TabIndex = 5;
			this.slickCheckbox1.Text = "EnableDevUi";
			this.slickCheckbox1.UncheckedText = null;
			// 
			// TB_CustomArgs
			// 
			this.TB_CustomArgs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.TLP_AdvancedDev.SetColumnSpan(this.TB_CustomArgs, 2);
			this.TB_CustomArgs.LabelText = "CustomLaunchArguments";
			this.TB_CustomArgs.Location = new System.Drawing.Point(11, 151);
			this.TB_CustomArgs.Name = "TB_CustomArgs";
			this.TB_CustomArgs.Padding = new System.Windows.Forms.Padding(6, 19, 6, 6);
			this.TB_CustomArgs.Placeholder = "LaunchArgsInfo";
			this.TB_CustomArgs.SelectedText = "";
			this.TB_CustomArgs.SelectionLength = 0;
			this.TB_CustomArgs.SelectionStart = 0;
			this.TB_CustomArgs.Size = new System.Drawing.Size(848, 52);
			this.TB_CustomArgs.TabIndex = 2;
			this.TB_CustomArgs.TextChanged += new System.EventHandler(this.ValueChanged);
			// 
			// CB_DevUI
			// 
			this.CB_DevUI.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.CB_DevUI.AutoSize = true;
			this.CB_DevUI.Checked = false;
			this.CB_DevUI.CheckedText = null;
			this.CB_DevUI.ColorStyle = Extensions.ColorStyle.Red;
			this.CB_DevUI.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_DevUI.DefaultValue = false;
			this.CB_DevUI.EnterTriggersClick = false;
			this.CB_DevUI.Location = new System.Drawing.Point(11, 49);
			this.CB_DevUI.Name = "CB_DevUI";
			this.CB_DevUI.Size = new System.Drawing.Size(122, 44);
			this.CB_DevUI.SpaceTriggersClick = true;
			this.CB_DevUI.TabIndex = 2;
			this.CB_DevUI.Text = "EnableDevUi";
			this.CB_DevUI.UncheckedText = null;
			this.CB_DevUI.CheckChanged += new System.EventHandler(this.ValueChanged);
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
			this.CB_UseCitiesExe.Location = new System.Drawing.Point(11, 99);
			this.CB_UseCitiesExe.Name = "CB_UseCitiesExe";
			this.CB_UseCitiesExe.Size = new System.Drawing.Size(164, 44);
			this.CB_UseCitiesExe.SpaceTriggersClick = true;
			this.CB_UseCitiesExe.TabIndex = 4;
			this.CB_UseCitiesExe.Text = "LaunchThroughCities";
			this.CB_UseCitiesExe.UncheckedText = null;
			this.CB_UseCitiesExe.CheckChanged += new System.EventHandler(this.ValueChanged);
			// 
			// logLevelDropdown1
			// 
			this.logLevelDropdown1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.logLevelDropdown1.Items = new string[] {
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
			this.logLevelDropdown1.Location = new System.Drawing.Point(438, 99);
			this.logLevelDropdown1.Name = "logLevelDropdown1";
			this.logLevelDropdown1.Size = new System.Drawing.Size(266, 46);
			this.logLevelDropdown1.TabIndex = 6;
			this.logLevelDropdown1.Text = "LogLevel";
			// 
			// DD_PlaysetUsage
			// 
			this.TLP_Side.SetColumnSpan(this.DD_PlaysetUsage, 3);
			this.DD_PlaysetUsage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PlaysetUsage.Location = new System.Drawing.Point(3, 261);
			this.DD_PlaysetUsage.Name = "DD_PlaysetUsage";
			this.DD_PlaysetUsage.Size = new System.Drawing.Size(244, 51);
			this.DD_PlaysetUsage.TabIndex = 2;
			this.DD_PlaysetUsage.Text = "Usage";
			this.DD_PlaysetUsage.Visible = false;
			this.DD_PlaysetUsage.SelectedItemChanged += new System.EventHandler(this.T_PlaysetUsage_SelectedValueChanged);
			// 
			// P_Side
			// 
			this.P_Side.Controls.Add(this.roundedPanel1);
			this.P_Side.Dock = System.Windows.Forms.DockStyle.Right;
			this.P_Side.Location = new System.Drawing.Point(883, 30);
			this.P_Side.Name = "P_Side";
			this.P_Side.Size = new System.Drawing.Size(294, 930);
			this.P_Side.TabIndex = 15;
			// 
			// roundedPanel1
			// 
			this.roundedPanel1.AutoSize = true;
			this.roundedPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedPanel1.Controls.Add(this.slickScroll1);
			this.roundedPanel1.Controls.Add(this.TLP_Side);
			this.roundedPanel1.Location = new System.Drawing.Point(3, 3);
			this.roundedPanel1.MinimumSize = new System.Drawing.Size(250, 0);
			this.roundedPanel1.Name = "roundedPanel1";
			this.roundedPanel1.Size = new System.Drawing.Size(250, 349);
			this.roundedPanel1.TabIndex = 0;
			// 
			// slickScroll1
			// 
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.roundedPanel1;
			this.slickScroll1.Location = new System.Drawing.Point(242, 349);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(8, 0);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 3;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.Text = "slickScroll1";
			// 
			// TLP_Side
			// 
			this.TLP_Side.AutoSize = true;
			this.TLP_Side.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Side.ColumnCount = 2;
			this.TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Side.Controls.Add(this.P_Name, 0, 1);
			this.TLP_Side.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.TLP_Side.Controls.Add(this.DD_PlaysetUsage, 0, 5);
			this.TLP_Side.Controls.Add(this.LI_ModCount, 0, 4);
			this.TLP_Side.Controls.Add(this.LI_ModSize, 1, 4);
			this.TLP_Side.Controls.Add(this.slickSpacer2, 0, 2);
			this.TLP_Side.Controls.Add(this.B_Activate, 0, 3);
			this.TLP_Side.Controls.Add(this.B_EditColor, 0, 6);
			this.TLP_Side.Controls.Add(this.B_EditThumbnail, 1, 6);
			this.TLP_Side.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Side.Location = new System.Drawing.Point(0, 0);
			this.TLP_Side.Name = "TLP_Side";
			this.TLP_Side.RowCount = 7;
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.Size = new System.Drawing.Size(250, 349);
			this.TLP_Side.TabIndex = 1;
			// 
			// P_Name
			// 
			this.TLP_Side.SetColumnSpan(this.P_Name, 2);
			this.P_Name.Controls.Add(this.TB_Name);
			this.P_Name.Controls.Add(this.L_CurrentPlayset);
			this.P_Name.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Name.Location = new System.Drawing.Point(0, 100);
			this.P_Name.Margin = new System.Windows.Forms.Padding(0);
			this.P_Name.Name = "P_Name";
			this.P_Name.Size = new System.Drawing.Size(250, 42);
			this.P_Name.TabIndex = 16;
			// 
			// L_CurrentPlayset
			// 
			this.L_CurrentPlayset.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.L_CurrentPlayset.Dock = System.Windows.Forms.DockStyle.Fill;
			this.L_CurrentPlayset.Location = new System.Drawing.Point(0, 0);
			this.L_CurrentPlayset.Name = "L_CurrentPlayset";
			this.L_CurrentPlayset.Size = new System.Drawing.Size(250, 42);
			this.L_CurrentPlayset.TabIndex = 3;
			this.L_CurrentPlayset.VerticalAlignment = System.Drawing.StringAlignment.Center;
			this.L_CurrentPlayset.Click += new System.EventHandler(this.B_EditName_Click);
			this.L_CurrentPlayset.Paint += new System.Windows.Forms.PaintEventHandler(this.L_CurrentPlayset_Paint);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 3;
			this.TLP_Side.SetColumnSpan(this.tableLayoutPanel2, 2);
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.I_More, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.PB_Icon, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.I_Favorite, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(250, 100);
			this.tableLayoutPanel2.TabIndex = 27;
			this.tableLayoutPanel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PB_Icon_MouseClick);
			// 
			// I_More
			// 
			this.I_More.ActiveColor = null;
			this.I_More.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "I_VertialMore";
			this.I_More.ImageName = dynamicIcon4;
			this.I_More.Location = new System.Drawing.Point(236, 0);
			this.I_More.Margin = new System.Windows.Forms.Padding(0);
			this.I_More.Name = "I_More";
			this.I_More.Size = new System.Drawing.Size(14, 28);
			this.I_More.TabIndex = 3;
			this.I_More.Click += new System.EventHandler(this.I_More_Click);
			// 
			// PB_Icon
			// 
			this.PB_Icon.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.PB_Icon.Location = new System.Drawing.Point(78, 0);
			this.PB_Icon.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Icon.Name = "PB_Icon";
			this.PB_Icon.Size = new System.Drawing.Size(100, 100);
			this.PB_Icon.TabIndex = 21;
			this.PB_Icon.TabStop = false;
			this.PB_Icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PB_Icon_MouseClick);
			// 
			// LI_ModCount
			// 
			this.LI_ModCount.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_ModCount.LabelText = "ModCount";
			this.LI_ModCount.Location = new System.Drawing.Point(3, 199);
			this.LI_ModCount.Name = "LI_ModCount";
			this.LI_ModCount.Padding = new System.Windows.Forms.Padding(5);
			this.LI_ModCount.Size = new System.Drawing.Size(119, 14);
			this.LI_ModCount.TabIndex = 24;
			// 
			// LI_ModSize
			// 
			this.LI_ModSize.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_ModSize.LabelText = "ModSizes";
			this.LI_ModSize.Location = new System.Drawing.Point(128, 199);
			this.LI_ModSize.Name = "LI_ModSize";
			this.LI_ModSize.Padding = new System.Windows.Forms.Padding(4);
			this.LI_ModSize.Size = new System.Drawing.Size(119, 56);
			this.LI_ModSize.TabIndex = 23;
			this.LI_ModSize.ValueText = "";
			// 
			// slickSpacer2
			// 
			this.TLP_Side.SetColumnSpan(this.slickSpacer2, 2);
			this.slickSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer2.Location = new System.Drawing.Point(3, 145);
			this.slickSpacer2.Name = "slickSpacer2";
			this.slickSpacer2.Size = new System.Drawing.Size(244, 14);
			this.slickSpacer2.TabIndex = 1;
			this.slickSpacer2.TabStop = false;
			this.slickSpacer2.Text = "slickSpacer2";
			// 
			// B_Activate
			// 
			this.B_Activate.AutoSize = true;
			this.B_Activate.ColorStyle = Extensions.ColorStyle.Green;
			this.TLP_Side.SetColumnSpan(this.B_Activate, 2);
			this.B_Activate.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Activate.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon5.Name = "I_Check";
			this.B_Activate.ImageName = dynamicIcon5;
			this.B_Activate.Location = new System.Drawing.Point(3, 165);
			this.B_Activate.MatchBackgroundColor = true;
			this.B_Activate.Name = "B_Activate";
			this.B_Activate.Size = new System.Drawing.Size(244, 28);
			this.B_Activate.SpaceTriggersClick = true;
			this.B_Activate.TabIndex = 25;
			this.B_Activate.Text = "ActivatePlayset";
			this.B_Activate.Click += new System.EventHandler(this.B_Activate_Click);
			// 
			// B_EditColor
			// 
			this.B_EditColor.AutoSize = true;
			this.B_EditColor.ColorStyle = Extensions.ColorStyle.Green;
			this.B_EditColor.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_EditColor.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon6.Name = "I_Paint";
			this.B_EditColor.ImageName = dynamicIcon6;
			this.B_EditColor.Location = new System.Drawing.Point(3, 318);
			this.B_EditColor.MatchBackgroundColor = true;
			this.B_EditColor.Name = "B_EditColor";
			this.B_EditColor.Size = new System.Drawing.Size(119, 28);
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
			this.B_EditThumbnail.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon7.Name = "I_EditImage";
			this.B_EditThumbnail.ImageName = dynamicIcon7;
			this.B_EditThumbnail.Location = new System.Drawing.Point(128, 318);
			this.B_EditThumbnail.MatchBackgroundColor = true;
			this.B_EditThumbnail.Name = "B_EditThumbnail";
			this.B_EditThumbnail.Size = new System.Drawing.Size(119, 28);
			this.B_EditThumbnail.SpaceTriggersClick = true;
			this.B_EditThumbnail.TabIndex = 26;
			this.B_EditThumbnail.Text = "EditPlaysetThumbnail";
			this.B_EditThumbnail.Click += new System.EventHandler(this.B_EditThumbnail_Click);
			// 
			// PC_PlaysetSettings
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.P_ScrollPanel);
			this.Controls.Add(this.P_Side);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_PlaysetSettings";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 5, 0);
			this.Size = new System.Drawing.Size(1182, 960);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.P_Side, 0);
			this.Controls.SetChildIndex(this.P_ScrollPanel, 0);
			this.P_ScrollPanel.ResumeLayout(false);
			this.P_ScrollPanel.PerformLayout();
			this.TLP_Options.ResumeLayout(false);
			this.TLP_Options.PerformLayout();
			this.TLP_LaunchSettings.ResumeLayout(false);
			this.TLP_LaunchSettings.PerformLayout();
			this.TLP_AdvancedDev.ResumeLayout(false);
			this.TLP_AdvancedDev.PerformLayout();
			this.P_Side.ResumeLayout(false);
			this.P_Side.PerformLayout();
			this.roundedPanel1.ResumeLayout(false);
			this.roundedPanel1.PerformLayout();
			this.TLP_Side.ResumeLayout(false);
			this.TLP_Side.PerformLayout();
			this.P_Name.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private SlickControls.SlickScroll slickScroll;
	private System.Windows.Forms.TableLayoutPanel TLP_Options;
	private System.Windows.Forms.Panel P_ScrollPanel;
	private SlickControls.SlickTextBox TB_Name;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_AdvancedDev;
	private SlickControls.SlickCheckbox CB_UseCitiesExe;
	private SlickControls.SlickCheckbox CB_DevUI;
	private SlickControls.SlickIcon I_Favorite;
	private SlickControls.SlickTextBox TB_CustomArgs;
	private PackageUsageSingleDropDown DD_PlaysetUsage;
	private RoundedGroupTableLayoutPanel TLP_LaunchSettings;
	private SlickCheckbox CB_NoMods;
	private DragAndDropControl DD_NewMap;
	private SlickCheckbox CB_NoAssets;
	private DragAndDropControl DD_SaveFile;
	private SlickSpacer slickSpacer1;
	private SlickCheckbox CB_LoadSave;
	private SlickCheckbox CB_StartNewGame;
	private SlickCheckbox slickCheckbox1;
	private Content.LogLevelDropdown logLevelDropdown1;
	private SlickCheckbox slickCheckbox2;
	private System.Windows.Forms.Panel P_Side;
	private System.Windows.Forms.TableLayoutPanel TLP_Side;
	private SlickSpacer slickSpacer2;
	private RoundedPanel roundedPanel1;
	private Content.InfoAndLabelControl LI_ModCount;
	private Content.InfoAndLabelControl LI_ModSize;
	private SlickScroll slickScroll1;
	private SlickButton B_Activate;
	private SlickButton B_EditColor;
	private SlickButton B_EditThumbnail;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	private SlickIcon I_More;
	private App.UserInterface.Content.PlaysetIcon PB_Icon;
	private System.Windows.Forms.Panel P_Name;
	private AutoSizeLabel L_CurrentPlayset;
}
