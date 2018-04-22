using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// Stub this out in case more data is needed
public class UnityMapHandle
{
	public GameObject Obj { get; set; }
}

public class GameMapManager : BaseManager
{
	private List<CityNode> _cities = new List<CityNode>();
	public List<CityNode> Cities => _cities;

	private UnityMapHandle _mapHandle;

	public UnityMapHandle GetMapHandle()
	{
		return _mapHandle;
	}
	
	public override void Init()
	{
	}

	public override void Begin()
	{
		base.Begin();
		GameManager.Instance.OnApplicationStateChanged += OnApplicationStateChange;
	}

	public void OnApplicationStateChange(GameManager.ApplicationState prevState, GameManager.ApplicationState newState)
	{

	}
}
