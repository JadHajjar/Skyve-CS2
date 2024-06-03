using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Forms;
public partial class ParadoxLoginForm : SlickForm
{
	public ParadoxLoginForm()
	{
		InitializeComponent();

		L_LoginFailed.Text = LocaleCS2.LoginFailed;
		L_RememberMeInfo.Text = LocaleCS2.RememberMeInfo;
		L_Disclaimer.Text = LocaleCS2.LoginDisclaimer;
		L_Title.Text = LocaleCS2.LoginToParadox;
		L_Title.Font = UI.Font(11.75F, FontStyle.Bold);
		L_Title.Margin = B_Login.Margin = UI.Scale(new Padding(0, 15, 0, 15));
		L_LoginFailed.Font = L_RememberMeInfo.Font = L_Disclaimer.Font = UI.Font(7.5F, FontStyle.Italic);
		L_LoginFailed.Margin = L_RememberMeInfo.Margin = L_Disclaimer.Margin = UI.Scale(new Padding(0, 8, 0, 8));
		I_Paradox.Size = UI.Scale(new Size(48, 48));
		B_Login.Padding = UI.Scale(new Padding(5));
		B_Login.Font = UI.Font(9F, FontStyle.Bold);
		TB_Email.Margin = TB_Password.Margin = B_Login.Margin = CB_RememberMe.Margin = UI.Scale(new Padding(5));
		L_Title.MaximumSize = L_LoginFailed.MaximumSize = L_RememberMeInfo.MaximumSize = L_Disclaimer.MaximumSize = new Size((int)(UI.FontScale * 230), 9999);
		TB_Email.MaximumSize = TB_Password.MaximumSize = new Size((int)(UI.FontScale * 230), L_Title.Font.Height * 4 / 3);
		TB_Email.MinimumSize = TB_Password.MinimumSize = new Size((int)(UI.FontScale * 230), L_Title.Font.Height * 4 / 3);
		I_Close.Size = UI.Scale(new Size(24, 24));
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		Icon = Owner?.Icon;
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP.BackColor = design.AccentBackColor;
		L_LoginFailed.ForeColor = design.RedColor;
		L_RememberMeInfo.ForeColor = L_Disclaimer.ForeColor = design.InfoColor;
	}

	private void TB_Password_IconClicked(object sender, EventArgs e)
	{
		TB_Password.Password = !TB_Password.Password;
		TB_Password.ImageName = TB_Password.Password ? "PasswordShow" : "PasswordHide";
	}

	private void I_Close_Click(object sender, EventArgs e)
	{
		Close();
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.Escape)
		{
			Close();

			return true;
		}

		if (keyData == Keys.Enter)
		{
			B_Login_Click(this, EventArgs.Empty);

			return true;
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private async void B_Login_Click(object sender, EventArgs e)
	{
		if (!this.CheckValidation())
		{
			return;
		}

		TB_Email.ReadOnly = TB_Password.ReadOnly = true;
		B_Login.Loading = true;
		B_Login.Enabled = false;
		B_Login.Width = B_Login.CalculateAutoSize(default).Width;

		try
		{
			var workshopService = ServiceCenter.Get<IWorkshopService>();

			if (await workshopService.Login(TB_Email.Text, TB_Password.Text, CB_RememberMe.Checked))
			{
				Hide();
				Close();

				await Task.Delay(50);

				await workshopService.RunSync();
			}
			else
			{
				Invoke(L_LoginFailed.Show);
			}
		}
		catch (Exception ex)
		{
			MessagePrompt.Show(ex, LocaleCS2.ParadoxLoginFailedTitle, form: this);
		}

		TB_Email.ReadOnly = TB_Password.ReadOnly = false;
		B_Login.Loading = false;
		B_Login.Enabled = true;
		B_Login.Width = B_Login.CalculateAutoSize(default).Width;
	}

	private void CB_RememberMe_CheckChanged(object sender, EventArgs e)
	{
		L_RememberMeInfo.Visible = CB_RememberMe.Checked;
	}
}
