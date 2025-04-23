using Skyve.App.UserInterface.Generic;


namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_PlaysetAdd
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
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			this.TLP_New = new System.Windows.Forms.TableLayoutPanel();
			this.B_Cancel = new SlickControls.SlickButton();
			this.L_Title = new System.Windows.Forms.Label();
			this.slickSpacer1 = new SlickControls.SlickSpacer();
			this.B_NewPlayset = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.DAD_NewPlayset = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.B_ClonePlayset = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.B_ImportById = new Skyve.App.UserInterface.Generic.BigSelectionOptionControl();
			this.TLP_New.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.Size = new System.Drawing.Size(150, 39);
			this.base_Text.Text = "AddPlayset";
			// 
			// TLP_New
			// 
			this.TLP_New.ColumnCount = 5;
			this.TLP_New.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_New.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_New.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_New.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_New.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_New.Controls.Add(this.B_Cancel, 4, 5);
			this.TLP_New.Controls.Add(this.B_NewPlayset, 1, 4);
			this.TLP_New.Controls.Add(this.DAD_NewPlayset, 1, 5);
			this.TLP_New.Controls.Add(this.B_ClonePlayset, 2, 4);
			this.TLP_New.Controls.Add(this.B_ImportById, 3, 4);
			this.TLP_New.Controls.Add(this.L_Title, 0, 1);
			this.TLP_New.Controls.Add(this.slickSpacer1, 1, 2);
			this.TLP_New.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_New.Location = new System.Drawing.Point(0, 30);
			this.TLP_New.Name = "TLP_New";
			this.TLP_New.RowCount = 6;
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.TLP_New.Size = new System.Drawing.Size(1182, 789);
			this.TLP_New.TabIndex = 0;
			// 
			// B_Cancel
			// 
			this.B_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Cancel.AutoSize = true;
			this.B_Cancel.ColorStyle = Extensions.ColorStyle.Red;
			this.B_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon1.Name = "Disposable";
			this.B_Cancel.ImageName = dynamicIcon1;
			this.B_Cancel.Location = new System.Drawing.Point(1107, 760);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(72, 26);
			this.B_Cancel.SpaceTriggersClick = true;
			this.B_Cancel.TabIndex = 3;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// L_Title
			// 
			this.L_Title.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.L_Title.AutoSize = true;
			this.TLP_New.SetColumnSpan(this.L_Title, 5);
			this.L_Title.Location = new System.Drawing.Point(572, 96);
			this.L_Title.Name = "L_Title";
			this.L_Title.Size = new System.Drawing.Size(38, 13);
			this.L_Title.TabIndex = 4;
			this.L_Title.Text = "label1";
			// 
			// slickSpacer1
			// 
			this.TLP_New.SetColumnSpan(this.slickSpacer1, 3);
			this.slickSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer1.Location = new System.Drawing.Point(360, 112);
			this.slickSpacer1.Name = "slickSpacer1";
			this.slickSpacer1.Size = new System.Drawing.Size(462, 2);
			this.slickSpacer1.TabIndex = 5;
			this.slickSpacer1.TabStop = false;
			this.slickSpacer1.Text = "slickSpacer1";
			// 
			// B_NewPlayset
			// 
			this.B_NewPlayset.ButtonText = "Continue";
			this.B_NewPlayset.ColorStyle = Extensions.ColorStyle.Active;
			this.B_NewPlayset.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon2.Name = "New";
			this.B_NewPlayset.ImageName = dynamicIcon2;
			this.B_NewPlayset.Location = new System.Drawing.Point(360, 216);
			this.B_NewPlayset.Name = "B_NewPlayset";
			this.B_NewPlayset.Size = new System.Drawing.Size(150, 278);
			this.B_NewPlayset.TabIndex = 0;
			this.B_NewPlayset.Text = "StartScratch";
			this.B_NewPlayset.Title = "New";
			this.B_NewPlayset.Click += new System.EventHandler(this.NewPlayset_Click);
			// 
			// DAD_NewPlayset
			// 
			this.DAD_NewPlayset.AllowDrop = true;
			this.DAD_NewPlayset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TLP_New.SetColumnSpan(this.DAD_NewPlayset, 3);
			this.DAD_NewPlayset.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DAD_NewPlayset.Location = new System.Drawing.Point(360, 636);
			this.DAD_NewPlayset.Name = "DAD_NewPlayset";
			this.DAD_NewPlayset.Size = new System.Drawing.Size(462, 150);
			this.DAD_NewPlayset.TabIndex = 15;
			this.DAD_NewPlayset.TabStop = false;
			this.DAD_NewPlayset.Text = "DropNewPlayset";
			this.DAD_NewPlayset.FileSelected += new System.Action<string>(this.DAD_NewPlayset_FileSelected);
			// 
			// B_ClonePlayset
			// 
			this.B_ClonePlayset.ButtonText = "Continue";
			this.B_ClonePlayset.ColorStyle = Extensions.ColorStyle.Active;
			this.B_ClonePlayset.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon3.Name = "Playsets";
			this.B_ClonePlayset.ImageName = dynamicIcon3;
			this.B_ClonePlayset.Location = new System.Drawing.Point(516, 216);
			this.B_ClonePlayset.Name = "B_ClonePlayset";
			this.B_ClonePlayset.Size = new System.Drawing.Size(150, 278);
			this.B_ClonePlayset.TabIndex = 1;
			this.B_ClonePlayset.Text = "ContinueFromCurrent";
			this.B_ClonePlayset.Title = "Copy";
			this.B_ClonePlayset.Click += new System.EventHandler(this.CopyPlayset_Click);
			// 
			// B_ImportById
			// 
			this.B_ImportById.ButtonText = "Continue";
			this.B_ImportById.ColorStyle = Extensions.ColorStyle.Active;
			this.B_ImportById.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon4.Name = "LinkChain";
			this.B_ImportById.ImageName = dynamicIcon4;
			this.B_ImportById.Location = new System.Drawing.Point(672, 216);
			this.B_ImportById.Name = "B_ImportById";
			this.B_ImportById.Size = new System.Drawing.Size(150, 278);
			this.B_ImportById.TabIndex = 2;
			this.B_ImportById.Text = "ImportFromLink";
			this.B_ImportById.Title = "Import";
			this.B_ImportById.Visible = false;
			this.B_ImportById.Click += new System.EventHandler(this.B_ImportLink_Click);
			// 
			// PC_PlaysetAdd
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.TLP_New);
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(69)))));
			this.Name = "PC_PlaysetAdd";
			this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
			this.Size = new System.Drawing.Size(1182, 819);
			this.Text = "AddPlayset";
			this.Controls.SetChildIndex(this.TLP_New, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.TLP_New.ResumeLayout(false);
			this.TLP_New.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel TLP_New;
	private BigSelectionOptionControl B_NewPlayset;
	private BigSelectionOptionControl B_ClonePlayset;
	private SlickControls.SlickButton B_Cancel;
	private DragAndDropControl DAD_NewPlayset;
	private BigSelectionOptionControl B_ImportById;
	private System.Windows.Forms.Label L_Title;
	private SlickSpacer slickSpacer1;
}
