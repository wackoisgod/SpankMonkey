﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum OverworldMessageType
{
	Click,
	Hold,
	EndTurnButton
}

public struct EventMessage
{
	public OverworldMessageType MessageType { get; set; }
	public Vector2 ClickPosition { get; set; }
	public Vector2 DragDirection { get; set; }
}

public class OverworldGameSimulation
{
	public static OverworldGameSimulation Instance { get; private set; }
	private OverworldState _state = new OverworldState();
	public OverworldState GameState => _state;

	//private Queue<EventMessage> _eventQueue = new Queue<EventMessage>();
	//public Queue<EventMessage> EventQueue => _eventQueue;

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

	private WorldMapData _currentMap;
	private bool _assignedMap = false;

	public WorldMapData CurrentMap
	{
		get
		{
			return _currentMap;
		}
		set
		{
			_currentMap = value;
			if (value == null)
			{
				_assignedMap = false;
			}
			else
			{
				_assignedMap = true;
			}
		}
	}

	public bool HasSuspendedState()
	{
		return (_assignedMap && _simObjects.Count > 0);
	}

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

	public void DumpMap()
	{
		CurrentMap = null;
		_simObjects.Clear();
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

	public void EndTurn()
	{
		// $AK This just stubs out some of the important functionality here
		Tick();
	}

	public List<IOverworldSimulationObject> SimObjectsUnderPosition(Vector2 position)
	{
		List<IOverworldSimulationObject> simObjects = _simObjects.FindAll((x) => { return Vector2.Distance(x.GetPosition(), position) < 6; });

		// Bad hack, need this to return a list for the sake of returning a list
		List<IOverworldSimulationObject> cityTest = simObjects.FindAll((x) => { return x.GetObjectType() == OverworldSimObjectType.City; });

		if (cityTest.Count > 0)
		{
			return cityTest;
		}
		return simObjects;
	}

	public void ProcessEvent(EventMessage message)
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
