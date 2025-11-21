namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_CompatibilityBulkEditing
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
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			this.panel1 = new System.Windows.Forms.Panel();
			this.smartFlowPanel1 = new SlickControls.SmartPanel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Add = new SlickControls.SlickButton();
			this.B_Paste = new SlickControls.SlickButton();
			this.B_Apply = new SlickControls.SlickButton();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(178, 41);
			this.base_Text.Text = "Patch Preparation";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.smartFlowPanel1);
			this.panel1.Controls.Add(this.slickScroll1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(5, 99);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(773, 334);
			this.panel1.TabIndex = 2;
			// 
			// smartFlowPanel1
			// 
			this.smartFlowPanel1.Location = new System.Drawing.Point(148, 96);
			this.smartFlowPanel1.Name = "smartFlowPanel1";
			this.smartFlowPanel1.Size = new System.Drawing.Size(200, 0);
			this.smartFlowPanel1.TabIndex = 1;
			// 
			// slickScroll1
			// 
			this.slickScroll1.AnimatedValue = 10;
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.smartFlowPanel1;
			this.slickScroll1.Location = new System.Drawing.Point(753, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(20, 334);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 0;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.TargetAnimationValue = 10;
			this.slickScroll1.Text = "slickScroll1";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.slickSpacer1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.B_Add, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_Paste, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.B_Apply, 3, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 30);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(773, 69);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// B_Add
			// 
			this.B_Add.AutoSize = true;
			this.B_Add.ColorStyle = Extensions.ColorStyle.Green;
			this.B_Add.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon3.Name = "Add";
			this.B_Add.ImageName = dynamicIcon3;
			this.B_Add.Location = new System.Drawing.Point(3, 3);
			this.B_Add.Name = "B_Add";
			this.B_Add.Size = new System.Drawing.Size(135, 34);
			this.B_Add.SpaceTriggersClick = true;
			this.B_Add.TabIndex = 0;
			this.B_Add.Text = "Add Packages";
			this.B_Add.Click += new System.EventHandler(this.B_Add_Click);
			// 
			// B_Paste
			// 
			this.B_Paste.AutoSize = true;
			this.B_Paste.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "Paste";
			this.B_Paste.ImageName = dynamicIcon4;
			this.B_Paste.Location = new System.Drawing.Point(144, 3);
			this.B_Paste.Name = "B_Paste";
			this.B_Paste.Size = new System.Drawing.Size(262, 34);
			this.B_Paste.SpaceTriggersClick = true;
			this.B_Paste.TabIndex = 0;
			this.B_Paste.Text = "Paste Package IDs From Clipboard";
			this.B_Paste.Click += new System.EventHandler(this.B_Paste_Click);
			// 
			// B_Apply
			// 
			this.B_Apply.AutoSize = true;
			this.B_Apply.ButtonType = SlickControls.ButtonType.Active;
			this.B_Apply.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon5.Name = "Ok";
			this.B_Apply.ImageName = dynamicIcon5;
			this.B_Apply.Location = new System.Drawing.Point(628, 3);
			this.B_Apply.Name = "B_Apply";
			this.B_Apply.Size = new System.Drawing.Size(142, 34);
			this.B_Apply.SpaceTriggersClick = true;
			this.B_Apply.TabIndex = 0;
			this.B_Apply.Text = "Apply Changes";
			this.B_Apply.Click += new System.EventHandler(this.B_Apply_Click);
			// 
			// slickSpacer1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.slickSpacer1, 4);
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(3, 43);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(767, 23);
			this.slickSpacer1.TabIndex = 4;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// PC_CompatibilityBulkEditing
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PC_CompatibilityBulkEditing";
			this.Text = "Patch Preparation";
			this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Panel panel1;
	private SlickScroll slickScroll1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private SlickButton B_Add;
	private SlickButton B_Paste;
	private SlickButton B_Apply;
	private SmartPanel smartFlowPanel1;
	private SlickSpacer slickSpacer1;
}
