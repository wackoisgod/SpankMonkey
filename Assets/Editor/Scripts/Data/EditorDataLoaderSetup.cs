using UnityEditor;

[InitializeOnLoad]
public class EditorDataLoaderSetup
{
	static EditorDataLoaderSetup()
	{
		AssetStore.SetInstance(new EditorAssetStore());
	}
}
