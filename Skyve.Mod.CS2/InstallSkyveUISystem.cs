using Colossal.UI.Binding;

using Game.UI;

using Microsoft.Win32;

using System;
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

			AddBinding(isInstalledBinding = new ValueBinding<bool>("SkyveMod", "IsInstalled", IsInstalled()));
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
			if (installComplete && IsInstalled())
			{
				installComplete = false;

				isInstalledBinding.Update(true);
			}

			base.OnUpdate();
		}

		private bool IsInstalled()
		{
			return File.Exists(Path.Combine(GetCurrentInstallationPath() ?? "C:\\Program Files", "Skyve.exe"));
		}

		public static string GetCurrentInstallationPath()
		{
			try
			{
				return File.ReadAllText(Path.Combine(FolderSettings.SettingsFolder, "InstallPath"));
			}
			catch
			{
				return null;
			}
		}
	}
}
