using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Lists;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;

internal class DownloadsListControl : SlickStackedListControl<ISubscriptionStatus, DownloadsListControl.Rectangles>
{
	private readonly ISettings _settings;
	private readonly IUserService _userService;
	private readonly IWorkshopService _workshopService;

	public DownloadsListControl()
	{
		SeparateWithLines = true;
		HighlightOnHover = true;
		DynamicSizing = true;

		ServiceCenter.Get(out _settings, out _workshopService, out _userService);
	}

	protected override void UIChanged()
	{
		ItemHeight = 24;

		base.UIChanged();

		Padding = UI.Scale(new Padding(0, 2, 0, 2));
	}

	protected override void OnPaintItemList(ItemPaintEventArgs<ISubscriptionStatus, Rectangles> e)
	{
		var workshopInfo = e.Item.Mod;
		var localIdentity = e.Item.Mod.GetLocalPackageIdentity();

		base.OnPaintItemList(e);

		DrawThumbnail(e, workshopInfo);
		DrawTitleAndTags(e);
		//DrawVersionAndTags(e, e.Item.Mod.GetPackage(), localIdentity, workshopInfo);
		DrawDownloadInfo(e);
		//DrawCenterInfo(e, localIdentity, workshopInfo);
		//DrawButtons(e, localIdentity, workshopInfo);
	}

	private void DrawDownloadInfo(ItemPaintEventArgs<ISubscriptionStatus, Rectangles> e)
	{

		var hovered = e.HoverState.HasFlag(HoverState.Hovered) && e.Rects.DownloadInfoRect.Contains(CursorLocation);

		using var backPen = new Pen(Color.FromArgb(25, e.BackColor.GetTextColor()), UI.Scale(2F));
		using var currentPen = new Pen(FormDesign.Design.MenuForeColor, UI.Scale(2F)) { EndCap = System.Drawing.Drawing2D.LineCap.Round, StartCap = System.Drawing.Drawing2D.LineCap.Round };
		using var totalPen = new Pen(FormDesign.Design.ActiveColor, UI.Scale(2F)) { EndCap = System.Drawing.Drawing2D.LineCap.Round, StartCap = System.Drawing.Drawing2D.LineCap.Round };

		var totalRect = e.Rects.DownloadInfoRect.Pad(UI.Scale(2));
		var currentRect = e.Rects.DownloadInfoRect.Pad((int)UI.Scale(5));

		e.Graphics.DrawEllipse(backPen, currentRect);
		e.Graphics.DrawEllipse(backPen, totalRect);
		e.Graphics.DrawArc(currentPen, currentRect, -90, 360 * e.Item.StageProgress);
		e.Graphics.DrawArc(totalPen, totalRect, -90, 360 * e.Item.TotalProgress);

		//var barsRect = e.Rects.DownloadInfoRect.Pad(0, e.Item.Stage == ModDownloadStage.Pending ? 0 : e.Rects.DownloadInfoRect.Height / 2, 0, 0)
		//	.CenterR(new Size(e.Rects.DownloadInfoRect.Width * 8 / 10, UI.Scale(10)));
		//var currentBarRect = barsRect.Align(new Size((int)(barsRect.Width * e.Item.StageProgress), barsRect.Height / 2), ContentAlignment.TopLeft);
		//var totalBarRect = barsRect.Align(new Size((int)(barsRect.Width * e.Item.TotalProgress), barsRect.Height / 2), ContentAlignment.BottomLeft);

		//totalBarRect.Y++;
		//barsRect.Height++;

		//if (e.Item.Stage == ModDownloadStage.Pending)
		//{
		//	using var pendingFont = UI.Font(7.5F);
		//	using var pendingBrush = new SolidBrush(FormDesign.Design.InfoColor);
		//	using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

		//	e.Graphics.DrawString(LocaleCS2.DownloadPending, pendingFont, pendingBrush, barsRect, format);

		//	return;
		//}

		//e.Graphics.FillRoundedRectangleWithShadow(barsRect.Pad(UI.Scale(-2)), UI.Scale(4), UI.Scale(4), FormDesign.Design.BackColor);

		//if (currentBarRect.Width > currentBarRect.Height)
		//{
		//	using var currentBarBrush = new SolidBrush(FormDesign.Design.ForeColor);
		//	e.Graphics.FillRoundedRectangle(currentBarBrush, currentBarRect, currentBarRect.Height / 2, botLeft: false);
		//}

		//if (totalBarRect.Width > currentBarRect.Height)
		//{
		//	using var totalBarBrush = new SolidBrush(FormDesign.Design.ActiveColor);
		//	e.Graphics.FillRoundedRectangle(totalBarBrush, totalBarRect, totalBarRect.Height / 2, topLeft: false);
		//}
	}

