using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
public class PackageTitleControl : SlickControl
{
	public IPackageIdentity Package { get; set; }

	public PackageTitleControl(IPackageIdentity package)
	{
		Package = package;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		var workshopInfo = Package.GetWorkshopInfo();
		var text = (workshopInfo ?? Package).CleanName(out var tags);

		if (tags.Count == 0)
		{
			PaintText(e, text, ClientRectangle, out var font_);

			font_.Dispose();

			return;
		}

		var tagRects = tags.ToList(x => new Rectangle(default, e.Graphics.MeasureLabel(x.Text, null, large: false)));

		for (var i = 1; i < tagRects.Count; i++)
		{
			tagRects[i] = new Rectangle(new(tagRects[i - 1].Right + Padding.Left, tagRects[i - 1].Y), tagRects[i].Size);

			if (tagRects[i].Right > Width)
			{
				tagRects[i] = new Rectangle(new(0, tagRects[i - 1].Bottom + Padding.Top), tagRects[i].Size);
			}
		}

		PaintText(e, text, ClientRectangle.Pad(0, 0, 0, tagRects.Max(x => x.Bottom)), out var font);

		var textSize = (int)e.Graphics.Measure(text, font, Width).Height;

		font.Dispose();

		for (var i = 0; i < tagRects.Count; i++)
		{
			e.Graphics.DrawLabel(tags[i].Text, null, tags[i].Color, tagRects[i].Pad(0, textSize, 0, 0), ContentAlignment.TopLeft, large: false);
		}
	}

	private void PaintText(PaintEventArgs e, string text, Rectangle textRect, out Font font)
	{
		font = UI.Font(12.5F, FontStyle.Bold).FitTo(text, textRect.Pad((int)(2 * UI.FontScale)), e.Graphics);

		using var brush = new SolidBrush(ForeColor);

		e.Graphics.DrawString(text, font, brush, textRect);
	}
}
