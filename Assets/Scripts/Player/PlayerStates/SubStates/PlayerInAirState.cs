using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    // Input
    private int _xInput;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _dashInput;

    // Checks
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _coyoteTime;
    private bool _isJumping;

    private readonly int _yVelocity = Animator.StringToHash("yVelocity");
    private readonly int _xVelocity = Animator.StringToHash("xVelocity");

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
        string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        _xInput = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;
        _jumpInputStop = Player.InputHandler.JumpInputStop;
        _dashInput = Player.InputHandler.DashInput;

        CheckJumpMultiplier();

        if (Player.InputHandler.AttackInputs[(int) CombatInputs.Primary])
        {
            StateMachine.ChangeState(Player.PrimaryAttackState);
        }
        else if (Player.InputHandler.AttackInputs[(int) CombatInputs.Secondary])
        {
            StateMachine.ChangeState(Player.SecondaryAttackState);
        }
        else if (_isGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.LandState);
        }
        else if (_jumpInput && Player.WallSlideState.IsWallSliding)
        {
            Player.InputHandler.UseJumpInput();
            _isTouchingWall = Core.CollisionSenses.WallFront;
            Player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            StateMachine.ChangeState(Player.WallJumpState);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            Player.InputHandler.UseJumpInput();
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (_isTouchingWall && _xInput == Core.Movement.FacingDirection && Player.Rb.velocity.y < 0)
        {
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else if (_dashInput && Player.DashState.CheckIfCanDash())
        {
            StateMachine.ChangeState(Player.DashState);
        }
        else
        {
            Core.Movement.CheckIfShouldFlip(_xInput);
            Core.Movement.SetVelocityX(PlayerData.movementVelocity * _xInput);

            Player.Anim.SetFloat(_yVelocity, Core.Movement.CurrentVelocity.y);
            Player.Anim.SetFloat(_xVelocity, Mathf.Abs(Core.Movement.CurrentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * PlayerData.jumpHeightMultiplier);
                _isJumping = false;
            }
            else if (Core.Movement.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > StartTime + PlayerData.coyoteTime)
        {
            _coyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void SetIsJumping() => _isJumping = true;


    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = Core.CollisionSenses.Ground;
        _isTouchingWall = Core.CollisionSenses.WallFront;
    }
}