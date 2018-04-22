using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;

[CustomDataDrawer(typeof(DataReferenceAttribute))]
public class DataReferenceDrawer : DataDrawer
{
	public override void Draw(object input, string name, string tooltip, PropertyInfo property, Action<object> setValueCallback)
	{
		DataEditorCache.DataInfo[] data = DataEditorCache.Instance.Data;

		int intValue = (int)input;
		string dataName = "Unassigned";
		for (int i = 0; i < data.Length; i++)
		{
			if (data[i].Id == intValue)
			{
				dataName = data[i].Name;
			}
		}
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(name);
		EditorGUILayout.LabelField(dataName);

		var dataRefAttribute = property.GetCustomAttribute<DataReferenceAttribute>();

		if (GUILayout.Button("Change", GUILayout.ExpandWidth(false)))
		{
			DataFieldAssignWindow.AssignValue((assignedData, path) => { setValueCallback(assignedData.Id); }, property.Name, (dataRefAttribute == null ? null :dataRefAttribute.ReferencedType));
		}

		EditorGUILayout.EndHorizontal();
		setValueCallback(intValue);
	}
}
