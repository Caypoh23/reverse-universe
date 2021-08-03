using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected bool JumpInput;
    protected int XInput;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine,
        PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        XInput = Player.InputHandler.NormalizedInputX;
        JumpInput = Player.InputHandler.JumpInput;

        if (JumpInput)
        {
            Player.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
            StateMachine.ChangeState(Player.WallJumpState);
        }
        else if (IsGrounded)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if (!IsTouchingWall || XInput != Core.Movement.FacingDirection && Core.Movement.CurrentVelocity.y <= 0)
        {
            StateMachine.ChangeState(Player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsGrounded = Core.CollisionSenses.Ground;
        IsTouchingWall = Core.CollisionSenses.WallFront;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}