namespace Skyve.App.CS2.UserInterface.Generic;

partial class CommentsControl
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
			this.C_Comments = new SlickControls.SlickControl();
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(638, 100);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// C_Comments
			// 
			this.C_Comments.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_Comments.Location = new System.Drawing.Point(0, 100);
			this.C_Comments.Name = "C_Comments";
			this.C_Comments.Size = new System.Drawing.Size(638, 150);
			this.C_Comments.TabIndex = 3;
			this.C_Comments.Paint += new System.Windows.Forms.PaintEventHandler(this.C_Comments_Paint);
			// 
			// CommentsControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.C_Comments);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "CommentsControl";
			this.Size = new System.Drawing.Size(638, 574);
			this.ResumeLayout(false);

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private SlickControl C_Comments;
}
