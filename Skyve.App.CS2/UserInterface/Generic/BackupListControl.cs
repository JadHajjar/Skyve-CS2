﻿using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
internal class BackupListControl : SlickStackedListControl<BackupListControl.RestoreGroup>
{
	[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool RestorePoint { get; set; } = true;
	[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool IndividualItem { get; set; }

	public BackupListControl()
	{
		HighlightOnHover = true;
		SeparateWithLines = true;
	}

	protected override void UIChanged()
	{
		ItemHeight = 42;
		Padding = UI.Scale(new Padding(3));

		base.UIChanged();
	}

	protected override bool IsItemActionHovered(DrawableItem<RestoreGroup, GenericDrawableItemRectangles<RestoreGroup>> item, Point location)
	{
		return true;
	}

	protected override IEnumerable<IDrawableItem<RestoreGroup>> OrderItems(IEnumerable<IDrawableItem<RestoreGroup>> items)
	{
		return RestorePoint
			? items.OrderByDescending(x => x.Item.Time)
			: items.OrderByDescending(x => x.Item.Time).ThenBy(x => x.Item.Name);
	}

	protected override void OnPaintItemList(ItemPaintEventArgs<RestoreGroup, GenericDrawableItemRectangles<RestoreGroup>> e)
	{
		base.OnPaintItemList(e);

		var title = RestorePoint ? e.Item.Time.ToString("d MMM yyyy - h:mm tt") : e.Item.Name.RegexRemove(@"#\w+$");
		var subText = RestorePoint ? string.Empty : Regex.Match(e.Item.Name, @"#\w+$").Value;
		var subTitle = RestorePoint ? $"{e.Item.Time.ToRelatedString(true)} • {e.Item.RestoreItems.GroupBy(x => x.MetaData.Type).ListStrings(x => x.First().MetaData.GetTypeTranslation().FormatPlural(x.Count()), ", ")}" : Locale.RestorePoint.FormatPlural(e.Item.RestoreItems.Count(), e.Item.Time.ToRelatedString(true).ToLower());

		using var brush = new SolidBrush(e.BackColor.GetTextColor());
		using var brush2 = new SolidBrush(e.BackColor.GetTextColor().MergeColor(e.BackColor, 70));
		using var fontTitle = UI.Font(9.75F, FontStyle.Bold);
		using var fontSubTitle = UI.Font(7.5F);
		using var format = new StringFormat { LineAlignment = StringAlignment.Far };

		var rectangle = e.ClipRectangle.Pad(UI.Scale(new Padding(8, 0, 8, 0)));

		var icon = RestorePoint ? "Clock" : e.Item.RestoreItems.First().MetaData.GetIcon();

		if (icon is not "")
		{
			using var image = IconManager.GetIcon(icon, rectangle.Height * 3 / 4).Color(brush.Color);

			e.Graphics.DrawImage(image, rectangle.Align(image.Size, ContentAlignment.MiddleLeft));

			rectangle = rectangle.Pad(image.Width + rectangle.X, 0, 0, 0);
		}

		e.Graphics.DrawString(title, fontTitle, brush, rectangle);
		e.Graphics.DrawString(subTitle, fontSubTitle, brush2, rectangle, format);

		var titleSize = e.Graphics.Measure(title, fontTitle).ToSize();

		if (IndividualItem)
		{
			var text = e.Item.RestoreItems.First().MetaData.GetTypeTranslation();

			if (subText != string.Empty)
			{
				var rect = e.Graphics.DrawLabel(subText
					, null
					, Color.FromArgb(50, FormDesign.Design.AccentColor)
					, new Rectangle(rectangle.X + titleSize.Width, rectangle.Y, rectangle.Width - titleSize.Width, titleSize.Height)
					, ContentAlignment.MiddleLeft);

				rectangle = rectangle.Pad(rect.Width + Padding.Horizontal, 0, 0, 0);
			}

			if (text != e.Item.Name)
			{
				var rect = e.Graphics.DrawLabel(text
					, null
					, Color.FromArgb(50, FormDesign.Design.ActiveColor)
					, new Rectangle(rectangle.X + titleSize.Width, rectangle.Y, rectangle.Width - titleSize.Width, titleSize.Height)
					, ContentAlignment.MiddleLeft);

				rectangle = rectangle.Pad(rect.Width + Padding.Horizontal, 0, 0, 0);
			}
		}
		else
		{
			var types = e.Item.RestoreItems.Distinct(x => x.MetaData.Type).ToList(x => IconManager.GetIcon(x.MetaData.GetIcon(), UI.Scale(24) * 3 / 4));
			var size = types.Sum(x => x.Width + Padding.Horizontal) + Padding.Horizontal;
			var bounds = rectangle.Align(new Size(size, UI.Scale(24)), ContentAlignment.MiddleRight);

			using var backBrush = new SolidBrush(!e.HoverState.HasFlag(HoverState.Hovered) ? FormDesign.Design.AccentBackColor : Color.FromArgb(120, FormDesign.Design.ActiveColor));

			e.Graphics.FillRoundedRectangle(backBrush, bounds, Padding.Left);

			bounds = bounds.Align(types[0].Size, ContentAlignment.MiddleLeft);

			foreach (var item in types)
			{
				using (item)
				{
					bounds.X += Padding.Horizontal;
					e.Graphics.DrawImage(item.Color(brush.Color), bounds);
					bounds.X += item.Width;
				}
			}
		}

		e.Graphics.DrawLabel(e.Item.TotalSize.SizeString(0)
			, null
			, Color.FromArgb(100, FormDesign.Design.RedColor.MergeColor(FormDesign.Design.GreenColor, ((int)(100 * e.Item.TotalSize / (500 * 1024 * 1024))).Between(0, 100)))
			, new Rectangle(rectangle.X + titleSize.Width, rectangle.Y, rectangle.Width - titleSize.Width, titleSize.Height)
			, ContentAlignment.MiddleLeft);
	}

	internal class RestoreGroup
	{
		public DateTime Time { get; }
		public string Name { get; }
		public IEnumerable<IRestoreItem> RestoreItems { get; }
		public long TotalSize { get; }

		public RestoreGroup(DateTime time, IEnumerable<IRestoreItem> restoreItems)
		{
			Time = time;
			Name = string.Empty;
			RestoreItems = restoreItems;
			TotalSize = restoreItems.Sum(x => x.BackupFile.Length);
		}

		public RestoreGroup(string name, IEnumerable<IRestoreItem> restoreItems)
		{
			Time = restoreItems.Max(x => x.MetaData.BackupTime);
			Name = LocaleHelper.GetGlobalText("Backup_" + name, out var translation) ? translation : name;
			RestoreItems = restoreItems;
			TotalSize = restoreItems.Sum(x => x.BackupFile.Length);
		}
	}
}
