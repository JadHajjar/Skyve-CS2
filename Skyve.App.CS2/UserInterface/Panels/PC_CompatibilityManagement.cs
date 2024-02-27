using Skyve.App.Interfaces;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_CompatibilityManagement : PC_PackagePageBase
{
	private readonly IPackageIdentity[] packages;

	public PC_CompatibilityManagement(IEnumerable<IPackageIdentity> packages) : base(packages.FirstOrDefault())
	{
		InitializeComponent();

		this.packages = packages.ToArray();

		if (this.packages.Length == 1)
		{
			Padding = new Padding(5, 0, 0, 0);
			base_P_Side.Visible = false;
		}
		else
		{
			packageCrList.SetItems(this.packages.Select(x => x.Id));
		}

		SetPackage(Package);
	}

	public PC_CompatibilityManagement() : base(new GenericPackageIdentity())
	{
		InitializeComponent();

		packages = [];

		SetPackage(Package);
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		PB_Icon.Cursor = L_Title.Cursor = Cursors.Hand;
		PB_Icon.MouseClick += PB_Icon_MouseClick;
		L_Title.MouseClick += PB_Icon_MouseClick;

		if (Form != null && base_P_Side.Visible)
		{
			Form.base_TLP_Side.TopRight = true;
			Form.base_TLP_Side.BotRight = true;
			Form.base_TLP_Side.Invalidate();

			slickTabControl.MouseDown += (s, e) => Form.ForceWindowMove(e);
		}

		TLP_Bottom.SendToBack();
		base_P_Side.SendToBack();
	}

	private void PB_Icon_MouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(Package, false);
		}
	}

	protected override void UIChanged()
	{
		base_P_Side.Width = (int)(175 * UI.FontScale);
		base_TLP_Side.Padding = UI.Scale(new Padding(5), UI.FontScale);
		base_P_Side.Padding = UI.Scale(new Padding(0, 5, 5, 5), UI.FontScale);
		slickTabControl.Padding = P_SideContainer.Padding = new Padding(0, (int)(30 * UI.FontScale), 0, 0);
		CustomTitleBounds = new Point(slickTabControl.Left, 0);

		base.UIChanged();

		I_Up.Margin = I_Down.Margin = I_Up.Padding = I_Down.Padding = TLP_Bottom.Padding = B_ReuseData.Margin = B_Apply.Margin = slickSpacer2.Margin = UI.Scale(new Padding(5), UI.FontScale);
		slickSpacer2.Height = (int)(2 * UI.FontScale);
		B_AddInteraction.Size = B_AddStatus.Size = UI.Scale(new Size(105, 70), UI.FontScale);
		B_AddInteraction.Margin = B_AddStatus.Margin = UI.Scale(new Padding(15), UI.FontScale);
		I_Up.Size = I_Down.Size = UI.Scale(new Size(28, 28), UI.FontScale);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		base_TLP_Side.BackColor = design.MenuColor;
		base_TLP_Side.ForeColor = design.MenuForeColor;
	}
}
