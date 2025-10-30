using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
public class IncludedButton : SlickButton
{
	private readonly IModLogicManager _modLogicManager;
	private readonly IModUtil _modUtil;
	private readonly IPlaysetManager _playsetManager;
	private readonly ISubscriptionsManager _subscriptionsManager;

	public IPackageIdentity Package { get; set; }

	public IncludedButton(IPackageIdentity package)
	{
		Package = package;

		ServiceCenter.Get(out _modLogicManager, out _modUtil, out _playsetManager, out _subscriptionsManager);
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.Alt)
		{
			Invalidate();
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	protected override async void OnMouseClick(MouseEventArgs e)
	{
		base.OnMouseClick(e);

		if (e.Button == MouseButtons.Left)
		{
			if (!Package.IsLocal())
			{
				if (e.Location.X > Width - Height)
				{
					await ServiceCenter.Get<IWorkshopService>().ToggleVote(Package);

					return;
				}

				if (e.Location.X > Width - Height - UI.Scale(8))
				{
					return;
				}
			}

			var isIncluded = Package.IsIncluded(out var partialIncluded, withVersion: false) && !partialIncluded;

			Loading = true;

			if (!isIncluded || ModifierKeys.HasFlag(Keys.Alt))
			{
				await _modUtil.SetIncluded(Package, !isIncluded, withVersion: false);
			}
			else
			{
				var enable = !Package.IsEnabled();

				if (enable || !_modLogicManager.IsRequired(Package.GetLocalPackageIdentity(), _modUtil))
				{
					await _modUtil.SetEnabled(Package, enable);
				}
			}

			Loading = false;
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);

		if (!Package.IsLocal())
		{
			if (e.Location.X > Width - Height)
			{
				SlickTip.SetTo(this, Package.GetWorkshopInfo()?.HasVoted == true ? LocaleCS2.UnVoteMod : LocaleCS2.VoteMod, offset: new Point(Width - Height, 0));
				Cursor = Cursors.Hand;

				return;
			}

			if (e.Location.X > Width - Height - UI.Scale(8))
			{
				SlickTip.SetTo(this, string.Empty);
				Cursor = Cursors.Default;

				return;
			}
		}

		SlickTip.SetTo(this, _playsetManager.CurrentPlayset is null && !Package.IsLocal() ? Locale.NoActivePlayset : string.Empty);
		Cursor = _playsetManager.CurrentPlayset is null && !Package.IsLocal() ? Cursors.Default : Cursors.Hand;
	}

	public override Size CalculateAutoSize(Size availableSize)
	{
		return new Size(Width, UI.Scale(30));
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(Parent?.BackColor ?? BackColor);

		var localIdentity = Package.GetLocalPackageIdentity();

		if (localIdentity is null && Package.IsLocal())
		{
			return; // missing local item
		}

		if (Package.IsLocal())
		{
			DrawIncludeButton(e, ClientRectangle, localIdentity);

			return;
		}

		DrawIncludeButton(e, ClientRectangle.Pad(0, 0, Height + UI.Scale(8), 0), localIdentity);

		DrawLikeButton(e, new Rectangle(Width - Height, 0, Height, Height));
	}

	private void DrawIncludeButton(PaintEventArgs e, Rectangle buttonRect, ILocalPackageIdentity? localIdentity)
	{
		var isIncluded = Package.IsIncluded(out var isPartialIncluded, withVersion: false);
		var isEnabled = Package.IsEnabled(withVersion: false);
		Color activeColor = default;
		string text;

		var required = _modLogicManager.IsRequired(localIdentity, _modUtil);
		var isHovered = !(_playsetManager.CurrentPlayset is null && !Package.IsLocal()) && (Loading || (buttonRect.Contains(CursorLocation) && HoverState.HasFlag(HoverState.Hovered)));

		if (isHovered)
		{
			if (!isIncluded)
			{
				text = LocaleCS2.IncludeItem;
			}
			else if (required)
			{
				text = Locale.ThisModIsRequiredYouCantDisableIt;
			}
			else if (ModifierKeys.HasFlag(Keys.Alt))
			{
				text = Locale.ExcludeItem;
			}
			else if (isEnabled)
			{
				text = Locale.DisableItem;
			}
			else
			{
				text = Locale.EnableItem;
			}
		}
		else
		{
			if (isPartialIncluded)
			{
				text = Locale.PartiallyIncluded;
			}
			else if (!isIncluded)
			{
				text = LocaleCS2.IncludeItem;
			}
			else if (isEnabled)
			{
				text = Locale.Enabled;
			}
			else
			{
				text = Locale.Disabled;
			}
		}

		if (!required && isIncluded && isHovered)
		{
			isPartialIncluded = false;
			isEnabled = !isEnabled;
		}

		if (isEnabled)
		{
			activeColor = isPartialIncluded ? FormDesign.Design.YellowColor : FormDesign.Design.GreenColor;
		}

		Color iconColor;

		if (required && activeColor != default)
		{
			iconColor = !FormDesign.Design.IsDarkTheme ? activeColor.MergeColor(ForeColor, 75) : activeColor;
			activeColor = activeColor.MergeColor(BackColor, !FormDesign.Design.IsDarkTheme ? 35 : 20);
		}
		else if (activeColor == default && isHovered)
		{
			iconColor = isIncluded ? isEnabled ? FormDesign.Design.GreenColor : FormDesign.Design.RedColor : FormDesign.Design.ActiveForeColor;
			activeColor = isIncluded ? Color.FromArgb(40, iconColor) : Color.FromArgb(200, FormDesign.Design.ForeColor.MergeColor(FormDesign.Design.ActiveColor));
		}
		else
		{
			if (activeColor == default)
			{
				activeColor = Color.FromArgb(isIncluded ? 20 : 200, FormDesign.Design.ForeColor);
			}
			else if (isHovered)
			{
				activeColor = activeColor.MergeColor(ForeColor, 75);
			}

			iconColor = activeColor.GetTextColor();
		}

		using var brush = buttonRect.Gradient(activeColor);
		e.Graphics.FillRoundedRectangle(brush, buttonRect, UI.Scale(4));

		Rectangle iconRect;

		if (Loading)
		{
			iconRect = new Rectangle((Height - (buttonRect.Height * 3 / 5)) / 2, (Height - (buttonRect.Height * 3 / 5)) / 2, buttonRect.Height * 3 / 5, buttonRect.Height * 3 / 5);

			if (_subscriptionsManager.Status.ModId != Package.Id || _subscriptionsManager.Status.Progress == 0 || !_subscriptionsManager.Status.IsActive)
			{
				DrawLoader(e.Graphics, iconRect, iconColor);
			}
			else
			{
				var width = Math.Min(Math.Min(iconRect.Width, iconRect.Height), (int)(32 * UI.UIScale));
				var size = (float)Math.Max(2, width / (8D - (Math.Abs(100 - LoaderPercentage) / 50)));
				var drawSize = new SizeF(width - size, width - size);
				var rect = new RectangleF(new PointF(iconRect.X + ((iconRect.Width - drawSize.Width) / 2), iconRect.Y + ((iconRect.Height - drawSize.Height) / 2)), drawSize).Pad(size / 2);
				using var pen = new Pen(iconColor, size) { StartCap = LineCap.Round, EndCap = LineCap.Round };

				e.Graphics.DrawArc(pen, rect, -90, 360 * _subscriptionsManager.Status.Progress);
			}
		}
		else
		{
			var icon = new DynamicIcon(_subscriptionsManager.IsSubscribing(Package) ? "Wait" : isPartialIncluded ? "Slash" : isEnabled ? "Ok" : !isIncluded ? "Add" : (isHovered && ModifierKeys.HasFlag(Keys.Alt)) ? "X" : "Enabled");
			using var includedIcon = icon.Get(Height * 3 / 4).Color(iconColor);

			iconRect = new Rectangle(new Point((Height - includedIcon.Height) / 2, (Height - includedIcon.Height) / 2), includedIcon.Size);
			e.Graphics.DrawImage(includedIcon, iconRect);
		}

		var textRect = new Rectangle(iconRect.Right + iconRect.X, 0, buttonRect.Width - iconRect.Right - (iconRect.X * 2), Height);
		using var font = UI.Font(9F).FitToWidth(text, textRect, e.Graphics);
		using var textBrush = new SolidBrush(iconColor);
		using var format = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
		e.Graphics.DrawString(text, font, textBrush, textRect, format);

		if (_playsetManager.CurrentPlayset is null && !Package.IsLocal())
		{
			var dimBrush = new SolidBrush(Color.FromArgb(150, BackColor));
			e.Graphics.FillRectangle(dimBrush, buttonRect);
		}
	}

	private void DrawLikeButton(PaintEventArgs e, Rectangle rectangle)
	{
		var hasVoted = Package.GetWorkshopInfo()?.HasVoted ?? false;

		Draw(e, new ButtonDrawArgs
		{
			Icon = hasVoted ? "VoteFilled" : "Vote",
			Rectangle = rectangle,
			ColorStyle = ColorStyle.Green,
			ButtonType = hasVoted ? ButtonType.Active : ButtonType.Normal,
			BorderRadius = rectangle.Height / 2,
			HoverState = rectangle.Contains(CursorLocation) ? (HoverState & ~HoverState.Focused) : default,
		});
	}
}
