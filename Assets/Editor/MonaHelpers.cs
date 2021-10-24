
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public class MonaHelpers
{
  private static class Constants
  {
    public static readonly string ExportsDirectory = "Exports";
    public static readonly string PlaygroundDirectory = "Exports/PlaygroundFiles";
    public static readonly string MintingFile = "Exports/MintablePackage.unitypackage";
    public static readonly string SpacePath = "Assets/Scenes/Space.unity";
    public static readonly string ArtifactsPath = "Assets/Scenes/Artifacts.unity";
    public static readonly string PortalsPath = "Assets/Scenes/Portals.unity";
  }

  static void UpsertExportsDirectory()
  {
    if (!Directory.Exists(Constants.ExportsDirectory))
    {
      Directory.CreateDirectory(Constants.ExportsDirectory);
      Directory.CreateDirectory(Constants.PlaygroundDirectory);
    }
  }

  static void OpenDirectory(string directory)
  {
    List<string> list = new List<string>(Application.dataPath.Split('/'));
    list.RemoveAt(list.Count - 1);
    string directoryPath = string.Join("/", list.ToArray()) + "/" + directory;
    Application.OpenURL("file://" + directoryPath);
  }

  [MenuItem("MoNA/Build Playground Files")]
  static void BuildPlaygroundAssetBundles()
  {
    UpsertExportsDirectory();
    BuildPipeline.BuildAssetBundles(Constants.PlaygroundDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
    OpenDirectory(Constants.ExportsDirectory);
  }

  [MenuItem("MoNA/Build Mintable Files")]
  static void BuildMintableFiles()
  {
    UpsertExportsDirectory();

    List<string> sceneList = new List<string>()
    {
        Constants.SpacePath,
        Constants.PortalsPath,
        Constants.ArtifactsPath
    };

    List<string> exportsList = new List<string>();


    foreach (string scene in sceneList)
    {
      exportsList.Add(scene);
      string[] sceneDependencies = AssetDatabase.GetDependencies(scene, true);
      foreach (string dependency in sceneDependencies)
      {
        exportsList.Add(dependency);
      }
    }

    AssetDatabase.ExportPackage(exportsList.ToArray(), Constants.MintingFile, ExportPackageOptions.Recurse);

    OpenDirectory(Constants.ExportsDirectory);
  }
}