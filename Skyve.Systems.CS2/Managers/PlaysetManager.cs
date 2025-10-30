using Extensions;

using Newtonsoft.Json;

using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Paradox;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using SlickControls;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skyve.Systems.CS2.Managers;
internal class PlaysetManager : IPlaysetManager
{
	private readonly Dictionary<int, ICustomPlayset> _customPlaysets = [];
	private readonly Dictionary<int, IPlayset> _playsets = [];

	public IPlayset? CurrentPlayset { get; internal set; }
	public ICustomPlayset? CurrentCustomPlayset { get; internal set; }
	public IEnumerable<IPlayset> Playsets
	{
		get
		{
			List<IPlayset> playsets;

			lock (_playsets)
			{
				playsets = new(_playsets.Values);
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
	private readonly SaveHandler _saveHandler;
	private readonly INotifier _notifier;

	public PlaysetManager(ILogger logger, ILocationService locationManager, ISettings settings, IPackageManager packageManager, INotifier notifier, IPackageUtil packageUtil, IWorkshopService workshopService, ICompatibilityManager compatibilityManager, SaveHandler saveHandler)
	{
		_logger = logger;
		_locationManager = locationManager;
		_settings = settings;
		_packageManager = packageManager;
		_notifier = notifier;
		_packageUtil = packageUtil;
		_compatibilityManager = compatibilityManager;
		_saveHandler = saveHandler;
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

	public async Task<bool> DeletePlayset(IPlayset? playset)
	{
		if (playset is null)
		{
			return false;
		}

		if (await _workshopService.DeletePlayset(playset.Id))
		{
			lock (_playsets)
			{
				_playsets.Remove(playset.Id);
			}

			await RefreshCurrentPlayset();

			_notifier.OnPlaysetUpdated();

			await _workshopService.RunSync();

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
				CurrentPlayset = _playsets.TryGet(activePlayset);
				CurrentCustomPlayset = CurrentPlayset is null ? null : GetCustomPlayset(CurrentPlayset);
			}
			else
			{
				CurrentPlayset = null;
				CurrentCustomPlayset = null;
			}
		}
	}

	public async Task ActivatePlayset(IPlayset playset)
	{
		await Task.Delay(250);

		await _workshopService.ActivatePlayset(playset.Id);

		CurrentPlayset = playset;

		var customPlayset = GetCustomPlayset(playset);
		customPlayset.DateUsed = DateTime.Now;

		Save(customPlayset);

		_notifier.OnPlaysetChanged();
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
			var customPlaysets = new List<ICustomPlayset>();
			var playsets = await _workshopService.GetPlaysets(!ConnectionHandler.IsConnected || !_notifier.IsPlaysetsLoaded);
			var activePlayset = await _workshopService.GetActivePlaysetId();

			foreach (var item in playsets)
			{
				_saveHandler.Load(out ExtendedPlayset playset, CrossIO.Combine("Playsets", $"{item.Id}.json"));

				if (playset is not null)
				{
					playset.Playset = item;

					customPlaysets.Add(playset);
				}
			}

			lock (_playsets)
			{
				_playsets.Clear();
				_customPlaysets.Clear();

				foreach (var item in playsets)
				{
					_playsets[item.Id] = item;
				}

				foreach (var item in customPlaysets)
				{
					_customPlaysets[item.Id] = item;
				}

				CurrentPlayset = _playsets.TryGet(activePlayset);
				CurrentCustomPlayset = CurrentPlayset is null ? null : GetCustomPlayset(CurrentPlayset);
			}

			if (CurrentPlayset != null && !_notifier.IsPlaysetsLoaded)
			{
				_notifier.OnPlaysetChanged();
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: $"Could not load local playsets.");
		}

		_notifier.IsPlaysetsLoaded = true;

		_notifier.OnPlaysetUpdated();
	}

	public async Task<bool> RenamePlayset(IPlayset playset, string text)
	{
		if (playset == null || string.IsNullOrWhiteSpace(text))
		{
			return false;
		}

		text = text.Trim();

		if (playset is Playset playset_)
		{
			playset_.Name = text;
		}

		return await _workshopService.RenamePlayset(playset.Id, text);
	}

	public async Task<IPlayset?> CreateNewPlayset(string playsetName)
	{
		var playset = await _workshopService.CreatePlayset(playsetName);

		if (playset is not null)
		{
			_playsets[playset.Id] = playset;

			var customPlayset = GetCustomPlayset(playset);
			customPlayset.DateCreated = DateTime.Now;
			customPlayset.DateUsed = DateTime.Now;

			Save(customPlayset);
		}

		return playset;
	}

	public List<IPackage> GetInvalidPackages(IPlayset playset, PackageUsage usage)
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

			return _packageUtil.IsIncluded(x, playset.Id);
		});
	}

	public async Task<IPlayset?> AddPlayset(IPlayset playset)
	{
		var newPlayset = await _workshopService.CreatePlayset(playset.Name!);

		if (newPlayset == null)
		{
			return null;
		}

		lock (_playsets)
		{
			_playsets[newPlayset.Id] = newPlayset;
		}

		_notifier.OnPlaysetUpdated();

		return newPlayset;
	}

	public async Task<IPlayset?> ImportPlayset(string fileName, bool createNew = true)
	{
		var playset = JsonConvert.DeserializeObject<PdxPlaysetImport>(File.ReadAllText(fileName.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase) ? ExtractZipPlayset(fileName) : fileName));

		if (!createNew && _playsets.ContainsKey(playset.GeneralData?.Id ?? 0))
		{
			throw new Exception(LocaleCS2.PlaysetAlreadyImported);
		}

		var newPlayset = (createNew ? await CreateNewPlayset(playset.GeneralData?.Name ?? "New Playset") : CurrentPlayset) ?? throw new Exception(Locale.CouldNotCreatePlayset);

		if (playset.SubscribedMods is not null)
		{
			await _packageUtil.SetIncluded(playset.SubscribedMods.Values, true, newPlayset.Id);

			await _packageUtil.SetEnabled(playset.SubscribedMods.Values.Where(x => !x.IsEnabled), false, newPlayset.Id);
		}

		if (playset.LocalMods is not null)
		{
			await _packageUtil.SetIncluded(playset.LocalMods.Values, true, newPlayset.Id);

			await _packageUtil.SetEnabled(playset.LocalMods.Values.Where(x => !x.IsEnabled), false, newPlayset.Id);
		}

		var customPlayset = GetCustomPlayset(newPlayset) as ExtendedPlayset;

		customPlayset!.BannerBytes = playset.GeneralData?.BannerBytes ?? customPlayset!.BannerBytes;
		customPlayset!.Color = playset.GeneralData?.Color ?? customPlayset!.Color;
		customPlayset!.Usage = playset.GeneralData?.Usage ?? customPlayset!.Usage;

		return newPlayset;
	}

	private string ExtractZipPlayset(string fileName)
	{
		using var stream = File.OpenRead(fileName);
		using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read, false);

		var entry = zipArchive.GetEntry("Skyve\\CurrentPlayset.json");

		if (entry is null)
		{
			return string.Empty;
		}

		var file = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");

		entry.ExtractToFile(file);

		return file;
	}

	public Task<IPlayset?> CreateLogPlayset(string file)
	{
		return Task.FromResult((IPlayset?)JsonConvert.DeserializeObject<PdxPlaysetImport>(CrossIO.FileExists(file) ? File.ReadAllText(file) : file));
	}

	public async Task SetIncludedForAll(IPackageIdentity package, bool value, bool withVersion = true, bool promptForDependencies = true)
	{
		await SetIncludedForAll([package], value, withVersion, promptForDependencies);
	}

	public async Task SetIncludedForAll(IEnumerable<IPackageIdentity> packages, bool value, bool withVersion = true, bool promptForDependencies = true)
	{
		try
		{
			foreach (var playset in Playsets)
			{
				await _packageUtil.SetIncluded(packages, value, playset.Id, withVersion, promptForDependencies);
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: $"Failed to apply included status '{value}' to package: '{packages}'");
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
			_logger.Exception(ex, memberName: $"Failed to apply included status '{value}' to package: '{packages}'");
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
			_logger.Exception(ex, memberName: "Failed to create shortcut");
		}
	}

	public async Task<IPlayset?> ClonePlayset(IPlayset playset)
	{
		return await _workshopService.ClonePlayset(playset.Id);
	}

	public IPlayset? GetPlayset(int id)
	{
		lock (_playsets)
		{
			return _playsets.TryGet(id);
		}
	}

	public ICustomPlayset GetCustomPlayset(IPlayset playset)
	{
		lock (_playsets)
		{
			return _customPlaysets.TryGet(playset.Id) ?? new ExtendedPlayset(playset);
		}
	}

	public async Task DeactivateActivePlayset()
	{
		await _workshopService.DeactivateActivePlayset();

		CurrentPlayset = null;
		CurrentCustomPlayset = null;

		_notifier.OnPlaysetChanged();
	}

	public void Save(ICustomPlayset customPlayset)
	{
		try
		{
			_saveHandler.Save(customPlayset, CrossIO.Combine("Playsets", $"{customPlayset.Id}.json"));
		}
		catch (Exception ex)
		{
			_logger.Exception(ex);
		}

		lock (_playsets)
		{
			_customPlaysets[customPlayset.Id] = customPlayset;
		}

		CurrentCustomPlayset = CurrentPlayset is null ? null : GetCustomPlayset(CurrentPlayset);
	}

	public async Task<IEnumerable<IPlaysetPackage>> GetPlaysetContents(IPlayset playset, bool includeOnline)
	{
		return await _workshopService.GetModsInPlayset(playset.Id, includeOnline);
	}

	public async Task<object> GenerateImportPlayset(IPlayset? playset, bool sharing = false, bool includeOnline = true)
	{
		if (playset is null)
		{
			throw new NullReferenceException(nameof(playset));
		}

		var contents = await GetPlaysetContents(playset!, includeOnline);
		var customPlayset = sharing ? GetCustomPlayset(playset) as ExtendedPlayset : null;

		return new PdxPlaysetImport
		{
			ContractFormatVersion = -1,
			GeneralData = new()
			{
				Id = playset.Id,
				Name = playset.Name,
				BannerBytes = customPlayset?.BannerBytes,
				Color = customPlayset?.Color,
				Usage = customPlayset?.Usage
			},
			SubscribedMods = contents.Where(x => !(_workshopService.GetInfo(x)?.Tags?.Any(x => x.Key is "Map" or "Savegame") ?? false)).ConvertDictionary(x => new KeyValuePair<string, PdxPlaysetImport.ModInfo>(x.Id.ToString(), new()
			{
				Id = (int)x.Id,
				Name = x.Name,
				IsEnabled = x.IsEnabled,
				LoadOrder = x.LoadOrder,
				IsVersionLocked = x.IsVersionLocked,
				Version = x.Version,
			})),
			LocalMods = _packageManager.Packages.Where(x => x.IsLocal && _packageUtil.IsIncluded(x) && x.Name is not "Maps" and not "Saves").ConvertDictionary(x => new KeyValuePair<string, PdxPlaysetImport.ModInfo>(x.Name, new()
			{
				Name = x.Name,
				IsEnabled = _packageUtil.IsEnabled(x),
				Version = x.Version,
			}))
		};
	}

	public Task<IPlayset?> ImportPlayset(string fileName)
	{
		throw new NotImplementedException();
	}
}
