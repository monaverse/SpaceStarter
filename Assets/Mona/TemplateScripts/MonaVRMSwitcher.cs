using UnityEngine;

namespace Mona
{
    public partial class MonaVRMSwitcher : MonoBehaviour
    {
        [Tooltip("The url of the Vrm to equip when the player interacts with this collider")]
        public string VrmUrl;

        [Tooltip("The name of the Vrm to show in the prompt")]
        public string VrmName;
    }
}