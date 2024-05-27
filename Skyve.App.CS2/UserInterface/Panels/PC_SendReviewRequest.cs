using Skyve.App.UserInterface.CompatibilityReport;
using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Systems.CS2.Utilities;

using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;

public partial class PC_SendReviewRequest : PC_PackagePageBase
{
	private IPackageStatusControl<StatusType, PackageStatus>? statusControl;
	private IPackageStatusControl<InteractionType, PackageInteraction>? interactionControl;

	public PC_SendReviewRequest(IPackageIdentity package) : base(package)
	{
		InitializeComponent();

		SetPackage(Package);
	}

	protected override void LocaleChanged()
	{
		base.LocaleChanged();

		Text = LocaleCR.RequestReview;
		L_Disclaimer.Text = LocaleCR.RequestReviewDisclaimer;
		B_Apply.Text = Locale.SendReview + "*";
		L_English.Text = Locale.UseEnglishPlease;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		foreach (Control item in TLP_MainInfo.Controls)
		{
			item.Margin = UI.Scale(new Padding(5), UI.FontScale);
		}

		TLP_Button.Padding = TLP_Description.Padding = P_Content.Padding = UI.Scale(new Padding(7), UI.FontScale);
		slickSpacer2.Margin = L_Disclaimer.Margin = B_Apply.Margin = B_Apply.Padding = TB_Note.Margin = UI.Scale(new Padding(5), UI.FontScale);
		B_AddInteraction.Padding = B_AddStatus.Padding = UI.Scale(new Padding(15), UI.FontScale);
		B_AddInteraction.Font = B_AddStatus.Font = UI.Font(9.75F);
		B_AddInteraction.Margin = B_AddStatus.Margin = UI.Scale(new Padding(50, 40, 0, 0), UI.UIScale);
		TB_Note.MinimumSize = UI.Scale(new Size(0, 100), UI.UIScale);
		L_Disclaimer.Font = UI.Font(7.5F, FontStyle.Bold | FontStyle.Italic);
		slickSpacer2.Height = (int)(2 * UI.FontScale);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		L_Disclaimer.ForeColor = design.InfoColor;
		L_English.ForeColor = design.YellowColor;
	}

	private void B_ReportIssue_Click(object sender, EventArgs e)
	{
		TLP_Actions.Hide();
		TLP_Button.Show();
		TLP_MainInfo.Show();
		TLP_Description.Show();
		P_Content.Show();

		var data = Package.GetPackageInfo();
		DD_Stability.SelectedItem = data?.Stability ?? PackageStability.NotReviewed;
		DD_PackageType.SelectedItem = data?.Type ?? PackageType.GenericPackage;
		DD_DLCs.SelectedItems = data is null ? Enumerable.Empty<IDlcInfo>() : ServiceCenter.Get<IDlcManager>().Dlcs.Where(x => data.RequiredDLCs?.Contains(x.Id) ?? false);
		DD_Usage.SelectedItems = Enum.GetValues(typeof(PackageUsage)).Cast<PackageUsage>().Where(x => data?.Usage.HasFlag(x) ?? true);
	}

	private void B_AddStatus_Click(object sender, EventArgs e)
	{
		P_Content.Controls.Add(statusControl = new IPackageStatusControl<StatusType, PackageStatus>(Package));

		statusControl.Margin = TB_Note.Margin;
		statusControl.P_Main.BackColor = default;
		statusControl.I_Close.Hide();
		statusControl.Dock = DockStyle.Top;

		TLP_Button.Show();
		TLP_Description.Show();
		P_Content.Show();
		TLP_Actions.Hide();
	}

	private void B_AddInteraction_Click(object sender, EventArgs e)
	{
		P_Content.Controls.Add(interactionControl = new IPackageStatusControl<InteractionType, PackageInteraction>(Package));

		interactionControl.Margin = TB_Note.Margin;
		interactionControl.P_Main.BackColor = default;
		interactionControl.I_Close.Hide();
		interactionControl.Dock = DockStyle.Top;

		TLP_Button.Show();
		TLP_Description.Show();
		P_Content.Show();
		TLP_Actions.Hide();
	}

	private async void B_Apply_Click(object sender, EventArgs e)
	{
		if (TB_Note.Text.AsEnumerable().Count(x => !char.IsWhiteSpace(x) && x is not '.' and not ',' and not '\'' and not '0') < 5)
		{
			ShowPrompt(Locale.AddMeaningfulDescription, PromptButtons.OK, PromptIcons.Hand);
			return;
		}

		if (B_Apply.Loading)
		{
			return;
		}

		B_Apply.Loading = true;

		var postPackage = new ReviewRequest
		{
			PackageId = Package.Id,
			PackageNote = TB_Note.Text
		};

		if (statusControl is not null)
		{
			postPackage.IsStatus = true;
			postPackage.StatusNote = statusControl.PackageStatus.Note;
			postPackage.StatusAction = (int)statusControl.PackageStatus.Action;
			postPackage.StatusPackages = statusControl.PackageStatus.Packages!.ListStrings(",");
			postPackage.StatusType = (int)statusControl.PackageStatus.Type;
		}
		else if (interactionControl is not null)
		{
			postPackage.IsInteraction = true;
			postPackage.StatusNote = interactionControl.PackageStatus.Note;
			postPackage.StatusAction = (int)interactionControl.PackageStatus.Action;
			postPackage.StatusPackages = interactionControl.PackageStatus.Packages!.ListStrings(",");
			postPackage.StatusType = (int)interactionControl.PackageStatus.Type;
		}
		else
		{
			postPackage.PackageStability = (int)DD_Stability.SelectedItem;
			postPackage.PackageType = (int)DD_PackageType.SelectedItem;
			postPackage.PackageUsage = (int)DD_Usage.SelectedItems.Aggregate((prev, next) => prev | next);
			postPackage.RequiredDLCs = DD_DLCs.SelectedItems.Select(x => x.Id).ListStrings(",");
		}

		postPackage.LogFile = await Task.Run(async () =>
		{
			using var stream = new MemoryStream();

			await ServiceCenter.Get<ILogUtil>().CreateZipToStream(stream);

			return stream.ToArray();
		});

		var response = await ServiceCenter.Get<SkyveApiUtil>().SendReviewRequest(postPackage);

		if (response.Success)
		{
			PushBack();

			ShowPrompt(Locale.ReviewRequestSent.Format(Package.CleanName()), PromptButtons.OK, PromptIcons.Info);
		}
		else
		{
			ShowPrompt(Locale.ReviewRequestFailed.Format(Package.CleanName(), response.Message), PromptButtons.OK, PromptIcons.Info);
		}

		B_Apply.Loading = false;
	}
}
