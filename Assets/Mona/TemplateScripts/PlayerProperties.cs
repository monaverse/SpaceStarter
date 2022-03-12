using UnityEngine;

namespace Mona
{
    [System.Serializable]
    public class PlayerProperties
    {
        [HideInInspector]
        public int PropertiesVersion = 1;

        // Fall speed of players in this volume
        [Tooltip("Fall speed of players in this volume")]
        public float Gravity = -15f;

        // The upward force applied to the player when jumping
        [Tooltip("The upward force applied to the player when jumping")]
        public float JumpHeight = -1;

        // The speed at which the player moves
        [Tooltip("The speed at which the player moves")]
        public float WalkSpeed = -1;

        // The speed at which the player moves when sprinting
        [Tooltip("The speed at which the player moves when sprinting")]
        public float SprintSpeed = -1;

        // The post processing strength of the Bloom effect
        public bool OverrideBloom = false;
        [Tooltip("The post processing strength of the bloom effect")]
        public float Bloom = 0f;

        // The post processing strength of the Chromatic Aberration effect
        public bool OverrideChromaticAberration = false;
        [Tooltip("The post processing strength of the Chromatic Aberration effect")]
        public float ChromaticAberration = 0f;

        // The post processing strength of the MotionBlur
        public bool OverrideMotionBlur = false;
        [Tooltip("The post processing strength of the MotionBlur")]
        public float MotionBlur = 0f;
    }
}