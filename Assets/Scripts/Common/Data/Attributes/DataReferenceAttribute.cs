using System;

public class DataReferenceAttribute : DataDrawAttribute
{
	public Type ReferencedType;

	public DataReferenceAttribute(Type referencedType)
	{
		ReferencedType = referencedType;
	}
}