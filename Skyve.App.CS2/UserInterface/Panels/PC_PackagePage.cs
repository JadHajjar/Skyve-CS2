using Skyve.App.CS2.UserInterface.Content;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.CompatibilityReport;
using Skyve.App.UserInterface.Lists;
using Skyve.App.UserInterface.Panels;
using Skyve.App.Utilities;
using Skyve.Compatibility.Domain.Enums;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain.Systems;

using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PackagePage : PC_PackagePageBase
{
	private readonly ItemListControl LC_Items;
	private readonly ContentList LC_References;
	private readonly ICompatibilityManager _compatibilityManager;
	private readonly ISettings _settings;
	private readonly PackageCompatibilityControl _packageCompatibilityControl;

	public PC_PackagePage(IPackageIdentity package, bool compatibilityPage = false) : base(package)
	{
		ServiceCenter.Get(out _compatibilityManager, out _settings, out IImageService imageService);

		InitializeComponent();

		T_References.LinkedControl = LC_References;
		T_Content.LinkedControl = LC_Items;
		T_Compatibility.LinkedControl = new PackageCompatibilityReportControl(package);
		T_Playsets.LinkedControl = new OtherPlaysetPackage(package);
		commentsControl1.Package = package;

		T_Playsets.Visible = !package.IsLocal();

		if (compatibilityPage)
		{
			T_Compatibility.PreSelected = true;
		}

		Controls.Add(_packageCompatibilityControl = new(package) { Dock = DockStyle.Top });

		if (_settings.UserSettings.ComplexListUI)
		{
			LC_Items = new ItemListControl.Complex(SkyvePage.SinglePackage) { IsPackagePage = true };
		}
		else
		{
			LC_Items = new ItemListControl.Simple(SkyvePage.SinglePackage) { IsPackagePage = true };
		}

		LC_References = new ContentList(SkyvePage.SinglePackage, true, GetItems, GetItemText);
		LC_References.TB_Search.Placeholder = "SearchGenericPackages";

		T_References.LinkedControl = LC_References;
		T_Content.LinkedControl = LC_Items;
		T_Compatibility.LinkedControl = new PackageCompatibilityReportControl(package);
		T_Playsets.LinkedControl = new OtherPlaysetPackage(package);

		T_Playsets.Visible = !package.IsLocal();

		if (compatibilityPage)
		{
			T_Compatibility.PreSelected = true;
		}

		SetPackage(package);
	}

	protected override async void SetPackage(IPackageIdentity package)
	{
		base.SetPackage(package);

		LC_Items.Invalidate();

		var workshopInfo = Package.GetWorkshopInfo();
		var localData = Package.GetLocalPackage();

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

		// Images
		{
			var images = localData?.Images ?? workshopInfo?.Images ?? [];

			T_Gallery.Visible = images.Any();

			carouselControl.SetThumbnails(images);
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

	protected override void UIChanged()
	{
		base.UIChanged();

		_packageCompatibilityControl.Margin = UI.Scale(new Padding(5), UI.FontScale);
		slickTabControl.Padding = UI.Scale(new Padding(5, 5, 0, 0), UI.FontScale);
	}

	protected async Task<IEnumerable<IPackageIdentity>> GetItems(CancellationToken cancellationToken)
	{
		return await Task.FromResult(_compatibilityManager.GetPackagesThatReference(Package, _settings.UserSettings.ShowAllReferencedPackages));
	}

	protected LocaleHelper.Translation GetItemText()
	{
		return Locale.Package;
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

	private async void T_References_TabSelected(object sender, EventArgs e)
	{
		await LC_References.RefreshItems();
	}

	private void T_Compatibility_Paint(object sender, PaintEventArgs e)
	{
		var compatibility = Package.GetCompatibilityInfo()?.GetNotification();

		if (compatibility > NotificationType.Info)
		{
			using var brush = new SolidBrush(compatibility.Value.GetColor());

			var rect = T_Compatibility.ClientRectangle.CenterR(UI.Scale(new Size(8, 8), UI.FontScale));

			rect.X += (int)(6 * UI.FontScale);
			rect.Y -= (int)(14 * UI.FontScale);

			e.Graphics.FillEllipse(brush, rect);
		}
	}
}
