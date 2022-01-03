#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Mona
{
    public class BuildMintableFiles
    {
        // Start is called before the first frame update
        [MenuItem("Mona/Build Mintable Files")]
        static void BuildMintableFilesHandler()
        {
            Helpers.UpsertExportsDirectory();

            BuildPlaygroundFiles.BuildPlaygroundFilesHandler();

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

            Helpers.OpenDirectory(Constants.ExportsDirectory);
        }
    }
}
#endif