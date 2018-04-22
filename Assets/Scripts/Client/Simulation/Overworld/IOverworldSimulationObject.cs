using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IOverworldSimulationObject
{
	void SetId(int Id);
	int GetId();
	void Tick();
}
