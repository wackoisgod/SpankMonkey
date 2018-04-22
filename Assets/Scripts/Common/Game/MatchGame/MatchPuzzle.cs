using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using DG.Tweening;
using System.Collections;

public struct MatchPoint
{
	public int x, y;
	public MatchPoint(int px, int py)
	{
		x = px;
		y = py;
	}
	public override string ToString()
	{
		return "(" + x + ", " + y + ")";
	}
}

public class Cell
{
	public Enumerations.OrbColor CellType { get; set; }
	public bool IsSuperOrb { get; set; }
	public bool IsEmpty { get { return CellType == Enumerations.OrbColor.empty; } }
	public void SetRandomOrb(int total)
	{
		CellType = (Enumerations.OrbColor)UnityEngine.Random.Range(1, total);
	}
}

public class MatchPuzzle : MonoBehaviour
{
	static public MatchPuzzle instance;

	private int maxWidth = 6;
	private int maxHeight = 5;
	private float orbScaling;
	public int orbSpacingX = 125;
	public int orbSpacingY = -10;


	private Cell[,] _cells;

	private List<MatchGem> _gems = new List<MatchGem>();

	public AnimatedMatchGem[] animationGems;

	public int animationCounter = 0;

	public SpriteAtlas MatchAtlas;

	public bool IsMatching { get; private set; }

	private bool isDragging;

	public bool HasMatched { get; private set; }

	public GameObject MatchGemTemplate;
	public GameObject Grid;

	public static GameObject CurrentGem;

	void Awake() { instance = this; }
	void OnDestroy() { instance = null; }

	public void Start()
	{
		_cells = new Cell[maxWidth, maxHeight];
		Init();
	}

	public void InitMatchGrid()
	{
		for (int x = 0; x < maxWidth; x++)
		{
			for (var y = 0; y < maxHeight; y++)
			{
				_cells[x, y] = new Cell();
				_cells[x, y].SetRandomOrb(7);
			}
		}
	}

	public void CreateOrbGrid()
	{
		for (int x = 0; x < maxWidth; x++)
		{
			for (var y = 0; y < maxHeight; y++)
			{
				_cells[x, y] = new Cell();
			}
		}
	}

	public MatchGem FindOrb(MatchPoint point)
	{
		foreach (MatchGem orb in _gems)
		{
			if (orb.Loc.Equals(point)) return orb;
		}
		return null;
	}

	public void DoSwapOrb(MatchGem a, MatchGem b)
	{
		MatchPoint p1 = a.Loc;
		MatchPoint p2 = b.Loc;

		Cell cell = _cells[p1.x, p1.y];
		_cells[p1.x, p1.y] = _cells[p2.x, p2.y];
		_cells[p2.x, p2.y] = cell;

		a.Loc = p2;
		b.Loc = p1;
	}

	public void DoAnimatedSwap(GameObject newGem, GameObject oldGem)
	{
		var posB = oldGem.transform.localPosition;

		oldGem.gameObject.transform.localPosition = newGem.gameObject.transform.localPosition;

		animationGems[animationCounter].Init(posB, newGem);
		animationGems[animationCounter].Move();

		if (++animationCounter >= animationGems.Length)
			animationCounter = 0;
	}

	private void DoEmptyDown(bool doMatchCheck = true)
	{
		for (var x = 0; x < maxWidth; x++)
		{
			for (var y = 0; y < maxHeight; y++)
			{
				var thiscell = _cells[x, y];
				if (!thiscell.IsEmpty) continue;
				int y1;
				for (y1 = y; y1 > 0; y1--)
				{
					DoSwapOrb(FindOrb(new MatchPoint(x, y1)), FindOrb(new MatchPoint(x, y1 - 1)));
				}
			}
		}
		for (var x = 0; x < maxWidth; x++)
		{
			int y;
			for (y = maxHeight - 1; y >= 0; y--)
			{
				var thiscell = _cells[x, y];
				if (thiscell.IsEmpty) break;
			}
			if (y < 0) continue;
			var y1 = y;
			for (y = 0; y <= y1; y++)
			{
				MatchGem gem = FindOrb(new MatchPoint(x, y));
				gem.transform.localPosition = new Vector3(x * orbSpacingX, (y - (y1 + 1)) * orbSpacingY, 0f);
				gem.RefreshOrb();
			}
		}

		foreach (MatchGem gem in _gems)
		{
			Vector3 pos = new Vector3(gem.Loc.x * orbSpacingX, gem.Loc.y * orbSpacingY);
			float dist = Vector3.Distance(gem.transform.localPosition, pos) * 0.01f;
			dist = 1f;
			gem.transform.DOLocalMove(pos, 0.5f * dist).SetEase(Ease.OutQuart);
		}

		if (doMatchCheck)
			StartCoroutine(CheckMatchOrb(0.8f));
		else
			EndResolveRound();
	}

