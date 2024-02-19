using Extensions;

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.Installer;
public partial class InstallingForm : Form
{
	public InstallingForm(bool uninstalling)
	{
		InitializeComponent();

		if (uninstalling)
		{
			using var resourceStream = GetType().Assembly.GetManifestResourceStream("Skyve.App.CS2.Installer.Resources.Skyve_Uninstall.png");

			pictureBox.Image = Image.FromStream(resourceStream);

			var width = Screen.PrimaryScreen.WorkingArea.Width * 2 / 10;
			Size = new Size(width, width);

			new BackgroundAction(UnInstall).Run();
		}
		else
		{
#if STABLE
			using var resourceStream = GetType().Assembly.GetManifestResourceStream("Skyve.App.CS2.Installer.Resources.Skyve_Background.png");
#else
			using var resourceStream = GetType().Assembly.GetManifestResourceStream("Skyve.App.CS2.Installer.Resources.Skyve_Background_Beta.png");
#endif

			pictureBox.Image = Image.FromStream(resourceStream);

			var width = Screen.PrimaryScreen.WorkingArea.Width * 3 / 10;
			Size = new Size(width, width * pictureBox.Image.Height / pictureBox.Image.Width);

			new BackgroundAction(Install).Run();
		}
	}

	private async void Install()
	{
		try
		{
			await Installer.Install();

			await Task.Delay(500);
		}
		catch (Exception ex)
		{
			Invoke(Hide);

			MessageBox.Show("Something unexpected went wrong while installing Skyve:\r\n\r\n" + ex.ToString());
		}
		finally
		{
			Application.Exit();
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

			MessageBox.Show("Something unexpected went wrong while un-installing Skyve:\r\n\r\n" + ex.ToString());
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
			ReleaseCapture();
			SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
		}
	}

	public const int HT_CAPTION = 0x2;
	public const int WM_NCLBUTTONDOWN = 0xA1;

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	private static extern bool ReleaseCapture();

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
}
