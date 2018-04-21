using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomDataDrawer(typeof(int))]
public class IntDrawer : DataDrawer
{
	public override void Draw(object input, string name, string tooltip, PropertyInfo property, Action<object> setValueCallback)
	{
		int value = (int)input;
		value = EditorGUILayout.IntField(new GUIContent(name, tooltip), value);
		setValueCallback(value);
	}
}

