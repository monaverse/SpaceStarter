using UnityEngine;

namespace Mona
{
    public partial class MonaWarp : MonoBehaviour
    {
        [Tooltip("The point in space to warp the player")]
        public GameObject WarpPoint;

        [Tooltip("Reset the rotation of the player to the warp point rotation")]
        public bool UseRotation;
    }
}