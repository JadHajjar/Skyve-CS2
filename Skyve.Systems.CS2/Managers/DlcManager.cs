using Extensions;

using Skyve.Domain;
using Skyve.Domain.CS2.Utilities;
using Skyve.Domain.Systems;
using Skyve.Systems.CS2.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyve.Systems.CS2.Managers;
internal class DlcManager : IDlcManager
{
	private readonly DlcConfig _config;

	public IEnumerable<IDlcInfo> Dlcs => [];

	public event Action? DlcsLoaded;

	public DlcManager(SaveHandler saveHandler)
	{
		_config = saveHandler.Load<DlcConfig>();
	}

	public bool IsAvailable(uint dlcId)
	{
		return false;
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

	public Task UpdateDLCs()
	{
		return Task.CompletedTask;		
	}
}
