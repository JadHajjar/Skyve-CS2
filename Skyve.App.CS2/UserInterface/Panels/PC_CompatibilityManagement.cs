using Skyve.App.Interfaces;
using Skyve.App.UserInterface.CompatibilityReport;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Forms;
using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Systems.CS2.Managers;
using Skyve.Systems.CS2.Utilities;

using SkyveApi.Domain.CS2;
using SkyveApi.Domain.Generic;

using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_CompatibilityManagement : PC_PackagePageBase
{
	private int currentPage;
	private PostPackage? postPackage;
	private PostPackage? lastPackageData;
	private bool valuesChanged;
	private readonly ReviewRequest? _request;
	private IPackageIdentity[] packages;

	private readonly ICompatibilityManager _compatibilityManager;
	private readonly ISkyveDataManager _skyveDataManager;
	private readonly IWorkshopService _workshopService;
	private readonly IUserService _userService;
	private readonly ITagsService _tagsService;

	public PC_CompatibilityManagement(IEnumerable<IPackageIdentity> packages) : this(packages.FirstOrDefault())
	{
		this.packages = packages.ToArray();

		if (this.packages.Length == 1)
		{
			Padding = new Padding(5, 0, 0, 0);
			base_P_Side.Visible = false;
		}
		else
		{
			packageCrList.SetItems(this.packages);
		}
	}

	public PC_CompatibilityManagement() : this(new GenericPackageIdentity())
	{
		packages = [];
	}

	public PC_CompatibilityManagement(IPackageIdentity package) : base(package)
	{
		ServiceCenter.Get(out _workshopService, out _compatibilityManager, out _userService, out _tagsService, out _skyveDataManager);

		InitializeComponent();

		packages = [];

		SetPackage(Package);

		SlickTip.SetTo(B_Skip, "Skip");
		SlickTip.SetTo(B_Previous, "Previous");
		SlickTip.SetTo(P_Tags, "GlobalTagsInfo");
		SlickTip.SetTo(B_ReuseData, "ReuseData_Tip");
		TB_Search.Placeholder = $"{LocaleSlickUI.Search}..";
		T_Statuses.Text = LocaleCR.StatusesCount.Format(0);
		T_Interactions.Text = LocaleCR.InteractionCount.Format(0);

		packageCrList.CanDrawItem += PackageCrList_CanDrawItem;

		DD_Stability.Enabled = _userService.User.Manager;
		TB_Note.Enabled = _userService.User.Manager;

		SetPackage(Package);
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		PB_Icon.Cursor = L_Title.Cursor = Cursors.Hand;
		PB_Icon.MouseClick += PB_Icon_MouseClick;
		L_Title.MouseClick += PB_Icon_MouseClick;

		if (Form != null)
		{
			if (base_P_Side.Visible)
			{
				Form.base_TLP_Side.TopRight = true;
				Form.base_TLP_Side.BotRight = true;
				Form.base_TLP_Side.Invalidate();
			}

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

		slickSpacer3.Margin = B_Previous.Margin = B_Skip.Margin = B_Previous.Padding = B_Skip.Padding = TLP_Bottom.Padding = B_ReuseData.Margin = B_Apply.Margin = slickSpacer2.Margin = UI.Scale(new Padding(5), UI.FontScale);
		slickSpacer2.Height = (int)(2 * UI.FontScale);
		slickSpacer3.Height = (int)(UI.FontScale);
		B_AddInteraction.Size = B_AddStatus.Size = UI.Scale(new Size(105, 70), UI.FontScale);
		B_AddInteraction.Margin = B_AddStatus.Margin = UI.Scale(new Padding(15), UI.FontScale);
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
		var canExit = !toBeDisposed
			|| currentPage <= 0
			|| currentPage >= packages.Length - 1
			|| ShowPrompt(LocaleCR.ConfirmEndSession, PromptButtons.YesNo, PromptIcons.Question) == DialogResult.Yes;

		if (toBeDisposed && canExit)
		{
			RefreshData();
		}

		return canExit;
	}

	private async void RefreshData()
	{
		await Task.Run(() =>
		{
			_skyveDataManager.DownloadData();
			_compatibilityManager.CacheReport();
		});
	}

	protected override async Task<bool> LoadDataAsync()
	{
		PB_Loading.Loading = true;

		var mods = _userService.User.Manager ?
			await _workshopService.QueryFilesAsync(WorkshopQuerySorting.DateUpdated, requiredTags: ["Code Mod"], all: true) :
			await _workshopService.GetWorkshopItemsByUserAsync(_userService.User.Id ?? 0);

		packages = mods.ToArray();

		packageCrList.SetItems(packages);

		return true;
	}

	private async void SetPackage(int page)
	{
		if (valuesChanged)
		{
			switch (ShowPrompt(LocaleCR.ApplyChangedBeforeExit, PromptButtons.YesNoCancel, PromptIcons.Hand))
			{
				case DialogResult.Yes:
					if (!await Apply())
					{
						return;
					}

					break;
				case DialogResult.Cancel:
					return;
			}
		}


		if (packages.Length == 0)
		{
			L_Page.Text = $"0 / 0";
			return;
		}

		if (page < 0 || page >= packages.Length)
		{
			PushBack();
			return;
		}

		L_Page.Text = $"{page + 1} / {packages.Length}";

		PB_Loading.BringToFront();
		PB_Loading.Loading = true;

		SetPackage(packages[currentPage = page]);

		try
		{
			if (!_userService.User.Manager && !_userService.User.Equals(Package.GetWorkshopInfo()?.Author))
			{
				packageCrList.Remove(Package);
				SetPackage(page);
				return;
			}

			var skyveApiUtil = ServiceCenter.Get<ISkyveApiUtil, SkyveApiUtil>();
			var skyveDataManager = ServiceCenter.Get<ISkyveDataManager, SkyveDataManager>();
			var catalogue = await skyveApiUtil.Catalogue(Package!.Id);

			postPackage = catalogue?.Packages.FirstOrDefault()?.CloneTo<CompatibilityPackageData, PostPackage>();

			if (postPackage is null)
			{
				postPackage = (skyveDataManager).GetAutomatedReport(Package).CloneTo<CompatibilityPackageData, PostPackage>();
			}
			else
			{
				var automatedPackage = (skyveDataManager).GetAutomatedReport(Package).CloneTo<CompatibilityPackageData, PostPackage>();

				if (automatedPackage.Stability is PackageStability.Broken)
				{
					postPackage.Stability = PackageStability.Broken;
				}

				foreach (var item in automatedPackage.Statuses ?? new())
				{
					if (!postPackage.Statuses.Any(x => x.Type == item.Type))
					{
						postPackage.Statuses!.Add(item);
					}
				}
			}

			postPackage.BlackListId = catalogue?.BlackListedIds?.Contains(postPackage.Id) ?? false;
			postPackage.BlackListName = catalogue?.BlackListedNames?.Contains(postPackage.Name ?? string.Empty) ?? false;

			SetData(postPackage);

			B_Previous.Enabled = currentPage > 0;
			B_Skip.Enabled = currentPage != packages.Length - 1;

			PB_Loading.SendToBack();
			PB_Loading.Loading = false;

			packageCrList.Invalidate();
			valuesChanged = false;
		}
		catch { OnLoadFail(); }
	}

	private void SetData(PostPackage postPackage)
	{
		if (!IsHandleCreated)
		{
			CreateHandle();
		}

		CB_BlackListName.Checked = postPackage.BlackListName;
		CB_BlackListId.Checked = postPackage.BlackListId;

		if (_request is not null && !_request.IsStatus && !_request.IsInteraction)
		{
			DD_Stability.SelectedItem = (PackageStability)_request.PackageStability;
			DD_PackageType.SelectedItem = (PackageType)_request.PackageType;
			DD_DLCs.SelectedItems = ServiceCenter.Get<IDlcManager>().Dlcs.Where(x => _request.RequiredDLCs?.Contains(x.Id.ToString()) ?? false);
			DD_Usage.SelectedItems = Enum.GetValues(typeof(PackageUsage)).Cast<PackageUsage>().Where(x => ((PackageUsage)_request.PackageUsage).HasFlag(x));
		}
		else
		{
			DD_Stability.SelectedItem = postPackage.Stability;
			DD_PackageType.SelectedItem = postPackage.Type;
			DD_DLCs.SelectedItems = ServiceCenter.Get<IDlcManager>().Dlcs.Where(x => postPackage.RequiredDLCs?.Contains(x.Id) ?? false);
			DD_Usage.SelectedItems = Enum.GetValues(typeof(PackageUsage)).Cast<PackageUsage>().Where(x => postPackage.Usage.HasFlag(x));
		}

		TB_Note.Text = postPackage.Note;

		P_Tags.Controls.Clear(true, x => !string.IsNullOrEmpty(x.Text));
		FLP_Statuses.Controls.Clear(true, x => x is IPackageStatusControl<StatusType, PackageStatus>);
		FLP_Interactions.Controls.Clear(true, x => x is IPackageStatusControl<InteractionType, PackageInteraction>);

		foreach (var item in postPackage.Tags ?? new())
		{
			var control = new TagControl { TagInfo = _tagsService.CreateCustomTag(item) };
			control.Click += TagControl_Click;
			P_Tags.Controls.Add(control);
			T_NewTag.SendToBack();
		}

		SetLinks(postPackage.Links ?? new());

		postPackage.Statuses ??= new();
		postPackage.Interactions ??= new();

		if (_request?.IsInteraction ?? false)
		{
			postPackage.Interactions.Add(new()
			{
				Action = (StatusAction)_request.StatusAction,
				IntType = _request.StatusType,
				Note = _request.StatusNote,
				Packages = _request.StatusPackages?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToArray(),
			});
		}

		if (_request?.IsStatus ?? false)
		{
			postPackage.Statuses.Add(new()
			{
				Action = (StatusAction)_request.StatusAction,
				IntType = _request.StatusType,
				Note = _request.StatusNote,
				Packages = _request.StatusPackages?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToArray(),
			});
		}

		foreach (var item in postPackage.Statuses)
		{
			var control = new IPackageStatusControl<StatusType, PackageStatus>(Package, item, !_userService.User.Manager)
			{
				Enabled = _userService.User.Manager || CRNAttribute.GetAttribute(item.Type).AllowedChange == CRNAttribute.ChangeType.Allow
			};

			control.ValuesChanged += ControlValueChanged;

			FLP_Statuses.Controls.Add(control);
			B_AddStatus.SendToBack();
		}

		foreach (var item in postPackage.Interactions)
		{
			var control = new IPackageStatusControl<InteractionType, PackageInteraction>(Package, item, !_userService.User.Manager)
			{
				Enabled = _userService.User.Manager || CRNAttribute.GetAttribute(item.Type).AllowedChange == CRNAttribute.ChangeType.Allow
			};

			control.ValuesChanged += ControlValueChanged;

			FLP_Interactions.Controls.Add(control);
			B_AddInteraction.SendToBack();
		}
	}

	private void B_Skip_Click(object sender, EventArgs e)
	{
		SetPackage(ModifierKeys.HasFlag(Keys.Shift) ? (packages.Length - 1) : (currentPage + 1));
	}

	private void B_Previous_Click(object sender, EventArgs e)
	{
		SetPackage(ModifierKeys.HasFlag(Keys.Shift) ? 0 : (currentPage - 1));
	}

	private void T_NewTag_Click(object sender, EventArgs e)
	{
		var prompt = ShowInputPrompt(LocaleCR.AddGlobalTag);

		if (prompt.DialogResult != DialogResult.OK)
		{
			return;
		}

		if (string.IsNullOrWhiteSpace(prompt.Input) || P_Tags.Controls.Any(x => x.Text.Equals(prompt.Input, StringComparison.CurrentCultureIgnoreCase)))
		{
			return;
		}

		var control = new TagControl { TagInfo = _tagsService.CreateCustomTag(prompt.Input) };
		control.Click += TagControl_Click;
		P_Tags.Controls.Add(control);
		T_NewTag.SendToBack();
		ControlValueChanged(sender, e);
	}

	private void TagControl_Click(object sender, EventArgs e)
	{
		(sender as Control)?.Dispose();

		ControlValueChanged(sender, e);
	}

	private void T_NewLink_Click(object sender, EventArgs e)
	{
		var form = new AddLinkForm(P_Links.Controls.OfType<LinkControl>().ToList(x => x.Link));

		form.Show(Form);

		form.LinksReturned += SetLinks;
	}

	private void SetLinks(IEnumerable<PackageLink> links)
	{
		P_Links.Controls.Clear(true, x => x is LinkControl);

		foreach (var item in links.OrderBy(x => x.Type))
		{
			var control = new LinkControl(item, false);
			control.Click += T_NewLink_Click;
			P_Links.Controls.Add(control);
		}

		T_NewLink.SendToBack();

		ControlValueChanged(this, EventArgs.Empty);
	}

	private void B_AddStatus_Click(object sender, EventArgs e)
	{
		var control = new IPackageStatusControl<StatusType, PackageStatus>(Package, default, !_userService.User.Manager);

		control.ValuesChanged += ControlValueChanged;

		FLP_Statuses.Controls.Add(control);
		B_AddStatus.SendToBack();

		ControlValueChanged(sender, e);
	}

	private void B_AddInteraction_Click(object sender, EventArgs e)
	{
		var control = new IPackageStatusControl<InteractionType, PackageInteraction>(Package, default, !_userService.User.Manager);

		control.ValuesChanged += ControlValueChanged;

		FLP_Interactions.Controls.Add(control);
		B_AddInteraction.SendToBack();

		ControlValueChanged(sender, e);
	}

	private async void B_Apply_Click(object sender, EventArgs e)
	{
		if (await Apply())
		{
			SetPackage(currentPage + 1);
		}
	}

	private async Task<bool> Apply()
	{
		if (B_Apply.Loading)
		{
			return false;
		}

		if (DD_Usage.SelectedItems.Count() == 0)
		{
			ShowPrompt(LocaleCR.PleaseReviewPackageUsage, PromptButtons.OK, PromptIcons.Hand);
			return false;
		}

		postPackage!.Id = Package.Id;
		postPackage.FileName = Path.GetFileName(Package.GetLocalPackageIdentity()?.FilePath ?? string.Empty).IfEmpty(postPackage.FileName);
		postPackage.Name = Package.Name;
		postPackage.ReviewDate = DateTime.UtcNow;
		postPackage.AuthorId = Package.GetWorkshopInfo()?.Author?.Id?.ToString();
		postPackage.Author = new Author
		{
			Id = postPackage.AuthorId,
			Name = Package.GetWorkshopInfo()?.Author?.Name,
		};

		postPackage.BlackListId = CB_BlackListId.Checked;
		postPackage.BlackListName = CB_BlackListName.Checked;
		postPackage.Stability = DD_Stability.SelectedItem;
		postPackage.Type = DD_PackageType.SelectedItem;
		postPackage.Usage = DD_Usage.SelectedItems.Aggregate((prev, next) => prev | next);
		postPackage.RequiredDLCs = DD_DLCs.SelectedItems.Select(x => x.Id).ToArray();
		postPackage.Note = TB_Note.Text;
		postPackage.Tags = P_Tags.Controls.OfType<TagControl>().Where(x => !string.IsNullOrEmpty(x.TagInfo?.Value)).ToList(x => x.TagInfo!.Value);
		postPackage.Links = P_Links.Controls.OfType<LinkControl>().ToList(x => (PackageLink)x.Link);
		postPackage.Statuses = FLP_Statuses.Controls.OfType<IPackageStatusControl<StatusType, PackageStatus>>().ToList(x => x.PackageStatus);
		postPackage.Interactions = FLP_Interactions.Controls.OfType<IPackageStatusControl<InteractionType, PackageInteraction>>().ToList(x => x.PackageStatus);

		if (!CRNAttribute.GetAttribute(postPackage.Stability).Browsable)
		{
			ShowPrompt(LocaleCR.PleaseReviewTheStability, PromptButtons.OK, PromptIcons.Hand);
			return false;
		}

		if (postPackage.Statuses.Any(x => !CRNAttribute.GetAttribute(x.Type).Browsable))
		{
			ShowPrompt(LocaleCR.PleaseReviewPackageStatuses, PromptButtons.OK, PromptIcons.Hand);
			return false;
		}

		if (postPackage.Interactions.Any(x => !CRNAttribute.GetAttribute(x.Type).Browsable || !(x.Packages?.Any() ?? false)))
		{
			ShowPrompt(LocaleCR.PleaseReviewPackageInteractions, PromptButtons.OK, PromptIcons.Hand);
			return false;
		}

		B_Apply.Loading = true;

		var response = await ServiceCenter.Get<ISkyveApiUtil, SkyveApiUtil>().SaveEntry(postPackage);

		B_Apply.Loading = false;

		if (!response.Success)
		{
			ShowPrompt(response.Message, PromptButtons.OK, PromptIcons.Error);
			return false;
		}

		lastPackageData = postPackage;
		B_ReuseData.Visible = true;

		valuesChanged = false;

		return true;
	}

	private void FLP_Statuses_ControlAdded(object sender, ControlEventArgs e)
	{
		T_Statuses.Text = LocaleCR.StatusesCount.Format(FLP_Statuses.Controls.Count - 1);
		T_Interactions.Text = LocaleCR.InteractionCount.Format(FLP_Interactions.Controls.Count - 1);
	}

	private void B_ReuseData_Click(object sender, EventArgs e)
	{
		if (lastPackageData is not null)
		{
			SetData(lastPackageData);
		}

		if (ModifierKeys.HasFlag(Keys.Shift))
		{
			B_Apply_Click(sender, e);
		}
	}

	private void packageCrList_ItemMouseClick(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			SetPackage(Array.IndexOf(packages, (IPackageIdentity)sender));
		}

		if (e.Button == MouseButtons.Right)
		{
			SlickToolStrip.Show(Form, packageCrList.PointToClient(e.Location), ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems(_workshopService.GetPackage(new GenericPackageIdentity((ulong)sender))!));
		}
	}

	private void TB_Search_TextChanged(object sender, EventArgs e)
	{
		TB_Search.ImageName = string.IsNullOrWhiteSpace(TB_Search.Text) ? "I_Search" : "I_ClearSearch";

		packageCrList.FilterChanged();

		//if (sender == CB_ShowUpToDate)
		//{
		//	SetPackage(0);
		//}
	}

	private void PackageCrList_CanDrawItem(object sender, CanDrawItemEventArgs<IPackageIdentity> e)
	{
		var package = _workshopService.GetPackage(e.Item);

		if (package is null)
		{
			return;
		}

		//if (!CB_ShowUpToDate.Checked)
		//{
		//	var cr = package.GetPackageInfo();

		//	if (cr is null || cr.ReviewDate > package.GetWorkshopInfo()?.ServerTime)
		//	{
		//		e.DoNotDraw = true;
		//		return;
		//	}
		//}

		e.DoNotDraw = !(TB_Search.Text.SearchCheck(package.ToString())
			|| TB_Search.Text.SearchCheck(package.GetWorkshopInfo()?.Author?.Name)
			|| package.Id.ToString().IndexOf(TB_Search.Text, StringComparison.OrdinalIgnoreCase) != -1);
	}

	private void TB_Search_IconClicked(object sender, EventArgs e)
	{
		TB_Search.Text = string.Empty;
	}

	private void ControlValueChanged(object sender, EventArgs e)
	{
		valuesChanged = true;
	}
}
