namespace Skyve.App.CS2.Installer;

partial class InstallingForm
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

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallingForm));
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.roundedTableLayoutPanel1 = new SlickControls.RoundedTableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.slickButton1 = new SlickControls.SlickButton();
			this.slickButton2 = new SlickControls.SlickButton();
			this.slickCheckbox1 = new SlickControls.SlickCheckbox();
			this.slickPathTextBox1 = new SlickControls.SlickPathTextBox();
			this.slickCheckbox2 = new SlickControls.SlickCheckbox();
			this.label3 = new System.Windows.Forms.Label();
			this.base_P_Container.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.base_PB_Icon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Close)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Max)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Min)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.TLP_Main.SuspendLayout();
			this.roundedTableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// base_P_Container
			// 
			this.base_P_Container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(199)))), ((int)(((byte)(145)))));
			this.base_P_Container.Controls.Add(this.TLP_Main);
			this.base_P_Container.Size = new System.Drawing.Size(709, 405);
			// 
			// pictureBox
			// 
			this.pictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Location = new System.Drawing.Point(4, 4);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(709, 405);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 3;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.Controls.Add(this.label2, 0, 1);
			this.TLP_Main.Controls.Add(this.roundedTableLayoutPanel1, 0, 0);
			this.TLP_Main.Controls.Add(this.slickButton1, 2, 6);
			this.TLP_Main.Controls.Add(this.slickButton2, 0, 6);
			this.TLP_Main.Controls.Add(this.slickCheckbox1, 0, 3);
			this.TLP_Main.Controls.Add(this.slickPathTextBox1, 0, 2);
			this.TLP_Main.Controls.Add(this.slickCheckbox2, 0, 4);
			this.TLP_Main.Controls.Add(this.label3, 0, 5);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(1, 1);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 7;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.Size = new System.Drawing.Size(707, 403);
			this.TLP_Main.TabIndex = 0;
			this.TLP_Main.Visible = false;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 157);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(295, 19);
			this.label2.TabIndex = 1;
			this.label2.Text = "Select where to install Skyve on your computer";
			this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
			// 
			// roundedTableLayoutPanel1
			// 
			this.roundedTableLayoutPanel1.AutoSize = true;
			this.roundedTableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedTableLayoutPanel1.ColumnCount = 2;
			this.TLP_Main.SetColumnSpan(this.roundedTableLayoutPanel1, 3);
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.roundedTableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
			this.roundedTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.roundedTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.roundedTableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.roundedTableLayoutPanel1.Name = "roundedTableLayoutPanel1";
			this.roundedTableLayoutPanel1.RowCount = 1;
			this.roundedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.Size = new System.Drawing.Size(707, 157);
			this.roundedTableLayoutPanel1.TabIndex = 2;
			this.roundedTableLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(621, 69);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "Skyve Setup";
			this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(186, 151);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
			// 
			// slickButton1
			// 
			this.slickButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.slickButton1.AutoSize = true;
			this.slickButton1.ButtonType = SlickControls.ButtonType.Dimmed;
			this.slickButton1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickButton1.Location = new System.Drawing.Point(644, 368);
			this.slickButton1.Margin = new System.Windows.Forms.Padding(0);
			this.slickButton1.Name = "slickButton1";
			this.slickButton1.Size = new System.Drawing.Size(63, 32);
			this.slickButton1.SpaceTriggersClick = true;
			this.slickButton1.TabIndex = 3;
			this.slickButton1.Text = "Cancel";
			this.slickButton1.Click += new System.EventHandler(this.slickButton1_Click);
			// 
			// slickButton2
			// 
			this.slickButton2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.slickButton2.AutoSize = true;
			this.slickButton2.ButtonType = SlickControls.ButtonType.Active;
			this.TLP_Main.SetColumnSpan(this.slickButton2, 2);
			this.slickButton2.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "Ok";
			this.slickButton2.ImageName = dynamicIcon1;
			this.slickButton2.Location = new System.Drawing.Point(560, 368);
			this.slickButton2.Name = "slickButton2";
			this.slickButton2.Size = new System.Drawing.Size(81, 32);
			this.slickButton2.SpaceTriggersClick = true;
			this.slickButton2.TabIndex = 0;
			this.slickButton2.Text = "Install";
			this.slickButton2.Click += new System.EventHandler(this.slickButton2_Click);
			// 
			// slickCheckbox1
			// 
			this.slickCheckbox1.AutoSize = true;
			this.slickCheckbox1.Checked = true;
			this.slickCheckbox1.CheckedText = null;
			this.slickCheckbox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox1.DefaultValue = true;
			this.slickCheckbox1.EnterTriggersClick = false;
			this.slickCheckbox1.Location = new System.Drawing.Point(3, 242);
			this.slickCheckbox1.Name = "slickCheckbox1";
			this.slickCheckbox1.Size = new System.Drawing.Size(221, 44);
			this.slickCheckbox1.SpaceTriggersClick = true;
			this.slickCheckbox1.TabIndex = 5;
			this.slickCheckbox1.Text = "Create a desktop shortcut";
			this.slickCheckbox1.UncheckedText = null;
			// 
			// slickPathTextBox1
			// 
			this.TLP_Main.SetColumnSpan(this.slickPathTextBox1, 3);
			this.slickPathTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickPathTextBox1.Folder = true;
			this.slickPathTextBox1.LabelText = "Installation Folder";
			this.slickPathTextBox1.Location = new System.Drawing.Point(3, 179);
			this.slickPathTextBox1.Name = "slickPathTextBox1";
			this.slickPathTextBox1.Padding = new System.Windows.Forms.Padding(6, 19, 6, 6);
			this.slickPathTextBox1.Placeholder = "Folder Path";
			this.slickPathTextBox1.SelectedText = "";
			this.slickPathTextBox1.SelectionLength = 0;
			this.slickPathTextBox1.SelectionStart = 0;
			this.slickPathTextBox1.Size = new System.Drawing.Size(701, 57);
			this.slickPathTextBox1.TabIndex = 4;
			this.slickPathTextBox1.Text = "C:\\Program Files (x86)\\Skyve CS-II";
			this.slickPathTextBox1.PathSelected += new System.EventHandler(this.slickPathTextBox1_PathSelected);
			// 
			// slickCheckbox2
			// 
			this.slickCheckbox2.AutoSize = true;
			this.slickCheckbox2.Checked = true;
			this.slickCheckbox2.CheckedText = null;
			this.slickCheckbox2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickCheckbox2.DefaultValue = true;
			this.slickCheckbox2.EnterTriggersClick = false;
			this.slickCheckbox2.Location = new System.Drawing.Point(3, 292);
			this.slickCheckbox2.Name = "slickCheckbox2";
			this.slickCheckbox2.Size = new System.Drawing.Size(222, 44);
			this.slickCheckbox2.SpaceTriggersClick = true;
			this.slickCheckbox2.TabIndex = 5;
			this.slickCheckbox2.Text = "Install background service";
			this.slickCheckbox2.UncheckedText = null;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 339);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(301, 26);
			this.label3.TabIndex = 1;
			this.label3.Text = "The Skyve background service automatically updates your mods and download new mod" +
    "s without you having to open Skyve or the game.";
			this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
			// 
			// InstallingForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.ClientSize = new System.Drawing.Size(720, 416);
			this.Controls.Add(this.pictureBox);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizedBounds = new System.Drawing.Rectangle(0, 0, 2560, 1380);
			this.Name = "InstallingForm";
			this.Text = "Skyve";
			this.Controls.SetChildIndex(this.base_P_Container, 0);
			this.Controls.SetChildIndex(this.pictureBox, 0);
			this.base_P_Container.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.base_PB_Icon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Close)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Max)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Min)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			this.roundedTableLayoutPanel1.ResumeLayout(false);
			this.roundedTableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.PictureBox pictureBox;
	private System.Windows.Forms.TableLayoutPanel TLP_Main;
	private System.Windows.Forms.PictureBox pictureBox1;
	private System.Windows.Forms.Label label1;
	private SlickControls.RoundedTableLayoutPanel roundedTableLayoutPanel1;
	private SlickControls.SlickButton slickButton1;
	private SlickControls.SlickButton slickButton2;
	private SlickControls.SlickCheckbox slickCheckbox1;
	private SlickControls.SlickPathTextBox slickPathTextBox1;
	private SlickControls.SlickCheckbox slickCheckbox2;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label3;
}