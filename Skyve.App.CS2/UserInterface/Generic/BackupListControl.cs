using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
internal class BackupListControl : SlickStackedListControl<BackupListControl.RestoreGroup>
{
	[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool RestorePoint { get; internal set; } = true;
	[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool IndividualItem { get; internal set; }

	public BackupListControl()
	{
		ItemHeight = 40;
		HighlightOnHover = true;
		SeparateWithLines = true;
	}

	protected override void UIChanged()
	{
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

		var title = RestorePoint ? e.Item.Time.ToRelatedString(true) : $"{e.Item.Name} - {e.Item.RestoreItems.First().MetaData.GetTypeTranslation()}";
		var subTitle = RestorePoint ? e.Item.RestoreItems.GroupBy(x => x.MetaData.Type).ListStrings(x => x.First().MetaData.GetTypeTranslation().FormatPlural(x.Count()), ", ") : Locale.RestorePoint.FormatPlural(e.Item.RestoreItems.Count(), e.Item.Time.ToRelatedString(true));
		var time = e.Item.Time.ToString("d MMM yyyy - h:mm tt");

		using var brush = new SolidBrush(e.BackColor.GetTextColor());
		using var brush2 = new SolidBrush(e.BackColor.GetTextColor().MergeColor(e.BackColor, 70));
		using var fontTitle = UI.Font(9.75F, FontStyle.Bold);
		using var fontSubTitle = UI.Font(7.5F);

		var rectangle = e.ClipRectangle.Pad(8, 0, 8, 0);

		if (IndividualItem)
		{
			var icon = e.Item.RestoreItems.First().MetaData.GetIcon();

			if (icon is not "")
			{
				using var image = IconManager.GetIcon(e.Item.RestoreItems.First().MetaData.GetIcon(), rectangle.Height * 3 / 4).Color(brush.Color);

				e.Graphics.DrawImage(image, rectangle.Align(image.Size, ContentAlignment.MiddleLeft));

				rectangle = rectangle.Pad(image.Width + rectangle.X, 0, 0, 0);
			}
		}

		e.Graphics.DrawString(title, fontTitle, brush, rectangle);
		e.Graphics.DrawString(subTitle, fontSubTitle, brush2, rectangle, new StringFormat { LineAlignment = StringAlignment.Far });

		SlickButton.AlignAndDraw(e.Graphics, rectangle, ContentAlignment.MiddleRight, new ButtonDrawArgs
		{
			BackColor = !e.HoverState.HasFlag(HoverState.Hovered) ? e.BackColor : Color.FromArgb(120, FormDesign.Design.ActiveColor),
			Text = time,
			Icon = "Clock",
			Font = UI.Font(9F, FontStyle.Bold),
			Padding = UI.Scale(new Padding(4)),
			BorderRadius = Padding.Left,
			NoGradient = true,
			Size = UI.Scale(new Size(175, 24))
		});
	}

	internal class RestoreGroup
	{
		public DateTime Time { get; }
		public string Name { get; }
		public IEnumerable<IRestoreItem> RestoreItems { get; }

		public RestoreGroup(DateTime time, IEnumerable<IRestoreItem> restoreItems)
		{
			Time = time;
			Name = string.Empty;
			RestoreItems = restoreItems;
		}

		public RestoreGroup(string name, IEnumerable<IRestoreItem> restoreItems)
		{
			Time = restoreItems.Max(x => x.MetaData.BackupTime);
			Name = name;
			RestoreItems = restoreItems;
		}
	}
}
