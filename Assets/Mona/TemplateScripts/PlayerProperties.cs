using UnityEngine;

namespace Mona
{
    [System.Serializable]
    public class PlayerProperties
    {
        [HideInInspector]
        public string Version = "0.0.1";

        public bool OverrideGravity = false;
        // Fall speed of players in this volume
        [Tooltip("(Default -15f) - Fall speed of players in this volume")]
        public float Gravity = -1;

        // The upward force applied to the player when jumping
        [Tooltip("(Default 6.5f) - The upward force applied to the player when jumping")]
        public float JumpHeight = -1;

        // The speed at which the player moves
        [Tooltip("(Default 2.7f) - The speed at which the player moves")]
        public float WalkSpeed = -1;

        // The speed at which the player moves when sprinting
        [Tooltip("(Default 10f) - The speed at which the player moves when sprinting")]
        public float SprintSpeed = -1f;

        // The post processing strength of the Bloom effect
        [Tooltip("The post processing strength of the bloom effect")]
        public float Bloom = -1f;

        // The post processing strength of the Chromatic Aberration effect
        [Tooltip("The post processing strength of the Chromatic Aberration effect")]
        public float ChromaticAberration = -1f;

        // The post processing strength of the MotionBlur
        [Tooltip("The post processing strength of the MotionBlur")]
        public float MotionBlur = -1f;
    }
}