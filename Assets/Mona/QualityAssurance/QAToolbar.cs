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

            if (QualityAssurance.ErrorCodes != null)
            {
                GUI.skin.button.fontSize = 12;
                GUI.skin.button.fixedHeight = 22;
                GUI.skin.button.border = new RectOffset(0, 0, 0, 0);
                GUI.skin.button.margin = new RectOffset(0, 0, 0, 0);
                GUI.skin.button.padding = new RectOffset(10, 10, 4, 4);
                GUI.skin.button.alignment = TextAnchor.MiddleCenter;
                GUI.color = Color.white * 0.75f;
                GUI.contentColor = Color.red * 20.19f;

                if (GUILayout.Button(new GUIContent("▲ Quality Assurance", "QA")))
                {
                    // Open the QA window
                    QAEditor.Init();
                }
            }
            GUI.color = Color.white;
        }
    }
}
#endif