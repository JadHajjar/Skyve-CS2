using Skyve.Domain.CS2.Game;
using Skyve.Domain.CS2.Paradox;

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using static Skyve.App.CS2.UserInterface.Generic.RestoreListControl;

namespace Skyve.App.CS2.UserInterface.Generic;
internal class RestoreListControl : SlickStackedListControl<RestoreItem>
{
	[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool RestorePoint { get; internal set; }

	public RestoreListControl(bool restorePoint)
	{
		RestorePoint = restorePoint;
		GridItemSize = new Size(250, 105);
		DynamicSizing = true;
		GridView = true;
	}

	protected override void UIChanged()
	{
		Padding = UI.Scale(new Padding(0));
		GridPadding = UI.Scale(new Padding(12));

		base.UIChanged();
	}

	protected override bool IsItemActionHovered(DrawableItem<RestoreItem, GenericDrawableItemRectangles<RestoreItem>> item, Point location)
	{
		return true;
	}

	protected override void OnItemMouseClick(DrawableItem<RestoreItem, GenericDrawableItemRectangles<RestoreItem>> item, MouseEventArgs e)
	{
		if (RestorePoint)
		{
			item.Item.Selected = !item.Item.Selected;
		}
		else
		{
			Items.Foreach(x => x.Selected = false);
			item.Item.Selected = true;
			Invalidate();
		}
	}

	protected override void OnPaintItemGrid(ItemPaintEventArgs<RestoreItem, GenericDrawableItemRectangles<RestoreItem>> e)
	{
		var radius = UI.Scale(6);

		if (e.Item.Selected)
		{
			using var pen = new Pen(FormDesign.Design.ActiveColor, UI.Scale(1.5f)) { Alignment = PenAlignment.Center };

			e.Graphics.FillRoundedRectangleWithShadow(e.ClipRectangle, radius, radius, FormDesign.Design.BackColor.MergeColor(FormDesign.Design.ActiveColor, 90), Color.FromArgb(8, FormDesign.Design.ActiveColor), true);
			e.Graphics.DrawRoundedRectangle(pen, e.ClipRectangle, radius);
		}
		else if (e.HoverState.HasFlag(HoverState.Hovered) && e.ClipRectangle.Pad(radius).Contains(PointToClient(Cursor.Position)))
		{
			e.Graphics.FillRoundedRectangleWithShadow(e.ClipRectangle, radius, radius, FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? 8 : -8), FormDesign.Design.IsDarkTheme ? Color.FromArgb(2, 255, 255, 255) : Color.FromArgb(15, FormDesign.Design.AccentColor), true);
		}
		else
		{
			using var brush = new SolidBrush(FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? 2 : -2));
			e.Graphics.FillRoundedRectangle(brush, e.ClipRectangle, radius);
		}

		using var titleFont = UI.Font(10.25F, FontStyle.Bold);
		using var textBrush = new SolidBrush(ForeColor);

		var rect = e.ClipRectangle.Pad(radius);
		var textColor = FormDesign.Design.InfoColor.MergeColor(FormDesign.Design.ForeColor);

		using var icon = IconManager.GetIcon(e.Item.Selected ? "Ok" : "Enabled", UI.Scale(24)).Color(e.Item.Selected ? FormDesign.Design.ActiveColor : FormDesign.Design.InfoColor);

		e.Graphics.DrawImage(icon, rect.Align(icon.Size, ContentAlignment.TopRight));

		rect.Width -= UI.Scale(20);

		var title = LocaleHelper.GetGlobalText("Backup_" + e.Item.Item.MetaData.Name, out var translation) ? translation.One : e.Item.Item.MetaData.Name.RegexRemove(@"#\w+$");
		var subText = Regex.Match(e.Item.Item.MetaData.Name, @"#\w+$").Value;

		e.Graphics.DrawStringItem(title
			, titleFont
			, ForeColor
			, ref rect);

		if (subText != string.Empty)
		{
			var titleSize = e.Graphics.Measure(title, titleFont).ToSize();
			var labelSize = e.Graphics.MeasureLabel(subText, null);
			Rectangle labelRect;

			if (titleSize.Width + labelSize.Width > rect.Width)
			{
				labelRect = new Rectangle(rect.X, rect.Y - radius, labelSize.Width, labelSize.Height);

				rect.Y += labelSize.Height;
			}
			else
			{
				labelRect = new Rectangle(rect.X + titleSize.Width, rect.Y-titleSize.Height-radius, titleSize.Width, titleSize.Height);
			}

			e.Graphics.DrawLabel(subText
				, null
				, Color.FromArgb(100, FormDesign.Design.AccentColor)
				, labelRect
				, ContentAlignment.MiddleLeft);
		}

		rect = rect.Pad(radius, 0, 0, 0);

		if (e.Item.SaveMetaData is not null)
		{
			e.Graphics.DrawStringItem(e.Item.SaveMetaData.Population.ToString("N0")
				, Font
				, textColor
				, ref rect
				, dIcon: "People");

			e.Graphics.DrawStringItem(e.Item.SaveMetaData.Money.ToString("N0")
				, Font
				, textColor
				, ref rect
				, dIcon: "Money");

			if (e.Item.SaveMetaData.SimulationDate is not null)
			{
				e.Graphics.DrawStringItem(new DateTime(e.Item.SaveMetaData.SimulationDate.Year, e.Item.SaveMetaData.SimulationDate.Month, 1).ToString("MMMM yyyy")
				, Font
				, textColor
				, ref rect
				, dIcon: "Calendar");
			}
		}
		else if (e.Item.PlaysetMetaData is not null)
		{
			e.Graphics.DrawStringItem($"{e.Item.PlaysetMetaData.ModCount} {Locale.Mod.FormatPlural(e.Item.PlaysetMetaData.ModCount).ToLower()}"
				, Font
				, textColor
				, ref rect
				, dIcon: "Mods");

			e.Graphics.DrawStringItem(e.Item.PlaysetMetaData.ModSize.SizeString(0)
				, Font
				, textColor
				, ref rect
				, dIcon: "Drive");
		}
		else
		{
			e.Graphics.DrawStringItem(Locale.ContainCount.Format(e.Item.Item.MetaData.FileCount, LocaleSlickUI.File.FormatPlural(e.Item.Item.MetaData.FileCount))
				, Font
				, textColor
				, ref rect
				, dIcon: "File");
		}

		if (!RestorePoint)
		{
			e.Graphics.DrawStringItem(e.Item.Item.MetaData.BackupTime.ToString("d MMM yyyy - h:mm tt")
				, Font
				, textColor
				, ref rect
				, dIcon: "Clock");
		}

		e.DrawableItem.CachedHeight = rect.Y + GridPadding.Vertical - e.ClipRectangle.Y + radius;
	}

	public class RestoreItem(IRestoreItem restoreItem, bool selected)
	{
		public IRestoreItem Item { get; } = restoreItem;
		public bool Selected { get; set; } = selected;
		public SaveGameMetaData? SaveMetaData { get; } = restoreItem.ItemMetaData as SaveGameMetaData;
		public PlaysetMetaData? PlaysetMetaData { get; } = restoreItem.ItemMetaData as PlaysetMetaData;
	}
}
