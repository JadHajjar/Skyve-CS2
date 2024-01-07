using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2;
using Skyve.Domain.CS2.Content;
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

	public PlaysetManager(ILogger logger, ILocationService locationManager, ISettings settings, IPackageManager packageManager, INotifier notifier, IPackageUtil packageUtil, IWorkshopService workshopService, ICompatibilityManager compatibilityManager)
	{
		_logger = logger;
		_locationManager = locationManager;
		_settings = settings;
		_packageManager = packageManager;
		_notifier = notifier;
		_packageUtil = packageUtil;
		_compatibilityManager = compatibilityManager;
		_workshopService = (workshopService as WorkshopService)!;
		_playsets ??= new();

		_notifier.AutoSaveRequested += OnAutoSave;
	}

	public async Task<bool> MergeIntoCurrentPlayset(IPlayset playset)
	{
		var modsInPlayset = await _workshopService.GetModsInPlayset(playset.Id, true);

		if (modsInPlayset.Any())
		{
			await _packageUtil.SetIncluded(modsInPlayset, true);

			return true;
		}

		return false;
	}

	public async Task<bool> ExcludeFromCurrentPlayset(IPlayset playset)
	{
		var modsInPlayset = await _workshopService.GetModsInPlayset(playset.Id);

		if (modsInPlayset.Any())
		{
			await _packageUtil.SetIncluded(modsInPlayset, false);

			return true;
		}

		return false;
	}

	public async Task<bool> DeletePlayset(ICustomPlayset playset)
	{
		if (await _workshopService.DeletePlayset(playset.Id))
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
		var currentPlayset = await _workshopService.GetActivePlaysetId();

		if (currentPlayset > 0)
		{
			lock (_playsets)
			{
				CurrentPlayset = _playsets.FirstOrDefault(x => x.Id == currentPlayset);

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
			ApplyPlayset(playset);
		}
		else
		{
			new BackgroundAction("Applying playset", () => ApplyPlayset(playset)).Run();
		}
	}

	internal async void ApplyPlayset(ICustomPlayset playset)
	{
		try
		{
			await _workshopService.ActivatePlayset(playset.Id);

			_notifier.OnPlaysetChanged();
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

	public async Task Initialize()
	{
		try
		{
			var playsets = await _workshopService.GetPlaysets(true);
			var activePlayset = await _workshopService.GetActivePlaysetId();

			lock (_playsets)
			{
				_playsets.Clear();
				_playsets.AddRange(playsets);

				CurrentPlayset = _playsets.FirstOrDefault(x => x.Id == activePlayset);
			}

			if (CurrentPlayset != null)
			{
				_notifier.OnPlaysetChanged();
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

		return await _workshopService.RenamePlayset(playset.Id, text);
	}

	public async Task<ICustomPlayset?> CreateNewPlayset(string playsetName)
	{
		return await _workshopService.CreatePlayset(playsetName);
	}

	public List<IPackage> GetInvalidPackages(PackageUsage usage)
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

	public async Task SetIncludedForAll(IPackageIdentity package, bool value)
	{
		try
		{
			foreach (var playset in Playsets)
			{
				if (value)
				{
				await	_packageUtil.SetIncluded(package, true, playset.Id);
				}
				else
				{
				await	_packageUtil.SetIncluded(package, false, playset.Id);
				}
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Failed to apply included status '{value}' to package: '{package}'");
		}
	}

	public string GetFileName(IPlayset playset)
	{
		return string.Empty;// CrossIO.Combine(_workshopService.Context!.Config.Mods.RootPath, "playsets_metadata", playset.Id.ToString());
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

	internal async Task<ICustomPlayset?> ClonePlayset(IPlayset playset)
	{
		return await _workshopService.ClonePlayset(playset.Id);
	}
}
