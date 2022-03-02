using MoreMountains.Feedbacks;
using UnityEngine;

namespace Player.Data
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/ Base Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Move State")] public float movementVelocity = 10f;

        [Header("Wall Jump State")] public float wallJumpVelocity = 20f;
        public float wallJumpTime = 0.4f;
        public Vector2 wallJumpAngle = new Vector2(1, 2);

        [Header("Jump State")] public float jumpVelocity = 15;
        public int amountOfJumps = 1;

        [Header("Wall Slide State")] public float wallSlideVelocity = 3f;

        [Header("In Air State")] public float coyoteTime = 0.2f;
        public float jumpHeightMultiplier = 0.5f;

        [Header("Dash State")] 
        public float dashCooldown = 0.5f;
        public float dashTime = 0.2f;
        public float dashVelocity = 30f;
        public float drag = 10f;
        public float dashEndYMultiplier = 0.2f;
        public float distanceBetweenAfterImages = 0.5f;

        [Header("Time")] 
        public float timeDilationCooldown = 0.5f;
        public float timeDilationTimeScale = 0.25f;
        public float maxTimeDilationHoldTime = 3f;

        [Header("Rewind Time")]
        public float reverseTimeCooldown = 5f;
        public float maxReverseTimeHoldTime = 5f;

    }
}