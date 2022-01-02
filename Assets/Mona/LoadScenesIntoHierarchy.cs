#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Mona
{
    public class LoadScenesIntoHierarchy
    {
        [MenuItem("Mona/Load Space Scenes")]
        static void LoadScenesIntoHierarchyHandler()
        {
            var initialScene = EditorSceneManager.GetSceneAt(0);
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
            var spaceScene = EditorSceneManager.GetSceneByName("Space");
            EditorSceneManager.SetActiveScene(spaceScene);
        }
    }
}

#endif