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
        public static readonly string PlaygroundURL = "https://monaverse.com/playground";
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
            List<string> list = new List<string>(Application.dataPath.Split('/'));
            list.RemoveAt(list.Count - 1);

            string directoryPath = string.Join("/", list.ToArray()) + "/" + directory;
            Application.OpenURL("file://" + directoryPath);
        }
    }
}
#endif