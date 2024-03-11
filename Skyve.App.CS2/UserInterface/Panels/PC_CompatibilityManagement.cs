using Skyve.App.Interfaces;
using Skyve.App.UserInterface.CompatibilityReport;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Forms;
using Skyve.Compatibility.Domain;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Systems.CS2.Domain;
using Skyve.Systems.CS2.Domain.Api;
using Skyve.Systems.CS2.Managers;
using Skyve.Systems.CS2.Utilities;

using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_CompatibilityManagement : PC_PackagePageBase
{
	private bool singlePackage;
	private int currentPage;
	private CompatibilityPostPackage? postPackage;
	private CompatibilityPostPackage? lastPackageData;
	private bool valuesChanged;
	private readonly ReviewRequest? _request;

	private readonly ICompatibilityManager _compatibilityManager;
	private readonly SkyveDataManager _skyveDataManager;
	private readonly IWorkshopService _workshopService;
	private readonly IUserService _userService;
	private readonly ITagsService _tagsService;

	public PC_CompatibilityManagement(IEnumerable<IPackageIdentity> packages) : this(false)
	{
		packageCrList.SetItems(packages.Distinct(x => x.Id));

		if (singlePackage = packageCrList.ItemCount == 1)
		{
			Padding = new Padding(5, 0, 0, 0);
			base_P_Side.Visible = false;
		}

		SetPackage(packageCrList.SortedItems.FirstOrDefault());
	}

	public PC_CompatibilityManagement() : this(true)
	{
	}

	public PC_CompatibilityManagement(bool load) : base(new GenericPackageIdentity(), load)
	{
		ServiceCenter.Get(out _workshopService, out _compatibilityManager, out _userService, out _tagsService, out ISkyveDataManager skyveDataManager);

		_skyveDataManager = (SkyveDataManager)skyveDataManager;

		InitializeComponent();

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
		CB_BlackListId.Visible= _userService.User.Manager;
		CB_BlackListName.Visible = _userService.User.Manager;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		PB_Loading.Location = ClientRectangle.Center(PB_Loading.Size);
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

		if (!DataLoaded)
		{
			foreach (Control item in Controls)
			{
				item.Visible = item == PB_Loading;
			}
		}
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
		CustomTitleBounds = new Point(singlePackage ? 0 : (int)(175 * UI.FontScale), 0);

		base.UIChanged();

		slickSpacer3.Margin = B_Previous.Margin = B_Skip.Margin = B_Previous.Padding = B_Skip.Padding = TLP_Bottom.Padding = B_ReuseData.Margin = B_Apply.Margin = slickSpacer2.Margin = UI.Scale(new Padding(5), UI.FontScale);
		slickSpacer2.Height = (int)(2 * UI.FontScale);
		slickSpacer3.Height = (int)UI.FontScale;
		B_AddInteraction.Size = B_AddStatus.Size = UI.Scale(new Size(105, 70), UI.FontScale);
		B_AddInteraction.Margin = B_AddStatus.Margin = UI.Scale(new Padding(15), UI.FontScale);
		B_Previous.Size = B_Skip.Size = UI.Scale(new Size(32, 32), UI.FontScale);
		L_Page.Font = UI.Font(7.5F, FontStyle.Bold);
		TB_Note.MinimumSize = new Size(0, (int)(200 * UI.FontScale));
		PB_Loading.Size = UI.Scale(new Size(32, 32), UI.FontScale);
		CB_BlackListId.Font = CB_BlackListName.Font = UI.Font(7.5F);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		base_TLP_Side.BackColor = design.MenuColor;
		base_TLP_Side.ForeColor = design.MenuForeColor;
		L_Page.ForeColor = design.LabelColor;
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);

		if (Live)
		{
			PB_Loading.Location = ClientRectangle.Center(PB_Loading.Size);
		}
	}

	public override bool CanExit(bool toBeDisposed)
	{
		var canExit = !toBeDisposed
			|| currentPage <= 0
			|| currentPage >= packageCrList.ItemCount - 1
			|| ShowPrompt(LocaleCR.ConfirmEndSession, PromptButtons.YesNo, PromptIcons.Question) == DialogResult.Yes;

		if (toBeDisposed && canExit)
		{
			Form.base_TLP_Side.TopRight = false;
			Form.base_TLP_Side.BotRight = false;
			Form.base_TLP_Side.Invalidate();

			RefreshData();
		}

		return canExit;
	}

	private async void RefreshData()
	{
		await Task.Run(async () =>
		{
			await _skyveDataManager.DownloadData();
			_compatibilityManager.CacheReport();
		});
	}

	protected override async Task<bool> LoadDataAsync()
	{
		PB_Loading.Loading = true;

		var mods = _userService.User.Manager ?
			await _workshopService.QueryFilesAsync(WorkshopQuerySorting.DateUpdated, requiredTags: ["Code Mod"], all: true) :
			await _workshopService.GetWorkshopItemsByUserAsync(_userService.User.Id ?? 0);

		packageCrList.SetItems(mods);

		return true;
	}

	protected override void OnDataLoad()
	{
		foreach (Control item in Controls)
		{
			item.Visible = true;
		}

		SetPackage(packageCrList.SortedItems.FirstOrDefault());
	}

	protected override async void SetPackage(IPackageIdentity package)
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

		if (package is null)
		{
			PushBack();

			return;
		}

		if (package.Id <= 0)
		{
			return;
		}

		base.SetPackage(package);

		if (packageCrList.ItemCount > 0)
		{
			currentPage = packageCrList.SortedItems.IndexOf(package);

			if (currentPage < 0 || currentPage >= packageCrList.ItemCount)
			{
				PushBack();
				return;
			}

			L_Page.Text = $"{currentPage + 1} / {packageCrList.ItemCount}";
		}
		else
		{
			L_Page.Text = $"0 / 0";
		}

		packageCrList.CurrentPackage = package;
		packageCrList.Invalidate();

		PB_Loading.BringToFront();
		PB_Loading.Loading = true;
		PB_Loading.Visible = true;
		slickTabControl.Visible = false;
		TLP_Bottom.Visible = false;

		try
		{
			if (!_userService.User.Manager && !_userService.User.Equals(Package.GetWorkshopInfo()?.Author))
			{
				packageCrList.Remove(Package);
				return;
			}

			var skyveApiUtil = ServiceCenter.Get<SkyveApiUtil>();
			var skyveDataManager = ServiceCenter.Get<ISkyveDataManager, SkyveDataManager>();
			var catalogue = await skyveApiUtil.GetPackageData(Package.Id);

			postPackage = catalogue?.CloneTo<PackageData, CompatibilityPostPackage>();

			if (postPackage is null)
			{
				postPackage = skyveDataManager.GetAutomatedReport(Package).CloneTo<PackageData, CompatibilityPostPackage>();
			}
			else
			{
				var automatedPackage = skyveDataManager.GetAutomatedReport(Package).CloneTo<PackageData, CompatibilityPostPackage>();

				if (automatedPackage.Stability is PackageStability.Broken)
				{
					postPackage.Stability = PackageStability.Broken;
				}

				foreach (var item in automatedPackage.Statuses ?? [])
				{
					if (!postPackage.Statuses.Any(x => x.Type == item.Type))
					{
						postPackage.Statuses!.Add(item);
					}
				}
			}

			postPackage.IsBlackListedById = skyveDataManager.CompatibilityData.BlackListedIds?.Contains(postPackage.Id) ?? false;
			postPackage.IsBlackListedByName = skyveDataManager.CompatibilityData.BlackListedNames?.Contains(postPackage.Name ?? string.Empty) ?? false;

			SetData(postPackage);

			B_Previous.Enabled = currentPage > 0;
			B_Skip.Enabled = currentPage != packageCrList.ItemCount - 1;

			PB_Loading.Loading = false;
			PB_Loading.Visible = false;
			slickTabControl.Visible = true;
			TLP_Bottom.Visible = true;

			T_Info.Selected = true;

			packageCrList.Invalidate();
			valuesChanged = false;
		}
		catch
		{
			OnLoadFail();
		}
	}

	private void SetData(CompatibilityPostPackage postPackage)
	{
		if (!IsHandleCreated)
		{
			CreateHandle();
		}

		CB_BlackListId.Checked = postPackage.IsBlackListedById;
		CB_BlackListName.Checked = postPackage.IsBlackListedByName;

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

		foreach (var item in postPackage.Tags ?? [])
		{
			var control = new TagControl { TagInfo = _tagsService.CreateCustomTag(item) };
			control.Click += TagControl_Click;
			P_Tags.Controls.Add(control);
			T_NewTag.SendToBack();
		}

		SetLinks(postPackage.Links ?? []);

		postPackage.Statuses ??= [];
		postPackage.Interactions ??= [];

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
		if (B_Skip.Enabled)
		{
			SetPackage(ModifierKeys.HasFlag(Keys.Control) ? packageCrList.SortedItems.LastOrDefault() : packageCrList.SortedItems.Next(Package, true));
		}
	}

	private void B_Previous_Click(object sender, EventArgs e)
	{
		if (B_Previous.Enabled)
		{
			SetPackage(ModifierKeys.HasFlag(Keys.Control) ? packageCrList.SortedItems.FirstOrDefault() : packageCrList.SortedItems.Previous(Package, true));
		}
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
			SetPackage(packageCrList.SortedItems.Next(Package));
		}
	}

	private async Task<bool> Apply()
	{
		if (B_Apply.Loading || postPackage is null)
		{
			return false;
		}

		if (DD_Usage.SelectedItems.Count() == 0)
		{
			ShowPrompt(LocaleCR.PleaseReviewPackageUsage, PromptButtons.OK, PromptIcons.Hand);
			return false;
		}

		postPackage.Id = Package.Id;
		postPackage.FileName = Path.GetFileName(Package.GetLocalPackageIdentity()?.FilePath ?? string.Empty).IfEmpty(postPackage.FileName);
		postPackage.Name = Package.Name;
		postPackage.ReviewDate = DateTime.UtcNow;
		postPackage.AuthorId = Package.GetWorkshopInfo()?.Author?.Id?.ToString();
		postPackage.IsBlackListedById = CB_BlackListId.Checked;
		postPackage.IsBlackListedByName = CB_BlackListName.Checked;
		postPackage.Stability = DD_Stability.SelectedItem;
		postPackage.Type = DD_PackageType.SelectedItem;
		postPackage.Usage = DD_Usage.SelectedItems.Aggregate((prev, next) => prev | next);
		postPackage.RequiredDLCs = DD_DLCs.SelectedItems.Select(x => x.Id).ToList();
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

		var response = await ServiceCenter.Get<SkyveApiUtil>().UpdatePackageData(postPackage);

		B_Apply.Loading = false;

		if (!response.Success)
		{
			ShowPrompt(response.Message, PromptButtons.OK, PromptIcons.Error);
			return false;
		}

		lastPackageData = postPackage;
		B_ReuseData.Visible = true;

		valuesChanged = false;

		if (Package.GetPackageInfo() is IndexedPackage indexedPackage)
		{
			indexedPackage.Package.ReviewDate = DateTime.Now;
			indexedPackage.Package.Stability = postPackage.Stability;

			_skyveDataManager.CompatibilityData.Packages[indexedPackage.Id] = indexedPackage;
			packageCrList.Invalidate();
		}

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
			SetPackage((IPackageIdentity)sender);
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
