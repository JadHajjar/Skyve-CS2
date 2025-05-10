using Skyve.Systems.CS2.Utilities;

using SkyveApi.Domain.CS2;

using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_CompatibilityCenter : PanelContent
{
	private readonly SkyveApiUtil _skyveApiUtil;

	private ReviewRequest[]? reviewRequests;
	private DateTime reviewRequestsTimestamp;

	public PC_CompatibilityCenter() : base(true)
	{
		ServiceCenter.Get(out _skyveApiUtil);

		InitializeComponent();

		AnnouncementDateFrom.Value = DateTime.Today;
		AnnouncementDateTo.Value = DateTime.Today.AddDays(7);
	}

	protected override async Task<bool> LoadDataAsync()
	{
		reviewRequests = await _skyveApiUtil.GetReviewRequests();
		reviewRequestsTimestamp = DateTime.Now;

		return true;
	}

	protected override void OnDataLoad()
	{
		UpdateReviewRequestsPanel();
	}

	protected override void OnShown()
	{
		reviewRequestsTimestamp = default;

		base.OnShown();
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		foreach (var panel in smartTablePanel.Controls.OfType<RoundedGroupTableLayoutPanel>())
		{
			panel.BackColor = design.AccentBackColor;
		}
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		AnnouncementText.Height = UI.Scale(100);

		foreach (var panel in smartTablePanel.Controls.OfType<RoundedGroupTableLayoutPanel>())
		{
			panel.Margin = UI.Scale(new Padding(10));

			foreach (Control item in panel.Controls)
			{
				item.Margin = UI.Scale(new Padding(5));
			}
		}
	}

	private async void AnnouncementButton_Click(object sender, EventArgs e)
	{
		if (AnnouncementButton.Loading)
		{
			return;
		}

		AnnouncementButton.Loading = true;

		var announcementData = new AnnouncementData
		{
			Title = AnnouncementTitle.Text,
			Description = AnnouncementText.Text,
			Date = AnnouncementDateFrom.Value,
			EndDate = AnnouncementDateTo.Value,
		};

		var result = await _skyveApiUtil.CreateAnnouncement(announcementData);

		AnnouncementButton.Loading = false;

		if (!result.Success)
		{
			ShowPrompt(result.Message, "Failed to send announcement", PromptButtons.OK, PromptIcons.Error);
			return;
		}

		AnnouncementTitle.Text = AnnouncementText.Text = string.Empty;

		AnnouncementButton.ImageName = "Check";

		await ServiceCenter.Get<ISkyveDataManager>().DownloadData();
		await Task.Delay(3000);

		AnnouncementButton.ImageName = "Send";
	}

	private async void B_ReviewRequests_Click(object sender, EventArgs e)
	{
		if (B_ReviewRequests.Loading)
		{
			return;
		}

		B_ReviewRequests.Loading = true;

		try
		{
			var reviewRequests = this.reviewRequests;

			if (reviewRequests is null || reviewRequestsTimestamp < DateTime.Now.AddMinutes(-10))
			{
				reviewRequests = await _skyveApiUtil.GetReviewRequests();

				this.reviewRequests = reviewRequests;
				reviewRequestsTimestamp = DateTime.Now;

				UpdateReviewRequestsPanel();
			}

			if (reviewRequests is not null)
			{
				Invoke(() => Form.PushPanel(new PC_ReviewRequests(reviewRequests)));
			}
		}
		catch (Exception ex)
		{
			ShowPrompt(ex, "Failed to load your packages");
		}

		B_ReviewRequests.Loading = false;
	}

	private void UpdateReviewRequestsPanel()
	{
		var active = reviewRequests != null && reviewRequests.Length > 0;

		activeReviewsControl.SetPackages(reviewRequests?.Distinct(x => x.PackageId) ?? []);
		activeReviewsControl.Loading = false;

		if (active)
		{
			activeReviewsControl.Visible = B_ReviewRequests.Visible = true;
			L_NoActiveRequests.Visible = false;
			roundedGroupTableLayoutPanel3.ColorStyle = reviewRequests!.Length > 12 ? ColorStyle.Red : ColorStyle.Orange;
		}
		else
		{
			activeReviewsControl.Visible = B_ReviewRequests.Visible = false;
			L_NoActiveRequests.Visible = true;
			roundedGroupTableLayoutPanel3.ColorStyle = ColorStyle.Green;
		}
	}

	private void B_Manage_Click(object sender, EventArgs e)
	{
		Form.PushPanel<PC_CompatibilityManagement>();
	}
}
