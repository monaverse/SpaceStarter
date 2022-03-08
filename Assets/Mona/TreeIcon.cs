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
            Icons = new List<Texture2D>();
            IconMap = new Dictionary<int, int[]>();
            Icons.Add(AssetDatabase.LoadAssetAtPath("Assets/Gizmos/h_light.png", typeof(Texture2D)) as Texture2D);
            Icons.Add(AssetDatabase.LoadAssetAtPath("Assets/Gizmos/h_bad.png", typeof(Texture2D)) as Texture2D);
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        }

        public static int RegisterHierarchyItem(int instanceID, int icon, int highlight = -1)
        {
            if (IconMap.ContainsKey(instanceID)) return instanceID;
            IconMap.Add(instanceID, new int[] { icon, highlight });

            EditorApplication.RepaintHierarchyWindow();
            return instanceID;
        }

        public static void UnregisterHierarchyItem(int instanceID)
        {
            IconMap.Remove(instanceID);

            EditorApplication.RepaintHierarchyWindow();
        }

        static void HierarchyItemCB(int instanceID, Rect selectionRect)
        {
            if (!IconMap.ContainsKey(instanceID)) return;

            int[] icon = IconMap[instanceID];

            Rect rect = new Rect(selectionRect);
            rect.width = 13;
            rect.height = 13;
            rect.y += 1;
            rect.x += selectionRect.width - 5;

            // Draw a highlight
            if (icon[1] != -1)
            {
                Rect rectBox = new Rect(selectionRect);
                rectBox.width = selectionRect.width + 16;
                rectBox.height = selectionRect.height;
                GUI.DrawTexture(rectBox, Icons[icon[1]]);
            }

            if (icon[0] != -1)
            {
                GUI.DrawTexture(rect, Icons[icon[0]]);
            }
        }
    }
}
#endif