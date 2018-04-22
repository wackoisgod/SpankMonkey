using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class OverworldGameUnitUnityHandle
{
	public UnityEngine.GameObject Obj;
}

public class OverworldGameUnit
{
	public UnitData Data { get; set; }

	public bool IsInStack
	{
		get
		{
			return (Prev != null) || (Next != null);
		}
		
	}

	public int Id;
	public OverworldGameUnit Prev;
	public OverworldGameUnit Next;
}

