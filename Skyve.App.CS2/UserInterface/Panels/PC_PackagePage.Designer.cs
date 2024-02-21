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
			ServiceCenter.Get<INotifier>().PackageInformationUpdated -= CentralManager_PackageInformationUpdated;
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
			this.slickTabControl1 = new SlickControls.SlickTabControl();
			this.T_Info = new SlickControls.SlickTabControl.Tab();
			this.slickWebBrowser1 = new SlickControls.Controls.Advanced.SlickWebBrowser();
			this.T_CR = new SlickControls.SlickTabControl.Tab();
			this.T_References = new SlickControls.SlickTabControl.Tab();
			this.T_Profiles = new SlickControls.SlickTabControl.Tab();
			this.TLP_Profiles = new System.Windows.Forms.TableLayoutPanel();
			this.P_Side = new System.Windows.Forms.Panel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.TLP_Side = new System.Windows.Forms.TableLayoutPanel();
			this.roundedTableLayoutPanel1 = new SlickControls.RoundedTableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.B_Incl = new SlickControls.SlickButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.autoSizeLabel1 = new SlickControls.Controls.Form.AutoSizeLabel();
			this.I_More = new SlickControls.SlickIcon();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.roundedTableLayoutPanel2 = new SlickControls.RoundedTableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.roundedTableLayoutPanel3 = new SlickControls.RoundedTableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.PB_Icon = new Skyve.App.UserInterface.Content.PackageIcon();
			this.LI_Version = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.LI_UpdateTime = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.infoAndLabelControl3 = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.infoAndLabelControl4 = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.infoAndLabelControl5 = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.infoAndLabelControl6 = new Skyve.App.CS2.UserInterface.Content.InfoAndLabelControl();
			this.P_Side.SuspendLayout();
			this.TLP_Side.SuspendLayout();
			this.roundedTableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.roundedTableLayoutPanel2.SuspendLayout();
			this.roundedTableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(150, 41);
			this.base_Text.Text = "Back";
			// 
			// slickTabControl1
			// 
			this.slickTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl1.Location = new System.Drawing.Point(0, 30);
			this.slickTabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.slickTabControl1.Name = "slickTabControl1";
			this.slickTabControl1.Size = new System.Drawing.Size(796, 623);
			this.slickTabControl1.TabIndex = 0;
			this.slickTabControl1.Tabs = new SlickControls.SlickTabControl.Tab[] {
        this.T_Info,
        this.T_CR,
        this.T_References,
        this.T_Profiles};
			// 
			// T_Info
			// 
			this.T_Info.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Info.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Info.FillTab = true;
			this.T_Info.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dynamicIcon1.Name = "I_Content";
			this.T_Info.IconName = dynamicIcon1;
			this.T_Info.LinkedControl = this.slickWebBrowser1;
			this.T_Info.Location = new System.Drawing.Point(0, 5);
			this.T_Info.Name = "T_Info";
			this.T_Info.Selected = true;
			this.T_Info.Size = new System.Drawing.Size(199, 25);
			this.T_Info.TabIndex = 0;
			this.T_Info.TabStop = false;
			this.T_Info.Text = "ContentAndInfo";
			// 
			// slickWebBrowser1
			// 
			this.slickWebBrowser1.Body = null;
			this.slickWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickWebBrowser1.Head = null;
			this.slickWebBrowser1.IsWebBrowserContextMenuEnabled = false;
			this.slickWebBrowser1.Location = new System.Drawing.Point(0, 0);
			this.slickWebBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.slickWebBrowser1.Name = "slickWebBrowser1";
			this.slickWebBrowser1.Size = new System.Drawing.Size(796, 592);
			this.slickWebBrowser1.TabIndex = 17;
			this.slickWebBrowser1.WebBrowserShortcutsEnabled = false;
			// 
			// T_CR
			// 
			this.T_CR.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_CR.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_CR.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dynamicIcon2.Name = "I_CompatibilityReport";
			this.T_CR.IconName = dynamicIcon2;
			this.T_CR.LinkedControl = null;
			this.T_CR.Location = new System.Drawing.Point(199, 5);
			this.T_CR.Name = "T_CR";
			this.T_CR.Selected = false;
			this.T_CR.Size = new System.Drawing.Size(199, 25);
			this.T_CR.TabIndex = 0;
			this.T_CR.TabStop = false;
			this.T_CR.Text = "CompatibilityInfo";
			// 
			// T_References
			// 
			this.T_References.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_References.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_References.FillTab = true;
			dynamicIcon3.Name = "I_Share";
			this.T_References.IconName = dynamicIcon3;
			this.T_References.LinkedControl = null;
			this.T_References.Location = new System.Drawing.Point(398, 5);
			this.T_References.Name = "T_References";
			this.T_References.Selected = false;
			this.T_References.Size = new System.Drawing.Size(199, 25);
			this.T_References.TabIndex = 1;
			this.T_References.TabStop = false;
			this.T_References.Text = "References";
			// 
			// T_Profiles
			// 
			this.T_Profiles.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Profiles.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Profiles.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dynamicIcon4.Name = "I_ProfileSettings";
			this.T_Profiles.IconName = dynamicIcon4;
			this.T_Profiles.LinkedControl = this.TLP_Profiles;
			this.T_Profiles.Location = new System.Drawing.Point(597, 5);
			this.T_Profiles.Name = "T_Profiles";
			this.T_Profiles.Selected = false;
			this.T_Profiles.Size = new System.Drawing.Size(199, 25);
			this.T_Profiles.TabIndex = 0;
			this.T_Profiles.TabStop = false;
			this.T_Profiles.Text = "OtherPlaysets";
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
			this.TLP_Side.Controls.Add(this.roundedTableLayoutPanel1, 0, 5);
			this.TLP_Side.Controls.Add(this.B_Incl, 0, 2);
			this.TLP_Side.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.TLP_Side.Controls.Add(this.slickSpacer1, 0, 1);
			this.TLP_Side.Controls.Add(this.roundedTableLayoutPanel2, 0, 6);
			this.TLP_Side.Controls.Add(this.roundedTableLayoutPanel3, 0, 7);
			this.TLP_Side.Location = new System.Drawing.Point(0, 0);
			this.TLP_Side.Name = "TLP_Side";
			this.TLP_Side.RowCount = 8;
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Side.Size = new System.Drawing.Size(312, 415);
			this.TLP_Side.TabIndex = 1;
			// 
			// roundedTableLayoutPanel1
			// 
			this.roundedTableLayoutPanel1.AutoSize = true;
			this.roundedTableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedTableLayoutPanel1.ColumnCount = 2;
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.Controls.Add(this.infoAndLabelControl6, 1, 2);
			this.roundedTableLayoutPanel1.Controls.Add(this.infoAndLabelControl5, 1, 3);
			this.roundedTableLayoutPanel1.Controls.Add(this.infoAndLabelControl4, 0, 3);
			this.roundedTableLayoutPanel1.Controls.Add(this.infoAndLabelControl3, 0, 2);
			this.roundedTableLayoutPanel1.Controls.Add(this.LI_UpdateTime, 1, 1);
			this.roundedTableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.roundedTableLayoutPanel1.Controls.Add(this.LI_Version, 0, 1);
			this.roundedTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.roundedTableLayoutPanel1.Location = new System.Drawing.Point(0, 202);
			this.roundedTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.roundedTableLayoutPanel1.Name = "roundedTableLayoutPanel1";
			this.roundedTableLayoutPanel1.RowCount = 4;
			this.roundedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedTableLayoutPanel1.Size = new System.Drawing.Size(312, 130);
			this.roundedTableLayoutPanel1.TabIndex = 16;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.roundedTableLayoutPanel1.SetColumnSpan(this.label1, 2);
			this.label1.Location = new System.Drawing.Point(133, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 19);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// B_Incl
			// 
			this.B_Incl.AutoSize = true;
			this.B_Incl.ColorShade = null;
			this.B_Incl.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Incl.Dock = System.Windows.Forms.DockStyle.Top;
			this.B_Incl.Location = new System.Drawing.Point(3, 125);
			this.B_Incl.Name = "B_Incl";
			this.B_Incl.Size = new System.Drawing.Size(306, 34);
			this.B_Incl.SpaceTriggersClick = true;
			this.B_Incl.TabIndex = 17;
			this.B_Incl.Text = "slickButton1";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.PB_Icon, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.autoSizeLabel1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.I_More, 2, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(312, 102);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// autoSizeLabel1
			// 
			this.autoSizeLabel1.Location = new System.Drawing.Point(93, 3);
			this.autoSizeLabel1.Name = "autoSizeLabel1";
			this.autoSizeLabel1.Size = new System.Drawing.Size(70, 41);
			this.autoSizeLabel1.TabIndex = 1;
			this.autoSizeLabel1.Text = "autoSizeLabel1";
			// 
			// I_More
			// 
			this.I_More.ActiveColor = null;
			this.I_More.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "I_VertialMore";
			this.I_More.ImageName = dynamicIcon5;
			this.I_More.Location = new System.Drawing.Point(298, 0);
			this.I_More.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.I_More.Name = "I_More";
			this.I_More.Size = new System.Drawing.Size(14, 28);
			this.I_More.TabIndex = 2;
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
			// roundedTableLayoutPanel2
			// 
			this.roundedTableLayoutPanel2.ColumnCount = 2;
			this.roundedTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel2.Controls.Add(this.label2, 0, 0);
			this.roundedTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.roundedTableLayoutPanel2.Location = new System.Drawing.Point(0, 332);
			this.roundedTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.roundedTableLayoutPanel2.Name = "roundedTableLayoutPanel2";
			this.roundedTableLayoutPanel2.RowCount = 2;
			this.roundedTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel2.Size = new System.Drawing.Size(312, 40);
			this.roundedTableLayoutPanel2.TabIndex = 18;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label2.AutoSize = true;
			this.roundedTableLayoutPanel2.SetColumnSpan(this.label2, 2);
			this.label2.Location = new System.Drawing.Point(133, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 19);
			this.label2.TabIndex = 0;
			this.label2.Text = "label2";
			// 
			// roundedTableLayoutPanel3
			// 
			this.roundedTableLayoutPanel3.ColumnCount = 2;
			this.roundedTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel3.Controls.Add(this.label3, 0, 0);
			this.roundedTableLayoutPanel3.Location = new System.Drawing.Point(0, 372);
			this.roundedTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.roundedTableLayoutPanel3.Name = "roundedTableLayoutPanel3";
			this.roundedTableLayoutPanel3.RowCount = 2;
			this.roundedTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel3.Size = new System.Drawing.Size(200, 43);
			this.roundedTableLayoutPanel3.TabIndex = 19;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label3.AutoSize = true;
			this.roundedTableLayoutPanel3.SetColumnSpan(this.label3, 2);
			this.label3.Location = new System.Drawing.Point(77, 1);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 19);
			this.label3.TabIndex = 0;
			this.label3.Text = "label3";
			// 
			// PB_Icon
			// 
			this.PB_Icon.HalfColor = false;
			this.PB_Icon.Location = new System.Drawing.Point(0, 0);
			this.PB_Icon.Margin = new System.Windows.Forms.Padding(0);
			this.PB_Icon.Name = "PB_Icon";
			this.tableLayoutPanel1.SetRowSpan(this.PB_Icon, 2);
			this.PB_Icon.Size = new System.Drawing.Size(90, 102);
			this.PB_Icon.TabIndex = 0;
			this.PB_Icon.TabStop = false;
			// 
			// LI_Version
			// 
			this.LI_Version.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_Version.LabelText = null;
			this.LI_Version.Location = new System.Drawing.Point(3, 22);
			this.LI_Version.Name = "LI_Version";
			this.LI_Version.Size = new System.Drawing.Size(150, 31);
			this.LI_Version.TabIndex = 1;
			this.LI_Version.ValueText = null;
			// 
			// LI_UpdateTime
			// 
			this.LI_UpdateTime.Dock = System.Windows.Forms.DockStyle.Top;
			this.LI_UpdateTime.LabelText = null;
			this.LI_UpdateTime.Location = new System.Drawing.Point(159, 22);
			this.LI_UpdateTime.Name = "LI_UpdateTime";
			this.LI_UpdateTime.Size = new System.Drawing.Size(150, 31);
			this.LI_UpdateTime.TabIndex = 2;
			this.LI_UpdateTime.ValueText = null;
			// 
			// infoAndLabelControl3
			// 
			this.infoAndLabelControl3.Dock = System.Windows.Forms.DockStyle.Top;
			this.infoAndLabelControl3.LabelText = null;
			this.infoAndLabelControl3.Location = new System.Drawing.Point(3, 59);
			this.infoAndLabelControl3.Name = "infoAndLabelControl3";
			this.infoAndLabelControl3.Size = new System.Drawing.Size(150, 31);
			this.infoAndLabelControl3.TabIndex = 3;
			this.infoAndLabelControl3.ValueText = null;
			// 
			// infoAndLabelControl4
			// 
			this.infoAndLabelControl4.Dock = System.Windows.Forms.DockStyle.Top;
			this.infoAndLabelControl4.LabelText = null;
			this.infoAndLabelControl4.Location = new System.Drawing.Point(3, 96);
			this.infoAndLabelControl4.Name = "infoAndLabelControl4";
			this.infoAndLabelControl4.Size = new System.Drawing.Size(150, 31);
			this.infoAndLabelControl4.TabIndex = 4;
			this.infoAndLabelControl4.ValueText = null;
			// 
			// infoAndLabelControl5
			// 
			this.infoAndLabelControl5.Dock = System.Windows.Forms.DockStyle.Top;
			this.infoAndLabelControl5.LabelText = null;
			this.infoAndLabelControl5.Location = new System.Drawing.Point(159, 96);
			this.infoAndLabelControl5.Name = "infoAndLabelControl5";
			this.infoAndLabelControl5.Size = new System.Drawing.Size(150, 31);
			this.infoAndLabelControl5.TabIndex = 5;
			this.infoAndLabelControl5.ValueText = null;
			// 
			// infoAndLabelControl6
			// 
			this.infoAndLabelControl6.Dock = System.Windows.Forms.DockStyle.Top;
			this.infoAndLabelControl6.LabelText = null;
			this.infoAndLabelControl6.Location = new System.Drawing.Point(159, 59);
			this.infoAndLabelControl6.Name = "infoAndLabelControl6";
			this.infoAndLabelControl6.Size = new System.Drawing.Size(150, 31);
			this.infoAndLabelControl6.TabIndex = 6;
			this.infoAndLabelControl6.ValueText = null;
			// 
			// PC_PackagePage
			// 
			this.Controls.Add(this.slickTabControl1);
			this.Controls.Add(this.P_Side);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_PackagePage";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(996, 653);
			this.Text = "Back";
			this.Controls.SetChildIndex(this.P_Side, 0);
			this.Controls.SetChildIndex(this.slickTabControl1, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.P_Side.ResumeLayout(false);
			this.P_Side.PerformLayout();
			this.TLP_Side.ResumeLayout(false);
			this.TLP_Side.PerformLayout();
			this.roundedTableLayoutPanel1.ResumeLayout(false);
			this.roundedTableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.roundedTableLayoutPanel2.ResumeLayout(false);
			this.roundedTableLayoutPanel2.PerformLayout();
			this.roundedTableLayoutPanel3.ResumeLayout(false);
			this.roundedTableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private PackageIcon PB_Icon;
	private SlickControls.SlickTabControl slickTabControl1;
	private SlickControls.SlickTabControl.Tab T_Info;
	public SlickControls.SlickTabControl.Tab T_CR;
	private SlickControls.SlickTabControl.Tab T_Profiles;
	private System.Windows.Forms.TableLayoutPanel TLP_Profiles;
	private SlickControls.SlickTabControl.Tab T_References;
	private System.Windows.Forms.Panel P_Side;
	private SlickControls.Controls.Advanced.SlickWebBrowser slickWebBrowser1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private SlickControls.Controls.Form.AutoSizeLabel autoSizeLabel1;
	private SlickIcon I_More;
	private RoundedTableLayoutPanel roundedTableLayoutPanel1;
	private System.Windows.Forms.Label label1;
	private SlickScroll slickScroll1;
	private System.Windows.Forms.TableLayoutPanel TLP_Side;
	private SlickButton B_Incl;
	private SlickSpacer slickSpacer1;
	private RoundedTableLayoutPanel roundedTableLayoutPanel2;
	private System.Windows.Forms.Label label2;
	private RoundedTableLayoutPanel roundedTableLayoutPanel3;
	private System.Windows.Forms.Label label3;
	private Content.InfoAndLabelControl infoAndLabelControl6;
	private Content.InfoAndLabelControl infoAndLabelControl5;
	private Content.InfoAndLabelControl infoAndLabelControl4;
	private Content.InfoAndLabelControl infoAndLabelControl3;
	private Content.InfoAndLabelControl LI_UpdateTime;
	private Content.InfoAndLabelControl LI_Version;
}
