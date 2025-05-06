using Skyve.App.UserInterface.Lists;

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
internal class OutOfDatePackagesControl : SlickControl
{
	private List<IPackageIdentity> packages = [];

	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Bindable(true)]
	public override string Text
	{
		get => base.Text; set
		{
			base.Text = value;
			UIChanged();
		}
	}

	public void SetPackages(IEnumerable<IPackageIdentity> value)
	{
		packages = value.ToList();
		Invalidate();
	}

	public OutOfDatePackagesControl()
	{
		AutoInvalidate = false;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		if (Loading)
		{
			DrawLoader(e.Graphics, ClientRectangle);

			Height = UI.Scale(32);

			return;
		}

		var imageRect = new Rectangle(UI.Scale(new Point(5, 5)), UI.Scale(new Size(32, 32)));

		foreach (var item in packages)
		{
			var image = item.GetThumbnail();

			e.Graphics.FillRoundShadow(imageRect.Pad(UI.Scale(-3)), Color.FromArgb(50, 0, 0, 0));

			if (image is not null)
			{
				if (item.IsLocal())
				{
					using var unsatImg = image.ToGrayscale();
					e.Graphics.DrawRoundImage(unsatImg, imageRect);
				}
				else
				{
					e.Graphics.DrawRoundImage(image, imageRect);
				}
			}
			else
			{
				image = item.IsLocal() ? ItemListControl.PackageThumbUnsat : ItemListControl.PackageThumb;

				e.Graphics.DrawRoundImage(image, imageRect);
			}

			imageRect.X += imageRect.Width / 2;
		}

		if (imageRect.Right > Width)
		{
			using var shadeBrush = new LinearGradientBrush(Rectangle.FromLTRB(Width - UI.Scale(40), 0, Width, Height), default, BackColor, 0f);
			e.Graphics.FillRectangle(shadeBrush, Rectangle.FromLTRB(Width - UI.Scale(40), 0, Width, Height));
		}

		using var brush = new SolidBrush(ForeColor);
		var rect = Rectangle.FromLTRB(0, imageRect.Bottom + UI.Scale(3), Width, 9999).Pad(UI.Scale(3));
		var format = LocaleHelper.GetGlobalText(Text);
		var text = packages.Count == 1 ? format.Format(packages[0].CleanName()) : string.Format(format.Plural, packages.Count.ToString(), Locale.Package.Plural);
		
		e.Graphics.DrawString(text, base.Font, brush, rect);

		Height = rect.Y + (int)e.Graphics.Measure(text, Font, rect.Width).Height + UI.Scale(3);
	}
}
