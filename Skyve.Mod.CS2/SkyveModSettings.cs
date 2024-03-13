using Colossal.IO.AssetDatabase;

using Game.Modding;
using Game.Settings;

namespace Skyve.Mod.CS2
{
	[FileLocation("Skyve")]
	[SettingsUIGroupOrder(GAMEPLAY_GROUP)]
	[SettingsUIShowGroupName(GAMEPLAY_GROUP)]
	public class SkyveModSettings : ModSetting
	{
		private readonly SkyveMod _mod;

		public const string GAMEPLAY_GROUP = nameof(GAMEPLAY_GROUP);

		[SettingsUISection(GAMEPLAY_GROUP)]
		public bool InstallApp { set => SkyveMod.InstallApp(); }

		public SkyveModSettings(SkyveMod mod) : base(mod)
		{
			_mod = mod;
		}

		public override void SetDefaults()
		{
		}

		public override void Apply()
		{
			base.Apply();
		}
	}
}
