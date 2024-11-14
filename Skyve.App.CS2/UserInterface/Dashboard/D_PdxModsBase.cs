using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Dashboard;
using Skyve.App.UserInterface.Lists;
using Skyve.App.UserInterface.Panels;

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

		for (var i = 0; i < Math.Min(packages.Count, horizontal ? 12 : (columns < 9 ? (columns * 2) : columns)); i++)
		{
			if (i > 0 && (i % columns) == 0)
			{
				preferredHeight += height;
			}

			var rect = new Rectangle(e.ClipRectangle.X + (Margin.Left / 2) + (i % columns * columnWidth), preferredHeight, columnWidth, height);

			if (applyDrawing)
			{
				if (horizontal)
				{
					DrawHorizontalMod(e, packages[i], rect);
				}
				else
				{
					DrawGridMod(e, packages[i], rect);
				}
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
		var banner = workshopInfo.GetThumbnail() ?? ItemListControl.WorkshopThumb;
		var bannerRect = rect.Pad(Margin.Left / 2, 0, 0, 0).Align(new Size(rect.Height - (Margin.Left / 2), rect.Height - (Margin.Left / 2)), ContentAlignment.MiddleLeft);

		if (banner is not null)
		{
			e.Graphics.DrawRoundedImage(banner, bannerRect, UI.Scale(4), FormDesign.Design.BackColor);
		}

		if (HoverState.HasFlag(HoverState.Hovered) && rect.Contains(CursorLocation))
		{
			using var brush = new SolidBrush(Color.FromArgb(40, FormDesign.Design.ForeColor));

			e.Graphics.FillRoundedRectangle(brush, rect.Pad(Margin.Left / 4, 0, Margin.Left / 2, 0), Margin.Left / 2);
		}

		var text = workshopInfo.CleanName(out var tags) ?? Locale.UnknownPackage;
		var textRect = rect.Pad(Margin.Left + bannerRect.Width, Margin.Left / 4, Margin.Left / 2, Margin.Left / 4);
		using var textBrush = new SolidBrush(FormDesign.Design.ForeColor);
		using var fadedBrush = new SolidBrush(Color.FromArgb(160, FormDesign.Design.ForeColor));
		using var authorBrush = new SolidBrush(Color.FromArgb(180, UserIcon.GetUserColor(workshopInfo.Author?.Name ?? string.Empty, true)));
		using var textFont = UI.Font(8F, FontStyle.Bold).FitToWidth(text, textRect, e.Graphics);
		using var smallFont = UI.Font(6.75F);
		using var format = new StringFormat { LineAlignment = StringAlignment.Far };

		var isVersion = workshopInfo.IsCodeMod;
		var versionText = isVersion ? workshopInfo.VersionName : null;

		e.Graphics.DrawString(text, textFont, textBrush, textRect);

		if (versionText is null)
		{
			e.Graphics.DrawString(workshopInfo.Author?.Name ?? string.Empty, smallFont, authorBrush, textRect, format);
		}
		else
		{
			e.Graphics.DrawString($"v{versionText} • ", smallFont, fadedBrush, textRect, format);

			e.Graphics.DrawString(workshopInfo.Author?.Name ?? string.Empty, smallFont, authorBrush, textRect.Pad((int)e.Graphics.Measure($"v{versionText} • ", smallFont).Width, 0, 0, 0), format);
		}

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

	private void DrawGridMod(PaintEventArgs e, IWorkshopInfo workshopInfo, Rectangle rect)
	{
		var banner = workshopInfo.GetThumbnail() ?? ItemListControl.WorkshopThumb;
		var bannerRect = rect.Pad(Margin.Top / 2).Align(new Size(rect.Width - Margin.Top*3/2, rect.Width - Margin.Top * 3 / 2), ContentAlignment.TopLeft);

		if (banner is not null)
		{
			e.Graphics.DrawRoundedImage(banner, bannerRect, UI.Scale(4), FormDesign.Design.BackColor);
		}

		if (HoverState.HasFlag(HoverState.Hovered) && rect.Contains(CursorLocation))
		{
			using var brush = new SolidBrush(Color.FromArgb(40, FormDesign.Design.ForeColor));

			e.Graphics.FillRoundedRectangle(brush, rect.Pad(Margin.Left / 4, 0, Margin.Left / 2, 0), Margin.Left / 2);
		}

		using var baseFont = UI.Font(8F, FontStyle.Bold);
		var text = workshopInfo.CleanName(out var tags) ?? Locale.UnknownPackage;
		var textRect = rect.Pad(Margin.Left / 2, bannerRect.Height + (Margin.Left * 4 / 4), Margin.Left / 2, Margin.Left / 4).ClipTo(baseFont.Height * 5 / 3);
		using var textBrush = new SolidBrush(FormDesign.Design.ForeColor);
		using var fadedBrush = new SolidBrush(FormDesign.Design.ForeColor.MergeColor(FormDesign.Design.BackColor, 70));
		using var authorBrush = new SolidBrush(Color.FromArgb(180, UserIcon.GetUserColor(workshopInfo.Author?.Name ?? string.Empty, true)));
		using var textFont = UI.Font(8F, FontStyle.Bold).FitTo(text, textRect, e.Graphics);
		using var format = new StringFormat { };

		var isVersion = workshopInfo.IsCodeMod;
		var versionText = isVersion ? workshopInfo.VersionName : null;

		e.Graphics.DrawString(text, textFont, textBrush, textRect);

		var tagRect = new Rectangle(textRect.X + (int)e.Graphics.Measure(text, Font).Width + (Margin.Left / 2), textRect.Y + (Margin.Top / 4), 0, textRect.Height);

		if (tags is not null)
		{
			foreach (var item in tags)
			{
				tagRect.X += (Margin.Left / 2) + e.Graphics.DrawLabel(item.Text, null, item.Color, tagRect, ContentAlignment.TopLeft, smaller: true).Width;
			}
		}

		textRect = Rectangle.FromLTRB(textRect.X, textRect.Y + (int)e.Graphics.Measure(text, textFont, textRect.Width).Height + UI.Scale(2), textRect.Right, rect.Bottom - (Margin.Left / 2));

		if (versionText is null)
		{
			using var smallFont = UI.Font(6.75F).FitTo(workshopInfo.Author?.Name ?? string.Empty, textRect, e.Graphics);
			e.Graphics.DrawString(workshopInfo.Author?.Name ?? string.Empty, smallFont, authorBrush, textRect, format);
		}
		else
		{
			using var smallFont = UI.Font(6.75F).FitTo($"v{versionText} • {workshopInfo.Author?.Name ?? string.Empty}", textRect, e.Graphics);


			e.Graphics.DrawString($"v{versionText} • {workshopInfo.Author?.Name ?? string.Empty}", smallFont, authorBrush, textRect, format);
			e.Graphics.DrawString($"v{versionText} • ", smallFont, fadedBrush, textRect, format);
		}

		_buttonActions[rect] = () => ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(workshopInfo);
		_buttonRightClickActions[rect] = () => RightClick(workshopInfo);
	}

	protected abstract void ViewMore();
}