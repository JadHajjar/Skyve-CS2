using Skyve.App.UserInterface.Lists;

namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_ReviewRequests
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_DeleteRequests = new SlickControls.SlickButton();
			this.B_ManagePackage = new SlickControls.SlickButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.P_Requests = new SlickControls.SlickStackedPanel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.base_P_Side = new System.Windows.Forms.Panel();
			this.base_TLP_Side = new SlickControls.RoundedTableLayoutPanel();
			this.packageCrList = new Skyve.App.UserInterface.Lists.PackageCrList();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.B_Previous = new SlickControls.SlickIcon();
			this.B_Skip = new SlickControls.SlickIcon();
			this.slickSpacer3 = new SlickControls.SlickSpacer();
			this.L_Page = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.base_P_Side.SuspendLayout();
			this.base_TLP_Side.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(-2, -27);
			this.base_Text.Size = new System.Drawing.Size(150, 41);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.B_DeleteRequests, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_ManagePackage, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(165, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(618, 438);
			this.tableLayoutPanel1.TabIndex = 21;
			// 
			// B_DeleteRequests
			// 
			this.B_DeleteRequests.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_DeleteRequests.AutoSize = true;
			this.B_DeleteRequests.ButtonType = SlickControls.ButtonType.Hidden;
			this.B_DeleteRequests.ColorStyle = Extensions.ColorStyle.Red;
			this.B_DeleteRequests.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "Trash";
			this.B_DeleteRequests.ImageName = dynamicIcon1;
			this.B_DeleteRequests.Location = new System.Drawing.Point(283, 403);
			this.B_DeleteRequests.Name = "B_DeleteRequests";
			this.B_DeleteRequests.Size = new System.Drawing.Size(179, 32);
			this.B_DeleteRequests.SpaceTriggersClick = true;
			this.B_DeleteRequests.TabIndex = 21;
			this.B_DeleteRequests.Text = "Delete these requests";
			this.B_DeleteRequests.Visible = false;
			this.B_DeleteRequests.Click += new System.EventHandler(this.B_DeleteRequests_Click);
			// 
			// B_ManagePackage
			// 
			this.B_ManagePackage.AutoSize = true;
			this.B_ManagePackage.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon2.Name = "Link";
			this.B_ManagePackage.ImageName = dynamicIcon2;
			this.B_ManagePackage.Location = new System.Drawing.Point(468, 403);
			this.B_ManagePackage.Name = "B_ManagePackage";
			this.B_ManagePackage.Size = new System.Drawing.Size(147, 32);
			this.B_ManagePackage.SpaceTriggersClick = true;
			this.B_ManagePackage.TabIndex = 22;
			this.B_ManagePackage.Text = "ManagePackage";
			this.B_ManagePackage.Click += new System.EventHandler(this.B_ManagePackage_Click);
			// 
			// panel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
			this.panel1.Controls.Add(this.P_Requests);
			this.panel1.Controls.Add(this.slickScroll1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(618, 400);
			this.panel1.TabIndex = 23;
			// 
			// P_Requests
			// 
			this.P_Requests.ColumnWidth = 300;
			this.P_Requests.Location = new System.Drawing.Point(0, 0);
			this.P_Requests.MaximizeSpaceUsage = true;
			this.P_Requests.Name = "P_Requests";
			this.P_Requests.Size = new System.Drawing.Size(0, 0);
			this.P_Requests.SmartAutoSize = true;
			this.P_Requests.TabIndex = 1;
			// 
			// slickScroll1
			// 
			this.slickScroll1.AnimatedValue = 10;
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.P_Requests;
			this.slickScroll1.Location = new System.Drawing.Point(598, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(20, 400);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 0;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.TargetAnimationValue = 10;
			this.slickScroll1.Text = "slickScroll1";
			// 
			// base_P_Side
			// 
			this.base_P_Side.Controls.Add(this.base_TLP_Side);
			this.base_P_Side.Dock = System.Windows.Forms.DockStyle.Left;
			this.base_P_Side.Location = new System.Drawing.Point(0, 0);
			this.base_P_Side.Name = "base_P_Side";
			this.base_P_Side.Size = new System.Drawing.Size(165, 438);
			this.base_P_Side.TabIndex = 22;
			// 
			// base_TLP_Side
			// 
			this.base_TLP_Side.BotLeft = true;
			this.base_TLP_Side.ColumnCount = 3;
			this.base_TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.base_TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.base_TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.base_TLP_Side.Controls.Add(this.packageCrList, 0, 1);
			this.base_TLP_Side.Controls.Add(this.TB_Search, 0, 0);
			this.base_TLP_Side.Controls.Add(this.B_Previous, 0, 3);
			this.base_TLP_Side.Controls.Add(this.B_Skip, 2, 3);
			this.base_TLP_Side.Controls.Add(this.slickSpacer3, 0, 2);
			this.base_TLP_Side.Controls.Add(this.L_Page, 1, 3);
			this.base_TLP_Side.Dock = System.Windows.Forms.DockStyle.Fill;
			this.base_TLP_Side.Location = new System.Drawing.Point(0, 0);
			this.base_TLP_Side.Name = "base_TLP_Side";
			this.base_TLP_Side.RowCount = 4;
			this.base_TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.base_TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.base_TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.base_TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.base_TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.base_TLP_Side.Size = new System.Drawing.Size(165, 438);
			this.base_TLP_Side.TabIndex = 43;
			this.base_TLP_Side.TopLeft = true;
			// 
			// packageCrList
			// 
			this.packageCrList.AutoInvalidate = false;
			this.packageCrList.AutoScroll = true;
			this.base_TLP_Side.SetColumnSpan(this.packageCrList, 3);
			this.packageCrList.CurrentPackage = null;
			this.packageCrList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.packageCrList.HighlightOnHover = true;
			this.packageCrList.ItemHeight = 32;
			this.packageCrList.Location = new System.Drawing.Point(3, 58);
			this.packageCrList.Name = "packageCrList";
			this.packageCrList.SeparateWithLines = true;
			this.packageCrList.ShowCompleted = false;
			this.packageCrList.Size = new System.Drawing.Size(159, 320);
			this.packageCrList.TabIndex = 2;
			this.packageCrList.ItemMouseClick += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.packageCrList_ItemMouseClick);
			// 
			// TB_Search
			// 
			this.base_TLP_Side.SetColumnSpan(this.TB_Search, 3);
			this.TB_Search.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon3.Name = "Search";
			this.TB_Search.ImageName = dynamicIcon3;
			this.TB_Search.Location = new System.Drawing.Point(3, 3);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Padding = new System.Windows.Forms.Padding(7, 7, 46, 7);
			this.TB_Search.Placeholder = "SearchGenericPackages";
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.ShowLabel = false;
			this.TB_Search.Size = new System.Drawing.Size(159, 49);
			this.TB_Search.TabIndex = 3;
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
			// 
			// B_Previous
			// 
			this.B_Previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Previous.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "ArrowUp";
			this.B_Previous.ImageName = dynamicIcon4;
			this.B_Previous.Location = new System.Drawing.Point(3, 401);
			this.B_Previous.Margin = new System.Windows.Forms.Padding(0);
			this.B_Previous.Name = "B_Previous";
			this.B_Previous.Size = new System.Drawing.Size(76, 37);
			this.B_Previous.TabIndex = 4;
			this.B_Previous.Click += new System.EventHandler(this.B_Previous_Click);
			// 
			// B_Skip
			// 
			this.B_Skip.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "ArrowDown";
			this.B_Skip.ImageName = dynamicIcon5;
			this.B_Skip.Location = new System.Drawing.Point(85, 401);
			this.B_Skip.Margin = new System.Windows.Forms.Padding(0);
			this.B_Skip.Name = "B_Skip";
			this.B_Skip.Size = new System.Drawing.Size(77, 37);
			this.B_Skip.TabIndex = 5;
			this.B_Skip.Click += new System.EventHandler(this.B_Skip_Click);
			// 
			// slickSpacer3
			// 
			this.base_TLP_Side.SetColumnSpan(this.slickSpacer3, 3);
			this.slickSpacer3.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer3.Location = new System.Drawing.Point(3, 384);
			this.slickSpacer3.Name = "slickSpacer3";
			this.slickSpacer3.Size = new System.Drawing.Size(159, 14);
			this.slickSpacer3.TabIndex = 6;
			this.slickSpacer3.TabStop = false;
			this.slickSpacer3.Text = "slickSpacer3";
			// 
			// L_Page
			// 
			this.L_Page.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Page.AutoSize = true;
			this.L_Page.Location = new System.Drawing.Point(82, 410);
			this.L_Page.Name = "L_Page";
			this.L_Page.Size = new System.Drawing.Size(0, 19);
			this.L_Page.TabIndex = 7;
			// 
			// PC_ReviewRequests
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.base_P_Side);
			this.Name = "PC_ReviewRequests";
			this.Padding = new System.Windows.Forms.Padding(0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.base_P_Side, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.base_P_Side.ResumeLayout(false);
			this.base_TLP_Side.ResumeLayout(false);
			this.base_TLP_Side.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private SlickControls.SlickButton B_DeleteRequests;
	private System.Windows.Forms.Panel base_P_Side;
	internal RoundedTableLayoutPanel base_TLP_Side;
	private PackageCrList packageCrList;
	private SlickTextBox TB_Search;
	private SlickIcon B_Previous;
	private SlickIcon B_Skip;
	private SlickSpacer slickSpacer3;
	private System.Windows.Forms.Label L_Page;
	private SlickButton B_ManagePackage;
	private System.Windows.Forms.Panel panel1;
	private SlickStackedPanel P_Requests;
	private SlickScroll slickScroll1;
}
