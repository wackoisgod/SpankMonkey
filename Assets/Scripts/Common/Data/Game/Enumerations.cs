using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Enumerations
{
	public enum OrbColor
	{
		empty,
		dark,
		fire,
		light,
		water,
		wood,
		attack,
		length
	}

	public enum Player
	{
		PlayerOne,
		PlayerNone
	}

	public enum UnitType
	{
		Light,
		Heavy,
		VeryHeavy
	}

	public enum DragTargets
	{
		Board,
		Card,
		Orb,
		Battle,
		Enemy,
		None
	}
}
