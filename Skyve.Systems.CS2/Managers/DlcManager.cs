using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Skyve.Systems.CS2.Managers;
internal class DlcManager : IDlcManager
{
	private readonly DlcConfig _config;

	public IEnumerable<IDlcInfo> Dlcs => SteamUtil.Dlcs;

	public event Action? DlcsLoaded;

	public DlcManager()
	{
		_config = DlcConfig.Load();

		SteamUtil.DLCsLoaded += DlcsLoaded;
	}

	public bool IsAvailable(uint dlcId)
	{
		return SteamUtil.IsDlcInstalledLocally(dlcId);
	}

	public bool IsIncluded(IDlcInfo dlc)
	{
		return !_config.RemovedDLCs.Contains(dlc.Id);
	}

	public void SetExcludedDlcs(IEnumerable<uint> dlcs)
	{
		_config.RemovedDLCs = dlcs.ToList();

		_config.Save();
	}

	public void SetIncluded(IDlcInfo dlc, bool value)
	{
		if (value)
		{
			_config.RemovedDLCs.Remove(dlc.Id);
		}
		else
		{
			_config.RemovedDLCs.AddIfNotExist(dlc.Id);
		}

		_config.Save();
	}

	public List<uint> GetExcludedDlcs()
	{
		return new(_config.RemovedDLCs);
	}
}
