
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace MoNA
{
    public class BuildPlaygroundFiles
    {
        [MenuItem("MoNA/Build Playground Files")]
        public static void BuildPlaygroundFilesHandler()
        {
            Helpers.UpsertExportsDirectory();
            BuildPipeline.BuildAssetBundles(Constants.PlaygroundDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
            Helpers.OpenDirectory(Constants.ExportsDirectory);
        }
    }
}
