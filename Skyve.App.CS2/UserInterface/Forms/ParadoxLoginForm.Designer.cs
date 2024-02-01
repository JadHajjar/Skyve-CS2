namespace Skyve.App.CS2.UserInterface.Forms;

partial class ParadoxLoginForm
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
			this.TLP = new System.Windows.Forms.TableLayoutPanel();
			this.L_Disclaimer = new System.Windows.Forms.Label();
			this.I_Paradox = new SlickControls.SlickIcon();
			this.TB_Email = new SlickControls.SlickTextBox();
			this.TB_Password = new SlickControls.SlickTextBox();
			this.L_Title = new System.Windows.Forms.Label();
			this.I_Close = new SlickControls.TopIcon();
			this.B_Login = new SlickControls.SlickButton();
			this.CB_RememberMe = new SlickControls.SlickCheckbox();
			this.L_LoginFailed = new System.Windows.Forms.Label();
			this.L_RememberMeInfo = new System.Windows.Forms.Label();
			this.spacer = new SlickControls.SlickSpacer();
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
			this.base_P_Container.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(203)))), ((int)(((byte)(145)))));
			this.base_P_Container.Controls.Add(this.TLP);
			this.base_P_Container.Size = new System.Drawing.Size(354, 432);
			// 
			// TLP
			// 
			this.TLP.ColumnCount = 4;
			this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
			this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
			this.TLP.Controls.Add(this.L_Disclaimer, 1, 10);
			this.TLP.Controls.Add(this.I_Paradox, 1, 2);
			this.TLP.Controls.Add(this.TB_Email, 1, 4);
			this.TLP.Controls.Add(this.TB_Password, 1, 5);
			this.TLP.Controls.Add(this.L_Title, 1, 3);
			this.TLP.Controls.Add(this.I_Close, 2, 0);
			this.TLP.Controls.Add(this.B_Login, 2, 7);
			this.TLP.Controls.Add(this.CB_RememberMe, 1, 7);
			this.TLP.Controls.Add(this.L_LoginFailed, 1, 6);
			this.TLP.Controls.Add(this.L_RememberMeInfo, 1, 9);
			this.TLP.Controls.Add(this.spacer, 1, 8);
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
			this.TLP.Size = new System.Drawing.Size(352, 430);
			this.TLP.TabIndex = 0;
			// 
			// L_Disclaimer
			// 
			this.L_Disclaimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.L_Disclaimer.AutoSize = true;
			this.TLP.SetColumnSpan(this.L_Disclaimer, 2);
			this.L_Disclaimer.Location = new System.Drawing.Point(61, 376);
			this.L_Disclaimer.Name = "L_Disclaimer";
			this.L_Disclaimer.Size = new System.Drawing.Size(229, 19);
			this.L_Disclaimer.TabIndex = 6;
			this.L_Disclaimer.Text = "label1";
			this.L_Disclaimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// I_Paradox
			// 
			this.I_Paradox.ActiveColor = null;
			this.I_Paradox.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.TLP.SetColumnSpan(this.I_Paradox, 2);
			this.I_Paradox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_Paradox.Enabled = false;
			dynamicIcon1.Name = "I_Paradox";
			this.I_Paradox.ImageName = dynamicIcon1;
			this.I_Paradox.Location = new System.Drawing.Point(126, 62);
			this.I_Paradox.Name = "I_Paradox";
			this.I_Paradox.Size = new System.Drawing.Size(99, 85);
			this.I_Paradox.TabIndex = 0;
			this.I_Paradox.TabStop = false;
			// 
			// TB_Email
			// 
			this.TLP.SetColumnSpan(this.TB_Email, 2);
			this.TB_Email.LabelText = "";
			this.TB_Email.Location = new System.Drawing.Point(61, 172);
			this.TB_Email.Name = "TB_Email";
			this.TB_Email.Padding = new System.Windows.Forms.Padding(0, 16, 0, 16);
			this.TB_Email.Placeholder = "Email Address";
			this.TB_Email.Required = true;
			this.TB_Email.SelectedText = "";
			this.TB_Email.SelectionLength = 0;
			this.TB_Email.SelectionStart = 0;
			this.TB_Email.ShowLabel = false;
			this.TB_Email.Size = new System.Drawing.Size(200, 48);
			this.TB_Email.TabIndex = 0;
			this.TB_Email.Validation = SlickControls.ValidationType.Regex;
			this.TB_Email.ValidationRegex = "[a-z0-9!#$%&\'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&\'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-" +
    "z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
			// 
			// TB_Password
			// 
			this.TLP.SetColumnSpan(this.TB_Password, 2);
			dynamicIcon2.Name = "I_PasswordShow";
			this.TB_Password.ImageName = dynamicIcon2;
			this.TB_Password.LabelText = "";
			this.TB_Password.Location = new System.Drawing.Point(61, 226);
			this.TB_Password.Name = "TB_Password";
			this.TB_Password.Padding = new System.Windows.Forms.Padding(0, 16, 0, 16);
			this.TB_Password.Password = true;
			this.TB_Password.Placeholder = "Password";
			this.TB_Password.Required = true;
			this.TB_Password.SelectedText = "";
			this.TB_Password.SelectionLength = 0;
			this.TB_Password.SelectionStart = 0;
			this.TB_Password.ShowLabel = false;
			this.TB_Password.Size = new System.Drawing.Size(200, 48);
			this.TB_Password.TabIndex = 1;
			this.TB_Password.IconClicked += new System.EventHandler(this.TB_Password_IconClicked);
			// 
			// L_Title
			// 
			this.L_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.L_Title.AutoSize = true;
			this.TLP.SetColumnSpan(this.L_Title, 2);
			this.L_Title.Location = new System.Drawing.Point(61, 150);
			this.L_Title.Name = "L_Title";
			this.L_Title.Size = new System.Drawing.Size(229, 19);
			this.L_Title.TabIndex = 2;
			this.L_Title.Text = "label1";
			this.L_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// I_Close
			// 
			this.I_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.I_Close.AnimatedValue = 0;
			this.I_Close.Color = SlickControls.TopIcon.IconStyle.Close;
			this.TLP.SetColumnSpan(this.I_Close, 2);
			this.I_Close.Cursor = System.Windows.Forms.Cursors.Hand;
			this.I_Close.LoaderSpeed = 1D;
			this.I_Close.Location = new System.Drawing.Point(300, 0);
			this.I_Close.Margin = new System.Windows.Forms.Padding(0);
			this.I_Close.Name = "I_Close";
			this.TLP.SetRowSpan(this.I_Close, 2);
			this.I_Close.Size = new System.Drawing.Size(52, 50);
			this.I_Close.TabIndex = 4;
			this.I_Close.TabStop = false;
			this.I_Close.Click += new System.EventHandler(this.I_Close_Click);
			// 
			// B_Login
			// 
			this.B_Login.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Login.AutoSize = true;
			this.B_Login.ButtonType = SlickControls.ButtonType.Active;
			this.B_Login.ColorShade = null;
			this.B_Login.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Login.Location = new System.Drawing.Point(227, 299);
			this.B_Login.Name = "B_Login";
			this.B_Login.Size = new System.Drawing.Size(63, 36);
			this.B_Login.SpaceTriggersClick = true;
			this.B_Login.TabIndex = 2;
			this.B_Login.Text = "Login";
			this.B_Login.Click += new System.EventHandler(this.B_Login_Click);
			// 
			// CB_RememberMe
			// 
			this.CB_RememberMe.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.CB_RememberMe.AutoSize = true;
			this.CB_RememberMe.Checked = false;
			this.CB_RememberMe.CheckedText = null;
			this.CB_RememberMe.ColorShade = null;
			this.CB_RememberMe.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_RememberMe.DefaultValue = false;
			this.CB_RememberMe.EnterTriggersClick = false;
			this.CB_RememberMe.Location = new System.Drawing.Point(61, 299);
			this.CB_RememberMe.Name = "CB_RememberMe";
			this.CB_RememberMe.Size = new System.Drawing.Size(160, 48);
			this.CB_RememberMe.SpaceTriggersClick = true;
			this.CB_RememberMe.TabIndex = 3;
			this.CB_RememberMe.Text = "Remember me";
			this.CB_RememberMe.UncheckedText = null;
			this.CB_RememberMe.CheckChanged += new System.EventHandler(this.CB_RememberMe_CheckChanged);
			// 
			// L_LoginFailed
			// 
			this.L_LoginFailed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.L_LoginFailed.AutoSize = true;
			this.TLP.SetColumnSpan(this.L_LoginFailed, 2);
			this.L_LoginFailed.Location = new System.Drawing.Point(61, 277);
			this.L_LoginFailed.Name = "L_LoginFailed";
			this.L_LoginFailed.Size = new System.Drawing.Size(229, 19);
			this.L_LoginFailed.TabIndex = 2;
			this.L_LoginFailed.Text = "label1";
			this.L_LoginFailed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.L_LoginFailed.Visible = false;
			// 
			// L_RememberMeInfo
			// 
			this.L_RememberMeInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.L_RememberMeInfo.AutoSize = true;
			this.TLP.SetColumnSpan(this.L_RememberMeInfo, 2);
			this.L_RememberMeInfo.Location = new System.Drawing.Point(61, 357);
			this.L_RememberMeInfo.Name = "L_RememberMeInfo";
			this.L_RememberMeInfo.Size = new System.Drawing.Size(229, 19);
			this.L_RememberMeInfo.TabIndex = 2;
			this.L_RememberMeInfo.Text = "label1";
			this.L_RememberMeInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.L_RememberMeInfo.Visible = false;
			// 
			// spacer
			// 
			this.TLP.SetColumnSpan(this.spacer, 2);
			this.spacer.Dock = System.Windows.Forms.DockStyle.Top;
			this.spacer.Location = new System.Drawing.Point(61, 353);
			this.spacer.Name = "spacer";
			this.spacer.Size = new System.Drawing.Size(229, 1);
			this.spacer.TabIndex = 7;
			this.spacer.TabStop = false;
			this.spacer.Text = "slickSpacer1";
			// 
			// ParadoxLoginForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(365, 443);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.MaximizedBounds = new System.Drawing.Rectangle(0, 0, 2560, 1380);
			this.Name = "ParadoxLoginForm";
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
	private SlickTextBox TB_Email;
	private SlickTextBox TB_Password;
	private System.Windows.Forms.Label L_Title;
	private SlickButton B_Login;
	private TopIcon I_Close;
	private SlickCheckbox CB_RememberMe;
	private System.Windows.Forms.Label L_LoginFailed;
	private System.Windows.Forms.Label L_RememberMeInfo;
	private System.Windows.Forms.Label L_Disclaimer;
	private SlickSpacer spacer;
}