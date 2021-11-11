
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;

namespace MoNA
{
    [InitializeOnLoad]
	public class SceneSwitchLeftButton
	{
		static SceneSwitchLeftButton()
		{
			ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
		}

		static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

            GUI.skin.button.fontSize = 13;
			if(GUILayout.Button(new GUIContent("▶️ Playground", "Build and open playground")))
			{
                Helpers.UpsertExportsDirectory();
                BuildPipeline.BuildAssetBundles(Constants.PlaygroundDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
                Helpers.OpenDirectory(Constants.ExportsDirectory);
                Application.OpenURL("https://www.mona.gallery/playground");
			}
		}
	}
}
