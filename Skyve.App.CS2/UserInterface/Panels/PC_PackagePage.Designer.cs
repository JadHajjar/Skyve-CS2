using Skyve.App.UserInterface.Content;
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
			_notifier.WorkshopInfoUpdated -= Notifier_WorkshopInfoUpdated;
			_notifier.PackageInclusionUpdated -= Notifier_WorkshopInfoUpdated;
			_notifier.PackageInformationUpdated -= Notifier_WorkshopInfoUpdated;
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
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			this.slickTabControl = new SlickControls.SlickTabControl();
			this.T_Info = new SlickControls.SlickTabControl.Tab();
			this.slickWebBrowser = new SlickControls.Controls.Advanced.SlickWebBrowser();
			this.T_Content = new SlickControls.SlickTabControl.Tab();
			this.T_Compatibility = new SlickControls.SlickTabControl.Tab();
			this.T_References = new SlickControls.SlickTabControl.Tab();
			this.T_Playsets = new SlickControls.SlickTabControl.Tab();
			this.P_Side = new System.Windows.Forms.Panel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.TLP_Side = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Links = new SlickControls.RoundedTableLayoutPanel();
			this.L_Links = new System.Windows.Forms.Label();
			this.FLP_Links = new SlickControls.SmartFlowPanel();
			this.TLP_ModInfo = new SlickControls.RoundedTableLayoutPanel();
			this.L_Info = new System.Windows.Forms.Label();
			this.TLP_TopInfo = new System.Windows.Forms.TableLayoutPanel();
			this.I_More = new SlickControls.SlickIcon();
			this.L_Author = new SlickControls.SlickLabel();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.TLP_ModRequirements = new SlickControls.RoundedTableLayoutPanel();
			this.L_Requirements = new System.Windows.Forms.Label();
			this.B_BulkRequirements = new SlickControls.SlickButton();
			this.P_Requirements = new System.Windows.Forms.Panel();
			this.TLP_Tags = new SlickControls.RoundedTableLayoutPanel();
			this.L_Tags = new System.Windows.Forms.Label();
			this.FLP_Tags = new SlickControls.SmartFlowPanel();
			this.T_Gallery = new SlickControls.SlickTabControl.Tab();
			this.carouselControl = new Skyve.App.UserInterface.Generic.CarouselControl();
			this.T_Changelog = new SlickControls.SlickTabControl.Tab();
			this.packageChangelogControl1 = new Skyve.App.CS2.UserInterface.Content.PackageChangelogControl();
			this.LI_Size = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_ModId = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_Subscribers = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_Votes = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_UpdateTime = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_Version = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.PB_Icon = new Skyve.App.UserInterface.Content.PackageIcon();
			this.P_Side.SuspendLayout();
			this.TLP_Side.SuspendLayout();
			this.TLP_Links.SuspendLayout();
			this.TLP_ModInfo.SuspendLayout();
			this.TLP_TopInfo.SuspendLayout();
			this.TLP_ModRequirements.SuspendLayout();
			this.TLP_Tags.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(150, 41);
			this.base_Text.Text = "Back";
			// 
			// slickTabControl
			// 
			this.slickTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl.Location = new System.Drawing.Point(0, 30);
			this.slickTabControl.Margin = new System.Windows.Forms.Padding(0);
			this.slickTabControl.Name = "slickTabControl";
			this.slickTabControl.Size = new System.Drawing.Size(796, 623);
			this.slickTabControl.TabIndex = 0;
			this.slickTabControl.Tabs = new SlickControls.SlickTabControl.Tab[] {
        this.T_Info,
        this.T_Gallery,
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
			dynamicIcon1.Name = "I_Content";
			this.T_Info.IconName = dynamicIcon1;
			this.T_Info.LinkedControl = this.slickWebBrowser;
			this.T_Info.Location = new System.Drawing.Point(0, 5);
			this.T_Info.Name = "T_Info";
			this.T_Info.Selected = true;
			this.T_Info.Size = new System.Drawing.Size(113, 25);
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
			this.slickWebBrowser.Size = new System.Drawing.Size(796, 593);
			this.slickWebBrowser.TabIndex = 17;
			this.slickWebBrowser.WebBrowserShortcutsEnabled = false;
			this.slickWebBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.SlickWebBrowser_Navigating);
			// 
			// T_Content
			// 
			this.T_Content.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Content.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Content.FillTab = true;
			dynamicIcon3.Name = "I_Assets";
			this.T_Content.IconName = dynamicIcon3;
			this.T_Content.LinkedControl = null;
			this.T_Content.Location = new System.Drawing.Point(226, 5);
			this.T_Content.Name = "T_Content";
			this.T_Content.Selected = false;
			this.T_Content.Size = new System.Drawing.Size(113, 25);
			this.T_Content.TabIndex = 2;
			this.T_Content.TabStop = false;
			this.T_Content.Text = "Content";
			// 
			// T_Compatibility
			// 
			this.T_Compatibility.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Compatibility.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Compatibility.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dynamicIcon4.Name = "I_CompatibilityReport";
			this.T_Compatibility.IconName = dynamicIcon4;
			this.T_Compatibility.LinkedControl = null;
			this.T_Compatibility.Location = new System.Drawing.Point(339, 5);
			this.T_Compatibility.Name = "T_Compatibility";
			this.T_Compatibility.Selected = false;
			this.T_Compatibility.Size = new System.Drawing.Size(113, 25);
			this.T_Compatibility.TabIndex = 0;
			this.T_Compatibility.TabStop = false;
			this.T_Compatibility.Text = "Compatibility";
			// 
			// T_References
			// 
			this.T_References.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_References.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_References.FillTab = true;
			dynamicIcon5.Name = "I_Share";
			this.T_References.IconName = dynamicIcon5;
			this.T_References.LinkedControl = null;
			this.T_References.Location = new System.Drawing.Point(452, 5);
			this.T_References.Name = "T_References";
			this.T_References.Selected = false;
			this.T_References.Size = new System.Drawing.Size(113, 25);
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
			dynamicIcon6.Name = "I_Playsets";
			this.T_Playsets.IconName = dynamicIcon6;
			this.T_Playsets.LinkedControl = null;
			this.T_Playsets.Location = new System.Drawing.Point(565, 5);
			this.T_Playsets.Name = "T_Playsets";
			this.T_Playsets.Selected = false;
			this.T_Playsets.Size = new System.Drawing.Size(113, 25);
			this.T_Playsets.TabIndex = 0;
			this.T_Playsets.TabStop = false;
			this.T_Playsets.Text = "Playsets";
			// 
			// P_Side
			// 
			this.P_Side.Controls.Add(this.slickScroll1);
			this.P_Side.Controls.Add(this.TLP_Side);
			this.P_Side.Dock = System.Windows.Forms.DockStyle.Right;
			this.P_Side.Location = new System.Drawing.Point(796, 30);
			this.P_Side.Name = "P_Side";
			this.P_Side.Size = new System.Drawing.Size(200, 623);
			this.P_Side.TabIndex = 14;
			// 
			// slickScroll1
			// 
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.TLP_Side;
			this.slickScroll1.Location = new System.Drawing.Point(190, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(10, 623);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 2;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.Text = "slickScroll1";
			// 
			// TLP_Side
			// 
			this.TLP_Side.AutoSize = true;
			this.TLP_Side.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Side.ColumnCount = 1;
			this.TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Side.Controls.Add(this.TLP_Links, 0, 7);
			this.TLP_Side.Controls.Add(this.TLP_ModInfo, 0, 4);
			this.TLP_Side.Controls.Add(this.TLP_TopInfo, 0, 0);
			this.TLP_Side.Controls.Add(this.slickSpacer1, 0, 1);
			this.TLP_Side.Controls.Add(this.TLP_ModRequirements, 0, 5);
			this.TLP_Side.Controls.Add(this.TLP_Tags, 0, 6);
			this.TLP_Side.Location = new System.Drawing.Point(0, 0);
			this.TLP_Side.Name = "TLP_Side";
			this.TLP_Side.RowCount = 7;
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.Size = new System.Drawing.Size(390, 489);
			this.TLP_Side.TabIndex = 1;
			// 
			// TLP_Links
			// 
			this.TLP_Links.AutoSize = true;
			this.TLP_Links.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Links.ColumnCount = 1;
			this.TLP_Links.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Links.Controls.Add(this.L_Links, 0, 0);
			this.TLP_Links.Controls.Add(this.FLP_Links, 0, 1);
			this.TLP_Links.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Links.Location = new System.Drawing.Point(0, 464);
			this.TLP_Links.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Links.Name = "TLP_Links";
			this.TLP_Links.RowCount = 2;
			this.TLP_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Links.Size = new System.Drawing.Size(390, 25);
			this.TLP_Links.TabIndex = 20;
			// 
			// L_Links
			// 
			this.L_Links.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Links.AutoSize = true;
			this.L_Links.Location = new System.Drawing.Point(172, 0);
			this.L_Links.Name = "L_Links";
			this.L_Links.Size = new System.Drawing.Size(45, 19);
			this.L_Links.TabIndex = 0;
			this.L_Links.Text = "label4";
			// 
			// FLP_Links
			// 
			this.FLP_Links.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Links.Location = new System.Drawing.Point(3, 22);
			this.FLP_Links.Name = "FLP_Links";
			this.FLP_Links.Size = new System.Drawing.Size(384, 0);
			this.FLP_Links.TabIndex = 1;
			// 
			// TLP_ModInfo
			// 
			this.TLP_ModInfo.AutoSize = true;
			this.TLP_ModInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_ModInfo.ColumnCount = 2;
			this.TLP_ModInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.TLP_ModInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.TLP_ModInfo.Controls.Add(this.LI_Size, 1, 2);
			this.TLP_ModInfo.Controls.Add(this.LI_ModId, 0, 2);
			this.TLP_ModInfo.Controls.Add(this.LI_Subscribers, 1, 3);
			this.TLP_ModInfo.Controls.Add(this.LI_Votes, 0, 3);
			this.TLP_ModInfo.Controls.Add(this.LI_UpdateTime, 1, 1);
			this.TLP_ModInfo.Controls.Add(this.L_Info, 0, 0);
			this.TLP_ModInfo.Controls.Add(this.LI_Version, 0, 1);
			this.TLP_ModInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_ModInfo.Location = new System.Drawing.Point(0, 122);
			this.TLP_ModInfo.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_ModInfo.Name = "TLP_ModInfo";
			this.TLP_ModInfo.RowCount = 5;
			this.TLP_ModInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_ModInfo.Size = new System.Drawing.Size(390, 252);
			this.TLP_ModInfo.TabIndex = 16;
			// 
			// L_Info
			// 
			this.L_Info.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Info.AutoSize = true;
			this.TLP_ModInfo.SetColumnSpan(this.L_Info, 2);
			this.L_Info.Location = new System.Drawing.Point(172, 0);
			this.L_Info.Name = "L_Info";
			this.L_Info.Size = new System.Drawing.Size(45, 19);
			this.L_Info.TabIndex = 0;
			this.L_Info.Text = "label1";
			// 
			// TLP_TopInfo
			// 
			this.TLP_TopInfo.ColumnCount = 3;
			this.TLP_TopInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_TopInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_TopInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_TopInfo.Controls.Add(this.PB_Icon, 0, 0);
			this.TLP_TopInfo.Controls.Add(this.I_More, 2, 0);
			this.TLP_TopInfo.Controls.Add(this.L_Author, 1, 1);
			this.TLP_TopInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_TopInfo.Location = new System.Drawing.Point(0, 0);
			this.TLP_TopInfo.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_TopInfo.Name = "TLP_TopInfo";
			this.TLP_TopInfo.RowCount = 2;
			this.TLP_TopInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_TopInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_TopInfo.Size = new System.Drawing.Size(390, 102);
			this.TLP_TopInfo.TabIndex = 0;
			this.TLP_TopInfo.Tag = "NoMouseDown";
			this.TLP_TopInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.I_More_MouseClick);
			// 
			// I_More
			// 
			this.I_More.ActiveColor = null;
			this.I_More.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon8.Name = "I_VertialMore";
			this.I_More.ImageName = dynamicIcon8;
			this.I_More.Location = new System.Drawing.Point(376, 0);
			this.I_More.Margin = new System.Windows.Forms.Padding(0);
			this.I_More.Name = "I_More";
			this.I_More.Size = new System.Drawing.Size(14, 28);
			this.I_More.TabIndex = 2;
			this.I_More.MouseClick += new System.Windows.Forms.MouseEventHandler(this.I_More_MouseClick);
			// 
			// L_Author
			// 
			this.L_Author.AutoSize = true;
			this.L_Author.ColorShade = null;
			this.L_Author.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon9.Name = "I_Author";
			this.L_Author.ImageName = dynamicIcon9;
			this.L_Author.Location = new System.Drawing.Point(93, 69);
			this.L_Author.Name = "L_Author";
			this.L_Author.Selected = false;
			this.L_Author.Size = new System.Drawing.Size(30, 30);
			this.L_Author.SpaceTriggersClick = true;
			this.L_Author.TabIndex = 3;
			this.L_Author.Click += new System.EventHandler(this.L_Author_Click);
			// 
			// slickSpacer1
			// 
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(3, 105);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(384, 14);
			this.slickSpacer1.TabIndex = 1;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// TLP_ModRequirements
			// 
			this.TLP_ModRequirements.AutoSize = true;
			this.TLP_ModRequirements.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_ModRequirements.ColumnCount = 1;
			this.TLP_ModRequirements.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_ModRequirements.Controls.Add(this.L_Requirements, 0, 0);
			this.TLP_ModRequirements.Controls.Add(this.B_BulkRequirements, 0, 2);
			this.TLP_ModRequirements.Controls.Add(this.P_Requirements, 0, 1);
			this.TLP_ModRequirements.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_ModRequirements.Location = new System.Drawing.Point(0, 374);
			this.TLP_ModRequirements.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_ModRequirements.Name = "TLP_ModRequirements";
			this.TLP_ModRequirements.RowCount = 3;
			this.TLP_ModRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModRequirements.Size = new System.Drawing.Size(390, 65);
			this.TLP_ModRequirements.TabIndex = 18;
			// 
			// L_Requirements
			// 
			this.L_Requirements.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Requirements.AutoSize = true;
			this.L_Requirements.Location = new System.Drawing.Point(172, 0);
			this.L_Requirements.Name = "L_Requirements";
			this.L_Requirements.Size = new System.Drawing.Size(45, 19);
			this.L_Requirements.TabIndex = 0;
			this.L_Requirements.Text = "label2";
			// 
			// B_BulkRequirements
			// 
			this.B_BulkRequirements.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_BulkRequirements.AutoSize = true;
			this.B_BulkRequirements.ColorShade = null;
			this.B_BulkRequirements.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon10.Name = "I_Actions";
			this.B_BulkRequirements.ImageName = dynamicIcon10;
			this.B_BulkRequirements.Location = new System.Drawing.Point(132, 28);
			this.B_BulkRequirements.MatchBackgroundColor = true;
			this.B_BulkRequirements.Name = "B_BulkRequirements";
			this.B_BulkRequirements.Size = new System.Drawing.Size(125, 34);
			this.B_BulkRequirements.SpaceTriggersClick = true;
			this.B_BulkRequirements.TabIndex = 1;
			this.B_BulkRequirements.Text = "BulkActions";
			this.B_BulkRequirements.Click += new System.EventHandler(this.B_BulkRequirements_Click);
			// 
			// P_Requirements
			// 
			this.P_Requirements.AutoSize = true;
			this.P_Requirements.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Requirements.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Requirements.Location = new System.Drawing.Point(3, 22);
			this.P_Requirements.Name = "P_Requirements";
			this.P_Requirements.Size = new System.Drawing.Size(384, 0);
			this.P_Requirements.TabIndex = 2;
			// 
			// TLP_Tags
			// 
			this.TLP_Tags.AutoSize = true;
			this.TLP_Tags.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Tags.ColumnCount = 1;
			this.TLP_Tags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Tags.Controls.Add(this.L_Tags, 0, 0);
			this.TLP_Tags.Controls.Add(this.FLP_Tags, 0, 1);
			this.TLP_Tags.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Tags.Location = new System.Drawing.Point(0, 439);
			this.TLP_Tags.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Tags.Name = "TLP_Tags";
			this.TLP_Tags.RowCount = 2;
			this.TLP_Tags.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Tags.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Tags.Size = new System.Drawing.Size(390, 25);
			this.TLP_Tags.TabIndex = 19;
			// 
			// L_Tags
			// 
			this.L_Tags.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Tags.AutoSize = true;
			this.L_Tags.Location = new System.Drawing.Point(172, 0);
			this.L_Tags.Name = "L_Tags";
			this.L_Tags.Size = new System.Drawing.Size(45, 19);
			this.L_Tags.TabIndex = 0;
			this.L_Tags.Text = "label3";
			// 
			// FLP_Tags
			// 
			this.FLP_Tags.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Tags.Location = new System.Drawing.Point(3, 22);
			this.FLP_Tags.Name = "FLP_Tags";
			this.FLP_Tags.Size = new System.Drawing.Size(384, 0);
			this.FLP_Tags.TabIndex = 1;
			// 
			// T_Gallery
			// 
			this.T_Gallery.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Gallery.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Gallery.FillTab = true;
			dynamicIcon2.Name = "I_Gallery";
			this.T_Gallery.IconName = dynamicIcon2;
			this.T_Gallery.LinkedControl = this.carouselControl;
			this.T_Gallery.Location = new System.Drawing.Point(113, 5);
			this.T_Gallery.Name = "T_Gallery";
			this.T_Gallery.Selected = false;
			this.T_Gallery.Size = new System.Drawing.Size(113, 25);
			this.T_Gallery.TabIndex = 0;
			this.T_Gallery.TabStop = false;
			this.T_Gallery.Text = "Gallery";
			// 
			// carouselControl
			// 
			this.carouselControl.Location = new System.Drawing.Point(0, 0);
			this.carouselControl.Name = "carouselControl";
			this.carouselControl.Size = new System.Drawing.Size(796, 593);
			this.carouselControl.TabIndex = 9;
			// 
			// T_Changelog
			// 
			this.T_Changelog.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Changelog.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Changelog.FillTab = true;
			dynamicIcon7.Name = "I_Versions";
			this.T_Changelog.IconName = dynamicIcon7;
			this.T_Changelog.LinkedControl = this.packageChangelogControl1;
			this.T_Changelog.Location = new System.Drawing.Point(678, 5);
			this.T_Changelog.Name = "T_Changelog";
			this.T_Changelog.Selected = false;
			this.T_Changelog.Size = new System.Drawing.Size(113, 25);
			this.T_Changelog.TabIndex = 0;
			this.T_Changelog.TabStop = false;
			this.T_Changelog.Text = "Changelog";
			// 
			// packageChangelogControl1
			// 
			this.packageChangelogControl1.Location = new System.Drawing.Point(0, 0);
			this.packageChangelogControl1.Name = "packageChangelogControl1";
			this.packageChangelogControl1.Size = new System.Drawing.Size(902, 644);
			this.packageChangelogControl1.TabIndex = 15;
			// 
			// LI_Size
			// 
			this.LI_Size.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_Size.LabelText = "Sorting_FileSize";
			this.LI_Size.Location = new System.Drawing.Point(159, 93);
			this.LI_Size.Name = "LI_Size";
			this.LI_Size.Padding = new System.Windows.Forms.Padding(5);
			this.LI_Size.Size = new System.Drawing.Size(228, 65);
			this.LI_Size.TabIndex = 8;
			// 
			// LI_ModId
			// 
			this.LI_ModId.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_ModId.LabelText = "ModID";
			this.LI_ModId.Location = new System.Drawing.Point(3, 93);
			this.LI_ModId.Name = "LI_ModId";
			this.LI_ModId.Padding = new System.Windows.Forms.Padding(5);
			this.LI_ModId.Size = new System.Drawing.Size(150, 65);
			this.LI_ModId.TabIndex = 7;
			// 
			// LI_Subscribers
			// 
			this.LI_Subscribers.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_Subscribers.LabelText = "Subscribers";
			this.LI_Subscribers.Location = new System.Drawing.Point(159, 164);
			this.LI_Subscribers.Name = "LI_Subscribers";
			this.LI_Subscribers.Padding = new System.Windows.Forms.Padding(5);
			this.LI_Subscribers.Size = new System.Drawing.Size(228, 65);
			this.LI_Subscribers.TabIndex = 4;
			// 
			// LI_Votes
			// 
			this.LI_Votes.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_Votes.LabelText = "Votes";
			this.LI_Votes.Location = new System.Drawing.Point(3, 164);
			this.LI_Votes.Name = "LI_Votes";
			this.LI_Votes.Padding = new System.Windows.Forms.Padding(5);
			this.LI_Votes.Size = new System.Drawing.Size(150, 65);
			this.LI_Votes.TabIndex = 3;
			this.LI_Votes.ValueClicked += new System.EventHandler(this.LI_Votes_ValueClicked);
			this.LI_Votes.HoverStateChanged += new System.EventHandler<SlickControls.HoverState>(this.LI_Votes_HoverStateChanged);
			// 
			// LI_UpdateTime
			// 
			this.LI_UpdateTime.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_UpdateTime.LabelText = "UpdateTime";
			this.LI_UpdateTime.Location = new System.Drawing.Point(159, 22);
			this.LI_UpdateTime.Name = "LI_UpdateTime";
			this.LI_UpdateTime.Padding = new System.Windows.Forms.Padding(5);
			this.LI_UpdateTime.Size = new System.Drawing.Size(228, 65);
			this.LI_UpdateTime.TabIndex = 2;
			// 
			// LI_Version
			// 
			this.LI_Version.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_Version.LabelText = "Version";
			this.LI_Version.Location = new System.Drawing.Point(3, 22);
			this.LI_Version.Name = "LI_Version";
			this.LI_Version.Padding = new System.Windows.Forms.Padding(5);
			this.LI_Version.Size = new System.Drawing.Size(150, 65);
			this.LI_Version.TabIndex = 1;
			this.LI_Version.ValueText = "";
			// 
			// PB_Icon
			// 
			this.PB_Icon.Location = new System.Drawing.Point(0, 0);
			this.PB_Icon.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Icon.Name = "PB_Icon";
			this.TLP_TopInfo.SetRowSpan(this.PB_Icon, 2);
			this.PB_Icon.Size = new System.Drawing.Size(90, 102);
			this.PB_Icon.TabIndex = 0;
			this.PB_Icon.TabStop = false;
			this.PB_Icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.I_More_MouseClick);
			// 
			// PC_PackagePage
			// 
			this.Controls.Add(this.slickTabControl);
			this.Controls.Add(this.P_Side);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_PackagePage";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(996, 653);
			this.Text = "Back";
			this.Controls.SetChildIndex(this.P_Side, 0);
			this.Controls.SetChildIndex(this.slickTabControl, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.P_Side.ResumeLayout(false);
			this.P_Side.PerformLayout();
			this.TLP_Side.ResumeLayout(false);
			this.TLP_Side.PerformLayout();
			this.TLP_Links.ResumeLayout(false);
			this.TLP_Links.PerformLayout();
			this.TLP_ModInfo.ResumeLayout(false);
			this.TLP_ModInfo.PerformLayout();
			this.TLP_TopInfo.ResumeLayout(false);
			this.TLP_TopInfo.PerformLayout();
			this.TLP_ModRequirements.ResumeLayout(false);
			this.TLP_ModRequirements.PerformLayout();
			this.TLP_Tags.ResumeLayout(false);
			this.TLP_Tags.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private PackageIcon PB_Icon;
	private SlickControls.SlickTabControl slickTabControl;
	private SlickControls.SlickTabControl.Tab T_Info;
	public SlickControls.SlickTabControl.Tab T_Compatibility;
	private SlickControls.SlickTabControl.Tab T_Playsets;
	private SlickControls.SlickTabControl.Tab T_References;
	private System.Windows.Forms.Panel P_Side;
	private SlickControls.Controls.Advanced.SlickWebBrowser slickWebBrowser;
	private System.Windows.Forms.TableLayoutPanel TLP_TopInfo;
	private SlickIcon I_More;
	private RoundedTableLayoutPanel TLP_ModInfo;
	private System.Windows.Forms.Label L_Info;
	private SlickScroll slickScroll1;
	private System.Windows.Forms.TableLayoutPanel TLP_Side;
	private SlickSpacer slickSpacer1;
	private RoundedTableLayoutPanel TLP_ModRequirements;
	private System.Windows.Forms.Label L_Requirements;
	private RoundedTableLayoutPanel TLP_Tags;
	private System.Windows.Forms.Label L_Tags;
	private Content.InfoAndLabelControl LI_Subscribers;
	private Content.InfoAndLabelControl LI_Votes;
	private Content.InfoAndLabelControl LI_UpdateTime;
	private Content.InfoAndLabelControl LI_Version;
	private Content.InfoAndLabelControl LI_Size;
	private Content.InfoAndLabelControl LI_ModId;
	private SlickControls.SmartFlowPanel FLP_Tags;
	private SlickButton B_BulkRequirements;
	private System.Windows.Forms.Panel P_Requirements;
	private SlickLabel L_Author;
	private SlickTabControl.Tab T_Gallery;
	private RoundedTableLayoutPanel TLP_Links;
	private System.Windows.Forms.Label L_Links;
	private SmartFlowPanel FLP_Links;
	private SlickTabControl.Tab T_Content;
	private App.UserInterface.Generic.CarouselControl carouselControl;
	private SlickTabControl.Tab T_Changelog;
	private Content.PackageChangelogControl packageChangelogControl1;
}
