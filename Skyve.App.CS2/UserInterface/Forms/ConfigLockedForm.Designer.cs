namespace Skyve.App.CS2.UserInterface.Forms;

partial class ConfigLockedForm
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
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			this.TLP = new System.Windows.Forms.TableLayoutPanel();
			this.spacer = new SlickControls.SlickSpacer();
			this.L_Title = new System.Windows.Forms.Label();
			this.B_Retry = new SlickControls.SlickButton();
			this.L_RetryInfo = new System.Windows.Forms.Label();
			this.B_TakeLock = new SlickControls.SlickButton();
			this.L_TakeLockInfo = new System.Windows.Forms.Label();
			this.L_Info = new System.Windows.Forms.Label();
			this.I_Paradox = new SlickControls.SlickIcon();
			this.I_Close = new SlickControls.TopIcon();
			this.base_P_Container.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.base_PB_Icon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Close)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Max)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Min)).BeginInit();
			this.TLP.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.I_Close)).BeginInit();
			this.SuspendLayout();
			// 
			// base_P_Container
			// 
			this.base_P_Container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(199)))), ((int)(((byte)(145)))));
			this.base_P_Container.Controls.Add(this.TLP);
			this.base_P_Container.Size = new System.Drawing.Size(364, 432);
			// 
			// TLP
			// 
			this.TLP.ColumnCount = 3;
			this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
			this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
			this.TLP.Controls.Add(this.spacer, 1, 6);
			this.TLP.Controls.Add(this.L_Title, 1, 3);
			this.TLP.Controls.Add(this.B_Retry, 1, 7);
			this.TLP.Controls.Add(this.L_RetryInfo, 1, 8);
			this.TLP.Controls.Add(this.B_TakeLock, 1, 9);
			this.TLP.Controls.Add(this.L_TakeLockInfo, 1, 10);
			this.TLP.Controls.Add(this.L_Info, 1, 5);
			this.TLP.Controls.Add(this.I_Paradox, 1, 2);
			this.TLP.Controls.Add(this.I_Close, 1, 0);
			this.TLP.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP.Location = new System.Drawing.Point(1, 1);
			this.TLP.Name = "TLP";
			this.TLP.RowCount = 12;
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP.Size = new System.Drawing.Size(362, 430);
			this.TLP.TabIndex = 0;
			// 
			// spacer
			// 
			this.spacer.Dock = System.Windows.Forms.DockStyle.Top;
			this.spacer.Location = new System.Drawing.Point(64, 232);
			this.spacer.Name = "spacer";
			this.spacer.Size = new System.Drawing.Size(233, 1);
			this.spacer.TabIndex = 7;
			this.spacer.TabStop = false;
			this.spacer.Text = "slickSpacer1";
			// 
			// L_Title
			// 
			this.L_Title.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.L_Title.AutoSize = true;
			this.L_Title.Location = new System.Drawing.Point(158, 191);
			this.L_Title.Name = "L_Title";
			this.L_Title.Size = new System.Drawing.Size(45, 19);
			this.L_Title.TabIndex = 8;
			this.L_Title.Text = "label1";
			// 
			// B_Retry
			// 
			this.B_Retry.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.B_Retry.AutoSize = true;
			this.B_Retry.ButtonType = SlickControls.ButtonType.Active;
			this.B_Retry.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "Retry";
			this.B_Retry.ImageName = dynamicIcon1;
			this.B_Retry.Location = new System.Drawing.Point(64, 239);
			this.B_Retry.Name = "B_Retry";
			this.B_Retry.Size = new System.Drawing.Size(58, 34);
			this.B_Retry.SpaceTriggersClick = true;
			this.B_Retry.TabIndex = 9;
			this.B_Retry.Text = "Retry";
			this.B_Retry.Click += new System.EventHandler(this.B_Cloud_Click);
			// 
			// L_RetryInfo
			// 
			this.L_RetryInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.L_RetryInfo.AutoSize = true;
			this.L_RetryInfo.Location = new System.Drawing.Point(64, 276);
			this.L_RetryInfo.Name = "L_RetryInfo";
			this.L_RetryInfo.Size = new System.Drawing.Size(45, 19);
			this.L_RetryInfo.TabIndex = 8;
			this.L_RetryInfo.Text = "label1";
			// 
			// B_TakeLock
			// 
			this.B_TakeLock.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.B_TakeLock.AutoSize = true;
			this.B_TakeLock.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon2.Name = "Skip";
			this.B_TakeLock.ImageName = dynamicIcon2;
			this.B_TakeLock.Location = new System.Drawing.Point(64, 298);
			this.B_TakeLock.Name = "B_TakeLock";
			this.B_TakeLock.Size = new System.Drawing.Size(233, 34);
			this.B_TakeLock.SpaceTriggersClick = true;
			this.B_TakeLock.TabIndex = 9;
			this.B_TakeLock.Text = "ConfigLockedTakeLockButton";
			this.B_TakeLock.Click += new System.EventHandler(this.B_Local_Click);
			// 
			// L_TakeLockInfo
			// 
			this.L_TakeLockInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.L_TakeLockInfo.AutoSize = true;
			this.L_TakeLockInfo.Location = new System.Drawing.Point(252, 335);
			this.L_TakeLockInfo.Name = "L_TakeLockInfo";
			this.L_TakeLockInfo.Size = new System.Drawing.Size(45, 19);
			this.L_TakeLockInfo.TabIndex = 8;
			this.L_TakeLockInfo.Text = "label1";
			// 
			// L_Info
			// 
			this.L_Info.AutoSize = true;
			this.L_Info.Location = new System.Drawing.Point(64, 210);
			this.L_Info.Name = "L_Info";
			this.L_Info.Size = new System.Drawing.Size(45, 19);
			this.L_Info.TabIndex = 8;
			this.L_Info.Text = "label1";
			// 
			// I_Paradox
			// 
			this.I_Paradox.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.I_Paradox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_Paradox.Enabled = false;
			dynamicIcon3.Name = "PDXMods";
			this.I_Paradox.ImageName = dynamicIcon3;
			this.I_Paradox.Location = new System.Drawing.Point(131, 103);
			this.I_Paradox.Name = "I_Paradox";
			this.I_Paradox.Size = new System.Drawing.Size(99, 85);
			this.I_Paradox.TabIndex = 0;
			this.I_Paradox.TabStop = false;
			// 
			// I_Close
			// 
			this.I_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.I_Close.Color = SlickControls.TopIcon.IconStyle.Close;
			this.TLP.SetColumnSpan(this.I_Close, 2);
			this.I_Close.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_Close.LoaderSpeed = 1D;
			this.I_Close.Location = new System.Drawing.Point(310, 0);
			this.I_Close.Margin = new System.Windows.Forms.Padding(0);
			this.I_Close.Name = "I_Close";
			this.TLP.SetRowSpan(this.I_Close, 2);
			this.I_Close.Size = new System.Drawing.Size(52, 49);
			this.I_Close.TabIndex = 10;
			this.I_Close.TabStop = false;
			this.I_Close.Click += new System.EventHandler(this.I_Close_Click);
			// 
			// ConfigLockedForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(375, 443);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.MaximizedBounds = new System.Drawing.Rectangle(0, 0, 2560, 1377);
			this.Name = "ConfigLockedForm";
			this.Opacity = 0D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ParadoxLoginForm";
			this.base_P_Container.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.base_PB_Icon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Close)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Max)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.base_B_Min)).EndInit();
			this.TLP.ResumeLayout(false);
			this.TLP.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.I_Close)).EndInit();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel TLP;
	private SlickIcon I_Paradox;
	private SlickSpacer spacer;
	private System.Windows.Forms.Label L_Title;
	private SlickButton B_Retry;
	private SlickButton B_TakeLock;
	private System.Windows.Forms.Label L_RetryInfo;
	private System.Windows.Forms.Label L_TakeLockInfo;
	private System.Windows.Forms.Label L_Info;
	private TopIcon I_Close;
}