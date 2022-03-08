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
            Scene _initialScene = EditorSceneManager.GetSceneAt(0);
            EditorSceneManager.CloseScene(_initialScene, true);
            
            List<string> _sceneList = new List<string>()
            {
                Constants.SpacePath,
                Constants.PortalsPath,
                Constants.ArtifactsPath
            };
            
            foreach (string _scene in _sceneList)
            {
                EditorSceneManager.OpenScene(_scene, OpenSceneMode.Additive);
            }

            Scene _spaceScene = EditorSceneManager.GetSceneByName("Space");
            EditorSceneManager.SetActiveScene(_spaceScene);
        }
    }
}

#endif