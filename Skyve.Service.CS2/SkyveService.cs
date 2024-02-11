using Microsoft.Extensions.DependencyInjection;

using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Service.CS2.Systems;
using Skyve.Systems;
using Skyve.Systems.CS2;
using Skyve.Systems.CS2.Systems;

using System.ServiceProcess;

namespace Skyve.Service.CS2;

public class SkyveService : ServiceBase
{
	public SkyveService()
	{
		ServiceName = "SkyveService";
	}

	protected override void OnStart(string[] args)
	{
		var services = new ServiceCollection();

		services.AddSkyveSystems();
		services.AddCs2SkyveSystems();
		
		services.AddSingleton<ILogger, AppLoggerSystem>();
		services.AddSingleton<ServiceSystem>();
		services.AddTransient<UpdateSystem>();

		ServiceCenter.Provider = services.BuildServiceProvider();

		ServiceCenter.Provider.GetService<ServiceSystem>()?.Start();
	}

	protected override void OnStop()
	{
	}
}
