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
        // Check for portals that are outside of the portal layer.
        // Code BAD_PORTAL_PLACEMENT
        public static void TestPortalPlacments()
        {
            string _badScenes = "";

            GameObject[] _portals = GameObject.FindGameObjectsWithTag("Portal");
            foreach (GameObject _portal in _portals)
            {
                if (FindParentWithTag("PortalLayer", _portal) == null)
                {
                    _badScenes += _portal.name + " ";
#if UNITY_EDITOR                    
                    // Register object
                    s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_portal.GetInstanceID(), -1, 0));
                    s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_portal.transform.parent.gameObject.GetInstanceID(), 1, 0));
#endif
                }
            }

            if (_badScenes != "")
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_PORTAL_PLACEMENT, "The following portals are not placed in the portal layer: " + _badScenes });
            }
        }

        // Check for missconfigured portals.
        // Code BAD_PORTAL_TAG
        public static void TestPortalTag()
        {
            bool _badPortals = false;

            // Check all gameobjects in the Portal Layer
            GameObject _portalLayer = GameObject.FindGameObjectWithTag("PortalLayer");
            if (_portalLayer != null)
            {
                foreach (Transform _portal in _portalLayer.transform)
                {
                    bool _hasPortal = false;

                    if (_portal.tag == "Portal")
                    {
                        _hasPortal = true;
                    }

                    // Check if any of the children has a portal tag
                    foreach (Transform _child in _portal)
                    {
                        if (_child.tag == "Portal")
                        {
                            _hasPortal = true;
                        }
                    }

                    if (!_hasPortal)
                    {
                        _badPortals = true;
#if UNITY_EDITOR
                        // Register object
                        s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_portal.gameObject.GetInstanceID(), -1, 0));
                        s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_portal.transform.parent.gameObject.GetInstanceID(), 1, 0));
#endif
                    }
                }
            }

            if (_badPortals)
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_PORTAL_TAG, "Portal is missing it's Portal Tag, Add it on a object that's the 3rd child of root." });
            }
        }

        // Check for portals without a box collider.
        // Code BAD_PORTAL_COLLIDER
        public static void TestPortalCollider()
        {
            bool _badPortals = false;

            // Check all gameobjects in the Portal Layer
            GameObject _portalLayer = GameObject.FindGameObjectWithTag("PortalLayer");
            if (_portalLayer != null)
            {
                foreach (Transform _portal in _portalLayer.transform)
                {
                    bool _hasCollider = false;

                    if (_portal.GetComponent<BoxCollider>() != null)
                    {
                        if (!_portal.GetComponent<BoxCollider>().enabled)
                        {
                            _badPortals = true;
                        }
                        if (_portal.GetComponent<BoxCollider>().isTrigger)
                        {
                            _badPortals = true;
                        }
                        _hasCollider = true;
                    }

                    // Check if any of the children has a portal tag
                    foreach (Transform _child in _portal)
                    {
                        if (_child.GetComponent<BoxCollider>() != null)
                        {
                            if (!_child.GetComponent<BoxCollider>().enabled)
                            {
                                _badPortals = true;
                            }
                            if (_child.GetComponent<BoxCollider>().isTrigger)
                            {
                                _badPortals = true;
                            }
                            _hasCollider = true;
                        }
                    }

                    if (!_hasCollider)
                    {
                        _badPortals = true;
#if UNITY_EDITOR
                        // Register object
                        s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_portal.gameObject.GetInstanceID(), -1, 0));
                        s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_portal.transform.parent.gameObject.GetInstanceID(), 1, 0));
#endif
                    }
                }
            }

            if (_badPortals)
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_PORTAL_COLLIDER, "Portals Box Collider is missing or, it's a trigger, or it's disabled." });
            }
        }

        // Check for portals with duplicate names.
        // Code BAD_PORTAL_NAME
        public static void TestPortalNames()
        {
            bool _badPortals = false;

            GameObject _portalLayer = GameObject.FindGameObjectWithTag("PortalLayer");
            if (_portalLayer != null)
            {
                GameObject[] _portals = GameObject.FindGameObjectsWithTag("Portal");
                for (int i = 0; i < _portals.Length; i++)
                {
                    for (int ix = 0; i < _portals.Length; i++)
                    {
                        if (i != ix)
                        {
                            if (_portals[i].name == _portals[ix].name)
                            {
                                _badPortals = true;
#if UNITY_EDITOR
                                // Register object
                                s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_portals[i].gameObject.GetInstanceID(), -1, 0));
                                s_iconRegisters.Add(TreeIcon.RegisterHierarchyItem(_portals[i].transform.parent.gameObject.GetInstanceID(), 1, 0));
#endif
                            }
                        }
                    }
                }
            }

            if (_badPortals)
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_PORTAL_NAME, "Portal has the same name as another portal." });
            }
        }


    }
}
#endif