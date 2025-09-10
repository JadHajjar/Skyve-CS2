using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.App.CS2.UserInterface.Content;
using Skyve.App.CS2.UserInterface.Generic;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Forms;
using Skyve.App.UserInterface.Panels;
using Skyve.App.Utilities;
using Skyve.Domain.CS2.Utilities;

using SlickControls;

using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PackageEdit : PanelContent
{
	protected readonly IncludedButton B_Incl;
	protected readonly PackageTitleControl L_Title;
	private readonly INotifier _notifier;
	private readonly IPackageUtil _packageUtil;
	private readonly ISettings _settings;
	private readonly IWorkshopService _workshopService;
	private readonly IModStagingDataBuilder _staging;
	private TagControl? addTagControl;

	public IPackageIdentity Package { get; private set; }

#nullable disable
	[Obsolete("DESIGNER ONLY", true)]
	public PC_PackageEdit()
	{
		InitializeComponent();
	}
#nullable enable

	public PC_PackageEdit(IPackageIdentity package, IModStagingDataBuilder staging)
	{
		ServiceCenter.Get(out _notifier, out _packageUtil, out _settings, out _workshopService, out IImageService imageService);

		InitializeComponent();

		Package = package;
		_staging = staging;

		TLP_Side.Controls.Add(B_Incl = new(package) { Dock = DockStyle.Top }, 0, 2);
		TLP_TopInfo.Controls.Add(L_Title = new(package) { Dock = DockStyle.Fill });
		L_Title.MouseClick += I_More_MouseClick;

		SetPackage(package, staging);
	}

	protected virtual void SetPackage(IPackageIdentity package, IModStagingDataBuilder staging)
	{
		Package = package;

		PB_Icon.Package = Package;
		B_Incl.Package = Package;
		L_Title.Package = Package;

		PB_Icon.Invalidate();
		B_Incl.Invalidate();
		L_Title.Invalidate();

		var workshopInfo = Package.GetWorkshopInfo();

		L_Author.Visible = workshopInfo is not null;
		L_Author.Author = workshopInfo?.Author;

		TB_Title.Text = staging.MetaData.DisplayName;
		TB_ShortDesc.Text = staging.MetaData.ShortDescription.Replace("\n", "\r\n");
		TB_LongDesc.Text = staging.MetaData.LongDescription.Replace("\n", "\r\n");
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		P_SideContainer.Width = UI.Scale(260);
		PB_Icon.Size = UI.Scale(new Size(72, 72));
		I_More.Size = UI.Scale(new Size(20, 28));
		TLP_Side.Padding = UI.Scale(new Padding(8, 0, 0, 0));
		TLP_TopInfo.Margin = B_Incl.Margin = base_slickSpacer.Margin = UI.Scale(new Padding(5));
		base_slickSpacer.Height = (int)UI.FontScale;
		L_Author.Margin = L_Title.Margin = UI.Scale(new Padding(5, 0, 0, 0));
		L_Author.Font = UI.Font(9.5F);
		TB_Title.Margin = TB_ShortDesc.Margin = TB_LongDesc.Margin = UI.Scale(new Padding(5));
		TB_ShortDesc.Height = UI.Scale(80);
		tableLayoutPanel1.Padding = tableLayoutPanel2.Padding = tableLayoutPanel3.Padding = UI.Scale(new Padding(5));
		slickTabControl1.Padding = UI.Scale(new Padding(5, 5, 0, 0));

		TLP_TopInfo.Height = UI.Scale(72);
	}

	protected override void LocaleChanged()
	{
		base.LocaleChanged();

		T_Images.Text = LocaleSlickUI.Image.Plural;
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

	}

	private void I_More_MouseClick(object sender, MouseEventArgs e)
	{
		if (sender == I_More || e.Button == MouseButtons.Right)
		{
			this.TryBeginInvoke(() =>
				SlickToolStrip.Show(
					App.Program.MainForm,
					ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems([Package]))
			);
		}
	}

	private void L_Author_Click(object sender, EventArgs e)
	{
		var workshopInfo = Package.GetWorkshopInfo();

		if (workshopInfo?.Author != null)
		{
			App.Program.MainForm.PushPanel(new PC_UserPage(workshopInfo.Author));
		}
	}
}
