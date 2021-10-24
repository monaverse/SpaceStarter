
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace MoNA
{
  public class LoadScenesIntoHierarchy
  {
    [MenuItem("MoNA/Load Space Scenes")]
    static void LoadScenesIntoHierarchyHandler()
    {
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
    }
  }
}
