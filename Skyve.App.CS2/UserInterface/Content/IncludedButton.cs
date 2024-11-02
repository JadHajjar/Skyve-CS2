using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Content;
public class IncludedButton : SlickButton
{
	private readonly IModLogicManager _modLogicManager;
	private readonly IModUtil _modUtil;
	private readonly ISubscriptionsManager _subscriptionsManager;

	public IPackageIdentity Package { get; set; }

	public IncludedButton(IPackageIdentity package)
	{
		Package = new GenericPackageIdentity(package)
		{
			Version = null
		};

		ServiceCenter.Get(out _modLogicManager, out _modUtil, out _subscriptionsManager);
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
			var isIncluded = Package.IsIncluded(out var partialIncluded) && !partialIncluded;

			Loading = true;

			if (!isIncluded || ModifierKeys.HasFlag(Keys.Alt))
			{
				await _modUtil.SetIncluded(Package, !isIncluded);
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

	public override Size CalculateAutoSize(Size availableSize)
	{
		return new Size(Width, UI.Scale(30));
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SetUp(Parent?.BackColor ?? BackColor);

		var localIdentity = Package.GetLocalPackageIdentity();
		var isIncluded = Package.IsIncluded(out var isPartialIncluded);
		var isEnabled = Package.IsEnabled();
		Color activeColor = default;
		string text;

		if (localIdentity is null && Package.IsLocal())
		{
			return; // missing local item
		}

		var required = _modLogicManager.IsRequired(localIdentity, _modUtil);
		var isHovered = Loading || HoverState.HasFlag(HoverState.Hovered);

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

		using var brush = ClientRectangle.Gradient(activeColor);
		e.Graphics.FillRoundedRectangle(brush, ClientRectangle, UI.Scale(4));

		Rectangle iconRect;

		if (Loading)
		{
			iconRect = new Rectangle((Height - (ClientRectangle.Height * 3 / 5)) / 2, (Height - (ClientRectangle.Height * 3 / 5)) / 2, ClientRectangle.Height * 3 / 5, ClientRectangle.Height * 3 / 5);

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

		var textRect = new Rectangle(iconRect.Right + iconRect.X, 0, Width - iconRect.Right - (iconRect.X * 2), Height);
		using var font = UI.Font(9F).FitToWidth(text, textRect, e.Graphics);
		using var textBrush = new SolidBrush(iconColor);
		using var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
		e.Graphics.DrawString(text, font, textBrush, textRect, format);
	}
}
