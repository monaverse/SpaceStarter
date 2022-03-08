#if UNITY_EDITOR
using UnityEditor;

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