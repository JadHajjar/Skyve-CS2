namespace Skyve.App.CS2.UserInterface.Generic;

partial class CommentControl
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
			this.roundedTableLayoutPanel1 = new SlickControls.RoundedTableLayoutPanel();
			this.slickControl1 = new SlickControls.SlickControl();
			this.L_Author = new System.Windows.Forms.Label();
			this.L_AuthorLabel = new SlickControls.SlickLabel();
			this.slickButton1 = new SlickControls.SlickButton();
			this.L_Time = new System.Windows.Forms.Label();
			this.roundedTableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// roundedTableLayoutPanel1
			// 
			this.roundedTableLayoutPanel1.AddOutline = true;
			this.roundedTableLayoutPanel1.AddShadow = true;
			this.roundedTableLayoutPanel1.AutoSize = true;
			this.roundedTableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.roundedTableLayoutPanel1.ColumnCount = 5;
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.roundedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.roundedTableLayoutPanel1.Controls.Add(this.slickControl1, 0, 0);
			this.roundedTableLayoutPanel1.Controls.Add(this.L_Author, 1, 0);
			this.roundedTableLayoutPanel1.Controls.Add(this.L_AuthorLabel, 2, 0);
			this.roundedTableLayoutPanel1.Controls.Add(this.slickButton1, 4, 0);
			this.roundedTableLayoutPanel1.Controls.Add(this.L_Time, 1, 1);
			this.roundedTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.roundedTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.roundedTableLayoutPanel1.Name = "roundedTableLayoutPanel1";
			this.roundedTableLayoutPanel1.RowCount = 3;
			this.roundedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.roundedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.roundedTableLayoutPanel1.Size = new System.Drawing.Size(0, 73);
			this.roundedTableLayoutPanel1.TabIndex = 0;
			// 
			// slickControl1
			// 
			this.slickControl1.Location = new System.Drawing.Point(3, 3);
			this.slickControl1.Name = "slickControl1";
			this.roundedTableLayoutPanel1.SetRowSpan(this.slickControl1, 2);
			this.slickControl1.Size = new System.Drawing.Size(55, 44);
			this.slickControl1.TabIndex = 0;
			// 
			// L_Author
			// 
			this.L_Author.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Author.AutoSize = true;
			this.L_Author.Location = new System.Drawing.Point(64, 7);
			this.L_Author.Name = "L_Author";
			this.L_Author.Size = new System.Drawing.Size(35, 13);
			this.L_Author.TabIndex = 1;
			this.L_Author.Text = "label1";
			// 
			// L_AuthorLabel
			// 
			this.L_AuthorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_AuthorLabel.AutoSize = true;
			this.L_AuthorLabel.ButtonType = SlickControls.ButtonType.Active;
			this.L_AuthorLabel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_AuthorLabel.Display = true;
			this.L_AuthorLabel.Location = new System.Drawing.Point(105, 3);
			this.L_AuthorLabel.Name = "L_AuthorLabel";
			this.L_AuthorLabel.Selected = true;
			this.L_AuthorLabel.Size = new System.Drawing.Size(49, 21);
			this.L_AuthorLabel.SpaceTriggersClick = true;
			this.L_AuthorLabel.TabIndex = 2;
			this.L_AuthorLabel.Text = "Author";
			// 
			// slickButton1
			// 
			this.slickButton1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.slickButton1.AutoSize = true;
			this.slickButton1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.slickButton1.Location = new System.Drawing.Point(-78, 3);
			this.slickButton1.Name = "slickButton1";
			this.slickButton1.Size = new System.Drawing.Size(76, 21);
			this.slickButton1.SpaceTriggersClick = true;
			this.slickButton1.TabIndex = 3;
			this.slickButton1.Text = "slickButton1";
			// 
			// L_Time
			// 
			this.L_Time.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Time.AutoSize = true;
			this.L_Time.Location = new System.Drawing.Point(64, 32);
			this.L_Time.Name = "L_Time";
			this.L_Time.Size = new System.Drawing.Size(35, 13);
			this.L_Time.TabIndex = 1;
			this.L_Time.Text = "label1";
			// 
			// CommentControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.roundedTableLayoutPanel1);
			this.Name = "CommentControl";
			this.Size = new System.Drawing.Size(0, 73);
			this.roundedTableLayoutPanel1.ResumeLayout(false);
			this.roundedTableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private RoundedTableLayoutPanel roundedTableLayoutPanel1;
	private SlickControl slickControl1;
	private System.Windows.Forms.Label L_Author;
	private SlickLabel L_AuthorLabel;
	private SlickButton slickButton1;
	private System.Windows.Forms.Label L_Time;
}
