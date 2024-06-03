using Skyve.App.Interfaces;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
public class DownloadsInfoControl : SlickControl
{
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly INotifier _notifier;

	public DownloadsInfoControl()
	{
		_subscriptionsManager = ServiceCenter.Get<ISubscriptionsManager>();
		_notifier = ServiceCenter.Get<INotifier>();

		_notifier.WorkshopInfoUpdated += Invalidate;
		_subscriptionsManager.UpdateDisplayNotification += SubscriptionsManager_UpdateDisplayNotification;

		Margin = default;
		Visible = false;
		Cursor = Cursors.Hand;
	}

	private async void SubscriptionsManager_UpdateDisplayNotification()
	{
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
			await Task.Delay(1500);

			if (!_subscriptionsManager.Status.IsActive)
			{
				this.TryInvoke(Hide);
			}
		}
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		var workshopInfo = new GenericPackageIdentity(_subscriptionsManager.Status.ModId).GetWorkshopInfo();

		if (workshopInfo is null)
		{
			return;
		}

		if (e.Button == MouseButtons.Left)
		{
			ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(workshopInfo);
		}
		else if (e.Button == MouseButtons.Right)
		{
			SlickToolStrip.Show(App.Program.MainForm, ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems(workshopInfo));
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

		SlickTip.SetTo(this, workshopInfo?.CleanName() ?? _subscriptionsManager.Status.ModId.ToString(), _subscriptionsManager.Status.TotalSize > 0 ? (_subscriptionsManager.Status.ProcessedBytes.SizeString(1) + "/" + _subscriptionsManager.Status.TotalSize.SizeString(1)) : null);

		if (thumbnail is null)
		{
			using var backBrush = new SolidBrush(Color.FromArgb(40, FormDesign.Design.ForeColor));

			e.Graphics.FillRoundedRectangle(backBrush, thumbRect, Margin.Left / 2);

			using var icon = IconManager.GetIcon("Paradox", thumbRect.Width * 3 / 4).Color(FormDesign.Design.ForeColor);

			e.Graphics.DrawImage(icon, thumbRect.CenterR(icon.Size));
		}
		else
		{
			e.Graphics.DrawRoundedImage(thumbnail, thumbRect, (int)(5 * UI.FontScale));
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

		var text = _subscriptionsManager.Status.Progress == 1f ? LocaleCS2.DownloadComplete : _subscriptionsManager.Status.Progress == -1f ? LocaleCS2.DownloadFailed : LocaleCS2.Downloading;
		var bottomTextRect = new Rectangle(thumbRect.Right + Padding.Left, thumbRect.Bottom + Padding.Left, Width - thumbRect.Right - Padding.Horizontal, 0).AlignToFontSize(font, ContentAlignment.BottomLeft);

		e.Graphics.DrawString(text, smallFont, brush, bottomTextRect, new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Near });

		if (_subscriptionsManager.Status.Progress < 1f)
		{
			e.Graphics.DrawString($"{_subscriptionsManager.Status.Progress * 100:0}%", font, brush, bottomTextRect, new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Far });
		}

		if (HoverState.HasFlag(HoverState.Hovered))
		{
			using var backBrush = new SolidBrush(Color.FromArgb(50, FormDesign.Design.ActiveColor));
			e.Graphics.FillRoundedRectangle(backBrush, ClientRectangle.Pad(Padding - new Padding(Padding.Left)), Padding.Left*2);
		}
	}
}
