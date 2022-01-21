#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;

namespace Mona
{
    [InitializeOnLoad]
    public class QAToolbar
    {
        static QAToolbar()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            GUI.skin.button.fontSize = 12;
            GUI.skin.button.fixedHeight = 22;
            GUI.skin.button.border = new RectOffset(0, 0, 0, 0);
            GUI.skin.button.margin = new RectOffset(0, 0, 0, 0);
            GUI.skin.button.padding = new RectOffset(10, 10, 4, 4);
            GUI.skin.button.alignment = TextAnchor.MiddleCenter;
            GUI.color = Color.white * 0.75f;

            if (QualityAssurance.ErrorCodes != null && QualityAssurance.ErrorCodes.Count != 0)
            {
                GUI.contentColor = Color.red * 20.19f;
            }else{
                GUI.contentColor = Color.white * 1.2f;
            }

            if (GUILayout.Button(new GUIContent("▲ QA", "QA")))
            {
                // Open the QA window
                QAEditor.Init();
            }
            GUI.color = Color.white;
        }
    }
}
#endif