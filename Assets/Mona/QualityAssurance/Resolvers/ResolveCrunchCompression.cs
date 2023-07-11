#if UNITY_EDITOR
using UnityEditor;

namespace Mona
{
    public class ResolveCrunchCompression
    {
        [MenuItem("Mona/Disable Crunch Compression")]
        public static void ResolveCrunchCompressionHandler()
        {
            string[] guids = AssetDatabase.FindAssets("t:Texture2D", null);
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

                if (importer != null)
                {
                    if (importer.crunchedCompression == true)
                    {
                        importer.crunchedCompression = false;
                    }
                }
            }
            QualityAssurance.SpaceErrors = QualityAssurance.GetSpaceErrors();
        }
    }
}
#endif