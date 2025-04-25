using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Panels;
using Skyve.Domain.CS2.Utilities;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_PdxModsUpdated() : D_PdxModsBase(lastTag)
{
	private static List<IWorkshopInfo> _recentlyUpdatedMods = [];
	private static string? lastTag;
	private List<IWorkshopInfo> recentlyUpdatedMods = _recentlyUpdatedMods;

	protected override List<IWorkshopInfo> GetPackages()
	{
		return [.. recentlyUpdatedMods];
	}

	protected override async Task<bool> ProcessDataLoad(CancellationToken token)
	{
		var newMods = (await WorkshopService.QueryFilesAsync(WorkshopQuerySorting.DateCreated, requiredTags: SelectedTags, limit: 16)).Mods.ToList();
		var list = (await WorkshopService.QueryFilesAsync(WorkshopQuerySorting.DateUpdated, requiredTags: SelectedTags, limit: 32)).Mods
			.Where(x => !newMods.Any(y => y.Id == x.Id))
			.Take(16)
			.ToList();

		if (token.IsCancellationRequested)
		{
			return false;
		}

		_recentlyUpdatedMods = recentlyUpdatedMods = list;
		lastTag = SelectedTags?.FirstOrDefault();

		OnResizeRequested();

		return await base.ProcessDataLoad(token);
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		if (recentlyUpdatedMods.Count == 0)
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
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsUpdated, "PDXMods");

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
		DrawLoadingSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsUpdated);
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		if (Loading)
		{
			DrawLoading(e, applyDrawing, ref preferredHeight);
		}
		else
		{
			DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PDXModsUpdated, "PDXMods");
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

		panel.SetSettings(PackageSorting.DateUpdated, SelectedTags);

		App.Program.MainForm.PushPanel(panel);
	}
}