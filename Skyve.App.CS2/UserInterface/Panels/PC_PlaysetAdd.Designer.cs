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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PC_PlaysetAdd));
			this.TLP_New = new System.Windows.Forms.TableLayoutPanel();
			this.B_NewPlayset = new Skyve.App.UserInterface.Generic.NewProfileOptionControl();
			this.B_ClonePlayset = new Skyve.App.UserInterface.Generic.NewProfileOptionControl();
			this.B_ImportById = new Skyve.App.UserInterface.Generic.NewProfileOptionControl();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Cancel = new SlickControls.SlickButton();
			this.DAD_NewPlayset = new Skyve.App.UserInterface.Generic.DragAndDropControl();
			this.TLP_New.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// base_Text
			// 
			this.base_Text.ButtonType = SlickControls.ButtonType.Normal;
			this.base_Text.Size = new System.Drawing.Size(150, 39);
			this.base_Text.Text = "AddPlayset";
			// 
			// TLP_New
			// 
			this.TLP_New.ColumnCount = 3;
			this.TLP_New.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_New.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_New.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_New.Controls.Add(this.B_NewPlayset, 1, 1);
			this.TLP_New.Controls.Add(this.B_ClonePlayset, 1, 2);
			this.TLP_New.Controls.Add(this.B_ImportById, 1, 3);
			this.TLP_New.Controls.Add(this.tableLayoutPanel1, 0, 4);
			this.TLP_New.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TLP_New.Location = new System.Drawing.Point(0, 30);
			this.TLP_New.Name = "TLP_New";
			this.TLP_New.RowCount = 5;
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
			this.TLP_New.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.TLP_New.Size = new System.Drawing.Size(1182, 789);
			this.TLP_New.TabIndex = 0;
			// 
			// B_NewPlayset
			// 
			this.B_NewPlayset.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_NewPlayset.FromLink = false;
			this.B_NewPlayset.FromScratch = true;
			this.B_NewPlayset.Location = new System.Drawing.Point(516, 143);
			this.B_NewPlayset.Name = "B_NewPlayset";
			this.B_NewPlayset.Size = new System.Drawing.Size(150, 150);
			this.B_NewPlayset.TabIndex = 0;
			this.B_NewPlayset.Click += new System.EventHandler(this.NewPlayset_Click);
			// 
			// B_ClonePlayset
			// 
			this.B_ClonePlayset.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_ClonePlayset.FromLink = false;
			this.B_ClonePlayset.FromScratch = false;
			this.B_ClonePlayset.Location = new System.Drawing.Point(516, 299);
			this.B_ClonePlayset.Name = "B_ClonePlayset";
			this.B_ClonePlayset.Size = new System.Drawing.Size(150, 110);
			this.B_ClonePlayset.TabIndex = 1;
			this.B_ClonePlayset.Click += new System.EventHandler(this.CopyPlayset_Click);
			// 
			// B_ImportById
			// 
			this.B_ImportById.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_ImportById.FromLink = true;
			this.B_ImportById.FromScratch = false;
			this.B_ImportById.Location = new System.Drawing.Point(516, 415);
			this.B_ImportById.Name = "B_ImportById";
			this.B_ImportById.Size = new System.Drawing.Size(150, 110);
			this.B_ImportById.TabIndex = 2;
			this.B_ImportById.Visible = false;
			this.B_ImportById.Click += new System.EventHandler(this.B_ImportLink_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.TLP_New.SetColumnSpan(this.tableLayoutPanel1, 3);
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.B_Cancel, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.DAD_NewPlayset, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 531);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1176, 255);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// B_Cancel
			// 
			this.B_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Cancel.AutoSize = true;
			this.B_Cancel.ColorStyle = Extensions.ColorStyle.Red;
			this.B_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.B_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("B_Cancel.Image")));
			this.B_Cancel.Location = new System.Drawing.Point(1093, 218);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(80, 34);
			this.B_Cancel.SpaceTriggersClick = true;
			this.B_Cancel.TabIndex = 14;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// DAD_NewProfile
			// 
			this.DAD_NewPlayset.AllowDrop = true;
			this.DAD_NewPlayset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.DAD_NewPlayset.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DAD_NewPlayset.Location = new System.Drawing.Point(3, 102);
			this.DAD_NewPlayset.Name = "DAD_NewProfile";
			this.DAD_NewPlayset.Size = new System.Drawing.Size(150, 150);
			this.DAD_NewPlayset.TabIndex = 15;
			this.DAD_NewPlayset.Text = "DropNewPlayset";
			this.DAD_NewPlayset.FileSelected += new System.Action<string>(this.DAD_NewPlayset_FileSelected);
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
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel TLP_New;
	private NewProfileOptionControl B_NewPlayset;
	private NewProfileOptionControl B_ClonePlayset;
	private SlickControls.SlickButton B_Cancel;
	private DragAndDropControl DAD_NewPlayset;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private NewProfileOptionControl B_ImportById;
}
