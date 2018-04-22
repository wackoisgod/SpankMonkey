using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class OverworldGameSimulation
{
	public enum State
	{
		Move,
		BattleCalculation
	}

	private List<IOverworldSimulationObject> _simObjects = new List<IOverworldSimulationObject>();

	public void AddSimulationObject(IOverworldSimulationObject inObj)
	{
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

	}

}