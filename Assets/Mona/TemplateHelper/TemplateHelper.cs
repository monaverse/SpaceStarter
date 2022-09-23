#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
using UnityEngine.Networking;

namespace Mona
{
    
    [InitializeOnLoad]
    class TenmplateCheck
    {
        static void OnLoad()
        {
            Debug.Log("Updating");
            EditorApplication.update += Update;
        }

        static void Update ()
        {
            Debug.Log("Updating");
        }
    }
    public class TemplateHelper : EditorWindow
    {
        private readonly string at_PublicAPIKey = "keywC7DhH4fzXGWcg"; // Currently test airtable
        private readonly string url = "https://api.airtable.com/v0/appglbIlOT8JLLnur/tblUmrrHSfzY6hbky?fields%5B%5D=version&fields%5B%5D=file&maxRecords=1&sort%5B0%5D%5Bfield%5D=date&sort%5B0%5D%5Bdirection%5D=desc";
        private string json = "";
        [System.Serializable]
        private class TemplateVersion
        {
            [System.Serializable]
            public class Fields
            {
                public Fields()
                {
                    file = new List<File>();
                }
                public string version;
                public List<File> file;
            }
            [System.Serializable]
            public class Item{

                public Fields fields;
            }
            [System.Serializable]
            public class File
            {
                public string id;
                public string url;
                public string filename;
                public int size;
                public string type;
            }
            public List<Item> records;
        }
        private TemplateVersion templateVersion;
        [MenuItem("Mona/Template Helper")]
        public static void ShowWindow()
        { 
            var window = EditorWindow.GetWindow(typeof(TemplateHelper), false, "Template Helper");
            window.minSize = new Vector2(498,250);
            window.maxSize = new Vector2(498,250);
        }
        void OnGUI()
        {
            if (templateVersion == null)
            {
                templateVersion = new TemplateVersion();
                EditorCoroutineUtility.StartCoroutine(DownloadJson(), this);
            }
            
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Resources/Editor/templatehelper.png", typeof(Texture));
            if (banner)
            {
                GUI.DrawTexture(new Rect(0, 0, 498, 66), banner, ScaleMode.ScaleToFit, false);
            }
            GUILayout.Space(60);
            
            EditorGUILayout.LabelField("");
           
            if (Application.unityVersion.Contains(TemplateInfo.UnityVer))
            {
                EditorGUILayout.LabelField("Unity Version: " + Application.unityVersion, EditorStyles.boldLabel);
            }
            else
            {
                EditorGUILayout.LabelField("Unity version installed is " + Application.unityVersion + " but " + TemplateInfo.UnityVer + " is required", EditorStyles.boldLabel);
                if (GUILayout.Button("Info"))
                {
                    Application.OpenURL("https://docs.monaverse.com/get-started#1.-install-unity-2020.3.18f1-free");
                }
            }
            if (json == "")
            {
                EditorGUILayout.LabelField("Checking for template update", EditorStyles.boldLabel);
            }
            else
            {
                if (!(versionCompare(TemplateInfo.Version, templateVersion.records[0].fields.version) < 0))
                {
                    EditorGUILayout.LabelField("Template Version: " + TemplateInfo.Version, EditorStyles.boldLabel);
                }   
                else
                {
                    EditorGUILayout.LabelField("Template Update Avaliable", EditorStyles.boldLabel);
                    if (GUILayout.Button("Update"))
                    {
                        EditorCoroutineUtility.StartCoroutine(DownloadAssetPackage(templateVersion.records[0].fields.file[0].url, templateVersion.records[0].fields.file[0].filename), this);
                    } 
                }
            }
            
            var moduleManager = System.Type.GetType("UnityEditor.Modules.ModuleManager,UnityEditor.dll");
            var isPlatformSupportLoaded = moduleManager.GetMethod("IsPlatformSupportLoaded", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var getTargetStringFromBuildTarget = moduleManager.GetMethod("GetTargetStringFromBuildTarget", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
     
            if((bool)isPlatformSupportLoaded.Invoke(null,new object[] {(string)getTargetStringFromBuildTarget.Invoke(null, new object[] {BuildTarget.WebGL})}))
            {
                EditorGUILayout.LabelField("WebGL Build Module: Installed", EditorStyles.boldLabel);
            }
            else
            {
                EditorGUILayout.LabelField("WebGL Build Module: Missing", EditorStyles.boldLabel);
                if (GUILayout.Button("Info"))
                {
                    Application.OpenURL("https://docs.monaverse.com/create-your-space/troubleshooting");
                }
            }
        }
        private IEnumerator DownloadJson()
        {
            using (UnityWebRequest webReq = new UnityWebRequest(url))
            {
                webReq.SetRequestHeader("Authorization", "Bearer " + at_PublicAPIKey);
                webReq.downloadHandler = new DownloadHandlerBuffer();
                yield return webReq.SendWebRequest();
                if (webReq.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Mona Library API Request Failed: " + webReq.result);
                    json = "";
                }
                else
                {
                    json = webReq.downloadHandler.text;
                    JsonUtility.FromJsonOverwrite(json, templateVersion);
                }
            }
        }
        private int versionCompare(string v1, string v2)
        {
            int vnum1 = 0, vnum2 = 0;
            for (int i = 0, j = 0; (i < v1.Length || j < v2.Length);)
            {
                while (i < v1.Length && v1[i] != '.')
                {
                    vnum1 = vnum1 * 10 + (v1[i] - '0');
                    i++;
                }
                while (j < v2.Length && v2[j] != '.')
                {
                    vnum2 = vnum2 * 10 + (v2[j] - '0');
                    j++;
                }
                if (vnum1 > vnum2)
                {
                    return 1;
                }
                if (vnum2 > vnum1)
                {
                    return -1;
                }
                vnum1 = vnum2 = 0;
                i++;
                j++;
            }
            return 0;
        }
        private IEnumerator DownloadAssetPackage(string url, string name)
        {
            Debug.Log("Downloading " + url);
            using (UnityWebRequest assetReq = new UnityWebRequest(url))
            {
                assetReq.SetRequestHeader("Authorization", "Bearer " + at_PublicAPIKey);
                assetReq.downloadHandler = new DownloadHandlerFile("/Assets/MonaLibrary/" + name + "/.unitypackage");
                yield return assetReq.SendWebRequest();
                if (assetReq.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Mona Library Request Failed: " + url + assetReq.result);
                }
                else
                {
                    AssetDatabase.ImportPackage("/Assets/MonaLibrary/" + name + "/.unitypackage", true);
                }
            }
        }
    }
}
#endif