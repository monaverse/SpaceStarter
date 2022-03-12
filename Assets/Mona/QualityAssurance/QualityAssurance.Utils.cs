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
        public static Dictionary<string, string> ErrorDescriptionMap;

        // Recursively check all gameobjects and their children's children
        public static GameObject[] GetAllChildren(GameObject parent)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in parent.transform)
            {
                children.Add(child.gameObject);
                children.AddRange(GetAllChildren(child.gameObject));
            }
            return children.ToArray();
        }

        public static GameObject FindParentWithTag(String tagToFind, GameObject startingObject)
        {
            var parent = startingObject.transform.parent;
            while (parent != null)
            {
                if (parent.tag == tagToFind)
                {
                    return parent.gameObject as GameObject;
                }
                parent = parent.transform.parent;
            }
            return null;
        }

        public static void InitDescriptions()
        {
            if (ErrorDescriptionMap != null) return;
            ErrorDescriptionMap = MonaErrorCodes.GetErrorDescriptionMap();
        }

        public static string GetErrorDescription(string error)
        {
            InitDescriptions();

            if (!ErrorDescriptionMap.ContainsKey(error)) return "";

            return ErrorDescriptionMap[error];
        }

        public static void AddError(string error, int objectID)
        {
            if (SpaceErrors.ContainsKey(error))
            {
                SpaceErrors[error].Add(objectID);
                return;
            }

            List<int> objectIDs = new List<int>();
            objectIDs.Add(objectID);
            SpaceErrors.Add(error, objectIDs);
        }
    }
}
#endif