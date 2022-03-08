#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace Mona
{
    public class QualityAssuranceEditor : EditorWindow
    {
        Vector2 Scroll;

        [MenuItem("Mona/Quality Assurance")]
        public static void Init()
        {

            QualityAssuranceEditor _window = (QualityAssuranceEditor)EditorWindow.GetWindow(typeof(QualityAssuranceEditor));

            // Set Editor title
            _window.titleContent = new GUIContent("Quality Assurance");

            _window.Show();

            QualityAssurance.SpaceErrors = QualityAssurance.GetSpaceErrors();
        }

        void OnGUI()
        {
            Scroll = GUILayout.BeginScrollView(Scroll, false, false);

            GUIStyle _font = new GUIStyle();
            _font.fontSize = 20;
            _font.wordWrap = true;

            GUIStyle _font2 = new GUIStyle();
            _font.fontSize = 15;
            _font.wordWrap = true;

            GUIStyle _button1 = GUI.skin.button;
            _button1.fontSize = 16;
            _button1.margin = new RectOffset(30, 30, 10, 10);

            GUIStyle _style = new GUIStyle();
            _style.fontSize = 15;
            _font.wordWrap = true;


            GUILayout.BeginHorizontal("", _style);
            
            GUILayout.EndHorizontal();

            if (QualityAssurance.SpaceErrors == null || QualityAssurance.SpaceErrors.Count == 0)
            {
                GUILayout.Space(1);
                GUILayout.Box("  √ No errors found.", _style, GUILayout.MinWidth(100));
            }
            else
            {
                GUILayout.BeginHorizontal("", _style, GUILayout.MinHeight(30));
                GUILayout.Space(10);
                GUILayout.TextArea("There are some issues in your space that needs to be fixed before minting can be approved.", _font2);
                GUILayout.EndHorizontal();
                GUILayout.Space(1);

                foreach (string _error in QualityAssurance.SpaceErrors)
                {
                    GUILayout.Space(10);

                    string title = _error.Replace(".", " ");
                    title = title.Replace("-", " ");
                    title = title.Substring(0, 1).ToUpper() + title.Substring(1);

                    GUILayout.Box("  ⚠️  " + title, _style, GUILayout.MinWidth(100));
                    GUILayout.BeginHorizontal("box");
                    GUILayout.Label("└-- " + QualityAssurance.GetErrorDescription(_error));
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Run Quality Test", _button1))
            {
                QualityAssurance.SpaceErrors = QualityAssurance.GetSpaceErrors();
            }

            GUILayout.EndScrollView();
        }
    }

}
#endif