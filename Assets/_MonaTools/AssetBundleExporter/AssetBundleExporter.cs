#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Mona
{
    public class AssetBundleExporter : EditorWindow
    {
        public static readonly string AssetBundleDirectory = "Exports/PrefabAssetBundles/";
        GameObject prefab;
        bool showWarning = true;

        [MenuItem("Mona Tools/Asset Bundle Exporter")]
        public static void showWindow()
        {
            var window = EditorWindow.GetWindow(typeof(AssetBundleExporter), false, "Asset Bundle Exporter");
            window.minSize = new Vector2(500,400);
        }

        void OnGUI()
        {
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/_MonaTools/AssetBundleExporter/AssetBundleBanner.png", typeof(Texture));
            if (banner)
            {
                GUI.DrawTexture(new Rect(0, 0, 500, 66), banner, ScaleMode.ScaleToFit);
            }
            GUILayout.Space(66);

            // Prefab selection
            prefab = (GameObject)EditorGUILayout.ObjectField("Prefab:", prefab, typeof(GameObject), false);
        
            if (prefab != null) 
            {
                showWarning = false;
                Texture2D thumbnail = AssetPreview.GetAssetPreview(prefab);
                if (thumbnail != null)
                {
                    GUILayout.Label(thumbnail);
                }
            }

            if (GUILayout.Button("Export Asset Bundle for WebGL"))
            {
                if (prefab == null)
                {
                    return;
                }
                // Set the target platform for the asset bundle to WebGL
                BuildTarget target = BuildTarget.WebGL;

                if (!Directory.Exists(AssetBundleDirectory))
                {
                    Directory.CreateDirectory(AssetBundleDirectory);
                }

                // Create a new asset bundle build
                AssetBundleBuild build = new AssetBundleBuild();
                build.assetBundleName = $"{prefab.name}.assetbundle";
                build.assetNames = new string[] { AssetDatabase.GetAssetPath(prefab) };

                // Build the asset bundle
                AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(AssetBundleDirectory, new AssetBundleBuild[] { build }, BuildAssetBundleOptions.None, target);

                // Display a message indicating that the asset bundle was created successfully
                Debug.Log($"Asset bundle for {prefab.name} created successfully at {AssetBundleDirectory}");
                Helpers.OpenDirectory(AssetBundleDirectory);
            }

            if (showWarning)
            {
                EditorGUILayout.HelpBox("Select an prefab to be exported first.", MessageType.Warning);
            }

            // Display a list of the dependencies for the selected prefab
            if (prefab != null)
            {
                string[] dependencies = AssetDatabase.GetDependencies(AssetDatabase.GetAssetPath(prefab));
                EditorGUILayout.LabelField("Dependencies:");
                EditorGUILayout.SelectableLabel(string.Join("\n", dependencies), EditorStyles.textArea, GUILayout.Height(128f));
            }
        }
    }
}
#endif