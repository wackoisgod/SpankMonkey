using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum OverworldSimObjectType
{
	Unit,
	City
}

public interface IOverworldSimulationObject
{
	void SetId(int Id);
	int GetId();
	void Tick();
	void Suspend(OverworldState state);
	void Resume(OverworldState state);
	UnityEngine.Vector2 GetPosition();
	OverworldSimObjectType GetObjectType();
}
