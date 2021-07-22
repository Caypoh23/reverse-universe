using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components

    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public GameObject AliveGO { get; private set; }
    public AnimationToStateMachine AnimationToState { get; private set; }

    #endregion

    #region State Machine

    public FiniteStateMachine StateMachine;

    [SerializeField] private D_Entity entityData;

    #endregion

    #region Variables and Check Transform

    public int FacingDirection { get; private set; }


    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;

    private Vector2 _velocityWorkspace;

    #endregion

    #region Unity Callback Functions

    public virtual void Start()
    {
        FacingDirection = 1;

        AliveGO = transform.Find("Alive").gameObject;
        Rb = AliveGO.GetComponent<Rigidbody2D>();
        Anim = AliveGO.GetComponent<Animator>();
        AnimationToState = AliveGO.GetComponent<AnimationToStateMachine>();

        StateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    public virtual void SetVelocity(float velocity)
    {
        _velocityWorkspace.Set(FacingDirection * velocity, Rb.velocity.y);
        Rb.velocity = _velocityWorkspace;
    }

    #region Check Functions

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, AliveGO.transform.right, entityData.wallCheckDistance,
            entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance,
            entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGO.transform.right, entityData.minAgroDistance,
            entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGO.transform.right, entityData.maxAgroDistance,
            entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, AliveGO.transform.right, entityData.closeRangeActionDistance,
            entityData.whatIsPlayer);
    }

    #endregion


    public virtual void Flip()
    {
        FacingDirection *= -1;
        AliveGO.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position,
            wallCheck.position + (Vector3) (Vector2.right * FacingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position,
            ledgeCheck.position + (Vector3) (Vector2.down * entityData.ledgeCheckDistance));
        Gizmos.DrawWireSphere(
            playerCheck.position + (Vector3) (Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(
            playerCheck.position + (Vector3) (Vector2.right * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(
            playerCheck.position + (Vector3) (Vector2.right * entityData.maxAgroDistance), 0.2f);
    }
}