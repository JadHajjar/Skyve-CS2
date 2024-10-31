using Skyve.App.CS2.UserInterface.Content;
using Skyve.App.CS2.UserInterface.Generic;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Forms;
using Skyve.App.UserInterface.Panels;
using Skyve.App.Utilities;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PackagePageBase : PanelContent
{
	protected readonly IncludedButton B_Incl;
	protected readonly ModVersionDropDown DD_Version;
	protected readonly PackageTitleControl L_Title;
	private readonly INotifier _notifier;
	private readonly IPackageUtil _packageUtil;
	private readonly ISettings _settings;
	private readonly IWorkshopService _workshopService;
	private TagControl? addTagControl;
	private bool refreshPending;
	private bool isReadOnly;

	public IPackageIdentity Package { get; private set; }

	protected bool IsReadOnly { get => isReadOnly; set => SetReadOnly(value); }

#nullable disable
	[Obsolete("DESIGNER ONLY", true)]
	public PC_PackagePageBase()
	{
		InitializeComponent();
	}
#nullable enable

	public PC_PackagePageBase(IPackageIdentity package, bool load = false, bool autoRefresh = true) : base(load)
	{
		ServiceCenter.Get(out _notifier, out _packageUtil, out _settings, out _workshopService, out IImageService imageService);

		InitializeComponent();

		Package = package;

		TLP_Side.Controls.Add(B_Incl = new(package) { Dock = DockStyle.Top }, 0, 2);
		TLP_Side.Controls.Add(DD_Version = new(package) { Dock = DockStyle.Top, Visible = !package.IsLocal() }, 0, 3);
		TLP_TopInfo.Controls.Add(L_Title = new(package) { Dock = DockStyle.Fill });
		L_Title.MouseClick += I_More_MouseClick;
		DD_Version.SelectedItemChanged += DD_Version_SelectedItemChanged;

		if (autoRefresh)
		{
			_notifier.WorkshopInfoUpdated += Notifier_WorkshopInfoUpdated;
			_notifier.PackageInclusionUpdated += Notifier_PackageInclusionUpdated;
			_notifier.PackageInformationUpdated += Notifier_WorkshopInfoUpdated;
		}
	}

	private async void DD_Version_SelectedItemChanged(object sender, EventArgs e)
	{
		var currentVersion = _packageUtil.GetSelectedVersion(Package);

		if (DD_Version.Items.Length > 1 && currentVersion != DD_Version.SelectedItem?.VersionId)
		{
			DD_Version.Loading = true;
			Package.Version = DD_Version.SelectedItem?.VersionId;
			await _packageUtil.SetIncluded(Package, true);
			await Task.Delay(1000);
			await _workshopService.RunSync();
			await Task.Delay(1000);
			DD_Version.Loading = false;
		}
	}

	private void SetReadOnly(bool value)
	{
		isReadOnly = value;

		B_Incl.Visible = !value;
		DD_Version.Visible = !value;
	}

	private void Notifier_PackageInclusionUpdated()
	{
		B_Incl.Invalidate();
	}

	private void Notifier_WorkshopInfoUpdated()
	{
		if (Form?.CurrentPanel == this)
		{
			Form.OnNextIdle(() => SetPackage(Package));
		}
		else
		{
			refreshPending = true;
		}
	}

	protected override void OnShown()
	{
		base.OnShown();

		if (refreshPending)
		{
			refreshPending = false;

			this.TryInvoke(() => SetPackage(Package));
		}
	}

	protected virtual void SetPackage(IPackageIdentity package)
	{
		Package = package;

		PB_Icon.Package = Package;
		B_Incl.Package = Package;
		L_Title.Package = Package;

		PB_Icon.Invalidate();
		B_Incl.Invalidate();
		L_Title.Invalidate();

		var workshopInfo = Package.GetWorkshopInfo();
		var localData = Package.GetLocalPackage();

		var date = workshopInfo is null || workshopInfo.ServerTime == default ? (localData?.LocalTime ?? default) : workshopInfo.ServerTime;

		LI_Version.LabelText = localData?.IsCodeMod ?? true ? "Version" : "Content";
		LI_Version.ValueText = localData?.IsCodeMod ?? true ? localData?.VersionName ?? workshopInfo?.VersionName : $"{localData.Assets.Length} {Locale.Asset.FormatPlural(localData.Assets.Length).ToLower()}";
		LI_UpdateTime.ValueText = date == default ? null : _settings.UserSettings.ShowDatesRelatively ? date.ToLocalTime().ToRelatedString(true, false) : date.ToLocalTime().ToString("g");
		LI_ModId.ValueText = Package.Id > 0 ? Package.Id.ToString() : null;
		LI_Size.ValueText = localData?.FileSize.SizeString(0) ?? workshopInfo?.ServerSize.SizeString(0);
		LI_Votes.ValueText = workshopInfo?.VoteCount >= 0 ? Locale.VotesCount.FormatPlural(workshopInfo.VoteCount, workshopInfo.VoteCount.ToString("N0")) : null;
		LI_Subscribers.ValueText = workshopInfo?.Subscribers >= 0 ? Locale.SubscribersCount.FormatPlural(workshopInfo.Subscribers, workshopInfo.Subscribers.ToString("N0")) : null;
		LI_Votes.ValueColor = workshopInfo?.HasVoted == true ? FormDesign.Design.GreenColor : null;

		L_Author.Visible = workshopInfo is not null;
		L_Author.Author = workshopInfo?.Author;

		var currentVersion = _packageUtil.GetSelectedVersion(package);
		DD_Version.Items = workshopInfo?.Changelog.OrderByDescending(x => x.ReleasedDate).ToArray() ?? [];
		DD_Version.Visible = !isReadOnly && !string.IsNullOrEmpty(currentVersion) && DD_Version.Items.Length > 0;
		DD_Version.SelectedItem = DD_Version.Items.FirstOrDefault(x => x.VersionId == currentVersion);

		// Links
		{
			var links = new List<ILink>();

			if (workshopInfo?.HasComments() ?? false)
			{
				links.Add(ServiceCenter.Get<IWorkshopService>().GetCommentsPageUrl(Package)!);
			}

			links.AddRange(workshopInfo?.Links ?? []);
			links.AddRange(Package.GetPackageInfo()?.Links ?? []);

			links = links.DistinctList(x => x.Url);

			if (!links.ToList(x => x.Url).SequenceEqual(FLP_Package_Links.Controls.OfType<LinkControl>().Select(x => x.Link.Url)))
			{
				FLP_Package_Links.SuspendDrawing();
				FLP_Package_Links.Controls.Clear(true);
				FLP_Package_Links.Controls.AddRange(links.ToArray(x => new LinkControl(x, true)));
				FLP_Package_Links.ResumeDrawing();
			}

			if (TLP_Links.Visible = links.Count > 0)
			{
				FLP_Package_Links_SizeChanged(this, EventArgs.Empty);
			}
		}

		// Requirements
		{
			var requirements = workshopInfo?.Requirements.ToList() ?? [];

			if (requirements.Count > 0)
			{
				if (!requirements.ToList(x => x.Id).SequenceEqual(P_Requirements.Controls.OfType<MiniPackageControl>().Select(x => x.Id)))
				{
					P_Requirements.SuspendDrawing();
					P_Requirements.Controls.Clear(true);
					P_Requirements.Controls.AddRange(requirements.ToArray(x => new MiniPackageControl(x)
					{
						ReadOnly = true,
						Large = requirements.Count < 6,
						ShowIncluded = true,
						IsDlc = x.IsDlc,
						Dock = DockStyle.Top
					}));
					P_Requirements.ResumeDrawing();
				}
				else
				{
					TLP_ModRequirements.Invalidate(true);
				}

				TLP_ModRequirements.Visible = true;
			}
			else
			{
				TLP_ModRequirements.Visible = false;
			}
		}

		// Tags
		if (!Package.GetTags().SequenceEqual(FLP_Package_Tags.Controls.OfType<TagControl>().SelectWhereNotNull(x => x.TagInfo)))
		{
			AddTags();
		}
	}

	private void AddTagControl_MouseClick(object sender, MouseEventArgs e)
	{
		var frm = new EditTagsForm([Package]);

		App.Program.MainForm.OnNextIdle(() =>
		{
			frm.Show(App.Program.MainForm);

			frm.ShowUp();
		});

		frm.FormClosed += (_, _) =>
		{
			if (frm.DialogResult == DialogResult.OK)
			{
				AddTags();
			}
		};
	}

	private void AddTags()
	{
		FLP_Package_Tags.SuspendDrawing();
		FLP_Package_Tags.Controls.Clear(true);

		foreach (var item in Package.GetTags())
		{
			var control = new TagControl { TagInfo = item, Display = true };
			control.MouseClick += TagControl_Click;
			FLP_Package_Tags.Controls.Add(control);
		}

		if (!isReadOnly)
		{
			addTagControl = new TagControl { ImageName = "Add", ColorStyle = ColorStyle.Green };
			addTagControl.MouseClick += AddTagControl_MouseClick;
			FLP_Package_Tags.Controls.Add(addTagControl);
		}

		FLP_Package_Tags.ResumeDrawing();
	}

	private void TagControl_Click(object sender, EventArgs e)
	{
		if (!(sender as TagControl)!.TagInfo!.IsCustom)
		{
			return;
		}

		(sender as TagControl)!.Dispose();

		ServiceCenter.Get<ITagsService>().SetTags(Package, FLP_Package_Tags.Controls.OfType<TagControl>().Select(x => x.TagInfo?.IsCustom == true ? x.TagInfo.Value?.Replace(' ', '-') : null)!);

		_notifier.OnRefreshUI(true);
	}

	protected override void LocaleChanged()
	{
		L_Info.Text = Locale.Info.One.ToUpper();
		L_Requirements.Text = Locale.Dependency.Plural.ToUpper();
		L_Tags.Text = LocaleSlickUI.Tags.One.ToUpper();
		L_Links.Text = Locale.Links.One.ToUpper();
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		P_SideContainer.Width = UI.Scale(260);
		PB_Icon.Size = UI.Scale(new Size(72, 72));
		I_More.Size = UI.Scale(new Size(20, 28));
		TLP_Side.Padding = UI.Scale(new Padding(8, 0, 0, 0));
		TLP_TopInfo.Margin = B_Incl.Margin = base_slickSpacer.Margin = UI.Scale(new Padding(5));
		base_slickSpacer.Height = (int)UI.FontScale;
		TLP_ModInfo.Padding = TLP_ModRequirements.Padding = TLP_Tags.Padding = TLP_Links.Padding = DD_Version.Margin =
		TLP_ModInfo.Margin = TLP_ModRequirements.Margin = TLP_Tags.Margin = TLP_Links.Margin = UI.Scale(new Padding(5));
		L_Info.Font = L_Requirements.Font = L_Tags.Font = L_Links.Font = UI.Font(7F, FontStyle.Bold);
		L_Info.Margin = L_Requirements.Margin = L_Tags.Margin = L_Links.Margin = UI.Scale(new Padding(3));
		L_Author.Margin = L_Title.Margin = UI.Scale(new Padding(5, 0, 0, 0));
		L_Author.Font = UI.Font(9.5F);

		TLP_TopInfo.Height = UI.Scale(72);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		L_Info.ForeColor = L_Requirements.ForeColor = L_Tags.ForeColor = L_Links.ForeColor = design.LabelColor;
		TLP_ModInfo.BackColor = TLP_ModRequirements.BackColor = TLP_Tags.BackColor = TLP_Links.BackColor = design.BackColor.Tint(Lum: design.IsDarkTheme ? 6 : -5);
	}

	private void I_More_MouseClick(object sender, MouseEventArgs e)
	{
		if (sender == I_More || e.Button == MouseButtons.Right)
		{
			this.TryBeginInvoke(() =>
				SlickToolStrip.Show(
					App.Program.MainForm,
					ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems([Package]))
			);
		}
	}

	private void B_BulkRequirements_Click(object sender, EventArgs e)
	{
		var items = Package.GetWorkshopInfo()?.Requirements ?? [];
		var isFiltered = false;
		var isSelected = false;
		var anyIncluded = items.Any(x => _packageUtil.IsIncluded(x));
		var anyExcluded = items.Any(x => !_packageUtil.IsIncluded(x));
		var anyEnabled = items.Any(x => _packageUtil.IsIncluded(x) && _packageUtil.IsEnabled(x));
		var anyDisabled = items.Any(x => _packageUtil.IsIncluded(x) && !_packageUtil.IsEnabled(x));
		var allLocal = items.Any(x => !x.IsLocal());
		var allWorkshop = items.Any(x => x.IsLocal());

		var stripItems = new SlickStripItem?[]
		{
			  anyDisabled ? new (isSelected ? Locale.EnableAllSelected : isFiltered ? Locale.EnableAllFiltered : Locale.EnableAll, "Ok", async () => await EnableAll(items)) : null
			, anyEnabled ? new (isSelected ? Locale.DisableAllSelected : isFiltered ? Locale.DisableAllFiltered : Locale.DisableAll, "Enabled",  async () => await DisableAll(items)) : null
			, new ()
			, anyExcluded ? new (isSelected ? Locale.IncludeAllSelected : isFiltered ? Locale.IncludeAllFiltered : Locale.IncludeAll, "Add",  async() => await IncludeAll(items)) : null
			, anyIncluded ? new (isSelected ? Locale.ExcludeAllSelected : isFiltered ? Locale.ExcludeAllFiltered : Locale.ExcludeAll, "X",  async() => await ExcludeAll(items)) : null
			, anyDisabled ? new (isSelected ? Locale.ExcludeAllDisabledSelected : isFiltered ? Locale.ExcludeAllDisabledFiltered : Locale.ExcludeAllDisabled, "Cancel",  async() => await ExcludeAllDisabled(items)) : null
			, new (isSelected ? Locale.CopyAllIdsSelected : isFiltered ? Locale.CopyAllIdsFiltered : Locale.CopyAllIds, "Copy", () => Clipboard.SetText(items.ListStrings(x => x.IsLocal() ? $"Local: {x.Name}" : $"{x.Id}: {x.Name}", CrossIO.NewLine)))
		};

		this.TryBeginInvoke(() => SlickToolStrip.Show(App.Program.MainForm, B_BulkRequirements.PointToScreen(new Point(0, B_BulkRequirements.Height + 5)), stripItems));
	}

	protected async Task SetIncluded(IEnumerable<IPackageIdentity> filteredItems, bool included)
	{
		await ServiceCenter.Get<IPackageUtil>().SetIncluded(filteredItems, included);
	}

	protected async Task SetEnabled(IEnumerable<IPackageIdentity> filteredItems, bool enabled)
	{
		await ServiceCenter.Get<IPackageUtil>().SetEnabled(filteredItems, enabled);
	}

	private async Task DisableAll(IEnumerable<IPackageRequirement> items)
	{
		B_BulkRequirements.Loading = true;
		await SetEnabled(items.ToList(), false);
		P_Requirements.Invalidate(true);
		B_BulkRequirements.Loading = false;
	}

	private async Task EnableAll(IEnumerable<IPackageRequirement> items)
	{
		B_BulkRequirements.Loading = true;
		await SetEnabled(items.ToList(), true);
		P_Requirements.Invalidate(true);
		B_BulkRequirements.Loading = false;
	}

	private async Task ExcludeAll(IEnumerable<IPackageRequirement> item)
	{
		var items = item.ToList();

		if (items.Count > 10 && MessagePrompt.Show(Locale.AreYouSure, PromptButtons.YesNo, PromptIcons.Question, App.Program.MainForm) != DialogResult.Yes)
		{
			return;
		}

		B_BulkRequirements.Loading = true;
		await SetIncluded(items, false);
		P_Requirements.Invalidate(true);
		B_BulkRequirements.Loading = false;
	}

	private async Task ExcludeAllDisabled(IEnumerable<IPackageRequirement> items)
	{
		await SetIncluded(items.AllWhere(x => !_packageUtil.IsEnabled(x)), false);
		P_Requirements.Invalidate(true);
		B_BulkRequirements.Loading = false;
	}

	private async Task IncludeAll(IEnumerable<IPackageRequirement> items)
	{
		B_BulkRequirements.Loading = true;
		await SetIncluded(items.ToList(), true);
		P_Requirements.Invalidate(true);
		B_BulkRequirements.Loading = false;
	}

	private void L_Author_Click(object sender, EventArgs e)
	{
		var workshopInfo = Package.GetWorkshopInfo();

		if (workshopInfo?.Author != null)
		{
			App.Program.MainForm.PushPanel(new PC_UserPage(workshopInfo.Author));
		}
	}

	private void SlickWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
	{
		if (e.Url.AbsoluteUri == "about:blank")
		{
			return;
		}

		e.Cancel = true;

		var regex = Regex.Match(e.Url.AbsoluteUri, @"mods\.paradoxplaza\.com/mods/(\d+)");

		if (regex.Success)
		{
			ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(new GenericPackageIdentity(ulong.Parse(regex.Groups[1].Value)), false);
		}
		else
		{
			PlatformUtil.OpenUrl(e.Url.AbsoluteUri);
		}
	}

	private async void LI_Votes_ValueClicked(object sender, EventArgs e)
	{
		await ServiceCenter.Get<IWorkshopService>().ToggleVote(Package);

		var workshopInfo = Package.GetWorkshopInfo();
		LI_Votes.ValueText = workshopInfo?.VoteCount >= 0 ? Locale.VotesCount.FormatPlural(workshopInfo.VoteCount, workshopInfo.VoteCount.ToString("N0")) : null;
		LI_Votes.ValueColor = workshopInfo?.HasVoted == true ? FormDesign.Design.GreenColor : null;
		LI_Votes.Invalidate();
	}

	private void LI_Votes_HoverStateChanged(object sender, HoverState e)
	{
		var workshopInfo = Package.GetWorkshopInfo();
		LI_Votes.LabelText = LI_Votes.HoverState.HasFlag(HoverState.Hovered) ? (workshopInfo?.HasVoted == true ? LocaleCS2.UnVoteMod : LocaleCS2.VoteMod) : "Votes";
		LI_Votes.Invalidate();
	}

	private void FLP_Package_Links_SizeChanged(object sender, EventArgs e)
	{
		foreach (Control ctrl in FLP_Package_Links.Controls)
		{
			ctrl.Size = new(ctrl.Parent.Width / 3 - ctrl.Margin.Horizontal, ctrl.Parent.Width / 3 - ctrl.Margin.Horizontal);
		}

		FLP_Package_Links.PerformLayout();
	}
}
