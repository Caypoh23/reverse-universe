using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms

    public Transform GroundCheck
    {
        get => groundCheck;
        private set => groundCheck = value;
    }

    public Transform WallCheck
    {
        get => wallCheck;
        private set => wallCheck = value;
    }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;

    [SerializeField] private LayerMask whatIsGround;

    #endregion

    #region Check Functions

    public bool Ground => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    public bool WallFront =>
        Physics2D.Raycast(wallCheck.position, Vector2.right * Core.Movement.FacingDirection, wallCheckDistance,
            whatIsGround);

    #endregion
}