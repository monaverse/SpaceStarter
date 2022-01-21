#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

namespace Mona
{
    public class QAEditor : EditorWindow
    {
        Vector2 Scroll;

        [MenuItem("Mona/Quality Assurance")]
        public static void Init()
        {

            QAEditor _window = (QAEditor)EditorWindow.GetWindow(typeof(QAEditor));

            // Set Editor title
            _window.titleContent = new GUIContent("Quality Assurance");

            _window.Show();

            QualityAssurance.CheckQuality();
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
            //button1.fixedHeight = 30;

            GUIStyle _style = new GUIStyle();
            _style.fontSize = 15;
            _font.wordWrap = true;

            Texture2D _tex = new Texture2D(1, 1);
            _tex.SetPixel(0, 0, new Color(1f, 0.0f, 0.0f, 1.0f));
            _style.normal.background = _tex;

            GUILayout.BeginHorizontal("", _style);
            GUILayout.Space(10);
            GUILayout.Label("Mona Quality Assurance", _font);
            GUILayout.EndHorizontal();

            // Enable wrapping text


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

                foreach (string[] error in QualityAssurance.SpaceErrors)
                {
                    GUILayout.Box("  ⚠️  " + error[0], _style, GUILayout.MinWidth(100));
                    GUILayout.BeginHorizontal("box");
                    GUILayout.Label("└ " + error[1]);
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Run Quality Test", _button1))
            {
                QualityAssurance.CheckQuality();
            }

            GUILayout.EndScrollView();
        }
    }

}
#endif