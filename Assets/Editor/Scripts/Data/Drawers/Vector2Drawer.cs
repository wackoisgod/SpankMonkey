using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomDataDrawer(typeof(Vector2))]
public class Vector2Drawer : DataDrawer
{
	public override void Draw(object input, string name, string tooltip, PropertyInfo property, Action<object> setValueCallback)
	{
		Vector2 inputValue = input != null ? (Vector2)input : new Vector2();
		inputValue = EditorGUILayout.Vector2Field(new GUIContent(name, tooltip), inputValue, null);
		setValueCallback(inputValue);
	}
}
