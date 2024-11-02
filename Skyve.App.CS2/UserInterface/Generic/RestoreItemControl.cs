using Skyve.Domain.CS2.Game;
using Skyve.Domain.CS2.Paradox;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;

internal class RestoreItemControl : SlickControl
{
	private readonly bool _restorePoint;
	private readonly SaveGameMetaData? _saveMetaData;
	private readonly PlaysetMetaData? _playsetMetaData;

	public IRestoreItem RestoreItem { get; }
	public bool Selected { get; set; }

	public RestoreItemControl(IRestoreItem restoreItem, bool restorePoint)
	{
		RestoreItem = restoreItem;
		Cursor = Cursors.Hand;

		_restorePoint = restorePoint;
		_saveMetaData = restoreItem.ItemMetaData as SaveGameMetaData;
		_playsetMetaData = restoreItem.ItemMetaData as PlaysetMetaData;
	}

	protected override void OnParentChanged(EventArgs e)
	{
		base.OnParentChanged(e);

		if (Parent is not null)
		{
			Parent.Resize += Parent_Resize;

			Parent_Resize(this, e);
		}
	}

	private void Parent_Resize(object sender, EventArgs e)
	{
		Width = Math.Min(UI.Scale(250), Parent.Width - Parent.Padding.Horizontal - Margin.Horizontal);
	}

	protected override void UIChanged()
	{
		Size = UI.Scale(new Size(250, 93));
		Margin = UI.Scale(new Padding(3));
		Padding = UI.Scale(new Padding(12));
	}

	protected override void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		if (e.Button != MouseButtons.Left)
		{
			return;
		}

		if (!_restorePoint)
		{
			foreach (var item in Parent.Controls.OfType<RestoreItemControl>())
			{
				item.Selected = false;
				item.Invalidate();
			}

			Selected = true;
		}
		else
		{
			Selected = !Selected;
		}
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(BackColor);

		if (Selected)
		{
			using var pen = new Pen(FormDesign.Design.ActiveColor, UI.Scale(1.5f)) { Alignment = PenAlignment.Center };

			e.Graphics.FillRoundedRectangleWithShadow(ClientRectangle.Pad(Padding.Left / 2), Padding.Left / 2, Padding.Left / 2, FormDesign.Design.BackColor.MergeColor(FormDesign.Design.ActiveColor, 90), Color.FromArgb(8, FormDesign.Design.ActiveColor), true);
			e.Graphics.DrawRoundedRectangle(pen, ClientRectangle.Pad(Padding.Left / 2), Padding.Left / 2);
		}
		else if (HoverState.HasFlag(HoverState.Hovered) && ClientRectangle.Pad(Padding).Contains(PointToClient(Cursor.Position)))
		{
			e.Graphics.FillRoundedRectangleWithShadow(ClientRectangle.Pad(Padding.Left / 2), Padding.Left / 2, Padding.Left / 2, FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? 8 : -8), FormDesign.Design.IsDarkTheme ? Color.FromArgb(2, 255, 255, 255) : Color.FromArgb(15, FormDesign.Design.AccentColor), true);
		}
		else
		{
			using var brush = new SolidBrush(FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? 2 : -2));
			e.Graphics.FillRoundedRectangle(brush, ClientRectangle.Pad(Padding.Left / 2), Padding.Left / 2);
		}

		using var titleFont = UI.Font(10.25F, FontStyle.Bold);
		using var textBrush = new SolidBrush(ForeColor);

		var rect = ClientRectangle.Pad(Padding);

		using var icon = IconManager.GetIcon(Selected ? "Ok" : "Enabled", UI.Scale(24)).Color(Selected ? FormDesign.Design.ActiveColor : FormDesign.Design.InfoColor);

		e.Graphics.DrawImage(icon, rect.Align(icon.Size, ContentAlignment.TopRight));

		rect.Width -= UI.Scale(20);

		e.Graphics.DrawStringItem(LocaleHelper.GetGlobalText("Backup_" + RestoreItem.MetaData.Name, out var translation) ? translation : RestoreItem.MetaData.Name
			, titleFont
			, ForeColor
			, ref rect);

		rect = rect.Pad(Padding.Left / 2, 0, 0, 0);

		if (_saveMetaData is not null)
		{
			e.Graphics.DrawStringItem(_saveMetaData.Population.ToString("N0")
				, Font
				, FormDesign.Design.InfoColor
				, ref rect
				, dIcon: "People");

			e.Graphics.DrawStringItem(_saveMetaData.Money.ToString("N0")
				, Font
				, FormDesign.Design.InfoColor
				, ref rect
				, dIcon: "Money");

			if (_saveMetaData.SimulationDate is not null)
			{
				e.Graphics.DrawStringItem(new DateTime(_saveMetaData.SimulationDate.Year, _saveMetaData.SimulationDate.Month, 1).ToString("MMMM yyyy")
				, Font
				, FormDesign.Design.InfoColor
				, ref rect
				, dIcon: "Calendar");
			}
		}
		else if (_playsetMetaData is not null)
		{
			e.Graphics.DrawStringItem($"{_playsetMetaData.ModCount} {Locale.Mod.FormatPlural(_playsetMetaData.ModCount).ToLower()}"
				, Font
				, FormDesign.Design.InfoColor
				, ref rect
				, dIcon: "Mods");

			e.Graphics.DrawStringItem(_playsetMetaData.ModSize.SizeString(0)
				, Font
				, FormDesign.Design.InfoColor
				, ref rect
				, dIcon: "Drive");
		}
		else
		{
			e.Graphics.DrawStringItem(Locale.ContainCount.Format(RestoreItem.MetaData.FileCount, LocaleSlickUI.File.FormatPlural(RestoreItem.MetaData.FileCount))
				, Font
				, FormDesign.Design.InfoColor
				, ref rect
				, dIcon: "File");
		}

		if (!_restorePoint)
		{
			e.Graphics.DrawStringItem(RestoreItem.MetaData.BackupTime.ToString("d MMM yyyy - h:mm tt")
				, Font
				, FormDesign.Design.InfoColor
				, ref rect
				, dIcon: "Clock");
		}

		Height = rect.Y + Padding.Bottom;
	}
}
