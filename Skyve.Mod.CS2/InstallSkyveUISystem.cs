using Colossal.UI.Binding;

using Game.UI;

using System.IO;

namespace Skyve.Mod.CS2
{
	internal partial class InstallSkyveUISystem : UISystemBase
	{
		private ValueBinding<bool> isInstalledBinding;
		private bool installComplete;

		protected override void OnCreate()
		{
			base.OnCreate();

			AddBinding(isInstalledBinding = new ValueBinding<bool>("SkyveMod", "IsInstalled", File.Exists(@"C:\Program Files\Skyve CS-II\Skyve.exe")));
			AddBinding(new TriggerBinding("SkyveMod", "InstallSkyve", Install));
		}

		private void Install()
		{
			if (SkyveMod.InstallApp())
			{
				installComplete = true;
			}
		}

		protected override void OnUpdate()
		{
			if (installComplete && File.Exists(@"C:\Program Files\Skyve CS-II\Skyve.exe"))
			{
				installComplete = false;

				isInstalledBinding.Update(true);
			}

			base.OnUpdate();
		}
	}
}
