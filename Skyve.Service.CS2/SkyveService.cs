using Microsoft.Extensions.DependencyInjection;

using Skyve.Domain;
using Skyve.Domain.Systems;
using Skyve.Service.CS2.Systems;
using Skyve.Systems;
using Skyve.Systems.CS2;
using Skyve.Systems.CS2.Systems;

using System.Net.Mime;
using System.ServiceProcess;
using System.Security.Principal;
using System.Management;
using System.IO;
using Extensions;
using System.Linq;

namespace Skyve.Service.CS2;

public class SkyveService : ServiceBase
{
	public SkyveService()
	{
		ServiceName = "SkyveService";
	}

	protected override void OnStart(string[] args)
	{
		var folders = Directory.GetDirectories("C:\\Users")
			.Select(x => Path.Combine(x, "AppData", "LocalLow", "Colossal Order", "Cities Skylines II"))
			.AllWhere(Directory.Exists);

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
