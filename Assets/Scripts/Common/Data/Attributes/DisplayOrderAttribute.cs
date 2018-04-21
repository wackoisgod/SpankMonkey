using System;

public class DisplayOrderAttribute : Attribute
{
	public uint Order;

	public DisplayOrderAttribute(uint order)
	{
		Order = order;
	}
}
