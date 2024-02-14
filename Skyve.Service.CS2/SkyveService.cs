using Extensions;

using Microsoft.Extensions.DependencyInjection;

using Skyve.Domain.Systems;
using Skyve.Service.CS2.Systems;
using Skyve.Systems;
using Skyve.Systems.CS2;
using Skyve.Systems.CS2.Systems;

using System;
using System.IO;
using System.Linq;
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
		var folders = Directory.GetDirectories("C:\\Users")
			.Select(x => Path.Combine(x, "AppData", "LocalLow", "Colossal Order", "Cities Skylines II"))
			.AllWhere(Directory.Exists);

		foreach (var folder in folders)
		{
			var services = new ServiceCollection();

			services.AddSkyveSystems();
			services.AddCs2SkyveSystems();

			services.AddSingleton(new SaveHandler(Path.Combine(folder, "ModsData")));
			services.AddSingleton<ILogger, ServiceLoggerSystem>();
			services.AddSingleton<IInterfaceService, InterfaceService>();
			services.AddSingleton<ServiceSystem>();
			services.AddTransient<UpdateSystem>();

			var provider = services.BuildServiceProvider();

			provider.GetService<ServiceSystem>()?.Start();
		}
	}

	protected override void OnStop()
	{
	}

#if DEBUG
	internal void TestRun()
	{
		OnStart([]);
	}
#endif
}
