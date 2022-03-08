#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;

namespace Mona
{
    public class BuildMintableFiles
    {
        [MenuItem("Mona/Build Mintable Files")]
        static void BuildMintableFilesHandler()
        {
            Helpers.UpsertExportsDirectory();
            BuildPlaygroundFiles.BuildPlaygroundFilesHandler();

            List<string> _sceneList = new List<string>()
            {
                Constants.SpacePath,
                Constants.PortalsPath,
                Constants.ArtifactsPath
            };

            List<string> _exportsList = new List<string>();

            foreach (string _scene in _sceneList)
            {
                _exportsList.Add(_scene);
                string[] _sceneDependencies = AssetDatabase.GetDependencies(_scene, true);
                foreach (string _dependency in _sceneDependencies)
                {
                    _exportsList.Add(_dependency);
                }
            }

            AssetDatabase.ExportPackage(_exportsList.ToArray(), Constants.MintingFile, ExportPackageOptions.Recurse);
            Helpers.OpenDirectory(Constants.ExportsDirectory);
        }
    }
}
#endif