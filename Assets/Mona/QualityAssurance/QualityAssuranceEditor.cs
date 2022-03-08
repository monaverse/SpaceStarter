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

            _window.titleContent = new GUIContent("Quality Assurance");
            _window.Show();

            QualityAssurance.SpaceErrors = QualityAssurance.GetSpaceErrors();
        }

        void OnGUI()
        {
            Scroll = GUILayout.BeginScrollView(Scroll, false, false);

            GUIStyle _buttonStyle = GUI.skin.button;
            _buttonStyle.margin = new RectOffset(30, 30, 10, 10);

            GUILayout.BeginHorizontal();
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Resources/Editor/qa.png", typeof(Texture));
            GUI.DrawTexture(new Rect(0, 0, 498, 66), banner, ScaleMode.ScaleToFit, false);
            GUILayout.EndHorizontal();
            GUILayout.Space(64);

            if (QualityAssurance.SpaceErrors == null || QualityAssurance.SpaceErrors.Count == 0)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Label("√ No errors found.");
                GUILayout.EndHorizontal();
            }
            else
            {
                Texture _errIcon = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Resources/Editor/qa_err.png", typeof(Texture));
                GUI.DrawTexture(new Rect(10, 10, 48, 48), _errIcon, ScaleMode.ScaleAndCrop, false);

                foreach (string _error in QualityAssurance.SpaceErrors)
                {
                    GUILayout.Space(1);

                    string _title = _error.Replace(".", " ");
                    _title = _title.Replace("-", " ");
                    _title = _title.Substring(0, 1).ToUpper() + _title.Substring(1);

                    GUILayout.BeginHorizontal("box");
                    GUILayout.Box(_title, GUILayout.MinWidth(100));
                    GUILayout.Label(QualityAssurance.GetErrorDescription(_error));
                    GUILayout.EndHorizontal();
                }
            }

            GUILayout.Space(1);
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Run Quality Test", _buttonStyle))
            {
                QualityAssurance.SpaceErrors = QualityAssurance.GetSpaceErrors();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
        }
    }

}
#endif