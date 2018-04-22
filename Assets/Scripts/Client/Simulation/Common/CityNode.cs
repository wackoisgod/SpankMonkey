using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CityNode
{
	private CityNodeData _cityData;

	public CityNodeData CityData
	{
		get
		{
			return _cityData;
		}
		set
		{
			_numReinforcements = value.MaxReinforcementsAvailable;
			_cityData = value;
		}
	}

	private int _numReinforcements;

	// gross
	//public CityNode(CityNodeData inData)
	//{
	//	_numReinforcements = inData.MaxReinforcementsAvailable;
	//	CityData = inData;
	//}

	public void IncrementNumReinforcements()
	{
		if (_numReinforcements < CityData.MaxReinforcementsAvailable)
		{
			_numReinforcements++;
		}
	}

	public void DecrementNumReinforcements()
	{
		if (_numReinforcements > 1)
		{
			_numReinforcements--;
		}
	}



}