	public IEnumerator FillEmpty(float delayTime, bool doMatchCheck = true)
	{
		yield return new WaitForSeconds(delayTime);
		DoEmptyDown(doMatchCheck);
	}

	void CheckMatch(Dictionary<MatchPoint, Enumerations.OrbColor> stack)
	{
		List<MatchGem> destroyList = new List<MatchGem>();
		foreach (KeyValuePair<MatchPoint, Enumerations.OrbColor> item in stack)
		{
			destroyList.Add(FindOrb(item.Key));
		}

		foreach (MatchGem item in destroyList)
		{
			int type = (int)item.CellType;

			item.CellType = Enumerations.OrbColor.empty;
			item.GetComponent<SpriteRenderer>().enabled = false;
			//item.GetComponentInChildren<ParticleSystem>().Play();
			//item.StartEffect();
		}

		StartCoroutine(FillEmpty(0.8f));
	}

	IEnumerator CheckMatchOrb(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		Dictionary<MatchPoint, Enumerations.OrbColor> stack = FindMatch(_cells);
		if (stack.Count > 0)
		{
			CheckMatch(stack);
		}
		else
		{
			EndResolveRound();
		}
	}

	private void EndResolveRound()
	{
		IsMatching = false;
		HasMatched = false;
	}

	public void ResolveTurn()
	{
		if (IsMatching)
			return;

		//timeSlider.value = 0;

		var wtfColor = MatchPuzzle.CurrentGem.GetComponent<SpriteRenderer>().color;
		wtfColor.a = 1.0f;
		MatchPuzzle.CurrentGem.GetComponent<SpriteRenderer>().color = wtfColor;

		MatchPuzzle.CurrentGem = null;

		MatchCursor.Clear();

		StartMatchCheck();

		StartEndTurnCheck();
	}

	private void StartMatchCheck()
	{
		IsMatching = true;
		isDragging = false;
		HasMatched = true;

		StartCoroutine(CheckMatchOrb(0.8f));
	}

	private void StartEndTurnCheck()
	{
		StartCoroutine(CheckIfEndTurn());
	}

	IEnumerator CheckIfEndTurn()
	{
		while (IsMatching)
		{
			yield return 0;
		}
	}

	public bool CanDrag()
	{
		if (IsMatching || isDragging || HasMatched)
			return false;

		return true;
	}

	private Dictionary<MatchPoint, Enumerations.OrbColor> FindMatch(Cell[,] cells)
	{
		Dictionary<MatchPoint, Enumerations.OrbColor> stack = new Dictionary<MatchPoint, Enumerations.OrbColor>();
		for (var x = 0; x < maxWidth; x++)
		{
			for (var y = 0; y < maxHeight; y++)
			{
				var thiscell = cells[x, y];
				if (thiscell.IsEmpty) continue;
				int matchCount = 0;
				int y2 = Mathf.Min(maxHeight - 1, y + 2);
				int y1;
				int superOrbModifier = 0;
				for (y1 = y + 1; y1 <= y2; y1++)
				{
					if (cells[x, y1].IsEmpty || thiscell.CellType != cells[x, y1].CellType) break;
					if (cells[x, y1].IsSuperOrb) superOrbModifier += 1;
					matchCount++;
				}
				if (matchCount >= 2)
				{
					y1 = Mathf.Min(maxHeight - 1, y1 - 1);
					for (var y3 = y; y3 <= y1; y3++)
					{
						if (!stack.ContainsKey(new MatchPoint(x, y3)))
							stack.Add(new MatchPoint(x, y3), cells[x, y3].CellType);
					}

					int totalCount = matchCount + superOrbModifier + 1;
					//BattleController.Instance.OnMatchMade(cells[x, y1].CellType, totalCount);
				}
			}
		}
		for (var y = 0; y < maxHeight; y++)
		{
			for (var x = 0; x < maxWidth; x++)
			{
				var thiscell = cells[x, y];
				if (thiscell.IsEmpty) continue;
				int matchCount = 0;
				int x2 = Mathf.Min(maxWidth - 1, x + 3);
				int x1;
				for (x1 = x + 1; x1 <= x2; x1++)
				{
					if (cells[x1, y].IsEmpty || thiscell.CellType != cells[x1, y].CellType) break;
					matchCount++;
				}
				if (matchCount >= 2)
				{
					x1 = Mathf.Min(maxWidth - 1, x1 - 1);
					for (var x3 = x; x3 <= x1; x3++)
					{
						if (!stack.ContainsKey(new MatchPoint(x3, y)))
							stack.Add(new MatchPoint(x3, y), cells[x3, y].CellType);
					}

					//BattleController.Instance.OnMatchMade(cells[x1, y].CellType, matchCount + 1);
				}
			}
		}

		return stack;
	}

