﻿using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
public class DownloadsInfoControl : SlickControl
{
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly INotifier _notifier;

	private IWorkshopInfo? workshopInfo;

	public DownloadsInfoControl()
	{
		_subscriptionsManager = ServiceCenter.Get<ISubscriptionsManager>();
		_notifier = ServiceCenter.Get<INotifier>();

		_notifier.WorkshopInfoUpdated += Invalidate;
		_subscriptionsManager.UpdateDisplayNotification += SubscriptionsManager_UpdateDisplayNotification;

		Margin = default;
		Visible = false;
	}

	private async void SubscriptionsManager_UpdateDisplayNotification()
	{
		if (_subscriptionsManager.Status.ModId == 0)
		{
			workshopInfo = null;
		}

		Invalidate();

		if (_subscriptionsManager.Status.IsActive)
		{
			if (!Visible)
			{
				this.TryInvoke(Show);
			}
		}
		else
		{
			await Task.Delay(1000);

			if (!_subscriptionsManager.Status.IsActive)
			{
				this.TryInvoke(Hide);
			}
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
	}

	protected override void UIChanged()
	{
		Padding = UI.Scale(new Padding(3, 8, 3, 4), UI.FontScale);
		Height = (int)(60 * UI.FontScale);
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

		var workshopInfo = new GenericPackageIdentity(_subscriptionsManager.Status.ModId).GetWorkshopInfo();
		var thumbnail = workshopInfo?.GetThumbnail();
		var thumbRect = new Rectangle(new Point(Padding.Left, Padding.Top), UI.Scale(new Size(34, 34), UI.FontScale));

		if (thumbnail is null)
		{
			using var generic = Properties.Resources.I_PdxMods;

			e.Graphics.DrawRoundedImage(generic, thumbRect, (int)(5 * UI.FontScale), FormDesign.Design.MenuColor.MergeColor(FormDesign.Design.BackColor, 90));
		}
		else
		{
			e.Graphics.DrawRoundedImage(thumbnail, thumbRect, (int)(5 * UI.FontScale), FormDesign.Design.MenuColor.MergeColor(FormDesign.Design.BackColor, 90));
		}

		using var font = UI.Font(8.25F, FontStyle.Bold);
		using var smallFont = UI.Font(8.25F);
		using var brush = new SolidBrush(FormDesign.Design.MenuForeColor);
		using var activeBrush = new SolidBrush(FormDesign.Design.ActiveColor);

		e.Graphics.DrawString(workshopInfo?.CleanName() ?? _subscriptionsManager.Status.ModId.ToString(), font, brush, new Rectangle(thumbRect.Right + Padding.Left, Padding.Top - (Padding.Left / 2), Width - thumbRect.Right - Padding.Horizontal, 0).AlignToFontSize(font, ContentAlignment.TopLeft), new StringFormat { Trimming = StringTrimming.EllipsisCharacter });

		var barRect = new Rectangle(Padding.Left, Height - Padding.Bottom - (int)(8 * UI.FontScale), Width - Padding.Right, (int)(8 * UI.FontScale));

		e.Graphics.FillRoundedRectangle(brush, barRect, barRect.Height / 2);

		var activeBarRect = barRect.Pad(0, 0, (int)((1f - _subscriptionsManager.Status.Progress) * barRect.Width), 0);

		if (activeBarRect.Width >= barRect.Height)
		{
			e.Graphics.FillRoundedRectangle(activeBrush, activeBarRect, barRect.Height / 2, topRight: activeBarRect.Width + (activeBarRect.Height / 2) > barRect.Width, botRight: activeBarRect.Width + (activeBarRect.Height / 2) > barRect.Width);
		}

		var text = _subscriptionsManager.Status.Progress == 1f ? LocaleCS2.DonwloadComplete : LocaleCS2.Downloading;
		var bottomTextRect = new Rectangle(thumbRect.Right + Padding.Left, thumbRect.Bottom + Padding.Left, Width - thumbRect.Right - Padding.Horizontal, 0).AlignToFontSize(font, ContentAlignment.BottomLeft);

		e.Graphics.DrawString(text, smallFont, brush, bottomTextRect, new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Near });

		if (_subscriptionsManager.Status.Progress < 1f)
		{
			e.Graphics.DrawString($"{_subscriptionsManager.Status.Progress * 100:0}%", font, brush, bottomTextRect, new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Far });
		}
	}
}
