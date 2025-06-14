﻿using Newtonsoft.Json.Linq;

using Skyve.App.UserInterface.Content;
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

	private readonly IWorkshopService _workshopService;
	private readonly IDlcManager _dlcManager;
	private readonly IUserService _userService;
	private readonly SkyveApiUtil _skyveApiUtil;

	public PC_ViewReviewRequest(ReviewRequest request) : base(request)
	{
		ServiceCenter.Get(out _dlcManager, out _skyveApiUtil, out _workshopService, out _userService);
		InitializeComponent();
		_request = request;

		P_Info.Controls.Add(new UserDescriptionControl(_userService.TryGetUser(request.UserId)) { Dock = DockStyle.Left });

		TLP_Savegame.Visible = !string.IsNullOrEmpty(request.SaveUrl);

		saveGameControl.Text = "Download SaveGame";
		logControl.Text = "Download Log Report";
		L_Note.Text = request.PackageNote.IfEmpty("No description given");

		if (!(TLP_Changes.Visible = request.IsMissingInfo))
		{
			TLP_BottomButtons.Controls.Remove(B_ApplyChanges);
			TLP_BottomButtons.ColumnStyles[2] = new ColumnStyle(SizeType.Absolute, 0);
		}

		SlickTip.SetTo(B_Copy, "Copy to clipboard");
		SlickTip.SetTo(B_Translate, "Translate to English");

		SetPackage(Package);
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (TLP_Changes.Visible)
		{
			DD_Stability.SelectedItem = _request.PackageStability.TryCast<PackageStability>();
			DD_PackageType.SelectedItem = _request.PackageType.TryCast<PackageType>();
			DD_DLCs.SelectedItems = ServiceCenter.Get<IDlcManager>().Dlcs.Where(x => _request.RequiredDLCs?.Contains(x.Id.ToString()) ?? false);
			DD_Usage.SelectedItems = Enum.GetValues(typeof(PackageUsage)).Cast<PackageUsage>().Where(x => _request.PackageUsage.TryCast<PackageUsage>().HasFlag(x));
			DD_SavegameEffect.SelectedItem = _request.SavegameEffect.TryCast<SavegameEffect>();
		}
	}

	private CompatibilityPackageReference StringToPackageReference(string arg)
	{
		if (ulong.TryParse(arg, out var id))
		{
			return new CompatibilityPackageReference(new GenericPackageIdentity(id));
		}

		return new(new GenericPackageIdentity(0));
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

		var fileName = CrossIO.Combine(ServiceCenter.Get<ILocationService>().SkyveDataPath, ".SupportLogs", $"RequestBy_{_userService.TryGetUser(_request.UserId)?.Name}_{DateTime.Now:yy-MM-dd_HH-mm}.zip");

		Directory.CreateDirectory(Path.GetDirectoryName(fileName));

		File.WriteAllBytes(fileName, _request.LogFile);

		PlatformUtil.OpenFolder(fileName);

		logControl.Loading = false;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		TB_Note.Margin = TB_Link.Margin = B_SendReply.Margin = UI.Scale(new Padding(10));
		P_StackedInfo.Padding = UI.Scale(new Padding(10, 5, 10, 0));
		B_Copy.Size = UI.Scale(new Size(32, 32));
		B_Copy.Padding = UI.Scale(new Padding(4));
		slickSpacer2.Height = UI.Scale(2);
		B_DeleteRequest.Margin = B_ApplyChanges.Margin = B_ManagePackage.Margin = B_Reply.Margin = UI.Scale(new Padding(5));
		TB_Note.Height = UI.Scale(200);
		TLP_Message.Margin = TLP_Actions.Margin = UI.Scale(new Padding(10));

		foreach (Control item in TLP_BottomButtons.Controls)
		{
			item.Margin = UI.Scale(new Padding(7));
		}
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);
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
		if (!P_Reply.Visible)
		{
			TLP_Message.Visible = false;
			TLP_Actions.Visible = true;
		}

		P_Reply.Visible = !P_Reply.Visible;
		P_Info.Visible = !P_Info.Visible;
	}

	private void B_SendMessageOption_Click(object sender, EventArgs e)
	{
		TLP_Message.Visible = true;
		TLP_Actions.Visible = false;
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

	private async void B_Translate_Click(object sender, EventArgs e)
	{
		B_Translate.Enabled = false;
		B_Translate.Loading = true;

		L_Note.Text = await TranslateToEnglishAsync(L_Note.Text);
		L_Note.Invalidate();

		B_Translate.Loading = false;
	}

	public async Task<string?> TranslateToEnglishAsync(string inputText)
	{
		try
		{
			var result = await ServiceCenter.Get<ApiUtil>().Get<dynamic>("https://translate.googleapis.com/translate_a/single", ("client", "gtx"), ("sl", "auto"), ("tl", "en"), ("dt", "t"), ("q", inputText.Replace("\r\n", " _ ")));

			return ((JValue)((JContainer)((JContainer)(result as JArray)!.First).First).First).Value.ToString().RegexReplace(" ?_ ?", "\r\n");
		}
		catch
		{
			return inputText;
		}
	}
}
