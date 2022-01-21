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
        // Ensure that Space, PortalLayer, ArtifactLayer scenes exist.
        // Code MISSING_LAYER
        public static void TestSceneExistance()
        {
            string _badScenes = "";
            if (s_spaceScene.IsValid() == false)
            {
                _badScenes += "Space ";
            }

            if (s_artifactsScene.IsValid() == false)
            {
                _badScenes += "Artifacts ";
            }

            if (s_portalsScene.IsValid() == false)
            {
                _badScenes += "Portals ";
            }

            if (_badScenes != "")
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.MISSING_LAYER, "The following scenes do not exist: " + _badScenes + ". Please create them." });
            }
        }

        // Check all scenes so they have a valid root level object tagged with "Layer".
        // Code BAD_LAYER_TAG
        public static void TestLayerTags()
        {
            string _badScenes = "";

            if (s_spaceScene.IsValid())
            {
                var _rootObjects = s_spaceScene.GetRootGameObjects();
                bool _found = false;
                foreach (var _rootObject in _rootObjects)
                {
                    if (_rootObject.tag == "Space")
                    {
                        _found = true;
                        break;
                    }
                }
                if (!_found)
                {
                    _badScenes += "Space ";
                }
            }

            if (s_artifactsScene.IsValid())
            {
                var _rootObjects = s_artifactsScene.GetRootGameObjects();
                bool _found = false;
                foreach (var _rootObject in _rootObjects)
                {
                    if (_rootObject.tag == "ArtifactLayer")
                    {
                        _found = true;
                        break;
                    }
                }
                if (!_found)
                {
                    _badScenes += "Artifacts ";
                }
            }

            if (s_portalsScene.IsValid())
            {
                var _rootObjects = s_portalsScene.GetRootGameObjects();
                bool _found = false;
                foreach (var _rootObject in _rootObjects)
                {
                    if (_rootObject.tag == "PortalLayer")
                    {
                        _found = true;
                        break;
                    }
                }
                if (!_found)
                {
                    _badScenes += "Portals ";
                }
            }

            if (_badScenes != "")
            {
                SpaceErrors.Add(new string[] { MonaErrorCodes.BAD_LAYER_TAG, "The following scenes have no root level object tagged with \"Layer\": " + _badScenes });
            }
        }
    }
}
#endif