using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using SlickControls;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using PlaysetSubscribedMod = PDX.SDK.Contracts.Service.Mods.Models.PlaysetSubscribedMod;

namespace Skyve.Systems.CS2.Managers;
internal class PlaysetManager : IPlaysetManager
{
	private readonly List<ICustomPlayset> _playsets;
	private bool disableAutoSave;
	private readonly FileWatcher? _watcher;

	public ICustomPlayset? CurrentPlayset { get; internal set; }
	public IEnumerable<ICustomPlayset> Playsets
	{
		get
		{
			List<ICustomPlayset> playsets;

			lock (_playsets)
			{
				playsets = new(_playsets);
			}

			foreach (var playset in playsets)
			{
				yield return playset;
			}
		}
	}

	private readonly WorkshopService _workshopService;
	private readonly ILogger _logger;
	private readonly ILocationService _locationManager;
	private readonly ISettings _settings;
	private readonly IPackageManager _packageManager;
	private readonly IPackageUtil _packageUtil;
	private readonly ICompatibilityManager _compatibilityManager;
	private readonly INotifier _notifier;
	private readonly IModUtil _modUtil;
	private readonly IAssetUtil _assetUtil;
	private readonly IDlcManager _dlcManager;

	public PlaysetManager(ILogger logger, ILocationService locationManager, ISettings settings, IPackageManager packageManager, IPackageUtil packageUtil, ICompatibilityManager compatibilityManager, INotifier notifier, IModUtil modUtil, IAssetUtil assetUtil, IDlcManager dlcManager, IWorkshopService workshopService)
	{
		_logger = logger;
		_locationManager = locationManager;
		_settings = settings;
		_packageManager = packageManager;
		_packageUtil = packageUtil;
		_compatibilityManager = compatibilityManager;
		_notifier = notifier;
		_modUtil = modUtil;
		_assetUtil = assetUtil;
		_dlcManager = dlcManager;
		_workshopService = (workshopService as WorkshopService)!;
		_playsets ??= new();

		_notifier.AutoSaveRequested += OnAutoSave;
	}

	public async Task<bool> MergeIntoCurrentPlayset(IPlayset playset)
	{
		var modsInPlayset = await _workshopService.Context!.Mods.ListModsInPlayset(playset.Id);

		if (modsInPlayset.Success)
		{
			var result = await _workshopService.Context.Mods.SubscribeBulk(modsInPlayset.Mods.Select(x => new KeyValuePair<int, string>(((PlaysetSubscribedMod)x).Id, x.DisplayName)), playset.Id);
		
			return result.Success;
		}

		return false;
	}

	public async Task<bool> ExcludeFromCurrentPlayset(IPlayset playset)
	{
		var modsInPlayset = await _workshopService.Context!.Mods.ListModsInPlayset(playset.Id);
		
		if (modsInPlayset.Success)
		{
			foreach (var item in modsInPlayset.Mods)
			{
				await _workshopService.Context.Mods.Unsubscribe(((PlaysetSubscribedMod)item).Id, playset.Id);
			}

			return true;
		}

		return false;
	}

	public async Task<bool> DeletePlayset(ICustomPlayset playset)
	{
		if ((await _workshopService.Context!.Mods.DeletePlayset(playset.Id)).Success)
		{
			lock (_playsets)
			{
				_playsets.Remove(playset);
			}

			await RefreshCurrentPlayset();

			_notifier.OnPlaysetUpdated();

			return true;
		}

		return false;
	}

	private async Task RefreshCurrentPlayset()
	{
		var currentPlayset = await _workshopService.Context!.Mods.GetActivePlayset();

		if (currentPlayset.Success)
		{
			lock (_playsets)
			{
				CurrentPlayset = _playsets.FirstOrDefault(x => x.Id == currentPlayset.PlaysetId);

				if(CurrentPlayset is null)
				{
					throw new NotImplementedException();
				}
			}
		}
	}

	public void SetCurrentPlayset(ICustomPlayset playset)
	{
		CurrentPlayset = playset;

		if (playset.Temporary)
		{
			_notifier.OnPlaysetChanged();

			_settings.SessionSettings.CurrentPlayset = null;
			_settings.SessionSettings.Save();

			try
			{
				CrossIO.DeleteFile(CrossIO.Combine(_locationManager.SkyveSettingsPath, "CurrentPlayset"));
			}
			catch { }

			return;
		}

		File.WriteAllText(CrossIO.Combine(_locationManager.SkyveSettingsPath, "CurrentPlayset"), playset.Name);

		if (SystemsProgram.MainForm as SlickForm is null)
		{
			ApplyPlayset(playset, true);
		}
		else
		{
			new BackgroundAction("Applying playset", () => ApplyPlayset(playset, true)).Run();
		}
	}