	private void DrawThumbnail(ItemPaintEventArgs<ISubscriptionStatus, Rectangles> e, IWorkshopInfo? workshopInfo)
	{
		if (!e.InvalidRects.Any(x => x.IntersectsWith(e.Rects.IconRect)))
		{
			return;
		}

		var thumbnail = e.Item.Mod.GetThumbnail();

		if (thumbnail is null)
		{
			thumbnail = ItemListControl.WorkshopThumb;

			if (thumbnail is not null)
			{
				drawThumbnail(thumbnail);
			}
		}
		else if (e.Item.Mod.IsLocal())
		{
			using var unsatImg = thumbnail.ToGrayscale();

			drawThumbnail(unsatImg);
		}
		else
		{
			drawThumbnail(thumbnail);
		}

		if (e.HoverState.HasFlag(HoverState.Hovered) && e.Rects.IconRect.Contains(CursorLocation))
		{
			using var brush = new SolidBrush(Color.FromArgb(75, 255, 255, 255));
			e.Graphics.FillRoundedRectangle(brush, e.Rects.IconRect, UI.Scale(3));
		}

		void drawThumbnail(Bitmap generic)
		{
			e.Graphics.DrawRoundedImage(generic, e.Rects.IconRect, UI.Scale(3), e.BackColor);
		}
	}

	private int DrawTitleAndTags(ItemPaintEventArgs<ISubscriptionStatus, Rectangles> e)
	{
		var text = e.Item.Mod.CleanName(out var tags) ?? Locale.UnknownPackage;
		var rect = e.Rects.TextRect.Pad(0, UI.Scale(2), 0, e.Rects.TextRect.Height / 2);
		using var textBrush = new SolidBrush(e.Rects.CenterRect.Contains(CursorLocation) && e.HoverState == HoverState.Hovered ? FormDesign.Design.ActiveColor : e.BackColor.GetTextColor());
		using var font = UI.Font(6F, FontStyle.Bold);
		using var format = new StringFormat { LineAlignment = StringAlignment.Center };

		using var highResBmp = new Bitmap(UI.Scale(500), rect.Height);
		using var highResG = Graphics.FromImage(highResBmp);

		highResG.SetUp(e.BackColor);

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

	private void DrawVersionAndTags(ItemPaintEventArgs<ISubscriptionStatus, Rectangles> e, IPackage? package, ILocalPackageIdentity? localPackageIdentity, IWorkshopInfo? workshopInfo)
	{
#if CS1
			var isVersion = localParentPackage?.Mod is not null && !e.Item.Mod.IsBuiltIn && !IsPackagePage;
			var text = isVersion ? "v" + localParentPackage!.Mod!.Version.GetString() : e.Item.Mod.IsBuiltIn ? Locale.Vanilla : e.Item.Mod is ILocalPackageData lp ? lp.LocalSize.SizeString() : workshopInfo?.ServerSize.SizeString();
#else
		var isVersion = package?.IsCodeMod() ?? false;
		var versionText = isVersion ? package?.VersionName ?? (workshopInfo?.Changelog.FirstOrDefault(x => x.VersionId == package?.Version)?.Version) : null;
		versionText = versionText is not null ? $"v{versionText}" : localPackageIdentity != null ? localPackageIdentity.FileSize.SizeString(0) : workshopInfo?.ServerSize.SizeString(0);
#endif

		var packageTags = e.Item.Mod.GetTags(true, true).ToList();

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
			using var fadedBrush = new SolidBrush(Color.FromArgb(GridView ? 150 : 200, e.BackColor.GetTextColor()));

			var rect = GridView
				? new Rectangle(e.Rects.TextRect.X, e.DrawableItem.CachedHeight, e.Rects.TextRect.Width, Height)
				: new Rectangle(e.Rects.TextRect.X, e.Rects.TextRect.Bottom, e.Rects.TextRect.Width, e.Rects.IconRect.Bottom - e.Rects.TextRect.Bottom - Padding.Bottom);

			using var versionFont = GridView ? UI.Font(7.5F) : UI.Font(8.25F).FitToHeight(versionText, rect, e.Graphics);
			using var format = GridView ? new() : new StringFormat { LineAlignment = StringAlignment.Far };

			e.Graphics.DrawString(versionText, versionFont, fadedBrush, rect, format);

			if (GridView)
			{
				e.DrawableItem.CachedHeight += (int)e.Graphics.Measure(versionText, versionFont, rect.Width).Height;
			}
		}
	}

