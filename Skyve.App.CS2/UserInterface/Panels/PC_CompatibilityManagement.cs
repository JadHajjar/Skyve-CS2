using Skyve.App.Interfaces;
using Skyve.App.UserInterface.CompatibilityReport;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Forms;
using Skyve.App.UserInterface.Lists;
using Skyve.App.Utilities;
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
	private readonly bool singlePackage;
	private int currentPage;
	private CompatibilityPostPackage? postPackage;
	private CompatibilityPostPackage? lastPackageData;
	private bool valuesChanged;
	private readonly EditHistoryList editHistoryList;
	private readonly ReviewRequest? _request;

	private readonly ICompatibilityManager _compatibilityManager;
	private readonly SkyveDataManager _skyveDataManager;
	private readonly IWorkshopService _workshopService;
	private readonly IUserService _userService;
	private readonly ITagsService _tagsService;
	private readonly ICitiesManager _cityManager;

	public PC_CompatibilityManagement(IEnumerable<IPackageIdentity> packages) : this(false)
	{
		packageCrList.SetItems(packages.Distinct(x => x.Id));

		if (singlePackage = packageCrList.FilteredCount == 1)
		{
			Padding = new Padding(5, 0, 0, 0);
			base_P_Side.Visible = false;
			CB_HideReviewedPackages.Visible = false;
		}

		SetPackage(packageCrList.SortedAndFilteredItems.FirstOrDefault());
	}

	public PC_CompatibilityManagement() : this(true)
	{
	}

	public PC_CompatibilityManagement(bool load) : base(new GenericPackageIdentity(), load, false)
	{
		ServiceCenter.Get(out _workshopService, out _compatibilityManager, out _userService, out _tagsService, out _cityManager, out ISkyveDataManager skyveDataManager);

		_skyveDataManager = (SkyveDataManager)skyveDataManager;

		InitializeComponent();

		IsReadOnly = true;

		SlickTip.SetTo(B_Skip, "Skip");
		SlickTip.SetTo(B_Previous, "Previous");
		SlickTip.SetTo(P_Tags, "GlobalTagsInfo");
		SlickTip.SetTo(B_ReuseData, "ReuseData_Tip");
		TB_Search.Placeholder = $"{LocaleSlickUI.Search}..";
		T_Statuses.Text = LocaleCR.StatusesCount.Format(0);
		T_Interactions.Text = LocaleCR.InteractionCount.Format(0);
		T_EditHistory.LinkedControl = editHistoryList = new EditHistoryList();

		packageCrList.CanDrawItem += PackageCrList_CanDrawItem;

		P_Tags.Enabled = _userService.User.Manager;
		DD_Stability.Enabled = _userService.User.Manager;
		TB_Note.Enabled = _userService.User.Manager;
		CB_BlackListId.Visible = _userService.User.Manager;
		CB_BlackListName.Visible = _userService.User.Manager;
	}

	public PC_CompatibilityManagement(ReviewRequest reviewRequest) : this([reviewRequest])
	{
		_request = reviewRequest;
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
			if (ModifierKeys.HasFlag(Keys.Control))
			{
				PlatformUtil.OpenUrl(Package.Url);
			}
			else
			{
				ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(Package, false);
			}
		}
	}

	protected override void UIChanged()
	{
		base_P_Side.Width = UI.Scale(175);
		base_TLP_Side.Padding = UI.Scale(new Padding(5));
		base_P_Side.Padding = UI.Scale(new Padding(0, 5, 5, 5));
		slickTabControl.Padding = P_SideContainer.Padding = new Padding(0, UI.Scale(30), 0, 0);
		CustomTitleBounds = new Point(singlePackage ? 0 : UI.Scale(175), 0);

		base.UIChanged();

		slickSpacer3.Margin = B_Previous.Margin = B_Skip.Margin = B_Previous.Padding = B_Skip.Padding
			= TLP_Bottom.Padding = P_Tags.Margin = P_Links.Margin
			= DD_DLCs.Margin = DD_PackageType.Margin = DD_Stability.Margin = DD_Usage.Margin
			= B_ReuseData.Margin = B_Apply.Margin = slickSpacer2.Margin = UI.Scale(new Padding(5));
		slickSpacer2.Height = UI.Scale(2);
		slickSpacer3.Height = slickSpacer4.Height = slickSpacer5.Height = UI.Scale(1);
		B_AddInteraction.Size = B_AddStatus.Size = UI.Scale(new Size(105, 70));
		B_AddInteraction.Margin = B_AddStatus.Margin = UI.Scale(new Padding(15));
		L_NoLinks.Margin = L_NoTags.Margin = UI.Scale(new Padding(10));
		B_Previous.Size = B_Skip.Size = UI.Scale(new Size(32, 32));
		L_Page.Font = UI.Font(7.5F, FontStyle.Bold);
		TB_EditNote.Margin = UI.Scale(new Padding(20, 5, 10, 5));
		TB_Note.Margin = UI.Scale(new Padding(5, 20, 5, 5));
		TB_Note.Height = UI.Scale(200);
		TB_EditNote.Height = UI.Scale(32);
		PB_Loading.Size = UI.Scale(new Size(32, 32));
		CB_BlackListId.Font = CB_BlackListName.Font = UI.Font(7.5F);
		B_Apply.Font = B_ReuseData.Font = UI.Font(9.75F);

		TLP_Bottom.ColumnStyles[3].Width = UI.Scale(260);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		P_Tags.BackColor = P_Links.BackColor = design.AccentBackColor;
		base_TLP_Side.BackColor = design.MenuColor;
		base_TLP_Side.ForeColor = design.MenuForeColor;
		L_Page.ForeColor = design.LabelColor;
	}

	protected override void LocaleChanged()
	{
		base.LocaleChanged();

		L_NoTags.Text = LocaleCR.NoTags;
		L_NoLinks.Text = LocaleCR.NoLinks;
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
			|| currentPage >= packageCrList.FilteredCount - 1
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

		SetPackage(packageCrList.SortedAndFilteredItems.FirstOrDefault());
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

		if (packageCrList.FilteredCount > 0)
		{
			currentPage = packageCrList.SortedAndFilteredItems.IndexOf(package);

			if (currentPage < 0 || currentPage >= packageCrList.FilteredCount)
			{
				PushBack();
				return;
			}

			L_Page.Text = $"{currentPage + 1} / {packageCrList.FilteredCount}";
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

		var workshopInfo = package.GetWorkshopInfo();
		var hasChangelog = workshopInfo?.Changelog?.Any() ?? false;

		T_Changelog.Visible = hasChangelog;

		if (hasChangelog)
		{
			packageChangelogControl1.SetChangelogs(workshopInfo!.Version ?? string.Empty, workshopInfo!.Changelog);
		}

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
			postPackage ??= skyveDataManager.GetAutomatedReport(Package).CloneTo<PackageData, CompatibilityPostPackage>();

			postPackage.IsBlackListedById = skyveDataManager.CompatibilityData.BlackListedIds?.Contains(postPackage.Id) ?? false;
			postPackage.IsBlackListedByName = skyveDataManager.CompatibilityData.BlackListedNames?.Contains(postPackage.Name ?? string.Empty) ?? false;

			SetData(postPackage);

			B_Previous.Enabled = currentPage > 0;
			B_Skip.Enabled = currentPage != packageCrList.FilteredCount - 1;

			PB_Loading.Loading = false;
			PB_Loading.Visible = false;
			slickTabControl.Visible = true;
			TLP_Bottom.Visible = true;

			T_Info.Selected = true;
			TLP_MainInfo.Width = 0;
			packageCrList.Invalidate();
			valuesChanged = false;

			editHistoryList.SetItems(await skyveApiUtil.GetPackageEdits(Package.Id));
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

		if (DD_Stability.SelectedItem is PackageStability.NotReviewed && !DD_Stability.Enabled)
		{
			DD_Stability.SelectedItem = PackageStability.NotEnoughInformation;
		}

		TB_Note.Text = postPackage.Note;
		TB_EditNote.Text = string.Empty;

		FLP_Tags.Controls.Clear(true);
		FLP_Statuses.Controls.Clear(true, x => x is IPackageStatusControl<StatusType, PackageStatus>);
		FLP_Interactions.Controls.Clear(true, x => x is IPackageStatusControl<InteractionType, PackageInteraction>);

		foreach (var item in postPackage.Tags ?? [])
		{
			var control = new TagControl { TagInfo = _tagsService.CreateCustomTag(item) };
			control.Click += TagControl_Click;
			FLP_Tags.Controls.Add(control);
		}

		L_NoTags.Visible = FLP_Tags.Controls.Count == 0;

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
			SetPackage(ModifierKeys.HasFlag(Keys.Control) ? packageCrList.SortedAndFilteredItems.LastOrDefault() : packageCrList.SortedAndFilteredItems.Next(Package, true));
		}
	}

	private void B_Previous_Click(object sender, EventArgs e)
	{
		if (B_Previous.Enabled)
		{
			SetPackage(ModifierKeys.HasFlag(Keys.Control) ? packageCrList.SortedAndFilteredItems.FirstOrDefault() : packageCrList.SortedAndFilteredItems.Previous(Package, true));
		}
	}

	private void T_NewTag_Click(object sender, EventArgs e)
	{
		var frm = new EditTagsForm([Package], FLP_Tags.Controls.Cast<TagControl>().Select(x => x.TagInfo!));

		App.Program.MainForm.OnNextIdle(() =>
		{
			frm.Show(App.Program.MainForm);

			frm.ShowUp();
		});

		frm.ApplyTags += (tags) =>
		{
			FLP_Tags.Controls.Clear(true);

			foreach (var item in tags)
			{
				var control = new TagControl { TagInfo = _tagsService.CreateCustomTag(item) };
				control.Click += TagControl_Click;
				FLP_Tags.Controls.Add(control);
			}

			L_NoTags.Visible = FLP_Tags.Controls.Count == 0;
		};

		ControlValueChanged(sender, e);
	}

	private void TagControl_Click(object sender, EventArgs e)
	{
		(sender as Control)?.Dispose();

		L_NoTags.Visible = FLP_Tags.Controls.Count == 0;

		ControlValueChanged(sender, e);
	}

	private void T_NewLink_Click(object sender, EventArgs e)
	{
		var form = new AddLinkForm(FLP_Links.Controls.OfType<LinkControl>().ToList(x => x.Link));

		form.Show(Form);

		form.LinksReturned += SetLinks;
	}

	private void SetLinks(IEnumerable<PackageLink> links)
	{
		FLP_Links.Controls.Clear(true);

		foreach (var item in links.OrderBy(x => x.Type))
		{
			var control = new LinkControl(item, true);
			control.Click += T_NewLink_Click;
			FLP_Links.Controls.Add(control);
		}

		L_NoLinks.Visible = FLP_Links.Controls.Count == 0;

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
			SetPackage(packageCrList.SortedAndFilteredItems.Next(Package));
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

		if (DD_Stability.SelectedItem is PackageStability.HasIssues or PackageStability.Broken && TB_Note.Text.Trim().Length < 5)
		{
			ShowPrompt(LocaleCR.AddMeaningfulNote, PromptButtons.OK, PromptIcons.Hand);
			return false;
		}

		postPackage.Id = Package.Id;
		postPackage.FileName = Path.GetFileName(Package.GetLocalPackageIdentity()?.FilePath ?? string.Empty).IfEmpty(postPackage.FileName);
		postPackage.Name = Package.Name;
		postPackage.ReviewDate = DateTime.UtcNow;
		postPackage.ReviewedGameVersion = _cityManager.GameVersion.IfEmpty(postPackage.ReviewedGameVersion);
		postPackage.AuthorId = Package.GetWorkshopInfo()?.Author?.Id?.ToString();
		postPackage.IsBlackListedById = CB_BlackListId.Checked;
		postPackage.IsBlackListedByName = CB_BlackListName.Checked;
		postPackage.Stability = DD_Stability.SelectedItem;
		postPackage.Type = DD_PackageType.SelectedItem;
		postPackage.Usage = DD_Usage.SelectedItems.Aggregate((prev, next) => prev | next);
		postPackage.RequiredDLCs = DD_DLCs.SelectedItems.Select(x => x.Id).ToList();
		postPackage.Note = TB_Note.Text;
		postPackage.EditNote = TB_EditNote.Text;
		postPackage.Tags = FLP_Tags.Controls.OfType<TagControl>().Where(x => !string.IsNullOrEmpty(x.TagInfo?.Value)).ToList(x => x.TagInfo!.Value);
		postPackage.Links = FLP_Links.Controls.OfType<LinkControl>().ToList(x => (PackageLink)x.Link);
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
			SlickToolStrip.Show(Form, packageCrList.PointToClient(e.Location), ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems((IPackageIdentity)sender));
		}
	}

	private void TB_Search_TextChanged(object sender, EventArgs e)
	{
		TB_Search.ImageName = string.IsNullOrWhiteSpace(TB_Search.Text) ? "Search" : "ClearSearch";

		packageCrList.FilterChanged();

		var package = packageCrList.FilteredItems.FirstOrDefault();
	}

	private void PackageCrList_CanDrawItem(object sender, CanDrawItemEventArgs<IPackageIdentity> e)
	{
		var package = _workshopService.GetPackage(e.Item);

		if (package is null)
		{
			return;
		}

		if (CB_HideReviewedPackages.Checked)
		{
			var cr = package.GetPackageInfo();
			var isUpToDate = cr?.ReviewDate.ToLocalTime() > e.Item.GetWorkshopInfo()?.ServerTime.ToLocalTime();

			if (isUpToDate)
			{
				e.DoNotDraw = true;
				return;
			}
		}

		e.DoNotDraw = !(TB_Search.Text.SearchCheck(package.Name)
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

	private void CB_HideReviewedPackages_CheckChanged(object sender, EventArgs e)
	{
		packageCrList.FilterChanged();

		var package = packageCrList.FilteredItems.FirstOrDefault();

		if (package is not null)
		{
			SetPackage(package);
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
