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
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			this.P_Side = new System.Windows.Forms.Panel();
			this.base_slickScroll = new SlickControls.SlickScroll();
			this.TLP_Side = new System.Windows.Forms.TableLayoutPanel();
			this.TLP_TopInfo = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Icon = new Skyve.App.UserInterface.Content.PackageIcon();
			this.I_More = new SlickControls.SlickIcon();
			this.L_Author = new Skyve.App.UserInterface.Generic.AuthorControl();
			this.base_slickSpacer = new SlickControls.SlickSpacer();
			this.P_SideContainer = new System.Windows.Forms.Panel();
			this.slickTabControl1 = new SlickControls.SlickTabControl();
			this.T_TitleDesc = new SlickControls.SlickTabControl.Tab();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.TB_Title = new SlickControls.SlickTextBox();
			this.TB_ShortDesc = new SlickControls.SlickTextBox();
			this.TB_LongDesc = new SlickControls.SlickTextBox();
			this.T_Metadata = new SlickControls.SlickTabControl.Tab();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.P_Links = new SlickControls.RoundedGroupTableLayoutPanel();
			this.B_NewLink = new SlickControls.SlickButton();
			this.slickSpacer5 = new SlickControls.SlickSpacer();
			this.L_NoLinks = new System.Windows.Forms.Label();
			this.FLP_Links = new SlickControls.SmartFlowPanel();
			this.slickTextBox1 = new SlickControls.SlickTextBox();
			this.pdxModsDlcDropdown1 = new Skyve.App.CS2.UserInterface.Generic.PdxModsDlcDropdown();
			this.accessLevelControlDropdown1 = new Skyve.App.CS2.UserInterface.Generic.AccessLevelControlDropdown();
			this.T_Images = new SlickControls.SlickTabControl.Tab();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.P_Side.SuspendLayout();
			this.TLP_Side.SuspendLayout();
			this.TLP_TopInfo.SuspendLayout();
			this.P_SideContainer.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.P_Links.SuspendLayout();
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
			this.P_Side.Size = new System.Drawing.Size(422, 625);
			this.P_Side.TabIndex = 14;
			// 
			// base_slickScroll
			// 
			this.base_slickScroll.AnimatedValue = 10;
			this.base_slickScroll.Dock = System.Windows.Forms.DockStyle.Right;
			this.base_slickScroll.LinkedControl = this.TLP_Side;
			this.base_slickScroll.Location = new System.Drawing.Point(402, 0);
			this.base_slickScroll.Name = "base_slickScroll";
			this.base_slickScroll.Size = new System.Drawing.Size(20, 625);
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
			this.TLP_Side.Location = new System.Drawing.Point(0, 0);
			this.TLP_Side.Name = "TLP_Side";
			this.TLP_Side.RowCount = 2;
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.Size = new System.Drawing.Size(390, 122);
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
			// P_SideContainer
			// 
			this.P_SideContainer.Controls.Add(this.P_Side);
			this.P_SideContainer.Dock = System.Windows.Forms.DockStyle.Right;
			this.P_SideContainer.Location = new System.Drawing.Point(577, 30);
			this.P_SideContainer.Name = "P_SideContainer";
			this.P_SideContainer.Size = new System.Drawing.Size(422, 625);
			this.P_SideContainer.TabIndex = 15;
			// 
			// slickTabControl1
			// 
			this.slickTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl1.Location = new System.Drawing.Point(0, 30);
			this.slickTabControl1.Name = "slickTabControl1";
			this.slickTabControl1.Size = new System.Drawing.Size(577, 625);
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
			dynamicIcon2.Name = "Text";
			this.T_TitleDesc.IconName = dynamicIcon2;
			this.T_TitleDesc.LinkedControl = this.tableLayoutPanel1;
			this.T_TitleDesc.Location = new System.Drawing.Point(0, 5);
			this.T_TitleDesc.Name = "T_TitleDesc";
			this.T_TitleDesc.Size = new System.Drawing.Size(192, 90);
			this.T_TitleDesc.TabIndex = 2;
			this.T_TitleDesc.TabStop = false;
			this.T_TitleDesc.Text = "TitleDesc";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.TB_Title, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.TB_ShortDesc, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.TB_LongDesc, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(577, 530);
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
			this.TB_Title.Size = new System.Drawing.Size(571, 69);
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
			this.TB_ShortDesc.Size = new System.Drawing.Size(571, 166);
			this.TB_ShortDesc.TabIndex = 1;
			// 
			// TB_LongDesc
			// 
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
			this.TB_LongDesc.Size = new System.Drawing.Size(571, 277);
			this.TB_LongDesc.TabIndex = 2;
			// 
			// T_Metadata
			// 
			this.T_Metadata.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Metadata.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon3.Name = "Content";
			this.T_Metadata.IconName = dynamicIcon3;
			this.T_Metadata.LinkedControl = this.tableLayoutPanel2;
			this.T_Metadata.Location = new System.Drawing.Point(192, 5);
			this.T_Metadata.Name = "T_Metadata";
			this.T_Metadata.Size = new System.Drawing.Size(192, 90);
			this.T_Metadata.TabIndex = 1;
			this.T_Metadata.TabStop = false;
			this.T_Metadata.Text = "Details";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.P_Links, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.slickTextBox1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.pdxModsDlcDropdown1, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.accessLevelControlDropdown1, 1, 1);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(540, 349);
			this.tableLayoutPanel2.TabIndex = 18;
			// 
			// P_Links
			// 
			this.P_Links.AddOutline = true;
			this.P_Links.AddShadow = true;
			this.P_Links.AutoSize = true;
			this.P_Links.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Links.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.P_Links.Controls.Add(this.B_NewLink, 0, 0);
			this.P_Links.Controls.Add(this.slickSpacer5, 0, 1);
			this.P_Links.Controls.Add(this.L_NoLinks, 0, 2);
			this.P_Links.Controls.Add(this.FLP_Links, 0, 3);
			this.P_Links.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon5.Name = "Link";
			this.P_Links.ImageName = dynamicIcon5;
			this.P_Links.Location = new System.Drawing.Point(273, 3);
			this.P_Links.Name = "P_Links";
			this.P_Links.Padding = new System.Windows.Forms.Padding(22);
			this.P_Links.RowCount = 4;
			this.P_Links.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
			this.P_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Links.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.P_Links.Size = new System.Drawing.Size(264, 134);
			this.P_Links.TabIndex = 2;
			this.P_Links.Text = "Links";
			this.P_Links.UseFirstRowForPadding = true;
			// 
			// B_NewLink
			// 
			this.B_NewLink.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_NewLink.AutoSize = true;
			this.B_NewLink.ColorStyle = Extensions.ColorStyle.Green;
			this.B_NewLink.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "Edit";
			this.B_NewLink.ImageName = dynamicIcon4;
			this.B_NewLink.Location = new System.Drawing.Point(137, 26);
			this.B_NewLink.MatchBackgroundColor = true;
			this.B_NewLink.Name = "B_NewLink";
			this.B_NewLink.Size = new System.Drawing.Size(102, 30);
			this.B_NewLink.SpaceTriggersClick = true;
			this.B_NewLink.TabIndex = 0;
			this.B_NewLink.Text = "EditLinks";
			// 
			// slickSpacer5
			// 
			this.slickSpacer5.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer5.Location = new System.Drawing.Point(25, 64);
			this.slickSpacer5.Name = "slickSpacer5";
			this.slickSpacer5.Size = new System.Drawing.Size(214, 23);
			this.slickSpacer5.TabIndex = 1;
			this.slickSpacer5.TabStop = false;
			this.slickSpacer5.Text = "slickSpacer5";
			// 
			// L_NoLinks
			// 
			this.L_NoLinks.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_NoLinks.AutoSize = true;
			this.L_NoLinks.Location = new System.Drawing.Point(110, 90);
			this.L_NoLinks.Name = "L_NoLinks";
			this.L_NoLinks.Size = new System.Drawing.Size(44, 16);
			this.L_NoLinks.TabIndex = 3;
			this.L_NoLinks.Text = "label1";
			// 
			// FLP_Links
			// 
			this.FLP_Links.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Links.Location = new System.Drawing.Point(25, 109);
			this.FLP_Links.Name = "FLP_Links";
			this.FLP_Links.Size = new System.Drawing.Size(214, 0);
			this.FLP_Links.TabIndex = 2;
			// 
			// slickTextBox1
			// 
			this.slickTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickTextBox1.LabelText = "Forum Link";
			this.slickTextBox1.Location = new System.Drawing.Point(3, 3);
			this.slickTextBox1.MaxLength = 50;
			this.slickTextBox1.Name = "slickTextBox1";
			this.slickTextBox1.Padding = new System.Windows.Forms.Padding(7, 25, 7, 7);
			this.slickTextBox1.Placeholder = "Link a forum thread to enable comments your mod";
			this.slickTextBox1.Required = true;
			this.slickTextBox1.SelectedText = "";
			this.slickTextBox1.SelectionLength = 0;
			this.slickTextBox1.SelectionStart = 0;
			this.slickTextBox1.Size = new System.Drawing.Size(264, 69);
			this.slickTextBox1.TabIndex = 1;
			// 
			// pdxModsDlcDropdown1
			// 
			this.pdxModsDlcDropdown1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pdxModsDlcDropdown1.ItemHeight = 24;
			this.pdxModsDlcDropdown1.Location = new System.Drawing.Point(3, 177);
			this.pdxModsDlcDropdown1.Name = "pdxModsDlcDropdown1";
			this.pdxModsDlcDropdown1.Size = new System.Drawing.Size(264, 72);
			this.pdxModsDlcDropdown1.TabIndex = 3;
			this.pdxModsDlcDropdown1.Text = "Required DLCs";
			// 
			// accessLevelControlDropdown1
			// 
			this.accessLevelControlDropdown1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.accessLevelControlDropdown1.ItemHeight = 24;
			this.accessLevelControlDropdown1.Location = new System.Drawing.Point(273, 177);
			this.accessLevelControlDropdown1.Name = "accessLevelControlDropdown1";
			this.accessLevelControlDropdown1.Size = new System.Drawing.Size(264, 72);
			this.accessLevelControlDropdown1.TabIndex = 4;
			this.accessLevelControlDropdown1.Text = "Mod Visibility";
			// 
			// T_Images
			// 
			this.T_Images.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Images.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon6.Name = "EditImage";
			this.T_Images.IconName = dynamicIcon6;
			this.T_Images.LinkedControl = this.tableLayoutPanel3;
			this.T_Images.Location = new System.Drawing.Point(384, 5);
			this.T_Images.Name = "T_Images";
			this.T_Images.Size = new System.Drawing.Size(192, 90);
			this.T_Images.TabIndex = 0;
			this.T_Images.TabStop = false;
			this.T_Images.Text = "Images";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(282, 143);
			this.tableLayoutPanel3.TabIndex = 19;
			// 
			// PC_PackageEdit
			// 
			this.Controls.Add(this.slickTabControl1);
			this.Controls.Add(this.P_SideContainer);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_PackageEdit";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(999, 655);
			this.Text = "Back";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.P_SideContainer, 0);
			this.Controls.SetChildIndex(this.slickTabControl1, 0);
			this.P_Side.ResumeLayout(false);
			this.P_Side.PerformLayout();
			this.TLP_Side.ResumeLayout(false);
			this.TLP_TopInfo.ResumeLayout(false);
			this.P_SideContainer.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.P_Links.ResumeLayout(false);
			this.P_Links.PerformLayout();
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
	private SlickTextBox slickTextBox1;
	private RoundedGroupTableLayoutPanel P_Links;
	private SlickButton B_NewLink;
	private SlickSpacer slickSpacer5;
	private System.Windows.Forms.Label L_NoLinks;
	private SmartFlowPanel FLP_Links;
	private Generic.PdxModsDlcDropdown pdxModsDlcDropdown1;
	private Generic.AccessLevelControlDropdown accessLevelControlDropdown1;
}
