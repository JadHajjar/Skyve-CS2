using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
internal class UpdateAvailableControl : SlickControl
{
	public UpdateAvailableControl()
	{
		Visible = false;
		Cursor = Cursors.Hand;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		Height = UI.Scale(42);
		Padding = UI.Scale(new Padding(7, 5, 7, 5));
		Margin = UI.Scale(new Padding(0, 5, 0, 5));
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
		using var brush = Gradient(HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveForeColor : HoverState.HasFlag(HoverState.Hovered) ? Color.FromArgb(200, FormDesign.Design.ActiveColor) : FormDesign.Design.ActiveColor);
		e.Graphics.FillRoundedRectangle(brush, ClientRectangle.Pad(1), Padding.Left);

		{
			var textRect = ClientRectangle.Pad(Padding);
			textRect.Height = textRect.Height * 6 / 10;
			textRect.X += textRect.Height * 3 / 4;
			textRect.Width -= textRect.Height * 3 / 4;
			var text = LocaleCS2.UpdateAvailable.One.ToUpper();
			using var font = UI.Font(9.25F, FontStyle.Bold).FitTo(text, textRect, e.Graphics);
			using var textBrush = new SolidBrush(HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveColor : FormDesign.Design.ActiveForeColor);
			e.Graphics.DrawString(text, font, textBrush, textRect, format);

			using var icon = IconManager.GetIcon("OutOfDate", textRect.Height).Color(HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveColor : FormDesign.Design.ActiveForeColor);

			textRect.Width = 0;
			e.Graphics.DrawImage(icon, textRect.Align(icon.Size, ContentAlignment.MiddleRight));
		}

		{
			var textRect = ClientRectangle.Pad(Padding);
			textRect.Y += textRect.Height * 6 / 10;
			textRect.Height = textRect.Height * 4 / 10;
			var text = LocaleCS2.UpdateAvailableInfo;
			using var font = UI.Font(7.75F).FitTo(text, textRect, e.Graphics);
			using var textBrush = new SolidBrush(Color.FromArgb(200, HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveColor : FormDesign.Design.ActiveForeColor));
			e.Graphics.DrawString(text, font, textBrush, textRect, format);
		}
	}
}
