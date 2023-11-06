using Colossal.Logging;

using Game;
using Game.Modding;

using System;

namespace Skyve.Mod.CS2;
public class SkyveMod : IMod
{
	public void OnLoad()
	{
		Logger.LogInfo("Loading..."); //Logger will be a static class we define later
	}

	public void OnCreateWorld(UpdateSystem updateSystem)
	{
		Logger.LogInfo("Handling create world");
		updateSystem.UpdateAt<DuringGameSimulationSystem>(Game.SystemUpdatePhase.GameSimulation);
	}

	public void OnDispose()
	{
		Logger.LogInfo("Disposing..");
	}
}