#if UNITY_EDITOR
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
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (scene.IsValid() != false) return;

            AddError(error, -1);
        }

        // 1. Check to make sure scene has a valid root level object tagged with the layer name
        //      Code: MISSING_X_LAYER
        // 2. Check to make sure there is only one root object in each scene
        //      Code: MULTIPLE_X_ROOTS
        private static void TestSceneLayer(string sceneName, string layerTag, string missingLayerError, string multipleRootError)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (!scene.IsValid()) return;
            var rootObjects = scene.GetRootGameObjects();

            bool found = false;
            foreach (var rootObject in rootObjects)
            {
                if (rootObject.tag != layerTag) continue;

                found = true;
                break;
            }

            if (!found) AddError(missingLayerError, -1);

            if (rootObjects.Length < 1) return;

            foreach (var rootObject in rootObjects)
            {
                if (
                    !rootObject.tag.Equals(layerTag)
                    && !rootObject.name.StartsWith("!ftraceLightmaps")
                    && !rootObject.name.Equals("PDC")
                    && rootObject.activeSelf
                )
                {
                    AddError(multipleRootError, rootObject.GetInstanceID());
                }
            }
        }

        // Checks if an object tag is properly contained within its parent layer
        private static void TestObjectPlacements(string objectTag, string layerTag, string error)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(objectTag);
            foreach (GameObject sceneObject in gameObjects)
            {
                if (FindParentWithTag(layerTag, sceneObject) != null) continue;
                AddError(error, sceneObject.GetInstanceID());
            }
        }

        // Tests the uniqueness of a set of game objects
        public static void TestNameUniqueness(string tag, string error)
        {
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tag);
            Dictionary<string, bool> names = new Dictionary<string, bool>();
            foreach (GameObject sceneObject in allObjects)
            {
                if (names.ContainsKey(sceneObject.name))
                {
                    AddError(error, sceneObject.GetInstanceID());
                    return;
                }
                names.Add(sceneObject.name, true);
            }
        }

        // Makes sure all of the game objects in a layer belong in that layer
        public static void TestLayerContents(string layerName, string[] validTags, string error)
        {
            GameObject layer = GameObject.FindGameObjectWithTag(layerName);
            if (!layer) return;

            foreach (Transform rootObject in layer.transform)
            {
                bool isValid = false;

                foreach (string tag in validTags)
                {
                    if (rootObject.tag == tag) isValid = true;
                }

                if (isValid) continue;

                foreach (Transform child in rootObject)
                {
                    foreach (string tag in validTags)
                    {
                        if (child.tag != tag) continue;
                        isValid = true;
                        break;
                    }
                }

                if (!isValid) AddError(error, rootObject.GetInstanceID());
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
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(objectTag);
            foreach (GameObject sceneObject in gameObjects)
            {
                Collider collider = sceneObject.GetComponent<T>() as Collider;
                if (collider != null || collider.enabled || !collider.isTrigger) continue;
                AddError(error, sceneObject.GetInstanceID());
            }
        }
    }
}
#endif