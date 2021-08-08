using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D Rb { get; private set; }
    
    public int FacingDirection { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 _workspace;

    protected override void Awake()
    {
        base.Awake();
        FacingDirection = 1;
        
        Rb = GetComponentInParent<Rigidbody2D>();
    }

    public void LogicUpdate()
    {
        CurrentVelocity = Rb.velocity;
    }

    #region Set Functions

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rb.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspace = direction * velocity;
        Rb.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        Rb.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    public void SetVelocityY(float velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);
        Rb.velocity = _workspace;
        CurrentVelocity = _workspace;
    }
    
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    
    public void Flip()
    {
        FacingDirection *= -1;
        Rb.transform.Rotate(0.0f, 180.0f, 0.0f);
    }


    #endregion
}