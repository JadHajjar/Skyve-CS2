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
public partial class PC_PackagePage : PanelContent
{
	private readonly ItemListControl LC_Items;
	private readonly ContentList LC_References;
	private readonly IncludedButton B_Incl;
	private readonly PackageTitleControl L_Title;
	private readonly INotifier _notifier;
	private readonly ICompatibilityManager _compatibilityManager;
	private readonly IPackageUtil _packageUtil;
	private readonly ISettings _settings;
	private readonly PackageCompatibilityControl _packageCompatibilityControl;
	private TagControl? addTagControl;

	public IPackageIdentity Package { get; }

	public PC_PackagePage(IPackageIdentity package, bool compatibilityPage = false)
	{
		ServiceCenter.Get(out _notifier, out _compatibilityManager, out _packageUtil, out _settings, out IImageService imageService);

		InitializeComponent();

		PB_Icon.Package = Package = package;

		Controls.Add(_packageCompatibilityControl = new(Package) { Dock = DockStyle.Top });
		TLP_Side.Controls.Add(B_Incl = new(Package) { Dock = DockStyle.Top }, 0, 2);
		TLP_TopInfo.Controls.Add(L_Title = new(Package) { Dock = DockStyle.Fill });
		L_Title.MouseClick += I_More_MouseClick;

		if (_settings.UserSettings.ComplexListUI)
		{
			LC_Items = new ItemListControl.Complex(SkyvePage.SinglePackage) { IsPackagePage = true };
		}
		else
		{
			LC_Items = new ItemListControl.Simple(SkyvePage.SinglePackage) { IsPackagePage = true };
		}

		LC_References = new ContentList(SkyvePage.SinglePackage, true, GetItems, SetIncluded, SetEnabled, GetItemText, GetCountText);
		LC_References.TB_Search.Placeholder = "SearchGenericPackages";

		T_References.LinkedControl = LC_References;
		T_Content.LinkedControl = LC_Items;
		T_Compatibility.LinkedControl = new PackageCompatibilityReportControl(package);
		T_Playsets.LinkedControl = new OtherPlaysetPackage(package);

		if (compatibilityPage)
		{
			T_Compatibility.PreSelected = true;
		}

		_notifier.WorkshopInfoUpdated += Notifier_WorkshopInfoUpdated;
		_notifier.PackageInclusionUpdated += Notifier_WorkshopInfoUpdated;
		_notifier.PackageInformationUpdated += Notifier_WorkshopInfoUpdated;

		Notifier_WorkshopInfoUpdated();
	}

