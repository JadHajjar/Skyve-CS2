﻿using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Content;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Managers;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Skyve.Systems.CS2.Systems;
internal class TroubleshootSystem : ITroubleshootSystem
{
	private TroubleshootState? currentState;
	private readonly IPackageManager _packageManager;
	private readonly IModLogicManager _modLogicManager;
	private readonly PlaysetManager _playsetManager;
	private readonly ISettings _settings;
	private readonly INotifier _notifier;
	private readonly IPackageUtil  _packageUtil;
	private readonly IModUtil _modUtil;

	public event Action? StageChanged;
	public event Action? AskForConfirmation;
	public event Action<List<ILocalPackageIdentity>>? PromptResult;

	public bool IsInProgress => currentState is not null;
	public string CurrentAction => LocaleHelper.GetGlobalText(currentState?.Stage.ToString());
	public bool WaitingForGameLaunch => currentState?.Stage is ActionStage.WaitingForGameLaunch;
	public bool WaitingForGameClose => currentState?.Stage is ActionStage.WaitingForGameClose;
	public bool WaitingForPrompt => currentState?.Stage is ActionStage.WaitingForConfirmation;
	public int CurrentStage => currentState?.CurrentStage ?? 0;
	public int TotalStages => currentState?.TotalStages ?? 0;

	public TroubleshootSystem(IPackageManager packageManager, IPlaysetManager playsetManager, ISettings settings, INotifier notifier, ICitiesManager citiesManager, IPackageUtil packageUtil, IModLogicManager modLogicManager, IModUtil modUtil)
	{
		try
		{ ISave.Load(out currentState, "TroubleshootState.json"); }
		catch { }

		_packageManager = packageManager;
		_playsetManager = (PlaysetManager)playsetManager;
		_modLogicManager = modLogicManager;
		_settings = settings;
		_notifier = notifier;
		_packageUtil = packageUtil;
		_modUtil = modUtil;

		citiesManager.MonitorTick += CitiesManager_MonitorTick;
	}

	public async void Start(ITroubleshootSettings settings)
	{
		if (_playsetManager.CurrentPlayset is null)
			return;

		currentState = new()
		{
			Stage = ActionStage.WaitingForConfirmation,
			Playset = (Playset)_playsetManager.CurrentPlayset,
			Mods = settings.Mods,
			ItemIsCausingIssues = settings.ItemIsCausingIssues,
			ItemIsMissing = settings.ItemIsMissing,
			NewItemCausingIssues = settings.NewItemCausingIssues,
			ProcessingItems = new(),
			UnprocessedItems = new()
		};

		IEnumerable<IPackageIdentity> packages = settings.Mods ? _packageManager.Packages : _packageManager.Assets;

		var packageToProcess = new List<ILocalPackageIdentity>();

		foreach (var item in packages)
		{
			if (CheckPackageValidity(settings, item))
			{
				if (item.GetLocalPackageIdentity() is ILocalPackageIdentity mod && _modLogicManager.IsRequired(mod, _modUtil))
				{
					continue;
				}

				if (item.GetPackageInfo()?.Statuses?.Any(x => x.Type is StatusType.StandardMod) == true)
				{
					continue;
				}

				packageToProcess.AddIfNotNull(item.GetLocalPackageIdentity());
			}
		}

		currentState.ProcessingItems = currentState.UnprocessedItems = GetItemGroups(packageToProcess.ToList());

		currentState.TotalStages = (int)Math.Ceiling(Math.Log(currentState.UnprocessedItems.Count, 2));

		if (currentState.TotalStages == 0)
		{
			if (packageToProcess.Any())
			{
				PromptResult?.Invoke(packageToProcess);
			}

			currentState = null;

			return;
		}

		var playset = await _playsetManager.ClonePlayset(_playsetManager.CurrentPlayset);

		_playsetManager.SetCurrentPlayset(playset);

		ApplyConfirmation(true);
	}

	private static bool CheckPackageValidity(ITroubleshootSettings settings, IPackageIdentity item)
	{
		if (settings.ItemIsCausingIssues)
		{
			return item.IsIncluded() == true;
		}

		if (settings.ItemIsMissing)
		{
			return item.IsIncluded() == false;
		}

		if (settings.NewItemCausingIssues)
		{
			if (!item.IsIncluded())
			{
				return false;
			}

			if (item.GetLocalPackage()?.LocalTime > DateTime.Today.AddDays(-7))
			{
				return true;
			}
		}

		return false;
	}

	public void Stop(bool keepSettings)
	{
		if (currentState is null)
		{
			return;
		}

		if (!keepSettings)
		{
			_playsetManager.ApplyPlayset(currentState.Playset!);
		}

		_playsetManager.SetCurrentPlayset(_playsetManager.Playsets.FirstOrDefault(x => x.Name == currentState.PlaysetName));

		_notifier.OnPlaysetChanged();

		_settings.SessionSettings.CurrentPlayset = currentState.PlaysetName;
		_settings.SessionSettings.Save();

		ISave.Delete("TroubleshootState.json");

		currentState = null;

		StageChanged?.Invoke();
	}

	public void ApplyConfirmation(bool issuePersists)
	{
		if (currentState?.Stage == ActionStage.WaitingForConfirmation)
		{
			NextStage();

			ApplyNextSettings(issuePersists);

			NextStage();
		}
	}

