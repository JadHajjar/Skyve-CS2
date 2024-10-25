using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Dashboard;
using Skyve.App.UserInterface.Lists;
using Skyve.App.UserInterface.Panels;
using Skyve.Domain.Systems;

using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal abstract class D_PdxModsBase : IDashboardItem
{
	private static readonly Dictionary<string, DateTime> _lastLoadTimes = [];
	private static readonly string[] _tags = ["Code Mod", "Map", "Savegame", "All"];
	private string selectedTag;

	protected readonly IWorkshopService WorkshopService;
	protected readonly IUserService UserService;
	protected string[]? SelectedTags => selectedTag == "All" ? null : [selectedTag];

	public D_PdxModsBase(string? tag)
	{
		selectedTag = tag ?? _tags[0];
		ServiceCenter.Get(out WorkshopService, out UserService);
	}

	protected abstract List<IWorkshopInfo> GetPackages();

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (WorkshopService.IsAvailable)
		{
			if (!_lastLoadTimes.TryGetValue(Key, out var date) || (DateTime.Now - date).TotalMinutes > 15 || GetPackages().Count == 0)
			{
				LoadData();
			}
		}
		else
		{
			WorkshopService.ContextAvailable += LoadData;
		}
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		WorkshopService.ContextAvailable -= LoadData;
	}

	protected override Task<bool> ProcessDataLoad(CancellationToken token)
	{
		_lastLoadTimes[Key] = DateTime.Now;

		return base.ProcessDataLoad(token);
	}

	private void RightClick(IWorkshopInfo package)
	{
		SlickToolStrip.Show(App.Program.MainForm, ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems(package));
	}

	private void SelectTag(string item)
	{
		selectedTag = item;

		LoadData();
	}

	protected void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight, bool horizontal)
	{
		using var fontSmall = UI.Font(6.75F);

		var tagRect = new Rectangle(e.ClipRectangle.X + BorderRadius, preferredHeight, 0, 0);

		foreach (var item in _tags)
		{
			using var buttonArgs = new ButtonDrawArgs
			{
				Font = fontSmall,
				Padding = UI.Scale(new Padding(3, 2, 4, 3)),
				Text = item,
			};

			if (selectedTag == item)
			{
				buttonArgs.ButtonType = ButtonType.Active;
			}
			else
			{
				buttonArgs.HoverState = HoverState & ~HoverState.Focused;
				buttonArgs.Cursor = CursorLocation;
			}

			SlickButton.PrepareLayout(e.Graphics, buttonArgs);

			buttonArgs.Rectangle = tagRect.Align(buttonArgs.Rectangle.Size, ContentAlignment.TopLeft);

			if (buttonArgs.Rectangle.Right > e.ClipRectangle.Right - BorderRadius)
			{
				tagRect.Y += buttonArgs.Rectangle.Height + (BorderRadius / 2);
				tagRect.X = e.ClipRectangle.X + BorderRadius;

				buttonArgs.Rectangle = tagRect.Align(buttonArgs.Rectangle.Size, ContentAlignment.TopLeft);
			}

			SlickButton.SetUpColors(buttonArgs);

			SlickButton.DrawButton(e.Graphics, buttonArgs);

			if (selectedTag != item)
			{
				_buttonActions[buttonArgs] = () => SelectTag(item);
			}

			tagRect.X += buttonArgs.Rectangle.Width + (BorderRadius / 2);
			tagRect.Height = buttonArgs.Rectangle.Height;
		}

		preferredHeight = tagRect.Bottom + (BorderRadius * 3 / 2);

		var packages = GetPackages();

		if (packages.Count == 0)
		{
			return;
		}

		var preferredSize = horizontal ? 350 : 90;
		var columns = (int)Math.Max(1, Math.Floor((e.ClipRectangle.Width - Margin.Left) / (preferredSize * UI.FontScale)));
		var columnWidth = (e.ClipRectangle.Width - Margin.Left) / columns;
		var height = horizontal ? UI.Scale(32) : (columnWidth * 5 / 3);

		for (var i = 0; i < Math.Min(packages.Count, horizontal ? 8 : (columns < 5 ? (columns * 2) : columns)); i++)
		{
			if (i > 0 && (i % columns) == 0)
			{
				preferredHeight += height;
			}

			var rect = new Rectangle(e.ClipRectangle.X + (Margin.Left / 2) + (i % columns * columnWidth), preferredHeight, columnWidth, height);

			if (applyDrawing)
			{
				DrawMod(e, packages[i], rect, horizontal);
			}
		}

		preferredHeight += Margin.Top + height;

		if (!horizontal)
		{
			preferredHeight += Margin.Top;
		}

		using var font = UI.Font(7.5F);

		DrawButton(e, applyDrawing, ref preferredHeight, ViewMore, new ButtonDrawArgs
		{
			Font = font,
			Icon = "PDXMods",
			ButtonType = ButtonType.Dimmed,
			Size = new Size(0, UI.Scale(20)),
			Text = Locale.ViewMoreWorkshop,
			Rectangle = e.ClipRectangle.Pad(BorderRadius)
		});

		preferredHeight += BorderRadius/2;
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
				e.Graphics.DrawRoundedImage(banner, bannerRect, UI.Scale(5));
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
			using var authorBrush = new SolidBrush(Color.FromArgb(180, UserIcon.GetUserColor(workshopInfo.Author?.Name ?? string.Empty, true)));
			using var textFont = UI.Font(8F, FontStyle.Bold).FitToWidth(text, textRect, e.Graphics);
			using var smallFont = UI.Font(6.75F);
			using var format = new StringFormat { LineAlignment = StringAlignment.Far };

			e.Graphics.DrawString(text, textFont, textBrush, textRect);
			e.Graphics.DrawString(workshopInfo.Author?.Name ?? string.Empty, smallFont, authorBrush, textRect, format);

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
			_buttonActions[pe.Rects.AuthorRect] = () => App.Program.MainForm.PushPanel(new PC_UserPage(workshopInfo.Author!));
			_buttonRightClickActions[rect] = () => RightClick(workshopInfo);
		}
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

			e.Graphics.FillRoundedRectangle(brush, e.Rects.IconRect, UI.Scale(5));
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
			e.Graphics.FillRoundedRectangle(brush, e.Rects.IconRect, UI.Scale(5));
		}

		void drawThumbnail(Bitmap generic) => e.Graphics.DrawRoundedImage(generic, e.Rects.IconRect, UI.Scale(5), FormDesign.Design.BackColor);
	}
	private void DrawTitleAndTags(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e)
	{
		var text = e.Item.CleanName(out var tags);

		using var font = UI.Font(7.75F, FontStyle.Bold);
		var textRect = new Rectangle(e.Rects.TextRect.X, e.Rects.TextRect.Y, e.Rects.TextRect.Width, Height);

		var textSize = e.Graphics.Measure(text, font, textRect.Width);
		var oneLineSize = e.Graphics.Measure(text, font);
		var oneLine = textSize.Height == oneLineSize.Height;
		var tagRect = new Rectangle(e.Rects.TextRect.X + (oneLine ? (int)textSize.Width : 0), textRect.Y + (oneLine ? 0 : (int)textSize.Height), 0, (int)oneLineSize.Height);

		e.Rects.TextRect.Height = (int)textSize.Height + (Margin.Top / 3);
		e.Rects.CenterRect = e.Rects.TextRect.Pad(0, -Margin.Top, 0, 0);
		e.DrawableItem.CachedHeight = e.Rects.TextRect.Bottom;

		using var brushTitle = new SolidBrush(e.Rects.CenterRect.Contains(CursorLocation) && e.HoverState == HoverState.Hovered ? FormDesign.Design.ActiveColor : e.BackColor.GetTextColor());

		e.Graphics.DrawString(text, font, brushTitle, textRect);

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
			using var brush = new SolidBrush(isHovered ? FormDesign.Design.ActiveColor : UserIcon.GetUserColor(author.Id?.ToString() ?? string.Empty, true));

			DrawAuthorImage(e, author, e.Rects.AuthorRect.Align(new(size.Height, size.Height), ContentAlignment.MiddleLeft), brush.Color);
			
			e.Graphics.DrawString(author.Name, isHovered ? authorFontUnderline : authorFont, brush, e.Rects.AuthorRect, stringFormat);
		}
	}

	private void DrawAuthorImage(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e, IUser author, Rectangle rectangle, Color color)
	{
		var image = WorkshopService.GetUser(author).GetThumbnail();

		if (image != null)
		{
			e.Graphics.DrawRoundImage(image, rectangle.Pad((int)(1.5 * UI.FontScale)));

			if (e.HoverState.HasFlag(HoverState.Hovered) && e.Rects.AuthorRect.Contains(CursorLocation))
			{
				using var pen = new Pen(color, 1.5f);

				e.Graphics.DrawEllipse(pen, rectangle.Pad((int)(1.5 * UI.FontScale)));
			}
		}
		else
		{
			using var authorIcon = IconManager.GetIcon("Author", rectangle.Height);

			e.Graphics.DrawImage(authorIcon.Color(color, color.A), rectangle.CenterR(authorIcon.Size));
		}

		if (UserService.IsUserVerified(author))
		{
			var checkRect = rectangle.Align(new Size(rectangle.Height / 3, rectangle.Height / 3), ContentAlignment.BottomRight);

			e.Graphics.FillEllipse(new SolidBrush(FormDesign.Design.GreenColor), checkRect.Pad(-UI.Scale(2)));

			using var img = IconManager.GetIcon("Check", checkRect.Height);
			e.Graphics.DrawImage(img.Color(Color.White), checkRect.Pad(0, 0, -1, -1));
		}
	}

	private void DrawVersionAndTags(ItemPaintEventArgs<IPackageIdentity, ItemListControl.Rectangles> e, IWorkshopInfo? workshopInfo)
	{
#if CS1
			var isVersion = localParentPackage?.Mod is not null && !e.Item.IsBuiltIn && !IsPackagePage;
			var text = isVersion ? "v" + localParentPackage!.Mod!.Version.GetString() : e.Item.IsBuiltIn ? Locale.Vanilla : e.Item is ILocalPackageData lp ? lp.LocalSize.SizeString() : workshopInfo?.ServerSize.SizeString();
#else
		var isVersion = workshopInfo?.IsCodeMod ?? false;
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

	private ItemListControl.Rectangles GenerateGridRectangles(IPackageIdentity item, Rectangle rectangle)
	{
		var rects = new ItemListControl.Rectangles(item)
		{
			IconRect = rectangle.Align(new Size(rectangle.Width, rectangle.Width), ContentAlignment.TopCenter),
			DotsRect = new Rectangle(rectangle.X, rectangle.Y + rectangle.Width + (Margin.Top / 2), rectangle.Width, 0).Align(UI.Scale(new Size(16, 24)), ContentAlignment.TopRight)
		};

		using var titleFont = UI.Font(10.5F, FontStyle.Bold);
		rects.TextRect = new Rectangle(rectangle.X, rectangle.Y + rectangle.Width + (Margin.Top / 2), rectangle.Width - Margin.Left - rects.DotsRect.Width, 0).AlignToFontSize(titleFont, ContentAlignment.TopLeft);
		rects.CenterRect = rects.TextRect.Pad(0, -Margin.Vertical, 0, 0);

		return rects;
	}

	protected abstract void ViewMore();
}