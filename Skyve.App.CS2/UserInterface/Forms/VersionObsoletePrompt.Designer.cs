namespace Skyve.App.CS2.UserInterface.Forms;

partial class VersionObsoletePrompt
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
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			this.TLP_Main = new System.Windows.Forms.TableLayoutPanel();
			this.PB_Icon = new System.Windows.Forms.PictureBox();
			this.L_Title = new System.Windows.Forms.Label();
			this.L_Description = new System.Windows.Forms.Label();
			this.B_Close = new SlickControls.SlickButton();
			this.B_Link = new SlickControls.SlickButton();
			this.base_P_Container.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.base_PB_Icon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Close)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Max)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Min)).BeginInit();
			this.TLP_Main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Icon)).BeginInit();
			this.SuspendLayout();
			// 
			// base_P_Container
			// 
			this.base_P_Container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(51)))), ((int)(((byte)(26)))));
			this.base_P_Container.Controls.Add(this.TLP_Main);
			this.base_P_Container.Size = new System.Drawing.Size(339, 450);
			// 
			// TLP_Main
			// 
			this.TLP_Main.ColumnCount = 2;
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Main.Controls.Add(this.PB_Icon, 0, 0);
			this.TLP_Main.Controls.Add(this.L_Title, 0, 1);
			this.TLP_Main.Controls.Add(this.L_Description, 0, 2);
			this.TLP_Main.Controls.Add(this.B_Close, 0, 3);
			this.TLP_Main.Controls.Add(this.B_Link, 1, 3);
			this.TLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_Main.Location = new System.Drawing.Point(1, 1);
			this.TLP_Main.Name = "TLP_Main";
			this.TLP_Main.RowCount = 4;
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Main.Size = new System.Drawing.Size(337, 448);
			this.TLP_Main.TabIndex = 0;
			// 
			// PB_Icon
			// 
			this.PB_Icon.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.TLP_Main.SetColumnSpan(this.PB_Icon, 2);
			this.PB_Icon.Location = new System.Drawing.Point(118, 160);
			this.PB_Icon.Name = "PB_Icon";
			this.PB_Icon.Size = new System.Drawing.Size(100, 50);
			this.PB_Icon.TabIndex = 0;
			this.PB_Icon.TabStop = false;
			this.PB_Icon.Paint += new System.Windows.Forms.PaintEventHandler(this.PB_Icon_Paint);
			// 
			// L_Title
			// 
			this.L_Title.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.L_Title.AutoSize = true;
			this.TLP_Main.SetColumnSpan(this.L_Title, 2);
			this.L_Title.Location = new System.Drawing.Point(146, 370);
			this.L_Title.Name = "L_Title";
			this.L_Title.Size = new System.Drawing.Size(45, 19);
			this.L_Title.TabIndex = 1;
			this.L_Title.Text = "label1";
			// 
			// L_Description
			// 
			this.L_Description.AutoSize = true;
			this.TLP_Main.SetColumnSpan(this.L_Description, 2);
			this.L_Description.Location = new System.Drawing.Point(3, 389);
			this.L_Description.Name = "L_Description";
			this.L_Description.Size = new System.Drawing.Size(45, 19);
			this.L_Description.TabIndex = 2;
			this.L_Description.Text = "label2";
			// 
			// B_Close
			// 
			this.B_Close.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.B_Close.AutoSize = true;
			this.B_Close.ButtonType = SlickControls.ButtonType.Dimmed;
			this.B_Close.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Close.Location = new System.Drawing.Point(3, 411);
			this.B_Close.Name = "B_Close";
			this.B_Close.Size = new System.Drawing.Size(95, 34);
			this.B_Close.SpaceTriggersClick = true;
			this.B_Close.TabIndex = 1;
			this.B_Close.Text = "CloseSkyve";
			this.B_Close.Click += new System.EventHandler(this.B_Close_Click);
			// 
			// B_Link
			// 
			this.B_Link.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_Link.AutoSize = true;
			this.B_Link.ButtonType = SlickControls.ButtonType.Active;
			this.B_Link.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon2.Name = "Link";
			this.B_Link.ImageName = dynamicIcon2;
			this.B_Link.Location = new System.Drawing.Point(161, 411);
			this.B_Link.Name = "B_Link";
			this.B_Link.Size = new System.Drawing.Size(173, 34);
			this.B_Link.SpaceTriggersClick = true;
			this.B_Link.TabIndex = 0;
			this.B_Link.Text = "ViewUpdateInstructions";
			this.B_Link.Click += new System.EventHandler(this.B_Link_Click);
			// 
			// VersionObsoletePrompt
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(350, 461);
			this.CurrentFormState = Extensions.FormState.Busy;
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.MaximizedBounds = new System.Drawing.Rectangle(0, 0, 2560, 1380);
			this.Name = "VersionObsoletePrompt";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "VersionObsolete";
			this.base_P_Container.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.base_PB_Icon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Close)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Max)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Min)).EndInit();
			this.TLP_Main.ResumeLayout(false);
			this.TLP_Main.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Icon)).EndInit();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel TLP_Main;
	private System.Windows.Forms.PictureBox PB_Icon;
	private System.Windows.Forms.Label L_Title;
	private System.Windows.Forms.Label L_Description;
	private SlickButton B_Close;
	private SlickButton B_Link;
}