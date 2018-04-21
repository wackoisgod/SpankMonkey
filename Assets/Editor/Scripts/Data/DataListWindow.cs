using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataListWindow : EditorWindow
{
	public delegate void DataListCallback(BaseData data, string path);

	protected DataListCallback Callback;

	protected bool[] Expanded;

	protected Type FilterType = null;

	private Vector2 _scrollPosition;

	private string _searchString = "";

	protected virtual void OnGUI()
	{
		int i = 0;
		_searchString = EditorGUILayout.TextField("Search:", _searchString);
		_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

		// allow for filter in data types to make assignment less confusing
		if (FilterType == null)
		{
			DrawSubTypes(typeof(BaseData), ref i);
		}
		else
		{
			DrawSubTypes(FilterType, ref i, false);
		}

		EditorGUILayout.EndScrollView();
	}

	protected void DrawSubTypes(Type baseType, ref int i, bool filterByBase = true)
	{
		foreach (Type t in DataUtils.GetDataTypes())
		{
			if (filterByBase)
			{
				if (t.BaseType != baseType)
				{
					continue;
				}

			}
			else
			{
				if (t != baseType)
				{
					continue;
				}

			}

			GUILayout.BeginHorizontal();
			string typeName = t.ToString();

			IOrderedEnumerable<DataEditorCache.DataInfo> dataInstances = (from info in DataEditorCache.Instance.Data
																		  where (info.Type == typeName) && info.Name.Contains(_searchString)
																		  select info).OrderBy(info => info.Name);

			Color textColor = Color.black;
			if (!dataInstances.Any())
			{
				textColor = Color.gray;
			}

			GUIStyle style = EditorStyles.foldout;
			style.normal.textColor = textColor;
			Expanded[i] = EditorGUILayout.Foldout(Expanded[i], t.Name, style);
			if (GUILayout.Button("Create"))
			{
				DataCreationWindow.Create(t, Callback);
			}

			GUI.color = Color.white;
			GUILayout.EndHorizontal();

			if (!Expanded[i++])
			{
				continue;
			}

			EditorGUI.indentLevel++;

			DrawSubTypes(t, ref i);

			foreach (DataEditorCache.DataInfo dataInfo in dataInstances)
			{
				if (GUILayout.Button(dataInfo.Name))
				{
					Callback(Serializer.Deserialize(t, Application.dataPath + dataInfo.FilePath) as BaseData,
					 Application.dataPath + dataInfo.FilePath);
				}
			}

			EditorGUI.indentLevel--;
		}
	}
}