namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_CompatibilityCenter
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
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			this.panel1 = new System.Windows.Forms.Panel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.smartTablePanel = new SlickControls.SmartTablePanel();
			this.roundedGroupTableLayoutPanel3 = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_ReviewRequests = new SlickControls.SlickButton();
			this.L_NoActiveRequests = new System.Windows.Forms.Label();
			this.roundedGroupTableLayoutPanel1 = new SlickControls.RoundedGroupTableLayoutPanel();
			this.AnnouncementTitle = new SlickControls.SlickTextBox();
			this.AnnouncementText = new SlickControls.SlickTextBox();
			this.AnnouncementDateFrom = new SlickControls.SlickDateTime();
			this.AnnouncementDateTo = new SlickControls.SlickDateTime();
			this.AnnouncementButton = new SlickControls.SlickButton();
			this.TB_GameVersion = new SlickControls.SlickTextBox();
			this.activeReviewsControl = new Skyve.App.CS2.UserInterface.Content.PackagesWithIssuesControl();
			this.bigSelectionOptionControl2 = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.bigSelectionOptionControl1 = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.panel1.SuspendLayout();
			this.smartTablePanel.SuspendLayout();
			this.roundedGroupTableLayoutPanel3.SuspendLayout();
			this.roundedGroupTableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(202, 41);
			this.base_Text.Text = "CompatibilityCenter";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.slickScroll1);
			this.panel1.Controls.Add(this.smartTablePanel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(5, 30);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(930, 657);
			this.panel1.TabIndex = 2;
			// 
			// slickScroll1
			// 
			this.slickScroll1.AnimatedValue = 10;
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.smartTablePanel;
			this.slickScroll1.Location = new System.Drawing.Point(910, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(20, 657);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 1;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.TargetAnimationValue = 10;
			this.slickScroll1.Text = "slickScroll1";
			// 
			// smartTablePanel
			// 
			this.smartTablePanel.ColumnCount = 4;
			this.smartTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
			this.smartTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.smartTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.smartTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.smartTablePanel.Controls.Add(this.roundedGroupTableLayoutPanel3, 0, 1);
			this.smartTablePanel.Controls.Add(this.roundedGroupTableLayoutPanel1, 0, 0);
			this.smartTablePanel.Controls.Add(this.bigSelectionOptionControl2, 2, 0);
			this.smartTablePanel.Controls.Add(this.bigSelectionOptionControl1, 3, 0);
			this.smartTablePanel.Controls.Add(this.TB_GameVersion, 2, 2);
			this.smartTablePanel.Location = new System.Drawing.Point(0, 0);
			this.smartTablePanel.Name = "smartTablePanel";
			this.smartTablePanel.RowCount = 3;
			this.smartTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.smartTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.smartTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.smartTablePanel.Size = new System.Drawing.Size(842, 644);
			this.smartTablePanel.TabIndex = 0;
			// 
			// roundedGroupTableLayoutPanel3
			// 
			this.roundedGroupTableLayoutPanel3.AddShadow = true;
			this.roundedGroupTableLayoutPanel3.AutoSize = true;
			this.roundedGroupTableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedGroupTableLayoutPanel3.ColumnCount = 1;
			this.roundedGroupTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.roundedGroupTableLayoutPanel3.Controls.Add(this.B_ReviewRequests, 0, 3);
			this.roundedGroupTableLayoutPanel3.Controls.Add(this.activeReviewsControl, 0, 2);
			this.roundedGroupTableLayoutPanel3.Controls.Add(this.L_NoActiveRequests, 0, 1);
			this.roundedGroupTableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon2.Name = "RequestReview";
			this.roundedGroupTableLayoutPanel3.ImageName = dynamicIcon2;
			this.roundedGroupTableLayoutPanel3.Location = new System.Drawing.Point(3, 350);
			this.roundedGroupTableLayoutPanel3.Name = "roundedGroupTableLayoutPanel3";
			this.roundedGroupTableLayoutPanel3.Padding = new System.Windows.Forms.Padding(22, 69, 22, 22);
			this.roundedGroupTableLayoutPanel3.RowCount = 4;
			this.smartTablePanel.SetRowSpan(this.roundedGroupTableLayoutPanel3, 2);
			this.roundedGroupTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel3.Size = new System.Drawing.Size(471, 216);
			this.roundedGroupTableLayoutPanel3.TabIndex = 2;
			this.roundedGroupTableLayoutPanel3.Text = "ReviewRequests";
			// 
			// B_ReviewRequests
			// 
			this.B_ReviewRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_ReviewRequests.AutoSize = true;
			this.B_ReviewRequests.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "Link";
			this.B_ReviewRequests.ImageName = dynamicIcon1;
			this.B_ReviewRequests.Location = new System.Drawing.Point(321, 157);
			this.B_ReviewRequests.Name = "B_ReviewRequests";
			this.B_ReviewRequests.Size = new System.Drawing.Size(125, 34);
			this.B_ReviewRequests.SpaceTriggersClick = true;
			this.B_ReviewRequests.TabIndex = 2;
			this.B_ReviewRequests.Text = "ReviewRequests";
			this.B_ReviewRequests.Visible = false;
			this.B_ReviewRequests.Click += new System.EventHandler(this.B_ReviewRequests_Click);
			// 
			// L_NoActiveRequests
			// 
			this.L_NoActiveRequests.AutoSize = true;
			this.L_NoActiveRequests.Dock = System.Windows.Forms.DockStyle.Top;
			this.L_NoActiveRequests.Location = new System.Drawing.Point(25, 69);
			this.L_NoActiveRequests.Name = "L_NoActiveRequests";
			this.L_NoActiveRequests.Size = new System.Drawing.Size(421, 19);
			this.L_NoActiveRequests.TabIndex = 0;
			this.L_NoActiveRequests.Text = "There are no active review requests at the moment";
			this.L_NoActiveRequests.Visible = false;
			// 
			// roundedGroupTableLayoutPanel1
			// 
			this.roundedGroupTableLayoutPanel1.AddShadow = true;
			this.roundedGroupTableLayoutPanel1.AutoSize = true;
			this.roundedGroupTableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedGroupTableLayoutPanel1.ColumnCount = 2;
			this.roundedGroupTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedGroupTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.AnnouncementTitle, 0, 0);
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.AnnouncementText, 0, 1);
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.AnnouncementDateFrom, 0, 2);
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.AnnouncementDateTo, 1, 2);
			this.roundedGroupTableLayoutPanel1.Controls.Add(this.AnnouncementButton, 0, 3);
			this.roundedGroupTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon4.Name = "Megaphone";
			this.roundedGroupTableLayoutPanel1.ImageName = dynamicIcon4;
			this.roundedGroupTableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.roundedGroupTableLayoutPanel1.Name = "roundedGroupTableLayoutPanel1";
			this.roundedGroupTableLayoutPanel1.Padding = new System.Windows.Forms.Padding(22, 69, 22, 22);
			this.roundedGroupTableLayoutPanel1.RowCount = 4;
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.Size = new System.Drawing.Size(471, 341);
			this.roundedGroupTableLayoutPanel1.TabIndex = 0;
			this.roundedGroupTableLayoutPanel1.Text = "Announcement";
			// 
			// AnnouncementTitle
			// 
			this.roundedGroupTableLayoutPanel1.SetColumnSpan(this.AnnouncementTitle, 2);
			this.AnnouncementTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.AnnouncementTitle.LabelText = "Title";
			this.AnnouncementTitle.Location = new System.Drawing.Point(25, 72);
			this.AnnouncementTitle.MaxLength = 100;
			this.AnnouncementTitle.Name = "AnnouncementTitle";
			this.AnnouncementTitle.Padding = new System.Windows.Forms.Padding(7, 25, 7, 7);
			this.AnnouncementTitle.Placeholder = "Write a title for the announcement...";
			this.AnnouncementTitle.SelectedText = "";
			this.AnnouncementTitle.SelectionLength = 0;
			this.AnnouncementTitle.SelectionStart = 0;
			this.AnnouncementTitle.Size = new System.Drawing.Size(421, 69);
			this.AnnouncementTitle.TabIndex = 0;
			// 
			// AnnouncementText
			// 
			this.roundedGroupTableLayoutPanel1.SetColumnSpan(this.AnnouncementText, 2);
			this.AnnouncementText.Dock = System.Windows.Forms.DockStyle.Top;
			this.AnnouncementText.LabelText = "Description";
			this.AnnouncementText.Location = new System.Drawing.Point(25, 147);
			this.AnnouncementText.MultiLine = true;
			this.AnnouncementText.Name = "AnnouncementText";
			this.AnnouncementText.Padding = new System.Windows.Forms.Padding(7, 25, 7, 7);
			this.AnnouncementText.Placeholder = "Write what the announcement is about...";
			this.AnnouncementText.SelectedText = "";
			this.AnnouncementText.SelectionLength = 0;
			this.AnnouncementText.SelectionStart = 0;
			this.AnnouncementText.Size = new System.Drawing.Size(421, 64);
			this.AnnouncementText.TabIndex = 1;
			// 
			// AnnouncementDateFrom
			// 
			this.AnnouncementDateFrom.DateType = SlickControls.DateType.DateTime;
			this.AnnouncementDateFrom.Dock = System.Windows.Forms.DockStyle.Top;
			this.AnnouncementDateFrom.LabelText = "Start Date";
			this.AnnouncementDateFrom.Location = new System.Drawing.Point(25, 217);
			this.AnnouncementDateFrom.Name = "AnnouncementDateFrom";
			this.AnnouncementDateFrom.Required = false;
			this.AnnouncementDateFrom.SelectedPart = SlickControls.SlickDateTime.DatePart.Day;
			this.AnnouncementDateFrom.Size = new System.Drawing.Size(207, 59);
			this.AnnouncementDateFrom.TabIndex = 2;
			this.AnnouncementDateFrom.Value = new System.DateTime(2025, 5, 2, 0, 0, 0, 0);
			// 
			// AnnouncementDateTo
			// 
			this.AnnouncementDateTo.DateType = SlickControls.DateType.DateTime;
			this.AnnouncementDateTo.Dock = System.Windows.Forms.DockStyle.Top;
			this.AnnouncementDateTo.LabelText = "End Date";
			this.AnnouncementDateTo.Location = new System.Drawing.Point(238, 217);
			this.AnnouncementDateTo.Name = "AnnouncementDateTo";
			this.AnnouncementDateTo.Required = false;
			this.AnnouncementDateTo.SelectedPart = SlickControls.SlickDateTime.DatePart.Day;
			this.AnnouncementDateTo.Size = new System.Drawing.Size(208, 59);
			this.AnnouncementDateTo.TabIndex = 3;
			this.AnnouncementDateTo.Value = new System.DateTime(2025, 5, 2, 0, 0, 0, 0);
			// 
			// AnnouncementButton
			// 
			this.AnnouncementButton.AutoSize = true;
			this.roundedGroupTableLayoutPanel1.SetColumnSpan(this.AnnouncementButton, 2);
			this.AnnouncementButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.AnnouncementButton.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon3.Name = "Megaphone";
			this.AnnouncementButton.ImageName = dynamicIcon3;
			this.AnnouncementButton.Location = new System.Drawing.Point(25, 282);
			this.AnnouncementButton.Name = "AnnouncementButton";
			this.AnnouncementButton.Size = new System.Drawing.Size(421, 34);
			this.AnnouncementButton.SpaceTriggersClick = true;
			this.AnnouncementButton.TabIndex = 4;
			this.AnnouncementButton.Text = "Create Announcement";
			this.AnnouncementButton.Click += new System.EventHandler(this.AnnouncementButton_Click);
			// 
			// TB_GameVersion
			// 
			this.TB_GameVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.smartTablePanel.SetColumnSpan(this.TB_GameVersion, 2);
			this.TB_GameVersion.LabelText = "Current Game Version";
			this.TB_GameVersion.Location = new System.Drawing.Point(557, 572);
			this.TB_GameVersion.Name = "TB_GameVersion";
			this.TB_GameVersion.Padding = new System.Windows.Forms.Padding(7, 25, 7, 7);
			this.TB_GameVersion.Placeholder = "The game version to edit info on";
			this.TB_GameVersion.SelectedText = "";
			this.TB_GameVersion.SelectionLength = 0;
			this.TB_GameVersion.SelectionStart = 0;
			this.TB_GameVersion.Size = new System.Drawing.Size(282, 69);
			this.TB_GameVersion.TabIndex = 5;
			this.TB_GameVersion.TextChanged += new System.EventHandler(this.TB_GameVersion_TextChanged);
			// 
			// activeReviewsControl
			// 
			this.activeReviewsControl.AutoInvalidate = false;
			this.activeReviewsControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.activeReviewsControl.Loading = true;
			this.activeReviewsControl.Location = new System.Drawing.Point(25, 91);
			this.activeReviewsControl.Name = "activeReviewsControl";
			this.activeReviewsControl.Size = new System.Drawing.Size(421, 60);
			this.activeReviewsControl.TabIndex = 1;
			this.activeReviewsControl.Text = "PackageActiveReviewRequest";
			// 
			// bigSelectionOptionControl2
			// 
			this.bigSelectionOptionControl2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.bigSelectionOptionControl2.ButtonText = "Get Started";
			dynamicIcon5.Name = "Actions";
			this.bigSelectionOptionControl2.ImageName = dynamicIcon5;
			this.bigSelectionOptionControl2.Location = new System.Drawing.Point(533, 3);
			this.bigSelectionOptionControl2.Name = "bigSelectionOptionControl2";
			this.smartTablePanel.SetRowSpan(this.bigSelectionOptionControl2, 2);
			this.bigSelectionOptionControl2.Size = new System.Drawing.Size(150, 150);
			this.bigSelectionOptionControl2.TabIndex = 4;
			this.bigSelectionOptionControl2.Text = "Do a bulk edit of mods that broke from the latest patch";
			this.bigSelectionOptionControl2.Title = "Patch Preparation";
			this.bigSelectionOptionControl2.Click += new System.EventHandler(this.B_BulkEdit_Click);
			// 
			// bigSelectionOptionControl1
			// 
			this.bigSelectionOptionControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.bigSelectionOptionControl1.ButtonText = "Get Started";
			this.bigSelectionOptionControl1.Highlighted = true;
			dynamicIcon6.Name = "CompatibilityReport";
			this.bigSelectionOptionControl1.ImageName = dynamicIcon6;
			this.bigSelectionOptionControl1.Location = new System.Drawing.Point(689, 3);
			this.bigSelectionOptionControl1.Name = "bigSelectionOptionControl1";
			this.smartTablePanel.SetRowSpan(this.bigSelectionOptionControl1, 2);
			this.bigSelectionOptionControl1.Size = new System.Drawing.Size(150, 150);
			this.bigSelectionOptionControl1.TabIndex = 3;
			this.bigSelectionOptionControl1.Text = "Start a queue of recently updated mods to update their compatibility information";
			this.bigSelectionOptionControl1.Title = "Management Queue";
			this.bigSelectionOptionControl1.Click += new System.EventHandler(this.B_Manage_Click);
			// 
			// PC_CompatibilityCenter
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.panel1);
			this.Name = "PC_CompatibilityCenter";
			this.Padding = new System.Windows.Forms.Padding(5, 30, 0, 0);
			this.Size = new System.Drawing.Size(935, 687);
			this.Text = "CompatibilityCenter";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.panel1.ResumeLayout(false);
			this.smartTablePanel.ResumeLayout(false);
			this.smartTablePanel.PerformLayout();
			this.roundedGroupTableLayoutPanel3.ResumeLayout(false);
			this.roundedGroupTableLayoutPanel3.PerformLayout();
			this.roundedGroupTableLayoutPanel1.ResumeLayout(false);
			this.roundedGroupTableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Panel panel1;
	private SmartTablePanel smartTablePanel;
	private RoundedGroupTableLayoutPanel roundedGroupTableLayoutPanel3;
	private RoundedGroupTableLayoutPanel roundedGroupTableLayoutPanel1;
	private SlickTextBox AnnouncementText;
	private SlickDateTime AnnouncementDateFrom;
	private SlickDateTime AnnouncementDateTo;
	private SlickButton AnnouncementButton;
	private SlickTextBox AnnouncementTitle;
	private SlickButton B_ReviewRequests;
	private Content.PackagesWithIssuesControl activeReviewsControl;
	private System.Windows.Forms.Label L_NoActiveRequests;
	private SlickScroll slickScroll1;
	private App.UserInterface.Generic.BigSelectionOptionControl bigSelectionOptionControl1;
	private App.UserInterface.Generic.BigSelectionOptionControl bigSelectionOptionControl2;
	private SlickTextBox TB_GameVersion;
}
