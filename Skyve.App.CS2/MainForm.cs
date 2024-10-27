using Skyve.App.CS2.Installer;
using Skyve.App.CS2.UserInterface.Content;
using Skyve.App.CS2.UserInterface.Panels;
using Skyve.App.Interfaces;
using Skyve.App.UserInterface.Content;
using Skyve.App.UserInterface.Lists;
using Skyve.App.UserInterface.Panels;
using Skyve.App.Utilities;
using Skyve.Systems.CS2.Services;
using Skyve.Systems.CS2.Utilities;

using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.App.CS2;

public partial class MainForm : BasePanelForm
{
	private readonly System.Timers.Timer _startTimeoutTimer = new(15000) { AutoReset = false };
	private bool isGameRunning;
	private bool? buttonStateRunning;
	private readonly TroubleshootInfoControl _troubleshootInfoControl;
	private readonly DownloadsInfoControl _downloadsInfoControl;
	private readonly UpdateAvailableControl _updateAvailableControl;

	private readonly IPlaysetManager _playsetManager;
	private readonly IPackageManager _packageManager;
	private readonly ICitiesManager _citiesManager;
	private readonly ISettings _settings;
	private readonly INotifier _notifier;
	private readonly IUserService _userService;
	private readonly IImageService _imageService;
	private readonly SkyveApiUtil _skyveApiUtil;

	public MainForm()
	{
		ServiceCenter.Get(out _skyveApiUtil, out _packageManager, out _playsetManager, out _citiesManager, out _settings, out _notifier, out _userService, out _imageService);

		InitializeComponent();

		_userService.UserInfoUpdated += _userService_UserInfoUpdated;

		_downloadsInfoControl = new() { Dock = DockStyle.Top };
		_troubleshootInfoControl = new() { Dock = DockStyle.Top };
		_updateAvailableControl = new() { Dock = DockStyle.Top };

		TLP_SideBarTools.Controls.Add(_downloadsInfoControl, 0, 0);
		TLP_SideBarTools.Controls.Add(_troubleshootInfoControl, 0, 1);
		TLP_SideBarTools.Controls.Add(_updateAvailableControl, 0, 2);

		TLP_SideBarTools.SetColumnSpan(_downloadsInfoControl, 2);
		TLP_SideBarTools.SetColumnSpan(_troubleshootInfoControl, 2);
		TLP_SideBarTools.SetColumnSpan(_updateAvailableControl, 2);

		_updateAvailableControl.MouseClick += _updateAvailableControl_MouseClick;

		base_PB_Icon.UserDraw = true;
		base_PB_Icon.Paint += Base_PB_Icon_Paint;

		SlickTip.SetTo(base_PB_Icon, string.Format(Locale.LaunchTooltip, "[F5]"));

		var currentVersion = Assembly.GetEntryAssembly().GetName().Version;

#if Stable
		L_Version.Text = "v" + currentVersion.GetString();
#else
		L_Version.Text = "v" + currentVersion.GetString() + " Beta";
#endif

		try
		{
			FormDesign.Initialize(this, DesignChanged);
		}
		catch { }

		try
		{

			if (!_settings.SessionSettings.FirstTimeSetupCompleted && string.IsNullOrEmpty(ConfigurationManager.AppSettings["GamePath"]))
			{
				SetPanel<PC_Options>(PI_Options);
			}
			else
			{
				SetPanel<PC_MainPage>(PI_Dashboard);
			}
		}
		catch (Exception ex)
		{
			OnNextIdle(() => MessagePrompt.Show(ex, "Failed to load the dashboard", form: this));
		}

		new BackgroundAction("Loading content", ServiceCenter.Get<ICentralManager>().Start).Run();

		var citiesManager = ServiceCenter.Get<ICitiesManager>();

		citiesManager.MonitorTick += CitiesManager_MonitorTick;

		isGameRunning = citiesManager.IsRunning();

#if CS1
		_playsetManager.PromptMissingItems += PromptMissingItemsEvent;
#endif

		_startTimeoutTimer.Elapsed += StartTimeoutTimer_Elapsed;
		_notifier.PlaysetChanged += PlaysetChanged;
		_notifier.RefreshUI += RefreshUI;
		_notifier.WorkshopInfoUpdated += RefreshUI;
		_notifier.WorkshopUsersInfoLoaded += RefreshUI;
		_notifier.ContentLoaded += _userService_UserInfoUpdated;
		_notifier.CompatibilityReportProcessed += _notifier_CompatibilityReportProcessed;
		_notifier.SkyveUpdateAvailable += () => Invoke(_updateAvailableControl.Show);

		ConnectionHandler.ConnectionChanged += ConnectionHandler_ConnectionChanged;

		if (CrossIO.FileExists(CrossIO.Combine(App.Program.CurrentDirectory, "batch.bat")))
		{
			try
			{
				CrossIO.DeleteFile(CrossIO.Combine(App.Program.CurrentDirectory, "batch.bat"));
			}
			catch { }
		}

		base_PB_Icon.Loading = true;
		PI_Compatibility.Loading = true;

		Task.Run(ItemListControl.LoadThumbnails);
	}

