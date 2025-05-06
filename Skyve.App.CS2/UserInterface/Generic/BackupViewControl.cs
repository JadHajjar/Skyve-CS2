using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
internal class BackupViewControl : SlickControl
{
	public delegate Task TaskAction();

	private Rectangle RestorePointRect;
	private Rectangle IndividualItemRect;

	[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool RestorePoint { get; set; } = true;
	[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool IndividualItem { get; set; }

	public event EventHandler? RestorePointClicked;
	public event EventHandler? IndividualItemClicked;

	public BackupViewControl()
	{
		Cursor = Cursors.Hand;
	}

	protected override void UIChanged()
	{
		Margin = UI.Scale(new Padding(4, 4, 4, 5));
		Padding = UI.Scale(new Padding(3));

		var itemHeight = UI.Scale(28);

		Size = new Size(itemHeight * 2, itemHeight - UI.Scale(4));

		var rect = ClientRectangle.Align(new Size(itemHeight, Height), ContentAlignment.MiddleLeft);

		RestorePointRect = rect;
		rect.X += rect.Width;
		
		IndividualItemRect = rect;
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);

		//if (RestorePointRect.Contains(e.Location))
		//{
		//	SlickTip.SetTo(this, "Switch to Compact-View");
		//}
		//else if (IndividualItemRect.Contains(e.Location))
		//{
		//	SlickTip.SetTo(this, "Switch to Grid-View");
		//}
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		if (e.Button != MouseButtons.Left)
		{
			return;
		}

		if (RestorePointRect.Contains(e.Location))
		{
			RestorePointClicked?.Invoke(this, e);
		}
		else if (IndividualItemRect.Contains(e.Location))
		{
			IndividualItemClicked?.Invoke(this, e);
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		var CursorLocation = PointToClient(Cursor.Position);

		using var brush = new SolidBrush(FormDesign.Design.ButtonColor);

		e.Graphics.FillRoundedRectangle(brush, ClientRectangle, Padding.Left);

		{
			var rect = RestorePointRect;
			using var icon = IconManager.GetIcon("History", rect.Width * 3 / 4);

			if (rect.Contains(CursorLocation))
			{
				SlickButton.GetColors(out var fore, out var back, HoverState);
				using var brush1 = rect.Gradient(back, 1.5F);
				e.Graphics.FillRoundedRectangle(brush1, rect, Padding.Left);
				e.Graphics.DrawImage(icon.Color(RestorePoint && !HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveColor : fore), rect.CenterR(icon.Size));
			}
			else
			{
				e.Graphics.DrawImage(icon.Color(RestorePoint ? FormDesign.Design.ActiveColor : FormDesign.Design.ButtonForeColor), rect.CenterR(icon.Size));
			}
		}

		{
			var rect = IndividualItemRect;
			using var icon = IconManager.GetIcon("Pages", rect.Width * 3 / 4);

			if (rect.Contains(CursorLocation))
			{
				SlickButton.GetColors(out var fore, out var back, HoverState);
				using var brush1 = rect.Gradient(back, 1.5F);
				e.Graphics.FillRoundedRectangle(brush1, rect, Padding.Left);
				e.Graphics.DrawImage(icon.Color(IndividualItem && !HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveColor : fore), rect.CenterR(icon.Size));
			}
			else
			{
				e.Graphics.DrawImage(icon.Color(IndividualItem ? FormDesign.Design.ActiveColor : FormDesign.Design.ButtonForeColor), rect.CenterR(icon.Size));
			}
		}
	}
}
