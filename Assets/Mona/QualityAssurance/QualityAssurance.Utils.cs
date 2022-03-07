using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Mona
{
    // Quailty Checks that run both in editor and in private-build
    public static partial class QualityAssurance
    {
        public static Dictionary<string, string> ErrorDescriptions;

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

        public static void InitDescriptions(){
            if(ErrorDescriptions != null) return;
            ErrorDescriptions = new Dictionary<string, string>();
            ErrorDescriptions.Add(MonaErrorCodes.MISSING_SPACE_LAYER, MonaErrorCodes.MISSING_SPACE_LAYER_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.MISSING_ARTIFACT_LAYER, MonaErrorCodes.MISSING_ARTIFACT_LAYER_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.MISSING_PORTAL_LAYER, MonaErrorCodes.MISSING_PORTAL_LAYER_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.MISSING_SPACE_SCENE, MonaErrorCodes.MISSING_SPACE_SCENE_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.MISSING_ARTIFACT_SCENE, MonaErrorCodes.MISSING_ARTIFACT_SCENE_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.MISSING_PORTAL_SCENE, MonaErrorCodes.MISSING_PORTAL_SCENE_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.MULTIPLE_SPACE_ROOTS, MonaErrorCodes.MULTIPLE_SPACE_ROOTS_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.MULTIPLE_ARTIFACT_ROOTS, MonaErrorCodes.MULTIPLE_ARTIFACT_ROOTS_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.MULTIPLE_PORTAL_ROOTS, MonaErrorCodes.MULTIPLE_PORTAL_ROOTS_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.BAD_ARTIFACT_PLACEMENT, MonaErrorCodes.BAD_ARTIFACT_PLACEMENT_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.BAD_PORTAL_PLACEMENT, MonaErrorCodes.BAD_PORTAL_PLACEMENT_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.BAD_CANVAS_PLACEMENT, MonaErrorCodes.BAD_CANVAS_PLACEMENT_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.DUPLICATE_ARTIFACT_NAME, MonaErrorCodes.DUPLICATE_ARTIFACT_NAME_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.DUPLICATE_PORTAL_NAME, MonaErrorCodes.DUPLICATE_PORTAL_NAME_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.DUPLICATE_CANVAS_NAME, MonaErrorCodes.DUPLICATE_CANVAS_NAME_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.BAD_PORTAL_LAYER_CONTENTS, MonaErrorCodes.BAD_PORTAL_LAYER_CONTENTS_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.BAD_PORTAL_COLLIDER, MonaErrorCodes.BAD_PORTAL_COLLIDER_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.BAD_ARTIFACT_COLLIDER, MonaErrorCodes.BAD_ARTIFACT_COLLIDER_DESCRIPTION);
            ErrorDescriptions.Add(MonaErrorCodes.BAD_CANVAS_COLLIDER, MonaErrorCodes.BAD_CANVAS_COLLIDER_DESCRIPTION);
        }

        public static string GetErrorDescription(string error){
            InitDescriptions();

            if(!ErrorDescriptions.ContainsKey(error)) return "";
            
            return ErrorDescriptions[error];

        }
    }
}