	private void PlaysetChanged()
	{
		PI_CurrentPlayset.Hidden = _playsetManager.CurrentPlayset is null;
		base_P_Tabs.FilterChanged();
	}

	private void _updateAvailableControl_MouseClick(object sender, MouseEventArgs e)
	{
		var logger = ServiceCenter.Get<ILogger>();
		var logicManager = ServiceCenter.Get<IModLogicManager>();
		var skyveApps = logicManager.GetCollection("Skyve Mod.dll");
		var mostRecent = skyveApps.Where(x => x.LocalData != null).OrderBy(x => File.GetLastWriteTimeUtc(x.LocalData?.FilePath)).LastOrDefault();

		if (mostRecent == null)
		{
			_updateAvailableControl.Hide();

			return;
		}

		try
		{
			var setupFile = Path.Combine(mostRecent.LocalData!.Folder, "Skyve Setup.exe");

			InstallHelper.Run(setupFile);

			_updateAvailableControl.Hide();
		}
		catch { }
	}

	private void _notifier_CompatibilityReportProcessed()
	{
		_notifier.CompatibilityReportProcessed -= _notifier_CompatibilityReportProcessed;

		PI_Compatibility.Loading = false;
	}

	private void _userService_UserInfoUpdated()
	{
		var hasPackages = _userService.User.Id is not null && _packageManager.Packages.Any(x => _userService.User.Id.Equals(x.GetWorkshopInfo()?.Author?.Id));
		PI_CompatibilityManagement.Hidden = !((hasPackages || _userService.User.Manager) && !_userService.User.Malicious);
		PI_ManageAllCompatibility.Hidden = PI_ReviewRequests.Hidden = !(_userService.User.Manager && !_userService.User.Malicious);
		PI_ManageYourPackages.Hidden = !(hasPackages && !_userService.User.Malicious);
		//panelItem1.Hidden = !_userService.User.Verified;
		base_P_Tabs.FilterChanged();
	}

	public void RefreshUI()
	{
		this.TryInvoke(() => Invalidate(true));
	}

	protected override void LocaleChanged()
	{
		PI_Packages.Text = Locale.Package.Plural;
		PI_Assets.Text = Locale.Asset.Plural;
		PI_Playsets.Text = Locale.Playset.Plural;
		PI_Mods.Text = Locale.Mod.Plural;
		PI_ReviewRequests.Text = LocaleCR.ReviewRequests.Format(string.Empty).Trim();
	}

	private void ConnectionHandler_ConnectionChanged(ConnectionState newState)
	{
		base_PB_Icon.Invalidate();
	}

	private void StartTimeoutTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
	{
		buttonStateRunning = null;
		//base_PB_Icon.Loading = false;

		_citiesManager.SetLaunchingStatus(false);

		//if (CurrentPanel is PC_MainPage mainPage)
		//{
		//	mainPage.B_StartStop.Loading = false;
		//}
	}

	private void CitiesManager_MonitorTick(bool isAvailable, bool isRunning)
	{
		isGameRunning = isRunning;

		if (buttonStateRunning is null || buttonStateRunning == isRunning)
		{
			if (_startTimeoutTimer.Enabled)
			{
				_startTimeoutTimer.Stop();
			}

			//if (base_PB_Icon.Loading != isRunning)
			//{
			//	base_PB_Icon.Loading = isRunning;
			//}

			base_PB_Icon.LoaderSpeed = 0.15;

			buttonStateRunning = null;

			_citiesManager.SetLaunchingStatus(false);
			//if (CurrentPanel is PC_MainPage mainPage)
			//{
			//	mainPage.B_StartStop.Loading = false;
			//}
		}
	}

	private void Base_PB_Icon_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.SetUp(base_PB_Icon.BackColor);

		var backBrightness = FormDesign.Design.MenuColor.GetBrightness();
		var foreBrightness = FormDesign.Design.ForeColor.GetBrightness();

