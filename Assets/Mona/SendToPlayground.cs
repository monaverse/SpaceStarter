#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;

namespace Mona
{
    [InitializeOnLoad]
    public class SendToPlayground
    {
        static SendToPlayground()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            GUI.skin.button.fontSize = 13;
            if (GUILayout.Button(new GUIContent("▶️ Playground", "Build and open playground")))
            {
                Helpers.UpsertExportsDirectory();
                BuildPipeline.BuildAssetBundles(Constants.PlaygroundDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
                Helpers.OpenDirectory(Constants.ExportsDirectory);
                Application.OpenURL(Constants.PlaygroundURL);
            }
        }
    }
}
#endif