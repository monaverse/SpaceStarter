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
    public class TemplateCheck
    {
        public static readonly string AIRTABLE_PUBLIC_API_KEY = "keywC7DhH4fzXGWcg";
        public static bool UpdateAvaliable = false;
        public static bool ConfigurationIssue = false;
        static TemplateCheck()
        {
            if (!SessionState.GetBool("init_UpdateChecker", false))
            {
                SessionState.SetBool("init_UpdateChecker", true);
                
                TemplateChecker templateChecker = new TemplateChecker();
                
                EditorCoroutineUtility.StartCoroutine(templateChecker.GetUpdate(), templateChecker);

                ConfigurationIssue = !templateChecker.HasCorrectUVersion();
                if (!ConfigurationIssue)
                {
                    ConfigurationIssue = !templateChecker.HasWebGLModule();
                }
            }
        }
    }
    public class TemplateChecker
    {
        private readonly string url = "https://api.airtable.com/v0/appglbIlOT8JLLnur/tblUmrrHSfzY6hbky?fields%5B%5D=version&fields%5B%5D=file&maxRecords=1&sort%5B0%5D%5Bfield%5D=date&sort%5B0%5D%5Bdirection%5D=desc";
        public string Update = "";
        public string FileName = "";

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

        public IEnumerator GetUpdate()
        {
            using (UnityWebRequest webReq = new UnityWebRequest(url))
            {
                webReq.SetRequestHeader("Authorization", $"Bearer {TemplateCheck.AIRTABLE_PUBLIC_API_KEY}");
                webReq.downloadHandler = new DownloadHandlerBuffer();
                yield return webReq.SendWebRequest();
                if (webReq.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log($"Failed to check for update: {webReq.result}");
                }
                else
                {
                    templateVersion = new TemplateVersion();
                    JsonUtility.FromJsonOverwrite(webReq.downloadHandler.text, templateVersion);
                    string newestVersion = templateVersion.records[0].fields.version;
                    if (!(versionCompare(TemplateInfo.Version, newestVersion) < 0))
                    {
                        Update = "None";
                        TemplateCheck.UpdateAvaliable = false;
                    }   
                    else
                    {
                        Update = templateVersion.records[0].fields.file[0].url;
                        FileName = templateVersion.records[0].fields.file[0].filename;
                        TemplateCheck.UpdateAvaliable = true;
                        AssetDatabase.Refresh();
                        Debug.LogWarning($"Newer version of the Mona StarterSpace Template is avaliable ({newestVersion}). Visit the template helper utility to easily download updates.");
                    }
                }
            }
        }
        public bool HasCorrectUVersion()
        {
            if (Application.unityVersion.Contains(TemplateInfo.UnityVer))
            {
                return true;
            }
            return false;
        }
        public bool HasWebGLModule()
        {
            var moduleManager = System.Type.GetType("UnityEditor.Modules.ModuleManager,UnityEditor.dll");
            var isPlatformSupportLoaded = moduleManager.GetMethod("IsPlatformSupportLoaded", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var getTargetStringFromBuildTarget = moduleManager.GetMethod("GetTargetStringFromBuildTarget", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            if ((bool)isPlatformSupportLoaded.Invoke(null, new object[] {(string)getTargetStringFromBuildTarget.Invoke(null, new object[] {BuildTarget.WebGL})}))
            {
                return true;
            }
            return false;
        }
        // Takes 2 input version numbers and outputs -1, 0, 1 depending on which verion number is larger
        // Goes through each numerical part of the version number until it finds one version number which is great than the other.
        private int versionCompare(string v1, string v2)
        {
            int vnum1 = 0, vnum2 = 0;
            for (int i = 0, j = 0; (i < v1.Length || j < v2.Length);)
            {//Loops until entire string compared or greater version number found

                //Gets each version number section at a time for comparison
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

                //Compares current sections of version numbers
                if (vnum1 > vnum2)
                {
                    return 1;
                }
                if (vnum2 > vnum1)
                {
                    return -1;
                }
                //If sections are equal loop
                vnum1 = vnum2 = 0;
                i++;
                j++;
            }
            return 0;
        }
    }
    public class TemplateHelper : EditorWindow
    {
        private TemplateChecker templateChecker;
        [MenuItem("Mona/Template Utility")]
        public static void ShowWindow()
        { 
            var window = EditorWindow.GetWindow(typeof(TemplateHelper), false, "Template Utility");
            window.minSize = new Vector2(498,250);
            window.maxSize = new Vector2(498,250);
        }
        void OnGUI()
        {
            if (templateChecker == null)
            {
                templateChecker = new TemplateChecker();
                EditorCoroutineUtility.StartCoroutine(templateChecker.GetUpdate(), this);
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
                EditorGUILayout.LabelField($"Unity Version: {Application.unityVersion}", EditorStyles.boldLabel);
            }
            else
            {
                EditorGUILayout.LabelField($"Unity version installed is {Application.unityVersion} but {TemplateInfo.UnityVer} is required", EditorStyles.boldLabel);
                if (GUILayout.Button("Info"))
                {
                    Application.OpenURL("https://docs.monaverse.com/get-started#1.-install-unity-2020.3.18f1-free");
                }
            }
            if (templateChecker.Update == "")
            {
                EditorGUILayout.LabelField("Checking for template update", EditorStyles.boldLabel);
            }
            else
            {
                if (templateChecker.Update == "None")
                {
                    EditorGUILayout.LabelField($"Template Version: {TemplateInfo.Version}", EditorStyles.boldLabel);
                }   
                else
                {
                    EditorGUILayout.LabelField("Template Update Avaliable", EditorStyles.boldLabel);
                    if (GUILayout.Button("Update"))
                    {
                        EditorCoroutineUtility.StartCoroutine(DownloadAssetPackage(templateChecker.Update, templateChecker.FileName), this);
                    } 
                }
            }
     
            if(templateChecker.HasWebGLModule())
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
        
        private IEnumerator DownloadAssetPackage(string url, string name)
        {
            Debug.Log($"Downloading {url}");
            using (UnityWebRequest assetReq = new UnityWebRequest(url))
            {
                assetReq.SetRequestHeader("Authorization", $"Bearer {TemplateCheck.AIRTABLE_PUBLIC_API_KEY}");
                assetReq.downloadHandler = new DownloadHandlerFile($"Assets/{name}.unitypackage");
                yield return assetReq.SendWebRequest();
                if (assetReq.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log($"Mona Library Request Failed: {url}{assetReq.result}");
                }
                else
                {
                    AssetDatabase.ImportPackage($"Assets/{name}.unitypackage", true);
                }
            }
        }
    }
}
#endif