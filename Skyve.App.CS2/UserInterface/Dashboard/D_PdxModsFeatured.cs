using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Dashboard;
using Skyve.App.UserInterface.Lists;
using Skyve.App.UserInterface.Panels;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_PdxModsFeatured : IDashboardItem
{
	private readonly IWorkshopService _workshopService;
	private readonly IModLogicManager _modLogicManager;
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly IModUtil _modUtil;
	private List<IWorkshopInfo> recentlyUpdatedMods = [];
	private List<IWorkshopInfo> newMods = [];

	public D_PdxModsFeatured()
	{
		Loading = true;

		ServiceCenter.Get(out _workshopService, out _modLogicManager, out _modUtil, out _subscriptionsManager);

		if (_workshopService.IsAvailable)
		{
			LoadShowcase();
		}
		else
		{
			_workshopService.ContextAvailable += LoadShowcase;
		}
	}

	private void LoadShowcase()
	{
		new BackgroundAction(async () =>
		{
			try
			{
				newMods = (await _workshopService.QueryFilesAsync(WorkshopQuerySorting.DateCreated, limit: 8)).ToList();
				recentlyUpdatedMods = (await _workshopService.QueryFilesAsync(WorkshopQuerySorting.DateUpdated, limit: 16))
					.Where(x => !newMods.Any(y => y.Id == x.Id))
					.Take(8)
					.ToList();
			}
			catch { }

			Loading = false;

			this.TryInvoke(OnResizeRequested);
		}).Run();
	}

	private void RightClick(IWorkshopInfo package)
	{
		SlickToolStrip.Show(App.Program.MainForm, ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems(package));
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		if (Loading)
		{
			return DrawLoading;
		}

		if (recentlyUpdatedMods.Count == 0)
		{
			return DrawNone;
		}

		if (Width / UI.FontScale < 350)
		{
			return DrawSmall;
		}

		return DrawLarge;
	}

	private void DrawNone(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsShowcase, "PDXMods");

		e.Graphics.DrawStringItem(LocaleCS2.CouldNotRetrieveMods
			, Font
			, FormDesign.Design.ForeColor
			, e.ClipRectangle.Pad(Margin)
			, ref preferredHeight
			, applyDrawing);

		preferredHeight += Padding.Top / 2;
	}

	private void DrawLoading(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawLoadingSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsShowcase);
	}

	private void DrawSmall(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		Draw(e, applyDrawing, ref preferredHeight, true);
	}

	private void DrawLarge(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		Draw(e, applyDrawing, ref preferredHeight, false);
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight, bool horizontal)
	{
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsShowcase, "PDXMods");

		using var fontSmall = UI.Font(6.75F);

		var preferredSize = horizontal ? 350 : 100;
		var columns = (int)Math.Max(1, Math.Floor((e.ClipRectangle.Width - Margin.Left) / (preferredSize * UI.FontScale)));
		var columnWidth = (e.ClipRectangle.Width - Margin.Left) / columns;
		var height = horizontal ? (int)(32 * UI.FontScale) : (columnWidth * 5 / 3);

		e.Graphics.DrawStringItem(Locale.NewUploads, fontSmall, Color.FromArgb(180, FormDesign.Design.ForeColor), e.ClipRectangle.Pad(Margin).Pad((int)(2 * UI.FontScale), 0, 0, 0), ref preferredHeight, applyDrawing);

		preferredHeight -= Margin.Top / 2;

		for (var i = 0; i < Math.Min(newMods.Count, horizontal ? 6 : (columns < 5 ? (columns * 2) : columns)); i++)
		{
			if (i > 0 && (i % columns) == 0)
			{
				preferredHeight += height;
			}

			var rect = new Rectangle(e.ClipRectangle.X + (Margin.Left / 2) + (i % columns * columnWidth), preferredHeight, columnWidth, height);

			if (applyDrawing)
			{
				DrawMod(e, newMods[i], rect, horizontal);
			}
		}

		preferredHeight += Margin.Top + height;

		if (!horizontal)
		{
			preferredHeight += Margin.Top;
		}

		e.Graphics.DrawStringItem(Locale.RecentlyUpdated, fontSmall, Color.FromArgb(180, FormDesign.Design.ForeColor), e.ClipRectangle.Pad(Margin).Pad((int)(2 * UI.FontScale), 0, 0, 0), ref preferredHeight, applyDrawing);

		preferredHeight -= Margin.Top / 2;

		for (var i = 0; i < Math.Min(recentlyUpdatedMods.Count, horizontal ? 6 : (columns < 5 ? (columns * 2) : columns)); i++)
		{
			if (i > 0 && (i % columns) == 0)
			{
				preferredHeight += height;
			}

			var rect = new Rectangle(e.ClipRectangle.X + (Margin.Left / 2) + (i % columns * columnWidth), preferredHeight, columnWidth, height);

			if (applyDrawing)
			{
				DrawMod(e, recentlyUpdatedMods[i], rect, horizontal);
			}
		}

		preferredHeight += Margin.Top + height;

		if (!horizontal)
		{
			preferredHeight += Margin.Top;
		}
	}

	private void DrawMod(PaintEventArgs e, IWorkshopInfo workshopInfo, Rectangle rect, bool horizontal)
	{
		if (horizontal)
		{
			var banner = workshopInfo.GetThumbnail();
			var bannerRect = rect.Pad(Margin.Left / 2, 0, 0, 0).Align(new Size(rect.Height - (Margin.Left / 2), rect.Height - (Margin.Left / 2)), ContentAlignment.MiddleLeft);

			if (banner is null)
			{
				using var brush = new SolidBrush(Color.FromArgb(40, FormDesign.Design.ForeColor));

				e.Graphics.FillRoundedRectangle(brush, bannerRect, Margin.Left / 2);

				using var icon = IconManager.GetIcon("Paradox", bannerRect.Width * 3 / 4).Color(FormDesign.Design.ForeColor);

				e.Graphics.DrawImage(icon, bannerRect.CenterR(icon.Size));
			}
			else
			{
				e.Graphics.DrawRoundedImage(banner, bannerRect, (int)(5 * UI.FontScale));
			}

			if (HoverState.HasFlag(HoverState.Hovered) && rect.Contains(CursorLocation))
			{
				using var brush = new SolidBrush(Color.FromArgb(40, FormDesign.Design.ForeColor));

				e.Graphics.FillRoundedRectangle(brush, rect.Pad(Margin.Left / 4, 0, Margin.Left / 2, 0), Margin.Left / 2);
			}

			var text = workshopInfo.CleanName(out var tags) ?? Locale.UnknownPackage;
			var textRect = rect.Pad(Margin.Left + bannerRect.Width, Margin.Left / 4, Margin.Left / 2, Margin.Left / 4);
			using var textBrush = new SolidBrush(FormDesign.Design.ForeColor);
			using var fadedBrush = new SolidBrush(Color.FromArgb(180, FormDesign.Design.ForeColor));
			using var textFont = UI.Font(8.5F, FontStyle.Bold).FitTo(text, textRect, e.Graphics);
			using var smallFont = UI.Font(7F);
			using var format = new StringFormat { LineAlignment = StringAlignment.Far };

			e.Graphics.DrawString(text, textFont, textBrush, textRect);
			e.Graphics.DrawString(workshopInfo.Author?.Name ?? string.Empty, smallFont, fadedBrush, textRect, format);

			var tagRect = new Rectangle(textRect.X + (int)e.Graphics.Measure(text, Font).Width + (Margin.Left / 2), textRect.Y + (Margin.Top / 4), 0, textRect.Height);

			if (tags is not null)
			{
				foreach (var item in tags)
				{
					tagRect.X += (Margin.Left / 2) + e.Graphics.DrawLabel(item.Text, null, item.Color, tagRect, ContentAlignment.TopLeft, smaller: true).Width;
				}
			}

			_buttonActions[rect] = () => ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(workshopInfo);
			_buttonRightClickActions[rect] = () => RightClick(workshopInfo);
		}
		else
		{
			var pe = new ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles>(new DrawableItem<IPackageIdentity, ItemListControl.Rectangles>(workshopInfo) { Rectangles = GenerateGridRectangles(workshopInfo, rect.Pad(Margin.Left / 2)) }, e.Graphics, [e.ClipRectangle], rect.Pad(Margin.Left / 2), HoverState, false);
			var isEnabled = workshopInfo.IsEnabled();

			DrawThumbnail(pe);
			DrawTitleAndTags(pe);
			DrawAuthor(pe, workshopInfo);
			DrawVersionAndTags(pe, workshopInfo);
			DrawDots(pe);

			_buttonActions[pe.Rects.IconRect] = () => ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(workshopInfo);
			_buttonActions[pe.Rects.DotsRect] = () => RightClick(workshopInfo);
			_buttonActions[pe.Rects.AuthorRect] = () => App.Program.MainForm.PushPanel(null, new PC_UserPage(workshopInfo.Author!));
			_buttonRightClickActions[rect] = () => RightClick(workshopInfo);
		}
	}

	private ItemListControl.Rectangles GenerateGridRectangles(IPackageIdentity item, Rectangle rectangle)
	{
		var rects = new ItemListControl.Rectangles(item)
		{
			IconRect = rectangle.Align(new Size(rectangle.Width, rectangle.Width), ContentAlignment.TopCenter),
			DotsRect = new Rectangle(rectangle.X, rectangle.Y + rectangle.Width + (Margin.Top / 2), rectangle.Width, 0).Align(UI.Scale(new Size(16, 24), UI.FontScale), ContentAlignment.TopRight)
		};

		using var titleFont = UI.Font(10.5F, FontStyle.Bold);
		rects.TextRect = new Rectangle(rectangle.X, rectangle.Y + rectangle.Width + (Margin.Top / 2), rectangle.Width - Margin.Left - rects.DotsRect.Width, 0).AlignToFontSize(titleFont, ContentAlignment.TopLeft);
		rects.CenterRect = rects.TextRect.Pad(0, -Margin.Vertical, 0, 0);

		return rects;
	}

	private void DrawThumbnail(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e)
	{
		if (!e.InvalidRects.Any(x => x.IntersectsWith(e.Rects.IconRect)))
		{
			return;
		}

		var thumbnail = e.Item.GetThumbnail();

		if (thumbnail is null)
		{
			using var generic = IconManager.GetIcon("Paradox", e.Rects.IconRect.Height).Color(e.BackColor);
			using var brush = new SolidBrush(FormDesign.Design.IconColor);

			e.Graphics.FillRoundedRectangle(brush, e.Rects.IconRect, (int)(5 * UI.FontScale));
			e.Graphics.DrawImage(generic, e.Rects.IconRect.CenterR(generic.Size));
		}
		else if (e.Item.IsLocal())
		{
			using var unsatImg = new Bitmap(thumbnail, e.Rects.IconRect.Size).Tint(Sat: 0);

			drawThumbnail(unsatImg);
		}
		else
		{
			drawThumbnail(thumbnail);
		}

		if (e.HoverState.HasFlag(HoverState.Hovered) && e.Rects.IconRect.Contains(CursorLocation))
		{
			using var brush = new SolidBrush(Color.FromArgb(75, 255, 255, 255));
			e.Graphics.FillRoundedRectangle(brush, e.Rects.IconRect, (int)(5 * UI.FontScale));
		}

		void drawThumbnail(Bitmap generic) => e.Graphics.DrawRoundedImage(generic, e.Rects.IconRect, (int)(5 * UI.FontScale), FormDesign.Design.BackColor);
	}
	private void DrawTitleAndTags(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e)
	{
		var text = e.Item.CleanName(out var tags);
		using var stringFormat = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, LineAlignment = StringAlignment.Near };

		using var font = UI.Font(8.25F, FontStyle.Bold);
		var textRect = new Rectangle(e.Rects.TextRect.X, e.Rects.TextRect.Y, e.Rects.TextRect.Width, Height);

		var textSize = e.Graphics.Measure(text, font, textRect.Width);
		var oneLineSize = e.Graphics.Measure(text, font);
		var oneLine = textSize.Height == oneLineSize.Height;
		var tagRect = new Rectangle(e.Rects.TextRect.X + (oneLine ? (int)textSize.Width : 0), textRect.Y + (oneLine ? 0 : (int)textSize.Height), 0, (int)oneLineSize.Height);

		e.Rects.TextRect.Height = (int)textSize.Height + (Margin.Top / 3);
		e.Rects.CenterRect = e.Rects.TextRect.Pad(0, -Margin.Top, 0, 0);
		e.DrawableItem.CachedHeight = e.Rects.TextRect.Bottom;

		using var brushTitle = new SolidBrush(e.Rects.CenterRect.Contains(CursorLocation) && e.HoverState == HoverState.Hovered ? FormDesign.Design.ActiveColor : e.BackColor.GetTextColor());

		e.Graphics.DrawString(text, font, brushTitle, textRect, stringFormat);

		for (var i = 0; i < tags.Count; i++)
		{
			var tagSize = e.Graphics.MeasureLabel(tags[i].Text, null, smaller: true);

			if (tagRect.X + tagSize.Width > e.Rects.TextRect.Right)
			{
				tagRect.Y += tagRect.Height;
				tagRect.X = e.Rects.TextRect.X;
				e.DrawableItem.CachedHeight += tagRect.Height;
			}

			var rect = e.Graphics.DrawLabel(tags[i].Text, null, tags[i].Color, tagRect, ContentAlignment.MiddleLeft, smaller: true);

			tagRect.X += Margin.Left + rect.Width;
		}

		if (!oneLine && tags.Count > 0)
		{
			e.DrawableItem.CachedHeight += tagRect.Height;
		}
	}
	private void DrawAuthor(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e, IWorkshopInfo? workshopInfo)
	{
		var author = workshopInfo?.Author;

		if (author?.Name is not null and not "")
		{
			using var authorFont = UI.Font(6.75F, FontStyle.Regular);
			using var authorFontUnderline = UI.Font(6.75F, FontStyle.Underline);
			using var stringFormat = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

			var rect = new Rectangle(e.Rects.TextRect.X, e.DrawableItem.CachedHeight, e.Rects.TextRect.Width, 0);
			var size = e.Graphics.Measure(author.Name, authorFont).ToSize();

			using var authorIcon = IconManager.GetIcon("Author", size.Height);

			e.Rects.AuthorRect = rect.Align(size + new Size(authorIcon.Width, 0), ContentAlignment.TopLeft);
			e.DrawableItem.CachedHeight = e.Rects.AuthorRect.Bottom + (Margin.Top / 3);

			var isHovered = e.Rects.AuthorRect.Contains(CursorLocation);
			using var brush = new SolidBrush(isHovered ? FormDesign.Design.ActiveColor : Color.FromArgb(200, ForeColor));

			e.Graphics.DrawImage(authorIcon.Color(brush.Color, brush.Color.A), e.Rects.AuthorRect.Align(authorIcon.Size, ContentAlignment.MiddleLeft));
			e.Graphics.DrawString(author.Name, isHovered ? authorFontUnderline : authorFont, brush, e.Rects.AuthorRect, stringFormat);
		}
	}
	private void DrawVersionAndTags(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e, IWorkshopInfo? workshopInfo)
	{
#if CS1
			var isVersion = localParentPackage?.Mod is not null && !e.Item.IsBuiltIn && !IsPackagePage;
			var text = isVersion ? "v" + localParentPackage!.Mod!.Version.GetString() : e.Item.IsBuiltIn ? Locale.Vanilla : e.Item is ILocalPackageData lp ? lp.LocalSize.SizeString() : workshopInfo?.ServerSize.SizeString();
#else
		var isVersion = (workshopInfo?.IsCodeMod ?? false);
		var versionText = isVersion ? "v" + workshopInfo!.Version : workshopInfo?.ServerSize.SizeString(0);
#endif

		var packageTags = e.Item.GetTags(false).ToList();

		if (packageTags.Count > 0)
		{
			if (!string.IsNullOrEmpty(versionText))
			{
				versionText += " • ";
			}
			else
			{
				versionText = string.Empty;
			}

			versionText += packageTags.ListStrings(", ");
		}

		if (!string.IsNullOrEmpty(versionText))
		{
			using var fadedBrush = new SolidBrush(Color.FromArgb(150, e.BackColor.GetTextColor()));

			var rect = new Rectangle(e.Rects.IconRect.X, e.DrawableItem.CachedHeight, e.Rects.IconRect.Width, Height);

			using var versionFont = UI.Font(6.75F);

			e.Graphics.DrawString(versionText, versionFont, fadedBrush, rect);

			e.DrawableItem.CachedHeight += (int)e.Graphics.Measure(versionText, versionFont, rect.Width).Height;
		}
	}
	private void DrawDots(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e)
	{
		var isHovered = e.Rects.DotsRect.Contains(CursorLocation);
		using var img = IconManager.GetIcon("VertialMore", e.Rects.DotsRect.Height * 3 / 4).Color(isHovered ? FormDesign.Design.ActiveColor : FormDesign.Design.IconColor);

		e.Graphics.DrawImage(img, e.Rects.DotsRect.CenterR(img.Size));
	}
}