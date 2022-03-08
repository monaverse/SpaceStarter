#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace Mona
{
    public static class Constants
    {
        public static readonly string ExportsDirectory = "Exports";
        public static readonly string PlaygroundDirectory = "Exports/PlaygroundFiles";
        public static readonly string MintingFile = "Exports/MintablePackage.unitypackage";
        public static readonly string SpacePath = "Assets/Scenes/Space.unity";
        public static readonly string ArtifactsPath = "Assets/Scenes/Artifacts.unity";
        public static readonly string PortalsPath = "Assets/Scenes/Portals.unity";
        public static readonly string PlaygroundURL = "https://www.mona.gallery/playground";
    }

    public class Helpers
    {
        public static void UpsertExportsDirectory()
        {
            if (!Directory.Exists(Constants.ExportsDirectory))
            {
                Directory.CreateDirectory(Constants.ExportsDirectory);
            }
            if (!Directory.Exists(Constants.PlaygroundDirectory))
            {
                Directory.CreateDirectory(Constants.PlaygroundDirectory);
            }
        }

        public static void OpenDirectory(string directory)
        {
            List<string> _list = new List<string>(Application.dataPath.Split('/'));
            _list.RemoveAt(_list.Count - 1);

            string _directoryPath = string.Join("/", _list.ToArray()) + "/" + directory;
            Application.OpenURL("file://" + _directoryPath);
        }
    }
}
#endif