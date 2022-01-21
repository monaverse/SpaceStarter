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
        // Check for canvas that are outside of the portal layer.
        // Code BAD_ARTIFACT_PLACEMENT
        public static void TestArtifactPlacments()
        {
            string _badArtifacts = "";

            GameObject[] _artifacts = GameObject.FindGameObjectsWithTag("Artifact");
            foreach (GameObject _artifact in _artifacts)
            {
                if (FindParentWithTag("ArtifactLayer", _artifact) == null)
                {
                    _badArtifacts += _artifact.name + " ";
#if UNITY_EDITOR                    
                    // Register object
                    s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_artifact.gameObject.GetInstanceID(), 1, 0));
                    s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_artifact.transform.parent.gameObject.GetInstanceID(), 1, 0));
#endif
                }
            }

            if (_badArtifacts != "")
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_ARTIFACT_PLACEMENT, "The following Artifact are not placed in the ArtifactLayer: " + _badArtifacts });
            }
        }

        // Check for missconfigured Artifacts.
        // Code BAD_ARTIFACT_TAG
        public static void TestArtifactTag()
        {
            bool _badArtifacts = false;

            GameObject _ArtifactLayer = GameObject.FindGameObjectWithTag("ArtifactLayer");
            if (_ArtifactLayer != null)
            {
                foreach (Transform _artifact in _ArtifactLayer.transform)
                {
                    bool _isValid = false;

                    if (_artifact.tag == "Artifact" || _artifact.tag == "Canvas")
                    {
                        _isValid = true;
                    }

                    foreach (Transform _child in _artifact)
                    {
                        if (_child.tag == "Artifact" || _child.tag == "Canvas")
                        {
                            _isValid = true;
                        }
                    }

                    if (!_isValid)
                    {
                        _badArtifacts = true;
#if UNITY_EDITOR
                        // Register object
                        s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_artifact.gameObject.GetInstanceID(), -1, 0));
                        s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_artifact.transform.parent.gameObject.GetInstanceID(), 1, 0));
#endif
                    }
                }
            }

            if (_badArtifacts)
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_ARTIFACT_TAG, "Artifact is missing it's Artifact Tag, Add it on a object that's the 3rd child of root." });
            }
        }

        // Check for Artifacts with duplicate names.
        // Code BAD_ARTIFACT_NAME
        public static void TestArtifactNames()
        {
            bool _badArtifact = false;

            GameObject _artifactLayer = GameObject.FindGameObjectWithTag("ArtifactLayer");
            if (_artifactLayer != null)
            {
                GameObject[] _artifacts = GameObject.FindGameObjectsWithTag("Artifact");
                for (int i = 0; i < _artifacts.Length; i++)
                {
                    for (int ix = 0; i < _artifacts.Length; i++)
                    {
                        if (i != ix)
                        {
                            if (_artifacts[i].name == _artifacts[ix].name)
                            {
                                _badArtifact = true;
#if UNITY_EDITOR
                                // Register object
                                s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_artifacts[i].gameObject.GetInstanceID(), -1, 0));
                                s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_artifacts[i].transform.parent.gameObject.GetInstanceID(), 1, 0));
#endif
                            }
                        }
                    }
                }
            }

            if (_badArtifact)
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_ARTIFACT_NAME, "Artifact has the same name as another Artifact." });
            }
        }

        // Check for Artifacts without a box or mesh collider.
        // Code BAD_ARTIFACT_COLLIDER
        public static void TestArtifactCollider()
        {
            bool _badArtifact = false;

            GameObject _artifactLayer = GameObject.FindGameObjectWithTag("ArtifactLayer");
            if (_artifactLayer != null)
            {
                GameObject[] _artifacts = GameObject.FindGameObjectsWithTag("Artifact");
                foreach (GameObject _artifact in _artifacts)
                {
                    BoxCollider _boxCollider = _artifact.GetComponent<BoxCollider>();
                    MeshCollider _meshCollider = _artifact.GetComponent<MeshCollider>();

                    if (_boxCollider == null && _meshCollider == null)
                    {
                        _badArtifact = true;

                    }
                    else
                    {
                        if (_boxCollider != null && _boxCollider.isTrigger)
                        {
                            _badArtifact = true;
                        }

                        if (_meshCollider != null && _meshCollider.isTrigger)
                        {
                            _badArtifact = true;
                        }
                    }

                    if (_badArtifact)
                    {
                        // Register object
                        s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_artifact.gameObject.GetInstanceID(), -1, 0));
                        s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_artifact.transform.parent.gameObject.GetInstanceID(), 1, 0));
                    }

                }
            }

            if (_badArtifact)
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_ARTIFACT_COLLIDER, "Artifact has no collider. Or the collider is marked as trigger!" });
            }
        }
    }
}
#endif