#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Mona
{
    public class LoadScenesIntoHierarchy
    {
        [MenuItem("Mona/Load Space Scenes")]
        static void LoadScenesIntoHierarchyHandler()
        {
            Scene initialScene = EditorSceneManager.GetSceneAt(0);
            EditorSceneManager.CloseScene(initialScene, true);

            List<string> sceneList = new List<string>()
            {
                Constants.SpacePath,
                Constants.PortalsPath,
                Constants.ArtifactsPath
            };

            foreach (string scene in sceneList)
            {
                EditorSceneManager.OpenScene(scene, OpenSceneMode.Additive);
            }

            Scene spaceScene = EditorSceneManager.GetSceneByName("Space");
            EditorSceneManager.SetActiveScene(spaceScene);
        }
    }
}
#endif