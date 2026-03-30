using Skyve.App.CS2.UserInterface.Generic;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Lists;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;

public class DownloadsInfoControl : SlickControl
{
	private DownloadsListControl LC_Downloads;
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly INotifier _notifier;
	private Rectangle cancelRect;
	private Rectangle pauseRect;
	private Rectangle expandRect;
	private bool visible;
	private bool expanded;
	private readonly Dictionary<ISubscriptionStatus, Rectangle> _downloadsRects = [];
	private IList<ISubscriptionStatus> downloads = [];

	public DownloadsInfoControl()
	{
		ServiceCenter.Get(out _subscriptionsManager, out _notifier);

		LC_Downloads = new() { Visible = false };
		Controls.Add(LC_Downloads);
		Margin = default;
		Visible = Loading = false;
		Cursor = Cursors.Hand;

		_subscriptionsManager.UpdateDisplayNotification += UpdateDownloads;
		_subscriptionsManager.DownloadAddedOrRemoved += UpdateDownloads;
	}

	private void UpdateDownloads()
	{
		downloads = [.. _subscriptionsManager.GetDownloads()
			.Where(x => x.Stage != ModDownloadStage.Completed)
			.OrderBy(x => x.Stage == ModDownloadStage.Pending)
			.ThenBy(x => x.Stage > ModDownloadStage.Completed)
			.ThenByDescending(x => x.TotalBytesToDownload)];

		this.TryInvoke(ToggleVisible);
	}

	private void ToggleVisible()
	{
		visible = downloads.Any();

		LC_Downloads.SetItems(downloads);

		UIChanged();

		if (visible)
		{
			Visible = true;
			Loading = true;
		}
		else
		{
			Visible = false;
			Loading = false;

			if (expanded)
			{
				ToggleExpanded();
			}
		}
	}

	private void ToggleExpanded()
	{
		expanded = !expanded;

		UIChanged();

		if (expanded)
		{
			LC_Downloads.Visible = true;
			LC_Downloads.Loading = true;
		}
		else
		{
			LC_Downloads.Visible = false;
			LC_Downloads.Loading = false;
		}
	}

	protected override async void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		if (e.Button != MouseButtons.Left)
			return;

		if (expandRect.Contains(e.Location))
		{
			ToggleExpanded();
			return;
		}

		if (cancelRect.Contains(e.Location))
		{
			_subscriptionsManager.CancelDownloads();
			return;
		}

		if (pauseRect.Contains(e.Location))
		{
			if (downloads.All(x => x.Stage == ModDownloadStage.Failed))
			{
				Enabled = false;
				foreach (var item in downloads)
				{
					await _subscriptionsManager.RetryDownload(item);
				}
				Enabled = true;
			}
			else
			{
				_subscriptionsManager.TogglePause();
			}

			return;
		}

		foreach (var item in _downloadsRects)
		{
			if (item.Value.Contains(e.Location))
			{
				ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(item.Key.Mod);
				return;
			}
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);

		var hovered = false;

		if (expandRect.Contains(e.Location))
		{
			SlickTip.SetTo(this, expanded ? "Expand" : "Collapse", false, expandRect.Location);
			hovered = true;
		}
		else if (cancelRect.Contains(e.Location))
		{
			SlickTip.SetTo(this, "Stop downloads", false, cancelRect.Location);
			hovered = true;
		}

		if (pauseRect.Contains(e.Location))
		{
			if (downloads.All(x => x.Stage == ModDownloadStage.Failed))
			{
				SlickTip.SetTo(this, "Retry downloads", false, expandRect.Location);
			}
			else
			{
				SlickTip.SetTo(this, _subscriptionsManager.IsPaused || !_subscriptionsManager.IsRunning ? "Start downloads" : "Pause downloads", false, pauseRect.Location);
			}

			hovered = true;
		}

