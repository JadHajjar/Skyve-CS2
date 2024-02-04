using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Dashboard;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_Playsets : IDashboardItem
{
	private readonly IPlaysetManager _playsetManager;
	private readonly INotifier _notifier;
	private int mainSectionHeight;

	public D_Playsets()
	{
		ServiceCenter.Get(out _playsetManager, out _notifier);

		_notifier.PlaysetChanged += _notifier_PlaysetChanged;
		_notifier.PlaysetUpdated += _notifier_PlaysetUpdated;

		Loading = !_notifier.IsPlaysetsLoaded;
	}

	protected override void Dispose(bool disposing)
	{
		_notifier.PlaysetChanged -= _notifier_PlaysetChanged;
		_notifier.PlaysetUpdated -= _notifier_PlaysetUpdated;

		base.Dispose(disposing);
	}

	private void _notifier_PlaysetUpdated()
	{
		this.TryInvoke(OnResizeRequested);
	}

	private void _notifier_PlaysetChanged()
	{
		this.TryInvoke(() =>
		{
			Enabled = true;
			Loading = false;
			OnResizeRequested();
		});
	}

	private void SwitchTo(IPlayset item)
	{
		Enabled = false;
		Loading = true;
		_playsetManager.SetCurrentPlayset(item);
		OnResizeRequested();
	}

	private void ViewPlaysetSettings()
	{
		App.Program.MainForm.PushPanel(ServiceCenter.Get<IAppInterfaceService>().PlaysetSettingsPanel());
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		return Draw;
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, e.ClipRectangle.ClipTo(mainSectionHeight), _playsetManager.CurrentPlayset?.Name ?? Locale.NoActivePlayset, _playsetManager.CurrentCustomPlayset?.GetIcon() ?? "I_Playsets", out var fore, ref preferredHeight, _playsetManager.CurrentCustomPlayset?.Color ?? FormDesign.Design.MenuColor, Locale.ActivePlayset);

		//var cs2Playset = (Playset)_playsetManager.CurrentPlayset;

		//if (cs2Playset.LaunchSettings.StartNewGame)
		//{
		//	var text = string.IsNullOrWhiteSpace(cs2Playset.LaunchSettings.MapToLoad)
		//		? Locale.StartsNewGameOnLaunch.One
		//		: Locale.StartsNewGameWithMap.Format(Path.GetFileNameWithoutExtension(cs2Playset.LaunchSettings.MapToLoad));

		//	e.Graphics.DrawStringItem(text
		//		, Font
		//		, fore
		//		, e.ClipRectangle.Pad(Padding.Left)
		//		, ref preferredHeight
		//		, applyDrawing
		//		, "I_Launch");
		//}

		//if (cs2Playset.LaunchSettings.LoadSaveGame)
		//{
		//	var text = string.IsNullOrWhiteSpace(cs2Playset.LaunchSettings.SaveToLoad)
		//		? Locale.LoadsSaveGameOnLaunch.One
		//		: Locale.LoadsSaveGameWithMap.Format(Path.GetFileNameWithoutExtension(cs2Playset.LaunchSettings.SaveToLoad));

		//	e.Graphics.DrawStringItem(text
		//		, Font
		//		, fore
		//		, e.ClipRectangle.Pad(Padding.Left)
		//		, ref preferredHeight
		//		, applyDrawing
		//		, "I_Launch");
		//}

		//if (cs2Playset.LaunchSettings.NewAsset || cs2Playset.LaunchSettings.LoadAsset)
		//{
		//	var text = cs2Playset.LaunchSettings.NewAsset
		//		? Locale.StartsNewAssetOnLaunch
		//		: Locale.LoadsAssetOnLaunch;

		//	e.Graphics.DrawStringItem(text
		//		, Font
		//		, fore
		//		, e.ClipRectangle.Pad(Padding.Left)
		//		, ref preferredHeight
		//		, applyDrawing
		//		, "I_Launch");
		//}

		preferredHeight += Margin.Top;

		mainSectionHeight = preferredHeight - e.ClipRectangle.Y;

		preferredHeight += Margin.Top;

		DrawButton(e, applyDrawing, ref preferredHeight, ViewPlaysetSettings, new()
		{
			Text = Locale.ChangePlaysetSettings,
			Icon = "I_Cog",
			Rectangle = e.ClipRectangle
		});

		var favs = _playsetManager.Playsets.AllWhere(x => x.GetCustomPlayset().IsFavorite);

		if (favs.Count == 0)
		{
			preferredHeight -= Margin.Top;
			return;
		}

		preferredHeight += Margin.Top;
		DrawDivider(e, e.ClipRectangle, applyDrawing, ref preferredHeight);
		preferredHeight += Margin.Top;

		using var font = UI.Font(9.75F);
		using var fontBold = UI.Font(9.75F, FontStyle.Bold);

		foreach (var item in favs.OrderByDescending(x => x.GetCustomPlayset().DateUsed))
		{
			var args = new ButtonDrawArgs()
			{
				Text = item.Name,
				Font = _playsetManager.CurrentPlayset == item ? fontBold : font,
				Icon = item.GetCustomPlayset().GetIcon(),
				BackColor = item.GetCustomPlayset().Color ?? default,
				Rectangle = e.ClipRectangle.Pad(2),
				BorderRadius = Padding.Left,
				ButtonType = item.GetCustomPlayset().Color == null ? ButtonType.Normal : ButtonType.Active
			};

			DrawButton(e, applyDrawing, ref preferredHeight, () => SwitchTo(item), args);

			if (applyDrawing && _playsetManager.CurrentPlayset == item)
			{
				using var pen = new Pen(FormDesign.Design.ActiveColor, (float)(2 * UI.FontScale));
				e.Graphics.DrawRoundedRectangle(pen, args.Rectangle.Pad((int)(2 * UI.FontScale), 0, (int)(2 * UI.FontScale), 0), Padding.Left);
			}
		}

		preferredHeight -= Margin.Bottom;

		if (Loading && !Enabled)
		{
			using var brush = new SolidBrush(Color.FromArgb(100, BackColor));
			e.Graphics.FillRectangle(brush, ClientRectangle);

			DrawLoader(e.Graphics, ClientRectangle.CenterR(UI.Scale(new Size(32, 32), UI.UIScale)));
		}
	}
}