	internal void ApplyPlayset(ICustomPlayset playset, bool setCurrentPlayset)
	{
		try
		{
			_workshopService.Context!.Mods.ActivatePlayset(playset.Id);
		}
		catch (Exception ex)
		{
			MessagePrompt.Show(ex, "Failed to apply your playset", form: SystemsProgram.MainForm as SlickForm);

			_notifier.OnPlaysetChanged();
		}
		finally
		{
			_notifier.ApplyingPlayset = false;
			disableAutoSave = false;
		}
	}

	public void OnAutoSave()
	{
		//if (!disableAutoSave && !_notifier.ApplyingPlayset && _notifier.IsContentLoaded && !_notifier.BulkUpdating)
		//{
		//	var playset = (CurrentPlayset as Playset)!;

		//	if (playset.AutoSave)
		//	{
		//		playset.Save();
		//	}
		//	else if (!CurrentPlayset.Temporary)
		//	{
		//		playset.UnsavedChanges = true;

		//		Save(CurrentPlayset);
		//	}
		//}
	}

	private async Task LoadAllPlaysets()
	{
		try
		{
			var playsets = await _workshopService.GetAllPlaysets(true);

			lock (_playsets)
			{
				_playsets.Clear();
				_playsets.AddRange(playsets);
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Could not load local playsets.");
		}

		_notifier.PlaysetsLoaded = true;

		_notifier.OnPlaysetUpdated();
	}

	public async Task<bool> RenamePlayset(IPlayset playset, string text)
	{
		if (playset == null || playset.Temporary)
		{
			return false;
		}

		return (await _workshopService.Context!.Mods.RenamePlayset(playset.Id, text)).Success;
	}

	public async Task<ICustomPlayset> CreateNewPlayset(string playsetName)
	{
		var newPlayset = await _workshopService.Context!.Mods.CreatePlayset(playsetName);

		return new Playset(newPlayset) { LastEditDate = DateTime.Now };
	}

	public List<ILocalPackageWithContents> GetInvalidPackages(PackageUsage usage)
	{
		if ((int)usage == -1)
		{
			return new();
		}

		return _packageManager.Packages.AllWhere(x =>
		{
			var cr = _compatibilityManager.GetPackageInfo(x);

			if (cr is null)
			{
				return false;
			}

			if (cr.Usage.HasFlag(usage))
			{
				return false;
			}

			return _packageUtil.IsIncluded(x, out var partial) || partial;
		});
	}

	public void AddPlayset(ICustomPlayset newPlayset)
	{
		lock (_playsets)
		{
			_playsets.Add(newPlayset);
		}

		_notifier.OnPlaysetUpdated();
	}

	public ICustomPlayset? ImportPlayset(string obj)
	{
		throw new NotImplementedException();
	}

	public void SetIncludedForAll(IPackage package, bool value)
	{
		try
		{
			foreach (var playset in Playsets)
			{
				if (value)
				{
					_workshopService.Context!.Mods.Enable((int)package.Id, playset.Id);
				}
				else
				{
					_workshopService.Context!.Mods.Disable((int)package.Id, playset.Id);
				}
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Failed to apply included status '{value}' to package: '{package}'");
		}
	}

	public async Task<bool> IsPackageIncludedInPlayset(IPackage package, IPlayset playset)
	{
		var modDetail = await _workshopService.Context!.Mods.GetDetails((int)package.Id);

		if (modDetail.Success && modDetail.Mod.Playsets.Any(x => x.PlaysetId == playset.Id))
		{
			return true;
		}

		return false;
	}

	public void SetIncludedFor(IPackage package, IPlayset playset, bool value)
	{
		if (value)
		{
			_workshopService.Context!.Mods.Enable((int)package.Id, playset.Id);
		}
		else
		{
			_workshopService.Context!.Mods.Disable((int)package.Id, playset.Id);
		}
	}

	public string GetFileName(IPlayset playset)
	{
		return CrossIO.Combine(_workshopService.Context!.Config.Mods.RootPath, "playsets_metadata", playset.Id.ToString());
	}

	public void CreateShortcut(IPlayset item)
	{
		try
		{
			var launch = MessagePrompt.Show(Locale.AskToLaunchGameForShortcut, PromptButtons.YesNo, PromptIcons.Question) == DialogResult.Yes;

			ExtensionClass.CreateShortcut(CrossIO.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), item.Name + ".lnk")
				, Application.ExecutablePath
				, (launch ? "-launch " : "") + $"-playset {item.Name}");
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, "Failed to create shortcut");
		}
	}

	internal async Task<ICustomPlayset> ClonePlayset(IPlayset playset)
	{
		var newPlayset = await _workshopService.Context!.Mods.ClonePlayset(playset.Id);

		return new Playset(newPlayset) { LastEditDate = DateTime.Now };
	}
}
