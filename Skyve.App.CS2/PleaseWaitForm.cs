using Skyve.Systems.CS2.Utilities;

using SlickControls;

using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2;
public partial class PleaseWaitForm : SlickForm
{
	private readonly string[] _args;

	public PleaseWaitForm(string[] args)
	{
		InitializeComponent();
		_args = args;

		LastUiScale = UI.UIScale;
		
		var width = Screen.PrimaryScreen.WorkingArea.Width * 3 / 10;
		Bounds = Screen.PrimaryScreen.WorkingArea.CenterR(new Size(width, width * pictureBox.Image.Height / pictureBox.Image.Width));
	}

	private void PictureBox_MouseDown(object sender, MouseEventArgs e)
	{
		if (e == null || e.Button == MouseButtons.Left)
		{
			Form_MouseDown(sender, e);
		}
	}

	protected override async void OnCreateControl()
	{
		base.OnCreateControl();

		await Task.Run(() =>
		{
			CommandUtil.Parse(_args);
			Application.Exit();
		});
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);

		e.Cancel = true;
	}
}
