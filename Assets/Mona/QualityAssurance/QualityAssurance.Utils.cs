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
        // Recursively check all gameobjects and their children's children
        public static GameObject[] GetAllChildren(GameObject parent)
        {
            List<GameObject> _children = new List<GameObject>();
            foreach (Transform _child in parent.transform)
            {
                _children.Add(_child.gameObject);
                _children.AddRange(GetAllChildren(_child.gameObject));
            }
            return _children.ToArray();
        }

        public static GameObject FindParentWithTag(String tagToFind, GameObject startingObject)
        {
            var _parent = startingObject.transform.parent;
            while (_parent != null)
            {
                if (_parent.tag == tagToFind)
                {
                    return _parent.gameObject as GameObject;
                }
                _parent = _parent.transform.parent;
            }
            return null;
        }
    }
}

#endif