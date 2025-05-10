using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Dashboard;
using Skyve.App.UserInterface.Lists;
using Skyve.Compatibility.Domain.Enums;

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
	private int maxHeight;

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
			WorkshopService.OnContextAvailable += LoadData;
		}
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		WorkshopService.OnContextAvailable -= LoadData;
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
		using var fontSmallBold = UI.Font(6.75F, FontStyle.Bold);

		var tagRect = new Rectangle(e.ClipRectangle.X + BorderRadius, preferredHeight, 0, 0);

		if (!applyDrawing)
		{
			maxHeight = 0;
		}

		foreach (var item in _tags)
		{
			using var buttonArgs = new ButtonDrawArgs
			{
				Font = selectedTag == item ? fontSmallBold : fontSmall,
				Padding = UI.Scale(new Padding(2, 2, 4, 2)),
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

			if (selectedTag == item)
			{
				e.Graphics.FillRoundedRectangleWithShadow(buttonArgs.Rectangle, buttonArgs.Padding.Top, buttonArgs.Padding.Top * 2, Color.Empty, Color.FromArgb(25, FormDesign.Design.ActiveColor));
			}

			SlickButton.SetUpColors(buttonArgs);

			SlickButton.DrawButton(e.Graphics, buttonArgs);

			if (selectedTag != item)
			{
				_buttonActions[buttonArgs] = () => SelectTag(item);
			}

			tagRect.X += buttonArgs.Rectangle.Width + UI.Scale(5);
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
		var height = horizontal ? UI.Scale(34) : (columnWidth + UI.Scale(42));

		maxHeight = Math.Max(height, maxHeight);

		for (var i = 0; i < Math.Min(packages.Count, horizontal ? 12 : (columns < 9 ? (columns * 2) : columns)); i++)
		{
			if (i > 0 && (i % columns) == 0)
			{
				preferredHeight += maxHeight;
			}

			var rect = new Rectangle(e.ClipRectangle.X + (Margin.Left / 2) + (i % columns * columnWidth), preferredHeight, columnWidth, maxHeight);

			if (horizontal)
			{
				if (applyDrawing)
				{
					DrawHorizontalMod(e, packages[i], rect);
				}
			}
			else
			{
				maxHeight = Math.Max(maxHeight, DrawGridMod(e, packages[i], rect, applyDrawing));
			}
		}

		preferredHeight += Margin.Top + height;

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

		preferredHeight += BorderRadius / 2;
	}

	private void DrawHorizontalMod(PaintEventArgs e, IWorkshopInfo workshopInfo, Rectangle rect)
	{
		var backColor = FormDesign.Design.BackColor;
		var banner = workshopInfo.GetThumbnail() ?? ItemListControl.WorkshopThumb;
		var bannerRect = rect.Pad(Margin.Left / 2, 0, 0, 0).Align(new Size(rect.Height - (Margin.Left / 2), rect.Height - (Margin.Left / 2)), ContentAlignment.MiddleLeft);
		var notification = workshopInfo.GetCompatibilityInfo().GetNotification();

		if (notification > NotificationType.Info)
		{
			using var brush = new SolidBrush(backColor = notification.GetColor().MergeColor(backColor, 30));
			using var icon = notification.GetIcon(true).Get(rect.Height - Margin.Vertical).Color(notification.GetColor());

			e.Graphics.FillRoundedRectangle(brush, rect.Pad(Margin.Left / 4, 0, Margin.Left / 2, 0).Pad(HoverState.HasFlag(HoverState.Hovered) && rect.Contains(CursorLocation) ? 0 : Margin.Left / 4), Margin.Left / 2);

			e.Graphics.DrawImage(icon, rect.Pad(Margin.Left).Align(icon.Size, ContentAlignment.MiddleRight));

			_buttonActions[Rectangle.FromLTRB(rect.Right - (rect.Height - Margin.Left), rect.Top, rect.Right, rect.Bottom)] = () => ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(workshopInfo, true);
		}

		if (banner is not null)
		{
			e.Graphics.DrawRoundedImage(banner, bannerRect, UI.Scale(4), backColor);
		}

		var text = workshopInfo.CleanName(out var tags) ?? Locale.UnknownPackage;
		var textRect = rect.Pad(Margin.Left + bannerRect.Width, Margin.Left / 4, notification > NotificationType.Info ? rect.Height - (Margin.Left / 2) : Margin.Left / 2, (rect.Height / 2) - (Margin.Left / 4));
		using var textBrush = new SolidBrush(FormDesign.Design.ForeColor);
		using var fadedBrush = new SolidBrush(Color.FromArgb(160, FormDesign.Design.ForeColor));
		using var authorBrush = new SolidBrush(Color.FromArgb(180, UserIcon.GetUserColor(workshopInfo.Author?.Name ?? string.Empty, true)));
		using var textFont = UI.Font(8F, FontStyle.Bold).FitToWidth(text, textRect, e.Graphics);
		using var smallFont = UI.Font(6.75F);
		using var format = new StringFormat { LineAlignment = StringAlignment.Far };

		var isVersion = workshopInfo.IsCodeMod();
		var versionText = isVersion ? "v" + workshopInfo.VersionName : workshopInfo.ServerSize.SizeString(0);

		DrawTextAndTags(e, workshopInfo, textBrush, backColor, textRect);

		textRect = rect.Pad(Margin.Left + bannerRect.Width, (rect.Height / 2) - (Margin.Left / 4), Margin.Left / 2, Margin.Left / 4);

		if (versionText is null)
		{
			e.Graphics.DrawString(workshopInfo.Author?.Name ?? string.Empty, smallFont, authorBrush, textRect, format);
		}
		else
		{
			e.Graphics.DrawString($"{versionText} • ", smallFont, fadedBrush, textRect, format);

			e.Graphics.DrawString(workshopInfo.Author?.Name ?? string.Empty, smallFont, authorBrush, textRect.Pad((int)e.Graphics.Measure($"{versionText} • ", smallFont).Width, 0, 0, 0), format);
		}

		_buttonActions[rect] = () => ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(workshopInfo);
		_buttonRightClickActions[rect] = () => RightClick(workshopInfo);

		if (HoverState.HasFlag(HoverState.Hovered) && rect.Contains(CursorLocation))
		{
			using var brush = new SolidBrush(Color.FromArgb(40, FormDesign.Design.ForeColor));

			e.Graphics.FillRoundedRectangle(brush, rect.Pad(Margin.Left / 4, 0, Margin.Left / 2, 0), Margin.Left / 2);
		}
	}

	private int DrawGridMod(PaintEventArgs e, IWorkshopInfo workshopInfo, Rectangle rect, bool applyDrawing)
	{
		rect.Height -= UI.Scale(3);

		var backColor = FormDesign.Design.BackColor;
		var banner = workshopInfo.GetThumbnail() ?? ItemListControl.WorkshopThumb;
		var bannerRect = rect.Pad(Margin.Top / 2).Align(new Size(rect.Width - (Margin.Top * 3 / 2), rect.Width - (Margin.Top * 3 / 2)), ContentAlignment.TopLeft);
		var notification = workshopInfo.GetCompatibilityInfo().GetNotification();

		if (applyDrawing && notification > NotificationType.Info)
		{
			using var brush = new SolidBrush(backColor = notification.GetColor().MergeColor(backColor, 30));
			using var font = UI.Font(6.5F);
			using var textBrush_ = new SolidBrush(notification.GetColor());
			using var icon = notification.GetIcon(true).Get(font.Height).Color(textBrush_.Color);

			e.Graphics.FillRoundedRectangle(brush, rect.Pad(Margin.Left / 4, 0, Margin.Left / 2, 0), Margin.Left / 2);

			e.Graphics.DrawImage(icon, rect.Pad(Margin.Left / 2).Align(icon.Size, ContentAlignment.BottomLeft));

			e.Graphics.DrawString(LocaleCR.Get(notification.ToString()), font, textBrush_, rect.Pad(0, 0, (Margin.Left / 2) + UI.Scale(1), (Margin.Left / 4) - UI.Scale(1)), new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far });

			_buttonActions[Rectangle.FromLTRB(rect.Left, rect.Bottom - font.Height, rect.Right, rect.Bottom)] = () => ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(workshopInfo, true);
		}

		if (applyDrawing && banner is not null)
		{
			e.Graphics.DrawRoundedImage(banner, bannerRect, UI.Scale(4), backColor);
		}

		using var baseFont = UI.Font(8F, FontStyle.Bold);
		var textRect = rect.Pad(Margin.Left / 2, bannerRect.Height + (Margin.Left * 4 / 4), Margin.Left / 2, Margin.Left / 4).ClipTo(baseFont.Height * 4 / 3);
		using var textBrush = new SolidBrush(FormDesign.Design.ForeColor);
		using var fadedBrush = new SolidBrush(FormDesign.Design.ForeColor.MergeColor(FormDesign.Design.BackColor, 70));
		using var authorBrush = new SolidBrush(Color.FromArgb(180, UserIcon.GetUserColor(workshopInfo.Author?.Name ?? string.Empty, true)));

		textRect.Y += DrawTextAndTags(e, workshopInfo, textBrush, backColor, textRect);

		textRect.Y += DrawVersionAndAuthor(e, workshopInfo, fadedBrush, authorBrush, backColor, textRect);

		_buttonActions[rect] = () => ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(workshopInfo);
		_buttonRightClickActions[rect] = () => RightClick(workshopInfo);

		if (applyDrawing && HoverState.HasFlag(HoverState.Hovered) && rect.Contains(CursorLocation))
		{
			using var brush = new SolidBrush(Color.FromArgb(40, FormDesign.Design.ForeColor));

			e.Graphics.FillRoundedRectangle(brush, rect.Pad(Margin.Left / 4, 0, Margin.Left / 2, notification > NotificationType.Info ? 0 : (rect.Bottom- textRect.Y)), Margin.Left / 2);
		}

		return textRect.Y - rect.Y + (notification > NotificationType.Info ?  UI.Scale(20):0);
	}

	private int DrawTextAndTags(PaintEventArgs e, IWorkshopInfo workshopInfo, SolidBrush textBrush, Color backColor, Rectangle rect)
	{
		var text = workshopInfo.CleanName(out var tags) ?? Locale.UnknownPackage;
		using var font = UI.Font(8F, FontStyle.Bold);
		using var format = new StringFormat { LineAlignment = StringAlignment.Center };

		using var highResBmp = new Bitmap(UI.Scale(500), rect.Height);
		using var highResG = Graphics.FromImage(highResBmp);

		highResG.SetUp(backColor);

		var textSize = highResG.Measure(text, font);

		highResG.DrawString(text, font, textBrush, new Rectangle(default, highResBmp.Size), format);

		var tagRect = new Rectangle((int)textSize.Width + (Margin.Left / 4), 0, 0, rect.Height);

		if (tags is not null)
		{
			foreach (var item in tags)
			{
				tagRect.X += (Margin.Left / 4) + highResG.DrawLabel(item.Text, null, item.Color, tagRect, ContentAlignment.MiddleLeft, smaller: true).Width;
			}
		}

		var factor = Math.Min(1, (double)rect.Width / tagRect.X);

		e.Graphics.SetClip(rect);
		e.Graphics.DrawImage(highResBmp, new Rectangle(rect.X, rect.Y, (int)(highResBmp.Width * factor), (int)(rect.Height * factor)));
		e.Graphics.ResetClip();

		return (int)(rect.Height * factor);
	}

	private int DrawVersionAndAuthor(PaintEventArgs e, IWorkshopInfo workshopInfo, SolidBrush fadedBrush, SolidBrush authorBrush, Color backColor, Rectangle rect)
	{
		var isVersion = workshopInfo.IsCodeMod();
		var versionText = isVersion ? "v" + workshopInfo.VersionName : workshopInfo.ServerSize.SizeString(0);
		using var font = UI.Font(6.75F);
		using var format = new StringFormat { LineAlignment = StringAlignment.Center };

		using var highResBmp = new Bitmap(UI.Scale(500), rect.Height);
		using var highResG = Graphics.FromImage(highResBmp);

		highResG.SetUp(backColor);

		var x = 0;

		if (versionText is not null)
		{
			highResG.DrawString($"{versionText} • ", font, fadedBrush, new Rectangle(default, highResBmp.Size), format);

			x = (int)highResG.Measure($"{versionText} • ", font).Width;
		}

		highResG.DrawString(workshopInfo.Author?.Name ?? string.Empty, font, authorBrush, new Rectangle(x, 0, highResBmp.Width - x, highResBmp.Height), format);

		var totalWidth = x + (int)highResG.Measure(workshopInfo.Author?.Name ?? string.Empty, font).Width;

		if (totalWidth == 0)
		{
			return 0;
		}

		var factor = Math.Min(1, (double)rect.Width / totalWidth);

		e.Graphics.SetClip(rect);
		e.Graphics.DrawImage(highResBmp, new Rectangle(rect.X, rect.Y, (int)(highResBmp.Width * factor), (int)(rect.Height * factor)));
		e.Graphics.ResetClip();

		return (int)(rect.Height * factor);
	}

	protected abstract void ViewMore();
}