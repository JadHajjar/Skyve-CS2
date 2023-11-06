using Game;

namespace Skyve.Mod.CS2;

public class DuringGameSimulationSystem : GameSystemBase
{
	protected override void OnCreate()
	{
		Logger.LogDebugInfo("DuringGameSimulationSystem.OnCreate");
		base.OnCreate();
	}
	protected override void OnUpdate()
	{
		Logger.LogDebugInfo("DuringGameSimulationSystem.OnUpdate");
	}
	protected override void OnStageChange(Stage stage)
	{
		base.OnStageChange(stage);
	}
	protected override void OnCreateForCompiler()
	{
		base.OnCreateForCompiler();
	}
	public DuringGameSimulationSystem() { }
}
