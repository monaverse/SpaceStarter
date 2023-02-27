#if UNITY_EDITOR
using UnityEditor;

namespace Mona
{
    public class BuildDevelopmentKit
    {
        [MenuItem("Assets/Export Development Kit")]
        static void Export()
        {
            AssetDatabase.ExportPackage(
                AssetDatabase.GetAllAssetPaths(), 
                $"MonaDevelopmentKit.{TemplateInfo.Version}.unitypackage", 
                ExportPackageOptions.Interactive | 
                ExportPackageOptions.Recurse | 
                ExportPackageOptions.IncludeDependencies | 
                ExportPackageOptions.IncludeLibraryAssets
            );
        }
    }
}
#endif
