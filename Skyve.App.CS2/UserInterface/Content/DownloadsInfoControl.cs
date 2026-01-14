using Skyve.App.Interfaces;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
public class DownloadsInfoControl : SlickControl
{
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly INotifier _notifier;
	private Rectangle cancelRect;

	public DownloadsInfoControl()
	{
		_subscriptionsManager = ServiceCenter.Get<ISubscriptionsManager>();
		_notifier = ServiceCenter.Get<INotifier>();

		_notifier.WorkshopInfoUpdated += Invalidate;
		_subscriptionsManager.UpdateDisplayNotification += SubscriptionsManager_UpdateDisplayNotification;

		Margin = default;
		Visible = false;
		Cursor = Cursors.Hand;
		Loading = true;
	}

	private void SubscriptionsManager_UpdateDisplayNotification()
	{
		this.TryInvoke(() => Visible = !Visible);
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		//var workshopInfo = new GenericPackageIdentity(download.ModId).GetWorkshopInfo();

		//if (workshopInfo is null)
		//{
		//	return;
		//}

		//if (ClientRectangle.Pad(Padding - new Padding(Padding.Left)).Pad(0, 0, 0, UI.Scale(20)).Contains(e.Location))
		//{
		//	if (e.Button == MouseButtons.Left)
		//	{
		//		ServiceCenter.Get<IAppInterfaceService>().OpenPackagePage(workshopInfo);
		//	}
		//	else if (e.Button == MouseButtons.Right)
		//	{
		//		SlickToolStrip.Show(App.Program.MainForm, ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems(workshopInfo));
		//	}
		//}
		//else if (cancelRect.Contains(e.Location))
		//{
		//	ServiceCenter.Get<IWorkshopService>().CancelActions();
		//}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
	}

	protected override void UIChanged()
	{
		Padding = UI.Scale(new Padding(3, 8, 3, 4));
		Height = UI.Scale(80);
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

		var y = Padding.Top;

		foreach (var download in _subscriptionsManager.GetDownloads())
		{
			var workshopInfo = download.Mod;
			var thumbnail = workshopInfo?.GetThumbnail();
			var thumbRect = new Rectangle(new Point(Padding.Left, y), UI.Scale(new Size(34, 34)));

			SlickTip.SetTo(this, workshopInfo?.CleanName() ?? download.Mod.Id.ToString(), download.TotalBytesToDownload > 0 ? (download.DownloadedBytes.SizeString(1) + "/" + download.TotalBytesToDownload.SizeString(1)) : null);

			if (thumbnail is null)
			{
				using var backBrush = new SolidBrush(Color.FromArgb(40, FormDesign.Design.ForeColor));

				e.Graphics.FillRoundedRectangle(backBrush, thumbRect, Margin.Left / 2);

				using var icon = IconManager.GetIcon("Paradox", thumbRect.Width * 3 / 4).Color(FormDesign.Design.ForeColor);

				e.Graphics.DrawImage(icon, thumbRect.CenterR(icon.Size));
			}
			else
			{
				e.Graphics.DrawRoundedImage(thumbnail, thumbRect, UI.Scale(5));
			}

			using var font = UI.Font(8.25F, FontStyle.Bold);
			using var smallFont = UI.Font(7.5F);
			using var brush = new SolidBrush(FormDesign.Design.MenuForeColor);
			using var activeBrush = new SolidBrush(FormDesign.Design.ActiveColor);

			e.Graphics.DrawString(workshopInfo?.CleanName() ?? download.Mod.Id.ToString(), font, brush, new Rectangle(thumbRect.Right + Padding.Left, y, Width - thumbRect.Right - Padding.Horizontal, 0).AlignToFontSize(font, ContentAlignment.TopLeft), new StringFormat { Trimming = StringTrimming.EllipsisCharacter });

			var barRect = new Rectangle(Padding.Left, y + UI.Scale(28) + Padding.Top, Width - Padding.Horizontal, UI.Scale(8));

			e.Graphics.FillRoundedRectangle(brush, barRect, barRect.Height / 2);

			var activeBarRect = barRect.Pad(0, 0, (int)((1f - download.TotalProgress) * barRect.Width), 0);

			if (activeBarRect.Width >= barRect.Height)
			{
				e.Graphics.FillRoundedRectangle(activeBrush, activeBarRect, barRect.Height / 2, topRight: activeBarRect.Width + (activeBarRect.Height / 2) > barRect.Width, botRight: activeBarRect.Width + (activeBarRect.Height / 2) > barRect.Width);
			}

			var text = GetText(download.Stage);
			var bottomTextRect = new Rectangle(thumbRect.Right + Padding.Left, thumbRect.Bottom, Width - thumbRect.Right - Padding.Horizontal, 0).AlignToFontSize(font, ContentAlignment.BottomLeft);

			e.Graphics.DrawString(text, smallFont, brush, bottomTextRect, new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Near });

			if (download.TotalProgress.IsWithin(0, 1))
			{
				e.Graphics.DrawString($"{download.TotalProgress * 100:0}%", font, brush, bottomTextRect, new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Far });
			}

			if (HoverState.HasFlag(HoverState.Hovered) && ClientRectangle.Pad(Padding - new Padding(Padding.Left)).Pad(0, 0, 0, UI.Scale(20)).Contains(PointToClient(Cursor.Position)))
			{
				using var backBrush = new SolidBrush(Color.FromArgb(50, FormDesign.Design.ActiveColor));
				e.Graphics.FillRoundedRectangle(backBrush, ClientRectangle.Pad(Padding - new Padding(Padding.Left)).Pad(0, 0, 0, UI.Scale(20)), Padding.Left * 2);
			}

			y += thumbRect.Height + Padding.Vertical + Padding.Top;
		}

		Height = y;

		//if (download.IsActive)
		//{
		//	using var tinyFont = UI.Font(7.5F);
		//	cancelRect = SlickButton.AlignAndDraw(e.Graphics, ClientRectangle, ContentAlignment.BottomLeft, new ButtonDrawArgs
		//	{
		//		Icon = "Cancel",
		//		Text = LocaleSlickUI.Cancel,
		//		Padding = UI.Scale(new Padding(2)),
		//		Font = tinyFont,
		//		Cursor = PointToClient(Cursor.Position),
		//		HoverState = HoverState
		//	});
		//}
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
