using System;
using System.Reflection;
using UnityEditor;

public class EnumMaskDrawer : DataDrawer
{
	public override void Draw(object input, string name, string tooltip, PropertyInfo property, Action<object> setValueCallback)
	{
		Enum enumValue = (Enum)input;
#pragma warning disable CS0618 // Type or member is obsolete
		enumValue = EditorGUILayout.EnumMaskField(name, enumValue);
#pragma warning restore CS0618 // Type or member is obsolete
		setValueCallback(enumValue);
	}
}

