using Skyve.App.Interfaces;
using Skyve.Domain.CS2.Utilities;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_PdxModsUpdated : D_PdxModsBase
{
	private readonly IWorkshopService _workshopService;
	private static List<IWorkshopInfo> _recentlyUpdatedMods = [];
	private List<IWorkshopInfo> recentlyUpdatedMods = _recentlyUpdatedMods;

	public D_PdxModsUpdated()
	{
		ServiceCenter.Get(out _workshopService);
	}

	protected override List<IWorkshopInfo> GetPackages()
	{
		return [.. recentlyUpdatedMods];
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (_workshopService.IsAvailable)
		{
			LoadData();
		}
		else
		{
			_workshopService.ContextAvailable += LoadData;
		}
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		_workshopService.ContextAvailable -= LoadData;
	}

	protected override async Task ProcessDataLoad(CancellationToken token)
	{
		var newMods = (await _workshopService.QueryFilesAsync(WorkshopQuerySorting.DateCreated, requiredTags: SelectedTags, limit: 8)).ToList();
		var list = (await _workshopService.QueryFilesAsync(WorkshopQuerySorting.DateUpdated, requiredTags: SelectedTags, limit: 16))
			.Where(x => !newMods.Any(y => y.Id == x.Id))
			.Take(8)
			.ToList();

		if (token.IsCancellationRequested)
		{
			return;
		}

		_recentlyUpdatedMods = recentlyUpdatedMods = list;

		OnResizeRequested();
	}

	private void RightClick(IWorkshopInfo package)
	{
		SlickToolStrip.Show(App.Program.MainForm, ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems(package));
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
}