using UnityEngine;

namespace Mona
{
    public partial class MonaPlatform : MonoBehaviour
    {
        public void Awake()
        {
            OnPlatformAwake(gameObject);
        }

        // On enter event
        public void OnTriggerEnter(Collider collider)
        {
            OnPlatformEnter(collider);
        }

        // On exit event
        public void OnTriggerExit(Collider collider)
        {
            OnPlatformLeave(collider);
        }
    }
}