		using var icon = new Bitmap(IconManager.GetIcons("AppIcon").FirstOrDefault(x => x.Key > base_PB_Icon.Width).Value).Color(base_PB_Icon.HoverState.HasFlag(HoverState.Hovered) && !base_PB_Icon.HoverState.HasFlag(HoverState.Pressed) ? FormDesign.Design.MenuForeColor : Math.Abs(backBrightness - foreBrightness) < 0.4F ? FormDesign.Design.BackColor : FormDesign.Design.ForeColor);

		var useGlow = !ConnectionHandler.IsConnected
			|| (buttonStateRunning is not null && buttonStateRunning != isGameRunning)
			|| isGameRunning
			|| base_PB_Icon.HoverState.HasFlag(HoverState.Pressed);

		e.Graphics.DrawImage(icon, base_PB_Icon.ClientRectangle);

		if (useGlow)
		{
			using var glowIcon = new Bitmap(IconManager.GetIcons("GlowAppIcon").FirstOrDefault(x => x.Key > base_PB_Icon.Width).Value);

			var color = FormDesign.Modern.ActiveColor;
			var minimum = 0;

			if (!ConnectionHandler.IsConnected)
			{
				minimum = 60;
				color = Color.FromArgb(194, 38, 33);
			}

			//if (_playsetManager.CurrentPlayset?.UnsavedChanges == true)
			//{
			//	minimum = 0;
			//	color = Color.FromArgb(122, 81, 207);
			//}

			if (buttonStateRunning is null && isGameRunning)
			{
				minimum = 120;
				color = Color.FromArgb(15, 153, 212);
			}

			if (buttonStateRunning == false)
			{
				minimum = 0;
				color = Color.FromArgb(232, 157, 22);
			}

			glowIcon.Tint(Sat: color.GetSaturation(), Hue: color.GetHue());

			if (base_PB_Icon.Loading && !base_PB_Icon.HoverState.HasFlag(HoverState.Pressed))
			{
				const int loops = 2;
				const int target = 256;
				var perc = (-Math.Cos(base_PB_Icon.LoaderPercentage / 100D * Math.PI * loops) * (target - minimum) / 2) + ((target + minimum) / 2);
				var alpha = (byte)perc;

				if (alpha == 0)
				{
					return;
				}

				glowIcon.Alpha(alpha);
			}

			e.Graphics.DrawImage(glowIcon, base_PB_Icon.ClientRectangle);
		}
	}

	protected override void UIChanged()
	{
		base.UIChanged();

		MinimumSize = UI.Scale(new Size(600, 350));

		_imageService.ClearCache(false);
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.F5)
		{
			if (_citiesManager.IsAvailable())
			{
				LaunchStopCities();

				return true;
			}
		}

		return base.ProcessCmdKey(ref msg, keyData);
	}

	protected override void OnAppIconClicked()
	{
		LaunchStopCities();
	}

	public void LaunchStopCities()
	{
		if (!_citiesManager.IsAvailable())
		{
			ServiceCenter.Get<ILogger>().Warning("Cities Unavailable to launch the game");

			return;
		}

		if (CrossIO.CurrentPlatform is Platform.Windows)
		{
			_citiesManager.SetLaunchingStatus(true);

			base_PB_Icon.LoaderSpeed = 1;
		}

		if (_citiesManager.IsRunning())
		{
			buttonStateRunning = false;
			new BackgroundAction("Stopping Cities: Skylines", _citiesManager.Kill).Run();
		}
		else
		{
			buttonStateRunning = true;
			new BackgroundAction("Starting Cities: Skylines", _citiesManager.Launch).Run();
		}

		_startTimeoutTimer.Stop();
		_startTimeoutTimer.Start();
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();

		if (_settings.SessionSettings.LastWindowsBounds != null)
		{
			if (!SystemInformation.VirtualScreen.Contains(_settings.SessionSettings.LastWindowsBounds.Value.Location))
			{
				return;
			}

			Bounds = _settings.SessionSettings.LastWindowsBounds.Value;

			LastUiScale = UI.UIScale;
		}

		if (_settings.SessionSettings.WindowWasMaximized)
		{
			WindowState = FormWindowState.Minimized;
			WindowState = FormWindowState.Maximized;
		}

		var assembly = Assembly.GetEntryAssembly();
		var currentVersion = assembly.GetName().Version;
		var date = File.GetLastWriteTime(assembly.Location);

		if (date > DateTime.Now.AddDays(-7))
		{
			ServiceCenter.Get<INotificationsService>().SendNotification(ServiceCenter.Get<IAppInterfaceService>().GetLastVersionNotification());
		}

		if (currentVersion.ToString() != _settings.SessionSettings.LastVersionNotification)
		{
			if (_settings.SessionSettings.FirstTimeSetupCompleted)
			{
				PushPanel(ServiceCenter.Get<IAppInterfaceService>().ChangelogPanel());
			}

			_settings.SessionSettings.LastVersionNotification = currentVersion.ToString();
			_settings.SessionSettings.Save();
		}
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		base.OnFormClosing(e);

		if (!TopMost)
		{
			if (_settings.SessionSettings.WindowWasMaximized = WindowState == FormWindowState.Maximized)
			{
				if (SystemInformation.VirtualScreen.IntersectsWith(RestoreBounds))
				{
					_settings.SessionSettings.LastWindowsBounds = RestoreBounds;
				}
			}
			else
			{
				if (SystemInformation.VirtualScreen.IntersectsWith(Bounds))
				{
					_settings.SessionSettings.LastWindowsBounds = Bounds;
				}
			}

			_settings.SessionSettings.Save();
		}
	}

	private void PI_Dashboard_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_MainPage>(PI_Dashboard);
	}

	private void PI_Mods_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_Mods>(PI_Mods);
	}

	private void PI_Assets_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_Assets>(PI_Assets);
	}

	private void PI_ModReview_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel(PI_ModUtilities, ServiceCenter.Get<IAppInterfaceService>().UtilitiesPanel());
	}

	private void PI_Packages_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_Packages>(PI_Packages);
	}

	private void PI_Options_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_Options>(PI_Options);
	}

	private void PI_Troubleshoot_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_HelpAndLogs>(PI_Troubleshoot);
	}

	private void PI_DLCs_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_DLCs>(PI_DLCs);
	}

	private void PI_Compatibility_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_CompatibilityReport>(PI_Compatibility);
	}

	private void PI_AddPlayset_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_PlaysetAdd>(PI_AddPlayset);
	}

	private void PI_CurrentPlayset_OnClick(object sender, MouseEventArgs e)
	{
		if (_playsetManager.CurrentPlayset is not null)
		{
			PushPanel(PI_CurrentPlayset, new PC_PlaysetPage(_playsetManager.CurrentPlayset, false));
		}
	}

	private async void PI_ManageYourPackages_OnClick(object sender, MouseEventArgs e)
	{
		if (PI_ManageYourPackages.Loading)
		{
			return;
		}

		PI_ManageYourPackages.Loading = true;

		try
		{
			var results = await ServiceCenter.Get<IWorkshopService>().GetWorkshopItemsByUserAsync(_userService.User.Id ?? string.Empty);

			if (results != null)
			{
				Invoke(() => PushPanel(PI_ManageYourPackages, new PC_CompatibilityManagement(results)));
			}
		}
		catch (Exception ex)
		{
			MessagePrompt.Show(ex, "Failed to load your packages", form: this);
		}

		PI_ManageYourPackages.Loading = false;
	}

	private async void PI_ReviewRequests_OnClick(object sender, MouseEventArgs e)
	{
		if (PI_ReviewRequests.Loading)
		{
			return;
		}

		PI_ReviewRequests.Loading = true;

		try
		{
			var reviewRequests = await _skyveApiUtil.GetReviewRequests();

			if (reviewRequests is not null)
			{
				Invoke(() => PushPanel(PI_ReviewRequests, new PC_ReviewRequests(reviewRequests)));
			}
		}
		catch (Exception ex)
		{
			MessagePrompt.Show(ex, "Failed to load your packages", form: this);
		}

		PI_ReviewRequests.Loading = false;
	}

	private void PI_ManageAllCompatibility_OnClick(object sender, MouseEventArgs e)
	{
		PushPanel<PC_CompatibilityManagement>(PI_ManageAllCompatibility);
	}

	private void PI_PdxMods_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_WorkshopList>(PI_PdxMods);
	}

	private void PI_Playsets_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_PlaysetList>(PI_Playsets);
	}

	private void PI_Backup_OnClick(object sender, MouseEventArgs e)
	{
		SetPanel<PC_BackupCenter>(PI_Backup);
	}

	private async void panelItem1_OnClick(object sender, MouseEventArgs e)
	{
		var io=new IOSelectionDialog();
		io.PromptFolder(this);

		if (!Directory.Exists(io.SelectedPath))
			return;
		var folder = io.SelectedPath;

		io.PromptFile(this);
		var thumb = io.SelectedPath;

var res = (await		ServiceCenter.Get<IWorkshopService, WorkshopService>().CreateCollection(folder,
			MessagePrompt.ShowInput("Name of the asset").Input,
			MessagePrompt.ShowInput("Description of the asset").Input,
			thumb));

		if (res != 0)
		{
			PlatformUtil.OpenUrl($"https://mods.paradoxplaza.com/mods/{res}/Windows");
		}
	}
}
