using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }


    [SerializeField] private PlayerData playerData;

    #endregion

    #region Components

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb { get; private set; }

    #endregion

    #region Check Transforms

    [SerializeField] private Transform groundCheck;
    

    #endregion

    #region Other Variables

    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 _workspace;

    #endregion

    #region Unity Callback Functions

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rb = GetComponent<Rigidbody2D>();

        FacingDirection = 1;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = Rb.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

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
    

    #endregion

    #region Check Functions

    // вывести логику в отдельный скрипт 
    // player isGrounded state
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    
    // вынести логику в отдельный скрипт
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    

    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishedTrigger() => StateMachine.CurrentState.AnimationFinishedTrigger();
    
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion
}