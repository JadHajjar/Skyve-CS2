using Skyve.App.UserInterface.Dashboard;
using Skyve.Domain.CS2.Notifications;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_PdxUser : IDashboardItem
{
	private readonly IUserService _userService;
	private readonly IWorkshopService _workshopService;
	private readonly INotificationsService _notificationsService;

	public D_PdxUser()
	{
		ServiceCenter.Get(out _userService, out _workshopService, out _notificationsService);

		_userService.UserInfoUpdated += Invalidate;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			_userService.UserInfoUpdated -= Invalidate;
		}

		base.Dispose(disposing);
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		return Draw;
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.ParadoxAccount, "Paradox");

		var loading = Loading;
		Loading = string.IsNullOrEmpty(_userService.User.Id?.ToString()) && _workshopService.IsLoginPending;

		if (loading != Loading)
		{
			OnResizeRequested();
		}

		var textRect = e.ClipRectangle.Pad(Margin);

		if (Loading)
		{
			DrawLoader(e.Graphics, new Rectangle(textRect.X, preferredHeight, 0, 0).Align(UI.Scale(new Size(18, 18), UI.FontScale), ContentAlignment.TopLeft));

			e.Graphics.DrawStringItem(LocaleCS2.LoggingIn
				, Font
				, FormDesign.Design.ForeColor
				, textRect.Pad((int)(22 * UI.FontScale), 0, 0, 0)
				, ref preferredHeight
				, applyDrawing);

			preferredHeight += Padding.Top / 2;

			return;
		}

		var dotRect = new Rectangle(textRect.X, preferredHeight - (Margin.Top / 2), 0, 0);
		using var dotBrush = new SolidBrush(string.IsNullOrWhiteSpace(_userService.User.Name) ? FormDesign.Design.RedColor : FormDesign.Design.GreenColor);

		e.Graphics.DrawStringItem(string.IsNullOrWhiteSpace(_userService.User.Name) ? LocaleCS2.NotLoggedInCheckNotification : Locale.LoggedInUser.Format(_userService.User.Name)
			, Font
			, FormDesign.Design.ForeColor
			, textRect.Pad((int)(16 * UI.FontScale), 0, 0, 0)
			, ref preferredHeight
			, applyDrawing);

		dotRect.Height = preferredHeight - dotRect.Y;

		e.Graphics.FillEllipse(dotBrush, dotRect.Align(UI.Scale(new Size(10, 10), UI.FontScale), ContentAlignment.MiddleLeft));

		preferredHeight += Padding.Top / 2;

		var notification = _notificationsService.GetNotifications<ParadoxLoginRequiredNotification>().FirstOrDefault();
		if (notification is not null)
		{
			using var font = UI.Font(7.5F);

			DrawButton(e, applyDrawing, ref preferredHeight, notification.OnClick, new ButtonDrawArgs
			{
				Icon = "User",
				Font = font,
				Size = new Size(0, (int)(20 * UI.FontScale)),
				Text = LocaleCS2.LoginToParadox,
				Rectangle = e.ClipRectangle
			});
		}
	}
}
