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
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			this.TLP_Main = new SlickControls.RoundedTableLayoutPanel();
			this.slickSpacer3 = new SlickControls.SlickSpacer();
			this.I_Copy = new SlickControls.SlickIcon();
			this.L_Desc = new System.Windows.Forms.Label();
			this.L_Note = new System.Windows.Forms.Label();
			this.L_ProposedChanges = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.DD_Stability = new Skyve.App.UserInterface.Dropdowns.PackageStabilityDropDown();
			this.DD_Usage = new Skyve.App.UserInterface.Dropdowns.PackageUsageDropDown();
			this.DD_PackageType = new Skyve.App.UserInterface.Dropdowns.PackageTypeDropDown();
			this.DD_DLCs = new Skyve.App.UserInterface.Dropdowns.DlcDropDown();
			this.L_LogReport = new System.Windows.Forms.Label();
			this.L_Savegame = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.B_DeleteRequest = new SlickControls.SlickButton();
			this.B_Reply = new SlickControls.SlickButton();
			this.B_ApplyChanges = new SlickControls.SlickButton();
			this.B_ManagePackage = new SlickControls.SlickButton();
			this.P_Info = new System.Windows.Forms.Panel();
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
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.P_Info.SuspendLayout();
			this.P_Reply.SuspendLayout();
			this.TLP_Reply.SuspendLayout();
			this.TLP_Actions.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(196, 39);
			this.base_Text.Text = "Review Request Info";
			// 
			// TLP_Main
			// 
			this.TLP_Main.AutoSize = true;
			this.TLP_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Main.ColumnCount = 3;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.Controls.Add(this.slickSpacer3, 0, 4);
			this.TLP_Main.Controls.Add(this.I_Copy, 2, 0);
			this.TLP_Main.Controls.Add(this.L_Desc, 0, 0);
			this.TLP_Main.Controls.Add(this.L_Note, 0, 1);
			this.TLP_Main.Controls.Add(this.L_ProposedChanges, 0, 5);
			this.TLP_Main.Controls.Add(this.tableLayoutPanel3, 0, 7);
			this.TLP_Main.Controls.Add(this.L_LogReport, 0, 2);
			this.TLP_Main.Controls.Add(this.L_Savegame, 1, 2);
			this.TLP_Main.Location = new System.Drawing.Point(0, 0);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 10;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Main.Size = new System.Drawing.Size(65535, 518);
			this.TLP_Main.TabIndex = 2;
			// 
			// slickSpacer3
			// 
			this.slickSpacer3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.TLP_Main.SetColumnSpan(this.slickSpacer3, 3);
			this.slickSpacer3.Location = new System.Drawing.Point(3, 60);
			this.slickSpacer3.Name = "slickSpacer3";
			this.slickSpacer3.Size = new System.Drawing.Size(65529, 1);
			this.slickSpacer3.TabIndex = 7;
			this.slickSpacer3.TabStop = false;
			this.slickSpacer3.Text = "slickSpacer3";
			// 
			// I_Copy
			// 
			this.I_Copy.ActiveColor = null;
			this.I_Copy.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "Copy";
			this.I_Copy.ImageName = dynamicIcon1;
			this.I_Copy.Location = new System.Drawing.Point(32767, 3);
			this.I_Copy.Name = "I_Copy";
			this.TLP_Main.SetRowSpan(this.I_Copy, 2);
			this.I_Copy.Size = new System.Drawing.Size(23, 26);
			this.I_Copy.TabIndex = 4;
			this.I_Copy.Click += new System.EventHandler(this.I_Copy_Click);
			// 
			// L_Desc
			// 
			this.L_Desc.AutoSize = true;
			this.TLP_Main.SetColumnSpan(this.L_Desc, 2);
			this.L_Desc.Location = new System.Drawing.Point(3, 0);
			this.L_Desc.Name = "L_Desc";
			this.L_Desc.Size = new System.Drawing.Size(78, 19);
			this.L_Desc.TabIndex = 1;
			this.L_Desc.Text = "Description";
			// 
			// L_Note
			// 
			this.L_Note.AutoSize = true;
			this.L_Note.Location = new System.Drawing.Point(3, 19);
			this.L_Note.Name = "L_Note";
			this.L_Note.Size = new System.Drawing.Size(45, 19);
			this.L_Note.TabIndex = 1;
			this.L_Note.Text = "label1";
			// 
			// L_ProposedChanges
			// 
			this.L_ProposedChanges.AutoSize = true;
			this.TLP_Main.SetColumnSpan(this.L_ProposedChanges, 3);
			this.L_ProposedChanges.Location = new System.Drawing.Point(3, 64);
			this.L_ProposedChanges.Name = "L_ProposedChanges";
			this.L_ProposedChanges.Size = new System.Drawing.Size(124, 19);
			this.L_ProposedChanges.TabIndex = 1;
			this.L_ProposedChanges.Text = "Proposed Changes";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 3;
			this.TLP_Main.SetColumnSpan(this.tableLayoutPanel3, 3);
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
			this.tableLayoutPanel3.Controls.Add(this.DD_Stability, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.DD_Usage, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.DD_PackageType, 2, 1);
			this.tableLayoutPanel3.Controls.Add(this.DD_DLCs, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 83);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(65535, 435);
			this.tableLayoutPanel3.TabIndex = 3;
			this.tableLayoutPanel3.Visible = false;
			// 
			// DD_Stability
			// 
			this.DD_Stability.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Stability.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Stability.ItemHeight = 24;
			this.DD_Stability.Location = new System.Drawing.Point(3, 3);
			this.DD_Stability.Name = "DD_Stability";
			this.DD_Stability.Size = new System.Drawing.Size(27300, 56);
			this.DD_Stability.TabIndex = 18;
			this.DD_Stability.Text = "Stability";
			// 
			// DD_Usage
			// 
			this.DD_Usage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Usage.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Usage.ItemHeight = 24;
			this.DD_Usage.Location = new System.Drawing.Point(32767, 3);
			this.DD_Usage.Name = "DD_Usage";
			this.DD_Usage.Size = new System.Drawing.Size(27301, 54);
			this.DD_Usage.TabIndex = 20;
			this.DD_Usage.Text = "Usage";
			// 
			// DD_PackageType
			// 
			this.DD_PackageType.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PackageType.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_PackageType.ItemHeight = 24;
			this.DD_PackageType.Location = new System.Drawing.Point(32767, 65);
			this.DD_PackageType.Name = "DD_PackageType";
			this.DD_PackageType.Size = new System.Drawing.Size(27301, 54);
			this.DD_PackageType.TabIndex = 21;
			this.DD_PackageType.Text = "PackageType";
			// 
			// DD_DLCs
			// 
			this.DD_DLCs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_DLCs.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_DLCs.ItemHeight = 24;
			this.DD_DLCs.Location = new System.Drawing.Point(3, 65);
			this.DD_DLCs.Name = "DD_DLCs";
			this.DD_DLCs.Size = new System.Drawing.Size(27300, 54);
			this.DD_DLCs.TabIndex = 19;
			this.DD_DLCs.Text = "RequiredDLCs";
			// 
			// L_LogReport
			// 
			this.L_LogReport.AutoSize = true;
			this.L_LogReport.Location = new System.Drawing.Point(3, 38);
			this.L_LogReport.Name = "L_LogReport";
			this.L_LogReport.Size = new System.Drawing.Size(77, 19);
			this.L_LogReport.TabIndex = 1;
			this.L_LogReport.Text = "Log Report";
			// 
			// L_Savegame
			// 
			this.L_Savegame.AutoSize = true;
			this.L_Savegame.Location = new System.Drawing.Point(32756, 38);
			this.L_Savegame.Name = "L_Savegame";
			this.L_Savegame.Size = new System.Drawing.Size(77, 19);
			this.L_Savegame.TabIndex = 1;
			this.L_Savegame.Text = "Save-game";
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
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 595);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(796, 58);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// slickSpacer2
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.slickSpacer2, 4);
			this.slickSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer2.Location = new System.Drawing.Point(3, 3);
			this.slickSpacer2.Name = "slickSpacer2";
			this.slickSpacer2.Size = new System.Drawing.Size(790, 14);
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
			dynamicIcon2.Name = "Trash";
			this.B_DeleteRequest.ImageName = dynamicIcon2;
			this.B_DeleteRequest.Location = new System.Drawing.Point(3, 23);
			this.B_DeleteRequest.Name = "B_DeleteRequest";
			this.B_DeleteRequest.Size = new System.Drawing.Size(141, 32);
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
			dynamicIcon3.Name = "Chat";
			this.B_Reply.ImageName = dynamicIcon3;
			this.B_Reply.Location = new System.Drawing.Point(277, 23);
			this.B_Reply.Name = "B_Reply";
			this.B_Reply.Size = new System.Drawing.Size(157, 32);
			this.B_Reply.SpaceTriggersClick = true;
			this.B_Reply.TabIndex = 1;
			this.B_Reply.Text = "ReplyToRequester";
			this.B_Reply.Click += new System.EventHandler(this.B_Reply_Click);
			// 
			// B_ApplyChanges
			// 
			this.B_ApplyChanges.AutoSize = true;
			this.B_ApplyChanges.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "Link";
			this.B_ApplyChanges.ImageName = dynamicIcon4;
			this.B_ApplyChanges.Location = new System.Drawing.Point(440, 23);
			this.B_ApplyChanges.Name = "B_ApplyChanges";
			this.B_ApplyChanges.Size = new System.Drawing.Size(200, 32);
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
			dynamicIcon5.Name = "Link";
			this.B_ManagePackage.ImageName = dynamicIcon5;
			this.B_ManagePackage.Location = new System.Drawing.Point(646, 23);
			this.B_ManagePackage.Name = "B_ManagePackage";
			this.B_ManagePackage.Size = new System.Drawing.Size(147, 32);
			this.B_ManagePackage.SpaceTriggersClick = true;
			this.B_ManagePackage.TabIndex = 0;
			this.B_ManagePackage.Text = "ManagePackage";
			this.B_ManagePackage.Click += new System.EventHandler(this.B_ManagePackage_Click);
			// 
			// P_Info
			// 
			this.P_Info.Controls.Add(this.slickScroll1);
			this.P_Info.Controls.Add(this.TLP_Main);
			this.P_Info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Info.Location = new System.Drawing.Point(0, 30);
			this.P_Info.Name = "P_Info";
			this.P_Info.Size = new System.Drawing.Size(796, 565);
			this.P_Info.TabIndex = 16;
			// 
			// slickScroll1
			// 
			this.slickScroll1.AnimatedValue = 8;
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.TLP_Main;
			this.slickScroll1.Location = new System.Drawing.Point(778, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(18, 565);
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
			this.P_Reply.Size = new System.Drawing.Size(796, 565);
			this.P_Reply.TabIndex = 17;
			this.P_Reply.Visible = false;
			// 
			// slickScroll2
			// 
			this.slickScroll2.AnimatedValue = 8;
			this.slickScroll2.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll2.LinkedControl = this.TLP_Reply;
			this.slickScroll2.Location = new System.Drawing.Point(778, 0);
			this.slickScroll2.Name = "slickScroll2";
			this.slickScroll2.Size = new System.Drawing.Size(18, 565);
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
			this.TLP_Reply.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Reply.Size = new System.Drawing.Size(1190, 378);
			this.TLP_Reply.TabIndex = 21;
			// 
			// TLP_Actions
			// 
			this.TLP_Actions.AutoSize = true;
			this.TLP_Actions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Actions.ColumnCount = 3;
			this.TLP_Reply.SetColumnSpan(this.TLP_Actions, 3);
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Actions.Controls.Add(this.B_LetUserKnowIsFixed, 1, 1);
			this.TLP_Actions.Controls.Add(this.B_LetUserKnowSaveFileNeeded, 1, 2);
			this.TLP_Actions.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Actions.Location = new System.Drawing.Point(3, 143);
			this.TLP_Actions.Name = "TLP_Actions";
			this.TLP_Actions.RowCount = 4;
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Actions.Size = new System.Drawing.Size(1184, 232);
			this.TLP_Actions.TabIndex = 26;
			// 
			// B_LetUserKnowIsFixed
			// 
			this.B_LetUserKnowIsFixed.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_LetUserKnowIsFixed.FromScratch = false;
			dynamicIcon6.Name = "Ok";
			this.B_LetUserKnowIsFixed.ImageName = dynamicIcon6;
			this.B_LetUserKnowIsFixed.Location = new System.Drawing.Point(464, 3);
			this.B_LetUserKnowIsFixed.Name = "B_LetUserKnowIsFixed";
			this.B_LetUserKnowIsFixed.Padding = new System.Windows.Forms.Padding(22);
			this.B_LetUserKnowIsFixed.Size = new System.Drawing.Size(256, 101);
			this.B_LetUserKnowIsFixed.TabIndex = 0;
			this.B_LetUserKnowIsFixed.Text = "LetUserKnowIsFixed";
			this.B_LetUserKnowIsFixed.Click += new System.EventHandler(this.B_LetUserKnowIsFixed_Click);
			// 
			// B_LetUserKnowSaveFileNeeded
			// 
			this.B_LetUserKnowSaveFileNeeded.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_LetUserKnowSaveFileNeeded.FromScratch = false;
			dynamicIcon7.Name = "SaveGame";
			this.B_LetUserKnowSaveFileNeeded.ImageName = dynamicIcon7;
			this.B_LetUserKnowSaveFileNeeded.Location = new System.Drawing.Point(464, 110);
			this.B_LetUserKnowSaveFileNeeded.Name = "B_LetUserKnowSaveFileNeeded";
			this.B_LetUserKnowSaveFileNeeded.Padding = new System.Windows.Forms.Padding(22);
			this.B_LetUserKnowSaveFileNeeded.Size = new System.Drawing.Size(256, 119);
			this.B_LetUserKnowSaveFileNeeded.TabIndex = 0;
			this.B_LetUserKnowSaveFileNeeded.Text = "LetUserKnowSaveFileNeeded";
			this.B_LetUserKnowSaveFileNeeded.Click += new System.EventHandler(this.B_LetUserKnowSaveFileNeeded_Click);
			// 
			// slickSpacer1
			// 
			this.slickSpacer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.slickSpacer1.Location = new System.Drawing.Point(3, 114);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(572, 23);
			this.slickSpacer1.TabIndex = 22;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// slickSpacer4
			// 
			this.slickSpacer4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.slickSpacer4.Location = new System.Drawing.Point(615, 114);
			this.slickSpacer4.Name = "slickSpacer4";
			this.slickSpacer4.Size = new System.Drawing.Size(572, 23);
			this.slickSpacer4.TabIndex = 23;
			this.slickSpacer4.TabStop = false;
			this.slickSpacer4.Text = "slickSpacer4";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(581, 111);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 19);
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
			this.tableLayoutPanel4.Controls.Add(this.TB_Link, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.TB_Note, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.B_SendReply, 1, 1);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 3;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(1184, 105);
			this.tableLayoutPanel4.TabIndex = 25;
			// 
			// TB_Link
			// 
			this.TB_Link.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_Link.LabelText = "Link";
			this.TB_Link.Location = new System.Drawing.Point(891, 3);
			this.TB_Link.MultiLine = true;
			this.TB_Link.Name = "TB_Link";
			this.TB_Link.Padding = new System.Windows.Forms.Padding(6, 19, 6, 6);
			this.TB_Link.Placeholder = "Write a message to send to the requester";
			this.TB_Link.SelectedText = "";
			this.TB_Link.SelectionLength = 0;
			this.TB_Link.SelectionStart = 0;
			this.TB_Link.Size = new System.Drawing.Size(290, 41);
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
			this.TB_Note.Padding = new System.Windows.Forms.Padding(6, 19, 6, 6);
			this.TB_Note.Placeholder = "Write a message to send to the requester. Use  {0}  as a placeholder for the mod\'" +
    "s name";
			this.tableLayoutPanel4.SetRowSpan(this.TB_Note, 2);
			this.TB_Note.SelectedText = "";
			this.TB_Note.SelectionLength = 0;
			this.TB_Note.SelectionStart = 0;
			this.TB_Note.Size = new System.Drawing.Size(882, 41);
			this.TB_Note.TabIndex = 21;
			// 
			// B_SendReply
			// 
			this.B_SendReply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_SendReply.AutoSize = true;
			this.B_SendReply.ButtonType = SlickControls.ButtonType.Active;
			this.B_SendReply.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon8.Name = "Link";
			this.B_SendReply.ImageName = dynamicIcon8;
			this.B_SendReply.Location = new System.Drawing.Point(1071, 50);
			this.B_SendReply.Name = "B_SendReply";
			this.B_SendReply.Size = new System.Drawing.Size(110, 32);
			this.B_SendReply.SpaceTriggersClick = true;
			this.B_SendReply.TabIndex = 0;
			this.B_SendReply.Text = "SendReply";
			this.B_SendReply.Click += new System.EventHandler(this.B_SendReply_Click);
			// 
			// PC_ViewReviewRequest
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.P_Reply);
			this.Controls.Add(this.P_Info);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PC_ViewReviewRequest";
			this.Text = "Review Request Info";
			this.Controls.SetChildIndex(this.P_SideContainer, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.P_Info, 0);
			this.Controls.SetChildIndex(this.P_Reply, 0);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.P_Info.ResumeLayout(false);
			this.P_Info.PerformLayout();
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
	private System.Windows.Forms.Label L_Desc;
	private System.Windows.Forms.Label L_Note;
	private System.Windows.Forms.Label L_ProposedChanges;
	private SlickControls.SlickButton B_DeleteRequest;
	private SlickControls.SlickButton B_ApplyChanges;
	private SlickControls.SlickButton B_ManagePackage;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
	private PackageStabilityDropDown DD_Stability;
	private DlcDropDown DD_DLCs;
	private PackageUsageDropDown DD_Usage;
	private PackageTypeDropDown DD_PackageType;
	private SlickControls.SlickIcon I_Copy;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label L_LogReport;
	private SlickSpacer slickSpacer3;
	private System.Windows.Forms.Label L_Savegame;
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
}
