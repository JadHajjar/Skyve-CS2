using Skyve.App.UserInterface.Content;

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
			SlickControls.DynamicIcon dynamicIcon1 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			this.TLP_Back = new SlickControls.RoundedTableLayoutPanel();
			this.L_Author = new System.Windows.Forms.Label();
			this.L_AuthorLabel = new SlickControls.SlickLabel();
			this.B_Reply = new SlickControls.SlickButton();
			this.C_Message = new SlickControls.SlickControl();
			this.FLP_Thumbnails = new SlickControls.SmartFlowPanel();
			this.L_Time = new SlickControls.SlickLabel();
			this.B_Copy = new SlickControls.SlickButton();
			this.C_UserImage = new Skyve.App.UserInterface.Content.UserIcon();
			this.TLP_Back.SuspendLayout();
			this.SuspendLayout();
			// 
			// TLP_Back
			// 
			this.TLP_Back.AddShadow = true;
			this.TLP_Back.AutoSize = true;
			this.TLP_Back.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Back.ColumnCount = 6;
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Back.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Back.Controls.Add(this.C_UserImage, 0, 0);
			this.TLP_Back.Controls.Add(this.L_Author, 1, 0);
			this.TLP_Back.Controls.Add(this.L_AuthorLabel, 2, 0);
			this.TLP_Back.Controls.Add(this.B_Reply, 5, 0);
			this.TLP_Back.Controls.Add(this.C_Message, 1, 2);
			this.TLP_Back.Controls.Add(this.FLP_Thumbnails, 1, 3);
			this.TLP_Back.Controls.Add(this.L_Time, 1, 1);
			this.TLP_Back.Controls.Add(this.B_Copy, 4, 0);
			this.TLP_Back.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_Back.Location = new System.Drawing.Point(0, 0);
			this.TLP_Back.Name = "TLP_Back";
			this.TLP_Back.RowCount = 4;
			this.TLP_Back.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Back.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Back.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Back.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Back.Size = new System.Drawing.Size(669, 107);
			this.TLP_Back.TabIndex = 0;
			// 
			// L_Author
			// 
			this.L_Author.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Author.AutoSize = true;
			this.L_Author.Location = new System.Drawing.Point(64, 8);
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
			this.L_AuthorLabel.Enabled = false;
			this.L_AuthorLabel.Location = new System.Drawing.Point(105, 4);
			this.L_AuthorLabel.Name = "L_AuthorLabel";
			this.L_AuthorLabel.Selected = true;
			this.L_AuthorLabel.Size = new System.Drawing.Size(49, 21);
			this.L_AuthorLabel.SpaceTriggersClick = true;
			this.L_AuthorLabel.TabIndex = 2;
			this.L_AuthorLabel.Text = "Author";
			// 
			// B_Reply
			// 
			this.B_Reply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Reply.AutoSize = true;
			this.B_Reply.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "Reply";
			this.B_Reply.ImageName = dynamicIcon1;
			this.B_Reply.Location = new System.Drawing.Point(602, 3);
			this.B_Reply.Name = "B_Reply";
			this.TLP_Back.SetRowSpan(this.B_Reply, 2);
			this.B_Reply.Size = new System.Drawing.Size(64, 24);
			this.B_Reply.SpaceTriggersClick = true;
			this.B_Reply.TabIndex = 3;
			this.B_Reply.Text = "Reply";
			this.B_Reply.Click += new System.EventHandler(this.B_Reply_Click);
			// 
			// C_Message
			// 
			this.TLP_Back.SetColumnSpan(this.C_Message, 5);
			this.C_Message.Dock = System.Windows.Forms.DockStyle.Top;
			this.C_Message.Location = new System.Drawing.Point(64, 60);
			this.C_Message.Name = "C_Message";
			this.C_Message.Size = new System.Drawing.Size(602, 44);
			this.C_Message.TabIndex = 0;
			this.C_Message.Paint += new System.Windows.Forms.PaintEventHandler(this.C_Message_Paint);
			this.C_Message.MouseClick += new System.Windows.Forms.MouseEventHandler(this.C_Message_MouseClick);
			this.C_Message.MouseMove += new System.Windows.Forms.MouseEventHandler(this.C_Message_MouseMove);
			// 
			// FLP_Thumbnails
			// 
			this.TLP_Back.SetColumnSpan(this.FLP_Thumbnails, 5);
			this.FLP_Thumbnails.Dock = System.Windows.Forms.DockStyle.Top;
			this.FLP_Thumbnails.Location = new System.Drawing.Point(61, 107);
			this.FLP_Thumbnails.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
			this.FLP_Thumbnails.Name = "FLP_Thumbnails";
			this.FLP_Thumbnails.Size = new System.Drawing.Size(608, 0);
			this.FLP_Thumbnails.TabIndex = 4;
			// 
			// L_Time
			// 
			this.L_Time.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_Time.AutoSize = true;
			this.L_Time.ButtonType = SlickControls.ButtonType.Active;
			this.L_Time.ColorStyle = Extensions.ColorStyle.Icon;
			this.TLP_Back.SetColumnSpan(this.L_Time, 3);
			this.L_Time.Cursor = System.Windows.Forms.Cursors.Hand;
			this.L_Time.Display = true;
			this.L_Time.Enabled = false;
			this.L_Time.Location = new System.Drawing.Point(64, 33);
			this.L_Time.Name = "L_Time";
			this.L_Time.Selected = true;
			this.L_Time.Size = new System.Drawing.Size(49, 21);
			this.L_Time.SpaceTriggersClick = true;
			this.L_Time.TabIndex = 2;
			this.L_Time.Text = "Author";
			// 
			// B_Copy
			// 
			this.B_Copy.AutoSize = true;
			this.B_Copy.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Copy.Dock = System.Windows.Forms.DockStyle.Right;
			dynamicIcon2.Name = "Copy";
			this.B_Copy.ImageName = dynamicIcon2;
			this.B_Copy.Location = new System.Drawing.Point(572, 3);
			this.B_Copy.Name = "B_Copy";
			this.B_Copy.Size = new System.Drawing.Size(24, 24);
			this.B_Copy.SpaceTriggersClick = true;
			this.B_Copy.TabIndex = 3;
			this.B_Copy.Click += new System.EventHandler(this.B_Copy_Click);
			// 
			// C_UserImage
			// 
			this.C_UserImage.Location = new System.Drawing.Point(3, 3);
			this.C_UserImage.Name = "C_UserImage";
			this.TLP_Back.SetRowSpan(this.C_UserImage, 3);
			this.C_UserImage.Size = new System.Drawing.Size(55, 44);
			this.C_UserImage.TabIndex = 0;
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
	private UserIcon C_UserImage;
	private System.Windows.Forms.Label L_Author;
	private SlickLabel L_AuthorLabel;
	private SlickControl C_Message;
	private SlickButton B_Reply;
	private SmartFlowPanel FLP_Thumbnails;
	private SlickLabel L_Time;
	private SlickButton B_Copy;
}
