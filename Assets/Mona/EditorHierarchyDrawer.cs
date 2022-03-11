#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Mona
{
    [InitializeOnLoad]
    public class EditorHierarchyDrawer
    {
        public Dictionary<int, int[]> DrawingList;
        public List<Texture2D> Icons;

        // Class init
        public EditorHierarchyDrawer()
        {
            Texture2D rowHighlight = AssetDatabase.LoadAssetAtPath("Assets/Gizmos/h_light.png", typeof(Texture2D)) as Texture2D;
            Texture2D failIcon = AssetDatabase.LoadAssetAtPath("Assets/Gizmos/h_bad.png", typeof(Texture2D)) as Texture2D;

            // Support if the script resources are not in the project
            if (rowHighlight == null && failIcon == null) return;

            DrawingList = new Dictionary<int, int[]>();

            Icons = new List<Texture2D> {
                rowHighlight,
                failIcon
            };

            // Register drawing event to unity.
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        }

        // Get all the errors and draw error icons on the hierarchy
        public void Register(Dictionary<string, List<int>> SpaceErrors)
        {
            if (SpaceErrors == null) return;
            foreach (List<int> error in SpaceErrors.Values)
            {
                foreach (int objectID in error)
                {
                    if (objectID == -1) continue;
                    RegisterHierarchyItem(objectID, 1, 0);
                }
            }
        }

        // Append a specific object to the drawing list
        public int RegisterHierarchyItem(int instanceID, int icon, int highlight = -1)
        {
            if (DrawingList.ContainsKey(instanceID)) return instanceID;
            DrawingList.Add(instanceID, new int[] { icon, highlight });

            EditorApplication.RepaintHierarchyWindow();
            return instanceID;
        }

        // Clear all the icons from the drawing list
        public void Unregister()
        {
            DrawingList = new Dictionary<int, int[]>();
        }

        // Remove a specific object from the drawing list
        public void UnregisterHierarchyItem(int instanceID)
        {
            DrawingList.Remove(instanceID);
            EditorApplication.RepaintHierarchyWindow();
        }

        // Draw the icons on the hierarchy, called by Unity per dirty object.
        void HierarchyItemCB(int instanceID, Rect selectionRect)
        {
            if (!DrawingList.ContainsKey(instanceID)) return;

            // Get the icons
            int[] icon = DrawingList[instanceID];

            // Draw a highlight
            if (icon[1] != -1)
            {
                if (Icons[icon[1]] == null) return;
                Rect rectBox = new Rect(selectionRect);
                rectBox.width = selectionRect.width + 16;
                rectBox.height = selectionRect.height;
                GUI.DrawTexture(rectBox, Icons[icon[1]]);
            }

            if (icon[0] == -1) return;
            Rect rect = new Rect(selectionRect);
            rect.width = 13;
            rect.height = 13;
            rect.y += 1;
            rect.x += selectionRect.width - 5;
            GUI.DrawTexture(rect, Icons[icon[0]]);
        }
    }
}
#endif