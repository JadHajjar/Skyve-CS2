using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Lists;
using Skyve.Systems.CS2.Utilities;

using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_ReviewRequests : PanelContent
{
	private List<ReviewRequest> _reviewRequests;

	public IPackageIdentity? CurrentPackage;

	private readonly IWorkshopService _workshopService = ServiceCenter.Get<IWorkshopService>();

	public PC_ReviewRequests(ReviewRequest[] reviewRequests)
	{
		InitializeComponent();

		_reviewRequests = [.. reviewRequests];

		packageCrList.SetItems(reviewRequests.Distinct(x => x.PackageId));
		packageCrList.CanDrawItem += PackageCrList_CanDrawItem;

		TB_Search.Placeholder = $"{LocaleSlickUI.Search}..";

		Text = LocaleCR.ReviewRequests.Format($"({reviewRequests?.Length})");

		SetPackage(packageCrList.Items.FirstOrDefault());
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		TLP_List.Padding = UI.Scale(new Padding(5), UI.FontScale);
		TLP_List.Width = (int)(210 * UI.FontScale);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);
	}

	private void packageCrList_ItemMouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			SetPackage((IPackageIdentity)sender);
		}

		if (e.Button == MouseButtons.Right)
		{
			SlickToolStrip.Show(Form, packageCrList.PointToClient(e.Location), ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems(_workshopService.GetPackage(new GenericPackageIdentity((ulong)sender)!)!));
		}
	}

	private void SetPackage(IPackageIdentity? package)
	{
		CurrentPackage = package;

		packageCrList.CurrentPackage = package;
		packageCrList.Invalidate();
		reviewRequestList1.SetItems(_reviewRequests.Where(x => x.PackageId == package?.Id));

		B_DeleteRequests.Text = LocaleCR.DeleteRequests.FormatPlural(reviewRequestList1.ItemCount);
		B_DeleteRequests.Visible = package != null;
	}

	private void PackageCrList_CanDrawItem(object sender, CanDrawItemEventArgs<IPackageIdentity> e)
	{
		var package = _workshopService.GetInfo(e.Item);

		if (package is null)
		{
			return;
		}

		e.DoNotDraw = !(TB_Search.Text.SearchCheck(package.ToString())
			|| TB_Search.Text.SearchCheck(package.Author?.Name)
			|| package.Id.ToString().IndexOf(TB_Search.Text, StringComparison.OrdinalIgnoreCase) != -1);
	}

	private void reviewRequestList1_ItemMouseClick(object sender, MouseEventArgs e)
	{
		Form.Invoke(() => Form.PushPanel(new PC_ViewReviewRequest((ReviewRequest)sender)));
	}

	private void TB_Search_TextChanged(object sender, EventArgs e)
	{
		TB_Search.ImageName = string.IsNullOrWhiteSpace(TB_Search.Text) ? "I_Search" : "I_ClearSearch";

		packageCrList.FilterChanged();
	}

	private void TB_Search_IconClicked(object sender, EventArgs e)
	{
		TB_Search.Text = string.Empty;
	}

	private async void B_DeleteRequests_Click(object sender, EventArgs e)
	{
		B_DeleteRequests.Loading = true;

		foreach (var request in _reviewRequests.Where(x => x.PackageId == CurrentPackage?.Id))
		{
			await ServiceCenter.Get<SkyveApiUtil>().ProcessReviewRequest(request);
		}

		OnShown();

		B_DeleteRequests.Loading = false;
	}
}
