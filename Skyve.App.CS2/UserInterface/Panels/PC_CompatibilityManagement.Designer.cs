namespace Skyve.App.CS2.UserInterface.Panels;

partial class PC_CompatibilityManagement
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
			SlickControls.DynamicIcon dynamicIcon3 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon2 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon4 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon5 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon6 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon7 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon8 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon9 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon10 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon11 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon12 = new SlickControls.DynamicIcon();
			SlickControls.DynamicIcon dynamicIcon13 = new SlickControls.DynamicIcon();
			this.slickTabControl = new SlickControls.SlickTabControl();
			this.T_Info = new SlickControls.SlickTabControl.Tab();
			this.TLP_MainInfo = new SlickControls.SmartTablePanel();
			this.TB_Note = new SlickControls.SlickTextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.P_Links = new SlickControls.RoundedGroupFlowLayoutPanel();
			this.T_NewLink = new Skyve.App.UserInterface.Content.TagControl();
			this.P_Tags = new SlickControls.RoundedGroupFlowLayoutPanel();
			this.T_NewTag = new Skyve.App.UserInterface.Content.TagControl();
			this.DD_Usage = new Skyve.App.UserInterface.Dropdowns.PackageUsageDropDown();
			this.CB_BlackListName = new SlickControls.SlickCheckbox();
			this.DD_DLCs = new Skyve.App.UserInterface.Dropdowns.DlcDropDown();
			this.DD_Stability = new Skyve.App.UserInterface.Dropdowns.PackageStabilityDropDown();
			this.CB_BlackListId = new SlickControls.SlickCheckbox();
			this.DD_PackageType = new Skyve.App.UserInterface.Dropdowns.PackageTypeDropDown();
			this.T_Statuses = new SlickControls.SlickTabControl.Tab();
			this.FLP_Statuses = new System.Windows.Forms.FlowLayoutPanel();
			this.B_AddStatus = new Skyve.App.UserInterface.Content.IconTopButton();
			this.T_Interactions = new SlickControls.SlickTabControl.Tab();
			this.FLP_Interactions = new System.Windows.Forms.FlowLayoutPanel();
			this.B_AddInteraction = new Skyve.App.UserInterface.Content.IconTopButton();
			this.TLP_Bottom = new System.Windows.Forms.TableLayoutPanel();
			this.slickSpacer2 = new SlickControls.SlickSpacer();
			this.B_Apply = new SlickControls.SlickButton();
			this.B_ReuseData = new SlickControls.SlickButton();
			this.base_P_Side = new System.Windows.Forms.Panel();
			this.base_TLP_Side = new SlickControls.RoundedTableLayoutPanel();
			this.packageCrList = new Skyve.App.UserInterface.Lists.PackageCrList();
			this.TB_Search = new SlickControls.SlickTextBox();
			this.B_Previous = new SlickControls.SlickIcon();
			this.B_Skip = new SlickControls.SlickIcon();
			this.slickSpacer3 = new SlickControls.SlickSpacer();
			this.L_Page = new System.Windows.Forms.Label();
			this.PB_Loading = new SlickControls.SlickPictureBox();
			this.P_Container = new System.Windows.Forms.Panel();
			this.TLP_MainInfo.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.P_Links.SuspendLayout();
			this.P_Tags.SuspendLayout();
			this.FLP_Statuses.SuspendLayout();
			this.FLP_Interactions.SuspendLayout();
			this.TLP_Bottom.SuspendLayout();
			this.base_P_Side.SuspendLayout();
			this.base_TLP_Side.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loading)).BeginInit();
			this.P_Container.SuspendLayout();
			this.SuspendLayout();
			// 
			// P_SideContainer
			// 
			this.P_SideContainer.Location = new System.Drawing.Point(1138, 0);
			this.P_SideContainer.Size = new System.Drawing.Size(200, 744);
			// 
			// base_Text
			// 
			this.base_Text.Location = new System.Drawing.Point(3, -27);
			// 
			// slickTabControl
			// 
			this.slickTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.slickTabControl.Location = new System.Drawing.Point(165, 0);
			this.slickTabControl.Margin = new System.Windows.Forms.Padding(0);
			this.slickTabControl.Name = "slickTabControl";
			this.slickTabControl.Size = new System.Drawing.Size(973, 675);
			this.slickTabControl.TabIndex = 15;
			this.slickTabControl.Tabs = new SlickControls.SlickTabControl.Tab[] {
        this.T_Info,
        this.T_Statuses,
        this.T_Interactions};
			// 
			// T_Info
			// 
			this.T_Info.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Info.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon1.Name = "I_Content";
			this.T_Info.IconName = dynamicIcon1;
			this.T_Info.LinkedControl = this.TLP_MainInfo;
			this.T_Info.Location = new System.Drawing.Point(0, 5);
			this.T_Info.Name = "T_Info";
			this.T_Info.Selected = true;
			this.T_Info.Size = new System.Drawing.Size(187, 90);
			this.T_Info.TabIndex = 2;
			this.T_Info.TabStop = false;
			this.T_Info.Text = "Info";
			// 
			// TLP_MainInfo
			// 
			this.TLP_MainInfo.ColumnCount = 3;
			this.TLP_MainInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.TLP_MainInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.TLP_MainInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.TLP_MainInfo.Controls.Add(this.TB_Note, 0, 4);
			this.TLP_MainInfo.Controls.Add(this.tableLayoutPanel1, 2, 2);
			this.TLP_MainInfo.Controls.Add(this.DD_Usage, 0, 2);
			this.TLP_MainInfo.Controls.Add(this.CB_BlackListName, 2, 1);
			this.TLP_MainInfo.Controls.Add(this.DD_DLCs, 0, 3);
			this.TLP_MainInfo.Controls.Add(this.DD_Stability, 0, 0);
			this.TLP_MainInfo.Controls.Add(this.CB_BlackListId, 2, 0);
			this.TLP_MainInfo.Controls.Add(this.DD_PackageType, 0, 1);
			this.TLP_MainInfo.Location = new System.Drawing.Point(0, 0);
			this.TLP_MainInfo.MaximumSize = new System.Drawing.Size(800, 0);
			this.TLP_MainInfo.Name = "TLP_MainInfo";
			this.TLP_MainInfo.RowCount = 5;
			this.TLP_MainInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_MainInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_MainInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_MainInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_MainInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_MainInfo.Size = new System.Drawing.Size(600, 392);
			this.TLP_MainInfo.TabIndex = 18;
			// 
			// TB_Note
			// 
			this.TLP_MainInfo.SetColumnSpan(this.TB_Note, 3);
			this.TB_Note.Dock = System.Windows.Forms.DockStyle.Top;
			this.TB_Note.LabelText = "Note";
			this.TB_Note.Location = new System.Drawing.Point(3, 321);
			this.TB_Note.MultiLine = true;
			this.TB_Note.Name = "TB_Note";
			this.TB_Note.Padding = new System.Windows.Forms.Padding(7, 23, 7, 7);
			this.TB_Note.Placeholder = "NoteInfo";
			this.TB_Note.SelectedText = "";
			this.TB_Note.SelectionLength = 0;
			this.TB_Note.SelectionStart = 0;
			this.TB_Note.Size = new System.Drawing.Size(594, 68);
			this.TB_Note.TabIndex = 19;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.P_Links, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.P_Tags, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(363, 107);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.TLP_MainInfo.SetRowSpan(this.tableLayoutPanel1, 2);
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(234, 208);
			this.tableLayoutPanel1.TabIndex = 17;
			// 
			// P_Links
			// 
			this.P_Links.AddOutline = true;
			this.P_Links.AutoSize = true;
			this.P_Links.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Links.Controls.Add(this.T_NewLink);
			this.P_Links.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon3.Name = "I_Link";
			this.P_Links.ImageName = dynamicIcon3;
			this.P_Links.Location = new System.Drawing.Point(3, 107);
			this.P_Links.Name = "P_Links";
			this.P_Links.Padding = new System.Windows.Forms.Padding(9, 53, 9, 9);
			this.P_Links.Size = new System.Drawing.Size(228, 98);
			this.P_Links.TabIndex = 20;
			this.P_Links.Text = "Links";
			// 
			// T_NewLink
			// 
			this.T_NewLink.AutoSize = true;
			this.T_NewLink.ColorStyle = Extensions.ColorStyle.Green;
			this.T_NewLink.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_NewLink.Display = false;
			dynamicIcon2.Name = "I_Add";
			this.T_NewLink.ImageName = dynamicIcon2;
			this.T_NewLink.Location = new System.Drawing.Point(12, 56);
			this.T_NewLink.Name = "T_NewLink";
			this.T_NewLink.Size = new System.Drawing.Size(67, 30);
			this.T_NewLink.SpaceTriggersClick = true;
			this.T_NewLink.TabIndex = 0;
			this.T_NewLink.TagInfo = null;
			this.T_NewLink.ToAddPreview = false;
			this.T_NewLink.Click += new System.EventHandler(this.T_NewLink_Click);
			// 
			// P_Tags
			// 
			this.P_Tags.AddOutline = true;
			this.P_Tags.AutoSize = true;
			this.P_Tags.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.P_Tags.Controls.Add(this.T_NewTag);
			this.P_Tags.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon4.Name = "I_Tag";
			this.P_Tags.ImageName = dynamicIcon4;
			this.P_Tags.Info = "";
			this.P_Tags.Location = new System.Drawing.Point(3, 3);
			this.P_Tags.Name = "P_Tags";
			this.P_Tags.Padding = new System.Windows.Forms.Padding(9, 53, 9, 9);
			this.P_Tags.Size = new System.Drawing.Size(228, 98);
			this.P_Tags.TabIndex = 19;
			this.P_Tags.Text = "GlobalTags";
			// 
			// T_NewTag
			// 
			this.T_NewTag.AutoSize = true;
			this.T_NewTag.ColorStyle = Extensions.ColorStyle.Green;
			this.T_NewTag.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_NewTag.Display = false;
			this.T_NewTag.ImageName = dynamicIcon2;
			this.T_NewTag.Location = new System.Drawing.Point(12, 56);
			this.T_NewTag.Name = "T_NewTag";
			this.T_NewTag.Size = new System.Drawing.Size(67, 30);
			this.T_NewTag.SpaceTriggersClick = true;
			this.T_NewTag.TabIndex = 0;
			this.T_NewTag.TagInfo = null;
			this.T_NewTag.ToAddPreview = false;
			this.T_NewTag.Click += new System.EventHandler(this.T_NewTag_Click);
			// 
			// DD_Usage
			// 
			this.DD_Usage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Usage.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Usage.Location = new System.Drawing.Point(3, 107);
			this.DD_Usage.Name = "DD_Usage";
			this.DD_Usage.Size = new System.Drawing.Size(234, 30);
			this.DD_Usage.TabIndex = 17;
			this.DD_Usage.Text = "Usage";
			// 
			// CB_BlackListName
			// 
			this.CB_BlackListName.AutoSize = true;
			this.CB_BlackListName.Checked = false;
			this.CB_BlackListName.CheckedText = null;
			this.CB_BlackListName.ColorStyle = Extensions.ColorStyle.Red;
			this.CB_BlackListName.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_BlackListName.DefaultValue = false;
			this.CB_BlackListName.EnterTriggersClick = false;
			this.CB_BlackListName.Location = new System.Drawing.Point(363, 55);
			this.CB_BlackListName.Name = "CB_BlackListName";
			this.CB_BlackListName.Size = new System.Drawing.Size(178, 46);
			this.CB_BlackListName.SpaceTriggersClick = true;
			this.CB_BlackListName.TabIndex = 17;
			this.CB_BlackListName.Text = "BlackListName";
			this.CB_BlackListName.UncheckedText = null;
			// 
			// DD_DLCs
			// 
			this.DD_DLCs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_DLCs.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_DLCs.Location = new System.Drawing.Point(3, 143);
			this.DD_DLCs.Name = "DD_DLCs";
			this.DD_DLCs.Size = new System.Drawing.Size(234, 35);
			this.DD_DLCs.TabIndex = 17;
			this.DD_DLCs.Text = "RequiredDLCs";
			// 
			// DD_Stability
			// 
			this.DD_Stability.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_Stability.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_Stability.Location = new System.Drawing.Point(3, 3);
			this.DD_Stability.Name = "DD_Stability";
			this.DD_Stability.Size = new System.Drawing.Size(234, 30);
			this.DD_Stability.TabIndex = 0;
			this.DD_Stability.Text = "Stability";
			// 
			// CB_BlackListId
			// 
			this.CB_BlackListId.AutoSize = true;
			this.CB_BlackListId.Checked = false;
			this.CB_BlackListId.CheckedText = null;
			this.CB_BlackListId.ColorStyle = Extensions.ColorStyle.Red;
			this.CB_BlackListId.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CB_BlackListId.DefaultValue = false;
			this.CB_BlackListId.EnterTriggersClick = false;
			this.CB_BlackListId.Location = new System.Drawing.Point(363, 3);
			this.CB_BlackListId.Name = "CB_BlackListId";
			this.CB_BlackListId.Size = new System.Drawing.Size(199, 46);
			this.CB_BlackListId.SpaceTriggersClick = true;
			this.CB_BlackListId.TabIndex = 17;
			this.CB_BlackListId.Text = "BlackListId";
			this.CB_BlackListId.UncheckedText = null;
			// 
			// DD_PackageType
			// 
			this.DD_PackageType.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DD_PackageType.Dock = System.Windows.Forms.DockStyle.Top;
			this.DD_PackageType.Location = new System.Drawing.Point(3, 55);
			this.DD_PackageType.Name = "DD_PackageType";
			this.DD_PackageType.Size = new System.Drawing.Size(234, 35);
			this.DD_PackageType.TabIndex = 17;
			this.DD_PackageType.Text = "PackageType";
			// 
			// T_Statuses
			// 
			this.T_Statuses.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Statuses.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon5.Name = "I_Statuses";
			this.T_Statuses.IconName = dynamicIcon5;
			this.T_Statuses.LinkedControl = this.FLP_Statuses;
			this.T_Statuses.Location = new System.Drawing.Point(187, 5);
			this.T_Statuses.Name = "T_Statuses";
			this.T_Statuses.Selected = false;
			this.T_Statuses.Size = new System.Drawing.Size(187, 90);
			this.T_Statuses.TabIndex = 0;
			this.T_Statuses.TabStop = false;
			this.T_Statuses.Text = "Statuses";
			// 
			// FLP_Statuses
			// 
			this.FLP_Statuses.AutoSize = true;
			this.FLP_Statuses.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Statuses.Controls.Add(this.B_AddStatus);
			this.FLP_Statuses.Location = new System.Drawing.Point(0, 0);
			this.FLP_Statuses.Name = "FLP_Statuses";
			this.FLP_Statuses.Size = new System.Drawing.Size(119, 36);
			this.FLP_Statuses.TabIndex = 18;
			this.FLP_Statuses.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.FLP_Statuses_ControlAdded);
			// 
			// B_AddStatus
			// 
			this.B_AddStatus.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon6.Name = "I_Add";
			this.B_AddStatus.ImageName = dynamicIcon6;
			this.B_AddStatus.LargeImage = true;
			this.B_AddStatus.Location = new System.Drawing.Point(3, 3);
			this.B_AddStatus.Name = "B_AddStatus";
			this.B_AddStatus.Padding = new System.Windows.Forms.Padding(9);
			this.B_AddStatus.Size = new System.Drawing.Size(113, 30);
			this.B_AddStatus.SpaceTriggersClick = true;
			this.B_AddStatus.TabIndex = 0;
			this.B_AddStatus.Text = "AddStatus";
			this.B_AddStatus.Click += new System.EventHandler(this.B_AddStatus_Click);
			// 
			// T_Interactions
			// 
			this.T_Interactions.Cursor = System.Windows.Forms.Cursors.Hand;
			this.T_Interactions.Dock = System.Windows.Forms.DockStyle.Left;
			dynamicIcon7.Name = "I_Switch";
			this.T_Interactions.IconName = dynamicIcon7;
			this.T_Interactions.LinkedControl = this.FLP_Interactions;
			this.T_Interactions.Location = new System.Drawing.Point(374, 5);
			this.T_Interactions.Name = "T_Interactions";
			this.T_Interactions.Selected = false;
			this.T_Interactions.Size = new System.Drawing.Size(187, 90);
			this.T_Interactions.TabIndex = 1;
			this.T_Interactions.TabStop = false;
			this.T_Interactions.Text = "Interactions";
			// 
			// FLP_Interactions
			// 
			this.FLP_Interactions.AutoSize = true;
			this.FLP_Interactions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.FLP_Interactions.Controls.Add(this.B_AddInteraction);
			this.FLP_Interactions.Location = new System.Drawing.Point(0, 0);
			this.FLP_Interactions.Name = "FLP_Interactions";
			this.FLP_Interactions.Size = new System.Drawing.Size(144, 36);
			this.FLP_Interactions.TabIndex = 19;
			// 
			// B_AddInteraction
			// 
			this.B_AddInteraction.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon8.Name = "I_Add";
			this.B_AddInteraction.ImageName = dynamicIcon8;
			this.B_AddInteraction.LargeImage = true;
			this.B_AddInteraction.Location = new System.Drawing.Point(3, 3);
			this.B_AddInteraction.Name = "B_AddInteraction";
			this.B_AddInteraction.Padding = new System.Windows.Forms.Padding(9);
			this.B_AddInteraction.Size = new System.Drawing.Size(138, 30);
			this.B_AddInteraction.SpaceTriggersClick = true;
			this.B_AddInteraction.TabIndex = 1;
			this.B_AddInteraction.Text = "AddInteraction";
			this.B_AddInteraction.Click += new System.EventHandler(this.B_AddInteraction_Click);
			// 
			// TLP_Bottom
			// 
			this.TLP_Bottom.AutoSize = true;
			this.TLP_Bottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TLP_Bottom.ColumnCount = 3;
			this.TLP_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TLP_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.TLP_Bottom.Controls.Add(this.slickSpacer2, 0, 0);
			this.TLP_Bottom.Controls.Add(this.B_Apply, 2, 1);
			this.TLP_Bottom.Controls.Add(this.B_ReuseData, 1, 1);
			this.TLP_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.TLP_Bottom.Location = new System.Drawing.Point(165, 675);
			this.TLP_Bottom.Name = "TLP_Bottom";
			this.TLP_Bottom.RowCount = 2;
			this.TLP_Bottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Bottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.TLP_Bottom.Size = new System.Drawing.Size(973, 69);
			this.TLP_Bottom.TabIndex = 16;
			// 
			// slickSpacer2
			// 
			this.TLP_Bottom.SetColumnSpan(this.slickSpacer2, 3);
			this.slickSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer2.Location = new System.Drawing.Point(3, 3);
			this.slickSpacer2.Name = "slickSpacer2";
			this.slickSpacer2.Size = new System.Drawing.Size(967, 23);
			this.slickSpacer2.TabIndex = 0;
			this.slickSpacer2.TabStop = false;
			this.slickSpacer2.Text = "slickSpacer2";
			// 
			// B_Apply
			// 
			this.B_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Apply.AutoSize = true;
			this.B_Apply.ButtonType = SlickControls.ButtonType.Active;
			this.B_Apply.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon9.Name = "I_Ok";
			this.B_Apply.ImageName = dynamicIcon9;
			this.B_Apply.Location = new System.Drawing.Point(829, 32);
			this.B_Apply.Name = "B_Apply";
			this.B_Apply.Size = new System.Drawing.Size(141, 34);
			this.B_Apply.SpaceTriggersClick = true;
			this.B_Apply.TabIndex = 18;
			this.B_Apply.Text = "ApplyContinue";
			this.B_Apply.Click += new System.EventHandler(this.B_Apply_Click);
			// 
			// B_ReuseData
			// 
			this.B_ReuseData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.B_ReuseData.AutoSize = true;
			this.B_ReuseData.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon10.Name = "I_Refresh";
			this.B_ReuseData.ImageName = dynamicIcon10;
			this.B_ReuseData.Location = new System.Drawing.Point(708, 32);
			this.B_ReuseData.Name = "B_ReuseData";
			this.B_ReuseData.Size = new System.Drawing.Size(115, 34);
			this.B_ReuseData.SpaceTriggersClick = true;
			this.B_ReuseData.TabIndex = 19;
			this.B_ReuseData.Text = "ReuseData";
			this.B_ReuseData.Visible = false;
			this.B_ReuseData.Click += new System.EventHandler(this.B_ReuseData_Click);
			// 
			// base_P_Side
			// 
			this.base_P_Side.Controls.Add(this.base_TLP_Side);
			this.base_P_Side.Dock = System.Windows.Forms.DockStyle.Left;
			this.base_P_Side.Location = new System.Drawing.Point(0, 0);
			this.base_P_Side.Name = "base_P_Side";
			this.base_P_Side.Size = new System.Drawing.Size(165, 744);
			this.base_P_Side.TabIndex = 17;
			// 
			// base_TLP_Side
			// 
			this.base_TLP_Side.BotLeft = true;
			this.base_TLP_Side.ColumnCount = 3;
			this.base_TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.base_TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.base_TLP_Side.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.base_TLP_Side.Controls.Add(this.packageCrList, 0, 1);
			this.base_TLP_Side.Controls.Add(this.TB_Search, 0, 0);
			this.base_TLP_Side.Controls.Add(this.B_Previous, 0, 3);
			this.base_TLP_Side.Controls.Add(this.B_Skip, 2, 3);
			this.base_TLP_Side.Controls.Add(this.slickSpacer3, 0, 2);
			this.base_TLP_Side.Controls.Add(this.L_Page, 1, 3);
			this.base_TLP_Side.Dock = System.Windows.Forms.DockStyle.Fill;
			this.base_TLP_Side.Location = new System.Drawing.Point(0, 0);
			this.base_TLP_Side.Name = "base_TLP_Side";
			this.base_TLP_Side.RowCount = 4;
			this.base_TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.base_TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.base_TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.base_TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.base_TLP_Side.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.base_TLP_Side.Size = new System.Drawing.Size(165, 744);
			this.base_TLP_Side.TabIndex = 43;
			this.base_TLP_Side.TopLeft = true;
			// 
			// packageCrList
			// 
			this.packageCrList.AutoInvalidate = false;
			this.packageCrList.AutoScroll = true;
			this.base_TLP_Side.SetColumnSpan(this.packageCrList, 3);
			this.packageCrList.CurrentPackage = null;
			this.packageCrList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.packageCrList.HighlightOnHover = true;
			this.packageCrList.ItemHeight = 32;
			this.packageCrList.Location = new System.Drawing.Point(3, 61);
			this.packageCrList.Name = "packageCrList";
			this.packageCrList.SeparateWithLines = true;
			this.packageCrList.Size = new System.Drawing.Size(159, 623);
			this.packageCrList.TabIndex = 2;
			this.packageCrList.ItemMouseClick += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.packageCrList_ItemMouseClick);
			// 
			// TB_Search
			// 
			this.base_TLP_Side.SetColumnSpan(this.TB_Search, 3);
			this.TB_Search.Dock = System.Windows.Forms.DockStyle.Top;
			dynamicIcon11.Name = "I_Search";
			this.TB_Search.ImageName = dynamicIcon11;
			this.TB_Search.Location = new System.Drawing.Point(3, 3);
			this.TB_Search.Name = "TB_Search";
			this.TB_Search.Padding = new System.Windows.Forms.Padding(7, 7, 85, 7);
			this.TB_Search.Placeholder = "SearchGenericPackages";
			this.TB_Search.SelectedText = "";
			this.TB_Search.SelectionLength = 0;
			this.TB_Search.SelectionStart = 0;
			this.TB_Search.ShowLabel = false;
			this.TB_Search.Size = new System.Drawing.Size(159, 52);
			this.TB_Search.TabIndex = 3;
			this.TB_Search.TextChanged += new System.EventHandler(this.TB_Search_TextChanged);
			this.TB_Search.IconClicked += new System.EventHandler(this.TB_Search_IconClicked);
			// 
			// B_Previous
			// 
			this.B_Previous.ActiveColor = null;
			this.B_Previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.B_Previous.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon12.Name = "I_ArrowUp";
			this.B_Previous.ImageName = dynamicIcon12;
			this.B_Previous.Location = new System.Drawing.Point(3, 707);
			this.B_Previous.Margin = new System.Windows.Forms.Padding(0);
			this.B_Previous.Name = "B_Previous";
			this.B_Previous.Size = new System.Drawing.Size(76, 37);
			this.B_Previous.TabIndex = 4;
			this.B_Previous.Click += new System.EventHandler(this.B_Previous_Click);
			// 
			// B_Skip
			// 
			this.B_Skip.ActiveColor = null;
			this.B_Skip.Cursor = System.Windows.Forms.Cursors.Hand;
			dynamicIcon13.Name = "I_ArrowDown";
			this.B_Skip.ImageName = dynamicIcon13;
			this.B_Skip.Location = new System.Drawing.Point(85, 707);
			this.B_Skip.Margin = new System.Windows.Forms.Padding(0);
			this.B_Skip.Name = "B_Skip";
			this.B_Skip.Size = new System.Drawing.Size(77, 37);
			this.B_Skip.TabIndex = 5;
			this.B_Skip.Click += new System.EventHandler(this.B_Skip_Click);
			// 
			// slickSpacer3
			// 
			this.base_TLP_Side.SetColumnSpan(this.slickSpacer3, 3);
			this.slickSpacer3.Dock = System.Windows.Forms.DockStyle.Top;
			this.slickSpacer3.Location = new System.Drawing.Point(3, 690);
			this.slickSpacer3.Name = "slickSpacer3";
			this.slickSpacer3.Size = new System.Drawing.Size(159, 14);
			this.slickSpacer3.TabIndex = 6;
			this.slickSpacer3.TabStop = false;
			this.slickSpacer3.Text = "slickSpacer3";
			// 
			// L_Page
			// 
			this.L_Page.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.L_Page.AutoSize = true;
			this.L_Page.Location = new System.Drawing.Point(82, 716);
			this.L_Page.Name = "L_Page";
			this.L_Page.Size = new System.Drawing.Size(0, 19);
			this.L_Page.TabIndex = 7;
			// 
			// PB_Loading
			// 
			this.PB_Loading.LoaderSpeed = 1D;
			this.PB_Loading.Location = new System.Drawing.Point(484, 3);
			this.PB_Loading.Name = "PB_Loading";
			this.PB_Loading.Size = new System.Drawing.Size(74, 44);
			this.PB_Loading.TabIndex = 18;
			this.PB_Loading.TabStop = false;
			this.PB_Loading.Visible = false;
			// 
			// P_Container
			// 
			this.P_Container.Controls.Add(this.slickTabControl);
			this.P_Container.Controls.Add(this.TLP_Bottom);
			this.P_Container.Controls.Add(this.base_P_Side);
			this.P_Container.Dock = System.Windows.Forms.DockStyle.Fill;
			this.P_Container.Location = new System.Drawing.Point(0, 0);
			this.P_Container.Name = "P_Container";
			this.P_Container.Size = new System.Drawing.Size(1138, 744);
			this.P_Container.TabIndex = 19;
			// 
			// PC_CompatibilityManagement
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.P_Container);
			this.Controls.Add(this.PB_Loading);
			this.Name = "PC_CompatibilityManagement";
			this.Padding = new System.Windows.Forms.Padding(0);
			this.Size = new System.Drawing.Size(1338, 744);
			this.Controls.SetChildIndex(this.PB_Loading, 0);
			this.Controls.SetChildIndex(this.P_SideContainer, 0);
			this.Controls.SetChildIndex(this.base_Text, 0);
			this.Controls.SetChildIndex(this.P_Container, 0);
			this.TLP_MainInfo.ResumeLayout(false);
			this.TLP_MainInfo.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.P_Links.ResumeLayout(false);
			this.P_Links.PerformLayout();
			this.P_Tags.ResumeLayout(false);
			this.P_Tags.PerformLayout();
			this.FLP_Statuses.ResumeLayout(false);
			this.FLP_Interactions.ResumeLayout(false);
			this.TLP_Bottom.ResumeLayout(false);
			this.TLP_Bottom.PerformLayout();
			this.base_P_Side.ResumeLayout(false);
			this.base_TLP_Side.ResumeLayout(false);
			this.base_TLP_Side.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PB_Loading)).EndInit();
			this.P_Container.ResumeLayout(false);
			this.P_Container.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private SlickTabControl slickTabControl;
	private SlickTabControl.Tab T_Info;
	private SlickTabControl.Tab T_Interactions;
	private SlickTabControl.Tab T_Statuses;
	private System.Windows.Forms.FlowLayoutPanel FLP_Statuses;
	private Skyve.App.UserInterface.Content.IconTopButton B_AddStatus;
	private System.Windows.Forms.FlowLayoutPanel FLP_Interactions;
	private Skyve.App.UserInterface.Content.IconTopButton B_AddInteraction;
	private System.Windows.Forms.TableLayoutPanel TLP_Bottom;
	private SlickSpacer slickSpacer2;
	private SlickButton B_Apply;
	private SlickButton B_ReuseData;
	private System.Windows.Forms.Panel base_P_Side;
	internal RoundedTableLayoutPanel base_TLP_Side;
	private App.UserInterface.Lists.PackageCrList packageCrList;
	private SlickTextBox TB_Search;
	private SlickIcon B_Previous;
	private SlickIcon B_Skip;
	private SmartTablePanel TLP_MainInfo;
	private SlickTextBox TB_Note;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private RoundedGroupFlowLayoutPanel P_Links;
	private App.UserInterface.Content.TagControl T_NewLink;
	private RoundedGroupFlowLayoutPanel P_Tags;
	private App.UserInterface.Content.TagControl T_NewTag;
	private App.UserInterface.Dropdowns.PackageStabilityDropDown DD_Stability;
	private App.UserInterface.Dropdowns.DlcDropDown DD_DLCs;
	private App.UserInterface.Dropdowns.PackageUsageDropDown DD_Usage;
	private SlickCheckbox CB_BlackListId;
	private SlickCheckbox CB_BlackListName;
	private App.UserInterface.Dropdowns.PackageTypeDropDown DD_PackageType;
	private SlickSpacer slickSpacer3;
	private System.Windows.Forms.Label L_Page;
	private SlickPictureBox PB_Loading;
	private System.Windows.Forms.Panel P_Container;
}
