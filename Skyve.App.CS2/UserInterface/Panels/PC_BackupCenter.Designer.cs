namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_BackupCenter
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
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			this.slickTabControl1 = new SlickControls.SlickTabControl();
			this.tab1 = new SlickControls.SlickTabControl.Tab();
			this.tab2 = new SlickControls.SlickTabControl.Tab();
			this.tab3 = new SlickControls.SlickTabControl.Tab();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(150, 39);
			this.base_Text.Text = "BackupCenter";
			// 
			// slickTabControl1
			// 
			this.slickTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl1.Location = new System.Drawing.Point(5, 30);
			this.slickTabControl1.Name = "slickTabControl1";
			this.slickTabControl1.Size = new System.Drawing.Size(921, 556);
			this.slickTabControl1.TabIndex = 2;
			this.slickTabControl1.Tabs = new SlickControls.SlickTabControl.Tab[] {
        this.tab1,
        this.tab2,
        this.tab3};
			// 
			// tab1
			// 
			this.tab1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.tab1.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon1.Name = "Dashboard";
			this.tab1.IconName = dynamicIcon1;
			this.tab1.LinkedControl = null;
			this.tab1.Location = new System.Drawing.Point(0, 5);
			this.tab1.Name = "tab1";
			this.tab1.Size = new System.Drawing.Size(156, 75);
			this.tab1.TabIndex = 2;
			this.tab1.TabStop = false;
			this.tab1.Text = "Dashboard";
			// 
			// tab2
			// 
			this.tab2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.tab2.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon2.Name = "SafeShield";
			this.tab2.IconName = dynamicIcon2;
			this.tab2.LinkedControl = null;
			this.tab2.Location = new System.Drawing.Point(156, 5);
			this.tab2.Name = "tab2";
			this.tab2.Size = new System.Drawing.Size(156, 75);
			this.tab2.TabIndex = 1;
			this.tab2.TabStop = false;
			this.tab2.Text = "BackupRestore";
			// 
			// tab3
			// 
			this.tab3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.tab3.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon3.Name = "Cog";
			this.tab3.IconName = dynamicIcon3;
			this.tab3.LinkedControl = null;
			this.tab3.Location = new System.Drawing.Point(312, 5);
			this.tab3.Name = "tab3";
			this.tab3.Size = new System.Drawing.Size(156, 75);
			this.tab3.TabIndex = 0;
			this.tab3.TabStop = false;
			this.tab3.Text = "Settings";
			// 
			// PC_BackupCenter
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.slickTabControl1);
			this.Name = "PC_BackupCenter";
			this.Size = new System.Drawing.Size(931, 591);
			this.Text = "BackupCenter";
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.slickTabControl1, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private SlickTabControl slickTabControl1;
	private SlickTabControl.Tab tab1;
	private SlickTabControl.Tab tab2;
	private SlickTabControl.Tab tab3;
}
