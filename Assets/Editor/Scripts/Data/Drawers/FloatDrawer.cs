using System;
using System.Reflection;
using UnityEditor;

[CustomDataDrawer(typeof(float))]
public class FloatDrawer : DataDrawer
{
	public override void Draw(object input, string name, string tooltip, PropertyInfo property, Action<object> setValueCallback)
	{
		float value = (float)input;
		value = EditorGUILayout.FloatField(name, value);
		setValueCallback(value);
	}
}