		foreach (var item in _downloadsRects)
		{
			if (item.Value.Contains(e.Location))
			{
				SlickTip.SetTo(this, item.Key.Mod.Name, false, item.Value.Location);
				hovered = true;
			}
		}

		Cursor = hovered ? Cursors.Hand : Cursors.Default;
	}

	protected override void UIChanged()
	{
		Padding = UI.Scale(new Padding(3, 4, 3, 12));
		Height = UI.Scale(expanded ? 200 : 64);
		LC_Downloads.Bounds = new Rectangle(Padding.Left, UI.Scale(2), Width - Padding.Horizontal, Height - UI.Scale(64));
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		if (!Live)
		{
			return;
		}

		e.Graphics.SetUp(BackColor);

		using var pen = new Pen(FormDesign.Design.AccentColor, (float)(1.5 * UI.FontScale));

		e.Graphics.DrawLine(pen, Padding.Left, pen.Width, Width - Padding.Right, pen.Width);

		var clientRectangle = Rectangle.FromLTRB(0, Height - UI.Scale(64), Width, Height);

		var displayedMods = downloads.OrderByDescending(x => x.Stage is > ModDownloadStage.Pending and < ModDownloadStage.Completed).Take(3).ToList();
		var anyPendingDownloads = downloads.Count > displayedMods.Count;
		var imageSize = UI.Scale(24);
		var imageRect = clientRectangle.Pad(Padding).Align(new(imageSize, imageSize), ContentAlignment.MiddleLeft);

		imageRect.Y += UI.Scale(8);
		imageRect.X += displayedMods.Count * (imageSize * 3 / 4);

		if (expanded)
			e.Graphics.DrawLine(pen, Padding.Left, clientRectangle.Y + pen.Width, Width - Padding.Right, clientRectangle.Y + pen.Width);

		if (anyPendingDownloads)
		{
			using var backBrush = new SolidBrush(FormDesign.Design.AccentColor.MergeColor(BackColor, 25));
			using var numberBrush = new SolidBrush(backBrush.Color.GetTextColor());
			using var numberFont = UI.Font(8F, FontStyle.Bold).FitTo($"+{downloads.Count - displayedMods.Count}", imageRect, e.Graphics);
			using var numberFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

			e.Graphics.FillRoundShadow(imageRect.Pad(UI.Scale(-3)), Color.FromArgb(50, 0, 0, 0));
			e.Graphics.FillEllipse(backBrush, imageRect);
			e.Graphics.DrawString($"+{downloads.Count - displayedMods.Count}", numberFont, numberBrush, imageRect, numberFormat);
		}

		imageRect.X -= imageSize * 3 / 4;
		_downloadsRects.Clear();

		for (var i = displayedMods.Count - 1; i >= 0; i--)
		{
			var download = displayedMods[i];
			var image = download.Mod.GetThumbnail() ?? ItemListControl.WorkshopThumb;

			e.Graphics.FillRoundShadow(imageRect.Pad(UI.Scale(-3)), Color.FromArgb(50, 0, 0, 0));

			if (image is not null)
				e.Graphics.DrawRoundImage(image, imageRect);

			if (imageRect.Pad(i == 0 ? 0 : imageSize / 4, 0, 0, 0).Contains(PointToClient(Cursor.Position)))
			{
				using var highlightBrush = new SolidBrush(Color.FromArgb(50, 255, 255, 255));
				e.Graphics.FillEllipse(highlightBrush, imageRect);
			}

			if (download.Stage > ModDownloadStage.Pending && download.Stage < ModDownloadStage.Completed)
			{
				using var backPen = new Pen(Color.FromArgb(40, BackColor.GetTextColor()), UI.Scale(2F)) { EndCap = System.Drawing.Drawing2D.LineCap.Round, StartCap = System.Drawing.Drawing2D.LineCap.Round };
				using var currentPen = new Pen(BackColor.GetTextColor(), UI.Scale(2)) { EndCap = System.Drawing.Drawing2D.LineCap.Round, StartCap = System.Drawing.Drawing2D.LineCap.Round };
				using var totalPen = new Pen(FormDesign.Design.ActiveColor, UI.Scale(2)) { EndCap = System.Drawing.Drawing2D.LineCap.Round, StartCap = System.Drawing.Drawing2D.LineCap.Round };

				var totalRect = imageRect;
				var currentRect = imageRect.Pad(UI.Scale(3));
				var invertCurrent = (int)download.Stage % 2 != 0;
				var currentArc = (int)(360 * Math.Min(download.StageProgress, 1F));

				e.Graphics.DrawEllipse(backPen, currentRect);
				e.Graphics.DrawEllipse(backPen, totalRect);
				e.Graphics.DrawArc(currentPen, currentRect, -90 + (invertCurrent ? currentArc : 0), invertCurrent ? (360 - currentArc) : currentArc);
				e.Graphics.DrawArc(totalPen, totalRect, -90, 360 * download.TotalProgress);
			}

			_downloadsRects[download] = imageRect;

			imageRect.X -= imageSize * 3 / 4;
		}

		var buttonRects = new Rectangle(Padding.Left, imageRect.Y, Width - Padding.Horizontal, imageRect.Height);
		expandRect = SlickButton.AlignAndDraw(e.Graphics, buttonRects, ContentAlignment.MiddleRight, new ButtonDrawArgs
		{
			BackgroundColor = FormDesign.Design.MenuColor,
			Icon = expanded ? "Shrink" : "Enlarge",
			Cursor = PointToClient(Cursor.Position),
			HoverState = HoverState & ~HoverState.Focused,
			ButtonType = ButtonType.Dimmed
		});

		buttonRects.X -= expandRect.Width + Padding.Right;
		if (_subscriptionsManager.IsRunning)
		{
			cancelRect = SlickButton.AlignAndDraw(e.Graphics, buttonRects, ContentAlignment.MiddleRight, new ButtonDrawArgs
			{
				BackgroundColor = FormDesign.Design.MenuColor,
				Icon = "StopSquare",
				Cursor = PointToClient(Cursor.Position),
				ColorStyle = ColorStyle.Red,
				HoverState = HoverState & ~HoverState.Focused,
				ButtonType = ButtonType.Dimmed
			});

			buttonRects.X -= cancelRect.Width + Padding.Right;
		}
		else
		{
			cancelRect = default;
		}

		var downloadsFailed = downloads.All(x => x.Stage == ModDownloadStage.Failed);
		var shouldResume = _subscriptionsManager.IsPaused || !_subscriptionsManager.IsRunning;
		pauseRect = SlickButton.AlignAndDraw(e.Graphics, buttonRects, ContentAlignment.MiddleRight, new ButtonDrawArgs
		{
			BackgroundColor = shouldResume ? default : FormDesign.Design.MenuColor,
			Icon = shouldResume ? downloadsFailed ? "Retry" : "Continue" : "Pause",
			Cursor = PointToClient(Cursor.Position),
			ColorStyle = downloadsFailed ? ColorStyle.Red : ColorStyle.Active,
			HoverState = HoverState & ~HoverState.Focused,
			ButtonType = _subscriptionsManager.IsPaused ? ButtonType.Active : shouldResume ? ButtonType.Normal : ButtonType.Dimmed
		});

		using var font = UI.Font(7f);
		using var textBrush = new SolidBrush(Color.FromArgb(200, FormDesign.Design.MenuForeColor));

		var text =
			_subscriptionsManager.IsRunning ? $"Downloading {downloads.Count} mods" :
			 $"{downloads.Count} mods queued";

		e.Graphics.DrawString(text, font, textBrush, clientRectangle.Pad(Padding));

		if (_subscriptionsManager.IsRunning && _subscriptionsManager.DownloadSpeed > 0)
		{
			DrawTopLabel(e,
				$"{_subscriptionsManager.DownloadSpeed.SizeString(_subscriptionsManager.DownloadSpeed > 1024 ? 1 : 0)}/s",
				"ReDownload",
				BackColor.MergeColor(FormDesign.Design.AccentColor));
		}
		else if (!_subscriptionsManager.IsRunning && downloadsFailed)
		{
			DrawTopLabel(e, "ERROR", "Warning", FormDesign.Design.RedColor);
		}

		using var brush = new SolidBrush(FormDesign.Design.MenuForeColor);
		using var activeBrush = new SolidBrush(FormDesign.Design.ActiveColor);

		var barRect = new Rectangle(Padding.Left, Height - Padding.Left - UI.Scale(6), Width - Padding.Horizontal, UI.Scale(6));

		e.Graphics.FillRoundedRectangle(brush, barRect, barRect.Height / 2);

		var activeBarRect = barRect.Pad(0, 0, (int)((1 - Math.Min(1, _subscriptionsManager.TotalProgress)) * barRect.Width), 0);

		if (activeBarRect.Width >= barRect.Height)
		{
			e.Graphics.FillRoundedRectangle(activeBrush, activeBarRect, barRect.Height / 2, topRight: activeBarRect.Width + (activeBarRect.Height / 2) > barRect.Width, botRight: activeBarRect.Width + (activeBarRect.Height / 2) > barRect.Width);
		}
	}

	private void DrawTopLabel(PaintEventArgs e, string text, string icon, Color backColor)
	{
		using var font = UI.Font(6.5f);
		using var backBrush = new SolidBrush(backColor);
		using var foreBrush = new SolidBrush(Color.FromArgb(220, backColor.GetTextColor()));
		using var stringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far };
		var textSize = e.Graphics.Measure(text, font);
		var prefferedHeight = font.Height + UI.Scale(2);
		var bitmaps = IconManager.GetIcons(icon);
		var key = bitmaps.Keys.Where(x => x >= font.Height).DefaultIfEmpty(bitmaps.Keys.Min()).Min();
		var labelRect = Rectangle.FromLTRB(0, Height - UI.Scale(64), Width, Height)
			.Pad(Padding)
			.Align(new Size(prefferedHeight + (string.IsNullOrEmpty(text) ? 0 : ((int)textSize.Width + UI.Scale(2))), prefferedHeight), ContentAlignment.TopRight);

		using var bitmap = new Bitmap(bitmaps[key], new Size(font.Height - UI.Scale(1), font.Height - UI.Scale(1))).Color(foreBrush.Color);

		labelRect.Y += Padding.Left;

		e.Graphics.FillRoundedRectangle(backBrush, labelRect, UI.Scale(2));
		e.Graphics.DrawString(text, font, foreBrush, labelRect.Pad(UI.Scale(2)), stringFormat);
		e.Graphics.DrawImage(bitmap, string.IsNullOrEmpty(text) ? labelRect.CenterR(bitmap.Size) : labelRect.Pad(UI.Scale(2)).Align(bitmap.Size, ContentAlignment.MiddleLeft));
	}

	private string GetText(ModDownloadStage status)
	{
		return status switch
		{
			ModDownloadStage.Pending => LocaleCS2.DownloadPending,
			ModDownloadStage.Started => LocaleCS2.DownloadStarted,
			ModDownloadStage.Downloading => LocaleCS2.Downloading,
			ModDownloadStage.CheckingIntegrity => LocaleCS2.CheckingIntegrity,
			ModDownloadStage.Processing => LocaleCS2.Processing,
			ModDownloadStage.CleaningUp => LocaleCS2.CleaningUp,
			ModDownloadStage.Completed => LocaleCS2.DownloadComplete,
			ModDownloadStage.Canceled => LocaleCS2.DownloadCancelled,
			ModDownloadStage.Failed => LocaleCS2.DownloadFailed,
			_ => status.ToString(),
		};
	}
}
