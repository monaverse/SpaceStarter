// Stubs
using UnityEngine;

namespace Mona
{

    public partial class MonaReactor
    {
        public void ReactorAwake() { }
        public void ReactorOnTriggerEnter(Collider eventObject) { }
        public void ReactorOnTriggerExit(Collider eventObject) { }
    }

    public partial class MonaPlatform
    {
        public void OnPlatformAwake(GameObject gameObject) { }
        public void OnPlatformEnter(Collider collider) { }
        public void OnPlatformLeave(Collider collider) { }
    }
}