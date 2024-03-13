using Skyve.Domain.Systems;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Service.CS2.Systems;
internal class UpdateSystem
{
	private readonly IWorkshopService _workshopService;

	public UpdateSystem(IWorkshopService workshopService)
    {
		_workshopService = workshopService;
	}

    internal async Task RunUpdate()
	{
		if (_workshopService.IsReady)
		{
			await _workshopService.RunSync();
		}
		else
		{
			await _workshopService.Login();
		}
	}
}
