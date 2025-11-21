using PDX.SDK.Contracts.Service.Mods.Enums;
using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.App.CS2.UserInterface.Content;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Forms;
using Skyve.App.UserInterface.Panels;
using Skyve.Compatibility.Domain;
using Skyve.Domain.CS2.Paradox;
using Skyve.Systems.CS2.Services;

using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_PackageEdit : PanelContent
{
	protected readonly PackageTitleControl L_Title;
	private readonly IWorkshopService _workshopService;
	private readonly IModStagingDataBuilder _staging;

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
		ServiceCenter.Get(out _workshopService, out IImageService imageService);

		InitializeComponent();

		Package = package;
		_staging = staging;

		ioSelectionDialog.ValidExtensions = [".png", ".jpg", ".jpeg"];
		screenshotEditControl.IOSelectionDialog = ioSelectionDialog;

		TLP_TopInfo.Controls.Add(L_Title = new(package) { Dock = DockStyle.Fill });
		L_Title.MouseClick += I_More_MouseClick;

		SetPackage(package, staging);
	}

	protected virtual void SetPackage(IPackageIdentity package, IModStagingDataBuilder staging)
	{
		Package = package;

		PB_Icon.Package = Package;
		L_Title.Package = Package;

		PB_Icon.Invalidate();
		L_Title.Invalidate();

		var workshopInfo = Package.GetWorkshopInfo();

		L_Author.Visible = workshopInfo is not null;
		L_Author.Author = workshopInfo?.Author;

		TB_Title.Text = staging.MetaData.DisplayName;
		TB_ShortDesc.Text = staging.MetaData.ShortDescription.Replace("\n", "\r\n");
		TB_LongDesc.Text = staging.MetaData.LongDescription.Replace("\n", "\r\n");
		TB_UserVersion.Text = staging.MetaData.UserModVersion;
		TB_UserVersion.Enabled = !string.IsNullOrEmpty(staging.MetaData.UserModVersion);
		TB_GameVersion.Text = staging.MetaData.RecommendedGameVersion;
		TB_ForumLink.Text = staging.MetaData.ForumLinks?.FirstOrDefault();

		if (CrossIO.FileExists(staging.ThumbnailAbsolutePath))
		{
			using var thumb = Image.FromFile(staging.ThumbnailAbsolutePath);
			PB_Thumbnail.Image = new Bitmap(thumb);
		}

		screenshotEditControl.Screenshots = staging.ScreenshotsAbsolutePaths.Where(CrossIO.FileExists);

		SetLinks(staging.MetaData.ExternalLinks.Select(link => new ParadoxLink(link)));

		foreach (var item in staging.MetaData.Dependencies)
		{
			switch (item.Type)
			{
				case DependencyType.Dlc:
					var dlc = DD_Dlcs.Items.FirstOrDefault(x => x.ModsDependencyId == item.DisplayName);

					if (dlc is not null)
					{
						DD_Dlcs.Select(dlc);
					}

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
		TLP_TopInfo.Margin = base_slickSpacer.Margin = UI.Scale(new Padding(5));
		base_slickSpacer.Height = (int)UI.FontScale;
		L_Author.Margin = L_Title.Margin = UI.Scale(new Padding(5, 0, 0, 0));
		L_Author.Font = UI.Font(9.5F);
		foreach (Control item in tableLayoutPanel1.Controls)
		{
			item.Margin = UI.Scale(item is SlickButton ? new Padding(0, 5, 5, 5) : new Padding(5));
		}

		TB_ShortDesc.Height = UI.Scale(80);
		tableLayoutPanel1.Padding = tableLayoutPanel2.Padding = tableLayoutPanel3.Padding = UI.Scale(new Padding(5));
		TLP_Versions.Margin = TLP_Dependencies.Margin = TLP_Links.Margin = UI.Scale(new Padding(0, 5, 5, 5));
		slickTabControl1.Padding = UI.Scale(new Padding(5, 5, 0, 0));
		TLP_Versions.MaximumSize = TLP_Dependencies.MaximumSize = TLP_Links.MaximumSize = TB_Title.MaximumSize = TB_ShortDesc.MaximumSize = new Size(UI.Scale(600), 9999);
		slickSpacer2.Margin = slickSpacer3.Margin = L_NoLinks.Margin = L_NoPackages.Margin = DD_Dlcs.Margin = TB_ForumLink.Margin = UI.Scale(new Padding(5));
		slickSpacer2.Height = slickSpacer3.Height = UI.Scale(1);
		I_AddLinks.Size = I_AddPackages.Size = I_CopyLinks.Size = I_CopyPackages.Size = I_PasteLinks.Size = I_PastePackages.Size = UI.Scale(new Size(24, 24));
		I_AddLinks.Padding = I_AddPackages.Padding = I_CopyLinks.Padding = I_CopyPackages.Padding = I_PasteLinks.Padding = I_PastePackages.Padding = UI.Scale(new Padding(4));
		PB_Thumbnail.Size = UI.Scale(new Size(200, 200));
		B_Publish.Font = UI.Font(11.25f);

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

		Form.PushPanel(form);
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

	private void PB_Thumbnail_Paint(object sender, PaintEventArgs e)
	{
		if (PB_Thumbnail.Image != null)
		{
			e.Graphics.SetUp(PB_Thumbnail.BackColor);
			e.Graphics.DrawRoundedImage(PB_Thumbnail.Image, PB_Thumbnail.ClientRectangle.Pad(1), UI.Scale(10), PB_Thumbnail.BackColor);
		}

		if (PB_Thumbnail.HoverState.HasFlag(HoverState.Hovered))
		{
			var rect = PB_Thumbnail.ClientRectangle.Pad(UI.Scale(16)).Align(UI.Scale(new Size(48, 48)), ContentAlignment.TopRight);
			var hovered = rect.Contains(PB_Thumbnail.PointToClient(Cursor.Position));
			var pressed = hovered && PB_Thumbnail.HoverState.HasFlag(HoverState.Pressed);
			using var darkBrush = new SolidBrush(Color.FromArgb(100, FormDesign.Design.ForeColor));
			using var brush = new SolidBrush(Color.FromArgb(hovered ? 240 : 185, pressed ? FormDesign.Design.ActiveColor : FormDesign.Design.BackColor));
			using var icon = IconManager.GetIcon("Edit", rect.Height * 2 / 3).Color(pressed ? FormDesign.Design.ActiveForeColor : FormDesign.Design.ForeColor);

			e.Graphics.FillRoundedRectangle(darkBrush, PB_Thumbnail.ClientRectangle.Pad(1), UI.Scale(10));
			e.Graphics.FillEllipse(brush, rect);
			e.Graphics.DrawImage(icon, rect.CenterR(icon.Size));
		}
	}

	private void PB_Thumbnail_MouseMove(object sender, MouseEventArgs e)
	{
		var rect = PB_Thumbnail.ClientRectangle.Pad(UI.Scale(16)).Align(UI.Scale(new Size(48, 48)), ContentAlignment.TopRight);

		PB_Thumbnail.Cursor = rect.Contains(e.Location) ? Cursors.Hand : Cursors.Default;
		PB_Thumbnail.Invalidate();
	}

	private void PB_Thumbnail_MouseClick(object sender, MouseEventArgs e)
	{
		var rect = PB_Thumbnail.ClientRectangle.Pad(UI.Scale(16)).Align(UI.Scale(new Size(48, 48)), ContentAlignment.TopRight);

		if (e.Button != MouseButtons.Left || !rect.Contains(e.Location))
		{
			return;
		}

		ioSelectionDialog.Title = "Select a thumbnail picture";

		if (ioSelectionDialog.PromptFile(Form) == DialogResult.OK)
		{
			try
			{
				using (var thumb = Image.FromFile(ioSelectionDialog.SelectedPath))
				{
					PB_Thumbnail.Image = new Bitmap(thumb);
				}

				_staging.WithThumbnail(ioSelectionDialog.SelectedPath);
			}
			catch { }
		}
	}

	private void B_AddScreenshot_Click(object sender, EventArgs e)
	{
		if (screenshotEditControl.Screenshots.Count() >= 10)
		{
			return;
		}

		ioSelectionDialog.Title = "Select a screenshot picture";

		if (ioSelectionDialog.PromptFile(Form) == DialogResult.OK)
		{
			try
			{
				screenshotEditControl.Screenshots = screenshotEditControl.Screenshots.Append(ioSelectionDialog.SelectedPath);
			}
			catch { }
		}
	}

	private async void B_Publish_Click(object sender, EventArgs e)
	{
		B_Publish.Loading = true;

		_staging
		  .WithScreenshots(screenshotEditControl.Screenshots)
		  .WithModMetaData(builder => builder
			.WithDisplayName(TB_Title.Text)
			.WithShortDescription(TB_ShortDesc.Text)
			.WithLongDescription(TB_LongDesc.Text)
			.WithUserModVersion(TB_UserVersion.Text)
			.WithRecommendedGameVersion(TB_GameVersion.Text)
			.WithForumLinks(TB_ForumLink.Text)
			.WithDependencies(DD_Dlcs.SelectedItems.Select(x => new ModDependency { Type = DependencyType.Dlc, DisplayName = x.ModsDependencyId })
			.Concat(P_ModDependencies.Controls.OfType<MiniPackageControl>().Select(x => new ModDependency { Type = DependencyType.Mod, Id = (int)x.Id }))));

		var result = await (_workshopService as WorkshopService)!.PublishMod(_staging);

		B_Publish.Loading = false;

		if (result?.Success ?? false)
		{
			ShowPrompt("Your mod has been updated successfully!", PromptButtons.OK, PromptIcons.Ok);

			PushBack();
		}
		else
		{
			ShowPrompt(result?.Error.ToString(), "Failed to update mod info", PromptButtons.OK, PromptIcons.Error);
		}
	}

	private void B_Header1_Click(object sender, EventArgs e)
	{
		Append("# ");
	}

	private void B_Header2_Click(object sender, EventArgs e)
	{
		Append("## ");
	}

	private void B_Header3_Click(object sender, EventArgs e)
	{
		Append("### ");
	}

	private void B_Bold_Click(object sender, EventArgs e)
	{
		var isBold = IsSurroundedBy(TB_LongDesc, "**");
		WrapSelection("**", isBold);
	}

	private void B_Italic_Click(object sender, EventArgs e)
	{
		var isItalic = IsSurroundedBy(TB_LongDesc, "*") && (!IsSurroundedBy(TB_LongDesc, "**") || IsSurroundedBy(TB_LongDesc, "***"));
		WrapSelection("*", isItalic);
	}

	private void B_Underline_Click(object sender, EventArgs e)
	{
		var isUnderline = IsSurroundedBy(TB_LongDesc, "__");
		WrapSelection("__", isUnderline);
	}

	private void B_List_Click(object sender, EventArgs e)
	{
		Append("* ");
	}

	private void WrapSelection(string text, bool remove)
	{
		var selectedText = TB_LongDesc.SelectedText;
		var firstPart = TB_LongDesc.Text.Substring(0, TB_LongDesc.SelectionStart - (remove ? text.Length : 0));
		var secondPart = TB_LongDesc.Text.Substring(TB_LongDesc.SelectionStart + TB_LongDesc.SelectionLength + (remove ? text.Length : 0));

		if (selectedText.LastOrDefault() is ' ')
		{
			selectedText = selectedText.Substring(0, selectedText.Length - 1);
			secondPart = ' ' + secondPart;
		}

		if (remove)
		{
			text = string.Empty;
		}

		var addedText = $"{text}{selectedText}{text}";

		TB_LongDesc.Text = firstPart + addedText + secondPart;
		TB_LongDesc.Focus();

		this.TryBeginInvoke(() =>
		{
			TB_LongDesc.Select(firstPart.Length + text.Length, addedText.Length - (text.Length * 2));
		});
	}

	private void Append(string text)
	{
		var tb = TB_LongDesc;
		var caretIndex = tb.SelectionStart;
		var focus = text.Length;

		var lastNewlineIndex = tb.Text.LastIndexOf('\n', Math.Max(0, caretIndex - 1));
		var lineStart = lastNewlineIndex == -1 ? 0 : lastNewlineIndex + 1;
		var nextNewlineIndex = tb.Text.IndexOf('\n', caretIndex);
		var lineEnd = nextNewlineIndex == -1 ? tb.Text.Length : nextNewlineIndex;
		var line = tb.Text.Substring(lineStart, lineEnd - lineStart);

		string newLine;
		if (line.StartsWith(text))
		{
			newLine = line.Substring(text.Length);
			focus = 0;
		}
		else
		{
			newLine = text + line;
		}

		tb.Text = tb.Text.Substring(0, lineStart) + newLine + tb.Text.Substring(lineEnd);
		tb.Focus();

		this.TryBeginInvoke(() =>
		{
			tb.Select(lineStart + focus, 0);
		});
	}

	private void SelectedTextTimer_Tick(object sender, EventArgs e)
	{
		var tb = TB_LongDesc;

		if (!tb.Focused)
		{
			return;
		}

		var caretIndex = tb.SelectionStart;

		var lastNewlineIndex = tb.Text.LastIndexOf('\n', Math.Max(0, caretIndex - 1));
		var lineStart = lastNewlineIndex == -1 ? 0 : lastNewlineIndex + 1;
		var nextNewlineIndex = tb.Text.IndexOf('\n', caretIndex);
		var lineEnd = nextNewlineIndex == -1 ? tb.Text.Length : nextNewlineIndex;
		var line = tb.Text.Substring(lineStart, lineEnd - lineStart);

		B_Header1.ButtonType = line.StartsWith("# ") ? ButtonType.Active : ButtonType.Normal;
		B_Header2.ButtonType = line.StartsWith("## ") ? ButtonType.Active : ButtonType.Normal;
		B_Header3.ButtonType = line.StartsWith("### ") ? ButtonType.Active : ButtonType.Normal;
		B_List.ButtonType = line.StartsWith("* ") ? ButtonType.Active : ButtonType.Normal;

		var isBold = IsSurroundedBy(tb, "**");
		var isItalic = IsSurroundedBy(TB_LongDesc, "*") && (!IsSurroundedBy(TB_LongDesc, "**") || IsSurroundedBy(TB_LongDesc, "***"));
		var isUnderline = IsSurroundedBy(tb, "__");

		B_Bold.ButtonType = isBold ? ButtonType.Active : ButtonType.Normal;
		B_Italic.ButtonType = isItalic ? ButtonType.Active : ButtonType.Normal;
		B_Underline.ButtonType = isUnderline ? ButtonType.Active : ButtonType.Normal;

		B_Header1.Invalidate();
		B_Header2.Invalidate();
		B_Header3.Invalidate();
		B_List.Invalidate();
		B_Bold.Invalidate();
		B_Italic.Invalidate();
		B_Underline.Invalidate();
	}

	private bool IsSurroundedBy(SlickTextBox tb, string marker)
	{
		var selStart = tb.SelectionStart;
		var selEnd = selStart + tb.SelectionLength;
		var text = tb.Text;
		var markerLen = marker.Length;

		if (tb.SelectionLength > 0)
		{
			if (selStart >= markerLen && selEnd + markerLen <= text.Length)
			{
				var before = text.Substring(selStart - markerLen, markerLen);
				var after = text.Substring(selEnd, Math.Min(markerLen, text.Length - selEnd));
				if (before == marker && after == marker)
				{
					return true;
				}
			}
		}
		else
		{
			var firstPart = tb.Text.Substring(0, selStart);
			var secondPart = tb.Text.Substring(selEnd);

			if (firstPart.EndsWith(marker) &&secondPart.StartsWith(marker))
			{
				return true;
			}
		}

		return false;
	}
}
