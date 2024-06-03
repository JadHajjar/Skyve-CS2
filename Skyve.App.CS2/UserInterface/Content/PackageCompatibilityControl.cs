using Skyve.Compatibility.Domain.Enums;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
internal class PackageCompatibilityControl : SlickControl
{
	private readonly INotifier _notifier;
	private readonly IPackageUtil _packageUtil;
	private IPackageIdentity Package { get; }

	public PackageCompatibilityControl(IPackageIdentity package)
	{
		Package = package;

		ServiceCenter.Get(out _notifier, out _packageUtil);

		_notifier.CompatibilityReportProcessed += Notifier_CompatibilityReportProcessed;
		_notifier.SnoozeChanged += Notifier_CompatibilityReportProcessed;
	}

	protected override void Dispose(bool disposing)
	{
		_notifier.CompatibilityReportProcessed -= Notifier_CompatibilityReportProcessed;
		_notifier.SnoozeChanged -= Notifier_CompatibilityReportProcessed;

		base.Dispose(disposing);
	}

	private void Notifier_CompatibilityReportProcessed()
	{
		this.TryInvoke(UIChanged);
	}

	protected override void UIChanged()
	{
		var compatibilityReport = Package.GetCompatibilityInfo();
		var notificationType = compatibilityReport?.GetNotification();
		var status = _packageUtil.GetStatus(Package, out _);

		Height = UI.Scale(32) * ((status <= DownloadStatus.OK ? 0 : 1) + (notificationType <= NotificationType.Info ? 0 : 1));
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		var compatibilityReport = Package.GetCompatibilityInfo();
		var notificationType = compatibilityReport?.GetNotification();
		var status = _packageUtil.GetStatus(Package, out _);

		var height = UI.Scale(32);

		if (notificationType > NotificationType.Info)
		{
			using var brush = new SolidBrush(notificationType.Value.GetColor().MergeColor(BackColor, 85));
			using var icon = IconManager.GetIcon("CompatibilityReport", height * 3 / 4).Color(brush.Color.GetTextColor());
			using var icon2 = notificationType.Value.GetIcon(true).Get(height * 3 / 4).Color(brush.Color.GetTextColor());
			var iconRect = new Rectangle(new Point((height - icon.Height) / 2, (height - icon.Height) / 2), icon.Size);
			var icon2Rect = new Rectangle(new Point(Width - icon.Width - ((height - icon.Height) / 2), (height - icon.Height) / 2), icon.Size);
			var text = LocaleCR.Get($"{notificationType}").One.ToUpper();
			var textRect = new Rectangle(iconRect.Right + iconRect.X, 0, Width - ((iconRect.Right + (iconRect.X * 2)) * 2), height);
			using var font = UI.Font(9.75F, FontStyle.Bold).FitToWidth(text, textRect, e.Graphics);
			using var textBrush = new SolidBrush(brush.Color.GetTextColor());
			using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

			e.Graphics.FillRectangle(brush, new Rectangle(0, textRect.Y, Width, height));
			e.Graphics.DrawImage(icon, iconRect);
			e.Graphics.DrawImage(icon2, icon2Rect);
			e.Graphics.DrawString(text, font, textBrush, textRect, format);
		}

		if (status > DownloadStatus.OK)
		{
			var text = "";
			var iconName = (DynamicIcon?)null;
			var color = Color.Empty;

			switch (_packageUtil.GetStatus(Package, out _))
			{
				case DownloadStatus.Unknown:
					text = Locale.StatusUnknown.One.ToUpper();
					iconName = "Question";
					color = FormDesign.Design.YellowColor;
					break;
				case DownloadStatus.OutOfDate:
					text = Locale.OutOfDate.One.ToUpper();
					iconName = "OutOfDate";
					color = FormDesign.Design.YellowColor;
					break;
				case DownloadStatus.PartiallyDownloaded:
					text = Locale.PartiallyDownloaded.One.ToUpper();
					iconName = "Broken";
					color = FormDesign.Design.RedColor;
					break;
				case DownloadStatus.Removed:
					text = Locale.RemovedByAuthor.One.ToUpper();
					iconName = "ContentRemoved";
					color = FormDesign.Design.RedColor;
					break;
			}

			using var brush = new SolidBrush(color.MergeColor(BackColor, 85));
			using var icon = iconName?.Get(height * 3 / 4).Color(brush.Color.GetTextColor());
			var iconRect = new Rectangle(new Point((height - icon.Height) / 2, (height - icon.Height) / 2), icon.Size);
			var icon2Rect = new Rectangle(new Point(Width - icon.Width - ((height - icon.Height) / 2), (height - icon.Height) / 2), icon.Size);
			var textRect = new Rectangle(iconRect.Right + iconRect.X, 0, Width - ((iconRect.Right + (iconRect.X * 2)) * 2), height);
			using var font = UI.Font(9.75F, FontStyle.Bold).FitToWidth(text, textRect, e.Graphics);
			using var textBrush = new SolidBrush(brush.Color.GetTextColor());
			using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

			if (notificationType > NotificationType.Info)
			{
				textRect.Y += height;
				iconRect.Y += height;
				icon2Rect.Y += height;
			}

			e.Graphics.FillRectangle(brush, new Rectangle(0, textRect.Y, Width, height));
			e.Graphics.DrawImage(icon, iconRect);
			e.Graphics.DrawImage(icon, icon2Rect);
			e.Graphics.DrawString(text, font, textBrush, textRect, format);
		}
	}
}
