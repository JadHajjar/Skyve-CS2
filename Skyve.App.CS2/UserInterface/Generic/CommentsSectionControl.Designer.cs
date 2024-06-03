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
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			this.TLP_SendMessage = new System.Windows.Forms.TableLayoutPanel();
			this.C_UserIcon = new Skyve.App.UserInterface.Content.UserIcon();
			this.TB_Message = new SlickControls.SlickTextBox();
			this.B_Send = new SlickControls.SlickButton();
			this.L_LoggedInAs = new System.Windows.Forms.Label();
			this.spacer = new SlickControls.SlickSpacer();
			this.PB_Loading = new SlickControls.SlickPictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.slickScroll1 = new SlickControls.SlickScroll();
			this.P_Comments = new System.Windows.Forms.Panel();
			this.TLP_SendMessage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loading)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TLP_SendMessage
			// 
			this.TLP_SendMessage.AutoSize = true;
			this.TLP_SendMessage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_SendMessage.ColumnCount = 3;
			this.TLP_SendMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_SendMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_SendMessage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_SendMessage.Controls.Add(this.C_UserIcon, 0, 0);
			this.TLP_SendMessage.Controls.Add(this.TB_Message, 1, 1);
			this.TLP_SendMessage.Controls.Add(this.B_Send, 2, 0);
			this.TLP_SendMessage.Controls.Add(this.L_LoggedInAs, 1, 0);
			this.TLP_SendMessage.Controls.Add(this.spacer, 0, 2);
			this.TLP_SendMessage.Dock = System.Windows.Forms.DockStyle.Top;
			this.TLP_SendMessage.Location = new System.Drawing.Point(0, 0);
			this.TLP_SendMessage.Name = "TLP_SendMessage";
			this.TLP_SendMessage.RowCount = 3;
			this.TLP_SendMessage.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_SendMessage.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_SendMessage.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_SendMessage.Size = new System.Drawing.Size(729, 132);
			this.TLP_SendMessage.TabIndex = 2;
			this.TLP_SendMessage.Visible = false;
			// 
			// C_UserIcon
			// 
			this.C_UserIcon.Location = new System.Drawing.Point(3, 3);
			this.C_UserIcon.Name = "C_UserIcon";
			this.TLP_SendMessage.SetRowSpan(this.C_UserIcon, 2);
			this.C_UserIcon.Size = new System.Drawing.Size(83, 97);
			this.C_UserIcon.TabIndex = 0;
			// 
			// TB_Message
			// 
			this.TB_Message.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_Message.LabelText = "";
			this.TB_Message.Location = new System.Drawing.Point(92, 19);
			this.TB_Message.MultiLine = true;
			this.TB_Message.Name = "TB_Message";
			this.TB_Message.Padding = new System.Windows.Forms.Padding(6);
			this.TB_Message.Placeholder = "Your message...";
			this.TB_Message.SelectedText = "";
			this.TB_Message.SelectionLength = 0;
			this.TB_Message.SelectionStart = 0;
			this.TB_Message.ShowLabel = false;
			this.TB_Message.Size = new System.Drawing.Size(575, 44);
			this.TB_Message.TabIndex = 1;
			this.TB_Message.TextChanged += new System.EventHandler(this.TB_Message_TextChanged);
			this.TB_Message.Enter += new System.EventHandler(this.TB_Message_Enter);
			this.TB_Message.Leave += new System.EventHandler(this.TB_Message_Enter);
			// 
			// B_Send
			// 
			this.B_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.B_Send.AutoSize = true;
			this.B_Send.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Send.Enabled = false;
			dynamicIcon2.Name = "Send";
			this.B_Send.ImageName = dynamicIcon2;
			this.B_Send.Location = new System.Drawing.Point(673, 72);
			this.B_Send.Name = "B_Send";
			this.TLP_SendMessage.SetRowSpan(this.B_Send, 2);
			this.B_Send.Size = new System.Drawing.Size(53, 28);
			this.B_Send.SpaceTriggersClick = true;
			this.B_Send.TabIndex = 2;
			this.B_Send.Text = "Send";
			this.B_Send.Click += new System.EventHandler(this.B_Send_Click);
			// 
			// L_LoggedInAs
			// 
			this.L_LoggedInAs.AutoSize = true;
			this.L_LoggedInAs.Location = new System.Drawing.Point(89, 0);
			this.L_LoggedInAs.Margin = new System.Windows.Forms.Padding(0);
			this.L_LoggedInAs.Name = "L_LoggedInAs";
			this.L_LoggedInAs.Size = new System.Drawing.Size(44, 16);
			this.L_LoggedInAs.TabIndex = 3;
			this.L_LoggedInAs.Text = "label1";
			// 
			// spacer
			// 
			this.TLP_SendMessage.SetColumnSpan(this.spacer, 3);
			this.spacer.Dock = System.Windows.Forms.DockStyle.Top;
			this.spacer.Location = new System.Drawing.Point(3, 106);
			this.spacer.Name = "spacer";
			this.spacer.Size = new System.Drawing.Size(723, 23);
			this.spacer.TabIndex = 4;
			this.spacer.TabStop = false;
			this.spacer.Text = "slickSpacer1";
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
			// panel1
			// 
			this.panel1.Controls.Add(this.slickScroll1);
			this.panel1.Controls.Add(this.P_Comments);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 132);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(729, 124);
			this.panel1.TabIndex = 4;
			// 
			// slickScroll1
			// 
			this.slickScroll1.Dock = System.Windows.Forms.DockStyle.Right;
			this.slickScroll1.LinkedControl = this.P_Comments;
			this.slickScroll1.Location = new System.Drawing.Point(721, 0);
			this.slickScroll1.Name = "slickScroll1";
			this.slickScroll1.Size = new System.Drawing.Size(8, 124);
			this.slickScroll1.Style = SlickControls.StyleType.Vertical;
			this.slickScroll1.TabIndex = 1;
			this.slickScroll1.TabStop = false;
			this.slickScroll1.Text = "slickScroll1";
			this.slickScroll1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Scrollbar_Scroll);
			// 
			// P_Comments
			// 
			this.P_Comments.AutoSize = true;
			this.P_Comments.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Comments.Location = new System.Drawing.Point(133, 69);
			this.P_Comments.Name = "P_Comments";
			this.P_Comments.Size = new System.Drawing.Size(0, 0);
			this.P_Comments.TabIndex = 0;
			// 
			// CommentsSectionControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.PB_Loading);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.TLP_SendMessage);
			this.Name = "CommentsSectionControl";
			this.Size = new System.Drawing.Size(729, 256);
			this.TLP_SendMessage.ResumeLayout(false);
			this.TLP_SendMessage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loading)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel TLP_SendMessage;
	private SlickPictureBox PB_Loading;
	private App.UserInterface.Content.UserIcon C_UserIcon;
	private SlickTextBox TB_Message;
	private SlickButton B_Send;
	private System.Windows.Forms.Panel panel1;
	private System.Windows.Forms.Panel P_Comments;
	private SlickScroll slickScroll1;
	private System.Windows.Forms.Label L_LoggedInAs;
	private SlickSpacer spacer;
}
