#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Mona
{
    public class BuildPlaygroundFiles
    {
        [MenuItem("Mona/Build Playground Files")]
        public static void BuildPlaygroundFilesHandler()
        {
            QualityAssurance.SpaceErrors = QualityAssurance.GetSpaceErrors();

            if (QualityAssurance.SpaceErrors == null || QualityAssurance.SpaceErrors.Count == 0)
            {
                Helpers.UpsertExportsDirectory();
                BuildPipeline.BuildAssetBundles(Constants.PlaygroundDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
                Helpers.OpenDirectory(Constants.ExportsDirectory);
                Application.OpenURL(Constants.PlaygroundURL);
            }
            else
            {
                QualityAssuranceEditor.Init();
            }
        }
    }
}
#endif