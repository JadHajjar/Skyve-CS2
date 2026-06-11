using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Forms;
public partial class ConfigLockedForm : SlickForm
{
	public ConfigLockedForm(string lockholder)
	{
		InitializeComponent();

		L_RetryInfo.Text = LocaleCS2.ConfigLockedRetry.Format(lockholder);
		L_TakeLockInfo.Text = LocaleCS2.ConfigLockedTakeLock.Format(lockholder);
		L_Info.Text = LocaleCS2.ConfigLockedInfo.Format(lockholder);
		L_Info.Font = UI.Font(7.5F);
		L_Title.Text = LocaleCS2.ConfigLockedTitle;
		L_Title.Font = UI.Font(11.75F, FontStyle.Bold);
		L_Title.Margin = UI.Scale(new Padding(0, 5, 0, 10));
		B_Retry.Margin = B_TakeLock.Margin = spacer.Margin = UI.Scale(new Padding(0, 15, 0, 5));
		L_RetryInfo.Font = L_TakeLockInfo.Font = UI.Font(7.5F, FontStyle.Italic);
		L_Info.Margin = L_RetryInfo.Margin = L_TakeLockInfo.Margin = UI.Scale(new Padding(0, 0, 0, 8));
		I_Paradox.Size = UI.Scale(new Size(48, 48));
		B_Retry.Padding = B_TakeLock.Padding =  UI.Scale(new Padding(5));
		B_Retry.Font = B_TakeLock.Font = UI.Font(9F, FontStyle.Bold);
		L_Title.MaximumSize = L_Info.MaximumSize = L_TakeLockInfo.MaximumSize = L_RetryInfo.MaximumSize = new Size((int)(UI.FontScale * 265), 9999);
		I_Close.Size = UI.Scale(new Size(24, 24));
	}

	private IEnumerable<string> GetConflictsText(ISyncConflictInfo[] conflicts)
	{
		for (var i = 0; i < conflicts.Length; i++)
		{
			if (i == 3)
			{
				yield return string.Format(LocaleCS2.BackupActivePlayset.Plural, $"+{conflicts.Length - 3}");
				yield break;
			}

			yield return conflicts[i].LocalPlaysetName ?? conflicts[i].OnlinePlaysetName ?? "N/A";
		}
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		Icon = Owner?.Icon;
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		TLP.BackColor = design.BackColor;
		L_Info.ForeColor = design.InfoColor.MergeColor(design.ForeColor);
		L_RetryInfo.ForeColor = L_TakeLockInfo.ForeColor = design.InfoColor;
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

		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void B_Cloud_Click(object sender, EventArgs e)
	{
		Close();

		ServiceCenter.Get<IWorkshopService>().RunDownSync();
	}

	private void B_Local_Click(object sender, EventArgs e)
	{
		Close();

		ServiceCenter.Get<IWorkshopService>().RunUpSync();
	}
}