	public void Init()
	{
		if (MatchGemTemplate != null)
		{
			if (Grid != null)
			{
				for (var i = Grid.transform.childCount - 1; i >= 0; i--)
				{
					var objectA = Grid.transform.GetChild(i);
					GameObject.Destroy(objectA.gameObject);
				}
			}
			_gems.Clear();

			CreateOrbGrid();
			while (true)
			{
				InitMatchGrid();
				Dictionary<MatchPoint, Enumerations.OrbColor> stack = FindMatch(_cells);
				if (stack.Count < 1) break;
			}

			SetupOrbScaling();

			DisplayOrbs();
			InitAnimationOrbs();
		}


		StartTurn();
	}

	private void SetupOrbScaling()
	{
		float targetaspect = 3.0f / 4.0f;
		float windowaspect = (float)Screen.width / (float)Screen.height;
		float scaleheight = windowaspect / targetaspect;

		if (scaleheight > 1.0f)
		{
			orbScaling = 0.2f;
			orbSpacingX = 140;
			orbSpacingY = -138;
		}

	}

	private void InitAnimationOrbs()
	{
		animationGems = new AnimatedMatchGem[7];
		for (int i = 0; i < animationGems.Length; i++)
		{
			var anOrb = GameObjectUtils.AddChild(Grid, MatchGemTemplate).gameObject.AddComponent<AnimatedMatchGem>();
			anOrb.gameObject.layer = LayerMask.NameToLayer("Board");

			var gg = anOrb.GetComponent<SpriteRenderer>();
			gg.gameObject.transform.localScale = new Vector2(135, 135);
			
			animationGems[i] = anOrb;
			animationGems[i].Start();
		}
	}

	private void StartTurn()
	{
	}

	private void DisplayOrbs()
	{
		for (int y = 0; y < maxHeight; ++y)
		{
			for (int x = 0; x < maxWidth; ++x)
			{
				int type = (int)_cells[x, y].CellType;

				GameObject go = GameObjectUtils.AddChild(Grid, MatchGemTemplate);
				go.name = $"{y},{x}";
				var gg = go.GetComponent<SpriteRenderer>();
				gg.gameObject.transform.localScale = new Vector2(135, 135);
				Transform t = go.transform;
				t.parent = Grid.transform;
				t.localPosition = new Vector3(x * orbSpacingX, y * orbSpacingY, 0f);

				var gem = go.GetComponent<MatchGem>();
				gem.Cell = _cells[x, y];
				gem.Loc = new MatchPoint(x, y);
				gem.Puzzle = this;
				gem.UpdateSprite();
				_gems.Add(gem);
			}
		}
	}

	void Update()
	{
		//if (isDragging)
		//{
		//	if (timeSlider.value == 1.0f)
		//	{
		//		ResolveTurn();
		//	}
		//	else
		//	{
		//		var delta = Time.time - turnStartTime;
		//		timeSlider.value = delta / timeSecondsDrag;
		//	}
		//}
	}

	public void StartMatching()
	{
		//timeSlider.value = 0;
		//turnStartTime = Time.time;
		isDragging = true;
	}

	public void RefreshBoard()
	{
		IsMatching = true;
		StartCoroutine(FillEmpty(0.8f, false));
	}
}
