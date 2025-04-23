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
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			this.TLP_Actions = new System.Windows.Forms.TableLayoutPanel();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.L_Title = new System.Windows.Forms.Label();
			this.B_ReportIssue = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.B_AddMissingInfo = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.L_Disclaimer = new System.Windows.Forms.Label();
			this.L_English = new System.Windows.Forms.Label();
			this.TB_Note = new SlickControls.SlickTextBox();
			this.TLP_Form = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Info = new System.Windows.Forms.TableLayoutPanel();
			this.DD_Stability = new Skyve.App.UserInterface.Dropdowns.PackageStabilityDropDown();
			this.DD_DLCs = new Skyve.App.UserInterface.Dropdowns.DlcDropDown();
			this.DD_SavegameEffect = new Skyve.App.UserInterface.Dropdowns.SavegameEffectDropDown();
			this.DD_PackageType = new Skyve.App.UserInterface.Dropdowns.PackageTypeDropDown();
			this.DD_Usage = new Skyve.App.UserInterface.Dropdowns.PackageUsageDropDown();
			this.B_Apply = new SlickControls.SlickButton();
			this.L_Disclaimer2 = new System.Windows.Forms.Label();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.TLP_Description = new System.Windows.Forms.TableLayoutPanel();
			this.DD_SaveFile = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.TLP_Actions.SuspendLayout();
			this.TLP_Form.SuspendLayout();
			this.TLP_Info.SuspendLayout();
			this.TLP_Description.SuspendLayout();
			this.SuspendLayout();
			// 
			// P_SideContainer
			// 
			this.P_SideContainer.Location = new System.Drawing.Point(1204, 30);
			this.P_SideContainer.Size = new System.Drawing.Size(200, 830);
			// 
			// base_Text
			// 
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
			this.TLP_Actions.Controls.Add(this.L_Disclaimer, 0, 5);
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
			this.B_ReportIssue.ButtonText = "Send";
			this.B_ReportIssue.ColorStyle = Extensions.ColorStyle.Active;
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
			this.B_AddMissingInfo.ColorStyle = Extensions.ColorStyle.Active;
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
			// L_Disclaimer
			// 
			this.L_Disclaimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.L_Disclaimer.AutoSize = true;
			this.TLP_Actions.SetColumnSpan(this.L_Disclaimer, 4);
			this.L_Disclaimer.Location = new System.Drawing.Point(3, 811);
			this.L_Disclaimer.Name = "L_Disclaimer";
			this.L_Disclaimer.Size = new System.Drawing.Size(45, 19);
			this.L_Disclaimer.TabIndex = 18;
			this.L_Disclaimer.Text = "label1";
			// 
			// L_English
			// 
			this.L_English.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.L_English.AutoSize = true;
			this.L_English.Location = new System.Drawing.Point(1150, 236);
			this.L_English.Name = "L_English";
			this.L_English.Size = new System.Drawing.Size(45, 19);
			this.L_English.TabIndex = 20;
			this.L_English.Text = "label1";
			// 
			// TB_Note
			// 
			this.TB_Note.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.TB_Note.LabelText = "ExplainIssue";
			this.TB_Note.Location = new System.Drawing.Point(3, 159);
			this.TB_Note.MultiLine = true;
			this.TB_Note.Name = "TB_Note";
			this.TB_Note.Padding = new System.Windows.Forms.Padding(7, 21, 7, 7);
			this.TB_Note.Placeholder = "ExplainIssueInfo";
			this.TB_Note.SelectedText = "";
			this.TB_Note.SelectionLength = 0;
			this.TB_Note.SelectionStart = 0;
			this.TB_Note.Size = new System.Drawing.Size(1192, 74);
			this.TB_Note.TabIndex = 19;
			// 
			// TLP_Form
			// 
			this.TLP_Form.ColumnCount = 3;
			this.TLP_Form.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Form.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Form.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Form.Controls.Add(this.TLP_Info, 1, 0);
			this.TLP_Form.Controls.Add(this.B_Apply, 2, 3);
			this.TLP_Form.Controls.Add(this.L_Disclaimer2, 1, 3);
			this.TLP_Form.Controls.Add(this.slickSpacer2, 1, 2);
			this.TLP_Form.Controls.Add(this.TLP_Description, 1, 1);
			this.TLP_Form.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Form.Location = new System.Drawing.Point(0, 30);
			this.TLP_Form.Name = "TLP_Form";
			this.TLP_Form.RowCount = 4;
			this.TLP_Form.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Form.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Form.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Form.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Form.Size = new System.Drawing.Size(1204, 830);
			this.TLP_Form.TabIndex = 16;
			this.TLP_Form.Visible = false;
			// 
			// TLP_Info
			// 
			this.TLP_Info.AutoSize = true;
			this.TLP_Info.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Info.ColumnCount = 2;
			this.TLP_Form.SetColumnSpan(this.TLP_Info, 2);
			this.TLP_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Info.Controls.Add(this.DD_Stability, 0, 0);
			this.TLP_Info.Controls.Add(this.DD_DLCs, 0, 2);
			this.TLP_Info.Controls.Add(this.DD_SavegameEffect, 1, 0);
			this.TLP_Info.Controls.Add(this.DD_PackageType, 0, 1);
			this.TLP_Info.Controls.Add(this.DD_Usage, 1, 1);
			this.TLP_Info.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Info.Location = new System.Drawing.Point(3, 3);
			this.TLP_Info.Name = "TLP_Info";
			this.TLP_Info.RowCount = 3;
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Info.Size = new System.Drawing.Size(1198, 118);
			this.TLP_Info.TabIndex = 22;
			this.TLP_Info.Visible = false;
			// 
			// DD_Stability
			// 
			this.DD_Stability.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Stability.ItemHeight = 24;
			this.DD_Stability.Location = new System.Drawing.Point(3, 3);
			this.DD_Stability.Name = "DD_Stability";
			this.DD_Stability.Size = new System.Drawing.Size(350, 30);
			this.DD_Stability.TabIndex = 17;
			this.DD_Stability.Text = "Stability";
			// 
			// DD_DLCs
			// 
			this.DD_DLCs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_DLCs.ItemHeight = 24;
			this.DD_DLCs.Location = new System.Drawing.Point(3, 80);
			this.DD_DLCs.Name = "DD_DLCs";
			this.DD_DLCs.Size = new System.Drawing.Size(350, 35);
			this.DD_DLCs.TabIndex = 21;
			this.DD_DLCs.Text = "RequiredDLCs";
			// 
			// DD_SavegameEffect
			// 
			this.DD_SavegameEffect.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_SavegameEffect.ItemHeight = 24;
			this.DD_SavegameEffect.Location = new System.Drawing.Point(359, 3);
			this.DD_SavegameEffect.Name = "DD_SavegameEffect";
			this.DD_SavegameEffect.Size = new System.Drawing.Size(350, 30);
			this.DD_SavegameEffect.TabIndex = 19;
			this.DD_SavegameEffect.Text = "SavegameEffect";
			// 
			// DD_PackageType
			// 
			this.DD_PackageType.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PackageType.ItemHeight = 24;
			this.DD_PackageType.Location = new System.Drawing.Point(3, 39);
			this.DD_PackageType.Name = "DD_PackageType";
			this.DD_PackageType.Size = new System.Drawing.Size(350, 35);
			this.DD_PackageType.TabIndex = 18;
			this.DD_PackageType.Text = "PackageType";
			// 
			// DD_Usage
			// 
			this.DD_Usage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Usage.ItemHeight = 24;
			this.DD_Usage.Location = new System.Drawing.Point(359, 39);
			this.DD_Usage.Name = "DD_Usage";
			this.DD_Usage.Size = new System.Drawing.Size(332, 30);
			this.DD_Usage.TabIndex = 20;
			this.DD_Usage.Text = "Usage";
			// 
			// B_Apply
			// 
			this.B_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Apply.AutoSize = true;
			this.B_Apply.ButtonType = SlickControls.ButtonType.Active;
			this.B_Apply.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon3.Name = "Link";
			this.B_Apply.ImageName = dynamicIcon3;
			this.B_Apply.Location = new System.Drawing.Point(985, 795);
			this.B_Apply.Name = "B_Apply";
			this.B_Apply.Size = new System.Drawing.Size(216, 32);
			this.B_Apply.SpaceTriggersClick = true;
			this.B_Apply.TabIndex = 16;
			this.B_Apply.Text = "SendReview";
			this.B_Apply.Click += new System.EventHandler(this.B_Apply_Click);
			// 
			// L_Disclaimer2
			// 
			this.L_Disclaimer2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.L_Disclaimer2.AutoSize = true;
			this.L_Disclaimer2.Location = new System.Drawing.Point(934, 801);
			this.L_Disclaimer2.Name = "L_Disclaimer2";
			this.L_Disclaimer2.Size = new System.Drawing.Size(45, 19);
			this.L_Disclaimer2.TabIndex = 18;
			this.L_Disclaimer2.Text = "label1";
			// 
			// slickSpacer2
			// 
			this.TLP_Form.SetColumnSpan(this.slickSpacer2, 2);
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
			this.TLP_Description.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Description.ColumnCount = 1;
			this.TLP_Form.SetColumnSpan(this.TLP_Description, 2);
			this.TLP_Description.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Description.Controls.Add(this.TB_Note, 0, 1);
			this.TLP_Description.Controls.Add(this.L_English, 0, 2);
			this.TLP_Description.Controls.Add(this.DD_SaveFile, 0, 0);
			this.TLP_Description.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Description.Location = new System.Drawing.Point(3, 127);
			this.TLP_Description.Name = "TLP_Description";
			this.TLP_Description.RowCount = 3;
			this.TLP_Description.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Description.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Description.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Description.Size = new System.Drawing.Size(1198, 94);
			this.TLP_Description.TabIndex = 20;
			// 
			// DD_SaveFile
			// 
			this.DD_SaveFile.AllowDrop = true;
			this.DD_SaveFile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_SaveFile.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_SaveFile.ImageName = "SaveGame";
			this.DD_SaveFile.Location = new System.Drawing.Point(3, 3);
			this.DD_SaveFile.Name = "DD_SaveFile";
			this.DD_SaveFile.Size = new System.Drawing.Size(1192, 150);
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
			this.TLP_Info.ResumeLayout(false);
			this.TLP_Description.ResumeLayout(false);
			this.TLP_Description.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel TLP_Actions;
	private BigSelectionOptionControl B_AddMissingInfo;
	private System.Windows.Forms.TableLayoutPanel TLP_Form;
	private SlickControls.SlickButton B_Apply;
	private SlickControls.SlickTextBox TB_Note;
	private System.Windows.Forms.Label L_Disclaimer;
	private System.Windows.Forms.Label L_English;
	private SlickSpacer slickSpacer2;
	private System.Windows.Forms.TableLayoutPanel TLP_Description;
	private DragAndDropControl DD_SaveFile;
	private SlickSpacer slickSpacer1;
	private System.Windows.Forms.Label L_Title;
	private BigSelectionOptionControl B_ReportIssue;
	private System.Windows.Forms.Label L_Disclaimer2;
	private PackageStabilityDropDown DD_Stability;
	private PackageTypeDropDown DD_PackageType;
	private PackageUsageDropDown DD_Usage;
	private DlcDropDown DD_DLCs;
	private SavegameEffectDropDown DD_SavegameEffect;
	private System.Windows.Forms.TableLayoutPanel TLP_Info;
}
