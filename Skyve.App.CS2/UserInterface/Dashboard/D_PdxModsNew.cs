using Skyve.Domain.CS2.Utilities;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_PdxModsNew : D_PdxModsBase
{
	private static List<IWorkshopInfo> _newMods = [];
	private List<IWorkshopInfo> newMods = _newMods;

	protected override List<IWorkshopInfo> GetPackages()
	{
		return [.. newMods];
	}

	protected override async Task ProcessDataLoad(CancellationToken token)
	{
		var list = (await WorkshopService.QueryFilesAsync(WorkshopQuerySorting.DateCreated, requiredTags: SelectedTags, limit: 8)).ToList();

		if (token.IsCancellationRequested)
		{
			return;
		}

		_newMods = newMods = list;

		OnResizeRequested();

		await base.ProcessDataLoad(token);
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		if (newMods.Count == 0)
		{
			if (Loading)
			{
				return DrawLoading;
			}

			return DrawNone;
		}

		if (Width / UI.FontScale < 350)
		{
			return DrawSmall;
		}

		return DrawLarge;
	}

	private void DrawNone(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsNew, "PDXMods");

		Draw(e, applyDrawing, ref preferredHeight, false);

		e.Graphics.DrawStringItem(LocaleCS2.CouldNotRetrieveMods
			, Font
			, FormDesign.Design.ForeColor
			, e.ClipRectangle.Pad(Margin)
			, ref preferredHeight
			, applyDrawing);

		preferredHeight += Padding.Top / 2;
	}

	private void DrawLoading(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawLoadingSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsNew);
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		if (Loading)
		{
			DrawLoading(e, applyDrawing, ref preferredHeight);
		}
		else
		{
			DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsNew, "PDXMods");
		}
	}

	private void DrawSmall(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawHeader(e, applyDrawing, ref preferredHeight);

		Draw(e, applyDrawing, ref preferredHeight, true);
	}

	private void DrawLarge(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawHeader(e, applyDrawing, ref preferredHeight);

		Draw(e, applyDrawing, ref preferredHeight, false);
	}
}