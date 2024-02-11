using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public bool InstallApp { set => _mod.InstallApp(); }

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
