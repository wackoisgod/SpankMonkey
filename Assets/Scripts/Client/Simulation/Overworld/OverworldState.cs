using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum BattleResult
{
	Win,
	Loss,
	Tie
}

public class OverworldState
{
	private List<OverworldGameUnit> _overworldUnits = new List<OverworldGameUnit>();
	public List<OverworldGameUnit> OverworldUnits => _overworldUnits;

	private List<int> _currentUnitStack = null;

	private string _currentMap;

	public void RegisterUnit(OverworldGameUnit unit)
	{
		unit.Id = _overworldUnits.Count;
		_overworldUnits.Add(unit);
	}

	public void PushToUnitStack(OverworldGameUnit unit)
	{
		_currentUnitStack.Add(unit.Id);
	}

	public void PushToUnitStack(int unitID)
	{
		if (_overworldUnits.Count > 0)
		{
			_overworldUnits[unitID].Prev = _overworldUnits[unitID - 1];
			_overworldUnits[unitID - 1].Next = _overworldUnits[unitID];
		}
		
		_currentUnitStack.Add(unitID);
	}

	public List<int> PopUnitStack()
	{
		List<int> result = new List<int>(_currentUnitStack);
		_currentUnitStack.RemoveAll((x) => { return true; }); // remove all
		return result;
	}

	private List<OverworldGameUnit> ToGameUnits(List<int> inUnits)
	{
		List<OverworldGameUnit> result = new List<OverworldGameUnit>();

		foreach (int unit in inUnits)
		{
			result.Add(OverworldUnits[unit]);
		}

		return result;
	}

	public BattleResult ResolveBattle(CityNode playerCity, List<int> enemyUnitHandles)
	{
		return ResolveBattle(playerCity.UnitsHere, enemyUnitHandles);
	}

	public BattleResult ResolveBattle(List<int> playerUnitHandles, CityNode enemyCity)
	{
		return ResolveBattle(playerUnitHandles, enemyCity.UnitsHere);
	}

	public BattleResult ResolveBattle(List<int> playerUnitHandles, List<int> enemyUnitHandles)
	{
		List<OverworldGameUnit> playerUnits = ToGameUnits(playerUnitHandles);
		List<OverworldGameUnit> enemyUnits = ToGameUnits(enemyUnitHandles);

		return ResolveBattle(playerUnits, enemyUnits);
	}

	public BattleResult ResolveBattle(List<OverworldGameUnit> playerUnits, List<OverworldGameUnit> enemyUnits)
	{
		return BattleResult.Win;
	}

}
