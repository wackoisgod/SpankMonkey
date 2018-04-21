using System;
using UnityEngine;

public class DataFieldAssignWindow : DataListWindow
{
	private string _fieldName;

	public static void AssignValue(DataListCallback callback, string fieldName, Type filterType)
	{
		DataFieldAssignWindow assignWindow = GetWindow<DataFieldAssignWindow>();
		assignWindow.Callback = (data, path) =>
		{
			callback(data, path);
			assignWindow.Close();
		};
		assignWindow.Show();
		assignWindow._fieldName = fieldName;
		assignWindow.Expanded = new bool[DataUtils.GetDataTypes().Length];
		assignWindow.FilterType = filterType;
	}

	protected override void OnGUI()
	{
		if (Callback == null)
		{
			Close();
			return;
		}
		GUILayout.Label("Assign Value to: " + _fieldName);
		base.OnGUI();
	}
}
