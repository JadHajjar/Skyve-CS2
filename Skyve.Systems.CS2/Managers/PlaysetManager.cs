using Extensions;

using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
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

namespace Skyve.Systems.CS2.Managers;
internal class PlaysetManager : IPlaysetManager
{
	private readonly List<ICustomPlayset> _customPlaysets = [];
	private readonly List<IPlayset> _playsets = [];

	public IPlayset? CurrentPlayset { get; internal set; }
	public ICustomPlayset? CurrentCustomPlayset { get; internal set; }
	public IEnumerable<IPlayset> Playsets
	{
		get
		{
			List<IPlayset> playsets;

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

		_notifier.AutoSaveRequested += OnAutoSave;
		_notifier.WorkshopSyncEnded += async () => await Initialize();
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

	public async Task<bool> DeletePlayset(IPlayset playset)
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
		var activePlayset = await _workshopService.GetActivePlaysetId();

		lock (_playsets)
		{
			if (activePlayset > 0)
		{
				CurrentPlayset = _playsets.FirstOrDefault(x => x.Id == activePlayset);
				CurrentCustomPlayset = CurrentPlayset is null ? null : GetCustomPlayset(CurrentPlayset);
			}
			else
			{ CurrentPlayset = null;
				CurrentCustomPlayset = null;
			}
		}
	}

	public void SetCurrentPlayset(IPlayset playset)
	{
		CurrentPlayset = playset;
		CurrentCustomPlayset = GetCustomPlayset(playset);

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

	internal async void ApplyPlayset(IPlayset playset)
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
			_notifier.IsApplyingPlayset = false;
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
			var playsets = await _workshopService.GetPlaysets(!ConnectionHandler.IsConnected || !_notifier.IsPlaysetsLoaded);
			var activePlayset = await _workshopService.GetActivePlaysetId();

			lock (_playsets)
			{
				_playsets.Clear();
				_playsets.AddRange(playsets);

				CurrentPlayset = _playsets.FirstOrDefault(x => x.Id == activePlayset);
				CurrentCustomPlayset = CurrentPlayset is null ? null : GetCustomPlayset(CurrentPlayset);
			}

			if (CurrentPlayset != null && !_notifier.IsPlaysetsLoaded)
			{
				_notifier.OnPlaysetChanged();
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Could not load local playsets.");
		}

		_notifier.IsPlaysetsLoaded = true;

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

	public async Task<IPlayset?> CreateNewPlayset(string playsetName)
	{
		return await _workshopService.CreatePlayset(playsetName);
	}

	public List<IPackage> GetInvalidPackages(PackageUsage usage)
	{
		if ((int)usage == -1)
		{
			return [];
		}

		return _packageManager.Packages.AllWhere(x =>
		{
			var cr = x.GetPackageInfo();

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

	public void AddPlayset(IPlayset newPlayset)
	{
		lock (_playsets)
		{
			_playsets.Add(newPlayset);
		}

		_notifier.OnPlaysetUpdated();
	}

	public IPlayset? ImportPlayset(string obj)
	{
		throw new NotImplementedException();
	}

	public async Task SetIncludedForAll(IPackageIdentity package, bool value)
	{
		await SetIncludedForAll([package], value);
	}

	public async Task SetIncludedForAll(IEnumerable<IPackageIdentity> packages, bool value)
	{
		try
		{
			foreach (var playset in Playsets)
			{
				await _packageUtil.SetIncluded(packages, value, playset.Id);
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Failed to apply included status '{value}' to package: '{packages}'");
		}
	}

	public async Task SetEnabledForAll(IPackageIdentity package, bool value)
	{
		await SetEnabledForAll([package], value);
	}

	public async Task SetEnabledForAll(IEnumerable<IPackageIdentity> packages, bool value)
	{
		try
		{
			foreach (var playset in Playsets)
			{
				if (value)
				{
					await _packageUtil.SetIncluded(packages, true, playset.Id);
					await _packageUtil.SetEnabled(packages, true, playset.Id);
				}
				else
				{
					await _packageUtil.SetEnabled(packages, false, playset.Id);
				}
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, $"Failed to apply included status '{value}' to package: '{packages}'");
		}
	}

	public string GetFileName(IPlayset playset)
	{
		return CrossIO.Combine(_locationManager.DataPath, ".cache", "Mods", "playsets_metadata", playset.Id.ToString());
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

	public async Task<IPlayset?> ClonePlayset(IPlayset playset)
	{
		return await _workshopService.ClonePlayset(playset.Id);
	}

	public IPlayset? GetPlayset(int id)
	{
		return _playsets.FirstOrDefault(x => x.Id == id);
	}

	public ICustomPlayset GetCustomPlayset(IPlayset playset)
	{
		return _customPlaysets.FirstOrDefault(x => x.Id == playset.Id) ?? new ExtendedPlayset(playset);
	}

	public async Task DeactivateActivePlayset()
	{
		await _workshopService.DeactivateActivePlayset();

		CurrentPlayset = null;
		CurrentCustomPlayset = null;

		_notifier.OnPlaysetChanged();
	}
}
