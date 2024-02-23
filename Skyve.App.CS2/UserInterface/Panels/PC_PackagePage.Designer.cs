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
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon11 = new SlickControls.DynamicIcon();
			this.slickTabControl = new SlickControls.SlickTabControl();
			this.T_Info = new SlickControls.SlickTabControl.Tab();
			this.slickWebBrowser = new SlickControls.Controls.Advanced.SlickWebBrowser();
			this.T_Gallery = new SlickControls.SlickTabControl.Tab();
			this.T_Compatibility = new SlickControls.SlickTabControl.Tab();
			this.T_References = new SlickControls.SlickTabControl.Tab();
			this.T_Playsets = new SlickControls.SlickTabControl.Tab();
			this.TLP_Profiles = new System.Windows.Forms.TableLayoutPanel();
			this.P_Side = new System.Windows.Forms.Panel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.TLP_Side = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_ModInfo = new SlickControls.RoundedTableLayoutPanel();
			this.LI_Size = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_ModId = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_Subscribers = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_Votes = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_UpdateTime = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.L_Info = new System.Windows.Forms.Label();
			this.LI_Version = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.TLP_TopInfo = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Icon = new Skyve.App.UserInterface.Content.PackageIcon();
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
			this.TLP_Links = new SlickControls.RoundedTableLayoutPanel();
			this.L_Links = new System.Windows.Forms.Label();
			this.FLP_Links = new SlickControls.SmartFlowPanel();
			this.P_Side.SuspendLayout();
			this.TLP_Side.SuspendLayout();
			this.TLP_ModInfo.SuspendLayout();
			this.TLP_TopInfo.SuspendLayout();
			this.TLP_ModRequirements.SuspendLayout();
			this.TLP_Tags.SuspendLayout();
			this.TLP_Links.SuspendLayout();
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
        this.T_Compatibility,
        this.T_References,
        this.T_Playsets};
			// 
			// T_Info
			// 
			this.T_Info.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Info.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Info.FillTab = true;
			this.T_Info.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dynamicIcon9.Name = "I_Content";
			this.T_Info.IconName = dynamicIcon9;
			this.T_Info.LinkedControl = this.slickWebBrowser;
			this.T_Info.Location = new System.Drawing.Point(0, 5);
			this.T_Info.Name = "T_Info";
			this.T_Info.Selected = true;
			this.T_Info.Size = new System.Drawing.Size(159, 25);
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
			// T_Gallery
			// 
			this.T_Gallery.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Gallery.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon1.Name = "I_Gallery";
			this.T_Gallery.IconName = dynamicIcon1;
			this.T_Gallery.LinkedControl = null;
			this.T_Gallery.Location = new System.Drawing.Point(159, 5);
			this.T_Gallery.Name = "T_Gallery";
			this.T_Gallery.Selected = false;
			this.T_Gallery.Size = new System.Drawing.Size(159, 25);
			this.T_Gallery.TabIndex = 0;
			this.T_Gallery.TabStop = false;
			this.T_Gallery.Text = "Gallery";
			// 
			// T_Compatibility
			// 
			this.T_Compatibility.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Compatibility.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Compatibility.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dynamicIcon2.Name = "I_CompatibilityReport";
			this.T_Compatibility.IconName = dynamicIcon2;
			this.T_Compatibility.LinkedControl = null;
			this.T_Compatibility.Location = new System.Drawing.Point(318, 5);
			this.T_Compatibility.Name = "T_Compatibility";
			this.T_Compatibility.Selected = false;
			this.T_Compatibility.Size = new System.Drawing.Size(159, 25);
			this.T_Compatibility.TabIndex = 0;
			this.T_Compatibility.TabStop = false;
			this.T_Compatibility.Text = "Compatibility";
			// 
			// T_References
			// 
			this.T_References.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_References.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_References.FillTab = true;
			dynamicIcon3.Name = "I_Share";
			this.T_References.IconName = dynamicIcon3;
			this.T_References.LinkedControl = null;
			this.T_References.Location = new System.Drawing.Point(477, 5);
			this.T_References.Name = "T_References";
			this.T_References.Selected = false;
			this.T_References.Size = new System.Drawing.Size(159, 25);
			this.T_References.TabIndex = 1;
			this.T_References.TabStop = false;
			this.T_References.Text = "References";
			// 
			// T_Playsets
			// 
			this.T_Playsets.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Playsets.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Playsets.Font = new System.Drawing.Font("Nirmala UI", 9F);
			dynamicIcon4.Name = "I_Playsets";
			this.T_Playsets.IconName = dynamicIcon4;
			this.T_Playsets.LinkedControl = this.TLP_Profiles;
			this.T_Playsets.Location = new System.Drawing.Point(636, 5);
			this.T_Playsets.Name = "T_Playsets";
			this.T_Playsets.Selected = false;
			this.T_Playsets.Size = new System.Drawing.Size(159, 25);
			this.T_Playsets.TabIndex = 0;
			this.T_Playsets.TabStop = false;
			this.T_Playsets.Text = "Playsets";
			// 
			// TLP_Profiles
			// 
			this.TLP_Profiles.ColumnCount = 2;
			this.TLP_Profiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Profiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Profiles.Location = new System.Drawing.Point(0, 0);
			this.TLP_Profiles.MaximumSize = new System.Drawing.Size(773, 2147483647);
			this.TLP_Profiles.MinimumSize = new System.Drawing.Size(773, 0);
			this.TLP_Profiles.Name = "TLP_Profiles";
			this.TLP_Profiles.RowCount = 2;
			this.TLP_Profiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Profiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Profiles.Size = new System.Drawing.Size(773, 208);
			this.TLP_Profiles.TabIndex = 16;
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
			this.slickScroll1.Location = new System.Drawing.Point(191, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(9, 623);
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
			this.TLP_Side.Size = new System.Drawing.Size(312, 439);
			this.TLP_Side.TabIndex = 1;
			// 
			// TLP_ModInfo
			// 
			this.TLP_ModInfo.AutoSize = true;
			this.TLP_ModInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_ModInfo.ColumnCount = 2;
			this.TLP_ModInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_ModInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
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
			this.TLP_ModInfo.RowCount = 3;
			this.TLP_ModInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModInfo.Size = new System.Drawing.Size(312, 226);
			this.TLP_ModInfo.TabIndex = 16;
			// 
			// LI_Size
			// 
			this.LI_Size.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_Size.LabelText = "Sorting_FileSize";
			this.LI_Size.Location = new System.Drawing.Point(159, 87);
			this.LI_Size.Name = "LI_Size";
			this.LI_Size.Padding = new System.Windows.Forms.Padding(5);
			this.LI_Size.Size = new System.Drawing.Size(150, 65);
			this.LI_Size.TabIndex = 8;
			// 
			// LI_ModId
			// 
			this.LI_ModId.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_ModId.LabelText = "ModID";
			this.LI_ModId.Location = new System.Drawing.Point(3, 87);
			this.LI_ModId.Name = "LI_ModId";
			this.LI_ModId.Padding = new System.Windows.Forms.Padding(5);
			this.LI_ModId.Size = new System.Drawing.Size(150, 65);
			this.LI_ModId.TabIndex = 7;
			// 
			// LI_Subscribers
			// 
			this.LI_Subscribers.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_Subscribers.LabelText = "Subscribers";
			this.LI_Subscribers.Location = new System.Drawing.Point(159, 158);
			this.LI_Subscribers.Name = "LI_Subscribers";
			this.LI_Subscribers.Padding = new System.Windows.Forms.Padding(5);
			this.LI_Subscribers.Size = new System.Drawing.Size(150, 65);
			this.LI_Subscribers.TabIndex = 4;
			// 
			// LI_Votes
			// 
			this.LI_Votes.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_Votes.LabelText = "Votes";
			this.LI_Votes.Location = new System.Drawing.Point(3, 158);
			this.LI_Votes.Name = "LI_Votes";
			this.LI_Votes.Padding = new System.Windows.Forms.Padding(5);
			this.LI_Votes.Size = new System.Drawing.Size(150, 65);
			this.LI_Votes.TabIndex = 3;
			// 
			// LI_UpdateTime
			// 
			this.LI_UpdateTime.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_UpdateTime.LabelText = "UpdateTime";
			this.LI_UpdateTime.Location = new System.Drawing.Point(159, 16);
			this.LI_UpdateTime.Name = "LI_UpdateTime";
			this.LI_UpdateTime.Padding = new System.Windows.Forms.Padding(5);
			this.LI_UpdateTime.Size = new System.Drawing.Size(150, 65);
			this.LI_UpdateTime.TabIndex = 2;
			// 
			// L_Info
			// 
			this.L_Info.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Info.AutoSize = true;
			this.TLP_ModInfo.SetColumnSpan(this.L_Info, 2);
			this.L_Info.Location = new System.Drawing.Point(137, 0);
			this.L_Info.Name = "L_Info";
			this.L_Info.Size = new System.Drawing.Size(38, 13);
			this.L_Info.TabIndex = 0;
			this.L_Info.Text = "label1";
			// 
			// LI_Version
			// 
			this.LI_Version.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_Version.LabelText = "Version";
			this.LI_Version.Location = new System.Drawing.Point(3, 16);
			this.LI_Version.Name = "LI_Version";
			this.LI_Version.Padding = new System.Windows.Forms.Padding(5);
			this.LI_Version.Size = new System.Drawing.Size(150, 59);
			this.LI_Version.TabIndex = 1;
			this.LI_Version.ValueText = "";
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
			this.TLP_TopInfo.Size = new System.Drawing.Size(312, 102);
			this.TLP_TopInfo.TabIndex = 0;
			this.TLP_TopInfo.Tag = "NoMouseDown";
			this.TLP_TopInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.I_More_MouseClick);
			// 
			// PB_Icon
			// 
			this.PB_Icon.HalfColor = false;
			this.PB_Icon.Location = new System.Drawing.Point(0, 0);
			this.PB_Icon.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Icon.Name = "PB_Icon";
			this.TLP_TopInfo.SetRowSpan(this.PB_Icon, 2);
			this.PB_Icon.Size = new System.Drawing.Size(90, 102);
			this.PB_Icon.TabIndex = 0;
			this.PB_Icon.TabStop = false;
			this.PB_Icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.I_More_MouseClick);
			// 
			// I_More
			// 
			this.I_More.ActiveColor = null;
			this.I_More.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "I_VertialMore";
			this.I_More.ImageName = dynamicIcon5;
			this.I_More.Location = new System.Drawing.Point(298, 0);
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
			dynamicIcon10.Name = "I_Author";
			this.L_Author.ImageName = dynamicIcon10;
			this.L_Author.Location = new System.Drawing.Point(93, 71);
			this.L_Author.Name = "L_Author";
			this.L_Author.Selected = false;
			this.L_Author.Size = new System.Drawing.Size(28, 28);
			this.L_Author.SpaceTriggersClick = true;
			this.L_Author.TabIndex = 3;
			this.L_Author.Click += new System.EventHandler(this.L_Author_Click);
			// 
			// slickSpacer1
			// 
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(3, 105);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(306, 14);
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
			this.TLP_ModRequirements.Location = new System.Drawing.Point(0, 348);
			this.TLP_ModRequirements.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_ModRequirements.Name = "TLP_ModRequirements";
			this.TLP_ModRequirements.RowCount = 3;
			this.TLP_ModRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModRequirements.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_ModRequirements.Size = new System.Drawing.Size(312, 53);
			this.TLP_ModRequirements.TabIndex = 18;
			// 
			// L_Requirements
			// 
			this.L_Requirements.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Requirements.AutoSize = true;
			this.L_Requirements.Location = new System.Drawing.Point(137, 0);
			this.L_Requirements.Name = "L_Requirements";
			this.L_Requirements.Size = new System.Drawing.Size(38, 13);
			this.L_Requirements.TabIndex = 0;
			this.L_Requirements.Text = "label2";
			// 
			// B_BulkRequirements
			// 
			this.B_BulkRequirements.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.B_BulkRequirements.AutoSize = true;
			this.B_BulkRequirements.ColorShade = null;
			this.B_BulkRequirements.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon11.Name = "I_Actions";
			this.B_BulkRequirements.ImageName = dynamicIcon11;
			this.B_BulkRequirements.Location = new System.Drawing.Point(103, 22);
			this.B_BulkRequirements.MatchBackgroundColor = true;
			this.B_BulkRequirements.Name = "B_BulkRequirements";
			this.B_BulkRequirements.Size = new System.Drawing.Size(105, 28);
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
			this.P_Requirements.Location = new System.Drawing.Point(3, 16);
			this.P_Requirements.Name = "P_Requirements";
			this.P_Requirements.Size = new System.Drawing.Size(306, 0);
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
			this.TLP_Tags.Location = new System.Drawing.Point(0, 401);
			this.TLP_Tags.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Tags.Name = "TLP_Tags";
			this.TLP_Tags.RowCount = 2;
			this.TLP_Tags.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Tags.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Tags.Size = new System.Drawing.Size(312, 19);
			this.TLP_Tags.TabIndex = 19;
			// 
			// L_Tags
			// 
			this.L_Tags.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Tags.AutoSize = true;
			this.L_Tags.Location = new System.Drawing.Point(137, 0);
			this.L_Tags.Name = "L_Tags";
			this.L_Tags.Size = new System.Drawing.Size(38, 13);
			this.L_Tags.TabIndex = 0;
			this.L_Tags.Text = "label3";
			// 
			// FLP_Tags
			// 
			this.FLP_Tags.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Tags.Location = new System.Drawing.Point(3, 16);
			this.FLP_Tags.Name = "FLP_Tags";
			this.FLP_Tags.Size = new System.Drawing.Size(306, 0);
			this.FLP_Tags.TabIndex = 1;
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
			this.TLP_Links.Location = new System.Drawing.Point(0, 420);
			this.TLP_Links.Margin = new System.Windows.Forms.Padding(0);
			this.TLP_Links.Name = "TLP_Links";
			this.TLP_Links.RowCount = 2;
			this.TLP_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Links.Size = new System.Drawing.Size(312, 19);
			this.TLP_Links.TabIndex = 20;
			// 
			// L_Links
			// 
			this.L_Links.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Links.AutoSize = true;
			this.L_Links.Location = new System.Drawing.Point(137, 0);
			this.L_Links.Name = "L_Links";
			this.L_Links.Size = new System.Drawing.Size(38, 13);
			this.L_Links.TabIndex = 0;
			this.L_Links.Text = "label4";
			// 
			// FLP_Links
			// 
			this.FLP_Links.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Links.Location = new System.Drawing.Point(3, 16);
			this.FLP_Links.Name = "FLP_Links";
			this.FLP_Links.Size = new System.Drawing.Size(306, 0);
			this.FLP_Links.TabIndex = 1;
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
			this.TLP_ModInfo.ResumeLayout(false);
			this.TLP_ModInfo.PerformLayout();
			this.TLP_TopInfo.ResumeLayout(false);
			this.TLP_TopInfo.PerformLayout();
			this.TLP_ModRequirements.ResumeLayout(false);
			this.TLP_ModRequirements.PerformLayout();
			this.TLP_Tags.ResumeLayout(false);
			this.TLP_Tags.PerformLayout();
			this.TLP_Links.ResumeLayout(false);
			this.TLP_Links.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private PackageIcon PB_Icon;
	private SlickControls.SlickTabControl slickTabControl;
	private SlickControls.SlickTabControl.Tab T_Info;
	public SlickControls.SlickTabControl.Tab T_Compatibility;
	private SlickControls.SlickTabControl.Tab T_Playsets;
	private System.Windows.Forms.TableLayoutPanel TLP_Profiles;
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
}
