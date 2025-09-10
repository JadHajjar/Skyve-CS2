using PDX.SDK.Contracts.Service.Mods.Enums;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
internal class AccessLevelControlDropdown : SlickSelectionDropDown<ModAccessControlLevelState>
{
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);

		if (Live)
		{
			Items = [ModAccessControlLevelState.Public, ModAccessControlLevelState.Unlisted, ModAccessControlLevelState.Private];
		}
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		Width = UI.Scale(150);
	}

	protected override void PaintItem(PaintEventArgs e, Rectangle rectangle, Color foreColor, HoverState hoverState, ModAccessControlLevelState item)
	{
		var text = item.ToString();

		using var brush = new SolidBrush(foreColor);
		using var font = UI.Font(8.25F).FitTo(text, rectangle, e.Graphics);
		using var format = new StringFormat { LineAlignment = StringAlignment.Center };

		e.Graphics.DrawString(text, font, brush, rectangle, format);
	}
}
