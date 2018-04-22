using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MatchGem : MonoBehaviour
{
	public Enumerations.OrbColor CellType
	{
		get { return Cell.CellType; }
		set
		{
			if (Cell.CellType == value)
			{
				return;
			}

			Cell.CellType = value;
			UpdateSprite();
		}
	}

	public Cell Cell { get; set; }
	public MatchPoint Loc { get; set; }
	public MatchPuzzle Puzzle { get; set; }

	private string[] _sprites = new string[] { "0", "dark", "fire", "light", "water", "earth", "attack" };

	public void UpdateSprite()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (CellType == Enumerations.OrbColor.empty)
		{
			sprite.enabled = false;
			return;
		}

		string spriteName = _sprites[(int)CellType];
		sprite.sprite = Puzzle.MatchAtlas.GetSprite(spriteName);
		sprite.enabled = true;
	}

	public void RefreshOrb()
	{
		Cell.SetRandomOrb(7);
		UpdateSprite();
	}
}

