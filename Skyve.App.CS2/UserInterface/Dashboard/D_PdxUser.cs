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
		_workshopService.OnLogin += Invalidate;
		_workshopService.OnLogout += Invalidate;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			_userService.UserInfoUpdated -= Invalidate;
			_workshopService.OnLogin -= Invalidate;
			_workshopService.OnLogout -= Invalidate;
		}

		base.Dispose(disposing);
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		return Draw;
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.ParadoxAccount, "Paradox");
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
			DrawLoader(e.Graphics, new Rectangle(textRect.X, preferredHeight, 0, 0).Align(UI.Scale(new Size(18, 18)), ContentAlignment.TopLeft));

			e.Graphics.DrawStringItem(LocaleCS2.LoggingIn
				, Font
				, FormDesign.Design.ForeColor
				, textRect.Pad(UI.Scale(22), 0, 0, 0)
				, ref preferredHeight
				, applyDrawing);

			preferredHeight += Padding.Top / 2;

			return;
		}

		var dotRect = new Rectangle(textRect.X, preferredHeight - (Margin.Top / 2), 0, 0);
		using var dotBrush = new SolidBrush(!_workshopService.IsLoggedIn ? FormDesign.Design.RedColor : FormDesign.Design.GreenColor);

		e.Graphics.DrawStringItem(!_workshopService.IsLoggedIn ? LocaleCS2.NotLoggedInCheckNotification : Locale.LoggedInUser.Format(_userService.User.Name.IfEmpty("Unknown User"))
			, Font
			, FormDesign.Design.ForeColor
			, textRect.Pad(UI.Scale(16), 0, 0, 0)
			, ref preferredHeight
			, applyDrawing);

		dotRect.Height = preferredHeight - dotRect.Y;

		e.Graphics.FillEllipse(dotBrush, dotRect.Align(UI.Scale(new Size(10, 10)), ContentAlignment.MiddleLeft));

		preferredHeight += Padding.Top / 2;

		var notification = _notificationsService.GetNotifications<ParadoxLoginRequiredNotification>().FirstOrDefault();
		if (notification is not null)
		{
			using var font = UI.Font(7.5F);

			DrawButton(e, applyDrawing, ref preferredHeight, notification.OnClick, new ButtonDrawArgs
			{
				Icon = "User",
				Font = font,
				Size = new Size(0, UI.Scale(20)),
				Text = LocaleCS2.LoginToParadox,
				Rectangle = e.ClipRectangle.Pad(BorderRadius)
			});

			preferredHeight += BorderRadius / 2;
		}

		if (_workshopService.IsLoggedIn)
		{
			preferredHeight += UI.Scale(_userService.User.Manager ? 16 : 12);
			var rect = Rectangle.FromLTRB(e.ClipRectangle.Left + UI.Scale(8), e.ClipRectangle.Bottom - UI.Scale(20), e.ClipRectangle.Right - BorderRadius, e.ClipRectangle.Bottom - (BorderRadius / 2));

			_buttonActions.Add(rect.Pad(rect.Width / 2, 0, 0, 0), async () => await _workshopService.Logout());

			if (applyDrawing)
			{
				var hovered = rect.Pad(rect.Width / 2, 0, 0, 0).Contains(CursorLocation);
				using var font = UI.Font(7F, hovered ? FontStyle.Underline : FontStyle.Regular);
				using var brush = new SolidBrush(hovered ? FormDesign.Design.ActiveColor : FormDesign.Design.InfoColor);
				using var format = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

				e.Graphics.DrawString(LocaleCS2.Logout, font, brush, rect, format);

				if (_userService.User.Manager)
				{
					using var font2 = UI.Font(7F, FontStyle.Bold);
					using var brush2 = new SolidBrush(FormDesign.Design.OrangeColor);
					using var format2 = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

					e.Graphics.DrawString("Manager", font2, brush2, rect, format2);
				}
			}
		}
	}
}
