using UnityEngine;

namespace Cores.CoreComponents
{
    public class CollisionSenses : CoreComponent
    {
        #region Check Transforms

        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheck;

        [SerializeField] private float groundCheckRadius;
        [SerializeField] private float wallCheckDistance;

        [SerializeField] private LayerMask whatIsGround;

        #endregion

        #region Check Functions

        public bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        public bool IsCheckingWall =>
            Physics2D.Raycast(wallCheck.position, Vector2.right * Core.Movement.FacingDirection, wallCheckDistance,
                whatIsGround);

        public bool IsCheckingLedge => Physics2D.Raycast(ledgeCheck.position, Vector2.down, wallCheckDistance, whatIsGround);

        #endregion
    }
}