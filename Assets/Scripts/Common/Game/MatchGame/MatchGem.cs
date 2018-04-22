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
	public string SpriteName { get; private set; }

	private string[] _sprites = new string[] { "0", "dark", "fire", "light", "water", "earth", "attack" };

	void DoSwapMotion(GameObject newGem, GameObject oldGem)
	{
		Puzzle.DoAnimatedSwap(newGem, oldGem);
	}

	public void UpdateSprite()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (CellType == Enumerations.OrbColor.empty)
		{
			sprite.enabled = false;
			return;
		}

		SpriteName = _sprites[(int)CellType];
		sprite.sprite = Puzzle.MatchAtlas.GetSprite(SpriteName);
		sprite.enabled = true;
	}

	public void RefreshOrb()
	{
		Cell.SetRandomOrb(7);
		UpdateSprite();
	}

	public void OnPress(bool isDown)
	{
		if (isDown)
		{
			if (!Puzzle.CanDrag()) return;

			MatchPuzzle.CurrentGem = gameObject;

			var sprite = GetComponent<SpriteRenderer>();
			if (sprite != null)
			{
				var c = sprite.color;
				c.a = 0.5f;
				sprite.color = c;

				MatchCursor.Set(Puzzle.MatchAtlas, SpriteName);
			}

			Puzzle.StartMatching();

		}
		else
		{
			if (MatchPuzzle.CurrentGem != null)
			{
				Puzzle.ResolveTurn();
			}
		}
	}

	public virtual void OnDragOver(GameObject obj)
	{
		var gemObject = obj.GetComponent<MatchGem>();
		if (obj != MatchPuzzle.CurrentGem)
		{
			return;
		}

		DoSwapMotion(gameObject, obj);

		// If we are being swapped, we can reveal the color if it is dark
		//IsDark = false;
		Puzzle.DoSwapOrb(this, gemObject);
	}
}

