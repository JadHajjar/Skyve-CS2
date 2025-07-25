using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Forms;
public partial class SyncConflictForm : SlickForm
{
	public SyncConflictForm(ISyncConflictInfo[] conflicts)
	{
		InitializeComponent();

		L_CloudInfo.Text = LocaleCS2.SyncCloudInfo;
		L_LocalInfo.Text = LocaleCS2.SyncLocalInfo;
		L_Info.Text = LocaleCS2.SyncConflictInfo;
		L_Playsets.Visible = conflicts.Length > 0;
		L_Playsets.Text = LocaleCS2.SyncAffectedPlaysets.FormatPlural(conflicts.Length, string.Join(", ", GetConflictsText(conflicts)));
		L_Info.Font = L_Playsets.Font = UI.Font(7.5F);
		L_Title.Text = LocaleCS2.SyncConflictTitle;
		L_Title.Font = UI.Font(11.75F, FontStyle.Bold);
		L_Title.Margin = UI.Scale(new Padding(0, 5, 0, 10));
		B_Cloud.Margin = B_Local.Margin = spacer.Margin = UI.Scale(new Padding(0, 15, 0, 5));
		L_CloudInfo.Font = L_LocalInfo.Font = UI.Font(7.5F, FontStyle.Italic);
		L_Info.Margin = L_CloudInfo.Margin = L_LocalInfo.Margin = UI.Scale(new Padding(0, 0, 0, 8));
		I_Paradox.Size = UI.Scale(new Size(48, 48));
		B_Cloud.Padding = B_Local.Padding =  UI.Scale(new Padding(5));
		B_Cloud.Font = B_Local.Font = UI.Font(9F, FontStyle.Bold);
		L_Title.MaximumSize = L_Info.MaximumSize = L_Playsets.MaximumSize = L_LocalInfo.MaximumSize = L_CloudInfo.MaximumSize = new Size((int)(UI.FontScale * 265), 9999);
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
		L_CloudInfo.ForeColor = L_LocalInfo.ForeColor = design.InfoColor;
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
