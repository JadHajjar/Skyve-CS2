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
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			this.TLP_Back = new SlickControls.RoundedTableLayoutPanel();
			this.C_UserImage = new SlickControls.SlickControl();
			this.L_Author = new System.Windows.Forms.Label();
			this.L_AuthorLabel = new SlickControls.SlickLabel();
			this.slickButton1 = new SlickControls.SlickButton();
			this.L_Time = new System.Windows.Forms.Label();
			this.C_Message = new SlickControls.SlickControl();
			this.TLP_Back.SuspendLayout();
			this.SuspendLayout();
			// 
			// TLP_Back
			// 
			this.TLP_Back.AddShadow = true;
			this.TLP_Back.AutoSize = true;
			this.TLP_Back.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Back.ColumnCount = 5;
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Back.Controls.Add(this.C_UserImage, 0, 0);
			this.TLP_Back.Controls.Add(this.L_Author, 1, 0);
			this.TLP_Back.Controls.Add(this.L_AuthorLabel, 2, 0);
			this.TLP_Back.Controls.Add(this.slickButton1, 4, 0);
			this.TLP_Back.Controls.Add(this.L_Time, 1, 1);
			this.TLP_Back.Controls.Add(this.C_Message, 1, 2);
			this.TLP_Back.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Back.Location = new System.Drawing.Point(0, 0);
			this.TLP_Back.Name = "TLP_Back";
			this.TLP_Back.RowCount = 3;
			this.TLP_Back.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Back.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Back.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Back.Size = new System.Drawing.Size(669, 100);
			this.TLP_Back.TabIndex = 0;
			// 
			// C_UserImage
			// 
			this.C_UserImage.Location = new System.Drawing.Point(3, 3);
			this.C_UserImage.Name = "C_UserImage";
			this.TLP_Back.SetRowSpan(this.C_UserImage, 2);
			this.C_UserImage.Size = new System.Drawing.Size(55, 44);
			this.C_UserImage.TabIndex = 0;
			this.C_UserImage.Paint += new System.Windows.Forms.PaintEventHandler(this.C_UserImage_Paint);
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
			this.L_AuthorLabel.Display = true;
			this.L_AuthorLabel.Enabled = false;
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
			this.slickButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.slickButton1.AutoSize = true;
			this.slickButton1.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon3.Name = "Reply";
			this.slickButton1.ImageName = dynamicIcon3;
			this.slickButton1.Location = new System.Drawing.Point(622, 3);
			this.slickButton1.Name = "slickButton1";
			this.TLP_Back.SetRowSpan(this.slickButton1, 2);
			this.slickButton1.Size = new System.Drawing.Size(44, 21);
			this.slickButton1.SpaceTriggersClick = true;
			this.slickButton1.TabIndex = 3;
			this.slickButton1.Text = "Reply";
			// 
			// L_Time
			// 
			this.L_Time.AutoSize = true;
			this.TLP_Back.SetColumnSpan(this.L_Time, 2);
			this.L_Time.Location = new System.Drawing.Point(64, 27);
			this.L_Time.Name = "L_Time";
			this.L_Time.Size = new System.Drawing.Size(35, 13);
			this.L_Time.TabIndex = 1;
			this.L_Time.Text = "label1";
			// 
			// C_Message
			// 
			this.TLP_Back.SetColumnSpan(this.C_Message, 4);
			this.C_Message.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_Message.Location = new System.Drawing.Point(64, 53);
			this.C_Message.Name = "C_Message";
			this.C_Message.Size = new System.Drawing.Size(602, 44);
			this.C_Message.TabIndex = 0;
			this.C_Message.Paint += new System.Windows.Forms.PaintEventHandler(this.C_Message_Paint);
			// 
			// CommentControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.TLP_Back);
			this.Name = "CommentControl";
			this.Size = new System.Drawing.Size(669, 177);
			this.TLP_Back.ResumeLayout(false);
			this.TLP_Back.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private RoundedTableLayoutPanel TLP_Back;
	private SlickControl C_UserImage;
	private System.Windows.Forms.Label L_Author;
	private SlickLabel L_AuthorLabel;
	private System.Windows.Forms.Label L_Time;
	private SlickControl C_Message;
	private SlickButton slickButton1;
}
