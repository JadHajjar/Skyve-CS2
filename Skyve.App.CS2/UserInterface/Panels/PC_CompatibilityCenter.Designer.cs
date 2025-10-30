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
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			this.panel1 = new System.Windows.Forms.Panel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.smartTablePanel = new SlickControls.SmartTablePanel();
			this.roundedGroupTableLayoutPanel3 = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_ReviewRequests = new SlickControls.SlickButton();
			this.activeReviewsControl = new Skyve.App.CS2.UserInterface.Content.PackagesWithIssuesControl();
			this.L_NoActiveRequests = new System.Windows.Forms.Label();
			this.roundedGroupTableLayoutPanel1 = new SlickControls.RoundedGroupTableLayoutPanel();
			this.AnnouncementTitle = new SlickControls.SlickTextBox();
			this.AnnouncementText = new SlickControls.SlickTextBox();
			this.AnnouncementDateFrom = new SlickControls.SlickDateTime();
			this.AnnouncementDateTo = new SlickControls.SlickDateTime();
			this.AnnouncementButton = new SlickControls.SlickButton();
			this.bigSelectionOptionControl1 = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.panel1.SuspendLayout();
			this.smartTablePanel.SuspendLayout();
			this.roundedGroupTableLayoutPanel3.SuspendLayout();
			this.roundedGroupTableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(199, 39);
			this.base_Text.Text = "CompatibilityCenter";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.slickScroll1);
			this.panel1.Controls.Add(this.smartTablePanel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(5, 30);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(930, 576);
			this.panel1.TabIndex = 2;
			// 
			// slickScroll1
			// 
			this.slickScroll1.AnimatedValue = 10;
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.smartTablePanel;
			this.slickScroll1.Location = new System.Drawing.Point(910, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(20, 576);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 1;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.TargetAnimationValue = 10;
			this.slickScroll1.Text = "slickScroll1";
			// 
			// smartTablePanel
			// 
			this.smartTablePanel.ColumnCount = 2;
			this.smartTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.smartTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.smartTablePanel.Controls.Add(this.roundedGroupTableLayoutPanel3, 0, 1);
			this.smartTablePanel.Controls.Add(this.roundedGroupTableLayoutPanel1, 0, 0);
			this.smartTablePanel.Controls.Add(this.bigSelectionOptionControl1, 1, 0);
			this.smartTablePanel.Location = new System.Drawing.Point(0, 0);
			this.smartTablePanel.Name = "smartTablePanel";
			this.smartTablePanel.RowCount = 2;
			this.smartTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.smartTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.smartTablePanel.Size = new System.Drawing.Size(842, 529);
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
			this.roundedGroupTableLayoutPanel3.Location = new System.Drawing.Point(3, 326);
			this.roundedGroupTableLayoutPanel3.Name = "roundedGroupTableLayoutPanel3";
			this.roundedGroupTableLayoutPanel3.Padding = new System.Windows.Forms.Padding(21, 60, 21, 21);
			this.roundedGroupTableLayoutPanel3.RowCount = 4;
			this.roundedGroupTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel3.Size = new System.Drawing.Size(415, 200);
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
			this.B_ReviewRequests.Location = new System.Drawing.Point(247, 144);
			this.B_ReviewRequests.Name = "B_ReviewRequests";
			this.B_ReviewRequests.Size = new System.Drawing.Size(144, 32);
			this.B_ReviewRequests.SpaceTriggersClick = true;
			this.B_ReviewRequests.TabIndex = 2;
			this.B_ReviewRequests.Text = "ReviewRequests";
			this.B_ReviewRequests.Visible = false;
			this.B_ReviewRequests.Click += new System.EventHandler(this.B_ReviewRequests_Click);
			// 
			// activeReviewsControl
			// 
			this.activeReviewsControl.AutoInvalidate = false;
			this.activeReviewsControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.activeReviewsControl.Loading = true;
			this.activeReviewsControl.Location = new System.Drawing.Point(24, 82);
			this.activeReviewsControl.Name = "activeReviewsControl";
			this.activeReviewsControl.Size = new System.Drawing.Size(367, 56);
			this.activeReviewsControl.TabIndex = 1;
			this.activeReviewsControl.Text = "PackageActiveReviewRequest";
			// 
			// L_NoActiveRequests
			// 
			this.L_NoActiveRequests.AutoSize = true;
			this.L_NoActiveRequests.Dock = System.Windows.Forms.DockStyle.Top;
			this.L_NoActiveRequests.Location = new System.Drawing.Point(24, 60);
			this.L_NoActiveRequests.Name = "L_NoActiveRequests";
			this.L_NoActiveRequests.Size = new System.Drawing.Size(367, 19);
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
			dynamicIcon3.Name = "Megaphone";
			this.roundedGroupTableLayoutPanel1.ImageName = dynamicIcon3;
			this.roundedGroupTableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.roundedGroupTableLayoutPanel1.Name = "roundedGroupTableLayoutPanel1";
			this.roundedGroupTableLayoutPanel1.Padding = new System.Windows.Forms.Padding(21, 60, 21, 21);
			this.roundedGroupTableLayoutPanel1.RowCount = 4;
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedGroupTableLayoutPanel1.Size = new System.Drawing.Size(415, 317);
			this.roundedGroupTableLayoutPanel1.TabIndex = 0;
			this.roundedGroupTableLayoutPanel1.Text = "Announcement";
			// 
			// AnnouncementTitle
			// 
			this.roundedGroupTableLayoutPanel1.SetColumnSpan(this.AnnouncementTitle, 2);
			this.AnnouncementTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.AnnouncementTitle.LabelText = "Title";
			this.AnnouncementTitle.Location = new System.Drawing.Point(24, 63);
			this.AnnouncementTitle.MaxLength = 100;
			this.AnnouncementTitle.Name = "AnnouncementTitle";
			this.AnnouncementTitle.Padding = new System.Windows.Forms.Padding(7, 21, 7, 7);
			this.AnnouncementTitle.Placeholder = "Write a title for the announcement...";
			this.AnnouncementTitle.SelectedText = "";
			this.AnnouncementTitle.SelectionLength = 0;
			this.AnnouncementTitle.SelectionStart = 0;
			this.AnnouncementTitle.Size = new System.Drawing.Size(367, 57);
			this.AnnouncementTitle.TabIndex = 0;
			// 
			// AnnouncementText
			// 
			this.roundedGroupTableLayoutPanel1.SetColumnSpan(this.AnnouncementText, 2);
			this.AnnouncementText.Dock = System.Windows.Forms.DockStyle.Top;
			this.AnnouncementText.LabelText = "Description";
			this.AnnouncementText.Location = new System.Drawing.Point(24, 126);
			this.AnnouncementText.MultiLine = true;
			this.AnnouncementText.Name = "AnnouncementText";
			this.AnnouncementText.Padding = new System.Windows.Forms.Padding(7, 21, 7, 7);
			this.AnnouncementText.Placeholder = "Write what the announcement is about...";
			this.AnnouncementText.SelectedText = "";
			this.AnnouncementText.SelectionLength = 0;
			this.AnnouncementText.SelectionStart = 0;
			this.AnnouncementText.Size = new System.Drawing.Size(367, 64);
			this.AnnouncementText.TabIndex = 1;
			// 
			// AnnouncementDateFrom
			// 
			this.AnnouncementDateFrom.DateType = SlickControls.DateType.DateTime;
			this.AnnouncementDateFrom.Dock = System.Windows.Forms.DockStyle.Top;
			this.AnnouncementDateFrom.LabelText = "Start Date";
			this.AnnouncementDateFrom.Location = new System.Drawing.Point(24, 196);
			this.AnnouncementDateFrom.Name = "AnnouncementDateFrom";
			this.AnnouncementDateFrom.Required = false;
			this.AnnouncementDateFrom.SelectedPart = SlickControls.SlickDateTime.DatePart.Day;
			this.AnnouncementDateFrom.Size = new System.Drawing.Size(180, 59);
			this.AnnouncementDateFrom.TabIndex = 2;
			this.AnnouncementDateFrom.Value = new System.DateTime(2025, 5, 2, 0, 0, 0, 0);
			// 
			// AnnouncementDateTo
			// 
			this.AnnouncementDateTo.DateType = SlickControls.DateType.DateTime;
			this.AnnouncementDateTo.Dock = System.Windows.Forms.DockStyle.Top;
			this.AnnouncementDateTo.LabelText = "End Date";
			this.AnnouncementDateTo.Location = new System.Drawing.Point(210, 196);
			this.AnnouncementDateTo.Name = "AnnouncementDateTo";
			this.AnnouncementDateTo.Required = false;
			this.AnnouncementDateTo.SelectedPart = SlickControls.SlickDateTime.DatePart.Day;
			this.AnnouncementDateTo.Size = new System.Drawing.Size(181, 59);
			this.AnnouncementDateTo.TabIndex = 3;
			this.AnnouncementDateTo.Value = new System.DateTime(2025, 5, 2, 0, 0, 0, 0);
			// 
			// AnnouncementButton
			// 
			this.AnnouncementButton.AutoSize = true;
			this.roundedGroupTableLayoutPanel1.SetColumnSpan(this.AnnouncementButton, 2);
			this.AnnouncementButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.AnnouncementButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.AnnouncementButton.ImageName = dynamicIcon1;
			this.AnnouncementButton.Location = new System.Drawing.Point(24, 261);
			this.AnnouncementButton.Name = "AnnouncementButton";
			this.AnnouncementButton.Size = new System.Drawing.Size(367, 32);
			this.AnnouncementButton.SpaceTriggersClick = true;
			this.AnnouncementButton.TabIndex = 4;
			this.AnnouncementButton.Text = "Create Announcement";
			this.AnnouncementButton.Click += new System.EventHandler(this.AnnouncementButton_Click);
			// 
			// bigSelectionOptionControl1
			// 
			this.bigSelectionOptionControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.bigSelectionOptionControl1.ButtonText = "Get Started";
			this.bigSelectionOptionControl1.Highlighted = true;
			dynamicIcon4.Name = "CompatibilityReport";
			this.bigSelectionOptionControl1.ImageName = dynamicIcon4;
			this.bigSelectionOptionControl1.Location = new System.Drawing.Point(556, 3);
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
			this.Size = new System.Drawing.Size(935, 606);
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
}
