using Skyve.App.CS2.UserInterface.Content;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.CompatibilityReport;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Forms;
using Skyve.App.UserInterface.Lists;
using Skyve.App.UserInterface.Panels;
using Skyve.App.Utilities;
using Skyve.Compatibility.Domain.Interfaces;

using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PackagePage : PanelContent
{
	private readonly ItemListControl? LC_Items;
	private readonly ContentList? LC_References;
	private readonly IncludedButton B_Incl;
	private TagControl? addTagControl;
	private readonly PackageTitleControl L_Title;
	private readonly INotifier _notifier;
	private readonly ICompatibilityManager _compatibilityManager;
	private readonly IPackageUtil _packageUtil;
	private readonly ISettings _settings;
	private readonly PackageCompatibilityControl packageCompatibilityControl;

	public IPackageIdentity Package { get; }

	public PC_PackagePage(IPackageIdentity package, bool compatibilityPage = false)
	{
		ServiceCenter.Get(out _notifier, out _compatibilityManager, out _packageUtil, out _settings, out IImageService imageService);

		InitializeComponent();

		Package = package;

		PB_Icon.Package = package;

		if (package.GetWorkshopInfo() is IWorkshopInfo workshopInfo)
		{
			if (workshopInfo.GetThumbnail(imageService, out var thumbnail, out var thumbnailUrl) && thumbnail is not null)
			{
				PB_Icon.Image = new Bitmap(thumbnail);
			}
			else
			{
				PB_Icon.LoadImage(thumbnailUrl, imageService.GetImage);
			}
		}

		Controls.Add(packageCompatibilityControl = new(Package) { Dock = DockStyle.Top });
		TLP_Side.Controls.Add(B_Incl = new(Package) { Dock = DockStyle.Top }, 0, 2);
		TLP_TopInfo.Controls.Add(L_Title = new(Package) { Dock = DockStyle.Fill });

		L_Title.MouseClick += I_More_MouseClick;
		packageCompatibilityControl.CompatibilityInfoClicked += () => T_Compatibility.Selected = true;

		//P_Info.SetPackage(package);

		T_Compatibility.LinkedControl = new PackageCompatibilityReportControl(package);

		var crdata = Package.GetPackageInfo();
		var crAvailable = crdata is not null;

		//if (!crAvailable)
		//{
		//	TLP_Info.ColumnStyles[1].Width = 0;
		//}

		if (Package.GetLocalPackage() is ILocalPackageData p && p.Assets is not null && p.Assets.Length > 0)
		{
			if (_settings.UserSettings.ComplexListUI)
			{
				LC_Items = new ItemListControl.Complex(SkyvePage.SinglePackage) { Dock = DockStyle.Fill, IsPackagePage = true };
			}
			else
			{
				LC_Items = new ItemListControl.Simple(SkyvePage.SinglePackage) { Dock = DockStyle.Fill, IsPackagePage = true };
			}

			LC_Items.AddRange(p.Assets);

			//P_List.Controls.Add(LC_Items);
		}
		else if (crAvailable)
		{
			//TLP_Info.ColumnStyles[0].Width = 0;
		}
		else
		{
			slickTabControl.RemoveTab(T_Info);
			T_Compatibility.PreSelected = true;
		}

		if (compatibilityPage)
		{
			T_Compatibility.PreSelected = true;
		}

		if (crAvailable)
		{
			//foreach (var item in crdata?.Links ?? [])
			//{
			//	FLP_Links.Controls.Add(new LinkControl(item, true));
			//}

			//label5.Visible = FLP_Links.Visible = FLP_Links.Controls.Count > 0;

			AddTags();
		}

		if (GetItems().Result.Any())
		{
			LC_References = new ContentList(SkyvePage.SinglePackage, true, GetItems, SetIncluded, SetEnabled, GetItemText, GetCountText)
			{
				Dock = DockStyle.Fill
			};

			LC_References.TB_Search.Placeholder = "SearchGenericPackages";

			LC_References.RefreshItems();

			T_References.LinkedControl = LC_References;
		}
		else
		{
			slickTabControl.RemoveTab(T_References);
		}

		var pc = new OtherPlaysetPackage(package)
		{
			Dock = DockStyle.Fill
		};

		T_Playsets.FillTab = true;
		T_Playsets.LinkedControl = pc;

		_notifier.WorkshopInfoUpdated += Notifier_WorkshopInfoUpdated;

		Notifier_WorkshopInfoUpdated();
	}

	private void Notifier_WorkshopInfoUpdated()
	{
		this.TryInvoke(() =>
	{
		var workshopInfo = Package.GetWorkshopInfo();
		var localData = Package.GetLocalPackage();

		var date = workshopInfo is null || workshopInfo.ServerTime == default ? (localData?.LocalTime ?? default) : workshopInfo.ServerTime;

		//P_Info.Invalidate();
		LC_Items?.Invalidate();

		LI_Version.ValueText = localData?.Version ?? workshopInfo?.Version;
		LI_UpdateTime.ValueText = _settings.UserSettings.ShowDatesRelatively ? date.ToRelatedString(true, false) : date.ToString("g");
		LI_ModId.ValueText = Package.Id.ToString();
		LI_Size.ValueText = localData?.FileSize.SizeString(0) ?? workshopInfo?.ServerSize.SizeString(0);
		LI_Votes.ValueText = workshopInfo is not null ? Locale.VotesCount.FormatPlural(workshopInfo.VoteCount, workshopInfo.VoteCount.ToString("N0")) : null;
		LI_Subscribers.ValueText = workshopInfo is not null ? Locale.SubscribersCount.FormatPlural(workshopInfo.Subscribers, workshopInfo.Subscribers.ToString("N0")) : null;

		LI_Votes.ValueColor = workshopInfo?.HasVoted == true ? FormDesign.Design.GreenColor : null;

		L_Author.Visible = workshopInfo is not null;
		L_Author.Text = workshopInfo?.Author?.Name;

		var requirements = workshopInfo?.Requirements.ToList() ?? [];
		
		if (requirements.Count > 0)
		{
			P_Requirements.Controls.Clear(true);

			foreach (var requirement in requirements)
			{
				var control = new MiniPackageControl(requirement.Id) { ReadOnly = true, Large = true, ShowIncluded = true, Dock = DockStyle.Top };
				P_Requirements.Controls.Add(control);
			}
		}
		else
		{
			TLP_ModRequirements.Visible = false;
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
		var frm = EditTags(new[] { Package });

		frm.FormClosed += (_, _) =>
		{
			if (frm.DialogResult == DialogResult.OK)
			{
				AddTags();
			}
		};
	}

	private static EditTagsForm EditTags(IEnumerable<IPackageIdentity> item)
	{
		var frm = new EditTagsForm(item);

		App.Program.MainForm.OnNextIdle(() =>
		{
			frm.Show(App.Program.MainForm);

			frm.ShowUp();
		});

		return frm;
	}

	private void AddTags()
	{
		FLP_Tags.Controls.Clear(true);

		foreach (var item in Package.GetTags())
		{
			var control = new TagControl { TagInfo = item, Display = true };
			control.MouseClick += TagControl_Click;
			FLP_Tags.Controls.Add(control);
		}

		//if (Package.LocalPackage is not null)
		{
			addTagControl = new TagControl { ImageName = "I_Add" };
			addTagControl.MouseClick += AddTagControl_MouseClick;
			FLP_Tags.Controls.Add(addTagControl);
		}
	}

	private void TagControl_Click(object sender, EventArgs e)
	{
		if (!(sender as TagControl)!.TagInfo!.IsCustom)
		{
			return;
		}

		(sender as TagControl)!.Dispose();

		ServiceCenter.Get<ITagsService>().SetTags(Package, FLP_Tags.Controls.OfType<TagControl>().Select(x => x.TagInfo!.IsCustom ? x.TagInfo.Value?.Replace(' ', '-') : null)!);

		_notifier.OnRefreshUI(true);
	}

	protected override void LocaleChanged()
	{
		L_Info.Text = Locale.Info.One.ToUpper();
		L_Requirements.Text = Locale.Dependency.Plural.ToUpper();
		L_Tags.Text = LocaleSlickUI.Tags.One.ToUpper();

		//var cr = Package.GetPackageInfo();

		//if (cr is null)
		//{
		//	return;
		//}
		//label1.Text = LocaleCR.Usage;
		//label2.Text = cr.Usage.GetValues().If(x => x.Count() == Enum.GetValues(typeof(PackageUsage)).Length, x => Locale.AnyUsage.One, x => x.ListStrings(x => LocaleCR.Get(x.ToString()), ", "));
		//label3.Text = LocaleCR.PackageType;
		//label4.Text = cr.Type == PackageType.GenericPackage ? (Package.GetPackage()?.IsCodeMod == true ? Locale.Mod : Locale.Asset) : LocaleCR.Get(cr.Type.ToString());
		//label5.Text = LocaleCR.Links;
		//label6.Text = LocaleSlickUI.Tags;
		//L_Requirements.Text = LocaleHelper.GetGlobalText("CRT_RequiredPackages");
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		P_Side.Width = (int)(260 * UI.FontScale);
		PB_Icon.Size = UI.Scale(new Size(72, 72), UI.FontScale);
		I_More.Size = UI.Scale(new Size(20, 28), UI.FontScale);
		TLP_Side.Padding = UI.Scale(new Padding(8, 0, 0, 0), UI.FontScale);
		TLP_TopInfo.Margin = B_Incl.Margin = packageCompatibilityControl.Margin = slickSpacer1.Margin = UI.Scale(new Padding(5), UI.FontScale);
		slickSpacer1.Height = (int)UI.FontScale;
		TLP_ModInfo.Padding = TLP_ModRequirements.Padding = TLP_Tags.Padding =
		TLP_ModInfo.Margin = TLP_ModRequirements.Margin = TLP_Tags.Margin = UI.Scale(new Padding(5), UI.FontScale);
		L_Requirements.Margin = L_Requirements.Margin = L_Tags.Margin = UI.Scale(new Padding(0, 0, 0, 6), UI.FontScale);
		L_Info.Font = L_Requirements.Font = L_Tags.Font = UI.Font(7F, FontStyle.Bold);
		L_Info.Margin = L_Requirements.Margin = L_Tags.Margin = UI.Scale(new Padding(3), UI.FontScale);
		L_Author.Margin = L_Title.Margin = UI.Scale(new Padding(5, 0, 0, 0), UI.FontScale);
		slickTabControl.Padding = UI.Scale(new Padding(5, 5, 0, 0), UI.FontScale);

		TLP_TopInfo.Height = (int)(72 * UI.FontScale);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		L_Info.ForeColor = L_Requirements.ForeColor = L_Tags.ForeColor = design.LabelColor;
		TLP_ModInfo.BackColor = TLP_ModRequirements.BackColor = TLP_Tags.BackColor = design.BackColor.Tint(Lum: design.IsDarkTheme ? 6 : -5);
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (Package.GetWorkshopInfo()?.Description is string description)
		{
			slickWebBrowser.Body = Markdig.Markdown.ToHtml(description);
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

	private void slickWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
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
}
