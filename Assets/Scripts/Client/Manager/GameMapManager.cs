using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// Stub this out in case more data is needed
public class UnityMapHandle
{
	public WorldMapData MapData { get; set; }
	public Texture2D MapTexture { get; set; }
	public Sprite MapSprite { get; private set; }
	public GameObject Obj { get; private set; }

	public void Setup()
	{
		Obj = new GameObject("MapRoot");
		Obj.transform.localScale = new Vector3(5, 5, 1);
		var renderer = Obj.AddComponent<SpriteRenderer>();
		MapSprite = Sprite.Create(MapTexture, new Rect(0, 0, MapTexture.width, MapTexture.height), new Vector2(0f, 0f));
		renderer.sprite = MapSprite;
	}
}

public class GameMapManager : BaseManager
{
	public static GameMapManager Instance { get; private set; }
	private List<CityNode> _cities = new List<CityNode>();
	public List<CityNode> Cities => _cities;

	private UnityMapHandle _mapHandle;

	public UnityMapHandle GetMapHandle()
	{
		return _mapHandle;
	}

	public static int CurrentMapWidth => Instance._mapHandle.MapTexture.width;
	public static int CurrentMapHeight => Instance._mapHandle.MapTexture.height;
	
	public override void Init()
	{
		if (Instance == null)
			Instance = this;
	}

	public override void Begin()
	{
		base.Begin();
		GameManager.Instance.OnApplicationStateChanged += OnApplicationStateChange;

		List<WorldMapData> maps = new List<WorldMapData>(DataStore.GetDataOfType<WorldMapData>());
		Debug.Assert(maps.Count > 0);
		_mapHandle = new UnityMapHandle();
		_mapHandle.MapData = maps[0];

		AssetStore.LoadAndGetAsset(maps[0].Image, LoadMap);
		OverworldGameSimulation.InitInstance();
	}

	void LoadMap(bool succeeded, object result)
	{
		if (succeeded)
		{
			_mapHandle.MapTexture = result as Texture2D;
			_mapHandle.Setup();

			foreach (int cityRef in _mapHandle.MapData.CityNodes)
			{
				CityNodeData city = DataStore.GetData<CityNodeData>(cityRef);
				CityNode node = new CityNode();
				node.CityData = city;
				OverworldGameSimulation.Instance.AddSimulationObject(node);
			}
		}
	}
	

	public void OnApplicationStateChange(GameManager.ApplicationState prevState, GameManager.ApplicationState newState)
	{
		if (prevState == newState)
		{
			return;
		}
	}
}
