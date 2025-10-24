using Extensions;

using Skyve.Compatibility.Domain.Enums;
using Skyve.Domain;
using Skyve.Domain.Enums;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Managers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ILogger = Skyve.Domain.Systems.ILogger;

namespace Skyve.Systems.CS2.Systems;
internal class TroubleshootSystem : ITroubleshootSystem
{
	private const string FILE_NAME = "TroubleshootState.json";

	private bool busy;
	private TroubleshootState? currentState;
	private readonly IPackageManager _packageManager;
	private readonly IModLogicManager _modLogicManager;
	private readonly PlaysetManager _playsetManager;
	private readonly ISettings _settings;
	private readonly INotifier _notifier;
	private readonly IPackageUtil _packageUtil;
	private readonly IModUtil _modUtil;
	private readonly ILogger _logger;
	private readonly SaveHandler _saveHandler;

	public event Action? StageChanged;
	public event Action? AskForConfirmation;
	public event Action<IEnumerable<ILocalPackageIdentity>>? PromptResult;

	public bool IsInProgress => currentState is not null;
	public bool IsBusy => busy && currentState is not null;
	public string CurrentAction => currentState is not null ? LocaleHelper.GetGlobalText((currentState.Stage & ~ActionStage.Primary & ~ActionStage.Secondary).ToString()) : string.Empty;
	public bool WaitingForGameLaunch => currentState?.Stage.HasFlag(ActionStage.WaitingForGameLaunch) ?? false;
	public bool WaitingForGameClose => currentState?.Stage.HasFlag(ActionStage.WaitingForGameClose) ?? false;
	public bool WaitingForPrompt => currentState?.Stage.HasFlag(ActionStage.WaitingForConfirmation) ?? false;
	public int CurrentStage => currentState?.CurrentStage ?? 0;
	public int TotalStages => ComputeStageCount((currentState?.LeftProcessingItems?.Count ?? 0) + (currentState?.RightProcessingItems?.Count ?? 0) + (currentState?.ProcessedItems?.Count ?? 0));

	public TroubleshootSystem(ILogger logger, IPackageManager packageManager, IPlaysetManager playsetManager, ISettings settings, INotifier notifier, ICitiesManager citiesManager, IPackageUtil packageUtil, IModLogicManager modLogicManager, IModUtil modUtil, SaveHandler saveHandler)
	{
		try
		{
			saveHandler.Load(out currentState, FILE_NAME);
		}
		catch { }

		_packageManager = packageManager;
		_playsetManager = (PlaysetManager)playsetManager;
		_modLogicManager = modLogicManager;
		_settings = settings;
		_notifier = notifier;
		_packageUtil = packageUtil;
		_modUtil = modUtil;
		_logger = logger;
		_saveHandler = saveHandler;

		citiesManager.MonitorTick += CitiesManager_MonitorTick;
	}

	public async Task<TroubleshootResult> Start(ITroubleshootSettings settings)
	{
		if (busy)
		{
			return TroubleshootResult.Busy;
		}

		if (_playsetManager.CurrentPlayset is null)
		{
			return TroubleshootResult.NoActivePlayset;
		}

		currentState = new()
		{
			Stage = ActionStage.Secondary | ActionStage.WaitingForConfirmation,
			OriginalPlaysetId = _playsetManager.CurrentPlayset.Id,
			Mods = settings.Mods,
			ItemIsCausingIssues = settings.ItemIsCausingIssues,
			ItemIsMissing = settings.ItemIsMissing,
			NewItemCausingIssues = settings.NewItemCausingIssues,
			UnprocessedItems = [],
			ProcessedItems = []
		};

		var packageToProcess = new List<GenericLocalPackageIdentity>();

		foreach (var item in _packageManager.Packages)
		{
			if (!CheckPackageValidity(item, out var localIdentity)
				|| (localIdentity is not null && _modLogicManager.IsRequired(localIdentity, _modUtil))
				|| (item.GetPackageInfo()?.Statuses?.Any(x => x.Type is StatusType.StandardMod) == true))
			{
				if (localIdentity is not null && _packageUtil.IsIncludedAndEnabled(item, currentState.OriginalPlaysetId, false))
				{
					currentState.UnprocessedItems.Add(new(localIdentity));
				}
			}
			else
			{
				packageToProcess.Add(new(localIdentity!));
			}
		}

		currentState.LeftProcessingItems = [];
		currentState.RightProcessingItems = GetItemGroups(packageToProcess);

		if (TotalStages <= 1)
		{
			currentState = null;

			Save();

			if (packageToProcess.Any())
			{
				PromptResult?.Invoke(packageToProcess);

				return TroubleshootResult.Success;
			}

			return TroubleshootResult.NoPacakgesToProcess;
		}

		var playset = await _playsetManager.CreateNewPlayset("[Troubleshoot] " + _playsetManager.CurrentPlayset.Name);

		if (playset == null)
		{
			currentState = null;

			return TroubleshootResult.CouldNotCreatePlayset;
		}

		currentState.TroubleshootingPlaysetId = playset.Id;

		await _playsetManager.ActivatePlayset(playset);

		await _packageUtil.SetIncluded(currentState.UnprocessedItems, true, playset.Id, true, false);

		return await ApplyConfirmation(true);
	}

