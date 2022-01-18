using System;
using UnityEngine;
using UnityEditor;

namespace Mona
{
    public class QAEditor : EditorWindow
    {
        Vector2 scroll;

        [MenuItem("Mona/Quality Assurance")]
        static void Init()
        {

            QAEditor window = (QAEditor)EditorWindow.GetWindow(typeof(QAEditor));
            window.title = "Quality Assurance";
            window.Show();

            QualityAssurance.CheckQuality();
        }

        void OnGUI()
        {
            /*
            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/code/Editor/reactor.png", typeof(Texture));
            GUI.DrawTexture(new Rect(0, 0, 498, 66), banner, ScaleMode.ScaleToFit, false);
            GUILayout.Space(65);
            */

            scroll = GUILayout.BeginScrollView(scroll, false, false);

            if(QualityAssurance.ErrorCodes == null || QualityAssurance.ErrorCodes.Length == 0)
            {
                GUILayout.Label("No errors found.");
            }
            else
            {
                foreach(string error in QualityAssurance.ErrorCodes)
                {
                    
                    GUILayout.Label(error);
                }
            }
            
            GUILayout.EndScrollView();
        }
    }

}