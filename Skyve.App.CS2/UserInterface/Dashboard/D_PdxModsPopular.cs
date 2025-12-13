using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Panels;
using Skyve.Domain.CS2.Utilities;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_PdxModsPopular() : D_PdxModsBase()
{
	private static List<IWorkshopInfo> _popularMods = [];
	private List<IWorkshopInfo> popularMods = _popularMods;

	protected override List<IWorkshopInfo> GetPackages()
	{
		return [.. popularMods];
	}

	protected override async Task<bool> ProcessDataLoad(CancellationToken token)
	{
		var list = (await WorkshopService.QueryFilesAsync(WorkshopQuerySorting.Best, WorkshopSearchTime.Week, requiredTags: SelectedTags, limit: 16)).Mods.ToList();

		if (token.IsCancellationRequested)
		{
			return false;
		}

		_popularMods = popularMods = list;

		OnResizeRequested();

		return await base.ProcessDataLoad(token);
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		if (popularMods.Count == 0)
		{
			if (Loading || !FirstLoadComplete)
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
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsPopularWeek, "PDXMods");

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
		DrawLoadingSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsPopularWeek);
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		if (Loading)
		{
			DrawLoading(e, applyDrawing, ref preferredHeight);
		}
		else
		{
			DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsPopularWeek, "PDXMods");
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

	protected override void ViewMore()
	{
		var panel = new PC_WorkshopList();

		panel.SetSettings(PackageSorting.Best, SelectedTags, WorkshopSearchTime.Week);

		App.Program.MainForm.PushPanel(panel);
	}
}