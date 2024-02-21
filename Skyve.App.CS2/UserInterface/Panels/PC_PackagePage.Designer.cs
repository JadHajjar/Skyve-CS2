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
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			this.slickTabControl1 = new SlickControls.SlickTabControl();
			this.T_Info = new SlickControls.SlickTabControl.Tab();
			this.slickWebBrowser1 = new SlickControls.Controls.Advanced.SlickWebBrowser();
			this.T_CR = new SlickControls.SlickTabControl.Tab();
			this.T_References = new SlickControls.SlickTabControl.Tab();
			this.T_Profiles = new SlickControls.SlickTabControl.Tab();
			this.TLP_Profiles = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Icon = new Skyve.App.UserInterface.Content.PackageIcon();
			this.P_Side = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.autoSizeLabel1 = new SlickControls.Controls.Form.AutoSizeLabel();
			this.slickIcon1 = new SlickControls.SlickIcon();
			this.roundedTableLayoutPanel1 = new SlickControls.RoundedTableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.slickButton1 = new SlickControls.SlickButton();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.roundedTableLayoutPanel2 = new SlickControls.RoundedTableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.roundedTableLayoutPanel3 = new SlickControls.RoundedTableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.P_Side.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.roundedTableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.roundedTableLayoutPanel2.SuspendLayout();
			this.roundedTableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(150, 31);
			this.base_Text.Text = "Back";
			// 
			// slickTabControl1
			// 
			this.slickTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl1.Location = new System.Drawing.Point(0, 30);
			this.slickTabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.slickTabControl1.Name = "slickTabControl1";
			this.slickTabControl1.Size = new System.Drawing.Size(583, 408);
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
			dynamicIcon6.Name = "I_Content";
			this.T_Info.IconName = dynamicIcon6;
			this.T_Info.LinkedControl = this.slickWebBrowser1;
			this.T_Info.Location = new System.Drawing.Point(0, 5);
			this.T_Info.Name = "T_Info";
			this.T_Info.Selected = true;
			this.T_Info.Size = new System.Drawing.Size(145, 25);
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
			this.slickWebBrowser1.Size = new System.Drawing.Size(583, 377);
			this.slickWebBrowser1.TabIndex = 17;
			this.slickWebBrowser1.WebBrowserShortcutsEnabled = false;
			// 
			// T_CR
			// 
			this.T_CR.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_CR.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_CR.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dynamicIcon1.Name = "I_CompatibilityReport";
			this.T_CR.IconName = dynamicIcon1;
			this.T_CR.LinkedControl = null;
			this.T_CR.Location = new System.Drawing.Point(145, 5);
			this.T_CR.Name = "T_CR";
			this.T_CR.Selected = false;
			this.T_CR.Size = new System.Drawing.Size(145, 25);
			this.T_CR.TabIndex = 0;
			this.T_CR.TabStop = false;
			this.T_CR.Text = "CompatibilityInfo";
			// 
			// T_References
			// 
			this.T_References.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_References.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_References.FillTab = true;
			dynamicIcon2.Name = "I_Share";
			this.T_References.IconName = dynamicIcon2;
			this.T_References.LinkedControl = null;
			this.T_References.Location = new System.Drawing.Point(290, 5);
			this.T_References.Name = "T_References";
			this.T_References.Selected = false;
			this.T_References.Size = new System.Drawing.Size(145, 25);
			this.T_References.TabIndex = 1;
			this.T_References.TabStop = false;
			this.T_References.Text = "References";
			// 
			// T_Profiles
			// 
			this.T_Profiles.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Profiles.Dock = System.Windows.Forms.DockStyle.Left;
			this.T_Profiles.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dynamicIcon3.Name = "I_ProfileSettings";
			this.T_Profiles.IconName = dynamicIcon3;
			this.T_Profiles.LinkedControl = this.TLP_Profiles;
			this.T_Profiles.Location = new System.Drawing.Point(435, 5);
			this.T_Profiles.Name = "T_Profiles";
			this.T_Profiles.Selected = false;
			this.T_Profiles.Size = new System.Drawing.Size(145, 25);
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
			// P_Side
			// 
			this.P_Side.Controls.Add(this.slickScroll1);
			this.P_Side.Controls.Add(this.tableLayoutPanel3);
			this.P_Side.Dock = System.Windows.Forms.DockStyle.Right;
			this.P_Side.Location = new System.Drawing.Point(583, 30);
			this.P_Side.Name = "P_Side";
			this.P_Side.Size = new System.Drawing.Size(200, 408);
			this.P_Side.TabIndex = 14;
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
			this.tableLayoutPanel1.Controls.Add(this.slickIcon1, 2, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(186, 102);
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
			// slickIcon1
			// 
			this.slickIcon1.ActiveColor = null;
			this.slickIcon1.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "I_VertialMore";
			this.slickIcon1.ImageName = dynamicIcon4;
			this.slickIcon1.Location = new System.Drawing.Point(169, 3);
			this.slickIcon1.Name = "slickIcon1";
			this.slickIcon1.Size = new System.Drawing.Size(14, 28);
			this.slickIcon1.TabIndex = 2;
			// 
			// roundedTableLayoutPanel1
			// 
			this.roundedTableLayoutPanel1.ColumnCount = 2;
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.roundedTableLayoutPanel1.Location = new System.Drawing.Point(0, 182);
			this.roundedTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.roundedTableLayoutPanel1.Name = "roundedTableLayoutPanel1";
			this.roundedTableLayoutPanel1.RowCount = 2;
			this.roundedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.Size = new System.Drawing.Size(200, 20);
			this.roundedTableLayoutPanel1.TabIndex = 16;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.roundedTableLayoutPanel1.SetColumnSpan(this.label1, 2);
			this.label1.Location = new System.Drawing.Point(81, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 10);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// slickButton1
			// 
			this.slickButton1.AutoSize = true;
			this.slickButton1.ColorShade = null;
			this.slickButton1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickButton1.Location = new System.Drawing.Point(3, 125);
			this.slickButton1.Name = "slickButton1";
			this.slickButton1.Size = new System.Drawing.Size(79, 14);
			this.slickButton1.SpaceTriggersClick = true;
			this.slickButton1.TabIndex = 17;
			this.slickButton1.Text = "slickButton1";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.roundedTableLayoutPanel1, 0, 5);
			this.tableLayoutPanel3.Controls.Add(this.slickButton1, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.slickSpacer1, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.roundedTableLayoutPanel2, 0, 6);
			this.tableLayoutPanel3.Controls.Add(this.roundedTableLayoutPanel3, 0, 7);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 8;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(200, 242);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// slickScroll1
			// 
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.tableLayoutPanel3;
			this.slickScroll1.Location = new System.Drawing.Point(188, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(12, 408);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 2;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.Text = "slickScroll1";
			// 
			// slickSpacer1
			// 
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(3, 105);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(194, 14);
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
			this.roundedTableLayoutPanel2.Location = new System.Drawing.Point(0, 202);
			this.roundedTableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.roundedTableLayoutPanel2.Name = "roundedTableLayoutPanel2";
			this.roundedTableLayoutPanel2.RowCount = 2;
			this.roundedTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel2.Size = new System.Drawing.Size(200, 20);
			this.roundedTableLayoutPanel2.TabIndex = 18;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label2.AutoSize = true;
			this.roundedTableLayoutPanel2.SetColumnSpan(this.label2, 2);
			this.label2.Location = new System.Drawing.Point(81, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 10);
			this.label2.TabIndex = 0;
			this.label2.Text = "label2";
			// 
			// roundedTableLayoutPanel3
			// 
			this.roundedTableLayoutPanel3.ColumnCount = 2;
			this.roundedTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel3.Controls.Add(this.label3, 0, 0);
			this.roundedTableLayoutPanel3.Location = new System.Drawing.Point(0, 222);
			this.roundedTableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.roundedTableLayoutPanel3.Name = "roundedTableLayoutPanel3";
			this.roundedTableLayoutPanel3.RowCount = 2;
			this.roundedTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel3.Size = new System.Drawing.Size(200, 20);
			this.roundedTableLayoutPanel3.TabIndex = 19;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label3.AutoSize = true;
			this.roundedTableLayoutPanel3.SetColumnSpan(this.label3, 2);
			this.label3.Location = new System.Drawing.Point(81, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 10);
			this.label3.TabIndex = 0;
			this.label3.Text = "label3";
			// 
			// PC_PackagePage
			// 
			this.Controls.Add(this.slickTabControl1);
			this.Controls.Add(this.P_Side);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_PackagePage";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Text = "Back";
			this.Controls.SetChildIndex(this.P_Side, 0);
			this.Controls.SetChildIndex(this.slickTabControl1, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.P_Side.ResumeLayout(false);
			this.P_Side.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.roundedTableLayoutPanel1.ResumeLayout(false);
			this.roundedTableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
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
	private SlickIcon slickIcon1;
	private RoundedTableLayoutPanel roundedTableLayoutPanel1;
	private System.Windows.Forms.Label label1;
	private SlickScroll slickScroll1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
	private SlickButton slickButton1;
	private SlickSpacer slickSpacer1;
	private RoundedTableLayoutPanel roundedTableLayoutPanel2;
	private System.Windows.Forms.Label label2;
	private RoundedTableLayoutPanel roundedTableLayoutPanel3;
	private System.Windows.Forms.Label label3;
}
