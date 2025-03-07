using Skyve.App.UserInterface.Panels;
using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain.CS2.Utilities;

using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2.UserInterface.Panels;
public partial class PC_Utilities : PanelContent
{
	private readonly ISettings _settings;
	private readonly ICitiesManager _citiesManager;
	private readonly ISubscriptionsManager _subscriptionsManager;
	private readonly INotifier _notifier;
	private readonly ILocationService _locationManager;
	private readonly IPackageManager _packageManager;
	private readonly IPackageUtil _packageUtil;
	private readonly IWorkshopService _workshopService;
	private readonly IDownloadService _downloadService;

	public PC_Utilities()
	{
		ServiceCenter.Get(out _settings, out _citiesManager, out _subscriptionsManager, out _notifier, out _locationManager, out _packageUtil, out _packageManager, out _workshopService, out _downloadService);

		InitializeComponent();

		Notifier_WorkshopSync();

		_notifier.WorkshopSyncStarted += Notifier_WorkshopSync;
		_notifier.WorkshopSyncEnded += Notifier_WorkshopSync;
	}

	private void Notifier_WorkshopSync()
	{
		var outOfDatePackages = _packageManager.Packages.AllWhere(x => _packageUtil.IsIncluded(x) && _packageUtil.GetStatus(x, out _) == DownloadStatus.OutOfDate);

		this.TryInvoke(() =>
		{
			var ready = _workshopService.IsReady && !_notifier.IsWorkshopSyncInProgress;
			B_RunSync.Enabled = ready;
			L_SyncStatus.Text = ready ? Locale.Ready : LocaleCS2.SyncOngoing;
			L_SyncStatus.ForeColor = (ready ? FormDesign.Design.GreenColor : FormDesign.Design.OrangeColor).MergeColor(FormDesign.Design.ForeColor, 75);

			outOfDatePackagesControl1.SetPackages(outOfDatePackages);
			P_Issues.Visible = outOfDatePackages.Count() > 0;
		});
	}

	public override Color GetTopBarColor()
	{
		return FormDesign.Design.BackColor.Tint(Lum: FormDesign.Design.IsDarkTheme ? 2 : -5);
	}

	protected override void LocaleChanged()
	{
		Text = LocaleSlickUI.Utilities;
		L_Troubleshoot.Text = Locale.TroubleshootInfo;
		L_PdxSyncInfo.Text = LocaleCS2.PdxSyncInfo;
		L_SyncStatusLabel.Text = LocaleCS2.CurrentStatus;
		L_SafeMode.Text = LocaleCS2.SafeModeInfo;
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		B_SafeMode.Margin = B_Troubleshoot.Margin = B_RunSync.Margin = P_Sync.Margin = P_Troubleshoot.Margin = P_Reset.Margin = P_Text.Margin = UI.Scale(new Padding(10, 0, 10, 10));
		B_ImportClipboard.Margin = UI.Scale(new Padding(10));
		L_Troubleshoot.Font = L_PdxSyncInfo.Font = L_SyncStatus.Font = UI.Font(9F);
		L_SyncStatusLabel.Font = UI.Font(9F, FontStyle.Bold);
		L_Troubleshoot.Margin = UI.Scale(new Padding(3));
		L_SafeMode.Margin = L_PdxSyncInfo.Margin = L_SyncStatus.Margin = UI.Scale(new Padding(3, 3, 3, 10));
		L_SyncStatusLabel.Margin = UI.Scale(new Padding(3, 3, 5, 10));

		foreach (Control item in P_Reset.Controls)
		{
			item.Margin = UI.Scale(new Padding(5));
		}

		slickScroll1.Reset();
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		foreach (Control item in panel1.Controls)
		{
			item.BackColor = design.BackColor;
		}

		foreach (Control item in panel2.Controls)
		{
			item.BackColor = design.BackColor;
		}
	}

	private bool DD_TextImport_ValidFile(object sender, string arg)
	{
		return true;
	}

	private void DD_TextImport_FileSelected(string obj)
	{
		LoadModsFromText(File.ReadAllText(obj), P_Text.Text);
	}

	private void B_ImportClipboard_Click(object sender, EventArgs e)
	{
		if (!Clipboard.ContainsText())
		{
			return;
		}

		LoadModsFromText(Clipboard.GetText(), B_ImportClipboard.Text);
	}

	private void LoadModsFromText(string text, string title)
	{
		var matches = Regex.Matches(text, @"(?<!\.)\b(\d{5,6})\b(?:\: (.+?) (?:[\d\.]))?");
		var packages = new List<IPackageIdentity>();

		foreach (Match item in matches)
		{
			if (ulong.TryParse(item.Groups[1].Value, out var id) && !packages.Any(x => x.Id == id))
			{
				packages.Add(new GenericPackageIdentity(id, item.Groups[2].Value));
			}
		}

		Form.PushPanel(new PC_GenericPackageList(packages, true) { Text = LocaleHelper.GetGlobalText(title) });
	}

