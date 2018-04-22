using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class OverworldGameSimulation
{
	public static OverworldGameSimulation Instance { get; private set; }
	private OverworldState _state = new OverworldState();
	public OverworldState GameState => _state;

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
	private bool _suspended = false;
	public bool IsSuspended => _suspended;

	public void AddSimulationObject(IOverworldSimulationObject inObj)
	{
		inObj.SetId(_simObjects.Count);
		_simObjects.Add(inObj);
	}

	public void ChangeToMoveState()
	{
		// Do something else here?
		Resume();
	}

	public void Suspend()
	{
		_suspended = true;
		if (_simObjects.Count > 0)
		{
			foreach (var simObj in _simObjects)
			{
				simObj.Suspend(_state);
			}
		}
	}

	public void Resume()
	{
		_suspended = false;
		if (_simObjects.Count > 0)
		{
			foreach (var simObj in _simObjects)
			{
				simObj.Resume(_state);
			}
		}
	}

	void EndTurn()
	{
		
	}
	
	// This tick is called when the overworld map updates
	// IE when the "end turn" button is hit
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