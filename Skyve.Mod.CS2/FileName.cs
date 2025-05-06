using Colossal.Serialization.Entities;

using Game;
using Game.SceneFlow;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyve.Mod.CS2
{
	public partial class AchievementsSystem : GameSystemBase
	{
		protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
		{
			Colossal.PSI.Common.PlatformManager.instance.achievementsEnabled = true;
		}

		protected override void OnUpdate()
		{ }
	}
}
