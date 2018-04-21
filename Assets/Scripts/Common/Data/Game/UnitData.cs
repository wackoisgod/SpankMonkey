using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : BaseData
{
	[AssetReference(AssetType = AssetReferenceAttribute.AssetReferenceType.Texture)]
	public string Icon { get; set; }
}
