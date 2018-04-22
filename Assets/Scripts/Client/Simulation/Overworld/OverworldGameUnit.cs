using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class OverworldGameUnitUnityHandle
{
	public GameObject Obj;
	public UnitMovementComponent MovementComponent { get; private set; }

	public void SetPosition(Vector2 position)
	{
		Vector2 conversion = (position / 100f);
		conversion *= 5f;

		MovementComponent.Destination = new Vector3(conversion.x, conversion.y);
	}

	public void Setup()
	{
		Obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Obj.transform.localScale = new Vector3(0.03f, 0.03f);
		Obj.GetComponent<Material>().color = Color.blue;
		MovementComponent = Obj.AddComponent<UnitMovementComponent>();
	}
}

public class OverworldGameUnit : IOverworldSimulationObject
{
	public UnitData Data { get; set; }

	public bool IsInStack
	{
		get
		{
			return (Prev != null) || (Next != null);
		}
		
	}

	private int _simId;

	public void SetId(int id)
	{
		_simId = id;
	}

	public int GetId()
	{
		return _simId;
	}

	public void Tick()
	{
		
	}

	public void Suspend(OverworldState state)
	{

	}

	public void Resume(OverworldState state)
	{

	}

	private bool _needsMove;
	public Vector2 Destination { get; set; }
	public Vector2 CurrentPosition { get; private set; }

	public int Id;
	public CityNode OwningCity { get; set; }
	public OverworldGameUnit Prev;
	public OverworldGameUnit Next;
}

