using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
internal class LogReportControl : SlickImageControl
{
	public LogReportControl()
	{
		Cursor = Cursors.Hand;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		Font = UI.Font(9.75F, FontStyle.Bold);
		Padding = UI.Scale(new Padding(10));
		Size = UI.Scale(new Size(150, 75));
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		var fileRect = ClientRectangle.Pad(Padding);

		SlickButton.GetColors(out var textColor, out var backColor, HoverState, ColorStyle.Text, buttonType: ButtonType.Active);

		e.Graphics.FillRoundedRectangleWithShadow(ClientRectangle.Pad(Padding.Left / 2), Padding.Left / 2, Padding.Left / 2, backColor, Color.FromArgb(10, backColor));

		using var fileIcon = ImageName.Large.Color(textColor);
		using var font = UI.Font(9.75F).FitTo(Text, fileRect.Pad(0, fileIcon.Height + (Padding.Top / 2), 0, 0), e.Graphics);

		var textSize = e.Graphics.Measure(Text, font, fileRect.Width);
		var fileHeight = (int)textSize.Height + fileIcon.Height + (Padding.Top / 2);

		fileRect = fileRect.CenterR(fileRect.Width, fileHeight);

		var iconRect = fileRect.Align(fileIcon.Size, ContentAlignment.TopCenter);

		if (Loading)
		{
			DrawLoader(e.Graphics, iconRect);
		}
		else
		{
			e.Graphics.DrawImage(fileIcon, iconRect);
		}

		using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
		using var brush = new SolidBrush(textColor);

		e.Graphics.DrawString(Text, font, brush, fileRect, format);
	}
}
