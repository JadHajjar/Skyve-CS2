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
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			this.TLP_Main = new SlickControls.RoundedTableLayoutPanel();
			this.I_Copy = new SlickControls.SlickIcon();
			this.L_Desc = new System.Windows.Forms.Label();
			this.L_Note = new System.Windows.Forms.Label();
			this.L_ProposedChanges = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.DD_Stability = new Skyve.App.UserInterface.Dropdowns.PackageStabilityDropDown();
			this.DD_Usage = new Skyve.App.UserInterface.Dropdowns.PackageUsageDropDown();
			this.DD_PackageType = new Skyve.App.UserInterface.Dropdowns.PackageTypeDropDown();
			this.DD_DLCs = new Skyve.App.UserInterface.Dropdowns.DlcDropDown();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_ManagePackage = new SlickControls.SlickButton();
			this.B_ApplyChanges = new SlickControls.SlickButton();
			this.B_DeleteRequest = new SlickControls.SlickButton();
			this.L_LogReport = new System.Windows.Forms.Label();
			this.slickSpacer3 = new SlickControls.SlickSpacer();
			this.TLP_Main.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(196, 39);
			this.base_Text.Text = "Review Request Info";
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.Controls.Add(this.slickSpacer3, 0, 4);
			this.TLP_Main.Controls.Add(this.I_Copy, 1, 0);
			this.TLP_Main.Controls.Add(this.L_Desc, 0, 0);
			this.TLP_Main.Controls.Add(this.L_Note, 0, 1);
			this.TLP_Main.Controls.Add(this.L_ProposedChanges, 0, 5);
			this.TLP_Main.Controls.Add(this.tableLayoutPanel3, 0, 7);
			this.TLP_Main.Controls.Add(this.slickSpacer2, 0, 8);
			this.TLP_Main.Controls.Add(this.tableLayoutPanel1, 0, 9);
			this.TLP_Main.Controls.Add(this.L_LogReport, 0, 2);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(0, 30);
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
			this.TLP_Main.Size = new System.Drawing.Size(796, 623);
			this.TLP_Main.TabIndex = 2;
			// 
			// I_Copy
			// 
			this.I_Copy.ActiveColor = null;
			this.I_Copy.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "Copy";
			this.I_Copy.ImageName = dynamicIcon5;
			this.I_Copy.Location = new System.Drawing.Point(770, 3);
			this.I_Copy.Name = "Copy";
			this.TLP_Main.SetRowSpan(this.I_Copy, 2);
			this.I_Copy.Size = new System.Drawing.Size(23, 26);
			this.I_Copy.TabIndex = 4;
			this.I_Copy.Click += new System.EventHandler(this.slickIcon1_Click);
			// 
			// L_Desc
			// 
			this.L_Desc.AutoSize = true;
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
			this.TLP_Main.SetColumnSpan(this.L_ProposedChanges, 2);
			this.L_ProposedChanges.Location = new System.Drawing.Point(3, 64);
			this.L_ProposedChanges.Name = "L_ProposedChanges";
			this.L_ProposedChanges.Size = new System.Drawing.Size(124, 19);
			this.L_ProposedChanges.TabIndex = 1;
			this.L_ProposedChanges.Text = "Proposed Changes";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 3;
			this.TLP_Main.SetColumnSpan(this.tableLayoutPanel3, 2);
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
			this.tableLayoutPanel3.Size = new System.Drawing.Size(796, 473);
			this.tableLayoutPanel3.TabIndex = 3;
			this.tableLayoutPanel3.Visible = false;
			// 
			// DD_Stability
			// 
			this.DD_Stability.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Stability.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Stability.Location = new System.Drawing.Point(3, 3);
			this.DD_Stability.Name = "DD_Stability";
			this.DD_Stability.Size = new System.Drawing.Size(325, 56);
			this.DD_Stability.TabIndex = 18;
			this.DD_Stability.Text = "Stability";
			// 
			// DD_Usage
			// 
			this.DD_Usage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Usage.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Usage.Location = new System.Drawing.Point(466, 3);
			this.DD_Usage.Name = "DD_Usage";
			this.DD_Usage.Size = new System.Drawing.Size(327, 54);
			this.DD_Usage.TabIndex = 20;
			this.DD_Usage.Text = "Usage";
			// 
			// DD_PackageType
			// 
			this.DD_PackageType.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PackageType.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_PackageType.Location = new System.Drawing.Point(466, 65);
			this.DD_PackageType.Name = "DD_PackageType";
			this.DD_PackageType.Size = new System.Drawing.Size(327, 54);
			this.DD_PackageType.TabIndex = 21;
			this.DD_PackageType.Text = "PackageType";
			// 
			// DD_DLCs
			// 
			this.DD_DLCs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_DLCs.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_DLCs.Location = new System.Drawing.Point(3, 65);
			this.DD_DLCs.Name = "DD_DLCs";
			this.DD_DLCs.Size = new System.Drawing.Size(325, 54);
			this.DD_DLCs.TabIndex = 19;
			this.DD_DLCs.Text = "RequiredDLCs";
			// 
			// slickSpacer2
			// 
			this.TLP_Main.SetColumnSpan(this.slickSpacer2, 2);
			this.slickSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer2.Location = new System.Drawing.Point(3, 559);
			this.slickSpacer2.Name = "slickSpacer2";
			this.slickSpacer2.Size = new System.Drawing.Size(790, 23);
			this.slickSpacer2.TabIndex = 5;
			this.slickSpacer2.TabStop = false;
			this.slickSpacer2.Text = "slickSpacer2";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.TLP_Main.SetColumnSpan(this.tableLayoutPanel1, 2);
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.B_ManagePackage, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_ApplyChanges, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_DeleteRequest, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 585);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(796, 38);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// B_ManagePackage
			// 
			this.B_ManagePackage.AutoSize = true;
			this.B_ManagePackage.ButtonType = SlickControls.ButtonType.Active;
			this.B_ManagePackage.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon6.Name = "Link";
			this.B_ManagePackage.ImageName = dynamicIcon6;
			this.B_ManagePackage.Location = new System.Drawing.Point(642, 3);
			this.B_ManagePackage.Name = "B_ManagePackage";
			this.B_ManagePackage.Size = new System.Drawing.Size(151, 32);
			this.B_ManagePackage.SpaceTriggersClick = true;
			this.B_ManagePackage.TabIndex = 0;
			this.B_ManagePackage.Text = "ManagePackage";
			this.B_ManagePackage.Click += new System.EventHandler(this.B_ManagePackage_Click);
			// 
			// B_ApplyChanges
			// 
			this.B_ApplyChanges.AutoSize = true;
			this.B_ApplyChanges.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon7.Name = "Link";
			this.B_ApplyChanges.ImageName = dynamicIcon7;
			this.B_ApplyChanges.Location = new System.Drawing.Point(428, 3);
			this.B_ApplyChanges.Name = "B_ApplyChanges";
			this.B_ApplyChanges.Size = new System.Drawing.Size(208, 32);
			this.B_ApplyChanges.SpaceTriggersClick = true;
			this.B_ApplyChanges.TabIndex = 1;
			this.B_ApplyChanges.Text = "ApplyRequestedChanges";
			this.B_ApplyChanges.Click += new System.EventHandler(this.B_ApplyChanges_Click);
			// 
			// B_DeleteRequest
			// 
			this.B_DeleteRequest.AutoSize = true;
			this.B_DeleteRequest.ButtonType = SlickControls.ButtonType.Hidden;
			this.B_DeleteRequest.ColorStyle = Extensions.ColorStyle.Red;
			this.B_DeleteRequest.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon8.Name = "Trash";
			this.B_DeleteRequest.ImageName = dynamicIcon8;
			this.B_DeleteRequest.Location = new System.Drawing.Point(3, 3);
			this.B_DeleteRequest.Name = "B_DeleteRequest";
			this.B_DeleteRequest.Size = new System.Drawing.Size(161, 32);
			this.B_DeleteRequest.SpaceTriggersClick = true;
			this.B_DeleteRequest.TabIndex = 2;
			this.B_DeleteRequest.Text = "DeleteRequests";
			this.B_DeleteRequest.Click += new System.EventHandler(this.B_DeleteRequest_Click);
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
			// slickSpacer3
			// 
			this.slickSpacer3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.TLP_Main.SetColumnSpan(this.slickSpacer3, 2);
			this.slickSpacer3.Location = new System.Drawing.Point(3, 60);
			this.slickSpacer3.Name = "slickSpacer3";
			this.slickSpacer3.Size = new System.Drawing.Size(790, 1);
			this.slickSpacer3.TabIndex = 7;
			this.slickSpacer3.TabStop = false;
			this.slickSpacer3.Text = "slickSpacer3";
			// 
			// PC_ViewReviewRequest
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TLP_Main);
			this.Name = "PC_ViewReviewRequest";
			this.Text = "Review Request Info";
			this.Controls.SetChildIndex(this.P_SideContainer, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.TLP_Main, 0);
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
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
	private SlickSpacer slickSpacer2;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label L_LogReport;
	private SlickSpacer slickSpacer3;
}