	private void DrawCenterInfo(ItemPaintEventArgs<ISubscriptionStatus, Rectangles> e, ILocalPackageIdentity? localIdentity, IWorkshopInfo? workshopInfo)
	{
		if ((e.ClipRectangle.Width - Padding.Horizontal) / UI.FontScale <= 500)
		{
			return;
		}

		using var fontBase = UI.Font(8.25F);
		using var brush = new SolidBrush(e.BackColor.GetTextColor());
		using var stringFormat = new StringFormat { LineAlignment = StringAlignment.Center };

		var index = 0;
		var itemHeight = (int)e.Graphics.Measure(" ", fontBase).Height;
		var rect = new Rectangle(e.Rects.TextRect.Right, e.Rects.TextRect.Y + Padding.Top, e.ClipRectangle.Width * 2 / 10, itemHeight);
		var author = workshopInfo?.Author;

		if (author?.Name is not null and not "")
		{
			var isHovered = rect.Contains(CursorLocation);

			using var authorBrush = new SolidBrush(isHovered ? FormDesign.Design.ActiveColor : UserIcon.GetUserColor(author.Id?.ToString() ?? string.Empty, true));
			using var font = UI.Font(8.25F).FitToWidth(author.Name, rect.Pad(itemHeight + Padding.Top + Padding.Left, 0, 0, 0), e.Graphics);
			using var fontUnderline = UI.Font(8.25F, FontStyle.Underline).FitToWidth(author.Name, rect.Pad(itemHeight + Padding.Top + Padding.Left, 0, 0, 0), e.Graphics);

			DrawAuthorImage(e, author, rect.Align(new Size(itemHeight + Padding.Top, itemHeight + Padding.Top), ContentAlignment.MiddleLeft), authorBrush.Color);
			e.Graphics.DrawString(author.Name, isHovered ? fontUnderline : font, authorBrush, rect.Pad(itemHeight + Padding.Top + Padding.Left, 0, 0, 0), stringFormat);

			e.Rects.AuthorRect = rect;
		}
		else if (localIdentity is not null)
		{
			using var icon = IconManager.GetIcon("Folder", itemHeight + Padding.Top).Color(brush.Color);
			using var font = UI.Font(8.25F).FitTo(Path.GetFileName(localIdentity.Folder), rect.Pad(icon.Width + Padding.Left, 0, 0, 0), e.Graphics);

			e.Graphics.DrawImage(icon, rect.Align(icon.Size, ContentAlignment.MiddleLeft));
			e.Graphics.DrawString(Path.GetFileName(localIdentity.Folder), font, brush, rect.Pad(icon.Width + Padding.Left, 0, 0, 0), stringFormat);
		}

		tick();

		var date = workshopInfo is null || workshopInfo.ServerTime == default ? (localIdentity?.LocalTime ?? default) : workshopInfo.ServerTime;

		if (date != default)
		{
			var dateText = _settings.UserSettings.ShowDatesRelatively ? date.ToLocalTime().ToRelatedString(true, false) : date.ToLocalTime().ToString("g");
			var isRecent = date > DateTime.UtcNow.AddDays(-7) && e.BackColor != FormDesign.Design.ActiveColor;

			using var activeBrush = new SolidBrush(FormDesign.Design.ActiveColor);
			using var icon = IconManager.GetIcon("UpdateTime", itemHeight + Padding.Top).Color(isRecent ? activeBrush.Color : brush.Color);
			using var font = UI.Font(8.25F).FitToWidth(dateText, rect.Pad(icon.Width + Padding.Left, 0, 0, 0), e.Graphics);

			e.Graphics.DrawImage(icon, rect.Align(icon.Size, ContentAlignment.MiddleLeft));
			e.Graphics.DrawString(dateText, font, isRecent ? activeBrush : brush, rect.Pad(icon.Width + Padding.Left, 0, 0, 0), stringFormat);
		}

		tick();

		if (workshopInfo is not null)
		{
			if (workshopInfo.VoteCount >= 0)
			{
				var isHovered = false;
				var text = Locale.VotesCount.FormatPlural(workshopInfo.VoteCount, workshopInfo.VoteCount.ToString("N0"));
				using var fontBold = UI.Font(8.25F, FontStyle.Bold);
				using var fontUnderline = UI.Font(8.25F, workshopInfo.HasVoted ? FontStyle.Bold | FontStyle.Underline : FontStyle.Underline);
				using var greenBrush = new SolidBrush(FormDesign.Design.GreenColor.MergeColor(brush.Color, 75));
				using var icon = IconManager.GetIcon(workshopInfo.HasVoted ? "VoteFilled" : "Vote", itemHeight + Padding.Top).Color(isHovered || workshopInfo.HasVoted ? greenBrush.Color : brush.Color);
				using var font = UI.Font(8.25F).FitToWidth(text, rect.Pad(icon.Width + Padding.Left, 0, 0, 0), e.Graphics);

				e.Graphics.DrawImage(icon, rect.Align(icon.Size, ContentAlignment.MiddleLeft));
				e.Graphics.DrawString(text, isHovered ? fontUnderline : workshopInfo.HasVoted ? fontBold : font, isHovered || workshopInfo.HasVoted ? greenBrush : brush, rect.Pad(icon.Width + Padding.Left, 0, 0, 0), stringFormat);
			}

			tick();

			if (workshopInfo.Subscribers >= 0)
			{
				var text2 = Locale.SubscribersCount.FormatPlural(workshopInfo.Subscribers, workshopInfo.Subscribers.ToString("N0"));
				using var subsIcon = IconManager.GetIcon("People", itemHeight + Padding.Top).Color(brush.Color);
				using var font2 = UI.Font(8.25F).FitToWidth(text2, rect.Pad(subsIcon.Width + Padding.Left, 0, 0, 0), e.Graphics);

				e.Graphics.DrawImage(subsIcon, rect.Align(subsIcon.Size, ContentAlignment.MiddleLeft));
				e.Graphics.DrawString(text2, font2, brush, rect.Pad(subsIcon.Width + Padding.Left, 0, 0, 0), stringFormat);
			}
		}

		void tick()
		{
			if (++index % 2 == 0)
			{
				rect.X += e.ClipRectangle.Width / 5;
				rect.Y = e.Rects.TextRect.Y + Padding.Top;
			}
			else
			{
				rect.Y = e.ClipRectangle.Bottom - (Padding.Bottom * 2) - rect.Height;
			}
		}
	}

