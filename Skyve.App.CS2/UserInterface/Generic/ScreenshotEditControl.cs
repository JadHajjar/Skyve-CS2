using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
internal class ScreenshotEditControl : SlickControl
{
	private bool hovered;
	private List<string> _screenshots = [];
	private List<Bitmap> _images = [];

	public IOSelectionDialog? IOSelectionDialog { get; set; }

	public IEnumerable<string> Screenshots
	{
		get => _screenshots;
		set
		{
			_screenshots = value.ToList();
			_images.ForEach(x => x.Dispose());
			_images = value.ToList(GetImage);

			Height = UI.Scale(84) * _screenshots.Count;

			Invalidate();
		}
	}

	private Bitmap GetImage(string source)
	{
		using var image = Image.FromFile(source);

		var height = UI.Scale(84) - (UI.Scale(8) * 2);

		return new Bitmap(image, image.Width * height / image.Height, height);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			Screenshots = [];
		}

		base.Dispose(disposing);
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		if (e.Button != MouseButtons.Left)
		{
			return;
		}

		Invalidate();

		var padding = UI.Scale(8);
		var cursor = e.Location;

		for (var i = 0; i < _screenshots.Count; i++)
		{
			var image = _images[i];

			var height = UI.Scale(84);
			var rectangle = new Rectangle(0, i * height, Width, height);
			var imageRect = new Rectangle(padding, (i * height) + padding, image.Width, image.Height);

			var iconSizes = UI.Scale(new Size(48, 48));

			var trashRect = rectangle.Pad(padding * 2).Align(iconSizes, ContentAlignment.MiddleRight);
			var upRect = rectangle.Pad(padding * 2).Pad(Math.Max(16 * image.Height / 9, image.Width) + (padding * 2), 0, 0, 0).Align(iconSizes, ContentAlignment.MiddleLeft);
			var downRect = upRect;

			downRect.X += iconSizes.Width * 3 / 2;

			if (imageRect.Contains(cursor))
			{
				EditAt(i);
				return;
			}

			if (upRect.Contains(cursor))
			{
				UpAt(i);
				return;
			}

			if (downRect.Contains(cursor))
			{
				DownAt(i);
				return;
			}

			if (trashRect.Contains(cursor))
			{
				RemoveAt(i);
				return;
			}
		}
	}

	private void RemoveAt(int i)
	{
		_images[i].Dispose();
		_images.RemoveAt(i);
		_screenshots.RemoveAt(i);

		Height = UI.Scale(84) * _screenshots.Count;
	}

	private void DownAt(int i)
	{
		if (i >= _screenshots.Count - 1)
		{
			return;
		}

		var screenshot = _screenshots[i];
		var image = _images[i];

		_screenshots[i] = _screenshots[i + 1];
		_images[i] = _images[i + 1];
		_screenshots[i + 1] = screenshot;
		_images[i + 1] = image;
	}

	private void UpAt(int i)
	{
		if (i <= 0)
		{
			return;
		}

		var screenshot = _screenshots[i];
		var image = _images[i];

		_screenshots[i] = _screenshots[i - 1];
		_images[i] = _images[i - 1];
		_screenshots[i - 1] = screenshot;
		_images[i - 1] = image;
	}

	private void EditAt(int i)
	{
		if (IOSelectionDialog is null)
		{
			return;
		}

		IOSelectionDialog.Title = "Select a thumbnail picture";

		if (IOSelectionDialog.PromptFile(FindForm()) == DialogResult.OK)
		{
			try
			{
				_images[i] = GetImage(IOSelectionDialog.SelectedPath);
				_screenshots[i] = IOSelectionDialog.SelectedPath;
			}
			catch { }
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		var padding = UI.Scale(8);
		var cursor = PointToClient(Cursor.Position);

		hovered = false;

		for (var i = 0; i < _screenshots.Count; i++)
		{
			var image = _images[i];

			var height = UI.Scale(84);
			var rectangle = new Rectangle(0, i * height, Width, height);
			var imageRect = new Rectangle(padding, (i * height) + padding, image.Width, image.Height);
			var isHovered = HoverState.HasFlag(HoverState.Hovered) && rectangle.Contains(cursor);

			if (i != 0)
			{
				using var pen = new Pen(FormDesign.Design.AccentColor, UI.Scale(1.5f));
				e.Graphics.DrawLine(pen, padding, rectangle.Y, Width - padding, rectangle.Y);
			}

			if (isHovered)
			{
				using var backBrush = new SolidBrush(FormDesign.Design.AccentBackColor);
				e.Graphics.FillRoundedRectangle(backBrush, rectangle, padding);
			}

			var iconSizes = UI.Scale(new Size(48, 48));
			var iconSize = iconSizes.Width * 2 / 3;
			using var trash = IconManager.GetIcon("Trash", iconSize);
			using var edit = IconManager.GetIcon("Edit", iconSize);
			using var up = IconManager.GetIcon("ArrowUp", iconSize);
			using var down = IconManager.GetIcon("ArrowDown", iconSize);

			var trashRect = rectangle.Pad(padding * 2).Align(iconSizes, ContentAlignment.MiddleRight);
			var upRect = rectangle.Pad(padding * 2).Pad(Math.Max(16 * image.Height / 9, image.Width) + (padding * 2), 0, 0, 0).Align(iconSizes, ContentAlignment.MiddleLeft);
			var downRect = upRect;

			downRect.X += iconSizes.Width * 3 / 2;

			DrawButton(e.Graphics, trash, trashRect, cursor, ColorStyle.Red, ButtonType.Hidden);

			if (isHovered && i != 0)
			{
				DrawButton(e.Graphics, up, upRect, cursor, ColorStyle.Active, ButtonType.Hidden);
			}

			if (isHovered && i != _screenshots.Count - 1)
			{
				DrawButton(e.Graphics, down, downRect, cursor, ColorStyle.Active, ButtonType.Hidden);
			}

			e.Graphics.DrawRoundedImage(image, imageRect, padding / 2, isHovered? FormDesign.Design.AccentBackColor:BackColor);

			if (isHovered)
			{
				var imgHovered = imageRect.Contains(cursor);
				var pressed = imgHovered && HoverState.HasFlag(HoverState.Pressed);
				using var brush = new SolidBrush(Color.FromArgb(imgHovered ? 210 : 120, pressed ? FormDesign.Design.ActiveColor : FormDesign.Design.BackColor));
				using var icon = IconManager.GetIcon("Edit", imageRect.Height * 2 / 3).Color(pressed ? FormDesign.Design.ActiveForeColor : FormDesign.Design.ForeColor);

				e.Graphics.FillRoundedRectangle(brush, imageRect, padding / 2);
				e.Graphics.DrawImage(icon, imageRect.CenterR(icon.Size));

				hovered |= imgHovered;
			}
		}

		Cursor = hovered ? Cursors.Hand : Cursors.Default;
	}

	private void DrawButton(Graphics graphics, Bitmap image, Rectangle rect, Point cursor, ColorStyle style, ButtonType type)
	{
		hovered |= rect.Contains(cursor);

		SlickButton.Draw(graphics, new ButtonDrawArgs
		{
			Rectangle = rect,
			Cursor = cursor,
			HoverState = HoverState & ~HoverState.Focused,
			Image = image,
			ColorStyle = style,
			ButtonType = type
		});
	}
}