	public void NextStage()
	{
		if (currentState is null)
		{
			return;
		}

		switch (currentState.Stage)
		{
			case ActionStage.ApplyingSettings:
				currentState.Stage = ActionStage.WaitingForGameLaunch;
				break;
			case ActionStage.WaitingForGameLaunch:
				currentState.Stage = ActionStage.WaitingForGameClose;
				break;
			case ActionStage.WaitingForGameClose:
				currentState.Stage = ActionStage.WaitingForConfirmation;
				break;
			case ActionStage.WaitingForConfirmation:
				currentState.Stage = ActionStage.ApplyingSettings;
				break;
		}

		Save();

		StageChanged?.Invoke();

		if (currentState.Stage is ActionStage.WaitingForConfirmation)
		{
			AskForConfirmation?.Invoke();
		}
	}

	private void ApplyNextSettings(bool issuePersists)
	{
		_playsetManager.ApplyPlayset(currentState!.Playset!);

		var lists = SplitGroup(issuePersists ? currentState.ProcessingItems! : currentState.UnprocessedItems!);

		if (lists.processingItems.Count == 1 && lists.unprocessedItems.Count == 0)
		{
			if (lists.processingItems[0].Count > 1)
			{
				lists = SplitGroup(lists.processingItems[0].ToList(x => new List<string> { x }));
			}
			else
			{
				_packageUtil.SetIncluded(GetPackages(new[] { lists.processingItems[0][0] }), currentState.ItemIsMissing);

				PromptResult?.Invoke(GetPackages(new[] { lists.processingItems[0][0] }).ToList());

				Stop(true);

				return;
			}
		}

		currentState.ProcessingItems = lists.processingItems;
		currentState.UnprocessedItems = lists.unprocessedItems;

		_packageUtil.SetIncluded(GetPackages(currentState.ProcessingItems.SelectMany(x => x)), currentState.ItemIsMissing);

		currentState.CurrentStage++;
	}

	private (List<List<string>> processingItems, List<List<string>> unprocessedItems) SplitGroup(List<List<string>> list)
	{
		var list1 = new List<List<string>>();
		var list2 = new List<List<string>>();

		foreach (var item in list.OrderByDescending(x => x.Count))
		{
			if (list1.Sum(x => x.Count) > list2.Sum(x => x.Count))
			{
				list2.Add(item);
			}
			else
			{
				list1.Add(item);
			}
		}

		return (list1, list2);
	}

	private List<List<string>> GetItemGroups(List<ILocalPackageIdentity> items)
	{
		var groups = new List<List<string>>();

		while (items.Count > 0)
		{
			var item = items[0];

			var list = new List<string>();

			GetPairedItems(items, list, item);

			groups.Add(list);

			items.RemoveAll(x => list.Contains(x.FilePath));
		}

		return groups;
	}

	private void GetPairedItems(List<ILocalPackageIdentity> items, List<string> group, ILocalPackageIdentity current)
	{
		foreach (var item in items)
		{
			if (group.Contains(item.FilePath))
			{
				continue;
			}

			if (item == current)
			{
				group.Add(item.FilePath);
			}
			else if (!currentState!.ItemIsMissing && currentState!.Mods && AreItemsPaired(item, current))
			{
				GetPairedItems(items, group, item);
			}
		}
	}

	private bool AreItemsPaired(IPackageIdentity packageA, IPackageIdentity packageB)
	{
		if (packageA != null && packageB != null)
		{
			return packageA.GetWorkshopInfo()?.Requirements.Any(x => x.Id == packageB.Id) == true
				|| packageB.GetWorkshopInfo()?.Requirements.Any(x => x.Id == packageA.Id) == true;
		}

		return false;
	}

	private IEnumerable<ILocalPackageIdentity> GetPackages(IEnumerable<string> packagePaths)
	{
		IEnumerable<IPackageIdentity> packages = currentState!.Mods ? _packageManager.Packages : _packageManager.Assets;

		foreach (var package in packages)
		{
			if (packagePaths.Contains(package.GetLocalPackageIdentity()!.FilePath))
			{
				yield return package.GetLocalPackageIdentity()!;
			}
		}
	}

	private void Save()
	{
		ISave.Save(currentState, "TroubleshootState.json");
	}

	private void CitiesManager_MonitorTick(bool isAvailable, bool isRunning)
	{
		if (currentState == null)
		{
			return;
		}

		if (currentState.Stage == ActionStage.WaitingForGameLaunch && isRunning)
		{
			NextStage();
		}
		else if (currentState.Stage == ActionStage.WaitingForGameClose && !isRunning && isAvailable)
		{
			NextStage();
		}
	}

	public void CleanDownload(List<ILocalPackageData> packages)
	{
		PackageWatcher.Pause();
		foreach (var item in packages)
		{
			try
			{
				CrossIO.DeleteFolder(item.Folder);
			}
			catch (Exception ex)
			{
				ServiceCenter.Get<ILogger>().Exception(ex, $"Failed to delete the folder '{item.Folder}'");
			}
		}

		PackageWatcher.Resume();

		SteamUtil.Download(packages);
	}

	public class TroubleshootState : ITroubleshootSettings
	{
		public string? PlaysetName { get; set; }
		public Playset? Playset { get; set; }
		public List<List<string>>? UnprocessedItems { get; set; }
		public List<List<string>>? ProcessingItems { get; set; }
		public ActionStage Stage { get; set; }
		public int CurrentStage { get; set; }
		public int TotalStages { get; set; }
		public bool ItemIsCausingIssues { get; set; }
		public bool ItemIsMissing { get; set; }
		public bool NewItemCausingIssues { get; set; }
		public bool Mods { get; set; }
	}

	public enum ActionStage
	{
		ApplyingSettings,
		WaitingForGameLaunch,
		WaitingForGameClose,
		WaitingForConfirmation,
	}
}