	private int ComputeStageCount(int totalItems)
	{
		return Math.Max(currentState?.CurrentStage ?? 0, 2 * (int)Math.Ceiling(Math.Log(2 * Math.Ceiling(totalItems / 2D), 2)));
	}

	private bool CheckPackageValidity(IPackageIdentity item, out ILocalPackageIdentity? localPackageIdentity)
	{
		if (currentState is null || item.IsLocal())
		{
			localPackageIdentity = null;
			return false;
		}

		if (item.GetLocalPackageIdentity() is not ILocalPackageIdentity localIdentity)
		{
			localPackageIdentity = null;
			return false;
		}

		localPackageIdentity = localIdentity;

		if (currentState.ItemIsCausingIssues)
		{
			return _packageUtil.IsIncludedAndEnabled(item, currentState.OriginalPlaysetId, false);
		}

		if (currentState.ItemIsMissing)
		{
			return _packageUtil.IsIncludedAndEnabled(item, currentState.OriginalPlaysetId, false);
		}

		if (currentState.NewItemCausingIssues)
		{
			if (!_packageUtil.IsIncludedAndEnabled(item, currentState.OriginalPlaysetId, false))
			{
				return false;
			}

			return localIdentity.LocalTime > DateTime.UtcNow.AddDays(-10);
		}

		return false;
	}

