using System;
using System.Reflection;
using UnityEditor;

public class DefaultDrawer : DataDrawer
{
	public override void Draw(object input, string name, string tooltip, PropertyInfo property, Action<object> setValueCallback)
	{
		EditorGUILayout.LabelField(name, "No drawer implemented");
		setValueCallback(input);
	}
}

