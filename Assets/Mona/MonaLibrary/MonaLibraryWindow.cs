#if UNITY_EDITOR
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

namespace Mona
{
    public class MonaLibraryWindow : EditorWindow
    {
        static MonaLibraryWindow window = null;
        
        private readonly string AIRTABLE_BASE_ID = "appglbIlOT8JLLnur";
        private readonly string AIRTABLE_ROOTTABLE_ID = "tblf3EHvQM36tPLJE";
        private readonly int ICON_RESOLUTION = 128;

        private bool _isInitialized = false;
        private bool _isLoading = false;
        private bool _isError = false;

        private string _searchString = "";
        private Vector2 _scrollPos = new Vector2(0,0);

        private MonaLibrary monaLibrary;
        private int sectionTab;

        [MenuItem("Mona/Mona Library")]
        public static void ShowWindow()
        {
            SessionState.SetBool("MonaLibraryOpen", true);
            window = EditorWindow.GetWindow(typeof(MonaLibraryWindow), false, "Mona Library") as MonaLibraryWindow;
            window.minSize = new Vector2(600,360);
            window.maxSize = new Vector2(600,1440);
            window._isInitialized = false;
            window._isLoading = false;
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

        private void OnGUI() 
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
                _isInitialized = false;
                _isLoading = false;
            }
            
            if (_isInitialized)
            {
                DrawLibrary();
                return;
            }

            if (_isLoading)
            {
                EditorGUILayout.LabelField(_isError ? "Error: Mona Library Failed to load." : "Loading Mona Library...", EditorStyles.boldLabel);
            }
            else
            {
                EditorGUILayout.LabelField("Starting loading...", EditorStyles.boldLabel);
                StartLoadingLibraryAsync();
            }
        }

        private async void StartLoadingLibraryAsync()
        {
            _isLoading = true;
            bool isLibraryLoaded = await GetLibraryAsync();
            if (!isLibraryLoaded)
            {
                _isError = true;
            }
        }

