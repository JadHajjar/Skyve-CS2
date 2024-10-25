using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
internal class OutOfDatePackagesControl : SlickControl
{
	private List<IPackage> packages = [];

	public void SetPackages(List<IPackage> value)
	{
		packages = value;
		Invalidate();
	}

	public OutOfDatePackagesControl()
	{
		AutoInvalidate = false;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp();

		var imageRect = new Rectangle(UI.Scale(new Point(5, 5)), UI.Scale(new Size(32, 32)));

		foreach (var item in packages)
		{
			var image = item.GetThumbnail();

			e.Graphics.FillRoundShadow(imageRect.Pad(UI.Scale(-3)), Color.FromArgb(50, 0, 0, 0));

			if (image is not null)
			{
				if (item!.IsLocal())
				{
					using var unsatImg = new Bitmap(image, imageRect.Size).Tint(Sat: 0);
					e.Graphics.DrawRoundImage(unsatImg, imageRect);
				}
				else
				{
					e.Graphics.DrawRoundImage(image, imageRect);
				}
			}
			else
			{
				using var generic = IconManager.GetIcon("Package", imageRect.Height).Color(BackColor);
				using var iconBrush = new SolidBrush(FormDesign.Design.IconColor);

				e.Graphics.FillEllipse(iconBrush, imageRect);
				e.Graphics.DrawImage(generic, imageRect.CenterR(generic.Size));
			}

			imageRect.X += imageRect.Width / 2;
		}

		using var brush = new SolidBrush(ForeColor);
		var rect = Rectangle.FromLTRB(0, imageRect.Bottom + UI.Scale(3), Width, 9999).Pad(UI.Scale(3));
		var text = packages.Count == 1 ? Locale.PackageIsOutOfDateVersion.Format(packages[0].CleanName()) : string.Format(Locale.PackageIsOutOfDateVersion.Plural, packages.Count.ToString(), Locale.Package.Plural);
		
		e.Graphics.DrawString(text, base.Font, brush, rect);

		Height = rect.Y + (int)e.Graphics.Measure(text, Font, rect.Width).Height + UI.Scale(3);
	}
}
