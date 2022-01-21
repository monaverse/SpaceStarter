#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Mona
{
    [InitializeOnLoad]
    class TreeIcon
    {
        static Dictionary<int, int[]> IconMap;
        static List<Texture2D> Icons;

        static TreeIcon()
        {
            // Init
            Icons = new List<Texture2D>();
            IconMap = new Dictionary<int, int[]>();
            Icons.Add(AssetDatabase.LoadAssetAtPath("Assets/Gizmos/h_light.png", typeof(Texture2D)) as Texture2D);
            Icons.Add(AssetDatabase.LoadAssetAtPath("Assets/Gizmos/h_bad.png", typeof(Texture2D)) as Texture2D);
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        }

        public static int RegisterHierarchyItem(int instanceID, int icon, int highlight = -1)
        {
            if (IconMap.ContainsKey(instanceID)) { return instanceID; }
            IconMap.Add(instanceID, new int[] { icon, highlight });

            // Force hierarchy to refresh
            EditorApplication.RepaintHierarchyWindow();

            return instanceID;
        }

        public static void UnregisterHierarchyItem(int instanceID)
        {
            IconMap.Remove(instanceID);

            // Force hierarchy to refresh
            EditorApplication.RepaintHierarchyWindow();
        }

        static void HierarchyItemCB(int instanceID, Rect selectionRect)
        {
            // Check if instanceID is registered in IconMap
            if (!IconMap.ContainsKey(instanceID))
                return;

            int[] _icon = IconMap[instanceID];

            Rect _r = new Rect(selectionRect);
            _r.width = 13;
            _r.height = 13;
            _r.y += 1;
            _r.x += selectionRect.width - 5;

            // Draw a highlight
            if (_icon[1] != -1)
            {
                Rect _h = new Rect(selectionRect);
                _h.width = selectionRect.width + 16;
                _h.height = selectionRect.height;
                GUI.DrawTexture(_h, Icons[_icon[1]]);
            }

            if (_icon[0] != -1)
            {
                GUI.DrawTexture(_r, Icons[_icon[0]]);
            }
        }
    }
}
#endif