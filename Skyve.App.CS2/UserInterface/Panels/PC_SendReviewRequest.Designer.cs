using Skyve.App.CS2.UserInterface.Generic;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Dropdowns;
using Skyve.App.UserInterface.Generic;

namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_SendReviewRequest
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
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			this.TLP_Actions = new System.Windows.Forms.TableLayoutPanel();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.L_Title = new System.Windows.Forms.Label();
			this.B_ReportIssue = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.B_AddMissingInfo = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.L_English = new System.Windows.Forms.Label();
			this.TB_Note = new SlickControls.SlickTextBox();
			this.TLP_Form = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_LogReport = new SlickControls.RoundedGroupTableLayoutPanel();
			this.logReportControl = new Skyve.App.CS2.UserInterface.Generic.LogReportControl();
			this.B_Apply = new SlickControls.SlickButton();
			this.TLP_Changes = new SlickControls.RoundedGroupTableLayoutPanel();
			this.DD_Stability = new Skyve.App.UserInterface.Dropdowns.PackageStabilityDropDown();
			this.DD_PackageType = new Skyve.App.UserInterface.Dropdowns.PackageTypeDropDown();
			this.DD_Usage = new Skyve.App.UserInterface.Dropdowns.PackageUsageDropDown();
			this.DD_DLCs = new Skyve.App.UserInterface.Dropdowns.DlcDropDown();
			this.DD_SavegameEffect = new Skyve.App.UserInterface.Dropdowns.SavegameEffectDropDown();
			this.L_Disclaimer = new System.Windows.Forms.Label();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.TLP_Description = new System.Windows.Forms.TableLayoutPanel();
			this.roundedGroupTableLayoutPanel1 = new SlickControls.RoundedGroupTableLayoutPanel();
			this.DD_SaveFile = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.TLP_Actions.SuspendLayout();
			this.TLP_Form.SuspendLayout();
			this.TLP_LogReport.SuspendLayout();
			this.TLP_Changes.SuspendLayout();
			this.TLP_Description.SuspendLayout();
			this.roundedGroupTableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// P_SideContainer
			// 
			this.P_SideContainer.Location = new System.Drawing.Point(1204, 30);
			this.P_SideContainer.Size = new System.Drawing.Size(200, 830);
			// 
			// base_Text
			// 
			this.base_Text.ButtonType = SlickControls.ButtonType.Normal;
			this.base_Text.Size = new System.Drawing.Size(150, 39);
			// 
			// TLP_Actions
			// 
			this.TLP_Actions.ColumnCount = 4;
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Actions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Actions.Controls.Add(this.slickSpacer1, 1, 2);
			this.TLP_Actions.Controls.Add(this.L_Title, 0, 1);
			this.TLP_Actions.Controls.Add(this.B_ReportIssue, 1, 4);
			this.TLP_Actions.Controls.Add(this.B_AddMissingInfo, 2, 4);
			this.TLP_Actions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Actions.Location = new System.Drawing.Point(0, 30);
			this.TLP_Actions.Name = "TLP_Actions";
			this.TLP_Actions.RowCount = 6;
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Actions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.TLP_Actions.Size = new System.Drawing.Size(1204, 830);
			this.TLP_Actions.TabIndex = 0;
			// 
			// slickSpacer1
			// 
			this.TLP_Actions.SetColumnSpan(this.slickSpacer1, 2);
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(432, 116);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(340, 2);
			this.slickSpacer1.TabIndex = 16;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// L_Title
			// 
			this.L_Title.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.L_Title.AutoSize = true;
			this.TLP_Actions.SetColumnSpan(this.L_Title, 4);
			this.L_Title.Location = new System.Drawing.Point(579, 94);
			this.L_Title.Name = "L_Title";
			this.L_Title.Size = new System.Drawing.Size(45, 19);
			this.L_Title.TabIndex = 15;
			this.L_Title.Text = "label1";
			// 
			// B_ReportIssue
			// 
			this.B_ReportIssue.ButtonText = "Continue";
			this.B_ReportIssue.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_ReportIssue.Highlighted = true;
			dynamicIcon1.Name = "Remarks";
			this.B_ReportIssue.ImageName = dynamicIcon1;
			this.B_ReportIssue.Location = new System.Drawing.Point(432, 218);
			this.B_ReportIssue.Name = "B_ReportIssue";
			this.B_ReportIssue.Size = new System.Drawing.Size(154, 326);
			this.B_ReportIssue.TabIndex = 0;
			this.B_ReportIssue.Text = "ReportIssueDesc";
			this.B_ReportIssue.Title = "ReportIssue";
			this.B_ReportIssue.Click += new System.EventHandler(this.B_ReportIssue_Click);
			// 
			// B_AddMissingInfo
			// 
			this.B_AddMissingInfo.ButtonText = "Continue";
			this.B_AddMissingInfo.ColorStyle = Extensions.ColorStyle.Green;
			this.B_AddMissingInfo.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon2.Name = "Content";
			this.B_AddMissingInfo.ImageName = dynamicIcon2;
			this.B_AddMissingInfo.Location = new System.Drawing.Point(592, 218);
			this.B_AddMissingInfo.Name = "B_AddMissingInfo";
			this.B_AddMissingInfo.Size = new System.Drawing.Size(180, 326);
			this.B_AddMissingInfo.TabIndex = 1;
			this.B_AddMissingInfo.Text = "AddInfoDesc";
			this.B_AddMissingInfo.Title = "AddInfo";
			this.B_AddMissingInfo.Click += new System.EventHandler(this.B_AddMissingInfo_Click);
			// 
			// L_English
			// 
			this.L_English.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.L_English.AutoSize = true;
			this.L_English.Location = new System.Drawing.Point(733, 323);
			this.L_English.Name = "L_English";
			this.L_English.Size = new System.Drawing.Size(45, 19);
			this.L_English.TabIndex = 20;
			this.L_English.Text = "label1";
			// 
			// TB_Note
			// 
			this.TB_Note.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_Note.LabelText = "ExplainIssue";
			this.TB_Note.Location = new System.Drawing.Point(3, 246);
			this.TB_Note.MultiLine = true;
			this.TB_Note.Name = "TB_Note";
			this.TB_Note.Padding = new System.Windows.Forms.Padding(7, 21, 7, 7);
			this.TB_Note.Placeholder = "ExplainIssueInfo";
			this.TB_Note.SelectedText = "";
			this.TB_Note.SelectionLength = 0;
			this.TB_Note.SelectionStart = 0;
			this.TB_Note.Size = new System.Drawing.Size(775, 74);
			this.TB_Note.TabIndex = 0;
			// 
			// TLP_Form
			// 
			this.TLP_Form.ColumnCount = 3;
			this.TLP_Form.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
			this.TLP_Form.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.5F));
			this.TLP_Form.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Form.Controls.Add(this.TLP_LogReport, 0, 0);
			this.TLP_Form.Controls.Add(this.B_Apply, 2, 3);
			this.TLP_Form.Controls.Add(this.TLP_Changes, 0, 1);
			this.TLP_Form.Controls.Add(this.L_Disclaimer, 0, 3);
			this.TLP_Form.Controls.Add(this.slickSpacer2, 0, 2);
			this.TLP_Form.Controls.Add(this.TLP_Description, 1, 0);
			this.TLP_Form.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Form.Location = new System.Drawing.Point(0, 30);
			this.TLP_Form.Name = "TLP_Form";
			this.TLP_Form.RowCount = 4;
			this.TLP_Form.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Form.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Form.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Form.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Form.Size = new System.Drawing.Size(1204, 830);
			this.TLP_Form.TabIndex = 0;
			this.TLP_Form.Visible = false;
			// 
			// TLP_LogReport
			// 
			this.TLP_LogReport.AddOutline = true;
			this.TLP_LogReport.AddShadow = true;
			this.TLP_LogReport.AutoSize = true;
			this.TLP_LogReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_LogReport.ColumnCount = 1;
			this.TLP_LogReport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_LogReport.Controls.Add(this.logReportControl, 0, 0);
			this.TLP_LogReport.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon4.Name = "Log";
			this.TLP_LogReport.ImageName = dynamicIcon4;
			this.TLP_LogReport.Info = "YourLogReportInfo";
			this.TLP_LogReport.Location = new System.Drawing.Point(3, 3);
			this.TLP_LogReport.Name = "TLP_LogReport";
			this.TLP_LogReport.Padding = new System.Windows.Forms.Padding(21, 60, 21, 21);
			this.TLP_LogReport.RowCount = 1;
			this.TLP_LogReport.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_LogReport.Size = new System.Drawing.Size(411, 165);
			this.TLP_LogReport.TabIndex = 18;
			this.TLP_LogReport.Text = "Log Report";
			// 
			// logReportControl
			// 
			this.logReportControl.Cursor = System.Windows.Forms.Cursors.Hand;
			this.logReportControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.logReportControl.Enabled = false;
			dynamicIcon3.Name = "File";
			this.logReportControl.ImageName = dynamicIcon3;
			this.logReportControl.Location = new System.Drawing.Point(24, 63);
			this.logReportControl.Name = "logReportControl";
			this.logReportControl.Size = new System.Drawing.Size(363, 78);
			this.logReportControl.TabIndex = 17;
			// 
			// B_Apply
			// 
			this.B_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Apply.AutoSize = true;
			this.B_Apply.ButtonType = SlickControls.ButtonType.Active;
			this.B_Apply.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "Link";
			this.B_Apply.ImageName = dynamicIcon5;
			this.B_Apply.Location = new System.Drawing.Point(985, 795);
			this.B_Apply.Name = "B_Apply";
			this.B_Apply.Size = new System.Drawing.Size(216, 32);
			this.B_Apply.SpaceTriggersClick = true;
			this.B_Apply.TabIndex = 16;
			this.B_Apply.Text = "SendReview";
			this.B_Apply.Click += new System.EventHandler(this.B_Apply_Click);
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
			dynamicIcon6.Name = "RequestReview";
			this.TLP_Changes.ImageName = dynamicIcon6;
			this.TLP_Changes.Location = new System.Drawing.Point(3, 174);
			this.TLP_Changes.Name = "TLP_Changes";
			this.TLP_Changes.Padding = new System.Windows.Forms.Padding(21, 60, 21, 21);
			this.TLP_Changes.RowCount = 5;
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Changes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Changes.Size = new System.Drawing.Size(411, 271);
			this.TLP_Changes.TabIndex = 17;
			this.TLP_Changes.Text = "Proposed Changes";
			this.TLP_Changes.Visible = false;
			// 
			// DD_Stability
			// 
			this.DD_Stability.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Stability.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Stability.ItemHeight = 24;
			this.DD_Stability.Location = new System.Drawing.Point(24, 63);
			this.DD_Stability.Name = "DD_Stability";
			this.DD_Stability.Size = new System.Drawing.Size(363, 30);
			this.DD_Stability.TabIndex = 0;
			this.DD_Stability.Text = "Stability";
			// 
			// DD_PackageType
			// 
			this.DD_PackageType.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PackageType.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_PackageType.ItemHeight = 24;
			this.DD_PackageType.Location = new System.Drawing.Point(24, 99);
			this.DD_PackageType.Name = "DD_PackageType";
			this.DD_PackageType.Size = new System.Drawing.Size(363, 35);
			this.DD_PackageType.TabIndex = 1;
			this.DD_PackageType.Text = "PackageType";
			// 
			// DD_Usage
			// 
			this.DD_Usage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Usage.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Usage.ItemHeight = 24;
			this.DD_Usage.Location = new System.Drawing.Point(24, 140);
			this.DD_Usage.Name = "DD_Usage";
			this.DD_Usage.Size = new System.Drawing.Size(363, 30);
			this.DD_Usage.TabIndex = 2;
			this.DD_Usage.Text = "Usage";
			// 
			// DD_DLCs
			// 
			this.DD_DLCs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_DLCs.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_DLCs.ItemHeight = 24;
			this.DD_DLCs.Location = new System.Drawing.Point(24, 176);
			this.DD_DLCs.Name = "DD_DLCs";
			this.DD_DLCs.Size = new System.Drawing.Size(363, 35);
			this.DD_DLCs.TabIndex = 3;
			this.DD_DLCs.Text = "RequiredDLCs";
			// 
			// DD_SavegameEffect
			// 
			this.DD_SavegameEffect.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_SavegameEffect.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_SavegameEffect.ItemHeight = 24;
			this.DD_SavegameEffect.Location = new System.Drawing.Point(24, 217);
			this.DD_SavegameEffect.Name = "DD_SavegameEffect";
			this.DD_SavegameEffect.Size = new System.Drawing.Size(363, 30);
			this.DD_SavegameEffect.TabIndex = 4;
			this.DD_SavegameEffect.Text = "SavegameEffect";
			// 
			// L_Disclaimer
			// 
			this.L_Disclaimer.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.L_Disclaimer.AutoSize = true;
			this.TLP_Form.SetColumnSpan(this.L_Disclaimer, 2);
			this.L_Disclaimer.Location = new System.Drawing.Point(933, 801);
			this.L_Disclaimer.Name = "L_Disclaimer";
			this.L_Disclaimer.Size = new System.Drawing.Size(45, 19);
			this.L_Disclaimer.TabIndex = 18;
			this.L_Disclaimer.Text = "label1";
			// 
			// slickSpacer2
			// 
			this.TLP_Form.SetColumnSpan(this.slickSpacer2, 3);
			this.slickSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer2.Location = new System.Drawing.Point(3, 781);
			this.slickSpacer2.Name = "slickSpacer2";
			this.slickSpacer2.Size = new System.Drawing.Size(1198, 8);
			this.slickSpacer2.TabIndex = 19;
			this.slickSpacer2.TabStop = false;
			this.slickSpacer2.Text = "slickSpacer2";
			// 
			// TLP_Description
			// 
			this.TLP_Description.AutoSize = true;
			this.TLP_Description.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Description.ColumnCount = 1;
			this.TLP_Form.SetColumnSpan(this.TLP_Description, 2);
			this.TLP_Description.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Description.Controls.Add(this.roundedGroupTableLayoutPanel1, 0, 0);
			this.TLP_Description.Controls.Add(this.TB_Note, 0, 1);
			this.TLP_Description.Controls.Add(this.L_English, 0, 2);
			this.TLP_Description.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Description.Location = new System.Drawing.Point(420, 3);
			this.TLP_Description.Name = "TLP_Description";
			this.TLP_Description.RowCount = 3;
			this.TLP_Form.SetRowSpan(this.TLP_Description, 2);
			this.TLP_Description.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Description.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Description.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Description.Size = new System.Drawing.Size(781, 342);
			this.TLP_Description.TabIndex = 0;
			// 
			// roundedGroupTableLayoutPanel1
			// 
			this.roundedGroupTableLayoutPanel1.AddOutline = true;
			this.roundedGroupTableLayoutPanel1.AddShadow = true;
			this.roundedGroupTableLayoutPanel1.AutoSize = true;
			this.roundedGroupTableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedGroupTableLayoutPanel1.ColumnCount = 1;
			this.roundedGroupTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.DD_SaveFile, 0, 0);
			this.roundedGroupTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon7.Name = "Savegame";
			this.roundedGroupTableLayoutPanel1.ImageName = dynamicIcon7;
			this.roundedGroupTableLayoutPanel1.Info = "SendSavegameInfo";
			this.roundedGroupTableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.roundedGroupTableLayoutPanel1.Name = "roundedGroupTableLayoutPanel1";
			this.roundedGroupTableLayoutPanel1.Padding = new System.Windows.Forms.Padding(21, 60, 21, 21);
			this.roundedGroupTableLayoutPanel1.RowCount = 1;
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.Size = new System.Drawing.Size(775, 237);
			this.roundedGroupTableLayoutPanel1.TabIndex = 19;
			this.roundedGroupTableLayoutPanel1.Text = "Savegame";
			// 
			// DD_SaveFile
			// 
			this.DD_SaveFile.AllowDrop = true;
			this.DD_SaveFile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_SaveFile.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_SaveFile.ImageName = "SaveGame";
			this.DD_SaveFile.Location = new System.Drawing.Point(24, 63);
			this.DD_SaveFile.Name = "DD_SaveFile";
			this.DD_SaveFile.Size = new System.Drawing.Size(727, 150);
			this.DD_SaveFile.TabIndex = 21;
			this.DD_SaveFile.Text = "ReviewSaveFileInfo";
			this.DD_SaveFile.ValidExtensions = new string[] {
        ".cok"};
			this.DD_SaveFile.FileSelected += new System.Action<string>(this.DD_SaveFile_FileSelected);
			// 
			// PC_SendReviewRequest
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TLP_Form);
			this.Controls.Add(this.TLP_Actions);
			this.Name = "PC_SendReviewRequest";
			this.Size = new System.Drawing.Size(1404, 860);
			this.Controls.SetChildIndex(this.P_SideContainer, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.TLP_Actions, 0);
			this.Controls.SetChildIndex(this.TLP_Form, 0);
			this.TLP_Actions.ResumeLayout(false);
			this.TLP_Actions.PerformLayout();
			this.TLP_Form.ResumeLayout(false);
			this.TLP_Form.PerformLayout();
			this.TLP_LogReport.ResumeLayout(false);
			this.TLP_Changes.ResumeLayout(false);
			this.TLP_Description.ResumeLayout(false);
			this.TLP_Description.PerformLayout();
			this.roundedGroupTableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel TLP_Actions;
	private BigSelectionOptionControl B_AddMissingInfo;
	private System.Windows.Forms.TableLayoutPanel TLP_Form;
	private SlickControls.SlickButton B_Apply;
	private SlickControls.SlickTextBox TB_Note;
	private System.Windows.Forms.Label L_English;
	private SlickSpacer slickSpacer2;
	private System.Windows.Forms.TableLayoutPanel TLP_Description;
	private DragAndDropControl DD_SaveFile;
	private SlickSpacer slickSpacer1;
	private System.Windows.Forms.Label L_Title;
	private BigSelectionOptionControl B_ReportIssue;
	private System.Windows.Forms.Label L_Disclaimer;
	private RoundedGroupTableLayoutPanel TLP_LogReport;
	private RoundedGroupTableLayoutPanel TLP_Changes;
	private PackageStabilityDropDown DD_Stability;
	private PackageTypeDropDown DD_PackageType;
	private PackageUsageDropDown DD_Usage;
	private DlcDropDown DD_DLCs;
	private SavegameEffectDropDown DD_SavegameEffect;
	private LogReportControl logReportControl;
	private RoundedGroupTableLayoutPanel roundedGroupTableLayoutPanel1;
}
