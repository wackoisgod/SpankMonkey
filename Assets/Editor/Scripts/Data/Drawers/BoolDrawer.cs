using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomDataDrawer(typeof(bool))]
public class BoolDrawer : DataDrawer
{
	public override void Draw(object input, string name, string tooltip, PropertyInfo property, Action<object> setValueCallback)
	{
		bool value = (bool)input;
		value = EditorGUILayout.Toggle(new GUIContent(name, tooltip), (bool)input);
		setValueCallback(value);
	}
}
