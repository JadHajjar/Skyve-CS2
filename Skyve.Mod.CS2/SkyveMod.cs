using Colossal.Logging;

using Game;
using Game.City;
using Game.Economy;
using Game.Modding;
using Game.Prefabs;
using Game.Simulation;

using HarmonyLib;

using System;
using System.Runtime.CompilerServices;

using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

using UnityEngine.Scripting;

namespace Skyve.Mod.CS2;
public class SkyveMod : IMod
{
	public static ILog Log = LogManager.GetLogger($"{nameof(Skyve)}.{nameof(SkyveMod)}", false);

	public void OnCreateWorld(UpdateSystem updateSystem)
	{
		try
		{
			var harmony = new Harmony(nameof(SkyveMod));

			harmony.PatchAll(typeof(SkyveMod).Assembly);
		}
		catch (Exception ex) { Log.Fatal(ex); }
	}

	public void OnDispose()
	{

	}

	public void OnLoad()
	{

	}
}