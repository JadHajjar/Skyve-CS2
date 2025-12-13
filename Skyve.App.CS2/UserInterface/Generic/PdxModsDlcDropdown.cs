using PDX.SDK.Contracts.Service.Mods.Models;

using Skyve.Systems.CS2.Services;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Generic;
internal class PdxModsDlcDropdown : SlickMultiSelectionDropDown<ModGameAddon>
{
	private readonly IImageService _imageService;

	public PdxModsDlcDropdown()
	{
		ServiceCenter.Get(out _imageService);
		
		Items = ServiceCenter.Get<IWorkshopService, WorkshopService>()?.GameData?.Addons.Where(x => x.Type is "DLC" or "Expansion").ToArray() ?? [];
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		Width = UI.Scale(200);
	}

	protected override IEnumerable<ModGameAddon> OrderItems(IEnumerable<ModGameAddon> items)
	{
		return items.OrderByDescending(x => SelectedItems.Contains(x));
	}

	protected override bool SearchMatch(string searchText, ModGameAddon item)
	{
		return searchText.SearchCheck(item.DisplayName.RegexRemove("^.+?- ").RegexRemove("(Content )?Creator Pack: "));
	}

	protected override void PaintItem(PaintEventArgs e, Rectangle rectangle, Color foreColor, HoverState hoverState, ModGameAddon item, bool selected)
	{
		if (item is null)
		{
			return;
		}

		var text = item.DisplayName.RegexRemove("^.+?- ").RegexRemove("(Content )?Creator Pack: ");
		var icon = _imageService.GetImage(item.DisplayImageUrl, true, $"Dlc_{item.ModsDependencyId}.png", false).Result;

		if (icon != null)
		{
			e.Graphics.DrawRoundedImage(icon, rectangle.Align(new Size(rectangle.Height * 460 / 215, rectangle.Height), ContentAlignment.MiddleLeft), UI.Scale(3), hoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.ActiveColor : BackColor);
		}

		rectangle = rectangle.Pad((rectangle.Height * 460 / 215) + Padding.Left, 0, 0, 0);

		using var format = new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter };
		using var brush = new SolidBrush(foreColor);
		e.Graphics.DrawString(text, base.Font, brush, rectangle.AlignToFontSize(base.Font), format);
	}

	protected override void PaintSelectedItems(PaintEventArgs e, Rectangle rectangle, Color foreColor, HoverState hoverState, IEnumerable<ModGameAddon> items)
	{
		if (items.Count() == 1)
		{
			PaintItem(e, rectangle, foreColor, hoverState, items.First(), false);

			return;
		}

		if (!items.Any())
		{
			using var icon = IconManager.GetIcon("Slash", rectangle.Height - 2).Color(foreColor);

			e.Graphics.DrawImage(icon, rectangle.Align(icon.Size, ContentAlignment.MiddleLeft));

			e.Graphics.DrawString(LocaleCR.NoRequiredDlcs, Font, new SolidBrush(foreColor), rectangle.Pad(icon.Width + Padding.Left, 0, 0, 0).AlignToFontSize(Font, ContentAlignment.MiddleLeft, e.Graphics), new StringFormat { Trimming = StringTrimming.EllipsisCharacter });

			return;
		}

		e.Graphics.DrawString(LocaleCR.DlcsSelected.FormatPlural(items.Count()), Font, new SolidBrush(foreColor), rectangle.AlignToFontSize(Font), new StringFormat { LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter });
	}
}
