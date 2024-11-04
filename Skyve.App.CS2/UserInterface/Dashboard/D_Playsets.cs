using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Dashboard;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Dashboard;
internal class D_Playsets : IDashboardItem
{
	private readonly IPlaysetManager _playsetManager;
	private readonly INotifier _notifier;
	private readonly ICitiesManager _citiesManager;
	private bool isCitiesRunning;
	private bool loadingFromGameLaunch;
	private bool isCitiesAvailable;

	public D_Playsets()
	{
		ServiceCenter.Get(out _playsetManager, out _notifier, out _citiesManager);

		_notifier.PlaysetChanged += _notifier_PlaysetChanged;
		_notifier.PlaysetUpdated += _notifier_PlaysetUpdated;

		_citiesManager.MonitorTick += CitiesManager_MonitorTick;
		_citiesManager.LaunchingStatusChanged += CitiesManager_LaunchingStatusChanged;

		Loading = !_notifier.IsPlaysetsLoaded;
	}

	protected override void Dispose(bool disposing)
	{
		_notifier.PlaysetChanged -= _notifier_PlaysetChanged;
		_notifier.PlaysetUpdated -= _notifier_PlaysetUpdated;
		_citiesManager.MonitorTick -= CitiesManager_MonitorTick;
		_citiesManager.LaunchingStatusChanged -= CitiesManager_LaunchingStatusChanged;

		base.Dispose(disposing);
	}

	private void CitiesManager_LaunchingStatusChanged(bool obj)
	{
		this.TryInvoke(() =>
		{
			Loading = obj;
			loadingFromGameLaunch = obj;
			Enabled = !obj;
		});
	}

	private void CitiesManager_MonitorTick(bool isAvailable, bool isRunning)
	{
		isCitiesAvailable = isAvailable;

		if (isCitiesRunning != isRunning)
		{
			isCitiesRunning = isRunning;

			OnResizeRequested();
		}
	}

	private void _notifier_PlaysetUpdated()
	{
		Loading = !_notifier.IsPlaysetsLoaded;

		this.TryInvoke(OnResizeRequested);
	}

	private void _notifier_PlaysetChanged()
	{
		Loading = !_notifier.IsPlaysetsLoaded;

		this.TryInvoke(() =>
		{
			Enabled = true;
			OnResizeRequested();
		});
	}

	private void SwitchTo(IPlayset item)
	{
		Enabled = false;
		Loading = true;
		_playsetManager.ActivatePlayset(item);
		OnResizeRequested();
	}

	private void RightClick(IPlayset? playset)
	{
		if (playset is not null)
		{
			SlickToolStrip.Show(App.Program.MainForm, ServiceCenter.Get<IRightClickService>().GetRightClickMenuItems(playset, true));
		}
	}

	protected override DrawingDelegate GetDrawingMethod(int width)
	{
		if (width / UI.FontScale < 350)
		{
			return DrawHorizontal;
		}

		return DrawVertical;
	}

	private void DrawHorizontal(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		Draw(e, applyDrawing, ref preferredHeight, true);
	}

	private void DrawVertical(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		Draw(e, applyDrawing, ref preferredHeight, false);
	}

	protected override void DrawHeader(PaintEventArgs e, bool applyDrawing, ref int preferredHeight)
	{
		DrawSection(e, applyDrawing, ref preferredHeight, "Playsets", Locale.Playset.Plural);
	}

	private void Draw(PaintEventArgs e, bool applyDrawing, ref int preferredHeight, bool horizontal)
	{
		if (Loading && !loadingFromGameLaunch)
		{
			DrawLoadingSection(e, applyDrawing, ref preferredHeight, Locale.Playset.Plural);
		}
		else
		{
			DrawSection(e, applyDrawing, ref preferredHeight, _playsetManager.CurrentPlayset?.Name ?? Locale.NoActivePlayset, _playsetManager.CurrentCustomPlayset?.GetIcon() ?? "Playsets", _playsetManager.CurrentPlayset is null ? null : Locale.ActivePlayset);
		}

		_buttonRightClickActions[headerRectangle] = () => RightClick(_playsetManager.CurrentPlayset);

		DrawButton(e, applyDrawing, ref preferredHeight, !isCitiesAvailable ? null : (App.Program.MainForm as MainForm)!.LaunchStopCities, new ButtonDrawArgs
		{
			Text = LocaleHelper.GetGlobalText(isCitiesRunning ? "StopCities" : "StartCities"),
			Rectangle = e.ClipRectangle.Pad(Margin),
			Icon = isCitiesRunning ? "Stop" : "CS",
			Padding = UI.Scale(new Padding(2)),
			Enabled = Enabled && isCitiesAvailable,
			Control = this
		});

		if (_playsetManager.CurrentCustomPlayset != null)
		{
			if (applyDrawing)
			{
				var backColor = _playsetManager.CurrentCustomPlayset.Color ?? _playsetManager.CurrentCustomPlayset.GetThumbnail()?.GetAverageColor() ?? FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? 5 : -4, Sat: 5);
				using var colorBrush = Gradient(backColor, 3.5f);
				e.Graphics.FillRoundedRectangle(colorBrush, new Rectangle(e.ClipRectangle.X + Margin.Left, preferredHeight, e.ClipRectangle.Width - Margin.Horizontal, Margin.Top), Margin.Top / 2);
			}

			preferredHeight += Margin.Vertical;
		}

		var favs = _playsetManager.Playsets.AllWhere(x => x.GetCustomPlayset().IsFavorite);

		if (favs.Count == 0)
		{
			preferredHeight += Margin.Top / 2;
			return;
		}

		preferredHeight += Margin.Top;

