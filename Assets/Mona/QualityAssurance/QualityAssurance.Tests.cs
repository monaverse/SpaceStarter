using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Mona
{
    // Quality Checks that run both in editor and in private-build
    public static partial class QualityAssurance
    {
        // Tests to make sure a specific scene exists
        private static void TestSceneExistence(string sceneName, string error)
        {
            Scene _scene = SceneManager.GetSceneByName(sceneName);
            if (_scene.IsValid() == false)
            {
                SpaceErrors.Add(error);
            }
        }

        // 1. Check to make sure scene has a valid root level object tagged with the layer name
        //      Code: MISSING_X_LAYER
        // 2. Check to make sure there is only one root object in each scene
        //      Code: MULTIPLE_X_ROOTS
        private static void TestSceneLayer(string sceneName, string layerTag, string missingLayerError, string multipleRootError)
        {
            Scene _scene = SceneManager.GetSceneByName(sceneName);
            if (!_scene.IsValid()) return;
            var _rootObjects = _scene.GetRootGameObjects();

            bool _found = false;
            foreach (var _rootObject in _rootObjects)
            {
                if (_rootObject.tag == layerTag)
                {
                    _found = true;
                    break;
                }
            }
            if (!_found) SpaceErrors.Add(missingLayerError);

            if (_rootObjects.Length > 1)
            {
                foreach (var _object in _rootObjects)
                {
                    if (
                        !_object.tag.Equals(layerTag)
                        && !_object.name.StartsWith("!ftraceLightmaps")
                        && !_object.name.Equals("PDC")
                        && _object.activeSelf
                    )
                    {
                        SpaceErrors.Add(multipleRootError);
                    }
                }
            }
        }

        // Checks if an object tag is properly contained within its parent layer
        private static void TestObjectPlacements(string objectTag, string layerTag, string error)
        {
            GameObject[] _gameObjects = GameObject.FindGameObjectsWithTag(objectTag);
            foreach (GameObject _object in _gameObjects)
            {
                if (FindParentWithTag(layerTag, _object) == null)
                {
                    SpaceErrors.Add(error);
                }
            }
        }

        // Tests the uniqueness of a set of game objects
        public static void TestNameUniqueness(string tag, string error)
        {
            GameObject[] _allObjects = GameObject.FindGameObjectsWithTag(tag);
            Dictionary<string, bool> _names = new Dictionary<string, bool>();
            foreach (GameObject _object in _allObjects)
            {
                if (_names.ContainsKey(_object.name))
                {
                    SpaceErrors.Add(error);
                    return;
                }
                _names.Add(_object.name, true);
            }
        }

        // Makes sure all of the game objects in a layer belong in that layer
        public static void TestLayerContents(string layerName, string[] validTags, string error)
        {
            GameObject _layer = GameObject.FindGameObjectWithTag(layerName);
            if (!_layer) return;

            foreach (Transform _rootObject in _layer.transform)
            {
                bool _isValid = false;

                foreach (string tag in validTags)
                {
                    if (_rootObject.tag == tag) _isValid = true;
                }

                if (_isValid) continue;

                foreach (Transform _child in _rootObject)
                {
                    foreach (string tag in validTags)
                    {
                        if (_child.tag == tag) _isValid = true;
                    }
                }

                if (!_isValid) SpaceErrors.Add(error);
            }
        }

        // Overload accepting a single tag instead of an array
        public static void TestLayerContents(string layerName, string validTag, string error)
        {
            TestLayerContents(layerName, new string[] { validTag }, error);
        }

        // Tests to make sure the collider on a game object is properly configured
        public static void TestObjectColliders<T>(string objectTag, string error)
        {
            GameObject[] _gameObjects = GameObject.FindGameObjectsWithTag(objectTag);
            foreach (GameObject _gameObject in _gameObjects)
            {
                Collider _collider = _gameObject.GetComponent<T>() as Collider;
                if (_collider == null || !_collider.enabled || _collider.isTrigger)
                {
                    SpaceErrors.Add(error);
                }
            }
        }
    }
}