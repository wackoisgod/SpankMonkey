using System;
using System.Reflection;
using UnityEditor;

[CustomDataDrawer(typeof(long))]
public class LongDrawer : DataDrawer
{
	public override void Draw(object input, string name, string tooltip, PropertyInfo property, Action<object> setValueCallback)
	{
		long value = (long)input;
		value = EditorGUILayout.LongField(name, value);
		setValueCallback(value);
	}
}