	private void DrawAuthorImage(ItemPaintEventArgs<ISubscriptionStatus, Rectangles> e, IUser author, Rectangle rectangle, Color color)
	{
		var image = _workshopService.GetUser(author).GetThumbnail();

		if (image != null)
		{
			e.Graphics.DrawRoundImage(image, rectangle.Pad((int)(1.5 * UI.FontScale)));

			if (e.HoverState.HasFlag(HoverState.Hovered))
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

		if (_userService.IsUserVerified(author))
		{
			var checkRect = rectangle.Align(new Size(rectangle.Height / 3, rectangle.Height / 3), ContentAlignment.BottomRight);

			using var greenBrush = new SolidBrush(FormDesign.Design.GreenColor);
			e.Graphics.FillEllipse(greenBrush, checkRect.Pad(-UI.Scale(2)));

			using var img = IconManager.GetIcon("Check", checkRect.Height);
			e.Graphics.DrawImage(img.Color(Color.White), checkRect.Pad(0, 0, -1, -1));
		}
	}

	protected override IDrawableItemRectangles<ISubscriptionStatus> GenerateRectangles(ISubscriptionStatus item, Rectangle rectangle, IDrawableItemRectangles<ISubscriptionStatus> current)
	{
		rectangle = rectangle.Pad(Padding.Left, 0, Padding.Right, 0);

		var rects = new Rectangles()
		{
			IconRect = rectangle.Align(new Size(rectangle.Height - Padding.Vertical, rectangle.Height - Padding.Vertical), ContentAlignment.MiddleLeft)
		};

		rects.IconRect.X += Padding.Horizontal;
		rects.DownloadInfoRect = rectangle.Align(new Size(rectangle.Height, rectangle.Height), ContentAlignment.MiddleRight);

		using var font = UI.Font(9.75F, FontStyle.Bold);
		rects.TextRect = new Rectangle(rects.IconRect.Right + UI.Scale(2), rectangle.Y, rects.DownloadInfoRect .Left - rects.IconRect.Right - UI.Scale(4), 0).AlignToFontSize(font, ContentAlignment.TopLeft);

		rects.CenterRect = rects.TextRect.Pad(-Padding.Horizontal, 0, 0, 0);

		return rects;
	}

	internal class Rectangles : IDrawableItemRectangles<ISubscriptionStatus>
	{
		internal Rectangle IconRect;
		internal Rectangle TextRect;
		internal Rectangle CenterRect;
		internal Rectangle AuthorRect;
		internal Rectangle DownloadInfoRect;

		public ISubscriptionStatus Item { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public bool GetToolTip(Control instance, Point location, out string text, out Point point)
		{
			text = string.Empty;
			point = default;
			return false;
		}

		public bool IsHovered(Control instance, Point location)
		{
			return false;
		}
	}
}
