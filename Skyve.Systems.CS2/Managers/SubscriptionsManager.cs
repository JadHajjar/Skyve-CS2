using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class SubscriptionsManager : ISubscriptionsManager
{
	private readonly WorkshopService _workshopService;
	private readonly ISettings _settings;

	public SubscriptionsManager(IWorkshopService workshopService, ISettings settings)
	{
		_workshopService = (WorkshopService)workshopService;
		_settings = settings;
	}

	public List<ulong> SubscribingTo { get; private set; } = new();
	public List<ulong> UnsubscribingFrom { get; private set; } = new();
	public List<ulong> PendingSubscribingTo { get; private set; } = new();
	public List<ulong> PendingUnsubscribingFrom { get; private set; } = new();
	public bool SubscriptionsPending { get; }
	public bool Redownload { get; set; }

	public void CancelPendingItems()
	{
		throw new NotImplementedException();
	}

	public bool IsSubscribing(IPackage package)
	{
		return false;
	}

	public void Start()
	{
	}

	public async Task<bool> Subscribe(IEnumerable<IPackageIdentity> ids)
	{
		return (await _workshopService.Context!.Mods.SubscribeBulk(ids.Select(x => new KeyValuePair<int, string>((int)x.Id, null)), null, !_settings.UserSettings.DisableNewModsByDefault)).Success;
	}

	public async Task<bool> UnSubscribe(IEnumerable<IPackageIdentity> ids)
	{
		return (await _workshopService.Context!.Mods.SubscribeBulk(ids.Select(x => new KeyValuePair<int, string>((int)x.Id, null)), null, !_settings.UserSettings.DisableNewModsByDefault)).Success;
	}
}
