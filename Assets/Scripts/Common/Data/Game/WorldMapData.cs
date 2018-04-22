using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WorldMapData : BaseData
{
	[AssetReference(AssetType = AssetReferenceAttribute.AssetReferenceType.Texture)]
	public string Image { get; set; }
	
	[DataReference(typeof(CityNodeData))]
	public int[] CityNodes { get; set; }
}

