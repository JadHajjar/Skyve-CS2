using Skyve.Compatibility.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
internal class DependencyResolutionDropdown : SlickSelectionDropDown<DependencyResolveBehavior>
{
	public SkyvePage SkyvePage { get; set; }

	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);

		if (Live)
		{
			Items = Enum.GetValues(typeof(DependencyResolveBehavior)).Cast<DependencyResolveBehavior>().ToArray();

			selectedItem = ServiceCenter.Get<ISettings>().UserSettings.DependencyResolution;
		}
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Middle)
		{
			SelectedItem = DependencyResolveBehavior.Automatic;
		}

		base.OnMouseClick(e);
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		Width = UI.Scale(250);
	}

	protected override bool SearchMatch(string searchText, DependencyResolveBehavior item)
	{
		return searchText.SearchCheck(LocaleHelper.GetGlobalText($"Dependency_{item}"));
	}

	public override void ResetValue()
	{

	}

	protected override void PaintItem(PaintEventArgs e, Rectangle rectangle, Color foreColor, HoverState hoverState, DependencyResolveBehavior item)
	{
		var text = LocaleHelper.GetGlobalText($"Dependency_{item}");

		using var brush = new SolidBrush(foreColor);
		using var font = UI.Font(8.25F).FitTo(text, rectangle, e.Graphics);
		using var format = new StringFormat { LineAlignment = StringAlignment.Center };
		e.Graphics.DrawString(text, font, brush, rectangle, format);
	}
}
