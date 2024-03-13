using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Lists;
using Skyve.Systems.CS2.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_ReviewRequests : PanelContent
{
	private readonly List<ReviewRequest> _reviewRequests;

	public IPackageIdentity? CurrentPackage;

	private readonly IWorkshopService _workshopService = ServiceCenter.Get<IWorkshopService>();

	public PC_ReviewRequests(ReviewRequest[] reviewRequests)
	{
		InitializeComponent();

		tableLayoutPanel1.Controls.Add(reviewRequestList = new ReviewRequestList { Dock = DockStyle.Fill }, 0, 0);

		reviewRequestList.ItemMouseClick += ReviewRequestList_ItemMouseClick;

		_reviewRequests = [.. reviewRequests];

		packageCrList.SetItems(reviewRequests.Distinct(x => x.PackageId));
		packageCrList.CanDrawItem += PackageCrList_CanDrawItem;

		TB_Search.Placeholder = $"{LocaleSlickUI.Search}..";

		Text = LocaleCR.ReviewRequests.Format(string.Empty);

		SetPackage(packageCrList.SortedItems.FirstOrDefault());
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (Form != null)
		{
			if (base_P_Side.Visible)
			{
				Form.base_TLP_Side.TopRight = true;
				Form.base_TLP_Side.BotRight = true;
				Form.base_TLP_Side.Invalidate();
			}
		}

		base_P_Side.SendToBack();
	}

	protected override void UIChanged()
	{
		base_P_Side.Width = (int)(175 * UI.FontScale);
		base_TLP_Side.Padding = UI.Scale(new Padding(5), UI.FontScale);
		base_P_Side.Padding = UI.Scale(new Padding(0, 5, 5, 5), UI.FontScale);
		tableLayoutPanel1.Padding = new Padding(0, (int)(30 * UI.FontScale), 0, 0);
		CustomTitleBounds = new Point(tableLayoutPanel1.Left, 0);

		base.UIChanged();

		B_DeleteRequests.Margin = slickSpacer3.Margin = B_Previous.Margin = B_Skip.Margin = B_Previous.Padding = B_Skip.Padding = UI.Scale(new Padding(5), UI.FontScale);
		slickSpacer3.Height = (int)UI.FontScale;
		B_Previous.Size = B_Skip.Size = UI.Scale(new Size(32, 32), UI.FontScale);
		L_Page.Font = UI.Font(7.5F, FontStyle.Bold);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		base_TLP_Side.BackColor = design.MenuColor;
		base_TLP_Side.ForeColor = design.MenuForeColor;
		L_Page.ForeColor = design.LabelColor;
	}

	public override bool CanExit(bool toBeDisposed)
	{
		Form.base_TLP_Side.TopRight = false;
		Form.base_TLP_Side.BotRight = false;
		Form.base_TLP_Side.Invalidate();

		return base.CanExit(toBeDisposed);
	}

	private void packageCrList_ItemMouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			SetPackage((IPackageIdentity)sender);
		}

		if (e.Button == MouseButtons.Right)
		{
			SlickToolStrip.Show(Form, packageCrList.PointToClient(e.Location), ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems(_workshopService.GetPackage((IPackageIdentity)sender)));
		}
	}

	private void SetPackage(IPackageIdentity? package)
	{
		CurrentPackage = package;

		packageCrList.CurrentPackage = package;
		packageCrList.Invalidate();
		reviewRequestList.SetItems(_reviewRequests.Where(x => x.PackageId == package?.Id));

		B_DeleteRequests.Text = LocaleCR.DeleteRequests.FormatPlural(reviewRequestList.ItemCount);
		B_DeleteRequests.Visible = package != null;

		var currentPage = -1;

		if (packageCrList.ItemCount > 0)
		{
			currentPage = packageCrList.SortedItems.IndexOf(package);

			if (currentPage < 0 || currentPage >= packageCrList.ItemCount)
			{
				return;
			}

			L_Page.Text = $"{currentPage + 1} / {packageCrList.ItemCount}";
		}
		else
		{
			L_Page.Text = $"0 / 0";
		}

		B_Previous.Enabled = currentPage > 0;
		B_Skip.Enabled = currentPage != packageCrList.ItemCount - 1;
	}

	private void PackageCrList_CanDrawItem(object sender, CanDrawItemEventArgs<IPackageIdentity> e)
	{
		var package = _workshopService.GetInfo(e.Item);

		if (package is null)
		{
			return;
		}

		e.DoNotDraw = !(TB_Search.Text.SearchCheck(package.Name)
			|| TB_Search.Text.SearchCheck(package.Author?.Name)
			|| package.Id.ToString().IndexOf(TB_Search.Text, StringComparison.OrdinalIgnoreCase) != -1);
	}

	private void ReviewRequestList_ItemMouseClick(object sender, MouseEventArgs e)
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

		foreach (var request in _reviewRequests.AllWhere(x => x.PackageId == CurrentPackage?.Id))
		{
			await ServiceCenter.Get<SkyveApiUtil>().ProcessReviewRequest(request);

			Remove(request);
		}

		B_DeleteRequests.Loading = false;
	}

	private void B_Skip_Click(object sender, EventArgs e)
	{
		if (B_Skip.Enabled)
		{
			SetPackage(ModifierKeys.HasFlag(Keys.Control) ? packageCrList.SortedItems.LastOrDefault() : packageCrList.SortedItems.Next(CurrentPackage, true));
		}
	}

	private void B_Previous_Click(object sender, EventArgs e)
	{
		if (B_Previous.Enabled)
		{
			SetPackage(ModifierKeys.HasFlag(Keys.Control) ? packageCrList.SortedItems.FirstOrDefault() : packageCrList.SortedItems.Previous(CurrentPackage, true));
		}
	}

	public void Remove(ReviewRequest request)
	{
		_reviewRequests.Remove(request);
		packageCrList.Remove(request);

		if (packageCrList.Items.FirstOrAny(x => x.Id == request.PackageId) is IPackageIdentity package)
		{
			SetPackage(package);
		}
		else
		{
			PushBack();
		}
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == (Keys.Control | Keys.F))
		{
			TB_Search.Focus();
			return true;
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}
}
