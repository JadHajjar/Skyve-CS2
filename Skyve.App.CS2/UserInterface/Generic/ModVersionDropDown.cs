using System.Drawing;
using System.Windows.Forms;


namespace Skyve.App.CS2.UserInterface.Generic;

public class ModVersionDropDown : SlickSelectionDropDown<IModChangelog>
{
	private readonly IModUtil _modUtil;
	private readonly IPackageIdentity _package;

	public ModVersionDropDown(IPackageIdentity package)
	{
		_modUtil = ServiceCenter.Get<IModUtil>();
		_package = package;

		Text = "Selected Version";
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		Height += Padding.Top;
	}

	protected override void PaintItem(PaintEventArgs e, Rectangle rectangle, Color foreColor, HoverState hoverState, IModChangelog item)
	{
		if (item == null)
		{
			return;
		}

		rectangle.Width -= Padding.Left;

		if (_modUtil.GetSelectedVersion(_package) == item.VersionId)
		{
			using var icon = IconManager.GetIcon("Ok", rectangle.Height * 3 / 4).Color(FormDesign.Design.GreenColor);

			e.Graphics.DrawImage(icon, rectangle.Align(icon.Size, ContentAlignment.MiddleLeft));

			rectangle = rectangle.Pad(icon.Width + Padding.Left, 0, 0, 0);
		}

		using var brush = new SolidBrush(foreColor);
		using var format1 = new StringFormat { LineAlignment = StringAlignment.Center };

		using var font1 = UI.Font(8.25F).FitTo(item.Version, rectangle, e.Graphics);
		e.Graphics.DrawString(item.Version, font1, brush, rectangle, format1);

		if (item.ReleasedDate != null)
		{
			var text = item.ReleasedDate.Value.ToLocalTime().ToReadableString(item.ReleasedDate.Value.Year != DateTime.UtcNow.Year, ExtensionClass.DateFormat.MDY);
			using var font2 = UI.Font(8.25F).FitTo(text, rectangle, e.Graphics);
			using var format2 = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far };
			e.Graphics.DrawString(text, font2, brush, rectangle, format2);
		}
	}
}
