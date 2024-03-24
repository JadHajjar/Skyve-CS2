using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
internal class PlaysetUsageSelection : SlickControl
{
	public event EventHandler? SelectedChanged;

	public PackageUsage Usage { get; }
	public bool Selected { get; set; }

	public PlaysetUsageSelection(PackageUsage usage)
	{
		Usage = usage;
	}

	protected override void UIChanged()
	{
		Padding = UI.Scale(new Padding(12), UI.FontScale);
		Font = UI.Font(9F, FontStyle.Bold);
		Size = UI.Scale(new Size(100, 100), UI.FontScale);

		base.UIChanged();
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);

		Cursor = ClientRectangle.Pad(Padding).Contains(e.Location) ? Cursors.Hand : Cursors.Default;
	}

	protected override async void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		if (e.Button == MouseButtons.Left && ClientRectangle.Pad(Padding).Contains(e.Location))
		{
			foreach (PlaysetUsageSelection item in Parent.Controls)
			{
				item.Selected = false;
				item.Invalidate();
			}

			Selected = true;

			Loading = true;
			await Task.Run(() => SelectedChanged?.Invoke(this, EventArgs.Empty));
			Loading = false;
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		if (Selected)
		{
			e.Graphics.FillRoundedRectangleWithShadow(ClientRectangle.Pad(Padding.Left / 2), Padding.Left / 2, Padding.Left / 2, FormDesign.Design.BackColor.MergeColor(FormDesign.Design.ActiveColor, 90), Color.FromArgb(8, FormDesign.Design.ActiveColor), true);
		}
		else if (HoverState.HasFlag(HoverState.Hovered) && ClientRectangle.Pad(Padding).Contains(PointToClient(Cursor.Position)))
		{
			e.Graphics.FillRoundedRectangleWithShadow(ClientRectangle.Pad(Padding.Left / 2), Padding.Left / 2, Padding.Left / 2, FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? 8 : -8), FormDesign.Design.IsDarkTheme ? Color.FromArgb(2, 255, 255, 255) : Color.FromArgb(15, FormDesign.Design.AccentColor), true);
		}
		else
		{
			e.Graphics.FillRoundedRectangleWithShadow(ClientRectangle.Pad(Padding.Left / 2), Padding.Left / 2, Padding.Left / 2, FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? -2 : 2));
		}

		var text = (int)Usage == -1 ? Locale.AnyUsage : LocaleCR.Get(Usage.ToString());
		using var font = UI.Font(7F, FontStyle.Bold).FitToWidth(text, ClientRectangle.Pad(Padding), e.Graphics);
		var textHeight = (int)e.Graphics.Measure(text, font).Height;
		using var img = Usage.GetIcon().Get(Height / 2)?.Color(FormDesign.Design.IconColor);
		using var textBrush = new SolidBrush(FormDesign.Design.ForeColor);

		if (img == null)
		{
			using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
			e.Graphics.DrawString(text, font, textBrush, ClientRectangle.Pad(Padding), format);
		}
		else
		{
			var rect = ClientRectangle.Pad(Padding).CenterR(ClientRectangle.Pad(Padding).Width, textHeight + img.Height + (int)(2.5 * UI.FontScale));

			if (Loading)
			{
				DrawLoader(e.Graphics, rect.Align(img.Size, ContentAlignment.TopCenter));
			}
			else
			{
				e.Graphics.DrawImage(img, rect.Align(img.Size, ContentAlignment.TopCenter));
			}

			using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
			e.Graphics.DrawString(text, font, textBrush, rect, format);
		}
	}
}
