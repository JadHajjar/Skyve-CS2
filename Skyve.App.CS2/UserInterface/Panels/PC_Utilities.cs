﻿using Skyve.App.UserInterface.Panels;
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
	private readonly IPackageManager _contentManager;
	private readonly IPackageUtil _packageUtil;
	private readonly IWorkshopService _workshopService;
	private readonly IDownloadService _downloadService;

	public PC_Utilities()
	{
		ServiceCenter.Get(out _settings, out _citiesManager, out _subscriptionsManager, out _notifier, out _locationManager, out _packageUtil, out _contentManager, out _workshopService, out _downloadService);

		InitializeComponent();

		Notifier_WorkshopSync();

		_notifier.WorkshopSyncStarted += Notifier_WorkshopSync;
		_notifier.WorkshopSyncEnded += Notifier_WorkshopSync;
	}

	private void Notifier_WorkshopSync()
	{
		this.TryInvoke(() =>
		{
			var ready = _workshopService.IsReady && !_notifier.IsWorkshopSyncInProgress;
			B_RunSync.Enabled = ready;
			L_SyncStatus.Text = ready ? Locale.Ready : LocaleCS2.SyncOngoing;
			L_SyncStatus.ForeColor = (ready ? FormDesign.Design.GreenColor : FormDesign.Design.OrangeColor).MergeColor(FormDesign.Design.ForeColor, 75);
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
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		B_Troubleshoot.Margin = B_RunSync.Margin = P_Sync.Margin = P_Troubleshoot.Margin = P_Reset.Margin = P_Text.Margin = UI.Scale(new Padding(10, 0, 10, 10), UI.FontScale);
		B_ImportClipboard.Margin = UI.Scale(new Padding(10), UI.FontScale);
		L_Troubleshoot.Font=L_PdxSyncInfo.Font=L_SyncStatus.Font = UI.Font(9F);
		L_SyncStatusLabel.Font = UI.Font(9F, FontStyle.Bold);
		L_Troubleshoot.Margin = UI.Scale(new Padding(3), UI.FontScale);
		L_PdxSyncInfo.Margin = L_SyncStatus.Margin = UI.Scale(new Padding(3, 3, 3, 10), UI.FontScale);
		L_SyncStatusLabel.Margin = UI.Scale(new Padding(3, 3, 5, 10), UI.FontScale);

		foreach (Control item in P_Reset.Controls)
		{
			item.Margin = UI.Scale(new Padding(5), UI.FontScale);
		}
	}

	protected override void DesignChanged(FormDesign design)
	{
		base.DesignChanged(design);

		foreach (Control item in TLP_Main.Controls)
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
		var matches = Regex.Matches(File.ReadAllText(obj), "(\\d{5,6})");
		var assets = new List<IPackageIdentity>();

		foreach (Match item in matches)
		{
			if (ulong.TryParse(item.Groups[1].Value, out var id) && !assets.Any(x => x.Id == id))
			{
				assets.Add(new GenericPackageIdentity(id));
			}
		}

		Form.PushPanel(new PC_GenericPackageList(assets, true) { Text = LocaleHelper.GetGlobalText(P_Text.Text) });
	}

	private void B_ImportClipboard_Click(object sender, EventArgs e)
	{
		if (!Clipboard.ContainsText())
		{
			return;
		}

		var matches = Regex.Matches(Clipboard.GetText(), "(\\d{5,6})");
		var assets = new List<IPackageIdentity>();

		foreach (Match item in matches)
		{
			if (ulong.TryParse(item.Groups[1].Value, out var id) && !assets.Any(x => x.Id == id))
			{
				assets.Add(new GenericPackageIdentity(id));
			}
		}

		Form.PushPanel(new PC_GenericPackageList(assets, true) { Text = LocaleHelper.GetGlobalText(B_ImportClipboard.Text) });
	}

	private void slickScroll1_Scroll(object sender, ScrollEventArgs e)
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
		ShowPrompt("Coming soon...", icon: PromptIcons.Info);return;
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
}
