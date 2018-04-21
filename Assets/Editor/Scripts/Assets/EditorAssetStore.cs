using System;
using UnityEditor;

public class EditorAssetStore : AssetStore
{
	protected override void LoadAssetInternal(string guid, Action onAssetLoadedCallback)
	{
		if (!IsAssetLoaded(guid))
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			object asset = AssetDatabase.LoadMainAssetAtPath(path);
			AddAsset(guid, asset);
		}
		onAssetLoadedCallback();
	}

	public override void LoadBulkAssetInternal(string bundleName, Action onLoadCompleteCallback)
	{
		onLoadCompleteCallback();
	}
}