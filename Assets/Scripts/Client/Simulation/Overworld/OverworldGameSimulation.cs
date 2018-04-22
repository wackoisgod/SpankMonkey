using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class OverworldGameSimulation
{
	public static OverworldGameSimulation Instance { get; private set; }

	public static void InitInstance()
	{
		if (Instance == null)
		{
			Instance = new OverworldGameSimulation();
		}
	}

	public enum State
	{
		Move,
		BattleCalculation
	}

	private List<IOverworldSimulationObject> _simObjects = new List<IOverworldSimulationObject>();

	public void AddSimulationObject(IOverworldSimulationObject inObj)
	{
		inObj.SetId(_simObjects.Count);
		_simObjects.Add(inObj);
	}

	void ChangeToMoveState()
	{

	}

	void EndTurn()
	{

	}
	
	public void Tick()
	{
		if (_simObjects.Count > 0)
		{
			foreach (var obj in _simObjects)
			{
				obj.Tick();
			}
		}
	}

}