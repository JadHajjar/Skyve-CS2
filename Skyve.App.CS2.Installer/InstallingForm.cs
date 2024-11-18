using Extensions;

using SlickControls;

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.Installer;
public partial class InstallingForm : SlickForm
{
	public InstallingForm(bool uninstalling)
	{
		InitializeComponent();

		var currentInstallationPath = Installer.GetCurrentInstallationPath();

		if (uninstalling)
		{
			LastUiScale = UI.UIScale;

			using var resourceStream = GetType().Assembly.GetManifestResourceStream("Skyve.App.CS2.Installer.Resources.Skyve_Uninstall.png");

			pictureBox.Image = Image.FromStream(resourceStream);

			var width = Screen.PrimaryScreen.WorkingArea.Width * 2 / 10;
			Size = new Size(width, width);

			new BackgroundAction(UnInstall).Run();
		}
		else
		{
			if (!string.IsNullOrEmpty(currentInstallationPath)) // Check if this is an update
			{
				LastUiScale = UI.UIScale;
				StartInstall();
			}
			else
			{
				FormDesign.Initialize(this, DesignChanged);
				TLP_Main.Visible = true;
				pictureBox.Visible = false;
			}
		}
	}

	private void StartInstall()
	{
#if STABLE
		using var resourceStream = GetType().Assembly.GetManifestResourceStream("Skyve.App.CS2.Installer.Resources.Skyve_Background.png");
#else
		using var resourceStream = GetType().Assembly.GetManifestResourceStream("Skyve.App.CS2.Installer.Resources.Skyve_Background_Beta.png");
#endif

		pictureBox.Image = Image.FromStream(resourceStream);

		var width = Screen.PrimaryScreen.WorkingArea.Width * 3 / 10;
		Bounds = Screen.PrimaryScreen.WorkingArea.CenterR(new Size(width, width * pictureBox.Image.Height / pictureBox.Image.Width));

		new BackgroundAction(Install).Run();
	}

	private async void Install()
	{
		var close = true;

		try
		{
			await Installer.Install();

			await Task.Delay(500);
		}
		catch (Exception ex)
		{
			Invoke(Hide);

			if (ex is KnownException)
			{
				MessagePrompt.Show(ex.InnerException, ex.Message);

				TLP_Main.Visible = true;
				pictureBox.Visible = false;

				close = false;
			}
			else
			{
				MessagePrompt.Show(ex, "Something unexpected went wrong while installing Skyve:");
			}
		}
		finally
		{
			if (close)
			{
				Application.Exit();
			}
		}
	}

	private async void UnInstall()
	{
		try
		{
			await Installer.UnInstall();
		}
		catch (Exception ex)
		{
			Invoke(Hide);

			MessagePrompt.Show(ex, "Something unexpected went wrong while un-installing Skyve:");
		}
		finally
		{
			Application.Exit();
		}
	}

	private void PictureBox_MouseDown(object sender, MouseEventArgs e)
	{
		if (e == null || e.Button == MouseButtons.Left)
		{
			Form_MouseDown(sender, e);
		}
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		roundedTableLayoutPanel1.Padding = TLP_Main.Padding = UI.Scale(new Padding(15));
		pictureBox1.Size = UI.Scale(new Size(64, 64));
		label1.Font = UI.Font(20F, FontStyle.Bold);
		slickButton2.Margin = UI.Scale(new Padding(0, 0, 10, 0));
		slickButton2.Font = slickButton1.Font = UI.Font(9.75F);
		label2.Margin = UI.Scale(new Padding(5, 20, 0, 5));
		label3.Margin = UI.Scale(new Padding(5, 5, 0, 0));
		TB_InstallPath.Margin = UI.Scale(new Padding(0, 0, 0, 15));
		label2.Font =		label3.Font = UI.Font(7.5F, FontStyle.Italic);
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		roundedTableLayoutPanel1.BackColor = design.MenuColor;
		roundedTableLayoutPanel1.ForeColor = design.MenuForeColor;
		TLP_Main.BackColor = design.BackColor;
		label2.ForeColor = label3.ForeColor = design.InfoColor.MergeColor(design.ForeColor);
	}

	private void PB_Background_Paint(object sender, PaintEventArgs e)
	{
		var icon1 = Properties.Resources.I_AppIcon_128;
		var icon2 = Properties.Resources.I_GlowAppIcon_128;

		icon2.Tint(Sat: FormDesign.Design.ActiveColor.GetSaturation(), Hue: FormDesign.Design.ActiveColor.GetHue());

		var backBrightness = FormDesign.Design.MenuColor.GetBrightness();
		var foreBrightness = FormDesign.Design.ForeColor.GetBrightness();

		e.Graphics.DrawImage(icon1.Color(Math.Abs(backBrightness - foreBrightness) < 0.4F ? FormDesign.Design.BackColor : FormDesign.Design.ForeColor), pictureBox1.ClientRectangle);
		e.Graphics.DrawImage(icon2, pictureBox1.ClientRectangle);
	}

	private void TB_InstallPath_PathSelected(object sender, EventArgs e)
	{
		var folder = Path.GetFileName(TB_InstallPath.Text);

		if (!folder.Contains("skyve", StringComparison.InvariantCultureIgnoreCase))
		{
			TB_InstallPath.Text = Path.Combine(TB_InstallPath.Text, "Skyve CS-II");
		}
	}

	private void B_Install_Click(object sender, EventArgs e)
	{
		if (TB_InstallPath.Text.PathContains(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)))
		{
			MessagePrompt.Show("You can not install Skyve in this folder. Please choose another one.", "Invalid Folder", PromptButtons.OK, PromptIcons.Hand, this);
			return;
		}

		Installer.SetInstallSettings(TB_InstallPath.Text, CB_CreateShortcut.Checked, CB_InstallService.Checked);
		StartInstall();
		TLP_Main.Visible = false;
		pictureBox.Visible = true;
	}

	private void B_Cancel_Click(object sender, EventArgs e)
	{
		Close();
	}
}