	private void Notifier_WorkshopInfoUpdated()
	{
		this.TryInvoke(() =>
		{
			var workshopInfo = Package.GetWorkshopInfo();
			var localData = Package.GetLocalPackage();

			var date = workshopInfo is null || workshopInfo.ServerTime == default ? (localData?.LocalTime ?? default) : workshopInfo.ServerTime;

			LC_Items?.Invalidate();
			B_Incl.Invalidate();

			LI_Version.ValueText = localData?.Version ?? workshopInfo?.Version;
			LI_UpdateTime.ValueText = _settings.UserSettings.ShowDatesRelatively ? date.ToRelatedString(true, false) : date.ToString("g");
			LI_ModId.ValueText = Package.Id > 0 ? Package.Id.ToString() : null;
			LI_Size.ValueText = localData?.FileSize.SizeString(0) ?? workshopInfo?.ServerSize.SizeString(0);
			LI_Votes.ValueText = workshopInfo?.VoteCount >= 0 ? Locale.VotesCount.FormatPlural(workshopInfo.VoteCount, workshopInfo.VoteCount.ToString("N0")) : null;
			LI_Subscribers.ValueText = workshopInfo?.Subscribers >= 0 ? Locale.SubscribersCount.FormatPlural(workshopInfo.Subscribers, workshopInfo.Subscribers.ToString("N0")) : null;
			LI_Votes.ValueColor = workshopInfo?.HasVoted == true ? FormDesign.Design.GreenColor : null;

			L_Author.Visible = workshopInfo is not null;
			L_Author.Text = workshopInfo?.Author?.Name;

			// Info
			{
				if (Package.GetWorkshopInfo()?.Description is string description && !string.IsNullOrWhiteSpace(description))
				{
					T_Info.Visible = true;

					if (IsHandleCreated)
					{
						slickWebBrowser.Body = Markdig.Markdown.ToHtml(description);
					}
				}
				else
				{
					T_Info.Visible = false;
				}
			}

			// Changelog
			{
				T_Changelog.Visible = workshopInfo?.Changelog?.Any() ?? false;

				if (T_Changelog.Visible)
				{
					packageChangelogControl1.SetChangelogs(workshopInfo!.Version ?? string.Empty, workshopInfo!.Changelog);
				}
			}

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

			// Images
			{
				var images = localData?.Images ?? workshopInfo?.Images ?? [];

				T_Gallery.Visible = images.Any();

				carouselControl.SetThumbnails(images);
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

			// Content
			{
				if (Package.GetLocalPackage() is ILocalPackageData p && p.Assets?.Length > 0)
				{
					LC_Items?.SetItems(p.Assets);

					T_Content.Visible = true;
				}
				else
				{
					LC_Items?.Clear();

					T_Content.Visible = false;
				}
			}

			// References
			{
				T_References.Visible = _compatibilityManager.GetPackagesThatReference(Package, _settings.UserSettings.ShowAllReferencedPackages).Any();
			}

			// Tags
			if (!Package.GetTags().SequenceEqual(FLP_Tags.Controls.OfType<TagControl>().SelectWhereNotNull(x => x.TagInfo)))
			{
				AddTags();
			}
		});
	}

	protected async Task<IEnumerable<IPackageIdentity>> GetItems()
	{
		return await Task.FromResult(_compatibilityManager.GetPackagesThatReference(Package, _settings.UserSettings.ShowAllReferencedPackages));
	}

	protected async Task SetIncluded(IEnumerable<IPackageIdentity> filteredItems, bool included)
	{
		await ServiceCenter.Get<IPackageUtil>().SetIncluded(filteredItems, included);
	}

	protected async Task SetEnabled(IEnumerable<IPackageIdentity> filteredItems, bool enabled)
	{
		await ServiceCenter.Get<IPackageUtil>().SetEnabled(filteredItems, enabled);
	}

	protected LocaleHelper.Translation GetItemText()
	{
		return Locale.Package;
	}

	protected string GetCountText()
	{
		int packagesIncluded = 0, modsIncluded = 0, modsEnabled = 0;

		foreach (var item in LC_References!.Items)
		{
			if (item?.IsIncluded() == true)
			{
				packagesIncluded++;

				if (item.GetPackage()?.IsCodeMod == true)
				{
					modsIncluded++;

					if (item.IsEnabled())
					{
						modsEnabled++;
					}
				}
			}
		}

		var total = LC_References!.ItemCount;

		if (!_settings.UserSettings.AdvancedIncludeEnable)
		{
			return string.Format(Locale.PackageIncludedTotal, packagesIncluded, total);
		}

		if (modsIncluded == modsEnabled)
		{
			return string.Format(Locale.PackageIncludedAndEnabledTotal, packagesIncluded, total);
		}

		return string.Format(Locale.PackageIncludedEnabledTotal, packagesIncluded, modsIncluded, modsEnabled, total);
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

		P_Side.Width = (int)(260 * UI.FontScale);
		PB_Icon.Size = UI.Scale(new Size(72, 72), UI.FontScale);
		I_More.Size = UI.Scale(new Size(20, 28), UI.FontScale);
		TLP_Side.Padding = UI.Scale(new Padding(8, 0, 0, 0), UI.FontScale);
		TLP_TopInfo.Margin = B_Incl.Margin = _packageCompatibilityControl.Margin = slickSpacer1.Margin = UI.Scale(new Padding(5), UI.FontScale);
		slickSpacer1.Height = (int)UI.FontScale;
		TLP_ModInfo.Padding = TLP_ModRequirements.Padding = TLP_Tags.Padding = TLP_Links.Padding =
		TLP_ModInfo.Margin = TLP_ModRequirements.Margin = TLP_Tags.Margin = TLP_Links.Margin = UI.Scale(new Padding(5), UI.FontScale);
		L_Info.Font = L_Requirements.Font = L_Tags.Font = L_Links.Font = UI.Font(7F, FontStyle.Bold);
		L_Info.Margin = L_Requirements.Margin = L_Tags.Margin = L_Links.Margin = UI.Scale(new Padding(3), UI.FontScale);
		L_Author.Margin = L_Title.Margin = UI.Scale(new Padding(5, 0, 0, 0), UI.FontScale);
		slickTabControl.Padding = UI.Scale(new Padding(5, 5, 0, 0), UI.FontScale);

		TLP_TopInfo.Height = (int)(72 * UI.FontScale);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		L_Info.ForeColor = L_Requirements.ForeColor = L_Tags.ForeColor = L_Links.ForeColor = design.LabelColor;
		TLP_ModInfo.BackColor = TLP_ModRequirements.BackColor = TLP_Tags.BackColor = TLP_Links.BackColor = design.BackColor.Tint(Lum: design.IsDarkTheme ? 6 : -5);
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		// Info
		{
			if (Package.GetWorkshopInfo()?.Description is string description && !string.IsNullOrWhiteSpace(description))
			{
				T_Info.Visible = true;

				if (IsHandleCreated)
				{
					slickWebBrowser.Body = Markdig.Markdown.ToHtml(description);
				}
			}
			else
			{
				T_Info.Visible = false;
			}
		}
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

	private async void T_References_TabSelected(object sender, EventArgs e)
	{
		await LC_References.RefreshItems();
	}
}
