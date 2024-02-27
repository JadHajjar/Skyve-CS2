using Skyve.App.CS2.UserInterface.Content;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.CompatibilityReport;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Forms;
using Skyve.App.UserInterface.Lists;
using Skyve.App.UserInterface.Panels;
using Skyve.App.Utilities;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PackagePageBase : PanelContent
{
	protected readonly IncludedButton B_Incl;
	protected readonly PackageTitleControl L_Title;
	private readonly INotifier _notifier;
	private readonly IPackageUtil _packageUtil;
	private readonly ISettings _settings;
	private TagControl? addTagControl;

	public IPackageIdentity Package { get; private set; }

#nullable disable
	[Obsolete("DESIGNER ONLY", true)]
    public PC_PackagePageBase()
	{
		InitializeComponent();
	}
#nullable enable

	public PC_PackagePageBase(IPackageIdentity package)
	{
		ServiceCenter.Get(out _notifier, out _packageUtil, out _settings, out IImageService imageService);

		InitializeComponent();

		Package = package;

		TLP_Side.Controls.Add(B_Incl = new(package) { Dock = DockStyle.Top }, 0, 2);
		TLP_TopInfo.Controls.Add(L_Title = new(package) { Dock = DockStyle.Fill });
		L_Title.MouseClick += I_More_MouseClick;

		_notifier.WorkshopInfoUpdated += Notifier_WorkshopInfoUpdated;
		_notifier.PackageInclusionUpdated += Notifier_WorkshopInfoUpdated;
		_notifier.PackageInformationUpdated += Notifier_WorkshopInfoUpdated;
	}

	private void Notifier_WorkshopInfoUpdated()
	{
		this.TryInvoke(() => SetPackage(Package));
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

		LI_Version.ValueText = localData?.Version ?? workshopInfo?.Version;
		LI_UpdateTime.ValueText = _settings.UserSettings.ShowDatesRelatively ? date.ToRelatedString(true, false) : date.ToString("g");
		LI_ModId.ValueText = Package.Id > 0 ? Package.Id.ToString() : null;
		LI_Size.ValueText = localData?.FileSize.SizeString(0) ?? workshopInfo?.ServerSize.SizeString(0);
		LI_Votes.ValueText = workshopInfo?.VoteCount >= 0 ? Locale.VotesCount.FormatPlural(workshopInfo.VoteCount, workshopInfo.VoteCount.ToString("N0")) : null;
		LI_Subscribers.ValueText = workshopInfo?.Subscribers >= 0 ? Locale.SubscribersCount.FormatPlural(workshopInfo.Subscribers, workshopInfo.Subscribers.ToString("N0")) : null;
		LI_Votes.ValueColor = workshopInfo?.HasVoted == true ? FormDesign.Design.GreenColor : null;

		L_Author.Visible = workshopInfo is not null;
		L_Author.Text = workshopInfo?.Author?.Name;

		// Links
		{
			var links = new List<ILink>();

			links.AddRange(workshopInfo?.Links ?? []);
			links.AddRange(Package.GetPackageInfo()?.Links ?? []);

			if (!links.ToList(x => x.Url).SequenceEqual(FLP_Links.Controls.OfType<LinkControl>().Select(x => x.Link.Url)))
			{
				FLP_Links.Controls.Clear(true);
				FLP_Links.Controls.AddRange(links.ToArray(x => new LinkControl(x, true)));
			}

			TLP_Links.Visible = links.Count > 0;
		}

		// Requirements
		{
			var requirements = workshopInfo?.Requirements.ToList() ?? [];

			if (requirements.Count > 0)
			{
				if (!requirements.ToList(x => x.Id).SequenceEqual(P_Requirements.Controls.OfType<MiniPackageControl>().Select(x => x.Id)))
				{
					P_Requirements.Controls.Clear(true);
					P_Requirements.Controls.AddRange(requirements.ToArray(x => new MiniPackageControl(x.Id)
					{
						ReadOnly = true,
						Large = true,
						ShowIncluded = true,
						Dock = DockStyle.Top
					}));
				}

				TLP_ModRequirements.Visible = true;
			}
			else
			{
				TLP_ModRequirements.Visible = false;
			}
		}

		// Tags
		if (!Package.GetTags().SequenceEqual(FLP_Tags.Controls.OfType<TagControl>().SelectWhereNotNull(x => x.TagInfo)))
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
		FLP_Tags.SuspendDrawing();
		FLP_Tags.Controls.Clear(true);

		foreach (var item in Package.GetTags())
		{
			var control = new TagControl { TagInfo = item, Display = true };
			control.MouseClick += TagControl_Click;
			FLP_Tags.Controls.Add(control);
		}

		//if (Package.LocalPackage is not null)
		{
			addTagControl = new TagControl { ImageName = "I_Add", ColorStyle = ColorStyle.Green };
			addTagControl.MouseClick += AddTagControl_MouseClick;
			FLP_Tags.Controls.Add(addTagControl);
		}

		FLP_Tags.ResumeDrawing();
	}

	private void TagControl_Click(object sender, EventArgs e)
	{
		if (!(sender as TagControl)!.TagInfo!.IsCustom)
		{
			return;
		}

		(sender as TagControl)!.Dispose();

		ServiceCenter.Get<ITagsService>().SetTags(Package, FLP_Tags.Controls.OfType<TagControl>().Select(x => x.TagInfo?.IsCustom == true ? x.TagInfo.Value?.Replace(' ', '-') : null)!);

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

		P_SideContainer.Width = (int)(260 * UI.FontScale);
		PB_Icon.Size = UI.Scale(new Size(72, 72), UI.FontScale);
		I_More.Size = UI.Scale(new Size(20, 28), UI.FontScale);
		TLP_Side.Padding = UI.Scale(new Padding(8, 0, 0, 0), UI.FontScale);
		TLP_TopInfo.Margin = B_Incl.Margin = slickSpacer1.Margin = UI.Scale(new Padding(5), UI.FontScale);
		slickSpacer1.Height = (int)UI.FontScale;
		TLP_ModInfo.Padding = TLP_ModRequirements.Padding = TLP_Tags.Padding = TLP_Links.Padding =
		TLP_ModInfo.Margin = TLP_ModRequirements.Margin = TLP_Tags.Margin = TLP_Links.Margin = UI.Scale(new Padding(5), UI.FontScale);
		L_Info.Font = L_Requirements.Font = L_Tags.Font = L_Links.Font = UI.Font(7F, FontStyle.Bold);
		L_Info.Margin = L_Requirements.Margin = L_Tags.Margin = L_Links.Margin = UI.Scale(new Padding(3), UI.FontScale);
		L_Author.Margin = L_Title.Margin = UI.Scale(new Padding(5, 0, 0, 0), UI.FontScale);

		TLP_TopInfo.Height = (int)(72 * UI.FontScale);
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
			  anyDisabled ? new (isSelected ? Locale.EnableAllSelected : isFiltered ? Locale.EnableAllFiltered : Locale.EnableAll, "I_Ok", async () => await EnableAll(items)) : null
			, anyEnabled ? new (isSelected ? Locale.DisableAllSelected : isFiltered ? Locale.DisableAllFiltered : Locale.DisableAll, "I_Enabled",  async () => await DisableAll(items)) : null
			, new ()
			, anyExcluded ? new (isSelected ? Locale.IncludeAllSelected : isFiltered ? Locale.IncludeAllFiltered : Locale.IncludeAll, "I_Add",  async() => await IncludeAll(items)) : null
			, anyIncluded ? new (isSelected ? Locale.ExcludeAllSelected : isFiltered ? Locale.ExcludeAllFiltered : Locale.ExcludeAll, "I_X",  async() => await ExcludeAll(items)) : null
			, anyDisabled ? new (isSelected ? Locale.ExcludeAllDisabledSelected : isFiltered ? Locale.ExcludeAllDisabledFiltered : Locale.ExcludeAllDisabled, "I_Cancel",  async() => await ExcludeAllDisabled(items)) : null
			, new (isSelected ? Locale.CopyAllIdsSelected : isFiltered ? Locale.CopyAllIdsFiltered : Locale.CopyAllIds, "I_Copy", () => Clipboard.SetText(items.ListStrings(x => x.IsLocal() ? $"Local: {x.Name}" : $"{x.Id}: {x.Name}", CrossIO.NewLine)))
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
}
