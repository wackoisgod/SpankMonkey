using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MatchPuzzleData : BaseData
{
	public int MaxOrbCount		{ get; set; }
	public int OrbSpacingX		{ get; set; }
	public int OrbSpacingY		{ get; set; }
	public int MaxWidth			{ get; set; }
	public int MaxHeight		{ get; set; }
	public float OrbScaling		{ get; set; }
	public int TimeSecondsDrag	{ get; set; }

	[AssetReference(AssetType = AssetReferenceAttribute.AssetReferenceType.GameObject)]
	public string OrbTemplate { get; set; }
}

