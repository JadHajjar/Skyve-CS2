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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_DeleteRequests = new SlickControls.SlickButton();
			this.base_P_Side = new System.Windows.Forms.Panel();
			this.base_TLP_Side = new SlickControls.RoundedTableLayoutPanel();
			this.packageCrList = new Skyve.App.UserInterface.Lists.PackageCrList();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.B_Previous = new SlickControls.SlickIcon();
			this.B_Skip = new SlickControls.SlickIcon();
			this.slickSpacer3 = new SlickControls.SlickSpacer();
			this.L_Page = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
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
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.B_DeleteRequests, 0, 1);
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
			this.B_DeleteRequests.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "I_Disposable";
			this.B_DeleteRequests.ImageName = dynamicIcon1;
			this.B_DeleteRequests.Location = new System.Drawing.Point(432, 401);
			this.B_DeleteRequests.Name = "B_DeleteRequests";
			this.B_DeleteRequests.Size = new System.Drawing.Size(183, 34);
			this.B_DeleteRequests.SpaceTriggersClick = true;
			this.B_DeleteRequests.TabIndex = 21;
			this.B_DeleteRequests.Text = "Delete these requests";
			this.B_DeleteRequests.Visible = false;
			this.B_DeleteRequests.Click += new System.EventHandler(this.B_DeleteRequests_Click);
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
			this.packageCrList.Location = new System.Drawing.Point(3, 61);
			this.packageCrList.Name = "packageCrList";
			this.packageCrList.SeparateWithLines = true;
			this.packageCrList.Size = new System.Drawing.Size(159, 317);
			this.packageCrList.TabIndex = 2;
			this.packageCrList.ItemMouseClick += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.packageCrList_ItemMouseClick);
			// 
			// TB_Search
			// 
			this.base_TLP_Side.SetColumnSpan(this.TB_Search, 3);
			this.TB_Search.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon2.Name = "I_Search";
			this.TB_Search.ImageName = dynamicIcon2;
			this.TB_Search.Location = new System.Drawing.Point(3, 3);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Padding = new System.Windows.Forms.Padding(7, 7, 46, 7);
			this.TB_Search.Placeholder = "SearchGenericPackages";
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.ShowLabel = false;
			this.TB_Search.Size = new System.Drawing.Size(159, 52);
			this.TB_Search.TabIndex = 3;
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
			// 
			// B_Previous
			// 
			this.B_Previous.ActiveColor = null;
			this.B_Previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Previous.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon3.Name = "I_ArrowUp";
			this.B_Previous.ImageName = dynamicIcon3;
			this.B_Previous.Location = new System.Drawing.Point(3, 401);
			this.B_Previous.Margin = new System.Windows.Forms.Padding(0);
			this.B_Previous.Name = "B_Previous";
			this.B_Previous.Size = new System.Drawing.Size(76, 37);
			this.B_Previous.TabIndex = 4;
			// 
			// B_Skip
			// 
			this.B_Skip.ActiveColor = null;
			this.B_Skip.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "I_ArrowDown";
			this.B_Skip.ImageName = dynamicIcon4;
			this.B_Skip.Location = new System.Drawing.Point(85, 401);
			this.B_Skip.Margin = new System.Windows.Forms.Padding(0);
			this.B_Skip.Name = "B_Skip";
			this.B_Skip.Size = new System.Drawing.Size(77, 37);
			this.B_Skip.TabIndex = 5;
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
			this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.base_P_Side, 0);
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.base_P_Side.ResumeLayout(false);
			this.base_TLP_Side.ResumeLayout(false);
			this.base_TLP_Side.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private ReviewRequestList reviewRequestList1;
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
}
