using UnityEngine;

namespace Mona
{
    public partial class MonaWarp : MonoBehaviour
    {
        [Tooltip("The point in space to warp the player")]
        public GameObject WarpPoint;

        [Tooltip("Reset the rotation of the player to the warp point rotation")]
        public bool UseRotation;

        [Tooltip("Should this warp be triggered when the player interacts (this will only work if the volume is not a trigger volume).")]
        public bool OnInteract;
    }
}