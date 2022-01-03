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
    public class BuildPlaygroundFiles
    {
        [MenuItem("Mona/Build Playground Files")]
        public static void BuildPlaygroundFilesHandler()
        {
            Helpers.UpsertExportsDirectory();
            BuildPipeline.BuildAssetBundles(Constants.PlaygroundDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
            Helpers.OpenDirectory(Constants.ExportsDirectory);
        }
    }
}

#endif