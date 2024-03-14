using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Dashboard;
using Skyve.App.UserInterface.Lists;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_PdxModsFeatured : IDashboardItem
{
	private readonly IWorkshopService _workshopService;
	private readonly IModLogicManager _modLogicManager;
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly IModUtil _modUtil;
	private List<IWorkshopInfo> mods = [];

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
				mods = (await _workshopService.QueryFilesAsync(WorkshopQuerySorting.DateUpdated, limit: 8)).ToList();
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

		if (mods.Count == 0)
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
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsShowcase, "I_PDXMods");

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
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsShowcase, "I_PDXMods");

		using var fontSmall = UI.Font(6.75F);
		e.Graphics.DrawStringItem(Locale.RecentlyUpdated, fontSmall, Color.FromArgb(150, FormDesign.Design.ForeColor), e.ClipRectangle.Pad(Margin).Pad((int)(2 * UI.FontScale), 0, 0, 0), ref preferredHeight, applyDrawing);

		preferredHeight -= Margin.Top;

		var preferredSize = horizontal ? 350 : 100;
		var columns = (int)Math.Max(1, Math.Floor((e.ClipRectangle.Width - Margin.Left) / (preferredSize * UI.FontScale)));
		var columnWidth = (e.ClipRectangle.Width - Margin.Left) / columns;
		var height = horizontal ? (int)(35 * UI.FontScale):( columnWidth *2) ;

		for (var i = 0; i < mods.Count; i++)
		{
			if (i > 0 && (i % columns) == 0)
			{
				preferredHeight += height;
			}

			var rect = new Rectangle(e.ClipRectangle.X + (Margin.Left / 2) + (i % columns * columnWidth), preferredHeight, columnWidth, height);

			if (applyDrawing)
			{
				DrawMod(e, mods[i], rect.Pad(Margin.Left / 2), horizontal);
			}
		}

		preferredHeight += Margin.Top + height;
	}

	private void DrawMod(PaintEventArgs e, IWorkshopInfo workshopInfo, Rectangle rect, bool horizontal)
	{
		_buttonActions[rect] = () => ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(workshopInfo);
		_buttonRightClickActions[rect] = () => RightClick(workshopInfo);

		if (horizontal)
		{
			var banner = workshopInfo.GetThumbnail();
			var bannerRect = horizontal
				? rect.Pad(Margin.Left / 4).Align(new Size(rect.Height - (Margin.Top / 2), rect.Height - (Margin.Top / 2)), ContentAlignment.MiddleLeft)
				: rect.Pad(Margin.Left / 2).ClipTo(rect.Width - Margin.Left);

			if (banner is null)
			{
				using var brush = new SolidBrush(Color.FromArgb(40, FormDesign.Design.ForeColor));

				e.Graphics.FillRoundedRectangle(brush, bannerRect, Margin.Left / 2);

				using var icon = IconManager.GetIcon("I_Paradox", bannerRect.Width * 3 / 4).Color(FormDesign.Design.ForeColor);

				e.Graphics.DrawImage(icon, bannerRect.CenterR(icon.Size));
			}
			else
			{
				e.Graphics.DrawRoundedImage(banner, bannerRect, (int)(5 * UI.FontScale));
			}

			if (HoverState.HasFlag(HoverState.Hovered) && rect.Contains(CursorLocation))
			{
				using var brush = new SolidBrush(Color.FromArgb(40, FormDesign.Design.ForeColor));

				e.Graphics.FillRoundedRectangle(brush, bannerRect, Margin.Left / 2);
			}

			var textRect = horizontal ? rect.Pad(Margin.Left + bannerRect.Width, Margin.Left / 2, Margin.Left / 2, Margin.Left / 2) : rect.Pad(Margin.Left / 2, bannerRect.Height + Margin.Top, Margin.Left / 2, Margin.Left / 2);
			using var textBrush = new SolidBrush(FormDesign.Design.ForeColor);
			using var textFont = UI.Font(horizontal ? 8.5F : 8.75F, FontStyle.Bold).FitTo(workshopInfo.Name, textRect, e.Graphics);
			using var centerFormat = new StringFormat { LineAlignment = StringAlignment.Center };

			e.Graphics.DrawString(workshopInfo.Name, textFont, textBrush, textRect, horizontal ? centerFormat : null);
		}
		else
		{
			var pe = new ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles>(new DrawableItem<IPackageIdentity, ItemListControl.Rectangles>(workshopInfo) { Rectangles = GenerateGridRectangles(workshopInfo, rect) }, e.Graphics, [e.ClipRectangle], rect, HoverState, false);
			var package = workshopInfo.GetPackage();
			var localIdentity = workshopInfo.GetLocalPackageIdentity();
			var isIncluded = workshopInfo.IsIncluded(out var partialIncluded) || partialIncluded;
			var isEnabled = workshopInfo.IsEnabled();

			DrawThumbnail(pe);
			DrawTitleAndTags(pe);
			DrawAuthor(pe, workshopInfo);
			DrawVersionAndTags(pe, package, localIdentity, workshopInfo);
			DrawIncludedButton(pe, isIncluded, partialIncluded, isEnabled, package?.LocalData, out var activeColor);
			DrawDots(pe);
		}
	}

	private ItemListControl.Rectangles GenerateGridRectangles(IPackageIdentity item, Rectangle rectangle)
	{
		var rects = new ItemListControl.Rectangles(item)
		{
			IconRect = rectangle.Align(new Size(rectangle.Width, rectangle.Width), ContentAlignment.TopCenter),
			IncludedRect = new Rectangle(new Point(rectangle.X, rectangle.Y + rectangle.Width + Padding.Top), UI.Scale(new Size(32, 32), UI.FontScale))
		};

		rects.DotsRect = new Rectangle(rectangle.X, rects.IncludedRect.Y, rectangle.Width, rects.IncludedRect.Height).Align(UI.Scale(new Size(16, 24), UI.FontScale), ContentAlignment.MiddleRight);

		using var titleFont = UI.Font( 10.5F, FontStyle.Bold);
		rects.TextRect = new Rectangle(rectangle.X + rects.IncludedRect.Width + Padding.Left, rectangle.Y + rectangle.Width + Padding.Top, rectangle.Width - rects.IncludedRect.Width - Padding.Horizontal - rects.DotsRect.Width, 0).AlignToFontSize(titleFont, ContentAlignment.TopLeft);
		rects.CenterRect = rects.TextRect.Pad(0, -Padding.Vertical, 0, 0);

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
			using var generic = IconManager.GetIcon("I_Paradox", e.Rects.IconRect.Height).Color(e.BackColor);
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
		var padding = Padding;
		var text = e.Item.CleanName(out var tags);
		using var stringFormat = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, LineAlignment = StringAlignment.Near };

		using var font = UI.Font(9F, FontStyle.Bold);
		var textRect = new Rectangle(e.Rects.TextRect.X, e.Rects.TextRect.Y, e.Rects.TextRect.Width, Height);

		var textSize = e.Graphics.Measure(text, font, textRect.Width);
		var oneLineSize = e.Graphics.Measure(text, font);
		var oneLine = textSize.Height == oneLineSize.Height;
		var tagRect = new Rectangle(e.Rects.TextRect.X + (oneLine ? (int)textSize.Width : 0), textRect.Y + (oneLine ? 0 : (int)textSize.Height), 0, (int)oneLineSize.Height);

		e.Rects.TextRect.Height = (int)textSize.Height + (padding.Top / 3);
		e.Rects.CenterRect = e.Rects.TextRect.Pad(0, -padding.Top, 0, 0);
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

			tagRect.X += padding.Left + rect.Width;
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
			using var authorFont = UI.Font(7.5F, FontStyle.Regular);
			using var authorFontUnderline = UI.Font(7.5F, FontStyle.Underline);
			using var stringFormat = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

			var rect = new Rectangle(e.Rects.TextRect.X, e.DrawableItem.CachedHeight, e.Rects.TextRect.Width, 0);
			var size = e.Graphics.Measure(author.Name, authorFont).ToSize();

			using var authorIcon = IconManager.GetIcon("I_Author", size.Height);

			e.Rects.AuthorRect = rect.Align(size + new Size(authorIcon.Width, 0), ContentAlignment.TopLeft);
			e.DrawableItem.CachedHeight = e.Rects.AuthorRect.Bottom + (Padding.Top / 3);

			var isHovered = e.Rects.AuthorRect.Contains(CursorLocation);
			using var brush = new SolidBrush(isHovered ? FormDesign.Design.ActiveColor : Color.FromArgb(200, ForeColor));

			e.Graphics.DrawImage(authorIcon.Color(brush.Color, brush.Color.A), e.Rects.AuthorRect.Align(authorIcon.Size, ContentAlignment.MiddleLeft));
			e.Graphics.DrawString(author.Name, isHovered ? authorFontUnderline : authorFont, brush, e.Rects.AuthorRect, stringFormat);
		}
	}
	private void DrawVersionAndTags(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e, IPackage? package, ILocalPackageIdentity? localPackageIdentity, IWorkshopInfo? workshopInfo)
	{
#if CS1
			var isVersion = localParentPackage?.Mod is not null && !e.Item.IsBuiltIn && !IsPackagePage;
			var text = isVersion ? "v" + localParentPackage!.Mod!.Version.GetString() : e.Item.IsBuiltIn ? Locale.Vanilla : e.Item is ILocalPackageData lp ? lp.LocalSize.SizeString() : workshopInfo?.ServerSize.SizeString();
#else
		var isVersion = (package?.IsCodeMod ?? workshopInfo?.IsCodeMod ?? false) && !string.IsNullOrEmpty(package?.Version);
		var versionText = isVersion ? "v" + package!.Version : localPackageIdentity != null ? localPackageIdentity.FileSize.SizeString(0) : workshopInfo?.ServerSize.SizeString(0);
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

			var rect = new Rectangle(e.Rects.TextRect.X, e.DrawableItem.CachedHeight, e.Rects.TextRect.Width, Height);

			using var versionFont = UI.Font(7.5F);

			e.Graphics.DrawString(versionText, versionFont, fadedBrush, rect);

			e.DrawableItem.CachedHeight += (int)e.Graphics.Measure(versionText, versionFont, rect.Width).Height;
		}
	}
	private void DrawIncludedButton(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e, bool isIncluded, bool isPartialIncluded, bool isEnabled, ILocalPackageIdentity? localIdentity, out Color activeColor)
	{
		activeColor = default;

		if (localIdentity is null && e.Item.IsLocal())
		{
			return; // missing local item
		}

		var required = _modLogicManager.IsRequired(localIdentity, _modUtil);
		var isHovered = e.DrawableItem.Loading || (e.HoverState.HasFlag(HoverState.Hovered) && e.Rects.IncludedRect.Contains(CursorLocation));

		if (!required && isIncluded && isHovered)
		{
			isPartialIncluded = false;
			isEnabled = !isEnabled;
		}

		if (isEnabled)
		{
			activeColor = isPartialIncluded ? FormDesign.Design.YellowColor : FormDesign.Design.GreenColor;
		}

		Color iconColor;

		if (required && activeColor != default)
		{
			iconColor = !FormDesign.Design.IsDarkTheme ? activeColor.MergeColor(ForeColor, 75) : activeColor;
			activeColor = activeColor.MergeColor(BackColor, !FormDesign.Design.IsDarkTheme ? 35 : 20);
		}
		else if (activeColor == default && isHovered)
		{
			iconColor = isIncluded ? isEnabled ? FormDesign.Design.GreenColor : FormDesign.Design.RedColor : FormDesign.Design.ActiveColor;
			activeColor = Color.FromArgb(40, iconColor);
		}
		else
		{
			if (activeColor == default)
			{
				activeColor = Color.FromArgb(20, ForeColor);
			}
			else if (isHovered)
			{
				activeColor = activeColor.MergeColor(ForeColor, 75);
			}

			iconColor = activeColor.GetTextColor();
		}

		using var brush = e.Rects.IncludedRect.Gradient(activeColor);
		e.Graphics.FillRoundedRectangle(brush, e.Rects.IncludedRect, (int)(4 * UI.FontScale));

		if (e.DrawableItem.Loading)
		{
			var rectangle = e.Rects.IncludedRect.CenterR(e.Rects.IncludedRect.Height * 3 / 5, e.Rects.IncludedRect.Height * 3 / 5);
#if CS2
			if (_subscriptionsManager.Status.ModId != e.Item.Id || _subscriptionsManager.Status.Progress == 0 || !_subscriptionsManager.Status.IsActive)
			{
				DrawLoader(e.Graphics, rectangle, iconColor);
				return;
			}

			var width = Math.Min(Math.Min(rectangle.Width, rectangle.Height), (int)(32 * UI.UIScale));
			var size = (float)Math.Max(2, width / (8D - (Math.Abs(100 - LoaderPercentage) / 50)));
			var drawSize = new SizeF(width - size, width - size);
			var rect = new RectangleF(new PointF(rectangle.X + ((rectangle.Width - drawSize.Width) / 2), rectangle.Y + ((rectangle.Height - drawSize.Height) / 2)), drawSize).Pad(size / 2);
			using var pen = new Pen(iconColor, size) { StartCap = LineCap.Round, EndCap = LineCap.Round };

			e.Graphics.DrawArc(pen, rect, -90, 360 * _subscriptionsManager.Status.Progress);
#else
			DrawLoader(e.Graphics, rectangle, iconColor);
#endif
			return;
		}

		var icon = new DynamicIcon(_subscriptionsManager.IsSubscribing(e.Item) ? "I_Wait" : isPartialIncluded ? "I_Slash" : isEnabled ? "I_Ok" : !isIncluded ? "I_Add" : "I_Enabled");
		using var includedIcon = icon.Get(e.Rects.IncludedRect.Height * 3 / 4).Color(iconColor);

		e.Graphics.DrawImage(includedIcon, e.Rects.IncludedRect.CenterR(includedIcon.Size));
	}
	private void DrawDots(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e)
	{
		var isHovered = e.Rects.DotsRect.Contains(CursorLocation);
		using var img = IconManager.GetIcon("I_VertialMore", e.Rects.IncludedRect.Height * 3 / 4).Color(isHovered ? FormDesign.Design.ActiveColor : FormDesign.Design.IconColor);

		e.Graphics.DrawImage(img, e.Rects.DotsRect.CenterR(img.Size));
	}
}