using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MatchGameManager : BaseManager
{
	public enum TurnState
	{
		Start,
		Wait,
		Ready,
		Activate,
		Matching,
		End
	}

	public static MatchGameManager Instance { get; private set; }

	public int CurrentPlayerIndex = -1;
	public int TurnCount { get; private set; }

//	private OrbBattle puzzleBattleBoard = null;
//	private OrbPuzzle puzzleOrbBoard = null;


	public override void Init()
	{
		if (Instance == null)
			Instance = this;
	}

	public override void Begin()
	{
		//if (puzzleBoard == null)
		//{
		//	puzzleBoard = GameObject.FindGameObjectWithTag("BoardObject");
		//	if (puzzleBoard == null)
		//	{
		//		Debug.LogError("WE ARE FUCKED");
		//		Debug.Break();
		//	}
		//	else
		//	{
		//		puzzleOrbBoard = puzzleBoard.GetComponentInChildren<OrbPuzzle>();
		//		puzzleBattleBoard = puzzleBoard.GetComponentInChildren<OrbBattle>();

		//		// We do not load the board here :)!
		//		puzzleBoard.SetActive(false);
		//	}
		//}

		// we hook into this event so we can know when a game state changes. 
		GameManager.Instance.OnApplicationStateChanged += OnApplicationStateChange;
	}

	public void OnMatchMade(Enumerations.OrbColor inColor, int count)
	{
		//if (puzzleBattleBoard != null)
		//{
		//	puzzleBattleBoard.UpdateMana(inColor, count);
		//}
	}

	public void OnTurnStart()
	{
		//if (puzzleBattleBoard != null)
		//{
		//	puzzleBattleBoard.OnTurnStart();
		//}
	}

	public void OnTick()
	{
		//if (puzzleBattleBoard != null)
		//{
		//	puzzleBattleBoard.OnTick();
		//}
	}

	public void OnTurnEnd()
	{
		//if (puzzleBattleBoard != null)
		//{
		//	puzzleBattleBoard.OnTurnEnd();
		//}

		TurnCount++;
	}

	public void OnApplicationStateChange(GameManager.ApplicationState toState, GameManager.ApplicationState fromState)
	{
		if (toState == fromState)
		{
			return;
		}
	}

	public void ColorSwap(Enumerations.OrbColor old, Enumerations.OrbColor newColor)
	{
//		puzzleOrbBoard.ColorSwap(old, newColor);
	}

	//public OrbPuzzle PuzzleOrbBoard
	//{
	//	get
	//	{
	//		return puzzleOrbBoard;
	//	}
	//}




}
