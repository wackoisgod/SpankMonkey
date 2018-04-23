using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class CityNodeUnityHandle
{
	public CityNodeData CityData { get; set; }
	public GameObject Obj { get; set; }


	// $AK IMPORTANT NOTE:
	//	* The positions are calcualted as such ((pixel_position / 100f) * 5.0f) 
	//  * This is because sprite.create is 100 pixels per meter when its created, then the scale is set to 5

	public void Setup()
	{
		Vector2 position = CityData.MapPosition;
		position /= 100f;
		position *= 5f;

		Obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Obj.name = CityData.CityName;
		Obj.tag = "city";
		Obj.GetComponent<MeshRenderer>().material.color = Color.red;
		Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
		Obj.transform.position = new Vector3(position.x, position.y, 0.0f);
	}

	public void Destroy()
	{
		CityData = null;
		GameObject.Destroy(Obj);
	}
}

public class CityNode : IOverworldSimulationObject
{
	private CityNodeData _cityData;
	private CityNodeUnityHandle _handle = null;

	public CityNodeData CityData
	{
		get
		{
			return _cityData;
		}
		set
		{
			_numReinforcements = value.MaxReinforcementsAvailable;
			_cityData = value;

			if (_handle != null)
			{
				_handle.Destroy();
			}

			_handle = new CityNodeUnityHandle();
			_handle.CityData = value;
			_handle.Setup();
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

	public Vector2 GetPosition()
	{
		return _cityData.MapPosition;
	}

	private int _numReinforcements;

	private List<int> _gameUnits;
	public List<int> UnitsHere => _gameUnits;

	public void IncrementNumReinforcements()
	{
		if (_numReinforcements < CityData.MaxReinforcementsAvailable)
		{
			_numReinforcements++;
		}
	}

	public void DecrementNumReinforcements()
	{
		if (_numReinforcements > 1)
		{
			_numReinforcements--;
		}
	}



}
