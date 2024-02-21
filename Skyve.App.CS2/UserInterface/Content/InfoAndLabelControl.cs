using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
internal class InfoAndLabelControl : SlickControl
{
	private string? _valueText;

	[Category("Behavior")]
	public string? LabelText { get; set; }

	[Category("Behavior")]
	public string? ValueText
	{
		get => _valueText; set
		{
			_valueText = value;
			UIChanged();
		}
	}

	protected override void UIChanged()
	{
		if (string.IsNullOrWhiteSpace(ValueText))
		{
			Height = 0;
		}
		else
		{
			Height = (int)(32 * UI.FontScale);
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		if (string.IsNullOrWhiteSpace(ValueText))
		{
			return;
		}

		var labelText = LocaleHelper.GetGlobalText(LabelText);
		var valueText = LocaleHelper.GetGlobalText(ValueText);

		using var fontLabel = UI.Font(7F, FontStyle.Bold).FitToWidth(labelText, ClientRectangle, e.Graphics);
		using var fontValue = UI.Font(8.25F).FitToWidth(valueText, ClientRectangle, e.Graphics);

		using var brushLabel = new SolidBrush(FormDesign.Design.InfoColor);
		using var brushValue = new SolidBrush(FormDesign.Design.ForeColor);

		e.Graphics.DrawString(labelText, fontLabel, brushLabel, ClientRectangle);
		e.Graphics.DrawString(valueText, fontValue, brushValue, ClientRectangle);
	}
}
