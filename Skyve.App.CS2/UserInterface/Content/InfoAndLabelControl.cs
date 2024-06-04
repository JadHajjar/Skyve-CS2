using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
internal class InfoAndLabelControl : SlickControl
{
	public event EventHandler? ValueClicked;

	private string? _valueText;
	private Rectangle clickRect;

	[Category("Behavior"), DefaultValue(null)]
	public Color? ValueColor { get; set; }
	[Category("Behavior"), DefaultValue(null)]
	public string? LabelText { get; set; }

	[Category("Behavior"), DefaultValue(null)]
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
		if (string.IsNullOrWhiteSpace(ValueText) && !DesignMode)
		{
			Height = 0;
		}
		else
		{
			Height = UI.Scale(35);
		}

		Padding = UI.Scale(new Padding(3));
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);

		Cursor = clickRect.Contains(e.Location) ? Cursors.Hand : Cursors.Default;
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		if (e.Button is MouseButtons.Left && clickRect.Contains(e.Location))
		{
			if (ValueClicked is null)
			{
				Clipboard.SetText(ValueText);
			}
			else
			{
				ValueClicked(this, e);
			}
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		if (string.IsNullOrWhiteSpace(ValueText))
		{
			return;
		}

		var labelText = (HoverState.HasFlag(HoverState.Hovered) && ValueClicked == null && clickRect.Contains(PointToClient(Cursor.Position)) ? LocaleSlickUI.Copy : LocaleHelper.GetGlobalText(LabelText)).One.ToUpper();
		var valueText = ValueText;

		using var fontLabel = UI.Font(6.5F, FontStyle.Bold).FitToWidth(labelText, ClientRectangle.Pad(Padding), e.Graphics);
		using var fontValue = UI.Font(8.5F).FitToWidth(valueText, ClientRectangle.Pad(Padding), e.Graphics);

		using var brushLabel = new SolidBrush(Color.FromArgb(125, FormDesign.Design.ForeColor.MergeColor(FormDesign.Design.InfoColor)));
		using var brushValue = new SolidBrush(ValueColor ?? FormDesign.Design.ForeColor);

		var valueRect = new Rectangle(new(Padding.Left, Padding.Top), e.Graphics.Measure(valueText, fontValue).ToSize());
		var labelRect = new Rectangle(new(valueRect.X, valueRect.Bottom), e.Graphics.Measure(labelText, fontLabel).ToSize());

		clickRect = Rectangle.Intersect(ClientRectangle, Rectangle.Union(valueRect, labelRect).InvertPad(Padding));

		if (HoverState.HasFlag(HoverState.Hovered) && clickRect.Contains(PointToClient(Cursor.Position)))
		{
			using var brush = new SolidBrush(Color.FromArgb(100, ValueColor ?? FormDesign.Design.ActiveColor));

			e.Graphics.FillRoundedRectangle(brush, clickRect.Pad(1), Padding.Left);
		}

		e.Graphics.DrawString(labelText, fontLabel, brushLabel, labelRect.Location);
		e.Graphics.DrawString(valueText, fontValue, brushValue, valueRect.Location);
	}
}
