using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomDataDrawer(typeof(string))]
public class StringDrawer : DataDrawer
{
	public override void Draw(object input, string name, string tooltip, PropertyInfo property, Action<object> setValueCallback)
	{
		string stringValue = input != null ? (string)input : "";
		stringValue = EditorGUILayout.TextField(new GUIContent(name, tooltip), stringValue);
		setValueCallback(stringValue);
	}
}

