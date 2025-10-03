using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.App.CS2.UserInterface.Content;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Forms;
using Skyve.App.UserInterface.Panels;
using Skyve.Compatibility.Domain;
using Skyve.Domain.CS2.Paradox;

using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PackageEdit : PanelContent
{
	protected readonly IncludedButton B_Incl;
	protected readonly PackageTitleControl L_Title;
	private readonly INotifier _notifier;
	private readonly IPackageUtil _packageUtil;
	private readonly ISettings _settings;
	private readonly IWorkshopService _workshopService;
	private readonly IModStagingDataBuilder _staging;
	private readonly TagControl? addTagControl;

	public IPackageIdentity Package { get; private set; }

#nullable disable
	[Obsolete("DESIGNER ONLY", true)]
	public PC_PackageEdit()
	{
		InitializeComponent();
	}
#nullable enable

	public PC_PackageEdit(IPackageIdentity package, IModStagingDataBuilder staging)
	{
		ServiceCenter.Get(out _notifier, out _packageUtil, out _settings, out _workshopService, out IImageService imageService);

		InitializeComponent();

		Package = package;
		_staging = staging;

		TLP_Side.Controls.Add(B_Incl = new(package) { Dock = DockStyle.Top }, 0, 2);
		TLP_TopInfo.Controls.Add(L_Title = new(package) { Dock = DockStyle.Fill });
		L_Title.MouseClick += I_More_MouseClick;

		SetPackage(package, staging);
	}

	protected virtual void SetPackage(IPackageIdentity package, IModStagingDataBuilder staging)
	{
		Package = package;

		PB_Icon.Package = Package;
		B_Incl.Package = Package;
		L_Title.Package = Package;

		PB_Icon.Invalidate();
		B_Incl.Invalidate();
		L_Title.Invalidate();

		var workshopInfo = Package.GetWorkshopInfo();

		L_Author.Visible = workshopInfo is not null;
		L_Author.Author = workshopInfo?.Author;

		TB_Title.Text = staging.MetaData.DisplayName;
		TB_ShortDesc.Text = staging.MetaData.ShortDescription.Replace("\n", "\r\n");
		TB_LongDesc.Text = staging.MetaData.LongDescription.Replace("\n", "\r\n");
		TB_UserVersion.Text = staging.MetaData.UserModVersion;
		TB_UserVersion.Enabled = !string.IsNullOrEmpty(staging.MetaData.UserModVersion);
		TB_GameVersion.Text= staging.MetaData.RecommendedGameVersion;
		TB_ForumLink.Text = staging.MetaData.ForumLinks?.FirstOrDefault();

		SetLinks(staging.MetaData.ExternalLinks.Select(link => new ParadoxLink( link)));

		foreach (var item in staging.MetaData.Dependencies)
		{
			switch (item.Type)
			{
				case DependencyType.Dlc:
					DD_Dlcs.Select(DD_Dlcs.Items.FirstOrDefault(x => x.ModsDependencyId == item.DisplayName));
					break;
				case DependencyType.Mod:
					var control = new MiniPackageControl(new GenericPackageIdentity((ulong)item.Id!, item.DisplayName)) { Dock = DockStyle.Top };
					P_ModDependencies.Controls.Add(control);
					break;
			}
		}
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
		L_Author.Margin = L_Title.Margin = UI.Scale(new Padding(5, 0, 0, 0));
		L_Author.Font = UI.Font(9.5F);
		TB_Title.Margin = TB_ShortDesc.Margin = TB_LongDesc.Margin = UI.Scale(new Padding(5));
		TB_ShortDesc.Height = UI.Scale(80);
		tableLayoutPanel1.Padding = tableLayoutPanel2.Padding = tableLayoutPanel3.Padding = UI.Scale(new Padding(5));
		TLP_Versions.Margin = TLP_Dependencies.Margin = TLP_Links.Margin = UI.Scale(new Padding(0, 5, 5, 5));
		slickTabControl1.Padding = UI.Scale(new Padding(5, 5, 0, 0));
		TLP_Versions.MaximumSize = TLP_Dependencies.MaximumSize = TLP_Links.MaximumSize =TB_Title.MaximumSize=TB_ShortDesc.MaximumSize= new Size(UI.Scale(600), 9999);
		slickSpacer2.Margin = slickSpacer3.Margin = L_NoLinks.Margin = L_NoPackages.Margin = DD_Dlcs.Margin = TB_ForumLink.Margin = UI.Scale(new Padding(5));
		slickSpacer2.Height = slickSpacer3.Height = UI.Scale(1);
		I_AddLinks.Size = I_AddPackages.Size = I_CopyLinks.Size = I_CopyPackages.Size = I_PasteLinks.Size = I_PastePackages.Size = UI.Scale(new Size(24, 24));
		I_AddLinks.Padding = I_AddPackages.Padding = I_CopyLinks.Padding = I_CopyPackages.Padding = I_PasteLinks.Padding = I_PastePackages.Padding = UI.Scale(new Padding(4));

		TLP_TopInfo.Height = UI.Scale(72);
	}

	protected override void LocaleChanged()
	{
		base.LocaleChanged();

		T_Images.Text = LocaleSlickUI.Image.Plural;
		L_NoLinks.Text = LocaleCR.NoLinks;
		L_NoPackages.Text = LocaleCR.NoPackages;
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);
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

	private void L_Author_Click(object sender, EventArgs e)
	{
		var workshopInfo = Package.GetWorkshopInfo();

		if (workshopInfo?.Author != null)
		{
			App.Program.MainForm.PushPanel(new PC_UserPage(workshopInfo.Author));
		}
	}

	private void P_ModDependencies_ControlRemoved(object sender, ControlEventArgs e)
	{
		L_NoPackages.Visible = P_ModDependencies.Controls.Count == 0;

		foreach (var item in P_ModDependencies.Controls.OfType<MiniPackageControl>())
		{
			item.Large = P_ModDependencies.Controls.Count < 5;
			item.RefreshHeight();
		}
	}

	private void I_AddLinks_Click(object sender, EventArgs e)
	{
		var form = new AddLinkForm(P_Links.Controls.OfType<LinkControl>().ToList(x => x.Link));

		form.Show(Form);

		form.LinksReturned += SetLinks;
	}

	private void SetLinks(IEnumerable<ILink> links)
	{
		P_Links.Controls.Clear(true);

		foreach (var item in links.OrderBy(x => x.Type))
		{
			var control = new LinkControl(item, true);
			control.Click += I_AddLinks_Click;
			P_Links.Controls.Add(control);
		}

		L_NoLinks.Visible = P_Links.Controls.Count == 0;
	}

	private void I_CopyLinks_Click(object sender, EventArgs e)
	{
		if (P_Links.Controls.Count > 0)
		{
			Clipboard.SetText(P_Links.Controls.OfType<LinkControl>().Select(x => $"{x.Link.Type}:{x.Link.Url}").ListStrings(","));
		}
	}

	private void I_PasteLinks_Click(object sender, EventArgs e)
	{
		if (!Clipboard.ContainsText())
		{
			return;
		}

		var matches = Clipboard.GetText().Split(',').Select(x => Regex.Match(x, @"(\w+?):.+"));

		foreach (var item in matches)
		{
			if (item.Success && Enum.TryParse(item.Groups[1].Value, out LinkType linkType))
			{
				var control = new LinkControl(new PackageLink { Type = linkType, Url = item.Groups[2].Value }, true);
				control.Click += I_AddLinks_Click;
				P_Links.Controls.Add(control);
			}
		}
	}

	private void I_CopyPackages_Click(object sender, EventArgs e)
	{
		if (P_ModDependencies.Controls.Count > 0)
		{
			Clipboard.SetText(P_ModDependencies.Controls.OfType<MiniPackageControl>().Select(x => x.Id).ListStrings(","));
		}
	}

	private void I_PastePackages_Click(object sender, EventArgs e)
	{
		if (!Clipboard.ContainsText())
		{
			return;
		}

		var matches = Regex.Matches(Clipboard.GetText(), "(\\d{5,6})");

		foreach (Match item in matches)
		{
			if (ulong.TryParse(item.Value, out var id))
			{
				if (!P_ModDependencies.Controls.OfType<MiniPackageControl>().Any(x => x.Id == id))
				{
					P_ModDependencies.Controls.Add(new MiniPackageControl(id) { Dock = DockStyle.Top });
				}
			}
		}
	}

	private void I_AddPackages_Click(object sender, EventArgs e)
	{
		var form = new PC_WorkshopPackageSelection(P_ModDependencies.Controls.OfType<MiniPackageControl>().Select(x => x.Id));

		form.PackageSelected += Form_PackageSelected;

		App.Program.MainForm.PushPanel(form);
	}

	private void Form_PackageSelected(IEnumerable<ulong> packages)
	{
		foreach (var item in packages)
		{
			if (!P_ModDependencies.Controls.OfType<MiniPackageControl>().Any(x => x.Id == item))
			{
				P_ModDependencies.Controls.Add(new MiniPackageControl(item) { Dock = DockStyle.Top });
			}
		}
	}
}