		using var fontSmall = UI.Font(6.75F);

		e.Graphics.DrawStringItem(Locale.FavoritePlaysets, fontSmall, Color.FromArgb(150, FormDesign.Design.ForeColor), e.ClipRectangle.Pad(Margin).Pad(UI.Scale(2), 0, 0, 0), ref preferredHeight, applyDrawing);

		preferredHeight -= Margin.Top;

		var preferredSize = horizontal ? 115 : 100;
		var columns = (int)Math.Max(1, Math.Floor((e.ClipRectangle.Width - Margin.Left) / (preferredSize * UI.FontScale)));
		var columnWidth = (e.ClipRectangle.Width - Margin.Left) / columns;
		var height = (horizontal ? 0 : columnWidth) + UI.Scale(35);

		for (var i = 0; i < favs.Count; i++)
		{
			if (i > 0 && (i % columns) == 0)
			{
				preferredHeight += height;
			}

			var rect = new Rectangle(e.ClipRectangle.X + (Margin.Left / 2) + (i % columns * columnWidth), preferredHeight, columnWidth, height);

			if (applyDrawing)
			{
				DrawPlayset(e, favs[i], rect.Pad(Margin.Left / 2), horizontal);
			}
		}

		preferredHeight += Margin.Top + height;
	}

	private void DrawPlayset(PaintEventArgs e, IPlayset playset, Rectangle rect, bool horizontal)
	{
		_buttonActions[rect] = () => SwitchTo(playset);
		_buttonRightClickActions[rect] = () => RightClick(playset);

		var customPlayset = playset.GetCustomPlayset();
		var banner = customPlayset.GetThumbnail();

		var backColor = customPlayset.Color ?? banner?.GetThemedAverageColor() ?? FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? 5 : -4, Sat: 5);
		using var backBrush = Gradient(backColor, rect, customPlayset.Color.HasValue ? 3F : 1F);

		e.Graphics.FillRoundedRectangle(backBrush, rect, Margin.Left / 2);

		var bannerRect = horizontal ? rect.Pad(Margin.Left / 4).Align(new Size(rect.Height - (Margin.Top / 2), rect.Height - (Margin.Top / 2)), ContentAlignment.MiddleLeft) : rect.Pad(Margin.Left / 2).ClipTo(rect.Width - Margin.Left);
		var onBannerColor = backColor.GetTextColor();

		if (banner is null)
		{
			using var brush = new SolidBrush(Color.FromArgb(40, onBannerColor));

			e.Graphics.FillRoundedRectangle(brush, bannerRect, Margin.Left / 2);

			using var icon = customPlayset.Usage.GetIcon().Get(bannerRect.Width * 3 / 4).Color(onBannerColor);

			e.Graphics.DrawImage(icon, bannerRect.CenterR(icon.Size));
		}
		else
		{
			e.Graphics.DrawRoundedImage(banner, bannerRect, UI.Scale(5));
		}

		if (HoverState.HasFlag(HoverState.Hovered) && rect.Contains(CursorLocation))
		{
			using var brush = new SolidBrush(Color.FromArgb(40, onBannerColor));

			e.Graphics.FillRoundedRectangle(brush, bannerRect, Margin.Left / 2);
		}

		if (playset.Equals(_playsetManager.CurrentPlayset))
		{
			if (horizontal)
			{
				using var greenPen = new Pen(FormDesign.Design.GreenColor, (float)(2 * UI.FontScale));

				e.Graphics.DrawRoundedRectangle(greenPen, rect, Margin.Left / 2);
			}
			else
			{
				var activeRect = new Rectangle(bannerRect.X + (Margin.Left / 2), bannerRect.Bottom - UI.Scale(16) - (Margin.Bottom / 2), bannerRect.Width - Margin.Left, UI.Scale(16));
				using var greenBrush = new SolidBrush(FormDesign.Design.GreenColor);

				e.Graphics.FillRoundedRectangle(greenBrush, activeRect, activeRect.Height / 4);

				var text = Locale.ActivePlayset.One.ToUpper();
				using var smallFont = UI.Font(7F, FontStyle.Bold).FitTo(text, activeRect.Pad(Margin.Left / 3), e.Graphics);
				using var format = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
				using var textBrush2 = new SolidBrush(FormDesign.Design.GreenColor.GetTextColor());

				e.Graphics.DrawString(text, smallFont, textBrush2, activeRect, format);
			}
		}

		var textRect = horizontal ? rect.Pad(Margin.Left + bannerRect.Width, Margin.Left / 2, Margin.Left / 2, Margin.Left / 2) : rect.Pad(Margin.Left / 2, bannerRect.Height + Margin.Top, Margin.Left / 2, Margin.Left / 2);
		using var textBrush = new SolidBrush(onBannerColor);
		using var textFont = UI.Font(horizontal ? 8.5F : 8.75F, FontStyle.Bold).FitTo(playset.Name, textRect, e.Graphics);
		using var centerFormat = new StringFormat { LineAlignment = StringAlignment.Center };

		e.Graphics.DrawString(playset.Name, textFont, textBrush, textRect, horizontal ? centerFormat : null);

		if (!horizontal)
		{
			using var fadedBrush = new SolidBrush(Color.FromArgb(175, onBannerColor));
			using var smallTextFont = UI.Font(7F);

			e.Graphics.DrawString(customPlayset.Usage > 0 ? Locale.UsagePlayset.Format(LocaleHelper.GetGlobalText(customPlayset.Usage.ToString())) : Locale.GenericPlayset, smallTextFont, fadedBrush, new Point(textRect.X, textRect.Y + (int)e.Graphics.Measure(playset.Name, textFont, textRect.Width).Height));
		}
	}
}
