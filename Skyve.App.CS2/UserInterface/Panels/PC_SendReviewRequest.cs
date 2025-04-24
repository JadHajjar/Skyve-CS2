using Skyve.Compatibility.Domain.Enums;
using Skyve.Systems.CS2.Utilities;

using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;

public partial class PC_SendReviewRequest : PC_PackagePageBase
{
	private string? lastSaveUrl;

	public PC_SendReviewRequest(IPackageIdentity package) : base(package)
	{
		InitializeComponent();

		SetPackage(Package);

		ServiceCenter.Get(out ILocationService locationService, out IPackageManager packageManager);

		DD_SaveFile.StartingFolder = IOSelectionDialog.CustomDirectory;
		DD_SaveFile.PinnedFolders = new()
		{
			["Your Save-games"] = IOSelectionDialog.CustomDirectory
		};
		DD_SaveFile.CustomFiles = packageManager.SaveGames.Where(x => x.IsLocal()).Select(x => new IOSelectionDialog.CustomFile
		{
			Name = x.Name,
			Icon = x.GetThumbnail(),
			Path = x.FilePath
		}).ToList();
	}

	protected override void LocaleChanged()
	{
		base.LocaleChanged();

		Text = LocaleCR.RequestReview;
		L_Title.Text = LocaleCR.ChooseWhatToRequest;
		L_Disclaimer.Text = LocaleCR.RequestReviewDisclaimer;
		B_ReportIssue.ButtonText = B_Apply.Text = Locale.SendReview + "*";
		L_English.Text = Locale.UseEnglishPlease;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		foreach (Control item in TLP_Info.Controls)
		{
			item.Margin = UI.Scale(new Padding(5));
		}

		TLP_Form.Padding = TLP_Description.Padding = UI.Scale(new Padding(7));
		slickSpacer2.Margin = L_Disclaimer.Margin = L_Disclaimer2.Margin = B_Apply.Margin = B_Apply.Padding = TB_Note.Margin = UI.Scale(new Padding(5));
		TB_Note.MinimumSize = UI.Scale(new Size(0, 100), UI.UIScale);
		L_Disclaimer.Font = L_Disclaimer2.Font = UI.Font(7.5F, FontStyle.Bold | FontStyle.Italic);
		slickSpacer2.Height = UI.Scale(2);
		L_Title.Font = UI.Font(12.75F, FontStyle.Bold);
		L_Title.Margin = UI.Scale(new Padding(6));
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		L_Disclaimer2.ForeColor = design.InfoColor;
		L_Disclaimer.ForeColor = L_English.ForeColor = design.YellowColor;
	}

	private void B_AddMissingInfo_Click(object sender, EventArgs e)
	{
		TLP_Actions.Hide();
		TLP_Form.Show();

		var data = Package.GetPackageInfo();
		DD_Stability.SelectedItem = data?.Stability ?? PackageStability.NotReviewed;
		DD_PackageType.SelectedItem = data?.Type ?? PackageType.GenericPackage;
		DD_DLCs.SelectedItems = data is null ? Enumerable.Empty<IDlcInfo>() : ServiceCenter.Get<IDlcManager>().Dlcs.Where(x => data.RequiredDLCs?.Contains(x.Id) ?? false);
		DD_Usage.SelectedItems = Enum.GetValues(typeof(PackageUsage)).Cast<PackageUsage>().Where(x => data?.Usage.HasFlag(x) ?? true);
	}

	private void B_ReportIssue_Click(object sender, EventArgs e)
	{
		TLP_Actions.Hide();
		TLP_Form.Show();
		TLP_Info.Show();
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
			PackageNote = TB_Note.Text,
			SaveUrl = lastSaveUrl,
			PackageStability = (int)DD_Stability.SelectedItem,
			PackageType = (int)DD_PackageType.SelectedItem,
			PackageUsage = (int)DD_Usage.SelectedItems.Aggregate((prev, next) => prev | next),
			RequiredDLCs = DD_DLCs.SelectedItems.ListStrings(x => x.Id.ToString(), ","),
			SavegameEffect = (int)DD_SavegameEffect.SelectedItem,
			IsMissingInfo = false,

			LogFile = await Task.Run(async () =>
			{
				using var stream = new MemoryStream();

				await ServiceCenter.Get<ILogUtil>().CreateZipToStream(stream);

				return stream.ToArray();
			})
		};

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

	private async void DD_SaveFile_FileSelected(string obj)
	{
		DD_SaveFile.SelectedFile = obj;
		DD_SaveFile.Loading = true;

		lastSaveUrl = null;

		try
		{
			if (!string.IsNullOrEmpty(obj))
			{
				lastSaveUrl = await ServiceCenter.Get<GoFileApiUtil>().UploadFile(obj);
			}
		}
		catch (Exception ex)
		{
			DD_SaveFile.SelectedFile = null;
			ShowPrompt(ex, Locale.CouldNotUploadFile);
		}

		DD_SaveFile.Loading = false;
	}
}
