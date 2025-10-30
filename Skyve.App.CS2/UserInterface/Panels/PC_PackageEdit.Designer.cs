using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Generic;
using Skyve.Domain;
using Skyve.Domain.Systems;

namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_PackageEdit
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
			this.components = new System.ComponentModel.Container();
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon11 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon12 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon16 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon13 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon14 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon15 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon20 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon17 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon18 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon19 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon21 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon23 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon22 = new SlickControls.DynamicIcon();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PC_PackageEdit));
			SlickControls.DynamicIcon dynamicIcon24 = new SlickControls.DynamicIcon();
			this.P_Side = new System.Windows.Forms.Panel();
			this.base_slickScroll = new SlickControls.SlickScroll();
			this.TLP_Side = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_TopInfo = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Icon = new Skyve.App.UserInterface.Content.PackageIcon();
			this.I_More = new SlickControls.SlickIcon();
			this.L_Author = new Skyve.App.UserInterface.Generic.AuthorControl();
			this.base_slickSpacer = new SlickControls.SlickSpacer();
			this.B_Publish = new SlickControls.SlickButton();
			this.P_SideContainer = new System.Windows.Forms.Panel();
			this.slickTabControl1 = new SlickControls.SlickTabControl();
			this.T_TitleDesc = new SlickControls.SlickTabControl.Tab();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Title = new SlickControls.SlickTextBox();
			this.TB_ShortDesc = new SlickControls.SlickTextBox();
			this.TB_LongDesc = new SlickControls.SlickTextBox();
			this.B_List = new SlickControls.SlickButton();
			this.B_Underline = new SlickControls.SlickButton();
			this.B_Italic = new SlickControls.SlickButton();
			this.B_Bold = new SlickControls.SlickButton();
			this.B_Header3 = new SlickControls.SlickButton();
			this.B_Header2 = new SlickControls.SlickButton();
			this.B_Header1 = new SlickControls.SlickButton();
			this.T_Metadata = new SlickControls.SlickTabControl.Tab();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Versions = new SlickControls.RoundedGroupTableLayoutPanel();
			this.TB_GameVersion = new SlickControls.SlickTextBox();
			this.TB_UserVersion = new SlickControls.SlickTextBox();
			this.TLP_Links = new SlickControls.RoundedGroupTableLayoutPanel();
			this.I_CopyLinks = new SlickControls.SlickIcon();
			this.I_PasteLinks = new SlickControls.SlickIcon();
			this.I_AddLinks = new SlickControls.SlickIcon();
			this.P_Links = new SlickControls.SmartFlowPanel();
			this.slickSpacer3 = new SlickControls.SlickSpacer();
			this.TB_ForumLink = new SlickControls.SlickTextBox();
			this.L_NoLinks = new System.Windows.Forms.Label();
			this.TLP_Dependencies = new SlickControls.RoundedGroupTableLayoutPanel();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.L_NoPackages = new System.Windows.Forms.Label();
			this.DD_Dlcs = new Skyve.App.CS2.UserInterface.Generic.PdxModsDlcDropdown();
			this.I_CopyPackages = new SlickControls.SlickIcon();
			this.I_PastePackages = new SlickControls.SlickIcon();
			this.I_AddPackages = new SlickControls.SlickIcon();
			this.P_ModDependencies = new System.Windows.Forms.Panel();
			this.accessLevelControlDropdown1 = new Skyve.App.CS2.UserInterface.Generic.AccessLevelControlDropdown();
			this.T_Images = new SlickControls.SlickTabControl.Tab();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_Screenshots = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_AddScreenshot = new SlickControls.SlickButton();
			this.screenshotEditControl = new Skyve.App.CS2.UserInterface.Generic.ScreenshotEditControl();
			this.TLP_Thumb = new SlickControls.RoundedGroupTableLayoutPanel();
			this.PB_Thumbnail = new SlickControls.SlickPictureBox();
			this.ioSelectionDialog = new SlickControls.IOSelectionDialog();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.P_Side.SuspendLayout();
			this.TLP_Side.SuspendLayout();
			this.TLP_TopInfo.SuspendLayout();
			this.P_SideContainer.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.TLP_Versions.SuspendLayout();
			this.TLP_Links.SuspendLayout();
			this.TLP_Dependencies.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.TLP_Screenshots.SuspendLayout();
			this.TLP_Thumb.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Thumbnail)).BeginInit();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(150, 41);
			this.base_Text.Text = "Back";
			// 
			// P_Side
			// 
			this.P_Side.Controls.Add(this.base_slickScroll);
			this.P_Side.Controls.Add(this.TLP_Side);
			this.P_Side.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Side.Location = new System.Drawing.Point(0, 0);
			this.P_Side.Name = "P_Side";
			this.P_Side.Size = new System.Drawing.Size(422, 982);
			this.P_Side.TabIndex = 14;
			// 
			// base_slickScroll
			// 
			this.base_slickScroll.AnimatedValue = 10;
			this.base_slickScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.base_slickScroll.LinkedControl = this.TLP_Side;
			this.base_slickScroll.Location = new System.Drawing.Point(402, 0);
			this.base_slickScroll.Name = "base_slickScroll";
			this.base_slickScroll.Size = new System.Drawing.Size(20, 982);
			this.base_slickScroll.Style = SlickControls.StyleType.Vertical;
			this.base_slickScroll.TabIndex = 2;
			this.base_slickScroll.TabStop = false;
			this.base_slickScroll.TargetAnimationValue = 10;
			this.base_slickScroll.Text = "slickScroll1";
			// 
			// TLP_Side
			// 
			this.TLP_Side.AutoSize = true;
			this.TLP_Side.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Side.ColumnCount = 1;
			this.TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Side.Controls.Add(this.TLP_TopInfo, 0, 0);
			this.TLP_Side.Controls.Add(this.base_slickSpacer, 0, 1);
			this.TLP_Side.Controls.Add(this.B_Publish, 0, 2);
			this.TLP_Side.Location = new System.Drawing.Point(0, 0);
			this.TLP_Side.Name = "TLP_Side";
			this.TLP_Side.RowCount = 3;
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.Size = new System.Drawing.Size(390, 162);
			this.TLP_Side.TabIndex = 1;
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
			// I_More
			// 
			this.I_More.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "VertialMore";
			this.I_More.ImageName = dynamicIcon1;
			this.I_More.Location = new System.Drawing.Point(376, 0);
			this.I_More.Margin = new System.Windows.Forms.Padding(0);
			this.I_More.Name = "I_More";
			this.I_More.Size = new System.Drawing.Size(14, 28);
			this.I_More.TabIndex = 2;
			this.I_More.MouseClick += new System.Windows.Forms.MouseEventHandler(this.I_More_MouseClick);
			// 
			// L_Author
			// 
			this.L_Author.Author = null;
			this.L_Author.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Author.Location = new System.Drawing.Point(93, 98);
			this.L_Author.Name = "L_Author";
			this.L_Author.Size = new System.Drawing.Size(24, 1);
			this.L_Author.SpaceTriggersClick = true;
			this.L_Author.TabIndex = 3;
			this.L_Author.Click += new System.EventHandler(this.L_Author_Click);
			// 
			// base_slickSpacer
			// 
			this.base_slickSpacer.Dock = System.Windows.Forms.DockStyle.Top;
			this.base_slickSpacer.Location = new System.Drawing.Point(3, 105);
			this.base_slickSpacer.Name = "base_slickSpacer";
			this.base_slickSpacer.Size = new System.Drawing.Size(384, 14);
			this.base_slickSpacer.TabIndex = 1;
			this.base_slickSpacer.TabStop = false;
			this.base_slickSpacer.Text = "slickSpacer1";
			// 
			// B_Publish
			// 
			this.B_Publish.AutoSize = true;
			this.B_Publish.ButtonType = SlickControls.ButtonType.Active;
			this.B_Publish.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Publish.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon2.Name = "Check";
			this.B_Publish.ImageName = dynamicIcon2;
			this.B_Publish.Location = new System.Drawing.Point(3, 125);
			this.B_Publish.Name = "B_Publish";
			this.B_Publish.Size = new System.Drawing.Size(384, 34);
			this.B_Publish.SpaceTriggersClick = true;
			this.B_Publish.TabIndex = 2;
			this.B_Publish.Text = "Apply Changes";
			this.B_Publish.Click += new System.EventHandler(this.B_Publish_Click);
			// 
			// P_SideContainer
			// 
			this.P_SideContainer.Controls.Add(this.P_Side);
			this.P_SideContainer.Dock = System.Windows.Forms.DockStyle.Right;
			this.P_SideContainer.Location = new System.Drawing.Point(748, 30);
			this.P_SideContainer.Name = "P_SideContainer";
			this.P_SideContainer.Size = new System.Drawing.Size(422, 982);
			this.P_SideContainer.TabIndex = 15;
			// 
			// slickTabControl1
			// 
			this.slickTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl1.Location = new System.Drawing.Point(0, 30);
			this.slickTabControl1.Name = "slickTabControl1";
			this.slickTabControl1.Size = new System.Drawing.Size(748, 982);
			this.slickTabControl1.TabIndex = 16;
			this.slickTabControl1.Tabs = new SlickControls.SlickTabControl.Tab[] {
        this.T_TitleDesc,
        this.T_Metadata,
        this.T_Images};
			// 
			// T_TitleDesc
			// 
			this.T_TitleDesc.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_TitleDesc.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_TitleDesc.FillTab = true;
			dynamicIcon3.Name = "Text";
			this.T_TitleDesc.IconName = dynamicIcon3;
			this.T_TitleDesc.LinkedControl = this.tableLayoutPanel1;
			this.T_TitleDesc.Location = new System.Drawing.Point(0, 5);
			this.T_TitleDesc.Name = "T_TitleDesc";
			this.T_TitleDesc.Size = new System.Drawing.Size(206, 90);
			this.T_TitleDesc.TabIndex = 2;
			this.T_TitleDesc.TabStop = false;
			this.T_TitleDesc.Text = "TitleDesc";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 8;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.TB_Title, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.TB_ShortDesc, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.TB_LongDesc, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.B_List, 7, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_Underline, 6, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_Italic, 5, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_Bold, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_Header3, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_Header2, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_Header1, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(748, 887);
			this.tableLayoutPanel1.TabIndex = 17;
			// 
			// TB_Title
			// 
			this.TB_Title.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_Title.LabelText = "Name";
			this.TB_Title.Location = new System.Drawing.Point(3, 3);
			this.TB_Title.MaxLength = 60;
			this.TB_Title.Name = "TB_Title";
			this.TB_Title.Padding = new System.Windows.Forms.Padding(7, 25, 7, 7);
			this.TB_Title.Placeholder = "The name of your mod";
			this.TB_Title.Required = true;
			this.TB_Title.SelectedText = "";
			this.TB_Title.SelectionLength = 0;
			this.TB_Title.SelectionStart = 0;
			this.TB_Title.Size = new System.Drawing.Size(490, 69);
			this.TB_Title.TabIndex = 0;
			// 
			// TB_ShortDesc
			// 
			this.TB_ShortDesc.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_ShortDesc.LabelText = "Short Description";
			this.TB_ShortDesc.Location = new System.Drawing.Point(3, 78);
			this.TB_ShortDesc.MaxLength = 200;
			this.TB_ShortDesc.MultiLine = true;
			this.TB_ShortDesc.Name = "TB_ShortDesc";
			this.TB_ShortDesc.Padding = new System.Windows.Forms.Padding(7, 25, 7, 7);
			this.TB_ShortDesc.Placeholder = "A description shown in popups and link previews";
			this.TB_ShortDesc.Required = true;
			this.TB_ShortDesc.SelectedText = "";
			this.TB_ShortDesc.SelectionLength = 0;
			this.TB_ShortDesc.SelectionStart = 0;
			this.TB_ShortDesc.Size = new System.Drawing.Size(490, 166);
			this.TB_ShortDesc.TabIndex = 1;
			// 
			// TB_LongDesc
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.TB_LongDesc, 8);
			this.TB_LongDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TB_LongDesc.LabelText = "Long Description";
			this.TB_LongDesc.Location = new System.Drawing.Point(3, 250);
			this.TB_LongDesc.MaxLength = 20000;
			this.TB_LongDesc.MultiLine = true;
			this.TB_LongDesc.Name = "TB_LongDesc";
			this.TB_LongDesc.Padding = new System.Windows.Forms.Padding(7, 25, 7, 7);
			this.TB_LongDesc.Placeholder = "A description shown in the mod\'s page, giving as much information as possible";
			this.TB_LongDesc.Required = true;
			this.TB_LongDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TB_LongDesc.SelectedText = "";
			this.TB_LongDesc.SelectionLength = 0;
			this.TB_LongDesc.SelectionStart = 0;
			this.TB_LongDesc.Size = new System.Drawing.Size(742, 634);
			this.TB_LongDesc.TabIndex = 2;
			// 
			// B_List
			// 
			this.B_List.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.B_List.AutoSize = true;
			this.B_List.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "List";
			this.B_List.ImageName = dynamicIcon4;
			this.B_List.Location = new System.Drawing.Point(715, 214);
			this.B_List.Name = "B_List";
			this.B_List.Size = new System.Drawing.Size(30, 30);
			this.B_List.SpaceTriggersClick = true;
			this.B_List.TabIndex = 21;
			this.B_List.Click += new System.EventHandler(this.B_List_Click);
			// 
			// B_Underline
			// 
			this.B_Underline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.B_Underline.AutoSize = true;
			this.B_Underline.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "Underline";
			this.B_Underline.ImageName = dynamicIcon5;
			this.B_Underline.Location = new System.Drawing.Point(679, 214);
			this.B_Underline.Name = "B_Underline";
			this.B_Underline.Size = new System.Drawing.Size(30, 30);
			this.B_Underline.SpaceTriggersClick = true;
			this.B_Underline.TabIndex = 19;
			this.B_Underline.Click += new System.EventHandler(this.B_Underline_Click);
			// 
			// B_Italic
			// 
			this.B_Italic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.B_Italic.AutoSize = true;
			this.B_Italic.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon6.Name = "Italic";
			this.B_Italic.ImageName = dynamicIcon6;
			this.B_Italic.Location = new System.Drawing.Point(643, 214);
			this.B_Italic.Name = "B_Italic";
			this.B_Italic.Size = new System.Drawing.Size(30, 30);
			this.B_Italic.SpaceTriggersClick = true;
			this.B_Italic.TabIndex = 18;
			this.B_Italic.Click += new System.EventHandler(this.B_Italic_Click);
			// 
			// B_Bold
			// 
			this.B_Bold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.B_Bold.AutoSize = true;
			this.B_Bold.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon7.Name = "Bold";
			this.B_Bold.ImageName = dynamicIcon7;
			this.B_Bold.Location = new System.Drawing.Point(607, 214);
			this.B_Bold.Name = "B_Bold";
			this.B_Bold.Size = new System.Drawing.Size(30, 30);
			this.B_Bold.SpaceTriggersClick = true;
			this.B_Bold.TabIndex = 17;
			this.B_Bold.Click += new System.EventHandler(this.B_Bold_Click);
			// 
			// B_Header3
			// 
			this.B_Header3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.B_Header3.AutoSize = true;
			this.B_Header3.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon8.Name = "Header3";
			this.B_Header3.ImageName = dynamicIcon8;
			this.B_Header3.Location = new System.Drawing.Point(571, 214);
			this.B_Header3.Name = "B_Header3";
			this.B_Header3.Size = new System.Drawing.Size(30, 30);
			this.B_Header3.SpaceTriggersClick = true;
			this.B_Header3.TabIndex = 22;
			this.B_Header3.Click += new System.EventHandler(this.B_Header3_Click);
			// 
			// B_Header2
			// 
			this.B_Header2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.B_Header2.AutoSize = true;
			this.B_Header2.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon9.Name = "Header2";
			this.B_Header2.ImageName = dynamicIcon9;
			this.B_Header2.Location = new System.Drawing.Point(535, 214);
			this.B_Header2.Name = "B_Header2";
			this.B_Header2.Size = new System.Drawing.Size(30, 30);
			this.B_Header2.SpaceTriggersClick = true;
			this.B_Header2.TabIndex = 22;
			this.B_Header2.Click += new System.EventHandler(this.B_Header2_Click);
			// 
			// B_Header1
			// 
			this.B_Header1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.B_Header1.AutoSize = true;
			this.B_Header1.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon10.Name = "Header1";
			this.B_Header1.ImageName = dynamicIcon10;
			this.B_Header1.Location = new System.Drawing.Point(499, 214);
			this.B_Header1.Name = "B_Header1";
			this.B_Header1.Size = new System.Drawing.Size(30, 30);
			this.B_Header1.SpaceTriggersClick = true;
			this.B_Header1.TabIndex = 22;
			this.B_Header1.Click += new System.EventHandler(this.B_Header1_Click);
			// 
			// T_Metadata
			// 
			this.T_Metadata.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Metadata.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon11.Name = "Content";
			this.T_Metadata.IconName = dynamicIcon11;
			this.T_Metadata.LinkedControl = this.tableLayoutPanel2;
			this.T_Metadata.Location = new System.Drawing.Point(206, 5);
			this.T_Metadata.Name = "T_Metadata";
			this.T_Metadata.Size = new System.Drawing.Size(206, 90);
			this.T_Metadata.TabIndex = 1;
			this.T_Metadata.TabStop = false;
			this.T_Metadata.Text = "Details";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.TLP_Versions, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.TLP_Links, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.TLP_Dependencies, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.accessLevelControlDropdown1, 0, 3);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 4;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(552, 564);
			this.tableLayoutPanel2.TabIndex = 18;
			// 
			// TLP_Versions
			// 
			this.TLP_Versions.AddShadow = true;
			this.TLP_Versions.AutoSize = true;
			this.TLP_Versions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Versions.ColumnCount = 2;
			this.TLP_Versions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Versions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Versions.Controls.Add(this.TB_GameVersion, 1, 0);
			this.TLP_Versions.Controls.Add(this.TB_UserVersion, 0, 0);
			this.TLP_Versions.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon12.Name = "Pages";
			this.TLP_Versions.ImageName = dynamicIcon12;
			this.TLP_Versions.Location = new System.Drawing.Point(3, 3);
			this.TLP_Versions.Name = "TLP_Versions";
			this.TLP_Versions.Padding = new System.Windows.Forms.Padding(22, 61, 22, 22);
			this.TLP_Versions.RowCount = 1;
			this.TLP_Versions.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Versions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Versions.Size = new System.Drawing.Size(546, 158);
			this.TLP_Versions.TabIndex = 17;
			this.TLP_Versions.Text = "Versions";
			// 
			// TB_GameVersion
			// 
			this.TB_GameVersion.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_GameVersion.LabelText = "Compatible Game Version";
			this.TB_GameVersion.Location = new System.Drawing.Point(276, 64);
			this.TB_GameVersion.MaxLength = 50;
			this.TB_GameVersion.Name = "TB_GameVersion";
			this.TB_GameVersion.Padding = new System.Windows.Forms.Padding(7, 25, 7, 7);
			this.TB_GameVersion.Placeholder = "The version of the game that your mod works with, use * for wildcard";
			this.TB_GameVersion.Required = true;
			this.TB_GameVersion.SelectedText = "";
			this.TB_GameVersion.SelectionLength = 0;
			this.TB_GameVersion.SelectionStart = 0;
			this.TB_GameVersion.Size = new System.Drawing.Size(245, 69);
			this.TB_GameVersion.TabIndex = 8;
			// 
			// TB_UserVersion
			// 
			this.TB_UserVersion.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_UserVersion.LabelText = "Display Version";
			this.TB_UserVersion.Location = new System.Drawing.Point(25, 64);
			this.TB_UserVersion.MaxLength = 50;
			this.TB_UserVersion.Name = "TB_UserVersion";
			this.TB_UserVersion.Padding = new System.Windows.Forms.Padding(7, 25, 7, 7);
			this.TB_UserVersion.Placeholder = "The version displayed for users in the UI";
			this.TB_UserVersion.Required = true;
			this.TB_UserVersion.SelectedText = "";
			this.TB_UserVersion.SelectionLength = 0;
			this.TB_UserVersion.SelectionStart = 0;
			this.TB_UserVersion.Size = new System.Drawing.Size(245, 69);
			this.TB_UserVersion.TabIndex = 7;
			// 
			// TLP_Links
			// 
			this.TLP_Links.AddShadow = true;
			this.TLP_Links.AutoSize = true;
			this.TLP_Links.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Links.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Links.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Links.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Links.Controls.Add(this.I_CopyLinks, 0, 0);
			this.TLP_Links.Controls.Add(this.I_PasteLinks, 1, 0);
			this.TLP_Links.Controls.Add(this.I_AddLinks, 2, 0);
			this.TLP_Links.Controls.Add(this.P_Links, 0, 2);
			this.TLP_Links.Controls.Add(this.slickSpacer3, 0, 3);
			this.TLP_Links.Controls.Add(this.TB_ForumLink, 0, 4);
			this.TLP_Links.Controls.Add(this.L_NoLinks, 0, 1);
			this.TLP_Links.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon16.Name = "Link";
			this.TLP_Links.ImageName = dynamicIcon16;
			this.TLP_Links.Location = new System.Drawing.Point(3, 363);
			this.TLP_Links.Name = "TLP_Links";
			this.TLP_Links.Padding = new System.Windows.Forms.Padding(22);
			this.TLP_Links.RowCount = 5;
			this.TLP_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Links.Size = new System.Drawing.Size(546, 178);
			this.TLP_Links.TabIndex = 2;
			this.TLP_Links.Text = "Links";
			this.TLP_Links.UseFirstRowForPadding = true;
			// 
			// I_CopyLinks
			// 
			this.I_CopyLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.I_CopyLinks.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_CopyLinks.HasAction = true;
			dynamicIcon13.Name = "Copy";
			this.I_CopyLinks.ImageName = dynamicIcon13;
			this.I_CopyLinks.Location = new System.Drawing.Point(287, 25);
			this.I_CopyLinks.Name = "I_CopyLinks";
			this.I_CopyLinks.Size = new System.Drawing.Size(74, 14);
			this.I_CopyLinks.TabIndex = 28;
			this.I_CopyLinks.Click += new System.EventHandler(this.I_CopyLinks_Click);
			// 
			// I_PasteLinks
			// 
			this.I_PasteLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.I_PasteLinks.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_PasteLinks.HasAction = true;
			dynamicIcon14.Name = "Paste";
			this.I_PasteLinks.ImageName = dynamicIcon14;
			this.I_PasteLinks.Location = new System.Drawing.Point(367, 25);
			this.I_PasteLinks.Name = "I_PasteLinks";
			this.I_PasteLinks.Size = new System.Drawing.Size(74, 14);
			this.I_PasteLinks.TabIndex = 27;
			this.I_PasteLinks.Click += new System.EventHandler(this.I_PasteLinks_Click);
			// 
			// I_AddLinks
			// 
			this.I_AddLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.I_AddLinks.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_AddLinks.HasAction = true;
			dynamicIcon15.Name = "Edit";
			this.I_AddLinks.ImageName = dynamicIcon15;
			this.I_AddLinks.Location = new System.Drawing.Point(447, 25);
			this.I_AddLinks.Name = "I_AddLinks";
			this.I_AddLinks.Size = new System.Drawing.Size(74, 14);
			this.I_AddLinks.TabIndex = 26;
			this.I_AddLinks.Click += new System.EventHandler(this.I_AddLinks_Click);
			// 
			// P_Links
			// 
			this.TLP_Links.SetColumnSpan(this.P_Links, 3);
			this.P_Links.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_Links.Location = new System.Drawing.Point(22, 61);
			this.P_Links.Margin = new System.Windows.Forms.Padding(0);
			this.P_Links.Name = "P_Links";
			this.P_Links.Size = new System.Drawing.Size(502, 0);
			this.P_Links.TabIndex = 25;
			// 
			// slickSpacer3
			// 
			this.TLP_Links.SetColumnSpan(this.slickSpacer3, 3);
			this.slickSpacer3.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer3.Location = new System.Drawing.Point(25, 64);
			this.slickSpacer3.Name = "slickSpacer3";
			this.slickSpacer3.Size = new System.Drawing.Size(496, 14);
			this.slickSpacer3.TabIndex = 5;
			this.slickSpacer3.TabStop = false;
			this.slickSpacer3.Text = "slickSpacer3";
			// 
			// TB_ForumLink
			// 
			this.TLP_Links.SetColumnSpan(this.TB_ForumLink, 3);
			this.TB_ForumLink.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_ForumLink.LabelText = "Forum Link";
			this.TB_ForumLink.Location = new System.Drawing.Point(25, 84);
			this.TB_ForumLink.MaxLength = 250;
			this.TB_ForumLink.Name = "TB_ForumLink";
			this.TB_ForumLink.Padding = new System.Windows.Forms.Padding(7, 25, 7, 7);
			this.TB_ForumLink.Placeholder = "Link a forum thread to enable comments your mod";
			this.TB_ForumLink.Required = true;
			this.TB_ForumLink.SelectedText = "";
			this.TB_ForumLink.SelectionLength = 0;
			this.TB_ForumLink.SelectionStart = 0;
			this.TB_ForumLink.Size = new System.Drawing.Size(496, 69);
			this.TB_ForumLink.TabIndex = 4;
			// 
			// L_NoLinks
			// 
			this.L_NoLinks.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoLinks.AutoSize = true;
			this.TLP_Links.SetColumnSpan(this.L_NoLinks, 3);
			this.L_NoLinks.Location = new System.Drawing.Point(250, 42);
			this.L_NoLinks.Name = "L_NoLinks";
			this.L_NoLinks.Size = new System.Drawing.Size(45, 19);
			this.L_NoLinks.TabIndex = 3;
			this.L_NoLinks.Text = "label1";
			// 
			// TLP_Dependencies
			// 
			this.TLP_Dependencies.AddShadow = true;
			this.TLP_Dependencies.AutoSize = true;
			this.TLP_Dependencies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Dependencies.ColumnCount = 3;
			this.TLP_Dependencies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Dependencies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Dependencies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Dependencies.Controls.Add(this.slickSpacer2, 0, 3);
			this.TLP_Dependencies.Controls.Add(this.L_NoPackages, 0, 1);
			this.TLP_Dependencies.Controls.Add(this.DD_Dlcs, 0, 4);
			this.TLP_Dependencies.Controls.Add(this.I_CopyPackages, 0, 0);
			this.TLP_Dependencies.Controls.Add(this.I_PastePackages, 1, 0);
			this.TLP_Dependencies.Controls.Add(this.I_AddPackages, 2, 0);
			this.TLP_Dependencies.Controls.Add(this.P_ModDependencies, 0, 2);
			this.TLP_Dependencies.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon20.Name = "Share";
			this.TLP_Dependencies.ImageName = dynamicIcon20;
			this.TLP_Dependencies.Location = new System.Drawing.Point(3, 167);
			this.TLP_Dependencies.Name = "TLP_Dependencies";
			this.TLP_Dependencies.Padding = new System.Windows.Forms.Padding(22);
			this.TLP_Dependencies.RowCount = 4;
			this.TLP_Dependencies.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Dependencies.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Dependencies.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Dependencies.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Dependencies.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Dependencies.Size = new System.Drawing.Size(546, 190);
			this.TLP_Dependencies.TabIndex = 18;
			this.TLP_Dependencies.Text = "Dependencies";
			this.TLP_Dependencies.UseFirstRowForPadding = true;
			// 
			// slickSpacer2
			// 
			this.TLP_Dependencies.SetColumnSpan(this.slickSpacer2, 3);
			this.slickSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer2.Location = new System.Drawing.Point(25, 64);
			this.slickSpacer2.Name = "slickSpacer2";
			this.slickSpacer2.Size = new System.Drawing.Size(496, 23);
			this.slickSpacer2.TabIndex = 26;
			this.slickSpacer2.TabStop = false;
			this.slickSpacer2.Text = "slickSpacer2";
			// 
			// L_NoPackages
			// 
			this.L_NoPackages.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoPackages.AutoSize = true;
			this.TLP_Dependencies.SetColumnSpan(this.L_NoPackages, 3);
			this.L_NoPackages.Location = new System.Drawing.Point(250, 42);
			this.L_NoPackages.Name = "L_NoPackages";
			this.L_NoPackages.Size = new System.Drawing.Size(45, 19);
			this.L_NoPackages.TabIndex = 4;
			this.L_NoPackages.Text = "label1";
			// 
			// DD_Dlcs
			// 
			this.DD_Dlcs.AccentBackColor = true;
			this.TLP_Dependencies.SetColumnSpan(this.DD_Dlcs, 3);
			this.DD_Dlcs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Dlcs.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Dlcs.ItemHeight = 24;
			this.DD_Dlcs.Items = new PDX.SDK.Contracts.Service.Mods.Models.ModGameAddon[0];
			this.DD_Dlcs.Location = new System.Drawing.Point(25, 93);
			this.DD_Dlcs.Name = "DD_Dlcs";
			this.DD_Dlcs.Size = new System.Drawing.Size(496, 72);
			this.DD_Dlcs.TabIndex = 4;
			this.DD_Dlcs.Text = "Required DLCs";
			// 
			// I_CopyPackages
			// 
			this.I_CopyPackages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.I_CopyPackages.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_CopyPackages.HasAction = true;
			dynamicIcon17.Name = "Copy";
			this.I_CopyPackages.ImageName = dynamicIcon17;
			this.I_CopyPackages.Location = new System.Drawing.Point(287, 25);
			this.I_CopyPackages.Name = "I_CopyPackages";
			this.I_CopyPackages.Size = new System.Drawing.Size(74, 14);
			this.I_CopyPackages.TabIndex = 23;
			this.I_CopyPackages.Click += new System.EventHandler(this.I_CopyPackages_Click);
			// 
			// I_PastePackages
			// 
			this.I_PastePackages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.I_PastePackages.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_PastePackages.HasAction = true;
			dynamicIcon18.Name = "Paste";
			this.I_PastePackages.ImageName = dynamicIcon18;
			this.I_PastePackages.Location = new System.Drawing.Point(367, 25);
			this.I_PastePackages.Name = "I_PastePackages";
			this.I_PastePackages.Size = new System.Drawing.Size(74, 14);
			this.I_PastePackages.TabIndex = 22;
			this.I_PastePackages.Click += new System.EventHandler(this.I_PastePackages_Click);
			// 
			// I_AddPackages
			// 
			this.I_AddPackages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.I_AddPackages.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_AddPackages.HasAction = true;
			dynamicIcon19.Name = "Add";
			this.I_AddPackages.ImageName = dynamicIcon19;
			this.I_AddPackages.Location = new System.Drawing.Point(447, 25);
			this.I_AddPackages.Name = "I_AddPackages";
			this.I_AddPackages.Size = new System.Drawing.Size(74, 14);
			this.I_AddPackages.TabIndex = 21;
			this.I_AddPackages.Click += new System.EventHandler(this.I_AddPackages_Click);
			// 
			// P_ModDependencies
			// 
			this.P_ModDependencies.AutoSize = true;
			this.P_ModDependencies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Dependencies.SetColumnSpan(this.P_ModDependencies, 3);
			this.P_ModDependencies.Dock = System.Windows.Forms.DockStyle.Top;
			this.P_ModDependencies.Location = new System.Drawing.Point(22, 61);
			this.P_ModDependencies.Margin = new System.Windows.Forms.Padding(0);
			this.P_ModDependencies.Name = "P_ModDependencies";
			this.P_ModDependencies.Size = new System.Drawing.Size(502, 0);
			this.P_ModDependencies.TabIndex = 24;
			this.P_ModDependencies.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.P_ModDependencies_ControlRemoved);
			this.P_ModDependencies.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.P_ModDependencies_ControlRemoved);
			// 
			// accessLevelControlDropdown1
			// 
			this.accessLevelControlDropdown1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.accessLevelControlDropdown1.ItemHeight = 24;
			this.accessLevelControlDropdown1.Location = new System.Drawing.Point(3, 547);
			this.accessLevelControlDropdown1.Name = "accessLevelControlDropdown1";
			this.accessLevelControlDropdown1.Size = new System.Drawing.Size(281, 14);
			this.accessLevelControlDropdown1.TabIndex = 4;
			this.accessLevelControlDropdown1.Text = "Mod Visibility";
			this.accessLevelControlDropdown1.Visible = false;
			// 
			// T_Images
			// 
			this.T_Images.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Images.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon21.Name = "EditImage";
			this.T_Images.IconName = dynamicIcon21;
			this.T_Images.LinkedControl = this.tableLayoutPanel3;
			this.T_Images.Location = new System.Drawing.Point(412, 5);
			this.T_Images.Name = "T_Images";
			this.T_Images.Size = new System.Drawing.Size(206, 90);
			this.T_Images.TabIndex = 0;
			this.T_Images.TabStop = false;
			this.T_Images.Text = "Images";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.Controls.Add(this.TLP_Screenshots, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.TLP_Thumb, 0, 0);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(362, 246);
			this.tableLayoutPanel3.TabIndex = 19;
			// 
			// TLP_Screenshots
			// 
			this.TLP_Screenshots.AddShadow = true;
			this.TLP_Screenshots.AutoSize = true;
			this.TLP_Screenshots.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Screenshots.ColumnCount = 1;
			this.TLP_Screenshots.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Screenshots.Controls.Add(this.B_AddScreenshot, 0, 0);
			this.TLP_Screenshots.Controls.Add(this.screenshotEditControl, 0, 1);
			this.TLP_Screenshots.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon23.Name = "Image";
			this.TLP_Screenshots.ImageName = dynamicIcon23;
			this.TLP_Screenshots.Location = new System.Drawing.Point(159, 3);
			this.TLP_Screenshots.Name = "TLP_Screenshots";
			this.TLP_Screenshots.Padding = new System.Windows.Forms.Padding(22);
			this.TLP_Screenshots.RowCount = 2;
			this.TLP_Screenshots.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Screenshots.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Screenshots.Size = new System.Drawing.Size(200, 236);
			this.TLP_Screenshots.TabIndex = 19;
			this.TLP_Screenshots.Text = "Screenshots";
			this.TLP_Screenshots.UseFirstRowForPadding = true;
			// 
			// B_AddScreenshot
			// 
			this.B_AddScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_AddScreenshot.AutoSize = true;
			this.B_AddScreenshot.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon22.Name = "Add";
			this.B_AddScreenshot.ImageName = dynamicIcon22;
			this.B_AddScreenshot.Location = new System.Drawing.Point(32, 25);
			this.B_AddScreenshot.Name = "B_AddScreenshot";
			this.B_AddScreenshot.Size = new System.Drawing.Size(143, 30);
			this.B_AddScreenshot.SpaceTriggersClick = true;
			this.B_AddScreenshot.TabIndex = 1;
			this.B_AddScreenshot.Text = "Add Screenshot";
			this.B_AddScreenshot.Click += new System.EventHandler(this.B_AddScreenshot_Click);
			// 
			// screenshotEditControl
			// 
			this.screenshotEditControl.Cursor = System.Windows.Forms.Cursors.Default;
			this.screenshotEditControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.screenshotEditControl.IOSelectionDialog = null;
			this.screenshotEditControl.Location = new System.Drawing.Point(25, 61);
			this.screenshotEditControl.Name = "screenshotEditControl";
			this.screenshotEditControl.Screenshots = ((System.Collections.Generic.IEnumerable<string>)(resources.GetObject("screenshotEditControl.Screenshots")));
			this.screenshotEditControl.Size = new System.Drawing.Size(150, 150);
			this.screenshotEditControl.TabIndex = 2;
			// 
			// TLP_Thumb
			// 
			this.TLP_Thumb.AddShadow = true;
			this.TLP_Thumb.AutoSize = true;
			this.TLP_Thumb.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Thumb.ColumnCount = 1;
			this.TLP_Thumb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Thumb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Thumb.Controls.Add(this.PB_Thumbnail, 0, 0);
			dynamicIcon24.Name = "Image";
			this.TLP_Thumb.ImageName = dynamicIcon24;
			this.TLP_Thumb.Location = new System.Drawing.Point(3, 3);
			this.TLP_Thumb.Name = "TLP_Thumb";
			this.TLP_Thumb.Padding = new System.Windows.Forms.Padding(22, 69, 22, 22);
			this.TLP_Thumb.RowCount = 1;
			this.TLP_Thumb.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Thumb.Size = new System.Drawing.Size(150, 147);
			this.TLP_Thumb.TabIndex = 18;
			this.TLP_Thumb.Text = "Thumbnail";
			// 
			// PB_Thumbnail
			// 
			this.PB_Thumbnail.LoaderSpeed = 1D;
			this.PB_Thumbnail.Location = new System.Drawing.Point(25, 72);
			this.PB_Thumbnail.Name = "PB_Thumbnail";
			this.PB_Thumbnail.Size = new System.Drawing.Size(100, 50);
			this.PB_Thumbnail.TabIndex = 0;
			this.PB_Thumbnail.TabStop = false;
			this.PB_Thumbnail.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_Thumbnail_Paint);
			this.PB_Thumbnail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PB_Thumbnail_MouseClick);
			this.PB_Thumbnail.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PB_Thumbnail_MouseMove);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.SelectedTextTimer_Tick);
			// 
			// PC_PackageEdit
			// 
			this.Controls.Add(this.slickTabControl1);
			this.Controls.Add(this.P_SideContainer);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_PackageEdit";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(1170, 1012);
			this.Text = "Back";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.P_SideContainer, 0);
			this.Controls.SetChildIndex(this.slickTabControl1, 0);
			this.P_Side.ResumeLayout(false);
			this.P_Side.PerformLayout();
			this.TLP_Side.ResumeLayout(false);
			this.TLP_Side.PerformLayout();
			this.TLP_TopInfo.ResumeLayout(false);
			this.P_SideContainer.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.TLP_Versions.ResumeLayout(false);
			this.TLP_Links.ResumeLayout(false);
			this.TLP_Links.PerformLayout();
			this.TLP_Dependencies.ResumeLayout(false);
			this.TLP_Dependencies.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.TLP_Screenshots.ResumeLayout(false);
			this.TLP_Screenshots.PerformLayout();
			this.TLP_Thumb.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PB_Thumbnail)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel TLP_TopInfo;
	private SlickIcon I_More;
	private SlickScroll base_slickScroll;
	private System.Windows.Forms.TableLayoutPanel TLP_Side;
	private AuthorControl L_Author;
	protected System.Windows.Forms.Panel P_SideContainer;
	private System.Windows.Forms.Panel P_Side;
	protected PackageIcon PB_Icon;
	private SlickSpacer base_slickSpacer;
	private SlickTabControl slickTabControl1;
	private SlickTabControl.Tab T_TitleDesc;
	private SlickTabControl.Tab T_Metadata;
	private SlickTabControl.Tab T_Images;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
	private SlickTextBox TB_Title;
	private SlickTextBox TB_ShortDesc;
	private SlickTextBox TB_LongDesc;
	private RoundedGroupTableLayoutPanel TLP_Links;
	private System.Windows.Forms.Label L_NoLinks;
	private Generic.AccessLevelControlDropdown accessLevelControlDropdown1;
	private RoundedGroupTableLayoutPanel TLP_Dependencies;
	private RoundedGroupTableLayoutPanel TLP_Versions;
	private SlickTextBox TB_GameVersion;
	private SlickTextBox TB_UserVersion;
	private Generic.PdxModsDlcDropdown DD_Dlcs;
	public SlickIcon I_AddPackages;
	public SlickIcon I_PastePackages;
	public SlickIcon I_CopyPackages;
	private System.Windows.Forms.Panel P_ModDependencies;
	private System.Windows.Forms.Label L_NoPackages;
	private SlickSpacer slickSpacer2;
	private SmartFlowPanel P_Links;
	private SlickSpacer slickSpacer3;
	private SlickTextBox TB_ForumLink;
	public SlickIcon I_CopyLinks;
	public SlickIcon I_PasteLinks;
	public SlickIcon I_AddLinks;
	private SlickPictureBox PB_Thumbnail;
	private IOSelectionDialog ioSelectionDialog;
	private RoundedGroupTableLayoutPanel TLP_Screenshots;
	private RoundedGroupTableLayoutPanel TLP_Thumb;
	private SlickButton B_AddScreenshot;
	private Generic.ScreenshotEditControl screenshotEditControl;
	private SlickButton B_Publish;
	private SlickButton B_Bold;
	private SlickButton B_Italic;
	private SlickButton B_List;
	private SlickButton B_Underline;
	private SlickButton B_Header1;
	private SlickButton B_Header3;
	private SlickButton B_Header2;
	private System.Windows.Forms.Timer timer1;
}
