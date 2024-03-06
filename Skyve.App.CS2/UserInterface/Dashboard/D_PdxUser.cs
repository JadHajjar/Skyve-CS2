using Skyve.App.UserInterface.Dashboard;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_PdxUser : IDashboardItem
{
	private readonly IUserService _userService;
	private readonly IWorkshopService _workshopService;

	public D_PdxUser()
	{
		ServiceCenter.Get(out _userService, out _workshopService);

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
		DrawSection(e, applyDrawing, e.ClipRectangle, LocaleCS2.ParadoxAccount, "I_Paradox", out var fore, ref preferredHeight);

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
				, fore
				, textRect.Pad((int)(22 * UI.FontScale), 0, 0, 0)
				, ref preferredHeight
				, applyDrawing);

			preferredHeight += Padding.Top / 2;

			return;
		}

		e.Graphics.DrawStringItem(string.IsNullOrWhiteSpace(_userService.User.Name) ? LocaleCS2.NotLoggedInCheckNotification : Locale.LoggedInUser.Format(_userService.User.Name)
			, Font
			, fore
			, textRect
			, ref preferredHeight
			, applyDrawing);

		preferredHeight += Padding.Top / 2;
	}
}
