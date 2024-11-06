using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;

internal class BackupTypeDropdown : SlickSelectionDropDown<string?>
{
	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);

		if (Live)
		{
			Items = [null, .. ServiceCenter.Get<IBackupSystem>().GetBackupTypes()];
		}
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		Width = UI.Scale(150);
	}

	protected override bool SearchMatch(string searchText, string? item)
	{
		return searchText.SearchCheck(new BackupMetaData { Type = item }.GetTypeTranslation());
	}

	public override void ResetValue()
	{
		SelectedItem = null;
	}

	protected override void PaintItem(PaintEventArgs e, Rectangle rectangle, Color foreColor, HoverState hoverState, string? item)
	{
		var meta = new BackupMetaData { Type = item };
		var text = item is null ? Locale.AllContentTypes.One : meta.GetTypeTranslation().One;
		using var icon = IconManager.GetIcon(item is null ? "Slash" : meta.GetIcon(), rectangle.Height - 2).Color(foreColor);

		e.Graphics.DrawImage(icon, rectangle.Align(icon.Size, ContentAlignment.MiddleLeft));

		using var brush = new SolidBrush(foreColor);
		using var font = UI.Font(8.25F).FitTo(text, rectangle.Pad(icon.Width + Padding.Left, 0, 0, 0), e.Graphics);
		using var format = new StringFormat { LineAlignment = StringAlignment.Center };
		e.Graphics.DrawString(text, font, brush, rectangle.Pad(icon.Width + Padding.Left, 0, 0, 0), format);
	}
}
