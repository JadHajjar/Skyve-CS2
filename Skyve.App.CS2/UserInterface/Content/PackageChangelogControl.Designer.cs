namespace Skyve.App.CS2.UserInterface.Content;

partial class PackageChangelogControl
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
			this.slickWebBrowser1 = new SlickControls.Controls.Advanced.SlickWebBrowser();
			this.roundedPanel1 = new SlickControls.RoundedPanel();
			this.SuspendLayout();
			// 
			// slickWebBrowser1
			// 
			this.slickWebBrowser1.Body = null;
			this.slickWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickWebBrowser1.Head = null;
			this.slickWebBrowser1.IsWebBrowserContextMenuEnabled = false;
			this.slickWebBrowser1.Location = new System.Drawing.Point(200, 0);
			this.slickWebBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.slickWebBrowser1.Name = "slickWebBrowser1";
			this.slickWebBrowser1.Size = new System.Drawing.Size(702, 644);
			this.slickWebBrowser1.TabIndex = 0;
			// 
			// roundedPanel1
			// 
			this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.roundedPanel1.Location = new System.Drawing.Point(0, 0);
			this.roundedPanel1.Name = "roundedPanel1";
			this.roundedPanel1.Size = new System.Drawing.Size(200, 644);
			this.roundedPanel1.TabIndex = 1;
			// 
			// PackageChangelogControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.slickWebBrowser1);
			this.Controls.Add(this.roundedPanel1);
			this.Name = "PackageChangelogControl";
			this.Size = new System.Drawing.Size(902, 644);
			this.ResumeLayout(false);

	}

	#endregion

	private SlickControls.Controls.Advanced.SlickWebBrowser slickWebBrowser1;
	private RoundedPanel roundedPanel1;
}
