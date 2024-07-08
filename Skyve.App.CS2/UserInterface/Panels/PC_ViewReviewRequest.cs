using Skyve.App.UserInterface.CompatibilityReport;
using Skyve.App.Utilities;
using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Systems.CS2.Utilities;

using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_ViewReviewRequest : PC_PackagePageBase
{
	private ReviewRequest _request;
	private readonly SlickControl? saveGameControl;
	private readonly SlickControl logControl;

	private readonly IWorkshopService _workshopService;
	private readonly IDlcManager _dlcManager;
	private readonly SkyveApiUtil _skyveApiUtil;

	public PC_ViewReviewRequest(ReviewRequest request) : base(request)
	{
		ServiceCenter.Get(out _dlcManager, out _skyveApiUtil, out _workshopService, out IUserService userService);
		InitializeComponent();
		_request = request;

		//TLP_Info.Controls.Add(new SteamUserControl(request.UserId) { InfoText = "Requested by", Dock = DockStyle.Top, Margin = UI.Scale(new Padding(5)) }, 0, 1);

		if (L_Savegame.Visible = !string.IsNullOrEmpty(request.SaveUrl))
		{
			saveGameControl = new SlickControl
			{
				Cursor = Cursors.Hand,
				Text = $"Download Save-Game",
				Size = UI.Scale(new Size(150, 75), UI.UIScale),
				Margin = UI.Scale(new Padding(5))
			};

			saveGameControl.Paint += SaveGameControl_Paint;
			saveGameControl.Click += SaveGameControl_Click;

			TLP_Main.Controls.Add(saveGameControl, 1, 3);
		}

		logControl = new SlickControl
		{
			Cursor = Cursors.Hand,
			Text = $"RequestBy_{userService.TryGetUser(_request.UserId)?.Name}_{DateTime.Now:yy-MM-dd_HH-mm}",
			Size = UI.Scale(new Size(150, 75), UI.UIScale),
			Margin = UI.Scale(new Padding(5))
		};

		logControl.Paint += LogControl_Paint;
		logControl.Click += LogControl_Click;

		TLP_Main.Controls.Add(logControl, 0, 3);

		L_Note.Text = request.PackageNote.IfEmpty("No description given");

		if (request.IsInteraction)
		{
			TLP_Main.Controls.Add(new IPackageStatusControl<InteractionType, PackageInteraction>(_workshopService.GetPackage(new GenericPackageIdentity(request.PackageId)), new PackageInteraction
			{
				Action = (StatusAction)request.StatusAction,
				IntType = request.StatusType,
				Note = request.StatusNote,
				Packages = request.StatusPackages?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToArray(),
			})
			{ BackColor = FormDesign.Design.AccentBackColor, Enabled = false }, 0, 6);
		}
		else if (request.IsStatus)
		{
			TLP_Main.Controls.Add(new IPackageStatusControl<StatusType, PackageStatus>(_workshopService.GetPackage(new GenericPackageIdentity(request.PackageId)), new PackageStatus
			{
				Action = (StatusAction)request.StatusAction,
				IntType = request.StatusType,
				Note = request.StatusNote,
				Packages = request.StatusPackages?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToArray(),
			})
			{ BackColor = FormDesign.Design.AccentBackColor, Enabled = false }, 0, 6);
		}
		else
		{
			DD_Stability.SelectedItem = (PackageStability)_request.PackageStability;
			DD_PackageType.SelectedItem = (PackageType)_request.PackageType;
			DD_DLCs.SelectedItems = _dlcManager.Dlcs.Where(x => _request.RequiredDLCs?.Contains(x.Id.ToString()) ?? false);
			DD_Usage.SelectedItems = Enum.GetValues(typeof(PackageUsage)).Cast<PackageUsage>().Where(x => ((PackageUsage)_request.PackageUsage).HasFlag(x));

			tableLayoutPanel3.Visible = true;
		}

		SetPackage(Package);
	}

	protected override void SetPackage(IPackageIdentity package)
	{
		base.SetPackage(package.GetWorkshopInfo() ?? package);
	}

	private async void LogControl_Click(object sender, EventArgs e)
	{
		if (logControl.Loading)
		{
			return;
		}

		if (_request.LogFile == null)
		{
			logControl.Loading = true;

			var request = await _skyveApiUtil.GetReviewRequest(_request.UserId ?? string.Empty, _request.PackageId);

			if (request is not null)
			{
				_request = request;
			}
		}

		if (_request.LogFile == null)
		{
			logControl.Dispose();
			return;
		}

		var fileName = CrossIO.Combine(ServiceCenter.Get<ILocationService>().SkyveDataPath, ".SupportLogs", logControl.Text + ".zip");

		Directory.CreateDirectory(Path.GetDirectoryName(fileName));

		File.WriteAllBytes(fileName, _request.LogFile);

		PlatformUtil.OpenFolder(fileName);

		logControl.Loading = false;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		L_ProposedChanges.Font = L_Desc.Font = L_LogReport.Font = L_Savegame.Font = UI.Font(9.75F, FontStyle.Bold);
		L_Note.Font = UI.Font(9.25F);
		L_LogReport.Margin = L_Savegame.Margin = slickSpacer3.Margin = UI.Scale(new Padding(3, 15, 3, 3));
		slickSpacer4.Margin = slickSpacer1.Margin = TB_Note.Margin = B_SendReply.Margin = UI.Scale(new Padding(5));
		L_ProposedChanges.Margin = UI.Scale(new Padding(3, 15, 3, 7));
		TLP_Main.Padding = UI.Scale(new Padding(10, 5, 10, 0));
		I_Copy.Size = UI.Scale(new Size(32, 32));
		I_Copy.Padding = UI.Scale(new Padding(4));
		slickSpacer3.Height = slickSpacer4.Height = slickSpacer1.Height = UI.Scale(1);
		slickSpacer2.Height = UI.Scale(2);
		L_Desc.Margin = L_Note.Margin = UI.Scale(new Padding(3));
		B_DeleteRequest.Margin = B_ApplyChanges.Margin = B_ManagePackage.Margin = B_Reply.Margin = UI.Scale(new Padding(5));
		TB_Note.Height = UI.Scale(200);
		tableLayoutPanel4.Margin = TLP_Actions.Margin = UI.Scale(new Padding(10));

		foreach (Control item in tableLayoutPanel1.Controls)
		{
			item.Margin = UI.Scale(new Padding(7));
		}
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);
	}

	private void LogControl_Paint(object sender, PaintEventArgs e)
	{
		var ctrl = (sender as SlickControl)!;

		e.Graphics.SetUp(ctrl.BackColor);

		using var fileIcon = IconManager.GetLargeIcon("File").Color(FormDesign.Design.MenuForeColor);

		var Padding = ctrl.Margin;
		var textSize = e.Graphics.Measure(ctrl.Text, new Font(Font, FontStyle.Bold), ctrl.Width - Padding.Left);
		var fileHeight = (int)textSize.Height + 3 + fileIcon.Height + Padding.Top;
		var fileRect = ctrl.ClientRectangle;
		var fileContentRect = fileRect.CenterR(Math.Max((int)textSize.Width + 3, fileIcon.Width), fileHeight);

		e.Graphics.FillRoundedRectangle(new SolidBrush(ctrl.HoverState.HasFlag(HoverState.Hovered) ? FormDesign.Design.MenuColor.MergeColor(FormDesign.Design.ActiveColor) : FormDesign.Design.MenuColor), fileRect, Padding.Left);

		if (ctrl.Loading)
		{
			ctrl.DrawLoader(e.Graphics, fileContentRect.Align(fileIcon.Size, ContentAlignment.TopCenter));
		}
		else
		{
			e.Graphics.DrawImage(fileIcon, fileContentRect.Align(fileIcon.Size, ContentAlignment.TopCenter));
		}

		e.Graphics.DrawString(ctrl.Text, new Font(Font, FontStyle.Bold), new SolidBrush(FormDesign.Design.MenuForeColor), fileContentRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far });
	}

	private void SaveGameControl_Paint(object sender, PaintEventArgs e)
	{
		var ctrl = (sender as SlickControl)!;

		e.Graphics.SetUp(ctrl.BackColor);

		using var fileIcon = IconManager.GetLargeIcon("SaveGame").Color(FormDesign.Design.MenuForeColor);

		var Padding = ctrl.Margin;
		var textSize = e.Graphics.Measure(ctrl.Text, new Font(Font, FontStyle.Bold), ctrl.Width - Padding.Left);
		var fileHeight = (int)textSize.Height + 3 + fileIcon.Height + Padding.Top;
		var fileRect = ctrl.ClientRectangle;
		var fileContentRect = fileRect.CenterR(Math.Max((int)textSize.Width + 3, fileIcon.Width), fileHeight);

		e.Graphics.FillRoundedRectangle(new SolidBrush(ctrl.HoverState.HasFlag(HoverState.Hovered) ? FormDesign.Design.MenuColor.MergeColor(FormDesign.Design.ActiveColor) : FormDesign.Design.MenuColor), fileRect, Padding.Left);

		if (ctrl.Loading)
		{
			ctrl.DrawLoader(e.Graphics, fileContentRect.Align(fileIcon.Size, ContentAlignment.TopCenter));
		}
		else
		{
			e.Graphics.DrawImage(fileIcon, fileContentRect.Align(fileIcon.Size, ContentAlignment.TopCenter));
		}

		e.Graphics.DrawString(ctrl.Text, new Font(Font, FontStyle.Bold), new SolidBrush(FormDesign.Design.MenuForeColor), fileContentRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far });
	}

	private async void B_ManagePackage_Click(object sender, EventArgs e)
	{
		Form.PushPanel(new PC_CompatibilityManagement([_request]));
		await _skyveApiUtil.ProcessReviewRequest(_request);
	}

	private async void B_ApplyChanges_Click(object sender, EventArgs e)
	{
		Form.PushPanel(new PC_CompatibilityManagement(_request));
		await _skyveApiUtil.ProcessReviewRequest(_request);
	}

	private async void B_DeleteRequest_Click(object sender, EventArgs e)
	{
		B_DeleteRequest.Loading = true;

		var response = await _skyveApiUtil.ProcessReviewRequest(_request);

		if (response.Success)
		{
			PushBack();

			if (Form.CurrentPanel is PC_ReviewRequests reviewRequests)
			{
				reviewRequests.Remove(_request);
			}
		}
		else
		{
			ShowPrompt("Failed to process the request: " + response.Message, PromptButtons.OK, PromptIcons.Info);
		}

		B_DeleteRequest.Loading = false;
	}

	private void I_Copy_Click(object sender, EventArgs e)
	{
		Clipboard.SetText(L_Note.Text);
	}

	private void SaveGameControl_Click(object sender, EventArgs e)
	{
		PlatformUtil.OpenUrl(_request.SaveUrl);
	}

	private void B_Reply_Click(object sender, EventArgs e)
	{
		P_Reply.Visible = !P_Reply.Visible;
		P_Info.Visible = !P_Info.Visible;
	}

	private async void B_LetUserKnowIsFixed_Click(object sender, EventArgs e)
	{
		await SendMessage(B_LetUserKnowIsFixed, new ReviewReply
		{
			Message = nameof(Locale.ReviewIsFixed),
			PackageId = Package.Id,
			RequestUpdate = false,
			Username = _request.UserId
		});
	}

	private async void B_LetUserKnowSaveFileNeeded_Click(object sender, EventArgs e)
	{
		await SendMessage(B_LetUserKnowSaveFileNeeded, new ReviewReply
		{
			Message = nameof(Locale.ReviewIsSaveFileNeeded),
			PackageId = Package.Id,
			RequestUpdate = true,
			Username = _request.UserId
		});
	}

	private async void B_SendReply_Click(object sender, EventArgs e)
	{
		await SendMessage(B_SendReply, new ReviewReply
		{
			Link = TB_Link.Text,
			Message = TB_Note.Text,
			PackageId = Package.Id,
			RequestUpdate = true,
			Username = _request.UserId
		});

		TB_Link.Text = string.Empty;
		TB_Note.Text = string.Empty;
	}

	private async Task SendMessage(SlickControl button, ReviewReply reply)
	{
		button.Loading = true;

		var result = await _skyveApiUtil.SendReviewMessage(reply);

		button.Loading = false;

		if (!result.Success)
		{
			ShowPrompt("Failed to send the reply.\r\n\r\n" + result.Message, icon: PromptIcons.Error);
		}
		else
		{
			P_Reply.Visible = false;
			P_Info.Visible = true;
		}
	}
}
