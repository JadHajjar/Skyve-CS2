using Skyve.App.UserInterface.Dashboard;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_PdxSync : IDashboardItem
{
	private readonly IWorkshopService _workshopService;
	private readonly INotifier _notifier;

	public D_PdxSync()
	{
		ServiceCenter.Get(out _workshopService, out _notifier);

		_notifier.WorkshopSyncStarted += OnChangeEvent;
		_notifier.WorkshopSyncEnded += OnChangeEvent;
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			_notifier.WorkshopSyncStarted -= OnChangeEvent;
			_notifier.WorkshopSyncEnded -= OnChangeEvent;
		}

		base.Dispose(disposing);
	}

	private void OnChangeEvent()
	{
		var ready = _workshopService.IsReady && !_notifier.IsWorkshopSyncInProgress;

		if (ready == Loading)
		{
			OnResizeRequested();
			Loading = !ready;
		}
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		return Draw;
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PdxSync, "PDXMods");
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		if (Loading)
		{
			DrawLoadingSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PdxSync);
		}
		else
		{
			DrawSection(e, applyDrawing, ref preferredHeight, LocaleCS2.PdxSync, "PDXMods");
		}

		var textRect = e.ClipRectangle.Pad(Margin);
		using var tinyFont = UI.Font(6.75f);
		using var rightFormat = new StringFormat { Alignment = StringAlignment.Far };
		using var font = UI.Font(8F, FontStyle.Bold).FitToWidth(Loading ? LocaleCS2.SyncOngoing : Locale.Ready, textRect, e.Graphics);


		if (Loading)
		{
			e.Graphics.DrawStringItem(LocaleCS2.CurrentStatus
				, tinyFont
				, FormDesign.Design.InfoColor
				, textRect
				, ref preferredHeight
				, applyDrawing);

			e.Graphics.DrawStringItem(LocaleCS2.SyncOngoing
				, font
				, FormDesign.Design.OrangeColor
				, textRect
				, ref preferredHeight
				, applyDrawing
				, stringFormat: rightFormat);

			preferredHeight += Padding.Top / 2;

			return;
		}

		var _ = preferredHeight;
		e.Graphics.DrawStringItem(LocaleCS2.CurrentStatus
			, tinyFont
			, FormDesign.Design.InfoColor
			, textRect
			, ref _
			, applyDrawing);

		e.Graphics.DrawStringItem(Locale.Ready
			, font
			, FormDesign.Design.GreenColor
			, textRect
			, ref preferredHeight
			, applyDrawing
			, stringFormat: rightFormat);

		preferredHeight += Padding.Top / 2;

		using var smallFont = UI.Font(7.5F);

		DrawButton(e, applyDrawing, ref preferredHeight, () => Task.Run(_workshopService.RunSync), new ButtonDrawArgs
		{
			Icon = "Sync",
			Font = smallFont,
			Size = new Size(0, UI.Scale(20)),
			Text = LocaleCS2.RunSync,
			Rectangle = e.ClipRectangle.Pad(BorderRadius)
		});

		preferredHeight += BorderRadius / 2;
	}
}
