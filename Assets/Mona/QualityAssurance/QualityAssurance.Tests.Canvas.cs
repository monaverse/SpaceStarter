#if UNITY_EDITOR
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Mona
{
    // Quailty Checks that run both in editor and in private-build
    public static partial class QualityAssurance
    {
        // Check for canvas that are outside of the Artifact layer.
        // Code BAD_CANVAS_PLACEMENT
        public static void TestCanvasPlacments()
        {
            string _badCanvas = "";

            GameObject[] _canvasObjects = GameObject.FindGameObjectsWithTag("Canvas");
            foreach (GameObject _canvas in _canvasObjects)
            {
                if (FindParentWithTag("ArtifactLayer", _canvas) == null)
                {
                    _badCanvas += _canvas.name + " ";
#if UNITY_EDITOR                    
                    // Register object
                    s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_canvas.gameObject.GetInstanceID(), 1, 0));
                    s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_canvas.transform.parent.gameObject.GetInstanceID(), 1, 0));
#endif
                }
            }

            if (_badCanvas != "")
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_CANVAS_PLACEMENT, "The following Canvas are not placed in the ArtifactLayer: " + _badCanvas });
            }
        }

        // Check for Canvas with duplicate names.
        // Code BAD_CANVAS_NAME
        public static void TestCanvasNames()
        {
            bool _badCanvas = false;

            GameObject _artifactLayer = GameObject.FindGameObjectWithTag("ArtifactLayer");
            if (_artifactLayer != null)
            {
                GameObject[] _canvas = GameObject.FindGameObjectsWithTag("Canvas");
                for (int i = 0; i < _canvas.Length; i++)
                {
                    for (int ix = 0; i < _canvas.Length; i++)
                    {
                        if (i != ix)
                        {
                            if (_canvas[i].name == _canvas[ix].name)
                            {
                                _badCanvas = true;
#if UNITY_EDITOR
                                // Register object
                                s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_canvas[i].gameObject.GetInstanceID(), -1, 0));
                                s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_canvas[i].transform.parent.gameObject.GetInstanceID(), 1, 0));
#endif
                            }
                        }
                    }
                }
            }

            if (_badCanvas)
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_CANVAS_NAME, "A Canvas has the same name as another canvas." });
            }
        }
    }
}
#endif