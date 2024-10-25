using Extensions;

using Microsoft.Extensions.DependencyInjection;

using Skyve.Domain.Systems;
using Skyve.Service.CS2.Systems;
using Skyve.Systems;
using Skyve.Systems.CS2;
using Skyve.Systems.CS2.Systems;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;

namespace Skyve.Service.CS2;

public class SkyveService : ServiceBase
{
	public SkyveService()
	{
		ServiceName = "SkyveService";
	}

	public List<ServiceProvider> Providers { get; } = [];

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

			Providers.Add(provider);

			new Thread(new ThreadStart(provider.GetService<ServiceSystem>()!.Run)) { IsBackground = true }.Start();
			new Thread(new ThreadStart(provider.GetService<ServiceSystem>()!.RunBackup)) { IsBackground = true }.Start();
		}

		foreach (var provider in Providers)
		{
			provider.GetService<ILogger>()?.Info("Service Started");
		}
	}

	protected override void OnStop()
	{
		foreach (var provider in Providers)
		{
			provider.GetService<ILogger>()?.Warning("Service Stopped");
		}
	}

#if DEBUG
	internal void TestRun()
	{
		OnStart([]);
	}
#endif
}
