using Colossal.UI.Binding;

using Game.UI;

using System;
using System.IO;

namespace Skyve.Mod.CS2
{
	internal partial class InstallSkyveUISystem : UISystemBase
	{
		private ValueBinding<bool> isInstalledBinding;

		protected override void OnCreate()
		{
			base.OnCreate();

			AddBinding(isInstalledBinding = new ValueBinding<bool>("SkyveMod", "IsInstalled", File.Exists(@"C:\Program Files\Skyve CS-II\Skyve.exe")));
			AddBinding(new TriggerBinding("SkyveMod", "InstallSkyve", Install));
		}

		private void Install()
		{
			SkyveMod.InstallApp();

			//isInstalledBinding.Update(false);
		}
	}
}
