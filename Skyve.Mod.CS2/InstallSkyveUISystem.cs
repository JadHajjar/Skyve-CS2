using Colossal.UI.Binding;

using Game.UI;

using Skyve.App.CS2.Installer;

using System;
using System.IO;
using System.Threading.Tasks;

namespace Skyve.Mod.CS2
{
	internal partial class InstallSkyveUISystem : UISystemBase
	{
		private ValueBinding<bool> isInstalledBinding;
		private ValueBinding<bool> isUpToDateBinding;
		private bool installing;

		protected override void OnCreate()
		{
			base.OnCreate();

			SkyveMod.CheckIfInstalled(out var isInstalled, out var isUpToDate);

			SkyveMod.Log.Info($"isInstalled: {isInstalled}, isUpToDate: {isUpToDate}");

			AddBinding(isInstalledBinding = new ValueBinding<bool>("SkyveMod", "IsUpToDate", isUpToDate));
			AddBinding(isUpToDateBinding = new ValueBinding<bool>("SkyveMod", "IsInstalled", isInstalled));
			AddBinding(new TriggerBinding("SkyveMod", "InstallSkyve", Install));
		}

		private void Install()
		{
			installing = true;

			SkyveMod.InstallApp();

			Task.Run(async () => { await Task.Delay(30_000); installing = false; });
		}

		protected override void OnUpdate()
		{
			if (installing)
			{
				SkyveMod.CheckIfInstalled(out var isInstalled, out var isUpToDate);

				isInstalledBinding.Update(isInstalled);
				isUpToDateBinding.Update(isUpToDate);
			}

			base.OnUpdate();
		}
	}
}
