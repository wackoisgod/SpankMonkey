using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WorldMapData : BaseData
{
	[AssetReference(AssetType = AssetReferenceAttribute.AssetReferenceType.Texture)]
	public string Image { get; set; }

	public Vector2[] MapPositions { get; set; }
	
	public int[] CityNodes { get; set; }
}

