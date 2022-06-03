using UnityEngine;

namespace Mona
{
    // Operation intended to be preform on the target property
    [System.Serializable]
    public enum OperationType
    {
        // Overwrites the target property with the value
        Set,
        // Adds the value to the target property
        Addition,
        // Subtracts the value from the target property
        Subtraction,
        // Multiplies the value to the target property
        Multiplication,
        // Divides the value from the target property
        Division,
        // Flips the target property to the opposite value
        Invert,
    }

    [System.Serializable]
    public enum ValueType
    {
        // The target property is a int
        Int,
        // The target property is a float
        Float,
        // The target property is a boolean
        Boolean,
        // Trigger
        Trigger
    }

    // Set of supported components
    [System.Serializable]
    public enum TargetComponent
    {
        // Animator controller with event system properties
        Animator
    }

    // Core event type
    [System.Serializable]
    public struct MonaEvent
    {
        // Event name, only an ease of use for the editor display
        public string Name;

        // Operation to be performed on the target property
        public OperationType Operation;

        // Target game object
        public GameObject Object;

        // Target component type (Not implemented yet)
        private TargetComponent Component;

        // Target Parameter
        public string Parameter;

        // Value type
        public ValueType ValueType;

        // Value to be used in the operation. Will be ignored if the operation is Toggle
        public string Value;

        // If to keep this event local
        public bool Local;
    }

    // Collider events
    [System.Serializable]
    public struct ColliderEvent
    {
        [Tooltip("The label name shown to the player")]
        public string Name;

        [Tooltip("List of events to be executed when a player looks at this trigger")]
        public Collider Collider;

        [Tooltip("Normal Reactor event result")]
        public MonaEvent InteractEvent;

    }

    // Collider events
    [System.Serializable]
    public struct LookEvent
    {
        [Tooltip("Internal name of this event")]
        public string Name;

        [Tooltip("List of events to be executed when a player looks at this trigger")]
        public Collider Collider;

        [Tooltip("Normal Reactor event result")]
        public MonaEvent OnLookStart;

        [Tooltip("Normal Reactor event result")]
        public MonaEvent OnLookEnd;
    }

    public partial class MonaReactor : MonoBehaviour
    {
        // Target API
        [HideInInspector]
        public string Version = "1.5.0";

        // Internal reference to self
        private BoxCollider Collider;

        // List of events to be executed
        private string[] EventGeneratorTags = { "Player" };

        // Fired when a GeneratorTarget has touched the collider
        [Tooltip("List of events to be executed when a GeneratorTarget has touched the collider")]
        public MonaEvent[] OnEnterTrigger;

        // Fired when a GeneratorTarget has left the collider
        [Tooltip("List of events to be executed when a GeneratorTarget has left the collider")]
        public MonaEvent[] OnExitTrigger;

        /*
        // When a player looks at this trigger and presses the interact button
        [Tooltip("List of events to be executed when a player looks at this trigger and presses the interact button")]
        public ColliderEvent[] OnPlayerInteract;

        // On Player look at this trigger
        [Tooltip("List of events to be executed when a player looks at this trigger")]
        public LookEvent[] OnPlayerLookAt;
        */

    }
}