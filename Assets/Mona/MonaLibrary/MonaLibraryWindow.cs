#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
using UnityEngine.Networking;

namespace Mona
{
    public class MonaLibraryWindow : EditorWindow
    {
        static MonaLibraryWindow window = null;
        private readonly string AIRTABLE_PUBLIC_API_KEY = "keywC7DhH4fzXGWcg";
        private readonly string AIRTABLE_BASE_ID = "appglbIlOT8JLLnur";
        private readonly string AIRTABLE_ROOTTABLE_ID = "tblf3EHvQM36tPLJE"; 
        private bool isInitalized = false, isLoading = false;
        private string searchString = "";
        private Vector2 scrollPos = new Vector2(0,0);

        private MonaLibrary monaLibrary;
        private int sectionTab;

        [MenuItem("Mona/Mona Library")]
        public static void ShowWindow()
        {
            SessionState.SetBool("MonaLibraryOpen", true);
            window = EditorWindow.GetWindow(typeof(MonaLibraryWindow), false, "Mona Library") as MonaLibraryWindow;
            window.minSize = new Vector2(600,360);
            window.maxSize = new Vector2(600,1440);
            window.isInitalized = false;
            window.isLoading = false;
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded() 
        {
            if (SessionState.GetBool("MonaLibraryOpen", false))
            {
                ShowWindow();
            }
        }
        void OnDestroy()
        {
            SessionState.SetBool("MonaLibraryOpen", false);
        }
        void OnInspectorUpdate()
        {
            Repaint();
        }

        void OnGUI()
        {
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Resources/Editor/library.png", typeof(Texture));
            if (banner)
            {
                GUI.DrawTexture(new Rect(0, 0, 600, 66), banner, ScaleMode.ScaleToFit);
            }
            GUILayout.Space(60);
            if (GUILayout.Button("Submit to Library"))
            {
                Application.OpenURL("https://docs.monaverse.com/resources/mona-library-submission");
            }
            if (GUILayout.Button("Refresh"))
            {
                isInitalized = false;
                isLoading = false;
            }
            if (!isInitalized)
            {
                if (!isLoading)
                {
                    isLoading = true;
                    EditorCoroutineUtility.StartCoroutine(getLibrary(), this);
                }
                EditorGUILayout.LabelField("Loading Mona Library...", EditorStyles.boldLabel);
                GUIUtility.ExitGUI();
            }
            else
            {
                string[] sections = new string[monaLibrary.sections.records.Count];
                for (int i = 0; i < sections.Length; i++)
                {
                    sections[i] = monaLibrary.sections.records[i].fields.name;
                }
                sectionTab = GUILayout.Toolbar (sectionTab, sections);
                searchString = GUILayout.TextField(searchString, GUI.skin.FindStyle("ToolbarSeachTextField"));
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                try
                {
                    if (monaLibrary.sections.records[sectionTab].fields.type == "asset")
                    {
                        for (int i = 0; i < monaLibrary.assets[sectionTab].records.Count;)
                        {
                            GUILayout.BeginHorizontal();
                            for (int j = 0; j < (int)(this.position.width / 145) & i < monaLibrary.assets[sectionTab].records.Count;)
                            {
                                if (!monaLibrary.assets[sectionTab].records[i].fields.invalid & ( monaLibrary.assets[sectionTab].records[i].fields.name.ToLower().Contains(searchString.ToLower()) | monaLibrary.assets[sectionTab].records[i].fields.artist.ToLower().Contains(searchString.ToLower())))
                                {
                                    GUILayout.BeginVertical(monaLibrary.assets[sectionTab].records[i].fields.name, "window", GUILayout.Height(128), GUILayout.Width(128));
                                    EditorGUILayout.BeginHorizontal();
                                    {
                                        GUILayout.FlexibleSpace();
                                        EditorGUILayout.LabelField(monaLibrary.assets[sectionTab].records[i].fields.artist, EditorStyles.wordWrappedMiniLabel);
                                        GUILayout.FlexibleSpace();
                                    }
                                    EditorGUILayout.EndHorizontal();
                                    if (monaLibrary.icons.ContainsKey(monaLibrary.assets[sectionTab].records[i].fields.icon[0].url))
                                    {
                                        GUILayout.Box(monaLibrary.icons[monaLibrary.assets[sectionTab].records[i].fields.icon[0].url], GUILayout.Width(128), GUILayout.Height(128));
                                    }
                                    else
                                    {
                                        monaLibrary.icons.Add(monaLibrary.assets[sectionTab].records[i].fields.icon[0].url, AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/Editor/error_thumb.png"));
                                        EditorCoroutineUtility.StartCoroutine(DownloadThumbnail(monaLibrary.assets[sectionTab].records[i].fields.icon[0].url), this);
                                    }
                                    if (GUILayout.Button("Download"))
                                    {
                                        EditorCoroutineUtility.StartCoroutine(DownloadAssetPackage(monaLibrary.assets[sectionTab].records[i].fields.file[0].url, monaLibrary.assets[sectionTab].records[i].fields.file[0].filename), this);
                                    }
                                    GUILayout.EndVertical();
                                    j++;
                                }
                                i++;
                            }
                            GUILayout.EndHorizontal();
                        }
                    }
                    else if (monaLibrary.sections.records[sectionTab].fields.type == "tool")
                    {
                        for (int i = 0; i < monaLibrary.tools[sectionTab].records.Count;)
                        {
                            GUILayout.BeginHorizontal();
                            for (int j = 0; j < (int)(this.position.width / 145) & i < monaLibrary.tools[sectionTab].records.Count;)
                            {
                                if (monaLibrary.tools[sectionTab].records[i].fields.name.ToLower().Contains(searchString.ToLower()))
                                {
                                    GUILayout.BeginVertical(monaLibrary.tools[sectionTab].records[i].fields.name, "window", GUILayout.Height(128), GUILayout.Width(128));
                                    if (monaLibrary.icons.ContainsKey(monaLibrary.tools[sectionTab].records[i].fields.icon[0].url))
                                    {
                                        GUILayout.Box(monaLibrary.icons[monaLibrary.tools[sectionTab].records[i].fields.icon[0].url], GUILayout.Width(128), GUILayout.Height(128));
                                    }
                                    else
                                    {
                                        monaLibrary.icons.Add(monaLibrary.tools[sectionTab].records[i].fields.icon[0].url, AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/Editor/error_thumb.png"));
                                        EditorCoroutineUtility.StartCoroutine(DownloadThumbnail(monaLibrary.tools[sectionTab].records[i].fields.icon[0].url), this);
                                    }
                                    if (GUILayout.Button("Download"))
                                    {
                                        EditorCoroutineUtility.StartCoroutine(DownloadAssetPackage(monaLibrary.tools[sectionTab].records[i].fields.file[0].url, monaLibrary.tools[sectionTab].records[i].fields.file[0].filename), this);
                                    }
                                    GUILayout.EndVertical();
                                    j++;
                                }
                                i++;
                            }
                            GUILayout.EndHorizontal();
                        }
                                
                    }
                }
                catch
                {
                    Repaint();
                }
                EditorGUILayout.EndScrollView();
            }
        } 

        private IEnumerator getLibrary()
        {
            string json = "";
            string[] fields;
            
            IEnumerator DownloadJson(string type, string url)
            {
                using (UnityWebRequest webReq = new UnityWebRequest(url))
                {
                    webReq.SetRequestHeader("Authorization", $"Bearer {AIRTABLE_PUBLIC_API_KEY}");
                    webReq.downloadHandler = new DownloadHandlerBuffer();
                    yield return webReq.SendWebRequest();
                    if (webReq.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log($"Mona Library API Request Failed: {url} Error = {webReq.result}");
                        json = "error";
                        yield return new WaitForSecondsRealtime(5);
                    }
                    else
                    {
                        json = FormatJson(type, webReq.downloadHandler.text);
                    }
                }
            }
            
            fields = new string[] {"name","table","type"};
            yield return EditorCoroutineUtility.StartCoroutine(DownloadJson("sections", getJsonURL(AIRTABLE_ROOTTABLE_ID, 100, fields)), this);
            if (json == "error")
            {
                isLoading = false;
                yield break;
            }
            monaLibrary = new MonaLibrary();
            monaLibrary.sections = new MonaLibrary.Sections();
            monaLibrary.sections.records = new List<MonaLibrary.Sections.Item>();
            JsonUtility.FromJsonOverwrite(json, monaLibrary.sections);
            monaLibrary.icons = new Dictionary<string, Texture2D>();
            monaLibrary.tools = new Dictionary<int, MonaLibrary.Tools>();
            monaLibrary.assets = new Dictionary<int, MonaLibrary.Assets>();
            for (int i = 0; i < monaLibrary.sections.records.Count; i++)
            {
                if (monaLibrary.sections.records[i].fields.type == "tool")
                {
                    fields = new string[] {"name", "file", "icon", "description", "docslink"};
                    yield return EditorCoroutineUtility.StartCoroutine(DownloadJson("tool", getJsonURL(monaLibrary.sections.records[i].fields.table, 300, fields)), this);
                    if (json == "error")
                    {
                        isLoading = false;
                        yield break;
                    }
                    monaLibrary.tempTool = new MonaLibrary.Tools();
                    monaLibrary.tempTool.records = new List<MonaLibrary.Tools.Item>();
                    JsonUtility.FromJsonOverwrite(json,  monaLibrary.tempTool);
                    monaLibrary.tools.Add(i, monaLibrary.tempTool);
                }
                if (monaLibrary.sections.records[i].fields.type == "asset")
                {
                    fields = new string[] {"Asset+Name", "Category", "Asset+Files", "Polycount+(Tris)", "Artist+Name", "Photo(s)", "Alias", "Invalid"};
                    yield return EditorCoroutineUtility.StartCoroutine(DownloadJson("asset", getJsonURL(monaLibrary.sections.records[i].fields.table, 300, fields)), this);
                    if (json == "error")
                    {
                        isLoading = false;
                        yield break;
                    }
                    monaLibrary.tempAsset = new MonaLibrary.Assets();
                    monaLibrary.tempAsset.records = new List<MonaLibrary.Assets.Item>();
                    JsonUtility.FromJsonOverwrite(json,  monaLibrary.tempAsset);
                    monaLibrary.assets.Add(i, monaLibrary.tempAsset);
                }
            }
            isInitalized = true;
        }

        private IEnumerator DownloadThumbnail(string url)
        {
            using (UnityWebRequest imgReq = UnityWebRequestAssetBundle.GetAssetBundle(url))
            {
                imgReq.SetRequestHeader("Authorization", $"Bearer {AIRTABLE_PUBLIC_API_KEY}");
                imgReq.downloadHandler = new DownloadHandlerTexture();
                yield return imgReq.SendWebRequest();
                if (imgReq.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log($"Mona Library Asset Request Failed: {url}{imgReq.result}");
                }
                else
                {
                    monaLibrary.icons[url] = DownloadHandlerTexture.GetContent(imgReq);
                }
            }
        }

        private IEnumerator DownloadAssetPackage(string url, string name)
        {
            using (UnityWebRequest assetReq = new UnityWebRequest(url))
            {
                assetReq.SetRequestHeader("Authorization", $"Bearer {AIRTABLE_PUBLIC_API_KEY}");
                assetReq.downloadHandler = new DownloadHandlerFile($"Assets/_MonaLibrary/{name}/.unitypackage");
                yield return assetReq.SendWebRequest();
                if (assetReq.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log($"Mona Library Request Failed: {url}{assetReq.result}");
                }
                else
                {
                    AssetDatabase.ImportPackage($"Assets/_MonaLibrary/{name}/.unitypackage", true);
                }
            }
        }

        private string getJsonURL(string _base, int maxRecords, string[] fields)
        {
            string url = string.Concat
            (
                "https://api.airtable.com/v0/",
                AIRTABLE_BASE_ID,
                "/",
                _base,
                "?maxRecords=",
                maxRecords
            );
            if (fields != null)
            {
                foreach (string field in fields)
                {
                    url += ($"&fields%5B%5D={field}");
                }
            }
            return url;
        }

        private string FormatJson(string type, string json)
        {
            switch (type)
            {
                case "sections":
                    return json;
                case "tool":
                    return json;
                case "asset":
                    return json
                        .Replace("Asset Name", "name")
                        .Replace("Artist Name", "artist")
                        .Replace("Polycount (Tris)", "polycount")
                        .Replace("Asset Files", "file")
                        .Replace("Photo(s)", "icon")
                        .Replace("Invalid", "invalid");
            }
            return json;
        }
    }
}
#endif