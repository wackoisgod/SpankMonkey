using System;
using System.Reflection;
using UnityEditor;

public class EnumDrawer : DataDrawer
{
	public override void Draw(object input, string name, string tooltip, PropertyInfo property, Action<object> setValueCallback)
	{
		Enum enumValue = (Enum)input;
		enumValue = EditorGUILayout.EnumPopup(name, enumValue);
		setValueCallback(enumValue);
	}
}

