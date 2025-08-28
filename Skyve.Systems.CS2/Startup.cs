using Microsoft.Extensions.DependencyInjection;

using Skyve.Compatibility.Domain.Interfaces;
using Skyve.Domain.Systems;
using Skyve.Systems.Compatibility.Domain;
using Skyve.Systems.CS2.Managers;
using Skyve.Systems.CS2.Services;
using Skyve.Systems.CS2.Systems;
using Skyve.Systems.CS2.Utilities;
using Skyve.Systems.CS2.Utilities.IO;

namespace Skyve.Systems.CS2;
public static class Startup
{
	public static IServiceCollection AddCs2SkyveSystems(this IServiceCollection services)
	{
		services.AddSingleton<ICitiesManager, CitiesManager>();
		services.AddSingleton<ILocationService, LocationService>();
		services.AddSingleton<IModLogicManager, ModLogicManager>();
		services.AddSingleton<IPackageManager, PackageManager>();
		services.AddSingleton<IPlaysetManager, PlaysetManager>();
		services.AddSingleton<ISubscriptionsManager, SubscriptionsManager>();
		services.AddSingleton<IUpdateManager, UpdateManager>();
		services.AddSingleton<ICompatibilityUtil, CompatibilityUtil>();
		services.AddSingleton<ISettings, SettingsService>();
		services.AddSingleton<IAssetUtil, AssetsUtil>();
		services.AddSingleton<IContentManager, ContentManager>();
		services.AddSingleton<IModDllManager, ModDllManager>();
		services.AddSingleton<IModUtil, ModsUtil>();
		services.AddSingleton<IWorkshopService, WorkshopService>();
		services.AddSingleton<IUserService, UserService>();
		services.AddSingleton<IDlcManager, DlcManager>();
		services.AddSingleton<ITagsService, TagsService>();
		services.AddSingleton<ITroubleshootSystem, TroubleshootSystem>();
		services.AddSingleton<INotificationsService, NotificationsService>();
		services.AddSingleton<ICompatibilityActionsHelper, CompatibilityActionsUtil>();
		services.AddSingleton<SkyveApiUtil>();
		services.AddSingleton<ISkyveDataManager, SkyveDataManager>();
		services.AddSingleton<IBackupService, BackupService>();
		services.AddSingleton<PdxLogUtil>();

		services.AddTransient<IVersionUpdateService, VersionUpdateService>();
		services.AddTransient<ILogUtil, LogUtil>();
		services.AddTransient<ICentralManager, CentralManager>();
		services.AddTransient<NamedPipelineUtil>();
		services.AddTransient<AssemblyUtil>();
		services.AddTransient<MacAssemblyUtil>();
		services.AddTransient<GoFileApiUtil>();
		services.AddTransient<IBackupSystem, BackupSystem>();

		return services;
	}
}