        private void DrawLibrary()
        {
            string[] sections = new string[monaLibrary.sections.records.Count];
            for (int i = 0; i < sections.Length; i++)
            {
                sections[i] = monaLibrary.sections.records[i].fields.name;
            }
            sectionTab = GUILayout.Toolbar (sectionTab, sections);
            _searchString = GUILayout.TextField(_searchString, GUI.skin.FindStyle("ToolbarSearchTextField"));
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            if (monaLibrary.sections.records[sectionTab].fields.type == "asset")
            {
                for (int i = 0; i < monaLibrary.assets[sectionTab].records.Count;)
                {
                    GUILayout.BeginHorizontal();
                    for (int j = 0; j < (int)(this.position.width / 145) & i < monaLibrary.assets[sectionTab].records.Count;)
                    {
                        if (!monaLibrary.assets[sectionTab].records[i].fields.invalid & ( monaLibrary.assets[sectionTab].records[i].fields.name.ToLower().Contains(_searchString.ToLower()) | monaLibrary.assets[sectionTab].records[i].fields.artist.ToLower().Contains(_searchString.ToLower())))
                        {
                            GUILayout.BeginVertical(monaLibrary.assets[sectionTab].records[i].fields.name, "window", GUILayout.Height(ICON_RESOLUTION), GUILayout.Width(ICON_RESOLUTION));
                            EditorGUILayout.BeginHorizontal();
                            {
                                GUILayout.FlexibleSpace();
                                EditorGUILayout.LabelField(monaLibrary.assets[sectionTab].records[i].fields.artist, EditorStyles.wordWrappedMiniLabel);
                                GUILayout.FlexibleSpace();
                            }
                            EditorGUILayout.EndHorizontal();
                            if (monaLibrary.icons.ContainsKey(monaLibrary.assets[sectionTab].records[i].fields.icon[0].url))
                            {
                                GUILayout.Box(monaLibrary.icons[monaLibrary.assets[sectionTab].records[i].fields.icon[0].url], GUILayout.Width(ICON_RESOLUTION), GUILayout.Height(ICON_RESOLUTION));
                            }
                            else
                            {
                                monaLibrary.icons.Add(monaLibrary.assets[sectionTab].records[i].fields.icon[0].url, AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/Editor/error_thumb.png"));
                                _ = DownloadThumbnailAsync(monaLibrary.assets[sectionTab].records[i].fields.icon[0].url);
                            }
                            if (GUILayout.Button("Download"))
                            {
                                _ = DownloadAssetPackageAsync(monaLibrary.assets[sectionTab].records[i].fields.file[0].url, monaLibrary.assets[sectionTab].records[i].fields.file[0].filename);
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
                        if (monaLibrary.tools[sectionTab].records[i].fields.name.ToLower().Contains(_searchString.ToLower()))
                        {
                            GUILayout.BeginVertical(monaLibrary.tools[sectionTab].records[i].fields.name, "window", GUILayout.Height(ICON_RESOLUTION), GUILayout.Width(ICON_RESOLUTION));
                            if (monaLibrary.icons.ContainsKey(monaLibrary.tools[sectionTab].records[i].fields.icon[0].url))
                            {
                                GUILayout.Box(monaLibrary.icons[monaLibrary.tools[sectionTab].records[i].fields.icon[0].url], GUILayout.Width(ICON_RESOLUTION), GUILayout.Height(ICON_RESOLUTION));
                            }
                            else
                            {
                                monaLibrary.icons.Add(monaLibrary.tools[sectionTab].records[i].fields.icon[0].url, AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/Editor/error_thumb.png"));
                                _ = DownloadThumbnailAsync(monaLibrary.tools[sectionTab].records[i].fields.icon[0].url);
                            }
                            if (GUILayout.Button("Download"))
                            {
                                _ = DownloadAssetPackageAsync(monaLibrary.tools[sectionTab].records[i].fields.file[0].url, monaLibrary.tools[sectionTab].records[i].fields.file[0].filename);
                            }
                            GUILayout.EndVertical();
                            j++;
                        }
                        i++;
                    }
                    GUILayout.EndHorizontal();
                }
                        
            }
            EditorGUILayout.EndScrollView();
        }

        private async Task<bool> GetLibraryAsync()
        {
            string json = "";
            string[] fields;

            async Task DownloadJsonAsync(string type, string url)
            {
                using (UnityWebRequest webReq = new UnityWebRequest(url))
                {
                    webReq.SetRequestHeader("Authorization", $"Bearer {TemplateCheck.AIRTABLE_PUBLIC_API_KEY}");
                    webReq.downloadHandler = new DownloadHandlerBuffer();

                    var operation = webReq.SendWebRequest();

                    while (!operation.isDone) await Task.Delay(100);

                    if (webReq.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log($"Mona Library API Request Failed: {url} Error = {webReq.result}");
                        json = "error";
                        await Task.Delay(5000);
                    }
                    else
                    {
                        json = FormatJson(type, webReq.downloadHandler.text);
                    }
                }
            }

            fields = new string[] { "name", "table", "type" };
            await DownloadJsonAsync("sections", getJsonURL(AIRTABLE_ROOTTABLE_ID, 100, fields));

            if (json == "error")
            {
                _isLoading = false;
                return false;
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
                    fields = new string[] { "name", "file", "icon", "description", "docslink" };
                    await DownloadJsonAsync("tool", getJsonURL(monaLibrary.sections.records[i].fields.table, 300, fields));

                    if (json == "error")
                    {
                        _isLoading = false;
                        return false;
                    }

                    monaLibrary.tempTool = new MonaLibrary.Tools();
                    monaLibrary.tempTool.records = new List<MonaLibrary.Tools.Item>();
                    JsonUtility.FromJsonOverwrite(json, monaLibrary.tempTool);
                    monaLibrary.tools.Add(i, monaLibrary.tempTool);
                }
                if (monaLibrary.sections.records[i].fields.type == "asset")
                {
                    fields = new string[] { "Asset+Name", "Category", "Asset+Files", "Polycount+(Tris)", "Artist+Name", "Photo(s)", "Alias", "Invalid" };
                    await DownloadJsonAsync("asset", getJsonURL(monaLibrary.sections.records[i].fields.table, 300, fields));

                    if (json == "error")
                    {
                        _isLoading = false;
                        return false;
                    }

                    monaLibrary.tempAsset = new MonaLibrary.Assets();
                    monaLibrary.tempAsset.records = new List<MonaLibrary.Assets.Item>();
                    JsonUtility.FromJsonOverwrite(json, monaLibrary.tempAsset);
                    monaLibrary.assets.Add(i, monaLibrary.tempAsset);
                }
            }
            _isInitialized = true;
            return true;
        }

        private async Task DownloadThumbnailAsync(string url)
        {
            await Task.Yield();

            using (UnityWebRequest imgReq = UnityWebRequestTexture.GetTexture(url))
            {
                imgReq.SetRequestHeader("Authorization", $"Bearer {TemplateCheck.AIRTABLE_PUBLIC_API_KEY}");

                var operation = imgReq.SendWebRequest();
                
                while (!operation.isDone) await Task.Delay(100);

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

        private async Task DownloadAssetPackageAsync(string url, string name)
        {
            await Task.Yield(); 

            using (UnityWebRequest assetReq = new UnityWebRequest(url))
            {
                string filePath = $"Assets/_MonaLibrary/{name}/.unitypackage";
                assetReq.SetRequestHeader("Authorization", $"Bearer {TemplateCheck.AIRTABLE_PUBLIC_API_KEY}");
                assetReq.downloadHandler = new DownloadHandlerFile(filePath);

                var operation = assetReq.SendWebRequest();

                while (!operation.isDone) await Task.Delay(100);

                if (assetReq.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log($"Mona Library Request Failed: {url}{assetReq.result}");
                }
                else
                {
                    AssetDatabase.ImportPackage(filePath, true);
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