namespace Skyve.App.CS2.UserInterface.Generic;

partial class CommentsSectionControl
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Loading = new SlickControls.SlickPictureBox();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loading)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(729, 100);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// PB_Loading
			// 
			this.PB_Loading.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.PB_Loading.LoaderSpeed = 1D;
			this.PB_Loading.Loading = true;
			this.PB_Loading.Location = new System.Drawing.Point(304, 137);
			this.PB_Loading.Name = "PB_Loading";
			this.PB_Loading.Size = new System.Drawing.Size(100, 50);
			this.PB_Loading.TabIndex = 3;
			this.PB_Loading.TabStop = false;
			// 
			// CommentsSectionControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.PB_Loading);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "CommentsSectionControl";
			this.Size = new System.Drawing.Size(729, 256);
			((System.ComponentModel.ISupportInitialize)(this.PB_Loading)).EndInit();
			this.ResumeLayout(false);

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private SlickPictureBox PB_Loading;
}
