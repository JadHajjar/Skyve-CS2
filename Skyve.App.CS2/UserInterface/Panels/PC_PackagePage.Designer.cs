﻿using Skyve.App.UserInterface.Content;
using Skyve.Domain;
using Skyve.Domain.Systems;

namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_PackagePage
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
			this.slickTabControl = new SlickControls.SlickTabControl();
			this.T_Info = new SlickControls.SlickTabControl.Tab();
			this.slickWebBrowser = new SlickControls.Controls.Advanced.SlickWebBrowser();
			this.T_Gallery = new SlickControls.SlickTabControl.Tab();
			this.carouselControl = new Skyve.App.UserInterface.Generic.CarouselControl();
			this.T_Comments = new SlickControls.SlickTabControl.Tab();
			this.commentsControl1 = new Skyve.App.CS2.UserInterface.Generic.CommentsSectionControl();
			this.T_Content = new SlickControls.SlickTabControl.Tab();
			this.T_Compatibility = new SlickControls.SlickTabControl.Tab();
			this.T_References = new SlickControls.SlickTabControl.Tab();
			this.T_Playsets = new SlickControls.SlickTabControl.Tab();
			this.T_Changelog = new SlickControls.SlickTabControl.Tab();
			this.packageChangelogControl1 = new Skyve.App.CS2.UserInterface.Content.PackageChangelogControl();
			this.SuspendLayout();
			// 
			// P_SideContainer
			// 
			this.P_SideContainer.Location = new System.Drawing.Point(1312, 30);
			this.P_SideContainer.Size = new System.Drawing.Size(200, 1039);
			// 
			// slickTabControl
			// 
			this.slickTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl.Location = new System.Drawing.Point(0, 30);
			this.slickTabControl.Margin = new System.Windows.Forms.Padding(0);
			this.slickTabControl.Name = "slickTabControl";
			this.slickTabControl.Size = new System.Drawing.Size(1312, 1039);
			this.slickTabControl.TabIndex = 0;
			this.slickTabControl.Tabs = new SlickControls.SlickTabControl.Tab[] {
        this.T_Info,
        this.T_Gallery,
        this.T_Comments,
        this.T_Content,
        this.T_Compatibility,
        this.T_References,
        this.T_Playsets,
        this.T_Changelog};
			// 
			// T_Info
			// 
			this.T_Info.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Info.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Info.FillTab = true;
			this.T_Info.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dynamicIcon1.Name = "Content";
			this.T_Info.IconName = dynamicIcon1;
			this.T_Info.LinkedControl = this.slickWebBrowser;
			this.T_Info.Location = new System.Drawing.Point(0, 5);
			this.T_Info.Name = "T_Info";
			this.T_Info.Size = new System.Drawing.Size(156, 75);
			this.T_Info.TabIndex = 0;
			this.T_Info.TabStop = false;
			this.T_Info.Text = "Info";
			// 
			// slickWebBrowser
			// 
			this.slickWebBrowser.Body = null;
			this.slickWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickWebBrowser.Head = null;
			this.slickWebBrowser.IsWebBrowserContextMenuEnabled = false;
			this.slickWebBrowser.Location = new System.Drawing.Point(0, 0);
			this.slickWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.slickWebBrowser.Name = "slickWebBrowser";
			this.slickWebBrowser.Size = new System.Drawing.Size(1312, 959);
			this.slickWebBrowser.TabIndex = 17;
			this.slickWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.SlickWebBrowser_Navigating);
			// 
			// T_Gallery
			// 
			this.T_Gallery.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Gallery.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Gallery.FillTab = true;
			dynamicIcon2.Name = "Gallery";
			this.T_Gallery.IconName = dynamicIcon2;
			this.T_Gallery.LinkedControl = this.carouselControl;
			this.T_Gallery.Location = new System.Drawing.Point(156, 5);
			this.T_Gallery.Name = "T_Gallery";
			this.T_Gallery.Size = new System.Drawing.Size(156, 75);
			this.T_Gallery.TabIndex = 0;
			this.T_Gallery.TabStop = false;
			this.T_Gallery.Text = "Gallery";
			// 
			// carouselControl
			// 
			this.carouselControl.Location = new System.Drawing.Point(0, 0);
			this.carouselControl.Name = "carouselControl";
			this.carouselControl.Size = new System.Drawing.Size(796, 543);
			this.carouselControl.TabIndex = 9;
			// 
			// T_Comments
			// 
			this.T_Comments.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Comments.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon3.Name = "Chat";
			this.T_Comments.IconName = dynamicIcon3;
			this.T_Comments.LinkedControl = this.commentsControl1;
			this.T_Comments.Location = new System.Drawing.Point(312, 5);
			this.T_Comments.Name = "T_Comments";
			this.T_Comments.Size = new System.Drawing.Size(156, 75);
			this.T_Comments.TabIndex = 3;
			this.T_Comments.TabStop = false;
			this.T_Comments.FillTab = true;
		this.T_Comments.Text = "Comments";
			// 
			// commentsControl1
			// 
			this.commentsControl1.AutoSize = true;
			this.commentsControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.commentsControl1.Location = new System.Drawing.Point(0, 0);
			this.commentsControl1.Name = "commentsControl1";
			this.commentsControl1.Package = null;
			this.commentsControl1.Size = new System.Drawing.Size(638, 574);
			this.commentsControl1.TabIndex = 16;
			// 
			// T_Content
			// 
			this.T_Content.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Content.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Content.FillTab = true;
			dynamicIcon4.Name = "Assets";
			this.T_Content.IconName = dynamicIcon4;
			this.T_Content.LinkedControl = null;
			this.T_Content.Location = new System.Drawing.Point(468, 5);
			this.T_Content.Name = "T_Content";
			this.T_Content.Size = new System.Drawing.Size(156, 75);
			this.T_Content.TabIndex = 2;
			this.T_Content.TabStop = false;
			this.T_Content.Text = "Content";
			// 
			// T_Compatibility
			// 
			this.T_Compatibility.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Compatibility.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Compatibility.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dynamicIcon5.Name = "CompatibilityReport";
			this.T_Compatibility.IconName = dynamicIcon5;
			this.T_Compatibility.LinkedControl = null;
			this.T_Compatibility.Location = new System.Drawing.Point(624, 5);
			this.T_Compatibility.Name = "T_Compatibility";
			this.T_Compatibility.Size = new System.Drawing.Size(156, 75);
			this.T_Compatibility.TabIndex = 0;
			this.T_Compatibility.TabStop = false;
			this.T_Compatibility.Text = "Compatibility";
			this.T_Compatibility.Paint += new System.Windows.Forms.PaintEventHandler(this.T_Compatibility_Paint);
			// 
			// T_References
			// 
			this.T_References.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_References.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_References.FillTab = true;
			dynamicIcon6.Name = "Share";
			this.T_References.IconName = dynamicIcon6;
			this.T_References.LinkedControl = null;
			this.T_References.Location = new System.Drawing.Point(780, 5);
			this.T_References.Name = "T_References";
			this.T_References.Size = new System.Drawing.Size(156, 75);
			this.T_References.TabIndex = 1;
			this.T_References.TabStop = false;
			this.T_References.Text = "References";
			this.T_References.TabSelected += new System.EventHandler(this.T_References_TabSelected);
			// 
			// T_Playsets
			// 
			this.T_Playsets.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Playsets.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Playsets.FillTab = true;
			this.T_Playsets.Font = new System.Drawing.Font("Nirmala UI", 9F);
			dynamicIcon7.Name = "Playsets";
			this.T_Playsets.IconName = dynamicIcon7;
			this.T_Playsets.LinkedControl = null;
			this.T_Playsets.Location = new System.Drawing.Point(936, 5);
			this.T_Playsets.Name = "T_Playsets";
			this.T_Playsets.Size = new System.Drawing.Size(156, 75);
			this.T_Playsets.TabIndex = 0;
			this.T_Playsets.TabStop = false;
			this.T_Playsets.Text = "Playsets";
			// 
			// T_Changelog
			// 
			this.T_Changelog.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Changelog.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Changelog.FillTab = true;
			dynamicIcon8.Name = "Versions";
			this.T_Changelog.IconName = dynamicIcon8;
			this.T_Changelog.LinkedControl = this.packageChangelogControl1;
			this.T_Changelog.Location = new System.Drawing.Point(1092, 5);
			this.T_Changelog.Name = "T_Changelog";
			this.T_Changelog.Size = new System.Drawing.Size(156, 75);
			this.T_Changelog.TabIndex = 0;
			this.T_Changelog.TabStop = false;
			this.T_Changelog.Text = "Changelog";
			// 
			// packageChangelogControl1
			// 
			this.packageChangelogControl1.Location = new System.Drawing.Point(0, 0);
			this.packageChangelogControl1.Name = "packageChangelogControl1";
			this.packageChangelogControl1.Size = new System.Drawing.Size(796, 543);
			this.packageChangelogControl1.TabIndex = 15;
			// 
			// PC_PackagePage
			// 
			this.Controls.Add(this.slickTabControl);
			this.Name = "PC_PackagePage";
			this.Size = new System.Drawing.Size(1512, 1069);
			this.Controls.SetChildIndex(this.P_SideContainer, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.slickTabControl, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private SlickControls.SlickTabControl slickTabControl;
	private SlickControls.SlickTabControl.Tab T_Info;
	public SlickControls.SlickTabControl.Tab T_Compatibility;
	private SlickControls.SlickTabControl.Tab T_Playsets;
	private SlickControls.SlickTabControl.Tab T_References;
	private SlickControls.Controls.Advanced.SlickWebBrowser slickWebBrowser;
	private SlickTabControl.Tab T_Gallery;
	private SlickTabControl.Tab T_Content;
	private App.UserInterface.Generic.CarouselControl carouselControl;
	private SlickTabControl.Tab T_Changelog;
	private Content.PackageChangelogControl packageChangelogControl1;
	private SlickTabControl.Tab T_Comments;
	private Generic.CommentsSectionControl commentsControl1;
}
