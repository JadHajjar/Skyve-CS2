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
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			this.tableLayoutPanel1 = new SlickControls.RoundedTableLayoutPanel();
			this.I_Copy = new SlickControls.SlickIcon();
			this.L_Desc = new System.Windows.Forms.Label();
			this.L_Note = new System.Windows.Forms.Label();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.L_ProposedChanges = new System.Windows.Forms.Label();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.DD_Stability = new Skyve.App.UserInterface.Dropdowns.PackageStabilityDropDown();
			this.DD_Usage = new Skyve.App.UserInterface.Dropdowns.PackageUsageDropDown();
			this.DD_PackageType = new Skyve.App.UserInterface.Dropdowns.PackageTypeDropDown();
			this.DD_DLCs = new Skyve.App.UserInterface.Dropdowns.DlcDropDown();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.roundedGroupTableLayoutPanel1 = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_DeleteRequest = new SlickControls.SlickButton();
			this.B_ApplyChanges = new SlickControls.SlickButton();
			this.B_ManagePackage = new SlickControls.SlickButton();
			this.TLP_Info = new SlickControls.RoundedGroupTableLayoutPanel();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.roundedGroupTableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.ButtonType = SlickControls.ButtonType.Normal;
			this.base_Text.ColorStyle = Extensions.ColorStyle.Active;
			this.base_Text.Size = new System.Drawing.Size(196, 39);
			this.base_Text.Text = "Review Request Info";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.I_Copy, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.L_Desc, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.slickSpacer1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.L_Note, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.L_ProposedChanges, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 5);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(205, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 6;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(573, 403);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// I_Copy
			// 
			this.I_Copy.ActiveColor = null;
			this.I_Copy.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "I_Copy";
			this.I_Copy.ImageName = dynamicIcon1;
			this.I_Copy.Location = new System.Drawing.Point(547, 3);
			this.I_Copy.Name = "I_Copy";
			this.tableLayoutPanel1.SetRowSpan(this.I_Copy, 2);
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
			// slickSpacer1
			// 
			this.slickSpacer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.slickSpacer1, 2);
			this.slickSpacer1.Location = new System.Drawing.Point(3, 41);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(567, 1);
			this.slickSpacer1.TabIndex = 2;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// L_ProposedChanges
			// 
			this.L_ProposedChanges.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.L_ProposedChanges, 2);
			this.L_ProposedChanges.Location = new System.Drawing.Point(3, 45);
			this.L_ProposedChanges.Name = "L_ProposedChanges";
			this.L_ProposedChanges.Size = new System.Drawing.Size(124, 19);
			this.L_ProposedChanges.TabIndex = 1;
			this.L_ProposedChanges.Text = "Proposed Changes";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 2);
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.DD_Stability, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.DD_Usage, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.DD_PackageType, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.DD_DLCs, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 64);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(573, 339);
			this.tableLayoutPanel3.TabIndex = 3;
			this.tableLayoutPanel3.Visible = false;
			// 
			// DD_Stability
			// 
			this.DD_Stability.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Stability.Location = new System.Drawing.Point(3, 3);
			this.DD_Stability.Name = "DD_Stability";
			this.DD_Stability.Size = new System.Drawing.Size(280, 56);
			this.DD_Stability.TabIndex = 18;
			this.DD_Stability.Text = "Stability";
			// 
			// DD_Usage
			// 
			this.DD_Usage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DD_Usage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Usage.Location = new System.Drawing.Point(289, 3);
			this.DD_Usage.Name = "DD_Usage";
			this.DD_Usage.Size = new System.Drawing.Size(281, 54);
			this.DD_Usage.TabIndex = 20;
			this.DD_Usage.Text = "Usage";
			// 
			// DD_PackageType
			// 
			this.DD_PackageType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DD_PackageType.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PackageType.Location = new System.Drawing.Point(289, 65);
			this.DD_PackageType.Name = "DD_PackageType";
			this.DD_PackageType.Size = new System.Drawing.Size(281, 54);
			this.DD_PackageType.TabIndex = 21;
			this.DD_PackageType.Text = "PackageType";
			// 
			// DD_DLCs
			// 
			this.DD_DLCs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_DLCs.Location = new System.Drawing.Point(3, 65);
			this.DD_DLCs.Name = "DD_DLCs";
			this.DD_DLCs.Size = new System.Drawing.Size(280, 54);
			this.DD_DLCs.TabIndex = 19;
			this.DD_DLCs.Text = "RequiredDLCs";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.roundedGroupTableLayoutPanel1, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.TLP_Info, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 30);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 403);
			this.tableLayoutPanel2.TabIndex = 3;
			// 
			// roundedGroupTableLayoutPanel1
			// 
			this.roundedGroupTableLayoutPanel1.AddOutline = true;
			this.roundedGroupTableLayoutPanel1.AutoSize = true;
			this.roundedGroupTableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedGroupTableLayoutPanel1.ColumnCount = 1;
			this.roundedGroupTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.roundedGroupTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.B_DeleteRequest, 0, 2);
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.B_ApplyChanges, 0, 1);
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.B_ManagePackage, 0, 0);
			this.roundedGroupTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon5.Name = "I_Actions";
			this.roundedGroupTableLayoutPanel1.ImageName = dynamicIcon5;
			this.roundedGroupTableLayoutPanel1.Location = new System.Drawing.Point(3, 63);
			this.roundedGroupTableLayoutPanel1.Name = "roundedGroupTableLayoutPanel1";
			this.roundedGroupTableLayoutPanel1.Padding = new System.Windows.Forms.Padding(8, 46, 8, 8);
			this.roundedGroupTableLayoutPanel1.RowCount = 3;
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.Size = new System.Drawing.Size(194, 168);
			this.roundedGroupTableLayoutPanel1.TabIndex = 2;
			this.roundedGroupTableLayoutPanel1.Text = "Actions";
			// 
			// B_DeleteRequest
			// 
			this.B_DeleteRequest.AutoSize = true;
			this.B_DeleteRequest.ColorStyle = Extensions.ColorStyle.Red;
			this.B_DeleteRequest.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_DeleteRequest.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon2.Name = "I_Disposable";
			this.B_DeleteRequest.ImageName = dynamicIcon2;
			this.B_DeleteRequest.Location = new System.Drawing.Point(11, 125);
			this.B_DeleteRequest.Name = "B_DeleteRequest";
			this.B_DeleteRequest.Size = new System.Drawing.Size(172, 32);
			this.B_DeleteRequest.SpaceTriggersClick = true;
			this.B_DeleteRequest.TabIndex = 2;
			this.B_DeleteRequest.Text = "DeleteRequests";
			this.B_DeleteRequest.Click += new System.EventHandler(this.B_DeleteRequest_Click);
			// 
			// B_ApplyChanges
			// 
			this.B_ApplyChanges.AutoSize = true;
			this.B_ApplyChanges.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_ApplyChanges.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon3.Name = "I_Link";
			this.B_ApplyChanges.ImageName = dynamicIcon3;
			this.B_ApplyChanges.Location = new System.Drawing.Point(11, 87);
			this.B_ApplyChanges.Name = "B_ApplyChanges";
			this.B_ApplyChanges.Size = new System.Drawing.Size(172, 32);
			this.B_ApplyChanges.SpaceTriggersClick = true;
			this.B_ApplyChanges.TabIndex = 1;
			this.B_ApplyChanges.Text = "ApplyRequestedChanges";
			this.B_ApplyChanges.Click += new System.EventHandler(this.B_ApplyChanges_Click);
			// 
			// B_ManagePackage
			// 
			this.B_ManagePackage.AutoSize = true;
			this.B_ManagePackage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_ManagePackage.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon4.Name = "I_Link";
			this.B_ManagePackage.ImageName = dynamicIcon4;
			this.B_ManagePackage.Location = new System.Drawing.Point(11, 49);
			this.B_ManagePackage.Name = "B_ManagePackage";
			this.B_ManagePackage.Size = new System.Drawing.Size(172, 32);
			this.B_ManagePackage.SpaceTriggersClick = true;
			this.B_ManagePackage.TabIndex = 0;
			this.B_ManagePackage.Text = "ManagePackage";
			this.B_ManagePackage.Click += new System.EventHandler(this.B_ManagePackage_Click);
			// 
			// TLP_Info
			// 
			this.TLP_Info.AddOutline = true;
			this.TLP_Info.AutoSize = true;
			this.TLP_Info.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Info.ColumnCount = 1;
			this.TLP_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Info.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Info.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon6.Name = "I_Info";
			this.TLP_Info.ImageName = dynamicIcon6;
			this.TLP_Info.Location = new System.Drawing.Point(3, 3);
			this.TLP_Info.Name = "TLP_Info";
			this.TLP_Info.Padding = new System.Windows.Forms.Padding(8, 46, 8, 8);
			this.TLP_Info.RowCount = 3;
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Info.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Info.Size = new System.Drawing.Size(194, 54);
			this.TLP_Info.TabIndex = 1;
			this.TLP_Info.Text = "Info";
			// 
			// PC_ViewReviewRequest
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Name = "PC_ViewReviewRequest";
			this.Text = "Review Request Info";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.roundedGroupTableLayoutPanel1.ResumeLayout(false);
			this.roundedGroupTableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private SlickControls.RoundedTableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label L_Desc;
	private System.Windows.Forms.Label L_Note;
	private System.Windows.Forms.Label L_ProposedChanges;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	private SlickControls.RoundedGroupTableLayoutPanel roundedGroupTableLayoutPanel1;
	private SlickControls.RoundedGroupTableLayoutPanel TLP_Info;
	private SlickControls.SlickButton B_DeleteRequest;
	private SlickControls.SlickButton B_ApplyChanges;
	private SlickControls.SlickButton B_ManagePackage;
	private SlickControls.SlickSpacer slickSpacer1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
	private PackageStabilityDropDown DD_Stability;
	private DlcDropDown DD_DLCs;
	private PackageUsageDropDown DD_Usage;
	private PackageTypeDropDown DD_PackageType;
	private SlickControls.SlickIcon I_Copy;
}
