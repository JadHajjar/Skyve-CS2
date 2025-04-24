using Skyve.App.CS2.UserInterface.Generic;
using Skyve.App.UserInterface.Dropdowns;

namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_ViewReviewRequest
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
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon11 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon12 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon13 = new SlickControls.DynamicIcon();
			this.TLP_Main = new SlickControls.RoundedTableLayoutPanel();
			this.roundedGroupTableLayoutPanel1 = new SlickControls.RoundedGroupTableLayoutPanel();
			this.L_Note = new SlickControls.AutoSizeLabel();
			this.B_Copy = new SlickControls.SlickButton();
			this.B_Translate = new SlickControls.SlickButton();
			this.TLP_LogReport = new SlickControls.RoundedGroupTableLayoutPanel();
			this.TLP_Changes = new SlickControls.RoundedGroupTableLayoutPanel();
			this.DD_Stability = new Skyve.App.UserInterface.Dropdowns.PackageStabilityDropDown();
			this.DD_PackageType = new Skyve.App.UserInterface.Dropdowns.PackageTypeDropDown();
			this.DD_Usage = new Skyve.App.UserInterface.Dropdowns.PackageUsageDropDown();
			this.DD_DLCs = new Skyve.App.UserInterface.Dropdowns.DlcDropDown();
			this.DD_SavegameEffect = new Skyve.App.UserInterface.Dropdowns.SavegameEffectDropDown();
			this.TLP_Savegame = new SlickControls.RoundedGroupTableLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.B_DeleteRequest = new SlickControls.SlickButton();
			this.B_Reply = new SlickControls.SlickButton();
			this.B_ApplyChanges = new SlickControls.SlickButton();
			this.B_ManagePackage = new SlickControls.SlickButton();
			this.P_Info = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.P_Reply = new System.Windows.Forms.Panel();
			this.slickScroll2 = new SlickControls.SlickScroll();
			this.TLP_Reply = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Actions = new System.Windows.Forms.TableLayoutPanel();
			this.B_LetUserKnowIsFixed = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.B_LetUserKnowSaveFileNeeded = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.slickSpacer4 = new SlickControls.SlickSpacer();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Link = new SlickControls.SlickTextBox();
			this.TB_Note = new SlickControls.SlickTextBox();
			this.B_SendReply = new SlickControls.SlickButton();
			this.TLP_Main.SuspendLayout();
			this.roundedGroupTableLayoutPanel1.SuspendLayout();
			this.TLP_Changes.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.P_Info.SuspendLayout();
			this.panel1.SuspendLayout();
			this.P_Reply.SuspendLayout();
			this.TLP_Reply.SuspendLayout();
			this.TLP_Actions.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// P_SideContainer
			// 
			this.P_SideContainer.Location = new System.Drawing.Point(1469, 30);
			this.P_SideContainer.Size = new System.Drawing.Size(200, 788);
			// 
			// base_Text
			// 
			this.base_Text.ButtonType = SlickControls.ButtonType.Normal;
			this.base_Text.ColorStyle = Extensions.ColorStyle.Active;
			this.base_Text.Size = new System.Drawing.Size(196, 39);
			this.base_Text.Text = "Review Request Info";
			// 
			// TLP_Main
			// 
			this.TLP_Main.AutoSize = true;
			this.TLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Main.ColumnCount = 3;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.TLP_Main.Controls.Add(this.roundedGroupTableLayoutPanel1, 0, 1);
			this.TLP_Main.Controls.Add(this.TLP_LogReport, 0, 0);
			this.TLP_Main.Controls.Add(this.TLP_Changes, 2, 0);
			this.TLP_Main.Controls.Add(this.TLP_Savegame, 1, 0);
			this.TLP_Main.Location = new System.Drawing.Point(15, 6);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 2;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Size = new System.Drawing.Size(1794, 259);
			this.TLP_Main.TabIndex = 2;
			// 
			// roundedGroupTableLayoutPanel1
			// 
			this.roundedGroupTableLayoutPanel1.AddOutline = true;
			this.roundedGroupTableLayoutPanel1.AddShadow = true;
			this.roundedGroupTableLayoutPanel1.AutoSize = true;
			this.roundedGroupTableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedGroupTableLayoutPanel1.ColumnCount = 2;
			this.TLP_Main.SetColumnSpan(this.roundedGroupTableLayoutPanel1, 2);
			this.roundedGroupTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.roundedGroupTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.L_Note, 0, 1);
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.B_Copy, 1, 0);
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.B_Translate, 0, 0);
			this.roundedGroupTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon3.Name = "AskHelp";
			this.roundedGroupTableLayoutPanel1.ImageName = dynamicIcon3;
			this.roundedGroupTableLayoutPanel1.Location = new System.Drawing.Point(3, 78);
			this.roundedGroupTableLayoutPanel1.Name = "roundedGroupTableLayoutPanel1";
			this.roundedGroupTableLayoutPanel1.Padding = new System.Windows.Forms.Padding(16);
			this.roundedGroupTableLayoutPanel1.RowCount = 2;
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.Size = new System.Drawing.Size(1189, 105);
			this.roundedGroupTableLayoutPanel1.TabIndex = 8;
			this.roundedGroupTableLayoutPanel1.Text = "Description";
			this.roundedGroupTableLayoutPanel1.UseFirstRowForPadding = true;
			// 
			// L_Note
			// 
			this.L_Note.AutoSize = true;
			this.roundedGroupTableLayoutPanel1.SetColumnSpan(this.L_Note, 2);
			this.L_Note.Dock = System.Windows.Forms.DockStyle.Top;
			this.L_Note.Location = new System.Drawing.Point(19, 56);
			this.L_Note.Name = "L_Note";
			this.L_Note.Size = new System.Drawing.Size(1151, 30);
			this.L_Note.TabIndex = 7;
			this.L_Note.Text = "Note";
			// 
			// B_Copy
			// 
			this.B_Copy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Copy.AutoSize = true;
			this.B_Copy.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "Copy";
			this.B_Copy.ImageName = dynamicIcon1;
			this.B_Copy.Location = new System.Drawing.Point(1144, 19);
			this.B_Copy.Name = "B_Copy";
			this.B_Copy.Size = new System.Drawing.Size(26, 26);
			this.B_Copy.SpaceTriggersClick = true;
			this.B_Copy.TabIndex = 5;
			this.B_Copy.Click += new System.EventHandler(this.I_Copy_Click);
			// 
			// B_Translate
			// 
			this.B_Translate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Translate.AutoSize = true;
			this.B_Translate.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon2.Name = "Translate";
			this.B_Translate.ImageName = dynamicIcon2;
			this.B_Translate.Location = new System.Drawing.Point(1112, 19);
			this.B_Translate.Name = "B_Translate";
			this.B_Translate.Size = new System.Drawing.Size(26, 26);
			this.B_Translate.SpaceTriggersClick = true;
			this.B_Translate.TabIndex = 6;
			this.B_Translate.Click += new System.EventHandler(this.B_Translate_Click);
			// 
			// TLP_LogReport
			// 
			this.TLP_LogReport.AddOutline = true;
			this.TLP_LogReport.AddShadow = true;
			this.TLP_LogReport.AutoSize = true;
			this.TLP_LogReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_LogReport.ColumnCount = 1;
			this.TLP_LogReport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_LogReport.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon4.Name = "Log";
			this.TLP_LogReport.ImageName = dynamicIcon4;
			this.TLP_LogReport.Location = new System.Drawing.Point(3, 3);
			this.TLP_LogReport.Name = "TLP_LogReport";
			this.TLP_LogReport.Padding = new System.Windows.Forms.Padding(16, 53, 16, 16);
			this.TLP_LogReport.RowCount = 1;
			this.TLP_LogReport.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LogReport.Size = new System.Drawing.Size(591, 69);
			this.TLP_LogReport.TabIndex = 6;
			this.TLP_LogReport.Text = "Log Report";
			// 
			// TLP_Changes
			// 
			this.TLP_Changes.AddOutline = true;
			this.TLP_Changes.AddShadow = true;
			this.TLP_Changes.AutoSize = true;
			this.TLP_Changes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Changes.ColumnCount = 1;
			this.TLP_Changes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Changes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Changes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Changes.Controls.Add(this.DD_Stability, 0, 0);
			this.TLP_Changes.Controls.Add(this.DD_PackageType, 0, 1);
			this.TLP_Changes.Controls.Add(this.DD_Usage, 0, 2);
			this.TLP_Changes.Controls.Add(this.DD_DLCs, 0, 3);
			this.TLP_Changes.Controls.Add(this.DD_SavegameEffect, 0, 4);
			this.TLP_Changes.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon5.Name = "RequestReview";
			this.TLP_Changes.ImageName = dynamicIcon5;
			this.TLP_Changes.Location = new System.Drawing.Point(1195, 0);
			this.TLP_Changes.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Changes.Name = "TLP_Changes";
			this.TLP_Changes.Padding = new System.Windows.Forms.Padding(16, 53, 16, 16);
			this.TLP_Changes.RowCount = 5;
			this.TLP_Main.SetRowSpan(this.TLP_Changes, 2);
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Changes.Size = new System.Drawing.Size(599, 259);
			this.TLP_Changes.TabIndex = 3;
			this.TLP_Changes.Text = "Proposed Changes";
			this.TLP_Changes.Visible = false;
			// 
			// DD_Stability
			// 
			this.DD_Stability.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Stability.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Stability.ItemHeight = 24;
			this.DD_Stability.Location = new System.Drawing.Point(19, 56);
			this.DD_Stability.Name = "DD_Stability";
			this.DD_Stability.Size = new System.Drawing.Size(561, 30);
			this.DD_Stability.TabIndex = 5;
			this.DD_Stability.Text = "Stability";
			// 
			// DD_PackageType
			// 
			this.DD_PackageType.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PackageType.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_PackageType.ItemHeight = 24;
			this.DD_PackageType.Location = new System.Drawing.Point(19, 92);
			this.DD_PackageType.Name = "DD_PackageType";
			this.DD_PackageType.Size = new System.Drawing.Size(561, 35);
			this.DD_PackageType.TabIndex = 6;
			this.DD_PackageType.Text = "PackageType";
			// 
			// DD_Usage
			// 
			this.DD_Usage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Usage.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Usage.ItemHeight = 24;
			this.DD_Usage.Location = new System.Drawing.Point(19, 133);
			this.DD_Usage.Name = "DD_Usage";
			this.DD_Usage.Size = new System.Drawing.Size(561, 30);
			this.DD_Usage.TabIndex = 8;
			this.DD_Usage.Text = "Usage";
			// 
			// DD_DLCs
			// 
			this.DD_DLCs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_DLCs.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_DLCs.ItemHeight = 24;
			this.DD_DLCs.Location = new System.Drawing.Point(19, 169);
			this.DD_DLCs.Name = "DD_DLCs";
			this.DD_DLCs.Size = new System.Drawing.Size(561, 35);
			this.DD_DLCs.TabIndex = 9;
			this.DD_DLCs.Text = "RequiredDLCs";
			// 
			// DD_SavegameEffect
			// 
			this.DD_SavegameEffect.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_SavegameEffect.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_SavegameEffect.ItemHeight = 24;
			this.DD_SavegameEffect.Location = new System.Drawing.Point(19, 210);
			this.DD_SavegameEffect.Name = "DD_SavegameEffect";
			this.DD_SavegameEffect.Size = new System.Drawing.Size(561, 30);
			this.DD_SavegameEffect.TabIndex = 7;
			this.DD_SavegameEffect.Text = "SavegameEffect";
			// 
			// TLP_Savegame
			// 
			this.TLP_Savegame.AddOutline = true;
			this.TLP_Savegame.AddShadow = true;
			this.TLP_Savegame.AutoSize = true;
			this.TLP_Savegame.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Savegame.ColumnCount = 1;
			this.TLP_Savegame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Savegame.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon6.Name = "Savegame";
			this.TLP_Savegame.ImageName = dynamicIcon6;
			this.TLP_Savegame.Location = new System.Drawing.Point(600, 3);
			this.TLP_Savegame.Name = "TLP_Savegame";
			this.TLP_Savegame.Padding = new System.Windows.Forms.Padding(16, 53, 16, 16);
			this.TLP_Savegame.RowCount = 1;
			this.TLP_Savegame.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Savegame.Size = new System.Drawing.Size(592, 69);
			this.TLP_Savegame.TabIndex = 7;
			this.TLP_Savegame.Text = "Savegame";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.slickSpacer2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_DeleteRequest, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_Reply, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_ApplyChanges, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_ManagePackage, 3, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 766);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1469, 52);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// slickSpacer2
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.slickSpacer2, 4);
			this.slickSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer2.Location = new System.Drawing.Point(3, 3);
			this.slickSpacer2.Name = "slickSpacer2";
			this.slickSpacer2.Size = new System.Drawing.Size(1463, 14);
			this.slickSpacer2.TabIndex = 6;
			this.slickSpacer2.TabStop = false;
			this.slickSpacer2.Text = "slickSpacer2";
			// 
			// B_DeleteRequest
			// 
			this.B_DeleteRequest.AutoSize = true;
			this.B_DeleteRequest.ButtonType = SlickControls.ButtonType.Hidden;
			this.B_DeleteRequest.ColorStyle = Extensions.ColorStyle.Red;
			this.B_DeleteRequest.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon7.Name = "Trash";
			this.B_DeleteRequest.ImageName = dynamicIcon7;
			this.B_DeleteRequest.Location = new System.Drawing.Point(3, 23);
			this.B_DeleteRequest.Name = "B_DeleteRequest";
			this.B_DeleteRequest.Size = new System.Drawing.Size(133, 26);
			this.B_DeleteRequest.SpaceTriggersClick = true;
			this.B_DeleteRequest.TabIndex = 2;
			this.B_DeleteRequest.Text = "DeleteRequests";
			this.B_DeleteRequest.Click += new System.EventHandler(this.B_DeleteRequest_Click);
			// 
			// B_Reply
			// 
			this.B_Reply.AutoSize = true;
			this.B_Reply.ButtonType = SlickControls.ButtonType.Dimmed;
			this.B_Reply.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon8.Name = "Chat";
			this.B_Reply.ImageName = dynamicIcon8;
			this.B_Reply.Location = new System.Drawing.Point(1029, 23);
			this.B_Reply.Name = "B_Reply";
			this.B_Reply.Size = new System.Drawing.Size(131, 26);
			this.B_Reply.SpaceTriggersClick = true;
			this.B_Reply.TabIndex = 1;
			this.B_Reply.Text = "ReplyToRequester";
			this.B_Reply.Click += new System.EventHandler(this.B_Reply_Click);
			// 
			// B_ApplyChanges
			// 
			this.B_ApplyChanges.AutoSize = true;
			this.B_ApplyChanges.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon9.Name = "Link";
			this.B_ApplyChanges.ImageName = dynamicIcon9;
			this.B_ApplyChanges.Location = new System.Drawing.Point(1166, 23);
			this.B_ApplyChanges.Name = "B_ApplyChanges";
			this.B_ApplyChanges.Size = new System.Drawing.Size(170, 26);
			this.B_ApplyChanges.SpaceTriggersClick = true;
			this.B_ApplyChanges.TabIndex = 1;
			this.B_ApplyChanges.Text = "ApplyRequestedChanges";
			this.B_ApplyChanges.Click += new System.EventHandler(this.B_ApplyChanges_Click);
			// 
			// B_ManagePackage
			// 
			this.B_ManagePackage.AutoSize = true;
			this.B_ManagePackage.ButtonType = SlickControls.ButtonType.Active;
			this.B_ManagePackage.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon10.Name = "Link";
			this.B_ManagePackage.ImageName = dynamicIcon10;
			this.B_ManagePackage.Location = new System.Drawing.Point(1342, 23);
			this.B_ManagePackage.Name = "B_ManagePackage";
			this.B_ManagePackage.Size = new System.Drawing.Size(124, 26);
			this.B_ManagePackage.SpaceTriggersClick = true;
			this.B_ManagePackage.TabIndex = 0;
			this.B_ManagePackage.Text = "ManagePackage";
			this.B_ManagePackage.Click += new System.EventHandler(this.B_ManagePackage_Click);
			// 
			// P_Info
			// 
			this.P_Info.Controls.Add(this.panel1);
			this.P_Info.Controls.Add(this.slickScroll1);
			this.P_Info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Info.Location = new System.Drawing.Point(0, 30);
			this.P_Info.Name = "P_Info";
			this.P_Info.Size = new System.Drawing.Size(1469, 736);
			this.P_Info.TabIndex = 16;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.TLP_Main);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1453, 736);
			this.panel1.TabIndex = 3;
			// 
			// slickScroll1
			// 
			this.slickScroll1.AnimatedValue = 8;
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.TLP_Main;
			this.slickScroll1.Location = new System.Drawing.Point(1453, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(16, 736);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 0;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.TargetAnimationValue = 8;
			this.slickScroll1.Text = "slickScroll1";
			// 
			// P_Reply
			// 
			this.P_Reply.Controls.Add(this.slickScroll2);
			this.P_Reply.Controls.Add(this.TLP_Reply);
			this.P_Reply.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Reply.Location = new System.Drawing.Point(0, 30);
			this.P_Reply.Name = "P_Reply";
			this.P_Reply.Size = new System.Drawing.Size(1469, 736);
			this.P_Reply.TabIndex = 17;
			this.P_Reply.Visible = false;
			// 
			// slickScroll2
			// 
			this.slickScroll2.AnimatedValue = 8;
			this.slickScroll2.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll2.LinkedControl = this.TLP_Reply;
			this.slickScroll2.Location = new System.Drawing.Point(1453, 0);
			this.slickScroll2.Name = "slickScroll2";
			this.slickScroll2.Size = new System.Drawing.Size(16, 736);
			this.slickScroll2.Style = SlickControls.StyleType.Vertical;
			this.slickScroll2.TabIndex = 22;
			this.slickScroll2.TabStop = false;
			this.slickScroll2.TargetAnimationValue = 8;
			this.slickScroll2.Text = "slickScroll2";
			// 
			// TLP_Reply
			// 
			this.TLP_Reply.AutoSize = true;
			this.TLP_Reply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Reply.ColumnCount = 3;
			this.TLP_Reply.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Reply.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Reply.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Reply.Controls.Add(this.TLP_Actions, 0, 2);
			this.TLP_Reply.Controls.Add(this.slickSpacer1, 0, 1);
			this.TLP_Reply.Controls.Add(this.slickSpacer4, 2, 1);
			this.TLP_Reply.Controls.Add(this.label1, 1, 1);
			this.TLP_Reply.Controls.Add(this.tableLayoutPanel4, 0, 0);
			this.TLP_Reply.Location = new System.Drawing.Point(0, 0);
			this.TLP_Reply.Name = "TLP_Reply";
			this.TLP_Reply.RowCount = 3;
			this.TLP_Reply.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Reply.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Reply.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Reply.Size = new System.Drawing.Size(827, 498);
			this.TLP_Reply.TabIndex = 21;
			// 
			// TLP_Actions
			// 
			this.TLP_Actions.AutoSize = true;
			this.TLP_Actions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Actions.ColumnCount = 4;
			this.TLP_Reply.SetColumnSpan(this.TLP_Actions, 3);
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Actions.Controls.Add(this.B_LetUserKnowIsFixed, 1, 1);
			this.TLP_Actions.Controls.Add(this.B_LetUserKnowSaveFileNeeded, 2, 1);
			this.TLP_Actions.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Actions.Location = new System.Drawing.Point(3, 164);
			this.TLP_Actions.Name = "TLP_Actions";
			this.TLP_Actions.RowCount = 3;
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Actions.Size = new System.Drawing.Size(821, 331);
			this.TLP_Actions.TabIndex = 26;
			// 
			// B_LetUserKnowIsFixed
			// 
			this.B_LetUserKnowIsFixed.ButtonText = "SendReply";
			this.B_LetUserKnowIsFixed.ColorStyle = Extensions.ColorStyle.Active;
			this.B_LetUserKnowIsFixed.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon11.Name = "Ok";
			this.B_LetUserKnowIsFixed.ImageName = dynamicIcon11;
			this.B_LetUserKnowIsFixed.Location = new System.Drawing.Point(210, 3);
			this.B_LetUserKnowIsFixed.Name = "B_LetUserKnowIsFixed";
			this.B_LetUserKnowIsFixed.Size = new System.Drawing.Size(197, 325);
			this.B_LetUserKnowIsFixed.TabIndex = 0;
			this.B_LetUserKnowIsFixed.Text = "LetUserKnowIsFixed";
			this.B_LetUserKnowIsFixed.Title = "IssueFixed";
			this.B_LetUserKnowIsFixed.Click += new System.EventHandler(this.B_LetUserKnowIsFixed_Click);
			// 
			// B_LetUserKnowSaveFileNeeded
			// 
			this.B_LetUserKnowSaveFileNeeded.ButtonText = "SendReply";
			this.B_LetUserKnowSaveFileNeeded.ColorStyle = Extensions.ColorStyle.Active;
			this.B_LetUserKnowSaveFileNeeded.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon12.Name = "SaveGame";
			this.B_LetUserKnowSaveFileNeeded.ImageName = dynamicIcon12;
			this.B_LetUserKnowSaveFileNeeded.Location = new System.Drawing.Point(413, 3);
			this.B_LetUserKnowSaveFileNeeded.Name = "B_LetUserKnowSaveFileNeeded";
			this.B_LetUserKnowSaveFileNeeded.Size = new System.Drawing.Size(197, 325);
			this.B_LetUserKnowSaveFileNeeded.TabIndex = 0;
			this.B_LetUserKnowSaveFileNeeded.Text = "LetUserKnowSaveFileNeeded";
			this.B_LetUserKnowSaveFileNeeded.Title = "RequestSavegame";
			this.B_LetUserKnowSaveFileNeeded.Click += new System.EventHandler(this.B_LetUserKnowSaveFileNeeded_Click);
			// 
			// slickSpacer1
			// 
			this.slickSpacer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.slickSpacer1.Location = new System.Drawing.Point(3, 135);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(393, 23);
			this.slickSpacer1.TabIndex = 22;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// slickSpacer4
			// 
			this.slickSpacer4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.slickSpacer4.Location = new System.Drawing.Point(431, 135);
			this.slickSpacer4.Name = "slickSpacer4";
			this.slickSpacer4.Size = new System.Drawing.Size(393, 23);
			this.slickSpacer4.TabIndex = 23;
			this.slickSpacer4.TabStop = false;
			this.slickSpacer4.Text = "slickSpacer4";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(402, 132);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 13);
			this.label1.TabIndex = 24;
			this.label1.Text = "OR";
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.AutoSize = true;
			this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel4.ColumnCount = 2;
			this.TLP_Reply.SetColumnSpan(this.tableLayoutPanel4, 3);
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel4.Controls.Add(this.TB_Link, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.TB_Note, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.B_SendReply, 0, 2);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 3;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.Size = new System.Drawing.Size(821, 126);
			this.tableLayoutPanel4.TabIndex = 25;
			// 
			// TB_Link
			// 
			this.TB_Link.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_Link.LabelText = "Link";
			this.TB_Link.Location = new System.Drawing.Point(3, 50);
			this.TB_Link.MultiLine = true;
			this.TB_Link.Name = "TB_Link";
			this.TB_Link.Padding = new System.Windows.Forms.Padding(5, 16, 5, 5);
			this.TB_Link.Placeholder = "Add an optional link for the requester to follow";
			this.TB_Link.SelectedText = "";
			this.TB_Link.SelectionLength = 0;
			this.TB_Link.SelectionStart = 0;
			this.TB_Link.Size = new System.Drawing.Size(609, 41);
			this.TB_Link.TabIndex = 22;
			// 
			// TB_Note
			// 
			this.TB_Note.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_Note.LabelText = "Message";
			this.TB_Note.Location = new System.Drawing.Point(3, 3);
			this.TB_Note.MaxLength = 500;
			this.TB_Note.MultiLine = true;
			this.TB_Note.Name = "TB_Note";
			this.TB_Note.Padding = new System.Windows.Forms.Padding(5, 16, 5, 5);
			this.TB_Note.Placeholder = "Write a message to send to the requester. Use  {0}  as a placeholder for the mod\'" +
    "s name";
			this.TB_Note.SelectedText = "";
			this.TB_Note.SelectionLength = 0;
			this.TB_Note.SelectionStart = 0;
			this.TB_Note.Size = new System.Drawing.Size(609, 41);
			this.TB_Note.TabIndex = 21;
			// 
			// B_SendReply
			// 
			this.B_SendReply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_SendReply.AutoSize = true;
			this.B_SendReply.ButtonType = SlickControls.ButtonType.Active;
			this.B_SendReply.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon13.Name = "Link";
			this.B_SendReply.ImageName = dynamicIcon13;
			this.B_SendReply.Location = new System.Drawing.Point(501, 97);
			this.B_SendReply.Name = "B_SendReply";
			this.B_SendReply.Size = new System.Drawing.Size(111, 26);
			this.B_SendReply.SpaceTriggersClick = true;
			this.B_SendReply.TabIndex = 0;
			this.B_SendReply.Text = "SendReply";
			this.B_SendReply.Click += new System.EventHandler(this.B_SendReply_Click);
			// 
			// PC_ViewReviewRequest
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.P_Info);
			this.Controls.Add(this.P_Reply);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PC_ViewReviewRequest";
			this.Size = new System.Drawing.Size(1669, 818);
			this.Text = "Review Request Info";
			this.Controls.SetChildIndex(this.P_SideContainer, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.P_Reply, 0);
			this.Controls.SetChildIndex(this.P_Info, 0);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.roundedGroupTableLayoutPanel1.ResumeLayout(false);
			this.roundedGroupTableLayoutPanel1.PerformLayout();
			this.TLP_Changes.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.P_Info.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.P_Reply.ResumeLayout(false);
			this.P_Reply.PerformLayout();
			this.TLP_Reply.ResumeLayout(false);
			this.TLP_Reply.PerformLayout();
			this.TLP_Actions.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private SlickControls.RoundedTableLayoutPanel TLP_Main;
	private SlickControls.SlickButton B_DeleteRequest;
	private SlickControls.SlickButton B_ApplyChanges;
	private SlickControls.SlickButton B_ManagePackage;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_Changes;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private SlickSpacer slickSpacer2;
	private SlickButton B_Reply;
	private System.Windows.Forms.Panel P_Info;
	private SlickScroll slickScroll1;
	private System.Windows.Forms.Panel P_Reply;
	private SlickScroll slickScroll2;
	private System.Windows.Forms.TableLayoutPanel TLP_Reply;
	private SlickSpacer slickSpacer1;
	private SlickSpacer slickSpacer4;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
	private SlickTextBox TB_Note;
	private SlickButton B_SendReply;
	private System.Windows.Forms.TableLayoutPanel TLP_Actions;
	private App.UserInterface.Generic.BigSelectionOptionControl B_LetUserKnowIsFixed;
	private App.UserInterface.Generic.BigSelectionOptionControl B_LetUserKnowSaveFileNeeded;
	private SlickTextBox TB_Link;
	private PackageStabilityDropDown DD_Stability;
	private PackageTypeDropDown DD_PackageType;
	private PackageUsageDropDown DD_Usage;
	private DlcDropDown DD_DLCs;
	private SavegameEffectDropDown DD_SavegameEffect;
	private RoundedGroupTableLayoutPanel TLP_LogReport;
	private RoundedGroupTableLayoutPanel TLP_Savegame;
	private System.Windows.Forms.Panel panel1;
	private RoundedGroupTableLayoutPanel roundedGroupTableLayoutPanel1;
	private SlickButton B_Copy;
	private SlickButton B_Translate;
	private AutoSizeLabel L_Note;
}