	private void SlickScroll_Scroll(object sender, ScrollEventArgs e)
	{
		slickSpacer1.Visible = slickScroll1.Percentage != 0;
	}

	private async void B_ReloadAllData_Click(object sender, EventArgs e)
	{
		if (!B_ReloadAllData.Loading)
		{
			B_ReloadAllData.Loading = true;
			await ServiceCenter.Get<ICentralManager>().Initialize();
			B_ReloadAllData.Loading = false;
			var img = B_ReloadAllData.ImageName;
			B_ReloadAllData.ImageName = "Check";
			await Task.Delay(1500);
			B_ReloadAllData.ImageName = img;
		}
	}

	private async void B_ResetSnoozes_Click(object sender, EventArgs e)
	{
		var img = B_ResetSnoozes.ImageName;
		ServiceCenter.Get<ICompatibilityManager>().ResetSnoozes();
		B_ResetSnoozes.ImageName = "Check";
		await Task.Delay(1500);
		B_ResetSnoozes.ImageName = img;
	}

	private async void B_ResetModsCache_Click(object sender, EventArgs e)
	{
		ServiceCenter.Get<IModDllManager>().ClearDllCache();
		var img = B_ResetModsCache.ImageName;
		B_ResetModsCache.ImageName = "Check";
		await Task.Delay(1500);
		B_ResetModsCache.ImageName = img;
	}

	private async void B_ResetCompatibilityCache_Click(object sender, EventArgs e)
	{
		if (!B_ResetCompatibilityCache.Loading)
		{
			B_ResetCompatibilityCache.Loading = true;
			await Task.Run(ServiceCenter.Get<ISkyveDataManager>().ResetCache);
			B_ResetCompatibilityCache.Loading = false;
			var img = B_ResetCompatibilityCache.ImageName;
			B_ResetCompatibilityCache.ImageName = "Check";
			await Task.Delay(1500);
			B_ResetCompatibilityCache.ImageName = img;
		}
	}

	private async void B_ResetImageCache_Click(object sender, EventArgs e)
	{
		if (!B_ResetImageCache.Loading)
		{
			B_ResetImageCache.Loading = true;
			await Task.Run(() => ServiceCenter.Get<IImageService>().ClearCache(true));
			B_ResetImageCache.Loading = false;
			var img = B_ResetImageCache.ImageName;
			B_ResetImageCache.ImageName = "Check";
			await Task.Delay(1500);
			B_ResetImageCache.ImageName = img;
		}
	}

	private async void B_ResetSteamCache_Click(object sender, EventArgs e)
	{
		var img = B_ResetSteamCache.ImageName;
		_workshopService.ClearCache();
		B_ResetSteamCache.ImageName = "Check";
		await Task.Delay(1500);
		B_ResetSteamCache.ImageName = img;
	}

	private async void B_Troubleshoot_Click(object sender, EventArgs e)
	{
		var sys = ServiceCenter.Get<ITroubleshootSystem>();

		if (sys.IsInProgress)
		{
			switch (MessagePrompt.Show(Locale.CancelTroubleshootMessage, Locale.CancelTroubleshootTitle, PromptButtons.YesNoCancel, PromptIcons.Hand, form: App.Program.MainForm))
			{
				case DialogResult.Yes:
					Hide();
					await Task.Run(() => sys.Stop(true));
					break;
				case DialogResult.No:
					Hide();
					await Task.Run(() => sys.Stop(false));
					break;
			}
		}
		else
		{
			Form.PushPanel<PC_Troubleshoot>();
		}
	}

	private async void B_RunSync_Click(object sender, EventArgs e)
	{
		B_RunSync.Loading = true;
		await _workshopService.RunSync();
		B_RunSync.Loading = false;
	}

	private async void B_FixAllIssues_Click(object sender, EventArgs e)
	{
		B_FixAllIssues.Loading = true;
		B_FixAllIssues.Enabled = false;
		var outOfDatePackages = _packageManager.Packages.AllWhere(x => _packageUtil.IsIncluded(x) && _packageUtil.GetStatus(x, out _) == DownloadStatus.OutOfDate);

		foreach (var package in outOfDatePackages)
		{
			await _packageUtil.SetIncluded(package, true, withVersion: false);
		}

		await _workshopService.RunSync();

		B_FixAllIssues.Loading = false;
		B_FixAllIssues.Enabled = true;
	}

	private async void B_SafeMode_Click(object sender, EventArgs e)
	{
		B_SafeMode.Loading = true;

		_citiesManager.RunSafeMode();

		await Task.Delay(5000);

		B_SafeMode.Loading = false;
	}
}