	public async Task<TroubleshootResult> Stop(bool keepSettings)
	{
		if (currentState is null)
		{
			return TroubleshootResult.InvalidState;
		}

		try
		{
			busy = true;

			if (!keepSettings)
			{
				var originalPlayset = _playsetManager.GetPlayset(currentState.OriginalPlaysetId);

				if (originalPlayset != null)
				{
					await _playsetManager.ActivatePlayset(originalPlayset);

					await _playsetManager.DeletePlayset(_playsetManager.GetPlayset(currentState.TroubleshootingPlaysetId));
				}
				else
				{
					_logger.Warning("Trying to go stop troubleshooting while the original playset is no longer available");

					currentState = null;

					Save();

					StageChanged?.Invoke();

					return TroubleshootResult.InvalidState;
				}
			}

			currentState = null;

			Save();

			StageChanged?.Invoke();

			return TroubleshootResult.Success;
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: $"Unexpected error in {nameof(TroubleshootSystem)}.{nameof(ApplyConfirmation)}");

			return TroubleshootResult.Error;
		}
		finally
		{
			busy = false;
		}
	}

	public async Task<TroubleshootResult> ApplyConfirmation(bool issuePersists)
	{
		if (currentState is null || !currentState.Stage.HasFlag(ActionStage.WaitingForConfirmation))
		{
			return TroubleshootResult.InvalidState;
		}

		if (busy)
		{
			return TroubleshootResult.Busy;
		}

		try
		{
			busy = true;

			var stage = currentState.Stage;

			GoToNextStage();

			var applyResult = await ApplyNextSettings(issuePersists);

			if (applyResult < TroubleshootResult.Error)
			{
				GoToNextStage();

				return TroubleshootResult.Success;
			}

			currentState.Stage = stage;

			Save();

			StageChanged?.Invoke();

			return applyResult;
		}
		catch (Exception ex)
		{
			_logger.Exception(ex, memberName: $"Unexpected error in {nameof(TroubleshootSystem)}.{nameof(ApplyConfirmation)}");

			return TroubleshootResult.Error;
		}
		finally
		{
			busy = false;
		}
	}

	public void GoToNextStage()
	{
		if (currentState is null)
		{
			return;
		}

		var subStage = currentState.Stage & (ActionStage.Primary | ActionStage.Secondary);

		switch (currentState.Stage & ~subStage)
		{
			case ActionStage.ApplyingSettings:
				currentState.Stage = ActionStage.WaitingForGameLaunch | (subStage is ActionStage.Primary ? ActionStage.Secondary : ActionStage.Primary);
				break;
			case ActionStage.WaitingForGameLaunch:
				currentState.Stage = ActionStage.WaitingForGameClose | subStage;
				break;
			case ActionStage.WaitingForGameClose:
				currentState.Stage = ActionStage.WaitingForConfirmation | subStage;
				break;
			case ActionStage.WaitingForConfirmation:
				currentState.Stage = ActionStage.ApplyingSettings | subStage;
				break;
		}

		Save();

		StageChanged?.Invoke();

		if (currentState.Stage.HasFlag(ActionStage.WaitingForConfirmation))
		{
			AskForConfirmation?.Invoke();
		}
	}

	private async Task<TroubleshootResult> ApplyNextSettings(bool issuePersists)
	{
		if (currentState is null)
		{
			return TroubleshootResult.InvalidState;
		}

		if (_playsetManager.CurrentPlayset?.Id != currentState.TroubleshootingPlaysetId)
		{
			var playset = _playsetManager.GetPlayset(currentState.TroubleshootingPlaysetId);

			if (playset is null)
			{
				_logger.Warning("Trying to go to the next troubleshoot stage while the playset is no longer available");

				await Stop(true);

				return TroubleshootResult.InvalidState;
			}

			await _playsetManager.ActivatePlayset(playset);
		}

		if (currentState.Stage.HasFlag(ActionStage.Primary))
		{
			await _packageUtil.SetIncluded(currentState.LeftProcessingItems?.AsLocalPackageIdentity() ?? [], false, currentState.TroubleshootingPlaysetId, true, false);
			await _packageUtil.SetIncluded(currentState.RightProcessingItems?.AsLocalPackageIdentity() ?? [], true, currentState.TroubleshootingPlaysetId, true, false);

			currentState.LastResult = issuePersists;

			currentState.CurrentStage++;

			return TroubleshootResult.Success;
		}

		if (issuePersists == currentState.LastResult) // Both groups resulted in the same thing, issue is likely unrelated to mods
		{
			PromptResult?.Invoke([]);

			return await Stop(false);
		}

		(PackageList Left, PackageList Right) group;

		var itemsToSplit = (issuePersists ? currentState.RightProcessingItems : currentState.LeftProcessingItems) ?? [];
		var processedItems = (issuePersists ? currentState.LeftProcessingItems : currentState.RightProcessingItems) ?? [];

		group = SplitGroup(itemsToSplit);

		if (group.Right.Count == 0) // Last Stage Reached
		{
			await _packageUtil.SetIncluded(processedItems.AsLocalPackageIdentity(), true, currentState.TroubleshootingPlaysetId, true, false);
			await _packageUtil.SetIncluded(group.Left.AsLocalPackageIdentity(), false, currentState.TroubleshootingPlaysetId, true, false);

			PromptResult?.Invoke(group.Left.AsLocalPackageIdentity());

			return await Stop(true);
		}

		await _packageUtil.SetIncluded(group.Left.AsLocalPackageIdentity().Concat(processedItems.AsLocalPackageIdentity()), true, currentState.TroubleshootingPlaysetId, true, false);
		await _packageUtil.SetIncluded(group.Right.AsLocalPackageIdentity(), false, currentState.TroubleshootingPlaysetId, true, false);

		currentState.LeftProcessingItems = group.Left;
		currentState.RightProcessingItems = group.Right;

		currentState.ProcessedItems ??= [];
		currentState.ProcessedItems.AddRange(processedItems.SelectMany(x => x));

		currentState.CurrentStage++;

		return TroubleshootResult.Success;
	}

	private (PackageList Left, PackageList Right) SplitGroup(PackageList list)
	{
		var list1 = new PackageList();
		var list2 = new PackageList();

		foreach (var item in list.OrderByDescending(x => x.Count))
		{
			if (list2.TotalCount >= list1.TotalCount)
			{
				list1.Add(item);
			}
			else
			{
				list2.Add(item);
			}
		}

		return (list1, list2);
	}

	private PackageList GetItemGroups(IEnumerable<GenericLocalPackageIdentity> items)
	{
		var groups = new PackageList();
		var remainingItems = items.ToList(); // Work with a mutable list

		while (remainingItems.Count > 0)
		{
			var item = remainingItems[0];

			var group = new PackageGroup();

			GetPairedItems(remainingItems, group, item);

			groups.Add(group);

			remainingItems.RemoveAll(group.Contains);
		}

		return groups;
	}

	private void GetPairedItems(List<GenericLocalPackageIdentity> items, PackageGroup group, GenericLocalPackageIdentity current)
	{
		foreach (var item in items)
		{
			if (group.Contains(item))
			{
				continue;
			}

			if (item == current || (currentState!.Mods && !currentState.ItemIsMissing && AreItemsPaired(item, current)))
			{
				group.Add(item);

				GetPairedItems(items, group, item);
			}
		}
	}

	private bool AreItemsPaired(IPackageIdentity packageA, IPackageIdentity packageB)
	{
		return packageA?.GetWorkshopInfo()?.Requirements.Any(x => x.Id == packageB?.Id) == true
			|| packageB?.GetWorkshopInfo()?.Requirements.Any(x => x.Id == packageA?.Id) == true;
	}

	private void Save()
	{
		try
		{
			if (currentState is null)
			{
				_saveHandler.Delete(FILE_NAME);
			}
			else
			{
				_saveHandler.Save(currentState, FILE_NAME);
			}
		}
		catch (Exception ex)
		{
			_logger.Exception(ex);
		}
	}

	private void CitiesManager_MonitorTick(bool isAvailable, bool isRunning)
	{
		if (currentState == null)
		{
			return;
		}

		if (currentState.Stage.HasFlag(ActionStage.WaitingForGameLaunch) && isRunning)
		{
			GoToNextStage();
		}
		else if (currentState.Stage.HasFlag(ActionStage.WaitingForGameClose) && !isRunning && isAvailable)
		{
			GoToNextStage();
		}
	}

	public class TroubleshootState : ITroubleshootSettings
	{
		public string? PlaysetName { get; set; }
		public string? OriginalPlaysetId { get; set; }
		public string? TroubleshootingPlaysetId { get; set; }
		public List<GenericLocalPackageIdentity>? UnprocessedItems { get; set; }
		public PackageList? LeftProcessingItems { get; set; }
		public PackageList? RightProcessingItems { get; set; }
		public List<GenericLocalPackageIdentity>? ProcessedItems { get; set; }
		public ActionStage Stage { get; set; }
		public int CurrentStage { get; set; }
		public bool ItemIsCausingIssues { get; set; }
		public bool ItemIsMissing { get; set; }
		public bool NewItemCausingIssues { get; set; }
		public bool Mods { get; set; }
		public bool LastResult { get; set; }
	}

	public class PackageList : List<PackageGroup>
	{
		public int TotalCount => this.Sum(x => x.Count);

		public IEnumerable<ILocalPackageIdentity> AsLocalPackageIdentity()
		{
			return this.SelectMany(x => x);
		}

		public IEnumerable<IPackageIdentity> AsPackageIdentity()
		{
			return this.SelectMany(x => x);
		}

		public static implicit operator List<GenericLocalPackageIdentity>(PackageList packageList)
		{
			return packageList?.SelectMany(x => x)?.ToList() ?? [];
		}
	}

	public class PackageGroup : List<GenericLocalPackageIdentity> { }

	[Flags]
	public enum ActionStage
	{
		None = 0,

		Primary = 1,
		Secondary = 2,

		ApplyingSettings = 4,
		WaitingForGameLaunch = 8,
		WaitingForGameClose = 16,
		WaitingForConfirmation = 32,
	}
}
