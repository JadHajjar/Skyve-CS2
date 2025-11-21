using Skyve.App.Utilities;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Forms;
public partial class VersionObsoletePrompt : SlickForm
{
	public VersionObsoletePrompt()
	{
		InitializeComponent();

		L_Title.Text = Text = LocaleCS2.SkyveVersionBlockedTitle;
		L_Description.Text = LocaleCS2.SkyveVersionBlockedDescription;
	}

	protected override void UIChanged()
	{
		Size = UI.Scale(new Size(300, 400));
		Font = UI.Font(8.25F);
		LastUiScale = UI.UIScale;

		L_Title.Font = UI.Font(12.75F, FontStyle.Bold);
		L_Description.Font = UI.Font(9.75F);
		PB_Icon.Size = UI.Scale(new Size(64, 64));
		PB_Icon.Margin = UI.Scale(new Padding(20, 20, 20, 5));
		L_Title.Margin = UI.Scale(new Padding(10, 10, 10, 20));
		L_Description.Margin = UI.Scale(new Padding(15));
		B_Close.Margin = B_Link.Margin = UI.Scale(new Padding(10, 20, 10, 10));
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP_Main.BackColor = design.BackColor;
	}

	private void PB_Icon_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.SetUp(PB_Icon.BackColor);

		var backBrightness = FormDesign.Design.MenuColor.GetBrightness();
		var foreBrightness = FormDesign.Design.ForeColor.GetBrightness();

		using var icon = new Bitmap(IconManager.GetIcons("AppIcon")
			.FirstOrAny(x => x.Key > PB_Icon.Width).Value)
			.Color(FormDesign.Design.ForeColor);

		e.Graphics.DrawImage(icon, PB_Icon.ClientRectangle);

		using var glowIcon = new Bitmap(IconManager.GetIcons("GlowAppIcon").FirstOrAny(x => x.Key > PB_Icon.Width).Value);

		var color = Color.FromArgb(194, 38, 33);

		glowIcon.Tint(Sat: color.GetSaturation(), Hue: color.GetHue());

		e.Graphics.DrawImage(glowIcon, PB_Icon.ClientRectangle);
	}

	private void B_Link_Click(object sender, EventArgs e)
	{
		var currentVersion = Assembly.GetEntryAssembly().GetName().Version;

		PlatformUtil.OpenUrl("https://skyve-mod.com/obsolete?v=" + currentVersion);
	}

	private void B_Close_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}
}
