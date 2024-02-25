using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
internal class LogLevelDropdown : SlickSelectionDropDown<string>
{
    public LogLevelDropdown()
    {
		Items = [
			"DEFAULT",
			"DISABLED",
			"EMERGENCY",
			"FATAL",
			"CRITICAL",
			"ERROR",
			"WARN",
			"INFO",
			"DEBUG",
			"TRACE",
			"VERBOSE",
			"ALL"
			];
	}

    protected override void PaintItem(PaintEventArgs e, Rectangle rectangle, Color foreColor, HoverState hoverState, string text)
	{
		using var font = UI.Font(8.25F).FitToWidth(text, rectangle, e.Graphics);
		using var brush = new SolidBrush(foreColor);
		using var format = new StringFormat { LineAlignment = StringAlignment.Center };
		
		e.Graphics.DrawString(text, font, brush, rectangle, format);
	}
}
